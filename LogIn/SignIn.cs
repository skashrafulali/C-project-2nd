using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C__project.LogIn
{
    public partial class SignIn : UserControl
    {
        public SignIn()
        {

            InitializeComponent();

            comboBox1.Items.Clear();
            comboBox1.Items.Add("HR Manager");
            comboBox1.Items.Add("Employee");
            comboBox1.Items.Add("Client");

            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.SelectedIndex = 2;
        }


       

        private void button1_Click(object sender, EventArgs e)
        {
            string role = comboBox1.SelectedItem?.ToString();
            string username = textBox1.Text.Trim();
            string password = textBox2.Text;

            if (string.IsNullOrEmpty(role))
            {
                MessageBox.Show("Please select a role first.");
                return;
            }

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Username and password required.");
                return;
            }

            DataAccess da = new DataAccess();
            DataTable dt;
            string query = "";

            switch (role)
            {
                
                case "Client":
                    {
                        query = $@"
                SELECT * FROM dbo.Client
                WHERE ClientId = '{username.Replace("'", "''")}'
                AND Password = '{password.Replace("'", "''")}'";

                        dt = da.ExecuteQueryTable(query);

                        if (dt.Rows.Count == 1)
                        {
                            Client_Dash cli = new Client_Dash();
                            cli.Show();
                            Log_in loginForm = this.FindForm() as Log_in;
                            if (loginForm != null)
                            {
                                loginForm.Hide();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Invalid Client username or password");
                        }
                        break;
                    }

                
                case "HR Manager":
                    {
                        query = $@"
                SELECT * FROM dbo.HR
                WHERE Username = '{username.Replace("'", "''")}'
                AND Password = '{password.Replace("'", "''")}'";

                        dt = da.ExecuteQueryTable(query);

                        if (dt.Rows.Count == 1)
                        {
                            Hr_Dash hr = new Hr_Dash();
                            hr.Show();
                            Log_in loginForm = this.FindForm() as Log_in;
                            if (loginForm != null)
                            {
                                loginForm.Hide();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Invalid HR credentials");
                        }
                        break;
                    }

                
                case "Employee":
                    {
                        query = $@"
    SELECT * FROM dbo.Employee
    WHERE EmpId = '{username.Replace("'", "''")}'
    AND Password = '{password.Replace("'", "''")}'";

                        dt = da.ExecuteQueryTable(query);

                        if (dt.Rows.Count == 1)
                        {
                            Employee_Dash emp = new Employee_Dash();
                            emp.Show();
                            Log_in loginForm = this.FindForm() as Log_in;
                            if (loginForm != null)
                            {
                                loginForm.Hide();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Invalid Employee ID or Password");
                        }
                        break;
                    }

                default:
                    MessageBox.Show("Invalid role selected");
                    break;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Log_in form = this.FindForm() as Log_in;
            if (form != null)
            {
                form.LoadControl(new SignUp());
            }
        }
    }

}
