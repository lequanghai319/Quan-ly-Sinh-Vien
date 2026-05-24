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
    public partial class Form_dang_nhap : Form
    {
        public Form_dang_nhap()
        {
            InitializeComponent();
        }
        SqlConnection connection;
        string query;
        SqlCommand cmd;
        SqlDataAdapter adapter;
        DataTable dt;
        private void Form_dang_nhap_Load(object sender, EventArgs e)
        {

        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            string user, password;
            user = txt_ten_dn.Text;
            password = txt_mk.Text;
            try
            {
                connection = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=Bai_tap_lon;Integrated Security=True");
                if(connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                
                query = "SELECT * FROM dang_nhap where username = N'" + txt_ten_dn.Text + "' AND password = '" + txt_mk.Text + "'";
                adapter = new SqlDataAdapter(query,connection);
                dt = new DataTable();
                adapter.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    Form1 form = new Form1();
                    form.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Lỗi đăng nhập", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txt_ten_dn.Clear();
                    txt_mk.Clear();
                    txt_ten_dn.Focus();
                }
            }
            catch
            {
                MessageBox.Show("Lỗi đăng nhập");
            }
            finally
            {
                if (connection.State != ConnectionState.Closed)
                {
                    connection.Close();
                }
            }
        }

        private void btn_refresh_Click(object sender, EventArgs e)
        {
            txt_ten_dn.Clear();
            txt_mk.Clear();
            txt_ten_dn.Focus();
        }

        private void btn_thoat_Click(object sender, EventArgs e)
        {
            DialogResult result;
            result = MessageBox.Show("Bạn có muốn thoát không?", "Thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(result == DialogResult.Yes)
            {
                Application.Exit();
            }
            else
            {
                this.Show();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            txt_mk.UseSystemPasswordChar = !checkBox1.Checked;
        }
    }
}
