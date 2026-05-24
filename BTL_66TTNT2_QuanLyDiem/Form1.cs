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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void quảnLýSinhViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Quan_ly_sv qlSV = new Quan_ly_sv();
            qlSV.Show(this);

        }

        private void thốngKêToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thongke tk = new Thongke();
            tk.Show(this);
        }

        private void btn_THOAT_Click(object sender, EventArgs e)
        {
            DialogResult result;
            result = MessageBox.Show("Bạn có muốn thoát không?", "Thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
            else
            {
                this.Show();
            }
        }

        private void aDDSCOREToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 frmDiem = new Form2();
            frmDiem.Show();
        }

        private void aVGSCOREToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 frmDiem = new Form3();
            frmDiem.Show();
        }
    }
}
