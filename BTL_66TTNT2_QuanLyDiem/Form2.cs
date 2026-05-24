using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace BTL_66TTNT2
{
    public partial class Form2 : Form
    {
        private DataTable dtDiem;
        public Form2()
        {
            InitializeComponent();
            InitializeDataGrid();
        }
        private void InitializeDataGrid()
        {
            dtDiem = new DataTable();
            dtDiem.Columns.Add("Họ và tên SV", typeof(string));
            dtDiem.Columns.Add("Môn học", typeof(string));
            dtDiem.Columns.Add("Điểm chuyên cần", typeof(double));
            dtDiem.Columns.Add("Điểm cuối kì", typeof(double));
            dtDiem.Columns.Add("Điểm TB", typeof(double));
            dataGridViewDiem.DataSource = dtDiem;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(dtDiem == null || dtDiem.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu điểm để tính điểm trung bình!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Form3 frmDiem = new Form3(dtDiem);
            frmDiem.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(textBoxSV.Text) || string.IsNullOrEmpty(comboBoxMonHoc.Text) || string.IsNullOrEmpty(textBoxChuyenCan.Text) || string.IsNullOrEmpty(textBoxCuoiKi.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin bắt buộc");
                return;
            }
            double ChuyenCan, CuoiKi;
            if(!double.TryParse(textBoxChuyenCan.Text, out ChuyenCan) || ChuyenCan < 0 || ChuyenCan > 10 )
            {
                MessageBox.Show("Điểm chuyên cần phải là số và nằm trong khoảng từ 0 đến 10");
                textBoxChuyenCan.Focus();
                return;
            }
            if(!double.TryParse(textBoxCuoiKi.Text, out CuoiKi) || CuoiKi < 0 || CuoiKi > 10)
            {
                MessageBox.Show("Điểm cuối kì phải là số và nằm trong khoảng từ 0 đến 10");
                textBoxCuoiKi.Focus();
                return;
            }
            double DiemTB = ChuyenCan * 0.4 + CuoiKi * 0.6;
            DiemTB = Math.Round(DiemTB, 2);
            dtDiem.Rows.Add(textBoxSV.Text, comboBoxMonHoc.Text, ChuyenCan, CuoiKi, DiemTB);
            textBoxSV.Text = "";
            textBoxChuyenCan.Text = "";
            textBoxCuoiKi.Text = "";
            textBoxSV.Focus();
            MessageBox.Show("Thêm điểm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void textBoxSV_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa bản ghi điểm này không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Lấy index của dòng hiện tại và tiến hành xóa trong DataTable
                int rowIndex = dataGridViewDiem.CurrentRow.Index;
                dataGridViewDiem.Rows.RemoveAt(rowIndex);
                MessageBox.Show("Đã xóa bản ghi điểm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        
            else
            {
                MessageBox.Show("Vui lòng chọn một dòng dữ liệu trong bảng để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
