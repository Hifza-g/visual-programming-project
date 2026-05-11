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
    public partial class Attendance : Form
    {
        public Attendance()
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
                }
                else
                {
                    Con.Open();
                    string query = "SELECT * FROM ETBL WHERE EmpId = @EmpId";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.Parameters.AddWithValue("@EmpId", Eid.Text);
                    DataTable dt = new DataTable();
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    sda.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow  dr in dt.Rows)
                        {
                            En.Text = dr["EmpName"].ToString();
                            Ep.Text = dr["EmpPos"].ToString();
                        }
                    }
                    else
                    {
                        MessageBox.Show("No record found with the given Employee Id");
                    }

                    Con.Close();
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

         private void DisplayEmp()
        {
            try
            {
                Con.Open();
                string Query = "Select * from AT";
                SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
                SqlCommandBuilder builder = new SqlCommandBuilder(sda);
                var ds = new DataSet(Query);
                sda.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
                Con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Con.Close();
            }
        } 

        private void button1_Click(object sender, EventArgs e)
        {

            Eid.Text = " ";
            En.Text = " ";
            Ep.Text = " ";
            S.Text = " ";


        }

        private void Crossbutton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            fetchemp();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Eid.Text) ||
                string.IsNullOrWhiteSpace(En.Text) ||
                string.IsNullOrWhiteSpace(Ep.Text) ||
                string.IsNullOrWhiteSpace(S.Text))
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "INSERT INTO AT (EmpId, EmpName, EmpPos, Status, DateCreated) VALUES (@Eid, @En, @Ep, @S, @Dc)";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.Parameters.AddWithValue("@Eid", Eid.Text);
                    cmd.Parameters.AddWithValue("@En", En.Text);
                    cmd.Parameters.AddWithValue("@Ep", Ep.Text);
                    cmd.Parameters.AddWithValue("@S", S.Text);
                    cmd.Parameters.AddWithValue("@Dc", Dc.Value.Date);
                    cmd.ExecuteNonQuery();
                    Con.Close();
                    MessageBox.Show("Record Entered Successfully");
                    DisplayEmp();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
                finally
                {
                    if (Con.State == System.Data.ConnectionState.Open)
                    {
                        Con.Close();
                    }
                }
            }
        }


        private void Attendance_Load(object sender, EventArgs e)
        {
            DisplayEmp();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            Eid.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            En.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            Ep.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            S.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Eid.Text))
            {
                MessageBox.Show("Enter the Employee Id");
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString: @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\line\Documents\EMS.mdf;Integrated Security=True;Connect Timeout=30")) 
                {
                    con.Open();

                    string checkQuery = "SELECT COUNT(*) FROM AT WHERE EmpId = @EmpId";
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, con))
                    {
                        checkCmd.Parameters.AddWithValue("@EmpId", Eid.Text);
                        int recordCount = (int)checkCmd.ExecuteScalar();

                        if (recordCount == 0)
                        {
                            MessageBox.Show("No record exists with the given Employee Id");
                        }
                        else
                        {
                            string deleteQuery = "DELETE FROM AT WHERE EmpId = @EmpId";
                            using (SqlCommand deleteCmd = new SqlCommand(deleteQuery, con))
                            {
                                deleteCmd.Parameters.AddWithValue("@EmpId", Eid.Text);
                                deleteCmd.ExecuteNonQuery();
                                MessageBox.Show("Record Deleted Successfully");
                                DisplayEmp();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            Home h = new Home();
            h.Show();
        }
    }
}
