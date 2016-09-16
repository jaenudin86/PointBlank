// ZipFile.saveSelfExtractor.cs
// ------------------------------------------------------------------
//
// Copyright (c)  2008, 2009 Dino Chiesa.  
// All rights reserved.
//
// This code module is part of DotNetZip, a zipfile class library.
//
// ------------------------------------------------------------------
//
// This code is licensed under the Microsoft Public License. 
// See the file License.txt for the license details.
// More info on: http://dotnetzip.codeplex.com
//
// ------------------------------------------------------------------
//
// last saved (in emacs): 
// Time-stamp: <2009-August-25 13:01:56>
//
// ------------------------------------------------------------------
//
// This is a the source module that implements the stuff for saving to a
// self-extracting Zip archive.
// 
// ZipFile is set up as a "partial class" - defined in multiple .cs source modules.
// This is one of the source modules for the ZipFile class.
//
// Here's the design: The self-extracting zip file is just a regular managed EXE
// file, with embedded resources.  The managed code logic instantiates a ZipFile, and
// then extracts each entry.  The embedded resources include the zip archive content,
// as well as the Zip library itself.  The latter is required so that self-extracting
// can work on any machine, whether or not it has the DotNetZip library installed on
// it.
// 
// What we need to do is create the animal I just described, within a method on the
// ZipFile class.  This source module provides that capability. The method is
// SaveSelfExtractor().
//
// The way the method works: it uses the programmatic interface to the csc.exe
// compiler, Microsoft.CSharp.CSharpCodeProvider, to compile "boilerplate" extraction
// logic into a new assembly.  As part of that compile, we embed within that assembly the zip archive
// itself, as well as the Zip library. 
//
// Therefore we need to first save to a temporary zip file, then produce the exe.  
//
// There are a few twists.  
//
// The Visual Studio Project structure is a little weird.  There are code files that ARE NOT compiled
// during a normal build of the VS Solution.  They are marked as embedded resources.  These
// are the various "boilerplate" modules that are used in the self-extractor. These modules are:
//   WinFormsSelfExtractorStub.cs
//   WinFormsSelfExtractorStub.Designer.cs
//   CommandLineSelfExtractorStub.cs
//   PasswordDialog.cs
//   PasswordDialog.Designer.cs
//
// At design time, if you want to modify the way the GUI looks, you have to mark those modules
// to have a "compile" build action.  Then tweak em, test, etc.  Then again mark them as 
// "Embedded resource". 
//
// ------------------------------------------------------------------

using System;
using System.Reflection;
using System.IO;
using System.Collections.Generic;


namespace Ionic.Zip
{
#if !NO_SFX
    /// <summary>
    /// An enum that provides the different self-extractor flavors
    /// </summary>
    public enum SelfExtractorFlavor
    {
        /// <summary>
        /// A self-extracting zip archive that runs from the console or command line. 
        /// </summary>
        ConsoleApplication = 0,

        /// <summary>
        /// A self-extracting zip archive that presents a graphical user interface when it is executed.. 
        /// </summary>
        WinFormsApplication,
    }


    partial class ZipFile
    {
        class ExtractorSettings
        {
            public SelfExtractorFlavor Flavor;
            public List<string> ReferencedAssemblies;
            public List<string> CopyThroughResources;
            public List<string> ResourcesToCompile;
        }


        private static ExtractorSettings[] SettingsList = {
            new ExtractorSettings() {
                Flavor = SelfExtractorFlavor.WinFormsApplication,
                ReferencedAssemblies= new List<string>{
                    "System.dll", "System.Windows.Forms.dll", "System.Drawing.dll"},
                CopyThroughResources = new List<string>{
                    "Ionic.Zip.WinFormsSelfExtractorStub.resources",
                    "Ionic.Zip.PasswordDialog.resources",
                    "Ionic.Zip.ZipContentsDialog.resources"},
                ResourcesToCompile = new List<string>{
                    "Ionic.Zip.Resources.WinFormsSelfExtractorStub.cs",
                    "Ionic.Zip.WinFormsSelfExtractorStub", // .Designer.cs
                    "Ionic.Zip.Resources.PasswordDialog.cs",
                    "Ionic.Zip.PasswordDialog",             //.Designer.cs"
                    "Ionic.Zip.Resources.ZipContentsDialog.cs",
                    "Ionic.Zip.ZipContentsDialog",             //.Designer.cs"
                    "Ionic.Zip.Resources.FolderBrowserDialogEx.cs",
                }
            },
            new ExtractorSettings() {
                Flavor = SelfExtractorFlavor.ConsoleApplication,
                ReferencedAssemblies= new List<string> { "System.dll", },
                CopyThroughResources = null,
                ResourcesToCompile = new List<string>{"Ionic.Zip.Resources.CommandLineSelfExtractorStub.cs"}
            }
        };


#if OLDSTYLE
        private string SfxSaveTemporary()
        {
            var tempFileName = System.IO.Path.Combine(TempFileFolder, System.IO.Path.GetRandomFileName() + ".zip");
            Stream outstream = null;
            try
            {
                bool save_contentsChanged = _contentsChanged;
                outstream = new System.IO.FileStream(tempFileName, System.IO.FileMode.CreateNew);
                if (outstream == null)
                    throw new BadStateException(String.Format("Cannot open the temporary file ({0}) for writing.", tempFileName));
                if (Verbose) StatusMessageTextWriter.WriteLine("Saving temp zip file....");
                // write an entry in the zip for each file
                int n = 0;
                foreach (ZipEntry e in _entries)
                {
                    OnSaveEntry(n, e, true);
                    e.Write(outstream);
                    n++;
                    OnSaveEntry(n, e, false);
                    if (_saveOperationCanceled)
                        break;
                }

                if (!_saveOperationCanceled)
                {
                    WriteCentralDirectoryStructure(outstream);
                    outstream.Close();
                    outstream = null;
                }
                _contentsChanged = save_contentsChanged;
            }

            finally
            {
                if (outstream != null)
                {
                    try { outstream.Close(); }
                    catch { }
                    try { outstream.Dispose(); }
                    catch { }
                }
            }
            return tempFileName;
        }
#endif


        //string _defaultExtractLocation;
        //string _postExtractCmdLine;
        //         string _SetDefaultLocationCode =
        //         "namespace Ionic.Zip { public partial class WinFormsSelfExtractorStub { partial void _SetDefaultExtractLocation() {" +
        //         " txtExtractDirectory.Text = \"@@VALUE\"; } }}";



        /// <summary>
        /// Saves the ZipFile instance to a self-extracting zip archive.
        /// </summary>
        /// 
        /// <remarks>
        /// 
        /// <para>
        /// The generated exe image will execute on any machine that has the .NET Framework 2.0
        /// installed on it.  The generated exe image is also a valid ZIP file, readable with DotNetZip
        /// or another Zip library or tool such as WinZip. 
        /// </para>
        /// 
        /// <para>
        /// There are two "flavors" of self-extracting archive.  The <c>WinFormsApplication</c>
        /// version will pop up a GUI and allow the user to select a target directory into which
        /// to extract. There's also a checkbox allowing the user to specify to overwrite
        /// existing files, and another checkbox to allow the user to request that Explorer be
        /// opened to see the extracted files after extraction.  The other flavor is
        /// <c>ConsoleApplication</c>.  A self-extractor generated with that flavor setting will
        /// run from the command line. It accepts command-line options to set the overwrite
        /// behavior, and to specify the target extraction directory.
        /// </para>
        /// 
        /// <para>
        /// There are a few temporary files created during the saving to a self-extracting zip.
        /// These files are created in the directory pointed to by <see
        /// cref="ZipFile.TempFileFolder"/>, which defaults to <see
        /// cref="System.IO.Path.GetTempPath"/>.  These temporary files are removed upon
        /// successful completion of this method.
        /// </para>
        ///
        /// <para>
        /// When a user runs the WinForms SFX, the user's personal directory (<see
        /// cref="Environment.SpecialFolder.Personal"/>) will be used as the default extract
        /// location.  The user who runs the SFX will have the opportunity to change the extract
        /// directory before extracting. When the user runs the Command-Line SFX, the user must
        /// explicitly specify the directory to which to extract.  The .NET Framework 2.0 is
        /// required on the computer when the self-extracting archive is run.
        /// </para>
        ///
        /// <para>
        /// NB: This method is not available in the version of DotNetZip
        /// build for the .NET Compact Framework, nor in the "Reduced" DotNetZip library.  
        /// </para>
        /// 
        /// </remarks>
        /// 
        /// <example>
        /// <code>
        /// string DirectoryPath = "c:\\Documents\\Project7";
        /// using (ZipFile zip = new ZipFile())
        /// {
        ///     zip.AddDirectory(DirectoryPath, System.IO.Path.GetFileName(DirectoryPath));
        ///     zip.Comment = "This will be embedded into a self-extracting console-based exe";
        ///     zip.SaveSelfExtractor("archive.exe", SelfExtractorFlavor.ConsoleApplication);
        /// }
        /// </code>
        /// <code lang="VB">
        /// Dim DirectoryPath As String = "c:\Documents\Project7"
        /// Using zip As New ZipFile()
        ///     zip.AddDirectory(DirectoryPath, System.IO.Path.GetFileName(DirectoryPath))
        ///     zip.Comment = "This will be embedded into a self-extracting console-based exe"
        ///     zip.SaveSelfExtractor("archive.exe", SelfExtractorFlavor.ConsoleApplication)
        /// End Using
        /// </code>
        /// </example>
        /// 
        /// <param name="exeToGenerate">a pathname, possibly fully qualified, to be created. Typically it will end in an .exe extension.</param>
        /// <param name="flavor">Indicates whether a Winforms or Console self-extractor is desired.</param>
        public void SaveSelfExtractor(string exeToGenerate, SelfExtractorFlavor flavor)
        {
            SaveSelfExtractor(exeToGenerate, flavor, null, null, null);
        }

        

        /// <summary>
        /// Saves the ZipFile instance to a self-extracting zip archive, using the specified 
        /// default extract directory. 
        /// </summary>
        /// <remarks>
        /// <para>
        /// This method saves a self extracting archive, with a specified default extracting
        /// location.  Actually, the default extract directory applies only if the flavor is <see
        /// cref="SelfExtractorFlavor.WinFormsApplication"/>.  See the documentation for <see
        /// cref="SaveSelfExtractor(string , SelfExtractorFlavor)"/> for more details.
        /// </para>
        ///
        /// <para>
        /// The user who runs the SFX will have the opportunity to change the extract directory
        /// before extracting.  If at the time of extraction, the specified directory does not
        /// exist, the SFX will create the directory before extracting the files.
        /// </para>
        /// </remarks>
        ///
        /// <example>
        /// This example saves a self-extracting archive that will use c:\ExtractHere as the default 
        /// extract location.
        /// <code>
        /// string DirectoryPath = "c:\\Documents\\Project7";
        /// using (ZipFile zip = new ZipFile())
        /// {
        ///     zip.AddDirectory(DirectoryPath, System.IO.Path.GetFileName(DirectoryPath));
        ///     zip.Comment = "This will be embedded into a self-extracting console-based exe";
        ///     zip.SaveSelfExtractor("archive.exe", SelfExtractorFlavor.ConsoleApplication, "c:\\ExtractHere");
        /// }
        /// </code>
        /// <code lang="VB">
        /// Dim DirectoryPath As String = "c:\Documents\Project7"
        /// Using zip As New ZipFile()
        ///     zip.AddDirectory(DirectoryPath, System.IO.Path.GetFileName(DirectoryPath))
        ///     zip.Comment = "This will be embedded into a self-extracting console-based exe"
        ///     zip.SaveSelfExtractor("archive.exe", SelfExtractorFlavor.ConsoleApplication, "c:\ExtractHere");
        /// End Using
        /// </code>
        /// </example>
        /// 
        /// <param name="exeToGenerate">The name of the EXE to generate.</param>
        /// <param name="flavor">Indicates whether a Winforms or Console self-extractor is desired.</param>
        /// <param name="defaultExtractDirectory">
        /// The default extract directory the user will see when running the self-extracting 
        /// archive. Passing null (or Nothing in VB) here will cause the Self Extractor to 
        /// use the the user's personal directory 
        /// (<see cref="Environment.SpecialFolder.Personal"/>) for the default extract 
        /// location.
        /// </param>
        public void SaveSelfExtractor(string exeToGenerate, SelfExtractorFlavor flavor, string defaultExtractDirectory)
        {
            SaveSelfExtractor(exeToGenerate, flavor, defaultExtractDirectory, null, null);
        }


        
        /// <summary>
        /// Saves the ZipFile instance to a self-extracting zip archive, using the specified 
        /// default extract directory, and a post-extract command to run.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This method saves a self extracting archive, with a specified default
        /// extracting location, and a command to run after extraction.  Actually, the
        /// default extract directory applies only if the flavor is <see
        /// cref="SelfExtractorFlavor.WinFormsApplication"/>.  See the documentation for
        /// <see cref="SaveSelfExtractor(string , SelfExtractorFlavor)"/> for more
        /// details.
        /// </para>
        ///
        /// <para>
        /// The user who runs the SFX will have the opportunity to change the extract directory
        /// before extracting.  If at the time of extraction, the specified directory does not
        /// exist, the SFX will create the directory before extracting the files.
        /// </para>
        /// </remarks>
        ///
        /// <example>
        /// This example saves a self-extracting archive that will use c:\ExtractHere as the default 
        /// extract location.
        /// <code>
        /// string DirectoryPath = "c:\\Documents\\Project7";
        /// using (ZipFile zip = new ZipFile())
        /// {
        ///     zip.AddDirectory(DirectoryPath, System.IO.Path.GetFileName(DirectoryPath));
        ///     zip.Comment = "This will be embedded into a self-extracting console-based exe";
        ///     zip.SaveSelfExtractor("archive.exe", SelfExtractorFlavor.ConsoleApplication, "c:\\ExtractHere");
        /// }
        /// </code>
        /// <code lang="VB">
        /// Dim DirectoryPath As String = "c:\Documents\Project7"
        /// Using zip As New ZipFile()
        ///     zip.AddDirectory(DirectoryPath, System.IO.Path.GetFileName(DirectoryPath))
        ///     zip.Comment = "This will be embedded into a self-extracting console-based exe"
        ///     zip.SaveSelfExtractor("archive.exe", SelfExtractorFlavor.ConsoleApplication, "c:\ExtractHere");
        /// End Using
        /// </code>
        /// </example>
        /// 
        /// <param name="exeToGenerate">The name of the EXE to generate.</param>
        /// <param name="flavor">Indicates whether a Winforms or Console self-extractor is desired.</param>
        /// <param name="defaultExtractDirectory">
        /// The default extract directory the user will see when running the self-extracting
        /// archive. Passing null (or Nothing in VB) here, if flavor is
        /// <c>SelfExtractorFlavor.WinFormsApplication</c>, will cause the Self Extractor to
        /// use the the user's personal directory (<see
        /// cref="Environment.SpecialFolder.Personal"/>) for the default extract location.
        /// Passing null when flavor is <c>SelfExtractorFlavor.ConsoleApplication</c> will
        /// cause the self-extractor to use the current directory for the default extract
        /// location; it will also be settable on the command line when the SFX is executed.
        /// </param>
        /// <param name ="postExtractCommandToExecute">
        /// The command to execute on the user's machine, after unpacking the archive. If the
        /// flavor is <c>SelfExtractorFlavor.ConsoleApplication</c>, then the SFX changes the
        /// current directory to the extract directory, and starts the post-extract command
        /// and waits for it to exit.  The exit code of the post-extract command line is
        /// returned as the exit code of the self-extractor exe.  A non-zero exit code is
        /// typically used to indicated a failure by the program. In the case of an SFX, a
        /// non-zero exit code may indicate a failure during extraction, OR, it may indicate a
        /// failure of the run-on-extract program if specified. There is no way to distinguish
        /// these conditions from the calling shell, aside from parsing output.  The GUI self
        /// extractor simply starts the post-extract command and exits; it does not wait for
        /// the command to exit first.
        /// </param>
        public void SaveSelfExtractor(string exeToGenerate, SelfExtractorFlavor flavor, string defaultExtractDirectory, string postExtractCommandToExecute)
        {
            SaveSelfExtractor(exeToGenerate, flavor, defaultExtractDirectory, postExtractCommandToExecute, null);
        }

        

        /// <summary>
        /// Saves the ZipFile instance to a self-extracting zip archive, using the specified 
        /// default extract directory, post-extract command, and icon. 
        /// </summary>
        /// <remarks>
        /// <para>
        /// This method saves a self extracting archive, with a specified default
        /// extracting location, a command to run after extraction, and application
        /// icon.  Actually, the default extract directory applies only if the flavor is
        /// <see cref="SelfExtractorFlavor.WinFormsApplication"/>.  See the
        /// documentation for <see cref="SaveSelfExtractor(string ,
        /// SelfExtractorFlavor)"/> for more details.
        /// </para>
        ///
        /// <para>
        /// The user who runs the SFX will have the opportunity to change the extract directory
        /// before extracting.  If at the time of extraction, the specified directory does not
        /// exist, the SFX will create the directory before extracting the files.
        /// </para>
        /// </remarks>
        ///
        /// <example>
        /// This example saves a self-extracting archive that will use c:\ExtractHere as the default 
        /// extract location.
        /// <code>
        /// string DirectoryPath = "c:\\Documents\\Project7";
        /// using (ZipFile zip = new ZipFile())
        /// {
        ///     zip.AddDirectory(DirectoryPath, System.IO.Path.GetFileName(DirectoryPath));
        ///     zip.Comment = "This will be embedded into a self-extracting console-based exe";
        ///     zip.SaveSelfExtractor("archive.exe", SelfExtractorFlavor.ConsoleApplication, "c:\\ExtractHere");
        /// }
        /// </code>
        /// <code lang="VB">
        /// Dim DirectoryPath As String = "c:\Documents\Project7"
        /// Using zip As New ZipFile()
        ///     zip.AddDirectory(DirectoryPath, System.IO.Path.GetFileName(DirectoryPath))
        ///     zip.Comment = "This will be embedded into a self-extracting console-based exe"
        ///     zip.SaveSelfExtractor("archive.exe", SelfExtractorFlavor.ConsoleApplication, "c:\ExtractHere");
        /// End Using
        /// </code>
        /// </example>
        /// 
        /// <param name="exeToGenerate">The name of the EXE to generate.</param>
        /// <param name="flavor">Indicates whether a Winforms or Console self-extractor is desired.</param>
        /// <param name="defaultExtractDirectory">
        /// The default extract directory the user will see when running the self-extracting
        /// archive. Passing null (or Nothing in VB) here, if flavor is
        /// <c>SelfExtractorFlavor.WinFormsApplication</c>, will cause the Self Extractor to
        /// use the the user's personal directory (<see
        /// cref="Environment.SpecialFolder.Personal"/>) for the default extract location.
        /// Passing null when flavor is <c>SelfExtractorFlavor.ConsoleApplication</c> will
        /// cause the self-extractor to use the current directory for the default extract
        /// location; it will also be settable on the command line when the SFX is executed.
        /// </param>
        /// <param name ="postExtractCommandToExecute">
        /// The command to execute on the user's machine, after unpacking the archive. If the
        /// flavor is <c>SelfExtractorFlavor.ConsoleApplication</c>, then the SFX changes the
        /// current directory to the extract directory, and starts the post-extract command
        /// and waits for it to exit.  The exit code of the post-extract command line is
        /// returned as the exit code of the self-extractor exe.  A non-zero exit code is
        /// typically used to indicated a failure by the program. In the case of an SFX, a
        /// non-zero exit code may indicate a failure during extraction, OR, it may indicate a
        /// failure of the run-on-extract program if specified. There is no way to distinguish
        /// these conditions from the calling shell, aside from parsing output.  The GUI self
        /// extractor simply starts the post-extract command and exits; it does not wait for
        /// the command to exit first.
        /// </param>
        /// <param name ="iconFile">
        /// the name of a .ico file in the filesystem to use for the application icon
        /// </param>
        public void SaveSelfExtractor(string exeToGenerate, SelfExtractorFlavor flavor, string defaultExtractDirectory, string postExtractCommandToExecute, string iconFile)
        {
            // Save an SFX that is both an EXE and a ZIP.

            // Check for the case where we are re-saving a zip archive 
            // that was originally instantiated with a stream.  In that case, 
            // the _name will be null. If so, we set _writestream to null, 
            // which insures that we'll cons up a new WriteStream (with a filesystem
            // file backing it) in the Save() method.
            if (_name == null)
                _writestream = null;

            _SavingSfx = true;
            _name = exeToGenerate;
            if (Directory.Exists(_name))
                throw new ZipException("Bad Directory", new System.ArgumentException("That name specifies an existing directory. Please specify a filename.", "exeToGenerate"));
            _contentsChanged = true;
            _fileAlreadyExists = File.Exists(_name);

            _SaveSfxStub(exeToGenerate, flavor, defaultExtractDirectory, postExtractCommandToExecute, iconFile);

            Save();
            _SavingSfx = false;
        }


        private void ExtractResourceToFile(Assembly a, string resourceName, string filename)
        {
            int n = 0;
            byte[] bytes = new byte[1024];
            using (Stream instream = a.GetManifestResourceStream(resourceName))
            {
                if (instream == null)
                    throw new ZipException(String.Format("missing resource '{0}'", resourceName));

                using (FileStream outstream = File.OpenWrite(filename))
                {
                    do
                    {
                        n = instream.Read(bytes, 0, bytes.Length);
                        outstream.Write(bytes, 0, n);
                    } while (n > 0);
                }
            }
        }


        private void _SaveSfxStub(string exeToGenerate, SelfExtractorFlavor flavor, string defaultExtractLocation, string postExtractCmdLine, string nameOfIconFile)
        {
            bool removeIconFile = false;
            string StubExe = null;
            string TempDir = null;
            try
            {
                if (File.Exists(exeToGenerate))
                {
                    if (Verbose) StatusMessageTextWriter.WriteLine("The existing file ({0}) will be overwritten.", exeToGenerate);
                }
                if (!exeToGenerate.EndsWith(".exe"))
                {
                    if (Verbose) StatusMessageTextWriter.WriteLine("Warning: The generated self-extracting file will not have an .exe extension.");
                }

                StubExe = GenerateTempPathname("exe", null);

                // get the Ionic.Zip assembly
                Assembly a1 = typeof(ZipFile).Assembly;

                Microsoft.CSharp.CSharpCodeProvider csharp = new Microsoft.CSharp.CSharpCodeProvider();

                // Perfect opportunity for a linq query, but I cannot use it.
                // The DotNetZip library can compile into 2.0, but needs to run on .NET 2.0.
                // Using LINQ would break that. Here's what it would look like: 
                // 
                //      var settings = (from x in SettingsList
                //                      where x.Flavor == flavor
                //                      select x).First();

                ExtractorSettings settings = null;
                foreach (var x in SettingsList)
                {
                    if (x.Flavor == flavor)
                    {
                        settings = x;
                        break;
                    }
                }

                if (settings == null)
                    throw new BadStateException(String.Format("While saving a Self-Extracting Zip, Cannot find that flavor ({0})?", flavor));

                // This is the list of referenced assemblies.  Ionic.Zip is needed here.
                // Also if it is the winforms (gui) extractor, we need other referenced assemblies,
                // like System.Windows.Forms.dll, etc.
                System.CodeDom.Compiler.CompilerParameters cp = new System.CodeDom.Compiler.CompilerParameters();
                cp.ReferencedAssemblies.Add(a1.Location);
                if (settings.ReferencedAssemblies != null)
                    foreach (string ra in settings.ReferencedAssemblies)
                        cp.ReferencedAssemblies.Add(ra);

                cp.GenerateInMemory = false;
                cp.GenerateExecutable = true;
                cp.IncludeDebugInformation = false;
                cp.CompilerOptions = "";                 

                Assembly a2 = Assembly.GetExecutingAssembly();
                
                if (nameOfIconFile==null)
                {
                    removeIconFile = true;
                    nameOfIconFile = GenerateTempPathname("ico", null);
                    ExtractResourceToFile(a2, "Ionic.Zip.Resources.zippedFile.ico", nameOfIconFile);
                    cp.CompilerOptions += String.Format("/win32icon:\"{0}\"", nameOfIconFile);
                }
                else if (nameOfIconFile!="")
                    cp.CompilerOptions += String.Format("/win32icon:\"{0}\"", nameOfIconFile);
                
                //cp.IncludeDebugInformation = true;
                cp.OutputAssembly = StubExe;
                if (flavor == SelfExtractorFlavor.WinFormsApplication)
                    cp.CompilerOptions += " /target:winexe";

                if (cp.CompilerOptions == "")
                    cp.CompilerOptions = null;

                TempDir = GenerateTempPathname("tmp", null);
                if ((settings.CopyThroughResources != null) && (settings.CopyThroughResources.Count != 0))
                {
                    System.IO.Directory.CreateDirectory(TempDir);
                    foreach (string re in settings.CopyThroughResources)
                    {
                        string filename = Path.Combine(TempDir, re);

                        ExtractResourceToFile(a2, re, filename);
                        // add the file into the target assembly as an embedded resource
                        cp.EmbeddedResources.Add(filename);
                    }
                }

                // add the Ionic.Utils.Zip DLL as an embedded resource
                cp.EmbeddedResources.Add(a1.Location);

                //Console.WriteLine("Resources in this assembly:");
                //foreach (string rsrc in a2.GetManifestResourceNames())
                //{
                //    Console.WriteLine(rsrc);
                //}
                //Console.WriteLine();

                //Console.WriteLine("reading source code resources:");


                // concatenate all the source code resources into a single module
                var sb = new System.Text.StringBuilder();

                // assembly attributes
                sb.Append("[assembly: System.Reflection.AssemblyTitle(\"DotNetZip SFX Archive\")]\n");
                sb.Append("[assembly: System.Reflection.AssemblyProduct(\"ZipLibrary\")]\n");
                sb.Append("[assembly: System.Reflection.AssemblyCopyright(\"Copyright © Dino Chiesa 2008, 2009\")]\n");
                sb.Append(String.Format("[assembly: System.Reflection.AssemblyVersion(\"{0}\")]\n\n", ZipFile.LibraryVersion.ToString()));
 

                // Set the default extract location if it is available, and if supported.

                bool haveLocation = (defaultExtractLocation != null);
                if (haveLocation)
                    defaultExtractLocation = defaultExtractLocation.Replace("\"", "").Replace("\\", "\\\\");

                foreach (string rc in settings.ResourcesToCompile)
                {
                    //Console.WriteLine("  trying to read stream: ({0})", rc);
                    Stream s = a2.GetManifestResourceStream(rc);
                    if (s == null)
                        throw new ZipException(String.Format("missing resource '{0}'", rc));
                    using (StreamReader sr = new StreamReader(s))
                    {
                        while (sr.Peek() >= 0)
                        {
                            string line = sr.ReadLine();
                            if (haveLocation)
                                line = line.Replace("@@EXTRACTLOCATION", defaultExtractLocation);
                            
                            if (postExtractCmdLine != null)
                                line = line.Replace("@@POST_UNPACK_CMD_LINE", postExtractCmdLine.Replace("\\","\\\\"));
                            
                            sb.Append(line).Append("\n");
                        }
                    }
                    sb.Append("\n\n");
                }

                string LiteralSource = sb.ToString();

                #if DEBUGSFX
                // for debugging only
                string sourceModule = GenerateTempPathname("cs", null);
                using (StreamWriter sw = File.CreateText(sourceModule))
                {
                    sw.Write(LiteralSource);
                }
                Console.WriteLine("source: {0}", sourceModule);
                #endif
                    
                System.CodeDom.Compiler.CompilerResults cr = csharp.CompileAssemblyFromSource(cp, LiteralSource);

                
                if (cr == null)
                    throw new SfxGenerationException("Cannot compile the extraction logic!");

                if (Verbose)
                    foreach (string output in cr.Output)
                        StatusMessageTextWriter.WriteLine(output);

                if (cr.Errors.Count != 0)
                {
                    //Console.ReadLine();
                    string sourcefile = GenerateTempPathname("cs", null);
                    using (TextWriter tw = new StreamWriter(sourcefile))
                    {
                        tw.Write(LiteralSource);
                    }
                    throw new SfxGenerationException(String.Format("Errors compiling the extraction logic!  {0}", sourcefile));
                }

                OnSaveEvent(ZipProgressEventType.Saving_AfterCompileSelfExtractor);

                // Now, copy the resulting EXE image to the _writestream.
                // Because this stub exe is being saved first, the effect will be to
                // concatenate the exe and the zip data together. 
                using (System.IO.Stream input = System.IO.File.OpenRead(StubExe))
                {
                    byte[] buffer = new byte[4000];
                    int n = 1;
                    while (n != 0)
                    {
                        n = input.Read(buffer, 0, buffer.Length);
                        if (n != 0)
                            WriteStream.Write(buffer, 0, n);
                    }
                }

                OnSaveEvent(ZipProgressEventType.Saving_AfterSaveTempArchive);
            }
            finally
            {
                try
                {
                    if (Directory.Exists(TempDir))
                    {
                        try { Directory.Delete(TempDir, true); }
                        catch { }
                    }
                    if (File.Exists(StubExe))
                    {
                        try { File.Delete(StubExe); }
                        catch { }
                    }
                    if (removeIconFile && File.Exists(nameOfIconFile))
                    {
                        try { File.Delete(nameOfIconFile); }
                        catch { }
                    }

                }
                catch { }

            }

            return;

        }




        internal static string GenerateTempPathname(string extension, string ContainingDirectory)
        {
            string candidate = null;
            String AppName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;

            string parentDir = (ContainingDirectory == null)
                ? System.IO.Path.GetTempPath() 
                : ContainingDirectory;

            //if (parentDir == null) return null;

            int index = 0;
            do
            {
                index++;
                string Name = String.Format("{0}-{1}-{2}.{3}",
                        AppName, System.DateTime.Now.ToString("yyyyMMMdd-HHmmss"), index, extension);
                candidate = System.IO.Path.Combine(parentDir, Name);
            } while (System.IO.File.Exists(candidate) || System.IO.Directory.Exists(candidate));

            // this file/path does not exist.  It can now be created, as file or directory. 
            return candidate;
        }
    }
#endif
}
