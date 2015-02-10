namespace Panappta {
    partial class frmMain {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.tmUpdate = new System.Windows.Forms.Timer(this.components);
            this.tbOutages = new System.Windows.Forms.TabControl();
            this.label1 = new System.Windows.Forms.Label();
            this.nTime = new System.Windows.Forms.NumericUpDown();
            this.cmdUpdate = new System.Windows.Forms.Button();
            this.cmdHide = new System.Windows.Forms.Button();
            this.nSystem = new System.Windows.Forms.NotifyIcon(this.components);
            this.tInfo = new System.Windows.Forms.ToolTip(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.nAlertTime = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.cmdShow = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblLast = new System.Windows.Forms.ToolStripStatusLabel();
            this.tbProgress = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.nTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nAlertTime)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tmUpdate
            // 
            this.tmUpdate.Enabled = true;
            this.tmUpdate.Interval = 60000;
            this.tmUpdate.Tick += new System.EventHandler(this.tmUpdate_Tick);
            // 
            // tbOutages
            // 
            this.tbOutages.ItemSize = new System.Drawing.Size(0, 22);
            this.tbOutages.Location = new System.Drawing.Point(5, 2);
            this.tbOutages.Name = "tbOutages";
            this.tbOutages.SelectedIndex = 0;
            this.tbOutages.Size = new System.Drawing.Size(447, 148);
            this.tbOutages.TabIndex = 6;
            this.tbOutages.Click += new System.EventHandler(this.tbOutages_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 182);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Poll Time (from Panappta):";
            // 
            // nTime
            // 
            this.nTime.Location = new System.Drawing.Point(5, 198);
            this.nTime.Maximum = new decimal(new int[] {
            900,
            0,
            0,
            0});
            this.nTime.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nTime.Name = "nTime";
            this.nTime.Size = new System.Drawing.Size(47, 20);
            this.nTime.TabIndex = 3;
            this.nTime.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.nTime.ValueChanged += new System.EventHandler(this.nTime_ValueChanged);
            // 
            // cmdUpdate
            // 
            this.cmdUpdate.Location = new System.Drawing.Point(377, 195);
            this.cmdUpdate.Name = "cmdUpdate";
            this.cmdUpdate.Size = new System.Drawing.Size(75, 23);
            this.cmdUpdate.TabIndex = 5;
            this.cmdUpdate.Text = "Check Now";
            this.cmdUpdate.UseVisualStyleBackColor = true;
            this.cmdUpdate.Click += new System.EventHandler(this.cmdUpdate_Click);
            // 
            // cmdHide
            // 
            this.cmdHide.Location = new System.Drawing.Point(5, 156);
            this.cmdHide.Name = "cmdHide";
            this.cmdHide.Size = new System.Drawing.Size(75, 23);
            this.cmdHide.TabIndex = 1;
            this.cmdHide.Text = "Hide Alert";
            this.cmdHide.UseVisualStyleBackColor = true;
            this.cmdHide.Click += new System.EventHandler(this.cmdHide_Click);
            // 
            // nSystem
            // 
            this.nSystem.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.nSystem.Icon = ((System.Drawing.Icon)(resources.GetObject("nSystem.Icon")));
            this.nSystem.Text = "Panappta Alerts";
            this.nSystem.Visible = true;
            this.nSystem.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.nSystem_MouseDoubleClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(133, 182);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(156, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Show System Tray Alerts every:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(58, 200);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "sec(s)";
            // 
            // nAlertTime
            // 
            this.nAlertTime.Location = new System.Drawing.Point(136, 198);
            this.nAlertTime.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.nAlertTime.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nAlertTime.Name = "nAlertTime";
            this.nAlertTime.Size = new System.Drawing.Size(47, 20);
            this.nAlertTime.TabIndex = 4;
            this.nAlertTime.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(184, 200);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "min(s)";
            // 
            // cmdShow
            // 
            this.cmdShow.Enabled = false;
            this.cmdShow.Location = new System.Drawing.Point(86, 156);
            this.cmdShow.Name = "cmdShow";
            this.cmdShow.Size = new System.Drawing.Size(75, 23);
            this.cmdShow.TabIndex = 2;
            this.cmdShow.Text = "Show Alert";
            this.cmdShow.UseVisualStyleBackColor = true;
            this.cmdShow.Click += new System.EventHandler(this.cmdShow_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblLast});
            this.statusStrip1.Location = new System.Drawing.Point(0, 222);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(455, 22);
            this.statusStrip1.TabIndex = 11;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblLast
            // 
            this.lblLast.Name = "lblLast";
            this.lblLast.Size = new System.Drawing.Size(0, 17);
            // 
            // tbProgress
            // 
            this.tbProgress.Location = new System.Drawing.Point(377, 156);
            this.tbProgress.Name = "tbProgress";
            this.tbProgress.Size = new System.Drawing.Size(75, 23);
            this.tbProgress.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.tbProgress.TabIndex = 12;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(455, 244);
            this.Controls.Add(this.tbProgress);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.cmdShow);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.nAlertTime);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmdHide);
            this.Controls.Add(this.cmdUpdate);
            this.Controls.Add(this.nTime);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbOutages);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Panappta";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.Resize += new System.EventHandler(this.frmMain_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.nTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nAlertTime)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer tmUpdate;
        private System.Windows.Forms.TabControl tbOutages;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nTime;
        private System.Windows.Forms.Button cmdUpdate;
        private System.Windows.Forms.Button cmdHide;
        private System.Windows.Forms.NotifyIcon nSystem;
        private System.Windows.Forms.ToolTip tInfo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nAlertTime;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button cmdShow;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblLast;
        private System.Windows.Forms.ProgressBar tbProgress;

    }
}

