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
    public partial class Employee : Form
    {
        public Employee()
        {
            InitializeComponent();
            DisplayEmp();
        }
        readonly SqlConnection Con = new SqlConnection(connectionString: @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\line\Documents\EMS.mdf;Integrated Security=True;Connect Timeout=30");

        private void DisplayEmp()
        {
            try
            {
                Con.Open();
                string Query = "Select * from ETBL";
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
        
        private void Crossbutton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void homebutton_Click(object sender, EventArgs e)
        {
            Home h = new Home();
            h.Show();
            this.Hide();
        }

        private void addbutton_Click(object sender, EventArgs e)
        {
            try
            {
                // Check for missing information
                if (string.IsNullOrWhiteSpace(textBox1.Text) ||
                    string.IsNullOrWhiteSpace(textBox2.Text) ||
                    string.IsNullOrWhiteSpace(textBox3.Text) ||
                    string.IsNullOrWhiteSpace(textBox4.Text) ||
                    comboBox1.SelectedItem == null ||
                    comboBox2.SelectedItem == null ||
                    comboBox3.SelectedItem == null)
                {
                    MessageBox.Show("Missing Information");
                }
                else
                {
                    Con.Open();
                    string query = "INSERT INTO ETBL (EmpId, EmpName, EmpAdd, EmpGen, EmpPos, EmpDob, EmpPho, EmpEdu) " +
                                   "VALUES (@EmpId, @EmpName, @EmpAdd, @EmpGen, @EmpPos, @EmpDob, @EmpPho, @EmpEdu)";
                    SqlCommand cmd = new SqlCommand(query, Con);

                    // Use parameters to prevent SQL injection
                    cmd.Parameters.AddWithValue("@EmpId", textBox1.Text);
                    cmd.Parameters.AddWithValue("@EmpName", textBox2.Text);
                    cmd.Parameters.AddWithValue("@EmpAdd", textBox3.Text);
                    cmd.Parameters.AddWithValue("@EmpGen", comboBox1.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@EmpPos", comboBox2.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@EmpDob", dateTimePicker1.Value.Date);
                    cmd.Parameters.AddWithValue("@EmpPho", textBox4.Text);
                    cmd.Parameters.AddWithValue("@EmpEdu", comboBox3.SelectedItem.ToString());

                    cmd.ExecuteNonQuery();
                    Con.Close();
                    MessageBox.Show("Record Entered Successfully");
                    DisplayEmp();
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


        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Employee_Load(object sender, EventArgs e)
        {
            DisplayEmp();
        }

        private void deletebutton_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(textBox1.Text))
                {
                    MessageBox.Show("Enter the Employee Id");
                }
                else
                {
                    Con.Open();
                    string query = "DELETE FROM ETBL WHERE EmpId = @EmpId";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.Parameters.AddWithValue("@EmpId", textBox1.Text);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    Con.Close();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Record Deleted Successfully");
                    }
                    else
                    {
                        MessageBox.Show("No record found with the given Employee Id");
                    }

                    DisplayEmp();
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

        private void resetbutton_Click(object sender, EventArgs e)
        {
            textBox1.Text = " ";
            textBox2.Text = " ";
            textBox3.Text = " ";
            comboBox1.Text = " ";
            comboBox2.Text = " ";
            textBox4.Text = " ";
            comboBox3.Text = " ";
        }

        private void updatebutton_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(textBox1.Text) ||
                    string.IsNullOrWhiteSpace(textBox2.Text) ||
                    string.IsNullOrWhiteSpace(textBox3.Text) ||
                    string.IsNullOrWhiteSpace(comboBox1.Text) ||
                    string.IsNullOrWhiteSpace(comboBox2.Text) ||
                    string.IsNullOrWhiteSpace(textBox4.Text) ||
                    string.IsNullOrWhiteSpace(comboBox3.Text))
                {
                    MessageBox.Show("Missing Information");
                }
                else
                {
                    Con.Open();
                    string query = "UPDATE ETBL SET EmpName = @EmpName, EmpAdd = @EmpAdd, EmpGen = @EmpGen, " +
                                   "EmpPos = @EmpPos, EmpDob = @EmpDob, EmpPho = @EmpPho, EmpEdu = @EmpEdu WHERE EmpId = @EmpId";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.Parameters.AddWithValue("@EmpName", textBox2.Text);
                    cmd.Parameters.AddWithValue("@EmpAdd", textBox3.Text);
                    cmd.Parameters.AddWithValue("@EmpGen", comboBox1.Text);
                    cmd.Parameters.AddWithValue("@EmpPos", comboBox2.Text);
                    cmd.Parameters.AddWithValue("@EmpDob", dateTimePicker1.Value.Date);
                    cmd.Parameters.AddWithValue("@EmpPho", textBox4.Text);
                    cmd.Parameters.AddWithValue("@EmpEdu", comboBox3.Text);
                    cmd.Parameters.AddWithValue("@EmpId", textBox1.Text);

                    cmd.ExecuteNonQuery();
                    Con.Close();
                    MessageBox.Show("Record Updated Successfully");
                    DisplayEmp();
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


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            comboBox1.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            comboBox2.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            textBox4.Text = dataGridView1.SelectedRows[0].Cells[6].Value.ToString();
            comboBox3.Text = dataGridView1.SelectedRows[0].Cells[7].Value.ToString();
        }
    }
}
