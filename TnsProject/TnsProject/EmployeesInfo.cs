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
    public partial class EmployeesInfo : Form
    {
        public EmployeesInfo()
        {
            InitializeComponent();
        }

        private void EmployeeIn_Load(object sender, EventArgs e)
        {
            comboBox2.SelectedIndex = 0;
            LoaDdata();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=Waheed-PC;Initial Catalog=TNSACCOUNTS;Integrated Security=True");
            // Inset logic
            con.Open();
            bool status = false;
            if (comboBox2.SelectedIndex == 0)
            {
                status = true;
            }
            else
            {
                status = false;
                                
            }
            var Sqlquery = "";
            
        
            if (IfEmployeesInfExists(con, textBox6.Text))
            {
                Sqlquery = @"UPDATE [Employeesinfo] SET [Name] = '" + textBox1.Text + "'  ,[Country] = '" + status + "'WHERE [EmployeeID] = '" + textBox6.Text + "'";
            }
            else
            {
                Sqlquery = @"INSERT INTO [TNSACCOUNTS].[dbo].[Employeesinfo] ([EmployeeID],[Name],[Country]) VALUES
           
                    ('" + textBox6.Text + "','" + textBox1.Text + "','" + status + "')";
            }
            

            SqlCommand cmd = new SqlCommand(Sqlquery, con);
            cmd.ExecuteNonQuery();
            con.Close();

            //READING DATA  
            LoaDdata();
        }

        private bool IfEmployeesInfExists(SqlConnection con, String employeesID)
        {
            SqlDataAdapter sda = new SqlDataAdapter("Select * From [TNSACCOUNTS].[dbo].[EmployeesInfo] Where [EmployeeID]='" + employeesID + "'", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
                return true;
            else
                return false;

             }
                 public void LoaDdata() {
            SqlConnection con = new SqlConnection("Data Source=Waheed-PC;Initial Catalog=TNSACCOUNTS;Integrated Security=True");
            SqlDataAdapter sda = new SqlDataAdapter("Select * From [TNSACCOUNTS].[dbo].[EmployeesInfo]", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            

            dataGridView1.Rows.Clear();
            foreach (DataRow item in dt.Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = item["EmployeeID"].ToString();
                dataGridView1.Rows[n].Cells[1].Value = item["Name"].ToString();
                if ((bool)item["Country"])
                {
                    dataGridView1.Rows[n].Cells[2].Value = "Dubai";
                }
                else
                {
                    dataGridView1.Rows[n].Cells[2].Value = "Abudhabi";
                }
                 }
                
            }

                 private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
                 {
                     textBox6.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                     textBox1.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                     if (dataGridView1.SelectedRows[0].Cells[2].Value.ToString() == "Dubai")
                     {
                         comboBox2.SelectedIndex = 0;
                     }
                     else
                     {
                         comboBox2.SelectedIndex = 1;
                     }
                      

                 }

                 public string EmployeeID { get; set; }

                 private void button1_Click(object sender, EventArgs e)
                 {
                     SqlConnection con = new SqlConnection("Data Source=Waheed-PC;Initial Catalog=TNSACCOUNTS;Integrated Security=True");
                     var Sqlquery = "";


                     if (IfEmployeesInfExists(con, textBox6.Text))
                     {
                         con.Open();
                         Sqlquery = @"DELETE FROM[Employeesinfo] WHERE [EmployeeID] = '" + textBox6.Text + "'";
                         SqlCommand cmd = new SqlCommand(Sqlquery, con);
                         cmd.ExecuteNonQuery();
                         con.Close();
                                              }
                     else
                     {
                         MessageBox.Show("Record Not Exists..!");
                     }

                             

                     //READING DATA  
                     LoaDdata();
                 }
    }
    }

