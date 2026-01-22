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

            string userId = textBox1.Text.Trim();   // Employee UserId
            string fullName = textBox2.Text.Trim();   // Name
            string address = textBox3.Text.Trim();
            string password = textBox4.Text.Trim();
            string dob = dateTimePicker2.Value.ToString("yyyy-MM-dd");
            string jobPost = comboBox1.SelectedItem?.ToString();

            if (userId == "" || fullName == "" || password == "" || jobPost == null)
            {
                MessageBox.Show("Please fill all required fields");
                return;
            }

            DataAccess da = new DataAccess();

            // 1️⃣ Check User already exists in Users table
            string checkQuery = $@"
        SELECT * FROM Users
        WHERE UserId = '{userId.Replace("'", "''")}'";

            DataTable checkDt = da.ExecuteQueryTable(checkQuery);

            if (checkDt.Rows.Count > 0)
            {
                MessageBox.Show("This User ID already exists");
                return;
            }

            // 2️⃣ Insert Employee as User (Created by HR)
            string insertQuery = $@"
        INSERT INTO Users
        (
            UserId,
            Password,
            FullName,
            Address,
            DateOfBirth,
            CreatedBy,
            IsEmployee,
            CreatedDate
        )
        VALUES
        (
            '{userId.Replace("'", "''")}',
            '{password.Replace("'", "''")}',
            '{fullName.Replace("'", "''")}',
            '{address.Replace("'", "''")}',
            '{dob}',
            'HR',
            1,
            GETDATE()
        )";

            int row = da.ExecuteDMLQuery(insertQuery);

            if (row > 0)
            {
                MessageBox.Show("Employee registered successfully");

                // clear form
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                comboBox1.SelectedIndex = -1;
                dateTimePicker2.Value = DateTime.Now;
            }
            else
            {
                MessageBox.Show("Employee registration failed");
            }
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
