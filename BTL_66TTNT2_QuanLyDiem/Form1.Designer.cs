namespace BTL_66TTNT2
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.studentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quan_ly_sv = new System.Windows.Forms.ToolStripMenuItem();
            this.thốngKêToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.courToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sCOREToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aDDSCOREToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aVGSCOREToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btn_THOAT = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("Arial Narrow", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.studentToolStripMenuItem,
            this.courToolStripMenuItem,
            this.sCOREToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 35);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // studentToolStripMenuItem
            // 
            this.studentToolStripMenuItem.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.studentToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.quan_ly_sv,
            this.thốngKêToolStripMenuItem});
            this.studentToolStripMenuItem.ForeColor = System.Drawing.Color.Black;
            this.studentToolStripMenuItem.Name = "studentToolStripMenuItem";
            this.studentToolStripMenuItem.Size = new System.Drawing.Size(101, 31);
            this.studentToolStripMenuItem.Text = "Sinh viên";
            // 
            // quan_ly_sv
            // 
            this.quan_ly_sv.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.quan_ly_sv.ForeColor = System.Drawing.Color.Black;
            this.quan_ly_sv.Name = "quan_ly_sv";
            this.quan_ly_sv.Size = new System.Drawing.Size(237, 32);
            this.quan_ly_sv.Text = "Quản lý sinh viên";
            this.quan_ly_sv.Click += new System.EventHandler(this.quảnLýSinhViênToolStripMenuItem_Click);
            // 
            // thốngKêToolStripMenuItem
            // 
            this.thốngKêToolStripMenuItem.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.thốngKêToolStripMenuItem.ForeColor = System.Drawing.Color.Black;
            this.thốngKêToolStripMenuItem.Name = "thốngKêToolStripMenuItem";
            this.thốngKêToolStripMenuItem.Size = new System.Drawing.Size(237, 32);
            this.thốngKêToolStripMenuItem.Text = "Thống kê";
            this.thốngKêToolStripMenuItem.Click += new System.EventHandler(this.thốngKêToolStripMenuItem_Click);
            // 
            // courToolStripMenuItem
            // 
            this.courToolStripMenuItem.Name = "courToolStripMenuItem";
            this.courToolStripMenuItem.Size = new System.Drawing.Size(103, 31);
            this.courToolStripMenuItem.Text = "Khóa học";
            // 
            // sCOREToolStripMenuItem
            // 
            this.sCOREToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aDDSCOREToolStripMenuItem,
            this.aVGSCOREToolStripMenuItem});
            this.sCOREToolStripMenuItem.Name = "sCOREToolStripMenuItem";
            this.sCOREToolStripMenuItem.Size = new System.Drawing.Size(71, 31);
            this.sCOREToolStripMenuItem.Text = "Điểm";
            // 
            // aDDSCOREToolStripMenuItem
            // 
            this.aDDSCOREToolStripMenuItem.Name = "aDDSCOREToolStripMenuItem";
            this.aDDSCOREToolStripMenuItem.Size = new System.Drawing.Size(224, 32);
            this.aDDSCOREToolStripMenuItem.Text = "Thêm điểm";
            this.aDDSCOREToolStripMenuItem.Click += new System.EventHandler(this.aDDSCOREToolStripMenuItem_Click);
            // 
            // aVGSCOREToolStripMenuItem
            // 
            this.aVGSCOREToolStripMenuItem.Name = "aVGSCOREToolStripMenuItem";
            this.aVGSCOREToolStripMenuItem.Size = new System.Drawing.Size(224, 32);
            this.aVGSCOREToolStripMenuItem.Text = "Điểm TB";
            this.aVGSCOREToolStripMenuItem.Click += new System.EventHandler(this.aVGSCOREToolStripMenuItem_Click);
            // 
            // btn_THOAT
            // 
            this.btn_THOAT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_THOAT.BackColor = System.Drawing.Color.Firebrick;
            this.btn_THOAT.FlatAppearance.BorderSize = 0;
            this.btn_THOAT.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_THOAT.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_THOAT.Location = new System.Drawing.Point(707, 0);
            this.btn_THOAT.Margin = new System.Windows.Forms.Padding(4);
            this.btn_THOAT.Name = "btn_THOAT";
            this.btn_THOAT.Size = new System.Drawing.Size(89, 30);
            this.btn_THOAT.TabIndex = 3;
            this.btn_THOAT.Text = "Thoát";
            this.btn_THOAT.UseVisualStyleBackColor = false;
            this.btn_THOAT.Click += new System.EventHandler(this.btn_THOAT_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btn_THOAT);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản lý thông tin sinh viên";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem studentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem courToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sCOREToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quan_ly_sv;
        private System.Windows.Forms.ToolStripMenuItem thốngKêToolStripMenuItem;
        private System.Windows.Forms.Button btn_THOAT;
        private System.Windows.Forms.ToolStripMenuItem aDDSCOREToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aVGSCOREToolStripMenuItem;
    }
}

