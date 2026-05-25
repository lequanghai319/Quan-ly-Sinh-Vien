using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BTL_66TTNT2
{
    public partial class frmQuanLyKhoaHoc : Form
    {
        // THAY THẾ: Thay "KHOAHOC_DB" bằng tên cơ sở dữ liệu và Data Source bằng tên Server của bạn
        private string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=BaiHocDauTien;Integrated Security=True";

        public frmQuanLyKhoaHoc()
        {
            InitializeComponent();
        }

        // Tự động tải toàn bộ danh sách lên DataGridView khi mở form
        private void frmQuanLyKhoaHoc_Load(object sender, EventArgs e)
        {
            LoadData();
            SetDefaultComboBox();
        }

        // Hàm bổ trợ: Tải dữ liệu từ SQL Server vào DataGridView
        private void LoadData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT MaKhoaHoc AS [Mã Khóa Học], TenKhoaHoc AS [Tên Khóa Học], SoTinChi AS [Số Tín Chỉ], HocKy AS [Học Kỳ] FROM KhoaHoc";

                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dgvKhoaHoc.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách khóa học: " + ex.Message, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Hàm bổ trợ: Đưa ComboBox học kỳ về giá trị mặc định đầu tiên
        private void SetDefaultComboBox()
        {
            if (cboHocKy.Items.Count > 0)
            {
                cboHocKy.SelectedIndex = 0; // Chọn mục đầu tiên trong danh sách
            }
        }

        // Chức năng: Thêm khóa học mới
        private void btnThem_Click(object sender, EventArgs e)
        {
            // Kiểm tra không để trống các trường thông tin bắt buộc
            if (string.IsNullOrWhiteSpace(txtMaKhoaHoc.Text) || string.IsNullOrWhiteSpace(txtTenKhoaHoc.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Mã khóa học và Tên khóa học!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "INSERT INTO KhoaHoc (MaKhoaHoc, TenKhoaHoc, SoTinChi, HocKy) VALUES (@Ma, @Ten, @TinChi, @HocKy)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Ma", txtMaKhoaHoc.Text.Trim());
                        cmd.Parameters.AddWithValue("@Ten", txtTenKhoaHoc.Text.Trim());
                        cmd.Parameters.AddWithValue("@TinChi", numSoTinChi.Value);
                        cmd.Parameters.AddWithValue("@HocKy", cboHocKy.Text);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Thêm khóa học mới thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        btnLamMoi_Click(sender, e); // Thêm xong tự làm sạch form
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể thêm khóa học. Lỗi: " + ex.Message, "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Chức năng: Sửa thông tin khóa học
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaKhoaHoc.Text))
            {
                MessageBox.Show("Vui lòng chọn một khóa học từ bảng để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    // Chỉ cập nhật Tên, Số tín chỉ, Học kỳ theo đúng Mã khóa học
                    string query = "UPDATE KhoaHoc SET TenKhoaHoc = @Ten, SoTinChi = @TinChi, HocKy = @HocKy WHERE MaKhoaHoc = @Ma";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Ma", txtMaKhoaHoc.Text.Trim());
                        cmd.Parameters.AddWithValue("@Ten", txtTenKhoaHoc.Text.Trim());
                        cmd.Parameters.AddWithValue("@TinChi", numSoTinChi.Value);
                        cmd.Parameters.AddWithValue("@HocKy", cboHocKy.Text);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Cập nhật thông tin khóa học thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        btnLamMoi_Click(sender, e);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật dữ liệu: " + ex.Message, "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Chức năng: Xóa khóa học
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaKhoaHoc.Text))
            {
                MessageBox.Show("Vui lòng chọn một khóa học cần xóa khỏi hệ thống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Hiển thị hộp thoại xác nhận Yes/No trước khi xóa dữ liệu
            DialogResult result = MessageBox.Show($"Bạn có chắc chắn muốn xóa khóa học có mã [{txtMaKhoaHoc.Text}] không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        string query = "DELETE FROM KhoaHoc WHERE MaKhoaHoc = @Ma";

                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@Ma", txtMaKhoaHoc.Text.Trim());
                            cmd.ExecuteNonQuery();

                            MessageBox.Show("Xóa khóa học thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            btnLamMoi_Click(sender, e);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa dữ liệu: " + ex.Message, "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Chức năng: Tìm kiếm theo tên sử dụng từ khóa LIKE
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT MaKhoaHoc AS [Mã Khóa Học], TenKhoaHoc AS [Tên Khóa Học], SoTinChi AS [Số Tín Chỉ], HocKy AS [Học Kỳ] FROM KhoaHoc WHERE TenKhoaHoc LIKE @TenSearch";

                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                    {
                        // Từ khóa dạng %chuỗi_tìm_kiếm% phục vụ câu lệnh LIKE
                        adapter.SelectCommand.Parameters.AddWithValue("@TenSearch", "%" + txtTenKhoaHoc.Text.Trim() + "%");

                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dgvKhoaHoc.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm kiếm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Chức năng: Làm mới ô nhập liệu, đưa ComboBox về mặc định, mở khóa ô Mã
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtMaKhoaHoc.Clear();
            txtTenKhoaHoc.Clear();
            numSoTinChi.Value = numSoTinChi.Minimum; // Đưa về giá trị nhỏ nhất (thường là 0)
            SetDefaultComboBox();

            txtMaKhoaHoc.Enabled = true; // Mở khóa ô mã để sẵn sàng thêm mới
            LoadData(); // Tải lại toàn bộ bảng
        }

        // Sự kiện: Click vào một dòng trên bảng DataGridView -> Tự động điền dữ liệu lên Form
        private void dgvKhoaHoc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra xem người dùng có click vào dòng dữ liệu hợp lệ không (tránh click vào hàng tiêu đề)
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvKhoaHoc.Rows[e.RowIndex];

                // Điền dữ liệu từ các cột tương ứng lên các điều khiển nhập liệu
                txtMaKhoaHoc.Text = row.Cells[0].Value?.ToString();
                txtTenKhoaHoc.Text = row.Cells[1].Value?.ToString();

                if (row.Cells[2].Value != null)
                {
                    numSoTinChi.Value = Convert.ToDecimal(row.Cells[2].Value);
                }

                cboHocKy.Text = row.Cells[3].Value?.ToString();

                // Khóa ô Mã khóa học để không cho phép sửa thông tin định danh này
                txtMaKhoaHoc.Enabled = false;
            }
        }

        private void dgvKhoaHoc_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}