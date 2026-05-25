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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInKhoaHoc)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(146, 20);
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
            "Học kỳ 3"});
            this.cboLocHocKy.Location = new System.Drawing.Point(164, 6);
            this.cboLocHocKy.Name = "cboLocHocKy";
            this.cboLocHocKy.Size = new System.Drawing.Size(121, 28);
            this.cboLocHocKy.TabIndex = 1;
            this.cboLocHocKy.SelectedIndexChanged += new System.EventHandler(this.cboLocHocKy_SelectedIndexChanged);
            // 
            // dgvInKhoaHoc
            // 
            this.dgvInKhoaHoc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvInKhoaHoc.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvInKhoaHoc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInKhoaHoc.Location = new System.Drawing.Point(4, 55);
            this.dgvInKhoaHoc.Name = "dgvInKhoaHoc";
            this.dgvInKhoaHoc.RowHeadersWidth = 62;
            this.dgvInKhoaHoc.RowTemplate.Height = 28;
            this.dgvInKhoaHoc.Size = new System.Drawing.Size(794, 303);
            this.dgvInKhoaHoc.TabIndex = 2;
            this.dgvInKhoaHoc.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvInKhoaHoc_CellContentClick);
            // 
            // btnXuatExcel
            // 
            this.btnXuatExcel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnXuatExcel.Location = new System.Drawing.Point(3, 3);
            this.btnXuatExcel.Name = "btnXuatExcel";
            this.btnXuatExcel.Size = new System.Drawing.Size(133, 67);
            this.btnXuatExcel.TabIndex = 3;
            this.btnXuatExcel.Text = "Xuất_Excel";
            this.btnXuatExcel.UseVisualStyleBackColor = true;
            this.btnXuatExcel.Click += new System.EventHandler(this.btnXuatExcel_Click);
            // 
            // btnDong
            // 
            this.btnDong.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDong.Location = new System.Drawing.Point(142, 3);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(110, 67);
            this.btnDong.TabIndex = 4;
            this.btnDong.Text = "Đóng";
            this.btnDong.UseVisualStyleBackColor = true;
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 54.54546F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45.45454F));
            this.tableLayoutPanel1.Controls.Add(this.btnXuatExcel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnDong, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(533, 364);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(255, 73);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // frmInKhoaHoc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 449);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.dgvInKhoaHoc);
            this.Controls.Add(this.cboLocHocKy);
            this.Controls.Add(this.label1);
            this.Name = "frmInKhoaHoc";
            this.Text = "frmInKhoaHoc_QH";
            this.Load += new System.EventHandler(this.frmInKhoaHoc_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvInKhoaHoc)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboLocHocKy;
        private System.Windows.Forms.DataGridView dgvInKhoaHoc;
        private System.Windows.Forms.Button btnXuatExcel;
        private System.Windows.Forms.Button btnDong;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}