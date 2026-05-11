using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace employeemanagementsystem
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }

        private void home_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Crossbutton_Click(object sender, EventArgs e)
        {
            this.Close();
            Form1 F1 = new Form1();
            F1.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            Attendance a = new Attendance();
            a.Show();
          
        }

        private void loginbutton_Click(object sender, EventArgs e)
        {
            Employee E = new Employee();
            E.Show();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            View v = new View();
            v.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            salary s = new salary();
            s.Show();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f = new Form1();
            f.Show();
            

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }
    }
}
