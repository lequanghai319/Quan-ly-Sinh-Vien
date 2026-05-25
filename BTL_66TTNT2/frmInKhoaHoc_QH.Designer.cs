namespace BTL_66TTNT2
{
    partial class frmInKhoaHoc
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
            this.label1 = new System.Windows.Forms.Label();
            this.cboLocHocKy = new System.Windows.Forms.ComboBox();
            this.dgvInKhoaHoc = new System.Windows.Forms.DataGridView();
            this.btnXuatExcel = new System.Windows.Forms.Button();
            this.btnDong = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInKhoaHoc)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 6);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Chọn học kỳ cần in:";
            // 
            // cboLocHocKy
            // 
            this.cboLocHocKy.FormattingEnabled = true;
            this.cboLocHocKy.Items.AddRange(new object[] {
            "Tất cả",
            "Học kỳ 1",
            "Học kỳ 2 ",
            "Học kỳ hè"});
            this.cboLocHocKy.Location = new System.Drawing.Point(109, 4);
            this.cboLocHocKy.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cboLocHocKy.Name = "cboLocHocKy";
            this.cboLocHocKy.Size = new System.Drawing.Size(82, 21);
            this.cboLocHocKy.TabIndex = 1;
            this.cboLocHocKy.SelectedIndexChanged += new System.EventHandler(this.cboLocHocKy_SelectedIndexChanged);
            // 
            // dgvInKhoaHoc
            // 
            this.dgvInKhoaHoc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvInKhoaHoc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInKhoaHoc.Location = new System.Drawing.Point(3, 36);
            this.dgvInKhoaHoc.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dgvInKhoaHoc.Name = "dgvInKhoaHoc";
            this.dgvInKhoaHoc.RowHeadersWidth = 62;
            this.dgvInKhoaHoc.RowTemplate.Height = 28;
            this.dgvInKhoaHoc.Size = new System.Drawing.Size(529, 197);
            this.dgvInKhoaHoc.TabIndex = 2;
            this.dgvInKhoaHoc.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvInKhoaHoc_CellContentClick);
            // 
            // btnXuatExcel
            // 
            this.btnXuatExcel.Location = new System.Drawing.Point(388, 259);
            this.btnXuatExcel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnXuatExcel.Name = "btnXuatExcel";
            this.btnXuatExcel.Size = new System.Drawing.Size(72, 25);
            this.btnXuatExcel.TabIndex = 3;
            this.btnXuatExcel.Text = "Xuất_Excel";
            this.btnXuatExcel.UseVisualStyleBackColor = true;
            this.btnXuatExcel.Click += new System.EventHandler(this.btnXuatExcel_Click);
            // 
            // btnDong
            // 
            this.btnDong.Location = new System.Drawing.Point(475, 259);
            this.btnDong.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(50, 25);
            this.btnDong.TabIndex = 4;
            this.btnDong.Text = "Đóng";
            this.btnDong.UseVisualStyleBackColor = true;
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // frmInKhoaHoc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(533, 292);
            this.Controls.Add(this.btnDong);
            this.Controls.Add(this.btnXuatExcel);
            this.Controls.Add(this.dgvInKhoaHoc);
            this.Controls.Add(this.cboLocHocKy);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "frmInKhoaHoc";
            this.Text = "frmInKhoaHoc_QH";
            this.Load += new System.EventHandler(this.frmInKhoaHoc_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvInKhoaHoc)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboLocHocKy;
        private System.Windows.Forms.DataGridView dgvInKhoaHoc;
        private System.Windows.Forms.Button btnXuatExcel;
        private System.Windows.Forms.Button btnDong;
    }
}