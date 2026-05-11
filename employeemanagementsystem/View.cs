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

namespace employeemanagementsystem
{
    public partial class View : Form
    {
        public View()
        {
            InitializeComponent();
        }
        readonly SqlConnection Con = new SqlConnection(connectionString: @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\line\Documents\EMS.mdf;Integrated Security=True;Connect Timeout=30");
        private void fetchemp()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(textBox1.Text))
                {
                    MessageBox.Show("Enter Employee Id");
                    return;
                }

                Con.Open();
                string query = "SELECT * FROM ETBL WHERE EmpId = @EmpId";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.Parameters.AddWithValue("@EmpId", textBox1.Text);

                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        label11.Text = dr["EmpId"].ToString();
                        label15.Text = dr["EmpName"].ToString();
                        label12.Text = dr["EmpAdd"].ToString();
                        label16.Text = dr["EmpGen"].ToString();
                        label17.Text = dr["EmpPos"].ToString();
                        label18.Text = dr["EmpDob"].ToString();
                        label13.Text = dr["EmpPho"].ToString();
                        label14.Text = dr["EmpEdu"].ToString();

                        label11.Visible = true;
                        label15.Visible = true;
                        label12.Visible = true;
                        label16.Visible = true;
                        label17.Visible = true;
                        label18.Visible = true;
                        label13.Visible = true;
                        label14.Visible = true;
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

        private void button2_Click(object sender, EventArgs e)
        {
            Home h = new Home();
            h.Show();
            this.Close();
        }

        private void Crossbutton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            fetchemp();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            


        }

        private void View_Load(object sender, EventArgs e)
        {

        }
    }
}
