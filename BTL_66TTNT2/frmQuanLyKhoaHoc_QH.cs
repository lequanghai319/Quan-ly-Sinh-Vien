using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BTL_66TTNT2
{
    public partial class frmQuanLyKhoaHoc : Form
    {
        SqlConnection connection;
        string query;
        SqlCommand cmd;
        SqlDataAdapter adapter;
        DataTable dt;
        string connectionString = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=Bai_tap_lon;Integrated Security=True";

        public frmQuanLyKhoaHoc()
        {
            InitializeComponent();
        }

        //LOAD FORM
        private void frmQuanLyKhoaHoc_Load(object sender, EventArgs e)
        {
            LoadData();
            SetDefaultComboBox();
        }

        //HÀM TẢI DỮ LIỆU LÊN BẢNG
        private void LoadData()
        {
            try
            {
                connection = new SqlConnection(connectionString);
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                // Cột SQL là KhoaHoc nhưng hiển thị lên UI là Môn Học
                query = "SELECT MaKhoaHoc AS [Mã Môn Học], TenKhoaHoc AS [Tên Môn Học], SoTinChi AS [Số Tín Chỉ], HocKy AS [Học Kỳ] FROM KhoaHoc";

                cmd = new SqlCommand(query, connection);
                adapter = new SqlDataAdapter(cmd);
                dt = new DataTable();
                adapter.Fill(dt);
                dgvKhoaHoc.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách môn học: " + ex.Message, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (connection != null && connection.State != ConnectionState.Closed)
                    connection.Close();
            }
        }

        private void SetDefaultComboBox()
        {
            if (cboHocKy.Items.Count > 0)
            {
                cboHocKy.SelectedIndex = 0;
            }
        }

        //NÚT THÊM
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaKhoaHoc.Text) || string.IsNullOrWhiteSpace(txtTenKhoaHoc.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Mã môn học và Tên môn học!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                connection = new SqlConnection(connectionString);
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                query = "INSERT INTO KhoaHoc (MaKhoaHoc, TenKhoaHoc, SoTinChi, HocKy) VALUES (@Ma, @Ten, @TinChi, @HocKy)";
                cmd = new SqlCommand(query, connection);

                cmd.Parameters.AddWithValue("@Ma", txtMaKhoaHoc.Text.Trim());
                cmd.Parameters.AddWithValue("@Ten", txtTenKhoaHoc.Text.Trim());
                cmd.Parameters.AddWithValue("@TinChi", numSoTinChi.Value);
                cmd.Parameters.AddWithValue("@HocKy", cboHocKy.Text);

                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    MessageBox.Show("Thêm môn học mới thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnLamMoi_Click(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể thêm môn học. Lỗi: " + ex.Message, "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (connection != null && connection.State != ConnectionState.Closed)
                    connection.Close();
            }
        }

        //NÚT SỬA
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaKhoaHoc.Text))
            {
                MessageBox.Show("Vui lòng chọn một môn học từ bảng để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                connection = new SqlConnection(connectionString);
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                query = "UPDATE KhoaHoc SET TenKhoaHoc = @Ten, SoTinChi = @TinChi, HocKy = @HocKy WHERE MaKhoaHoc = @Ma";
                cmd = new SqlCommand(query, connection);

                cmd.Parameters.AddWithValue("@Ma", txtMaKhoaHoc.Text.Trim());
                cmd.Parameters.AddWithValue("@Ten", txtTenKhoaHoc.Text.Trim());
                cmd.Parameters.AddWithValue("@TinChi", numSoTinChi.Value);
                cmd.Parameters.AddWithValue("@HocKy", cboHocKy.Text);

                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    MessageBox.Show("Cập nhật thông tin môn học thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnLamMoi_Click(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật dữ liệu: " + ex.Message, "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (connection != null && connection.State != ConnectionState.Closed)
                    connection.Close();
            }
        }

        //NÚT XÓA
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaKhoaHoc.Text))
            {
                MessageBox.Show("Vui lòng chọn một môn học cần xóa khỏi hệ thống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show($"Bạn có chắc chắn muốn xóa môn học có mã [{txtMaKhoaHoc.Text}] không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    connection = new SqlConnection(connectionString);
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    query = "DELETE FROM KhoaHoc WHERE MaKhoaHoc = @Ma";
                    cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@Ma", txtMaKhoaHoc.Text.Trim());

                    int rows = cmd.ExecuteNonQuery();

                    if (rows > 0)
                    {
                        MessageBox.Show("Xóa môn học thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnLamMoi_Click(sender, e);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa dữ liệu: " + ex.Message, "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (connection != null && connection.State != ConnectionState.Closed)
                        connection.Close();
                }
            }
        }

        //NÚT TÌM KIẾM
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string keyword = txtTenKhoaHoc.Text.Trim();

            if (keyword == "")
            {
                LoadData();
                return;
            }

            try
            {
                connection = new SqlConnection(connectionString);
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                query = "SELECT MaKhoaHoc AS [Mã Môn Học], TenKhoaHoc AS [Tên Môn Học], SoTinChi AS [Số Tín Chỉ], HocKy AS [Học Kỳ] FROM KhoaHoc WHERE TenKhoaHoc LIKE @TenSearch";

                cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@TenSearch", "%" + keyword + "%");

                adapter = new SqlDataAdapter(cmd);
                dt = new DataTable();
                adapter.Fill(dt);
                dgvKhoaHoc.DataSource = dt;

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy môn học nào!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm kiếm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (connection != null && connection.State != ConnectionState.Closed)
                    connection.Close();
            }
        }

        //NÚT LÀM MỚI
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtMaKhoaHoc.Clear();
            txtTenKhoaHoc.Clear();
            numSoTinChi.Value = numSoTinChi.Minimum;
            SetDefaultComboBox();

            txtMaKhoaHoc.Enabled = true;
            LoadData();
        }

        //CLICK VÀO DÒNG TRÊN GRID
        private void dgvKhoaHoc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvKhoaHoc.Rows[e.RowIndex];

                txtMaKhoaHoc.Text = row.Cells[0].Value?.ToString();
                txtTenKhoaHoc.Text = row.Cells[1].Value?.ToString();

                if (row.Cells[2].Value != null)
                {
                    numSoTinChi.Value = Convert.ToDecimal(row.Cells[2].Value);
                }

                cboHocKy.Text = row.Cells[3].Value?.ToString();

                txtMaKhoaHoc.Enabled = false;
            }
        }

        private void dgvKhoaHoc_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e) { }
        private void cboHocKy_SelectedIndexChanged(object sender, EventArgs e) { }
        private void label5_Click(object sender, EventArgs e) { }
        private void txtTenKhoaHoc_TextChanged(object sender, EventArgs e) { }
    }
}