using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C__project.HR
{
    public partial class regEmp : UserControl
    {
        public regEmp()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text.Trim();
            string name = textBox2.Text.Trim();
            string address = textBox3.Text.Trim();
            string password = textBox4.Text.Trim();
            string jobPost = comboBox1.SelectedItem?.ToString();

            if (username == "" || name == "" || password == "" || jobPost == null)
            {
                MessageBox.Show("Please fill all required fields");
                return;
            }

            DataAccess da = new DataAccess();

            string query = $@"
        INSERT INTO Employee
        (Username, Name, Address, Password, DateOfBirth, JobPost)
        VALUES
        (
            '{username.Replace("'", "''")}',
            '{name.Replace("'", "''")}',
            '{address.Replace("'", "''")}',
            '{password.Replace("'", "''")}',
            '{dateTimePicker2.Value:yyyy-MM-dd}',
            '{jobPost}'
        )";

            int row = da.ExecuteDMLQuery(query);

            if (row > 0)
                MessageBox.Show("Employee registered successfully");
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Hr_Dash form = this.FindForm() as Hr_Dash;
            if (form != null)
            {
                form.LoadControl(new welcome());
            }
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            button1.BackColor = Color.MediumSpringGreen;
            button1.ForeColor = Color.White;
            pictureBox2.BackColor = Color.MediumSpringGreen;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.BackColor = Color.FromArgb(95, 168, 211); ;
            pictureBox2.BackColor = Color.FromArgb(95, 168, 211);
        }

        private void button2_MouseEnter(object sender, EventArgs e)
        {
            button2.BackColor = Color.Tomato;
            button2.ForeColor = Color.White;
            pictureBox1.BackColor = Color.Tomato;
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            button2.BackColor = Color.FromArgb(95, 168, 211); 
            pictureBox1.BackColor = Color.FromArgb(95, 168, 211);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void regEmp_Load(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
