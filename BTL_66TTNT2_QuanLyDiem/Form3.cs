using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTL_66TTNT2
{
    public partial class Form3 : Form
    {
        private void TinhVaHienThiDiemTB(DataTable dtGoc)
        {
            DataTable dtKetQua = new DataTable();
            dtKetQua.Columns.Add("Tên môn học", typeof(string));
            dtKetQua.Columns.Add("Điểm TB", typeof(double));

            var danhSachTBMon = from row in dtGoc.AsEnumerable()
                                group row by row.Field<string>("Môn học") into nhomMon
                                select new
                                {
                                    TenMon = nhomMon.Key,
                                    DiemTBChung = Math.Round(nhomMon.Average(r => Convert.ToDouble(r["Điểm TB"])), 2)
                                };

            foreach (var item in danhSachTBMon)
            {
                dtKetQua.Rows.Add(item.TenMon, item.DiemTBChung);
            }

            dataGridView1.DataSource = dtKetQua;
        }
        public Form3()
        {
            InitializeComponent();
            System.Data.DataTable dtTrong = new System.Data.DataTable();
            dataGridView1.DataSource = dtTrong;
        }
        public Form3(DataTable dtGoc)
        {
            InitializeComponent();
            TinhVaHienThiDiemTB(dtGoc);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
