namespace WebCamera.WinForms
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openDeviceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeDeviceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.devicesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.device1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.device2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.device3ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.device4ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.connectToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(984, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(70, 20);
            this.fileToolStripMenuItem.Text = "ファイル(&F)";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.exitToolStripMenuItem.Text = "終了(&X)";
            // 
            // connectToolStripMenuItem
            // 
            this.connectToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openDeviceToolStripMenuItem,
            this.closeDeviceToolStripMenuItem,
            this.toolStripMenuItem1,
            this.devicesToolStripMenuItem});
            this.connectToolStripMenuItem.Name = "connectToolStripMenuItem";
            this.connectToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.connectToolStripMenuItem.Text = "接続(&C)";
            // 
            // openDeviceToolStripMenuItem
            // 
            this.openDeviceToolStripMenuItem.CheckOnClick = true;
            this.openDeviceToolStripMenuItem.Name = "openDeviceToolStripMenuItem";
            this.openDeviceToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.openDeviceToolStripMenuItem.Text = "開く(&O)";
            // 
            // closeDeviceToolStripMenuItem
            // 
            this.closeDeviceToolStripMenuItem.CheckOnClick = true;
            this.closeDeviceToolStripMenuItem.Name = "closeDeviceToolStripMenuItem";
            this.closeDeviceToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.closeDeviceToolStripMenuItem.Text = "閉じる(&C)";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(177, 6);
            // 
            // devicesToolStripMenuItem
            // 
            this.devicesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.device1ToolStripMenuItem,
            this.device2ToolStripMenuItem,
            this.device3ToolStripMenuItem,
            this.device4ToolStripMenuItem});
            this.devicesToolStripMenuItem.Name = "devicesToolStripMenuItem";
            this.devicesToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.devicesToolStripMenuItem.Text = "デバイス";
            // 
            // device1ToolStripMenuItem
            // 
            this.device1ToolStripMenuItem.CheckOnClick = true;
            this.device1ToolStripMenuItem.Name = "device1ToolStripMenuItem";
            this.device1ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.device1ToolStripMenuItem.Text = "デバイス 1";
            // 
            // device2ToolStripMenuItem
            // 
            this.device2ToolStripMenuItem.CheckOnClick = true;
            this.device2ToolStripMenuItem.Name = "device2ToolStripMenuItem";
            this.device2ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.device2ToolStripMenuItem.Text = "デバイス 2";
            // 
            // device3ToolStripMenuItem
            // 
            this.device3ToolStripMenuItem.CheckOnClick = true;
            this.device3ToolStripMenuItem.Name = "device3ToolStripMenuItem";
            this.device3ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.device3ToolStripMenuItem.Text = "デバイス 3";
            // 
            // device4ToolStripMenuItem
            // 
            this.device4ToolStripMenuItem.CheckOnClick = true;
            this.device4ToolStripMenuItem.Name = "device4ToolStripMenuItem";
            this.device4ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.device4ToolStripMenuItem.Text = "デバイス 4";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Black;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 24);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(984, 587);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 611);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WebCameraDemo";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolStripMenuItem connectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openDeviceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeDeviceToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem devicesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem device1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem device2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem device3ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem device4ToolStripMenuItem;
    }
}

