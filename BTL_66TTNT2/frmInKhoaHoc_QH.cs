using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel; // Thư viện Excel vừa thêm ở Bước 1

namespace BTL_66TTNT2
{
    public partial class frmInKhoaHoc : Form
    {
        // Chuỗi kết nối Database
        private string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=BaiHocDauTien;Integrated Security=True";

        public frmInKhoaHoc()
        {
            InitializeComponent();
        }

        // 1. Tự động chạy khi mở form
        private void frmInKhoaHoc_Load(object sender, EventArgs e)
        {
            // Thiết lập ComboBox hiển thị dòng đầu tiên ("Tất cả")
            if (cboLocHocKy.Items.Count > 0)
            {
                cboLocHocKy.SelectedIndex = 0;
            }
        }

        // 2. Chức năng Lọc thời gian thực: Chạy mỗi khi bạn đổi lựa chọn trong ComboBox
        private void cboLocHocKy_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "";

                    if (cboLocHocKy.Text == "Tất cả")
                    {
                        // Lấy hết nếu chọn "Tất cả"
                        query = "SELECT MaKhoaHoc AS [Mã Khóa Học], TenKhoaHoc AS [Tên Khóa Học], SoTinChi AS [Số Tín Chỉ], HocKy AS [Học Kỳ] FROM KhoaHoc";
                    }
                    else
                    {
                        // Lọc theo Học kỳ tương ứng
                        query = "SELECT MaKhoaHoc AS [Mã Khóa Học], TenKhoaHoc AS [Tên Khóa Học], SoTinChi AS [Số Tín Chỉ], HocKy AS [Học Kỳ] FROM KhoaHoc WHERE HocKy = @HocKy";
                    }

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        if (cboLocHocKy.Text != "Tất cả")
                        {
                            cmd.Parameters.AddWithValue("@HocKy", cboLocHocKy.Text);
                        }

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            dgvInKhoaHoc.DataSource = dt; // Đổ kết quả vừa lọc lên bảng
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 3. Chức năng Xuất ra file Excel
        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem bảng có dữ liệu không
            if (dgvInKhoaHoc.Rows.Count == 0 || dgvInKhoaHoc.Rows[0].Cells[0].Value == null)
            {
                MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Gọi cửa sổ chọn nơi lưu file
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Workbook|*.xlsx|Excel 97-2003 Workbook|*.xls";
            sfd.Title = "Lưu danh sách khóa học";
            sfd.FileName = "DanhSachKhoaHoc.xlsx"; // Tên file mặc định

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Đổi chuột thành biểu tượng xoay vòng chờ đợi vì xuất Excel hơi tốn thời gian
                    this.Cursor = Cursors.WaitCursor;

                    // Khởi tạo phần mềm Excel ngầm
                    Excel.Application excelApp = new Excel.Application();
                    excelApp.Visible = false;
                    Excel.Workbook workbook = excelApp.Workbooks.Add(Type.Missing);
                    Excel.Worksheet worksheet = (Excel.Worksheet)workbook.ActiveSheet;
                    worksheet.Name = "Danh sách khóa học";

                    // Ghi dòng Tiêu đề cột
                    for (int i = 1; i < dgvInKhoaHoc.Columns.Count + 1; i++)
                    {
                        worksheet.Cells[1, i] = dgvInKhoaHoc.Columns[i - 1].HeaderText;
                        worksheet.Cells[1, i].Font.Bold = true; // In đậm tiêu đề
                    }

                    // Vòng lặp ghi toàn bộ dữ liệu từ GridView sang các ô Excel
                    for (int i = 0; i < dgvInKhoaHoc.Rows.Count; i++)
                    {
                        for (int j = 0; j < dgvInKhoaHoc.Columns.Count; j++)
                        {
                            if (dgvInKhoaHoc.Rows[i].Cells[j].Value != null)
                            {
                                worksheet.Cells[i + 2, j + 1] = dgvInKhoaHoc.Rows[i].Cells[j].Value.ToString();
                            }
                        }
                    }

                    // Căn chỉnh tự động cho cột vừa với chữ
                    Excel.Range workSheet_range = worksheet.Range[worksheet.Cells[1, 1], worksheet.Cells[dgvInKhoaHoc.Rows.Count + 1, dgvInKhoaHoc.Columns.Count]];
                    workSheet_range.EntireColumn.AutoFit();
                    workSheet_range.Borders.LineStyle = Excel.XlLineStyle.xlContinuous; // Kẻ bảng

                    // Lưu file xuống máy tính
                    workbook.SaveAs(sfd.FileName);
                    workbook.Close();
                    excelApp.Quit();

                    // Giải phóng bộ nhớ
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);

                    this.Cursor = Cursors.Default; // Trả lại con trỏ chuột bình thường
                    MessageBox.Show("Xuất file Excel thành công rực rỡ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Lỗi khi xuất Excel: " + ex.Message, "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // 4. Chức năng Đóng Form
        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}