using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace TnsProject
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Clear();
            textBox1.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //TO-DO: check login username & password
            SqlConnection con = new SqlConnection("Data Source=Waheed-PC;Initial Catalog=tnsaccounts;Integrated Security=True");
            SqlDataAdapter sda = new SqlDataAdapter(@"SELECT *
  FROM [TNSACCOUNTS].[dbo].[Login] where Username='" + textBox1.Text + "'and Password='" + textBox2.Text + "'", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count == 1)
            {
                this.Hide();
                Salarymain main = new Salarymain();
                main.Show();
            }
            else
            {
                MessageBox.Show("Invalid Usmername & Password..!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}