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
    public partial class Quan_ly_sv : Form
    {
        SqlConnection connection;
        string query;
        SqlCommand cmd;
        SqlDataAdapter adapter;
        DataTable dt;
        string connectionString = "Data Source=DESKTOP-RPF9QH6;Initial Catalog=Bai_tap_lon;Integrated Security=True";

        public Quan_ly_sv()
        {
            InitializeComponent();
        }

        // ===================== LOAD FORM =====================
        private void Quan_ly_sv_Load(object sender, EventArgs e)
        {
            fillGrid(null);
        }

        // ===================== FILL DATAGRIDVIEW =====================
        public void fillGrid(SqlCommand command)
        {
            try
            {
                connection = new SqlConnection(connectionString);
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                if (command == null)
                {
                    query = "SELECT * FROM sinh_vien";
                    cmd = new SqlCommand(query, connection);
                }
                else
                {
                    command.Connection = connection;
                    cmd = command;
                }

                adapter = new SqlDataAdapter(cmd);
                dt = new DataTable();
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (connection != null && connection.State != ConnectionState.Closed)
                    connection.Close();
            }
        }

        // ===================== CLICK VÀO DÒNG TRÊN GRID =====================
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                textBox1.Text = row.Cells["ID"].Value.ToString();
                txt_name.Text = row.Cells["TenSinhVien"].Value.ToString();

                if (row.Cells["NgaySinh"].Value != DBNull.Value)
                    dateTimePicker1.Value = Convert.ToDateTime(row.Cells["NgaySinh"].Value);

                string gioiTinh = row.Cells["GioiTinh"].Value.ToString();
                if (gioiTinh == "Nam")
                    rb_nam.Checked = true;
                else
                    rb_nu.Checked = true;

                // SoDienThoai và DiaChi có thể NULL
                txt_sdt.Text = row.Cells["SoDienThoai"].Value == DBNull.Value ? ""
                                  : row.Cells["SoDienThoai"].Value.ToString();
                txt_diachi.Text = row.Cells["DiaChi"].Value == DBNull.Value ? ""
                                  : row.Cells["DiaChi"].Value.ToString();
            }
        }

        // ===================== LẤY GIỚI TÍNH =====================
        private string getGioiTinh()
        {
            if (rb_nam.Checked) return "Nam";
            return "Nữ";
        }

        // ===================== NÚT THÊM =====================
        private void btn_them_Click(object sender, EventArgs e)
        {
            if (txt_name.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập tên sinh viên!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_name.Focus();
                return;
            }

            try
            {
                connection = new SqlConnection(connectionString);
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                // ID là IDENTITY nên không cần nhập
                query = "INSERT INTO sinh_vien (TenSinhVien, NgaySinh, GioiTinh, SoDienThoai, DiaChi) " +
                        "VALUES (N'" + txt_name.Text + "', '" +
                        dateTimePicker1.Value.ToString("yyyy-MM-dd") + "', N'" +
                        getGioiTinh() + "', '" +
                        txt_sdt.Text + "', N'" +
                        txt_diachi.Text + "')";

                cmd = new SqlCommand(query, connection);
                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    MessageBox.Show("Thêm sinh viên thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    fillGrid(null);
                    clearForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thêm: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (connection.State != ConnectionState.Closed)
                    connection.Close();
            }
        }

        // ===================== NÚT SỬA =====================
        private void btn_sua_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng chọn sinh viên cần sửa từ danh sách!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                connection = new SqlConnection(connectionString);
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                query = "UPDATE sinh_vien SET " +
                        "TenSinhVien = N'" + txt_name.Text + "', " +
                        "NgaySinh = '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "', " +
                        "GioiTinh = N'" + getGioiTinh() + "', " +
                        "SoDienThoai = '" + txt_sdt.Text + "', " +
                        "DiaChi = N'" + txt_diachi.Text + "' " +
                        "WHERE ID = " + textBox1.Text;

                cmd = new SqlCommand(query, connection);
                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    MessageBox.Show("Cập nhật thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    fillGrid(null);
                    clearForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi sửa: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (connection.State != ConnectionState.Closed)
                    connection.Close();
            }
        }

        // ===================== NÚT XÓA =====================
        private void btn_xoa_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng chọn sinh viên cần xóa từ danh sách!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show(
                "Bạn có chắc muốn xóa sinh viên \"" + txt_name.Text + "\" không?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    connection = new SqlConnection(connectionString);
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    query = "DELETE FROM sinh_vien WHERE ID = " + textBox1.Text;
                    cmd = new SqlCommand(query, connection);
                    int rows = cmd.ExecuteNonQuery();

                    if (rows > 0)
                    {
                        MessageBox.Show("Xóa thành công!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        fillGrid(null);
                        clearForm();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xóa: " + ex.Message, "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (connection.State != ConnectionState.Closed)
                        connection.Close();
                }
            }
        }

        // ===================== NÚT TÌM KIẾM =====================
        private void btn_search_Click(object sender, EventArgs e)
        {
            string keyword = txt_search.Text.Trim();

            if (keyword == "")
            {
                fillGrid(null);
                return;
            }

            try
            {
                connection = new SqlConnection(connectionString);
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                query = "SELECT * FROM sinh_vien WHERE TenSinhVien LIKE N'%" + keyword + "%'";

                cmd = new SqlCommand(query, connection);
                adapter = new SqlDataAdapter(cmd);
                dt = new DataTable();
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;

                if (dt.Rows.Count == 0)
                    MessageBox.Show("Không tìm thấy sinh viên nào!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm kiếm: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (connection.State != ConnectionState.Closed)
                    connection.Close();
            }
        }

        // ===================== NÚT LÀM MỚI =====================
        private void btn_lammoi_Click(object sender, EventArgs e)
        {
            clearForm();
            fillGrid(null);
        }

        // ===================== HÀM XÓA TRẮNG FORM =====================
        private void clearForm()
        {
            textBox1.Text = "";
            txt_name.Text = "";
            dateTimePicker1.Value = DateTime.Now;
            rb_nam.Checked = true;
            txt_sdt.Text = "";
            txt_diachi.Text = "";
            txt_search.Text = "";
        }

        
    }
}