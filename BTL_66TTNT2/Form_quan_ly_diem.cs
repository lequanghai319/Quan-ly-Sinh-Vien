using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace BTL_66TTNT2
{
    public partial class Form_QL_Diem : Form
    {
        private DataTable dtDiem;

        public Form_QL_Diem()
        {
            InitializeComponent();
            InitializeDataGrid();
            SQLData();
            LoadSinhVienToComboBox();
        }

        private void InitializeDataGrid()
        {
            dtDiem = new DataTable();
            dtDiem.Columns.Add("Họ và tên SV", typeof(string));
            dtDiem.Columns.Add("Môn học", typeof(string));
            dtDiem.Columns.Add("Điểm chuyên cần", typeof(double));
            dtDiem.Columns.Add("Điểm cuối kì", typeof(double));
            dtDiem.Columns.Add("Điểm TB", typeof(double));
            dataGridViewDiem.AutoGenerateColumns = false;
            dataGridViewDiem.DataSource = dtDiem;
        }

        private void SQLData()
        {
            try
            {
                string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=Bai_tap_lon;Integrated Security=True";
                string query = "SELECT HoTenSV, MonHoc, DiemChuyenCan, DiemCuoiKy, DiemTB FROM DiemSinhVien";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            DataTable dtTemp = new DataTable();
                            adapter.Fill(dtTemp);

                            dtDiem.Rows.Clear();

                            foreach (DataRow row in dtTemp.Rows)
                            {
                                dtDiem.Rows.Add(
                                    row["HoTenSV"].ToString(),
                                    row["MonHoc"].ToString(),
                                    row["DiemChuyenCan"] != DBNull.Value ? Convert.ToDouble(row["DiemChuyenCan"]) : 0,
                                    row["DiemCuoiKy"] != DBNull.Value ? Convert.ToDouble(row["DiemCuoiKy"]) : 0,
                                    row["DiemTB"] != DBNull.Value ? Convert.ToDouble(row["DiemTB"]) : 0
                                );
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi kết nối cơ sở dữ liệu: " + ex.Message, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadSinhVienToComboBox()
        {
            try
            {
                string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=Bai_tap_lon;Integrated Security=True";
                string query = "SELECT TenSinhVien FROM [dbo].[sinh_vien]";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            comboBoxSV.Items.Clear();
                            while (reader.Read())
                            {
                                if (reader["TenSinhVien"] != DBNull.Value)
                                {
                                    comboBoxSV.Items.Add(reader["TenSinhVien"].ToString());
                                }
                            }
                        }
                    }
                }
                if (comboBoxSV.Items.Count > 0)
                {
                    comboBoxSV.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách sinh viên: " + ex.Message, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dtDiem == null || dtDiem.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu điểm để tính điểm trung bình!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Form_Diem_TB frmDiem = new Form_Diem_TB(dtDiem);
            frmDiem.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBoxSV.Text) || string.IsNullOrEmpty(comboBoxMonHoc.Text) || string.IsNullOrEmpty(textBoxChuyenCan.Text) || string.IsNullOrEmpty(textBoxCuoiKi.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin bắt buộc");
                return;
            }

            double ChuyenCan, CuoiKi;
            if (!double.TryParse(textBoxChuyenCan.Text, out ChuyenCan) || ChuyenCan < 0 || ChuyenCan > 10)
            {
                MessageBox.Show("Điểm chuyên cần phải là số và nằm trong khoảng từ 0 đến 10");
                textBoxChuyenCan.Focus();
                return;
            }
            if (!double.TryParse(textBoxCuoiKi.Text, out CuoiKi) || CuoiKi < 0 || CuoiKi > 10)
            {
                MessageBox.Show("Điểm cuối kì phải là số và nằm trong khoảng từ 0 đến 10");
                textBoxCuoiKi.Focus();
                return;
            }

            double DiemTB = ChuyenCan * 0.4 + CuoiKi * 0.6;
            DiemTB = Math.Round(DiemTB, 2);

            dtDiem.Rows.Add(comboBoxSV.Text, comboBoxMonHoc.Text, ChuyenCan, CuoiKi, DiemTB);
            textBoxChuyenCan.Text = "";
            textBoxCuoiKi.Text = "";
            comboBoxSV.Focus();
            MessageBox.Show("Thêm điểm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridViewDiem.CurrentRow != null && !dataGridViewDiem.CurrentRow.IsNewRow)
            {
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa bản ghi điểm này không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    int rowIndex = dataGridViewDiem.CurrentRow.Index;
                    dataGridViewDiem.Rows.RemoveAt(rowIndex);
                    MessageBox.Show("Đã xóa bản ghi điểm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một dòng dữ liệu hợp lệ trong bảng để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }
    }
}