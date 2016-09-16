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
            this.PB_FC = new System.Windows.Forms.PictureBox();
            this.PB_CANCEL = new System.Windows.Forms.PictureBox();
            this.PB_FILE = new System.Windows.Forms.ProgressBar();
            this.PB_FULL = new System.Windows.Forms.ProgressBar();
            this.L_PROGRESS = new System.Windows.Forms.Label();
            this.L_PROGRESS_FILE = new System.Windows.Forms.Label();
            this.L_PROGRESS_FULL = new System.Windows.Forms.Label();
            this.BW_CRITICAL = new System.ComponentModel.BackgroundWorker();
            this.BW_FULL = new System.ComponentModel.BackgroundWorker();
            this.BW_DOWNLOAD = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.PB_CLOSE)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PB_START)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PB_FC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PB_CANCEL)).BeginInit();
            this.SuspendLayout();
            // 
            // PB_CLOSE
            // 
            this.PB_CLOSE.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.PB_CLOSE.BackgroundImage = global::Game_Updater.Properties.Resources.close;
            this.PB_CLOSE.Image = global::Game_Updater.Properties.Resources.close;
            this.PB_CLOSE.InitialImage = global::Game_Updater.Properties.Resources.close;
            this.PB_CLOSE.Location = new System.Drawing.Point(767, 15);
            this.PB_CLOSE.Name = "PB_CLOSE";
            this.PB_CLOSE.Size = new System.Drawing.Size(20, 20);
            this.PB_CLOSE.TabIndex = 0;
            this.PB_CLOSE.TabStop = false;
            this.PB_CLOSE.MouseLeave += new System.EventHandler(this.PB_CLOSE_MouseLeave);
            this.PB_CLOSE.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PB_CLOSE_MouseMove);
            this.PB_CLOSE.Click += new System.EventHandler(this.PB_CLOSE_Click);
            // 
            // PB_START
            // 
            this.PB_START.Image = global::Game_Updater.Properties.Resources.SDisabled;
            this.PB_START.Location = new System.Drawing.Point(685, 485);
            this.PB_START.Name = "PB_START";
            this.PB_START.Size = new System.Drawing.Size(96, 99);
            this.PB_START.TabIndex = 2;
            this.PB_START.TabStop = false;
            this.PB_START.MouseLeave += new System.EventHandler(this.PB_START_MouseLeave);
            this.PB_START.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PB_START_MouseMove);
            this.PB_START.Click += new System.EventHandler(this.PB_START_Click);
            // 
            // PB_FC
            // 
            this.PB_FC.Image = global::Game_Updater.Properties.Resources.FCEnabled;
            this.PB_FC.Location = new System.Drawing.Point(573, 502);
            this.PB_FC.Name = "PB_FC";
            this.PB_FC.Size = new System.Drawing.Size(101, 27);
            this.PB_FC.TabIndex = 3;
            this.PB_FC.TabStop = false;
            this.PB_FC.MouseLeave += new System.EventHandler(this.PB_FC_MouseLeave);
            this.PB_FC.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PB_FC_MouseMove);
            this.PB_FC.Click += new System.EventHandler(this.PB_FC_Click);
            // 
            // PB_CANCEL
            // 
            this.PB_CANCEL.Image = global::Game_Updater.Properties.Resources.CDisabled;
            this.PB_CANCEL.Location = new System.Drawing.Point(573, 535);
            this.PB_CANCEL.Name = "PB_CANCEL";
            this.PB_CANCEL.Size = new System.Drawing.Size(101, 27);
            this.PB_CANCEL.TabIndex = 4;
            this.PB_CANCEL.TabStop = false;
            this.PB_CANCEL.MouseLeave += new System.EventHandler(this.PB_CANCEL_MouseLeave);
            this.PB_CANCEL.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PB_CANCEL_MouseMove);
            this.PB_CANCEL.Click += new System.EventHandler(this.PB_CANCEL_Click);
            // 
            // PB_FILE
            // 
            this.PB_FILE.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.PB_FILE.Location = new System.Drawing.Point(125, 528);
            this.PB_FILE.Name = "PB_FILE";
            this.PB_FILE.Size = new System.Drawing.Size(432, 10);
            this.PB_FILE.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.PB_FILE.TabIndex = 5;
            // 
            // PB_FULL
            // 
            this.PB_FULL.Location = new System.Drawing.Point(125, 544);
            this.PB_FULL.Name = "PB_FULL";
            this.PB_FULL.Size = new System.Drawing.Size(432, 10);
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
            this.L_PROGRESS.Location = new System.Drawing.Point(216, 505);
            this.L_PROGRESS.Name = "L_PROGRESS";
            this.L_PROGRESS.Size = new System.Drawing.Size(87, 13);
            this.L_PROGRESS.TabIndex = 7;
            this.L_PROGRESS.Text = "Инициализация";
            this.L_PROGRESS.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // L_PROGRESS_FILE
            // 
            this.L_PROGRESS_FILE.AutoSize = true;
            this.L_PROGRESS_FILE.BackColor = System.Drawing.Color.Transparent;
            this.L_PROGRESS_FILE.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.L_PROGRESS_FILE.Location = new System.Drawing.Point(27, 525);
            this.L_PROGRESS_FILE.Name = "L_PROGRESS_FILE";
            this.L_PROGRESS_FILE.Size = new System.Drawing.Size(92, 13);
            this.L_PROGRESS_FILE.TabIndex = 8;
            this.L_PROGRESS_FILE.Text = "Загрузка файла:";
            // 
            // L_PROGRESS_FULL
            // 
            this.L_PROGRESS_FULL.AutoSize = true;
            this.L_PROGRESS_FULL.BackColor = System.Drawing.Color.Transparent;
            this.L_PROGRESS_FULL.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.L_PROGRESS_FULL.Location = new System.Drawing.Point(27, 541);
            this.L_PROGRESS_FULL.Name = "L_PROGRESS_FULL";
            this.L_PROGRESS_FULL.Size = new System.Drawing.Size(95, 13);
            this.L_PROGRESS_FULL.TabIndex = 9;
            this.L_PROGRESS_FULL.Text = "Общий прогресс:";
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
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.WindowText;
            this.BackgroundImage = global::Game_Updater.Properties.Resources.bg_updater;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(800, 599);
            this.Controls.Add(this.L_PROGRESS_FULL);
            this.Controls.Add(this.L_PROGRESS_FILE);
            this.Controls.Add(this.L_PROGRESS);
            this.Controls.Add(this.PB_FULL);
            this.Controls.Add(this.PB_FILE);
            this.Controls.Add(this.PB_CANCEL);
            this.Controls.Add(this.PB_FC);
            this.Controls.Add(this.PB_START);
            this.Controls.Add(this.PB_CLOSE);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Автоматическое обновление клиента";
            this.TransparencyKey = this.BackColor;
            this.Load += new System.EventHandler(this.Main_Load);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Main_MouseUp);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Main_FormClosed);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Main_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Main_MouseMove);
            ((System.ComponentModel.ISupportInitialize)(this.PB_CLOSE)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PB_START)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PB_FC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PB_CANCEL)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox PB_CLOSE;
        private System.Windows.Forms.PictureBox PB_START;
        private System.Windows.Forms.PictureBox PB_FC;
        private System.Windows.Forms.PictureBox PB_CANCEL;
        private System.Windows.Forms.ProgressBar PB_FILE;
        private System.Windows.Forms.ProgressBar PB_FULL;
        private System.Windows.Forms.Label L_PROGRESS;
        private System.Windows.Forms.Label L_PROGRESS_FILE;
        private System.Windows.Forms.Label L_PROGRESS_FULL;
        private System.ComponentModel.BackgroundWorker BW_CRITICAL;
        private System.ComponentModel.BackgroundWorker BW_FULL;
        private System.ComponentModel.BackgroundWorker BW_DOWNLOAD;

    }
}

