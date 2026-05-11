using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace employeemanagementsystem
{
    public partial class salary : Form
    {
        public salary()
        {
            InitializeComponent();
        }
        readonly SqlConnection Con = new SqlConnection(connectionString: @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\line\Documents\EMS.mdf;Integrated Security=True;Connect Timeout=30");
        private void fetchemp()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Eid.Text))
                {
                    MessageBox.Show("Enter Employee Id");
                    return;
                }

                Con.Open();
                string query = "SELECT * FROM ETBL WHERE EmpId = @EmpId";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.Parameters.AddWithValue("@EmpId", Eid.Text);

                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        En.Text = dr["EmpName"].ToString();
                        Ep.Text = dr["EmpPos"].ToString();
                    }
                }
                else
                {
                    MessageBox.Show("No record found with the given Employee Id");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (Con.State == System.Data.ConnectionState.Open)
                {
                    Con.Close();
                }
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Home h = new Home();
            h.Show();
            this.Close();
            this.Hide();
        }
        

        private void button1_Click(object sender, EventArgs e)
        {
            if (Ep.Text == "")
            {
                MessageBox.Show("Select an Employee");
            }
            else
            {
                int workedDays;
                if (string.IsNullOrWhiteSpace(Wd.Text) || !int.TryParse(Wd.Text, out workedDays) || workedDays > 31 || workedDays < 1)
                {
                    MessageBox.Show("Enter a valid number of Days between 1 and 31");
                }
                else
                {
                    int dailybase;
                    if (Ep.Text == "Manager")
                    {
                        dailybase = 1200;
                    }
                    else if (Ep.Text == "Senior Developer")
                    {
                        dailybase = 1000;
                    }
                    else if (Ep.Text == "Junior Developer")
                    {
                        dailybase = 900;
                    }
                    else
                    {
                        dailybase = 850;
                    }

                    int Total = dailybase * workedDays;
                    show.Text = "Employee Id = " + Eid.Text + "\n" +
                                "Employee Name = " + En.Text + "\n" +
                                "Employee Position = " + Ep.Text + "\n" +
                                "Daily Salary = " + dailybase + "\n" +
                                "Total Amount = " + Total;
                }
            }
        }


        private void Crossbutton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            fetchemp();
        }

        private void salary_Load(object sender, EventArgs e)
        {

        }
    }
}
