namespace Game_Updater
{
    partial class Main
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.PB_CLOSE = new System.Windows.Forms.PictureBox();
            this.PB_START = new System.Windows.Forms.PictureBox();
            this.PBUPDATER = new System.Windows.Forms.PictureBox();
            this.PB_CANCEL = new System.Windows.Forms.PictureBox();
            this.PB_FILE = new System.Windows.Forms.ProgressBar();
            this.PB_FULL = new System.Windows.Forms.ProgressBar();
            this.L_PROGRESS = new System.Windows.Forms.Label();
            this.L_PROGRESS_FILE = new System.Windows.Forms.Label();
            this.L_PROGRESS_FULL = new System.Windows.Forms.Label();
            this.BW_CRITICAL = new System.ComponentModel.BackgroundWorker();
            this.BW_FULL = new System.ComponentModel.BackgroundWorker();
            this.BW_DOWNLOAD = new System.ComponentModel.BackgroundWorker();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.PB_CLOSE)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PB_START)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PBUPDATER)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PB_CANCEL)).BeginInit();
            this.SuspendLayout();
            // 
            // PB_CLOSE
            // 
            this.PB_CLOSE.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.PB_CLOSE.BackgroundImage = global::Game_Updater.Properties.Resources.close;
            this.PB_CLOSE.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PB_CLOSE.Image = global::Game_Updater.Properties.Resources.close;
            this.PB_CLOSE.InitialImage = global::Game_Updater.Properties.Resources.close;
            this.PB_CLOSE.Location = new System.Drawing.Point(721, 29);
            this.PB_CLOSE.Name = "PB_CLOSE";
            this.PB_CLOSE.Size = new System.Drawing.Size(40, 20);
            this.PB_CLOSE.TabIndex = 0;
            this.PB_CLOSE.TabStop = false;
            this.PB_CLOSE.Click += new System.EventHandler(this.PB_CLOSE_Click);
            this.PB_CLOSE.MouseLeave += new System.EventHandler(this.PB_CLOSE_MouseLeave);
            this.PB_CLOSE.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PB_CLOSE_MouseMove);
            // 
            // PB_START
            // 
            this.PB_START.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PB_START.Image = global::Game_Updater.Properties.Resources.SDisabled;
            this.PB_START.Location = new System.Drawing.Point(605, 496);
            this.PB_START.Name = "PB_START";
            this.PB_START.Size = new System.Drawing.Size(147, 59);
            this.PB_START.TabIndex = 2;
            this.PB_START.TabStop = false;
            this.PB_START.Click += new System.EventHandler(this.PB_START_Click);
            this.PB_START.MouseLeave += new System.EventHandler(this.PB_START_MouseLeave);
            this.PB_START.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PB_START_MouseMove);
            // 
            // PBUPDATER
            // 
            this.PBUPDATER.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PBUPDATER.Image = global::Game_Updater.Properties.Resources.FCEnabled;
            this.PBUPDATER.Location = new System.Drawing.Point(511, 496);
            this.PBUPDATER.Name = "PBUPDATER";
            this.PBUPDATER.Size = new System.Drawing.Size(87, 27);
            this.PBUPDATER.TabIndex = 3;
            this.PBUPDATER.TabStop = false;
            this.PBUPDATER.Click += new System.EventHandler(this.PB_FC_Click);
            this.PBUPDATER.MouseLeave += new System.EventHandler(this.PB_FC_MouseLeave);
            this.PBUPDATER.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PB_FC_MouseMove);
            // 
            // PB_CANCEL
            // 
            this.PB_CANCEL.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PB_CANCEL.Image = global::Game_Updater.Properties.Resources.CDisabled;
            this.PB_CANCEL.Location = new System.Drawing.Point(511, 528);
            this.PB_CANCEL.Name = "PB_CANCEL";
            this.PB_CANCEL.Size = new System.Drawing.Size(87, 27);
            this.PB_CANCEL.TabIndex = 4;
            this.PB_CANCEL.TabStop = false;
            this.PB_CANCEL.Click += new System.EventHandler(this.PB_CANCEL_Click);
            this.PB_CANCEL.MouseLeave += new System.EventHandler(this.PB_CANCEL_MouseLeave);
            this.PB_CANCEL.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PB_CANCEL_MouseMove);
            // 
            // PB_FILE
            // 
            this.PB_FILE.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.PB_FILE.Location = new System.Drawing.Point(144, 528);
            this.PB_FILE.Name = "PB_FILE";
            this.PB_FILE.Size = new System.Drawing.Size(361, 10);
            this.PB_FILE.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.PB_FILE.TabIndex = 5;
            this.PB_FILE.Click += new System.EventHandler(this.PB_FILE_Click);
            // 
            // PB_FULL
            // 
            this.PB_FULL.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.PB_FULL.Location = new System.Drawing.Point(144, 543);
            this.PB_FULL.Name = "PB_FULL";
            this.PB_FULL.Size = new System.Drawing.Size(361, 10);
            this.PB_FULL.TabIndex = 6;
            // 
            // L_PROGRESS
            // 
            this.L_PROGRESS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.L_PROGRESS.AutoEllipsis = true;
            this.L_PROGRESS.AutoSize = true;
            this.L_PROGRESS.BackColor = System.Drawing.Color.Transparent;
            this.L_PROGRESS.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.L_PROGRESS.Location = new System.Drawing.Point(53, 511);
            this.L_PROGRESS.Name = "L_PROGRESS";
            this.L_PROGRESS.Size = new System.Drawing.Size(154, 13);
            this.L_PROGRESS.TabIndex = 7;
            this.L_PROGRESS.Text = "                               Initialization";
            this.L_PROGRESS.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.L_PROGRESS.Click += new System.EventHandler(this.L_PROGRESS_Click);
            // 
            // L_PROGRESS_FILE
            // 
            this.L_PROGRESS_FILE.AutoSize = true;
            this.L_PROGRESS_FILE.BackColor = System.Drawing.Color.Transparent;
            this.L_PROGRESS_FILE.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.L_PROGRESS_FILE.Location = new System.Drawing.Point(49, 525);
            this.L_PROGRESS_FILE.Name = "L_PROGRESS_FILE";
            this.L_PROGRESS_FILE.Size = new System.Drawing.Size(77, 13);
            this.L_PROGRESS_FILE.TabIndex = 8;
            this.L_PROGRESS_FILE.Text = "File Download:";
            // 
            // L_PROGRESS_FULL
            // 
            this.L_PROGRESS_FULL.AutoSize = true;
            this.L_PROGRESS_FULL.BackColor = System.Drawing.Color.Transparent;
            this.L_PROGRESS_FULL.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.L_PROGRESS_FULL.Location = new System.Drawing.Point(49, 540);
            this.L_PROGRESS_FULL.Name = "L_PROGRESS_FULL";
            this.L_PROGRESS_FULL.Size = new System.Drawing.Size(87, 13);
            this.L_PROGRESS_FULL.TabIndex = 9;
            this.L_PROGRESS_FULL.Text = "Overall Progress:";
            this.L_PROGRESS_FULL.Click += new System.EventHandler(this.L_PROGRESS_FULL_Click);
            // 
            // BW_CRITICAL
            // 
            this.BW_CRITICAL.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BW_CRITICAL_DoWork);
            // 
            // BW_FULL
            // 
            this.BW_FULL.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BW_FULL_DoWork);
            // 
            // BW_DOWNLOAD
            // 
            this.BW_DOWNLOAD.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BW_DOWNLOAD_DoWork);
            // 
            // webBrowser1
            // 
            this.webBrowser1.IsWebBrowserContextMenuEnabled = false;
            this.webBrowser1.Location = new System.Drawing.Point(36, 54);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.ScrollBarsEnabled = false;
            this.webBrowser1.Size = new System.Drawing.Size(728, 430);
            this.webBrowser1.TabIndex = 10;
            this.webBrowser1.Url = new System.Uri("http://pb.ru/launcher-page", System.UriKind.Absolute);
            this.webBrowser1.WebBrowserShortcutsEnabled = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(40, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(218, 16);
            this.label1.TabIndex = 11;
            this.label1.Text = "Point Blank Private | PointBlank.PW";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightCoral;
            this.BackgroundImage = global::Game_Updater.Properties.Resources.bg_updater;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(800, 599);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.L_PROGRESS_FULL);
            this.Controls.Add(this.L_PROGRESS_FILE);
            this.Controls.Add(this.L_PROGRESS);
            this.Controls.Add(this.PB_FULL);
            this.Controls.Add(this.PB_FILE);
            this.Controls.Add(this.PB_CANCEL);
            this.Controls.Add(this.PBUPDATER);
            this.Controls.Add(this.PB_START);
            this.Controls.Add(this.PB_CLOSE);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Project Blackout 0.0.5.2 - OZ-Network";
            this.TransparencyKey = this.BackColor;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Main_FormClosed);
            this.Load += new System.EventHandler(this.Main_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Main_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Main_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Main_MouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.PB_CLOSE)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PB_START)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PBUPDATER)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PB_CANCEL)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox PB_CLOSE;
        private System.Windows.Forms.PictureBox PB_START;
        private System.Windows.Forms.PictureBox PBUPDATER;
        private System.Windows.Forms.PictureBox PB_CANCEL;
        private System.Windows.Forms.ProgressBar PB_FILE;
        private System.Windows.Forms.ProgressBar PB_FULL;
        private System.Windows.Forms.Label L_PROGRESS;
        private System.Windows.Forms.Label L_PROGRESS_FILE;
        private System.Windows.Forms.Label L_PROGRESS_FULL;
        private System.ComponentModel.BackgroundWorker BW_CRITICAL;
        private System.ComponentModel.BackgroundWorker BW_FULL;
        private System.ComponentModel.BackgroundWorker BW_DOWNLOAD;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.Label label1;

    }
}

