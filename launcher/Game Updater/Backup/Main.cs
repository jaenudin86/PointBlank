using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;
using Ionic.Zip;

namespace Game_Updater
{
    public partial class Main : Form
    {
        private string UPDATE_URL = "http://update.l2master.ru/";
        private string ARCH_TYPE = ".zip";
        private string CRITICAL_FILE = "critical.list";
        private string FULL_FILE = "full.list";
        private string VERSION_FILE = "update.version";
        private string MainDirSavePath = Application.StartupPath + "\\";
        private string[] delimeter = { "|" };
        private string[] delimeterF = { "\\" };        


        public Main()
        {
            InitializeComponent();
        }

        // delegates

        private delegate void MsgProgressDelegate(string text);
        public void WriteLog(string text)
        {
            FileStream logFS = new FileStream(MainDirSavePath + "\\update.log", FileMode.Append, FileAccess.Write);
            StreamWriter logSW = new StreamWriter(logFS, Encoding.GetEncoding("windows-1251"));
            logSW.WriteLine(text);
            logSW.Close();
            logFS.Close();        
            
        }
        public void MsgProgress(string text)
        {            
            if (this.L_PROGRESS.InvokeRequired)
            {
                this.L_PROGRESS.Invoke(new MsgProgressDelegate(MsgProgress), text);
            }
            else
            {
                this.WriteLog(text);
                this.L_PROGRESS.Text = text;
            }
        }

        private delegate void setMaxProgressFileDelegate(int val);
        private void setMaxProgressFile(int val)
        {
            if (this.PB_FILE.InvokeRequired)
                this.PB_FILE.Invoke(new setMaxProgressFileDelegate(setMaxProgressFile), val);
            else
                this.PB_FILE.Maximum = val;
        }

        private delegate void UpdateProgressFileDelegate(int val);
        private void UpdateProgressFile(int val)
        {
            if (this.PB_FILE.InvokeRequired)
                this.PB_FILE.Invoke(new UpdateProgressFileDelegate(UpdateProgressFile), val);
            else
                this.PB_FILE.Value = val;
        }

        private delegate void setMaxProgressFullDelegate(int val);
        private void setMaxProgressFull(int val)
        {
            if (this.PB_FULL.InvokeRequired)
                this.PB_FULL.Invoke(new setMaxProgressFullDelegate(setMaxProgressFull), val);
            else
                this.PB_FULL.Maximum = val;
        }

        private delegate void UpdateProgressFullDelegate(int val);
        private void UpdateProgressFull(int val)
        {
            if (this.PB_FULL.InvokeRequired)
                this.PB_FULL.Invoke(new UpdateProgressFullDelegate(UpdateProgressFull), val);
            else
                this.PB_FULL.Value = val;
        }

        private void Main_Load(object sender, EventArgs e)
        {
            //this.StartDownload();            
            this.BW_CRITICAL.RunWorkerAsync();
        }

        private void PB_CLOSE_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void PB_CLOSE_MouseMove(object sender, MouseEventArgs e)
        {
            this.PB_CLOSE.Image = global::Game_Updater.Properties.Resources.close_act;
        }

        private void PB_CLOSE_MouseLeave(object sender, EventArgs e)
        {
            this.PB_CLOSE.Image = global::Game_Updater.Properties.Resources.close;
        }

        private void PB_START_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("скоро, очень скоро :)");
            if (File.Exists(Application.StartupPath + "\\system\\l2.bin"))
            {
                this.Hide();
                if (ProcessAsUser.Launch(Application.StartupPath + "\\system\\l2.bin"))
                    Application.Exit();
            }
            else
            {
                MessageBox.Show("Файл l2.bin не найден!");
            }
        }

        private void PB_START_MouseLeave(object sender, EventArgs e)
        {
            this.PB_START.Image = global::Game_Updater.Properties.Resources.SDisabled;
        }

        private void PB_START_MouseMove(object sender, MouseEventArgs e)
        {
            this.PB_START.Image = global::Game_Updater.Properties.Resources.SEnabled;
        }

        private void PB_CANCEL_MouseMove(object sender, MouseEventArgs e)
        {                        
            //this.PB_CANCEL.Image = global::Game_Updater.Properties.Resources.CEnabled;
        }

        private void PB_CANCEL_MouseLeave(object sender, EventArgs e)
        {
            //this.PB_CANCEL.Image = global::Game_Updater.Properties.Resources.CDisabled;   
        }

        private void PB_FC_MouseLeave(object sender, EventArgs e)
        {            
            this.PB_FC.Image = global::Game_Updater.Properties.Resources.FCEnabled;
        }

        private void PB_FC_MouseMove(object sender, MouseEventArgs e)
        {
            this.PB_FC.Image = global::Game_Updater.Properties.Resources.FCOnMouse;
        }

        private void PB_FC_Click(object sender, EventArgs e)
        {
            this.BW_FULL.RunWorkerAsync();            
        }

        private void PB_CANCEL_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void CriticalUpdate()
        {
            this.BW_CRITICAL.RunWorkerAsync();
        }

        private int GetPathcVersion(string path,bool delete)
        {
            if (File.Exists(MainDirSavePath + VERSION_FILE))
            {
                //читаем его и получаем версию патча
                string VersionFileLine;
                int PatchVersion = 0;
                StreamReader VersionFile = new StreamReader(MainDirSavePath + VERSION_FILE);
                while ((VersionFileLine = VersionFile.ReadLine()) != null)
                {
                    if (VersionFileLine != "")
                    {
                        PatchVersion = Convert.ToInt16(VersionFileLine);
                    }
                }
                VersionFile.Close();
                
                //удаляем файл
                if(delete==true)
                    File.Delete(MainDirSavePath + VERSION_FILE);

                return PatchVersion;
            }
            return 0;
        }       

        private void Update(bool Type)
        {
            if (File.Exists(MainDirSavePath + "\\update.log"))
            {
                File.Delete(MainDirSavePath + "\\update.log");
            }
            //bool Type = true;// true = critical, false = full
            string UpdFile = "";
            int PatchVersion = this.GetPathcVersion(MainDirSavePath + VERSION_FILE,true);
            MsgProgress("Инициализируем процесс обновления");
            if (Type)
            {
                this.Downdload(UPDATE_URL + VERSION_FILE, MainDirSavePath + VERSION_FILE, VERSION_FILE);

                int UpdPatchVersion = this.GetPathcVersion(MainDirSavePath + VERSION_FILE,false);

                if (PatchVersion == UpdPatchVersion)
                {
                    UpdateProgressFull(100);
                    MsgProgress("Обновление не требуется");
                    return;
                }
                this.Downdload(UPDATE_URL + CRITICAL_FILE, MainDirSavePath + CRITICAL_FILE, CRITICAL_FILE);
                UpdFile = CRITICAL_FILE;
            }
            else
            {
                this.Downdload(UPDATE_URL + FULL_FILE, MainDirSavePath + FULL_FILE, FULL_FILE);
                UpdFile = FULL_FILE;
            }
            if (File.Exists(MainDirSavePath + UpdFile))
            {
                // Считаем общий прогресс
                string prefLine;
                int lCnt = 0;
                int err = 0;

                StreamReader prelFile = new StreamReader(MainDirSavePath + UpdFile);
                while ((prefLine = prelFile.ReadLine()) != null)
                {
                    lCnt++;
                }
                prelFile.Close();
                setMaxProgressFull(lCnt);


                // читаем файл обновлений
                string fLine;
                int fCnt = 0;
                Crc32 crc32 = new Crc32();
                StreamReader lFile = new StreamReader(MainDirSavePath + UpdFile);
                while ((fLine = lFile.ReadLine()) != null)
                {
                    // качаем = 1 нет = 0
                    int dl = 0;

                    UpdateProgressFile(0);

                    string[] arLine = fLine.Split(delimeter, StringSplitOptions.RemoveEmptyEntries);

                    MsgProgress("Собираем информацию о файле: " + arLine[0]);

                    if (System.IO.File.Exists(MainDirSavePath + arLine[0]))
                    {
                        string FileHash = crc32.Get(MainDirSavePath + arLine[0]);
                        FileInfo SizecF = new FileInfo(MainDirSavePath + arLine[0]);

                        if (arLine[1] != Convert.ToString(SizecF.Length) || arLine[2] != FileHash)
                        {
                            dl = 1;
                            MsgProgress("Файл: " + arLine[0] + " требудет обновлений");
                        }
                    }
                    else
                    {
                        MsgProgress("Файл:" + arLine[0] + " не найден");
                        dl = 1;
                    }

                    if (dl == 1)
                    {
                        MsgProgress("Файл: " + arLine[0] + " требует обновлений");
                        MsgProgress("Загружаем файл " + arLine[0]);

                        // проверяем директорию, если нету создаем
                        string ndFile = arLine[0].Replace("\\", "/");
                        string[] arDF = arLine[0].Split(delimeterF, StringSplitOptions.RemoveEmptyEntries);

                        if (!System.IO.Directory.Exists(MainDirSavePath + arDF[0]))
                        {
                            System.IO.Directory.CreateDirectory(MainDirSavePath + "\\" + arDF[0]);
                        }

                        string dfUrl = UPDATE_URL + ndFile + ARCH_TYPE;
                        string dfSP = MainDirSavePath + arLine[0] + ARCH_TYPE;

                        bool dlOk = this.Downdload(dfUrl, dfSP, arLine[0]);
                        if (dlOk == false)
                            err = 1;

                        if (dlOk)
                        {
                            // проверяем корректность скачанного файла
                            FileInfo SizecF = new FileInfo(MainDirSavePath + arLine[0] + ARCH_TYPE);
                            string zipFileHash = crc32.Get(MainDirSavePath + arLine[0] + ARCH_TYPE);

                            if (arLine[3] != Convert.ToString(SizecF.Length) && zipFileHash != arLine[4])
                            {
                                UpdateProgressFile(100);
                                MsgProgress("Загружен не корректный файл, запускаем проверку заново.");
                                this.Update(Type);
                                err = 1;
                            }
                            else
                            {
                                MsgProgress("Распаковываем файл " + arLine[0] + ARCH_TYPE);
                                //бекапим старый файл
                                if (File.Exists(MainDirSavePath + arLine[0]))
                                    File.Move(MainDirSavePath + arLine[0], MainDirSavePath + arLine[0]+".bak");

                                //распаковываем
                                MsgProgress("Распаковываем файл " + arLine[0] + ARCH_TYPE);
                                Unzip(MainDirSavePath + arLine[0] + ARCH_TYPE, MainDirSavePath + "\\" + arDF[0]);

                                //Проверяем на корректность
                                FileInfo SizecNF = new FileInfo(MainDirSavePath + arLine[0]);
                                string NewFileHash = crc32.Get(MainDirSavePath + arLine[0]);

                                if (arLine[1] != Convert.ToString(SizecNF.Length) && NewFileHash != arLine[2])
                                {
                                    MsgProgress("Файл:" + arLine[0] + " распакован не корректно");
                                    File.Delete(MainDirSavePath + arLine[0]);
                                    File.Move(MainDirSavePath + arLine[0]+".bak",MainDirSavePath + arLine[0]);
                                    this.Update(Type);
                                }
                                else
                                {
                                    MsgProgress("Файл " + arLine[0] + ARCH_TYPE + "успешно распакован");
                                    File.Delete(MainDirSavePath + arLine[0] + ARCH_TYPE);
                                    File.Delete(MainDirSavePath + arLine[0] + ".bak");
                                    UpdateProgressFile(100);
                                }
                            }
                        }
                    }
                    else
                    {
                        MsgProgress("Файл: " + arLine[0] + " не требует обновлений");
                        UpdateProgressFile(100);
                    }
                    fCnt++;
                    setMaxProgressFile(100);
                    UpdateProgressFile(100);
                    UpdateProgressFull(fCnt);
                }
                lFile.Close();

                if (err == 0)
                    MsgProgress("Обновления успешно завершены");
                else
                    MsgProgress("При обновлении произошли проблемы");

                if (File.Exists(MainDirSavePath + UpdFile))
                {
                    File.Delete(MainDirSavePath + UpdFile);
                }
            }
            else
            {
                MsgProgress("Фай обновлений не найден");
            }
        }

        private void BW_CRITICAL_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bw = sender as BackgroundWorker;
            Update(true);
        }

        private void BW_FULL_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bw = sender as BackgroundWorker;
            Update(false);
        }

        private void BW_DOWNLOAD_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bw = sender as BackgroundWorker;
        }

        /* ZIP */

        public static void Unzip(string exFile, string exDir)
        {
            try
            {
                using (ZipFile zip = ZipFile.Read(exFile))
                {
                    foreach (ZipEntry ef in zip)
                    {
                        ef.Extract(exDir, true);  // overwrite == true  
                    }
                }
                //return true;
            }
            catch
            {
                //return false;
            }
        }

        /* Downloader */
        private Stream strResponse;
        private Stream strLocal;
        private HttpWebRequest webRequest;
        private HttpWebResponse webResponse;

        private bool Downdload(string Url, string SaveFilePath, string strFile)
        {
            //Url = Url.ToLower();
            //MessageBox.Show(Url);
            try
            {
                int startPointInt = 0;
                if (File.Exists(SaveFilePath))
                {
                    startPointInt = Convert.ToInt32(new FileInfo(SaveFilePath).Length);
                }

                webRequest = (HttpWebRequest)WebRequest.Create(Url);
                webRequest.AddRange(startPointInt);
                webRequest.Credentials = CredentialCache.DefaultCredentials;
                webResponse = (HttpWebResponse)webRequest.GetResponse();
                Int64 fileSize = webResponse.ContentLength;

                strResponse = webResponse.GetResponseStream();

                if (startPointInt == 0)
                {
                    try
                    {
                        strLocal = new FileStream(SaveFilePath, FileMode.Create, FileAccess.Write, FileShare.None);
                    }
                    catch
                    {
                        MessageBox.Show("Critical Error. Exit.");
                        Application.Exit();
                    }
                }
                else
                {
                    strLocal = new FileStream(SaveFilePath, FileMode.Append, FileAccess.Write, FileShare.None);
                }

                int bytesSize = 0;
                byte[] downBuffer = new byte[2048];

                setMaxProgressFile(Convert.ToInt32(fileSize) + 20 + startPointInt);

                //UpdateProgressBarFile
                while ((bytesSize = strResponse.Read(downBuffer, 0, downBuffer.Length)) > 0)
                {
                    strLocal.Write(downBuffer, 0, bytesSize);
                    UpdateProgressFile(Convert.ToInt32(strLocal.Length + 20));

                    int PercentProgress = Convert.ToInt32((strLocal.Length * 100) / fileSize);
                    string textDownloadProgress = "Файл " + strFile + ": загружено " + strLocal.Length + " из " + (fileSize + startPointInt) + " (" + PercentProgress + "%)";

                    MsgProgress(textDownloadProgress);
                }
                strResponse.Close();
                strLocal.Close();
            }
            catch (WebException exception)
            {                
                if (exception.Status == WebExceptionStatus.ProtocolError && exception.Message.Contains("416"))
                {
                    MsgProgress("Файл уже полностью загружен");
                    return true;
                }
                else
                {
                    if (((HttpWebResponse)exception.Response).StatusCode.ToString() == "NotFound")
                    {
                        MsgProgress("Файл не найден на сервере обновлений. Код ошибки:" + ((HttpWebResponse)exception.Response).StatusCode.ToString());                        
                    }
                    else
                    {
                        MsgProgress("Проблемы при загрузке файла. Код ошибки:" + ((HttpWebResponse)exception.Response).StatusCode.ToString());
                        Downdload(Url, SaveFilePath, "");
                    }
                    return false;
                }
                
            }
            return true;             
        }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            /*
            if (File.Exists(MainDirSavePath + CRITICAL_FILE))
                File.Delete(MainDirSavePath + CRITICAL_FILE);

            if (File.Exists(MainDirSavePath + FULL_FILE))
                File.Delete(MainDirSavePath + FULL_FILE);
            */
        }


        /* End Downdloader */

        // Перемещение окна
        private Point clickPoint = Point.Empty;
        private Rectangle bounds = Rectangle.Empty;
        private bool shouldMove = false;
        private void MoveInternal(int x, int y)
        {
            int sx = this.Location.X + (x - this.clickPoint.X);
            int sy = this.Location.Y + (y - this.clickPoint.Y);

            this.SetBounds(sx, sy, 0, 0, BoundsSpecified.Location);
        }

        private void Main_MouseDown(object sender, MouseEventArgs e)
        {
            this.clickPoint = new Point(e.X, e.Y);
            this.bounds = new Rectangle(this.Bounds.Location, this.Bounds.Size);
            shouldMove = true;
        }

        private void Main_MouseMove(object sender, MouseEventArgs e)
        {
            if (shouldMove)
            {
                this.MoveInternal(e.X, e.Y);
            }
        }

        private void Main_MouseUp(object sender, MouseEventArgs e)
        {
            shouldMove = false;
        }
    }


}
