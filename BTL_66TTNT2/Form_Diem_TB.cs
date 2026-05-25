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
    public partial class Form_Diem_TB : Form
    {
        private string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=Bai_tap_lon;Integrated Security=True";

        private void TinhVaHienThiDiemTB(DataTable dtGoc)
        {
            DataTable dtKetQua = new DataTable();
            dtKetQua.Columns.Add("Tên môn học", typeof(string));
            dtKetQua.Columns.Add("Điểm TB", typeof(double));

            var danhSachTBMon = from row in dtGoc.AsEnumerable()
                                group row by row.Field<string>("MonHoc") into nhomMon
                                select new
                                {
                                    TenMon = nhomMon.Key,
                                    DiemTBChung = Math.Round(nhomMon.Average(r => Convert.ToDouble(r["DiemTB"])), 2)
                                };

            foreach (var item in danhSachTBMon)
            {
                dtKetQua.Rows.Add(item.TenMon, item.DiemTBChung);
            }

            dataGridView1.DataSource = dtKetQua;
        }
        public Form_Diem_TB()
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;
            LayDuLieuVaTinhDiemTB();
        }
        public Form_Diem_TB(DataTable dtGoc)
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;
            TinhVaHienThiDiemTB(dtGoc);
        }
        private void LayDuLieuVaTinhDiemTB()
        {
            try
            {
                DataTable dtGoc = new DataTable();
                string query = "SELECT MonHoc, DiemTB FROM DiemSinhVien";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(dtGoc);
                        }
                    }
                }
                if (dtGoc.Rows.Count == 0)
                {
                    MessageBox.Show("Chưa có dữ liệu bản ghi điểm nào trong SQL Server!", "Thông báo");
                    return;
                }
                TinhVaHienThiDiemTB(dtGoc);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy dữ liệu từ SQL Server: " + ex.Message, "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
