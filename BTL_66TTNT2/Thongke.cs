using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BTL_66TTNT2
{
    public partial class Thongke : Form
    {
        string connectionString =
            "Data Source=DESKTOP-RPF9QH6;Initial Catalog=Bai_tap_lon;Integrated Security=True";

        public Thongke()
        {
            InitializeComponent();
        }

        private void Thongke_Load(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();

            // Tổng số sinh viên
            string sqlTong = "SELECT COUNT(*) FROM sinh_vien";
            SqlCommand cmdTong = new SqlCommand(sqlTong, connection);
            int tongSo = Convert.ToInt32(cmdTong.ExecuteScalar());

            // Số nam
            string sqlNam = "SELECT COUNT(*) FROM sinh_vien WHERE GioiTinh = N'Nam'";
            SqlCommand cmdNam = new SqlCommand(sqlNam, connection);
            int soNam = Convert.ToInt32(cmdNam.ExecuteScalar());

            // Số nữ
            string sqlNu = "SELECT COUNT(*) FROM sinh_vien WHERE GioiTinh = N'Nữ'";
            SqlCommand cmdNu = new SqlCommand(sqlNu, connection);
            int soNu = Convert.ToInt32(cmdNu.ExecuteScalar());

            connection.Close();

            // Tính phần trăm
            double ptNam = 0;
            double ptNu = 0;

            if (tongSo > 0)
            {
                ptNam = (double)soNam * 100 / tongSo;
                ptNu = (double)soNu * 100 / tongSo;
            }

            // Hiển thị
            labelTong.Text = "Tổng số sinh viên: " + tongSo;

            labelNam.Text = "Nam: " + Math.Round(ptNam, 1) + "%";

            labelNu.Text = "Nữ: " + Math.Round(ptNu, 1) + "%";
        }
    }
}