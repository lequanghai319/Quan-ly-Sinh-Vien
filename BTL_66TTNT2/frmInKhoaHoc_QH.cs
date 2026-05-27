using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace BTL_66TTNT2
{
    public partial class frmInKhoaHoc : Form
    {

        SqlConnection connection;
        string query;
        SqlCommand cmd;
        SqlDataAdapter adapter;
        DataTable dt;
        string connectionString = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=Bai_tap_lon;Integrated Security=True";

        public frmInKhoaHoc()
        {
            InitializeComponent();
        }

        //LOAD FORM
        private void frmInKhoaHoc_Load(object sender, EventArgs e)
        {
            // Thiết lập ComboBox hiển thị dòng đầu tiên ("Tất cả")
            if (cboLocHocKy.Items.Count > 0)
            {
                cboLocHocKy.SelectedIndex = 0;
            }
        }

        //LỌC DỮ LIỆU COMBOBOX
        private void cboLocHocKy_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                
                connection = new SqlConnection(connectionString);
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                if (cboLocHocKy.Text == "Tất cả")
                {
                   
                    query = "SELECT MaKhoaHoc AS [Mã Môn Học], TenKhoaHoc AS [Tên Môn Học], SoTinChi AS [Số Tín Chỉ], HocKy AS [Học Kỳ] FROM KhoaHoc";
                    cmd = new SqlCommand(query, connection);
                }
                else
                {
                    
                    query = "SELECT MaKhoaHoc AS [Mã Môn Học], TenKhoaHoc AS [Tên Môn Học], SoTinChi AS [Số Tín Chỉ], HocKy AS [Học Kỳ] FROM KhoaHoc WHERE HocKy = @HocKy";
                    cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@HocKy", cboLocHocKy.Text);
                }

              
                adapter = new SqlDataAdapter(cmd);
                dt = new DataTable();
                adapter.Fill(dt);
                dgvInKhoaHoc.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
               
                if (connection != null && connection.State != ConnectionState.Closed)
                    connection.Close();
            }
        }

        //XUẤT EXCEL
        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            if (dgvInKhoaHoc.Rows.Count == 0 || dgvInKhoaHoc.Rows[0].Cells[0].Value == null)
            {
                MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Workbook|*.xlsx|Excel 97-2003 Workbook|*.xls";
            sfd.Title = "Lưu danh sách môn học";
            sfd.FileName = "DanhSachMonHoc.xlsx";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    this.Cursor = Cursors.WaitCursor;

                    Excel.Application excelApp = new Excel.Application();
                    excelApp.Visible = false;
                    Excel.Workbook workbook = excelApp.Workbooks.Add(Type.Missing);
                    Excel.Worksheet worksheet = (Excel.Worksheet)workbook.ActiveSheet;
                    worksheet.Name = "Danh sách môn học";

                    for (int i = 1; i < dgvInKhoaHoc.Columns.Count + 1; i++)
                    {
                        worksheet.Cells[1, i] = dgvInKhoaHoc.Columns[i - 1].HeaderText;
                        worksheet.Cells[1, i].Font.Bold = true;
                    }

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

                    Excel.Range workSheet_range = worksheet.Range[worksheet.Cells[1, 1], worksheet.Cells[dgvInKhoaHoc.Rows.Count + 1, dgvInKhoaHoc.Columns.Count]];
                    workSheet_range.EntireColumn.AutoFit();
                    workSheet_range.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;

                    workbook.SaveAs(sfd.FileName);
                    workbook.Close();
                    excelApp.Quit();

                    System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);

                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Xuất file Excel môn học thành công rực rỡ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Lỗi khi xuất Excel: " + ex.Message, "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        //ĐÓNG FORM
        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvInKhoaHoc_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }
    }
}