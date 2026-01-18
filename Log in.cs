using C__project.HR;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C__project
{
    public partial class Log_in : Form
    {
        public Log_in()
        {
            InitializeComponent();
        }


        private void ResetLoginForm()
        {
            textBox1.Clear();      // username clear
            textBox2.Clear();      // password clear
            comboBox1.SelectedIndex = 2; // Client default
            textBox1.Focus();     // cursor username এ যাবে
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {
            
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {
            


        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string role = comboBox1.SelectedItem?.ToString();
            string username = textBox1.Text.Trim();
            string password = textBox2.Text;

            if (string.IsNullOrEmpty(role))
            {
                MessageBox.Show("Please select a role first.");
                return;
            }

            switch (role)
            {
                case "HR Manager":
                    if (username == "hr" && password == "1234")
                    {
                        Hr_Dash hr = new Hr_Dash();
                        hr.FormClosed += (s, args) =>
                        {
                            ResetLoginForm();
                            this.Show();
                        };
                        hr.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Invalid HR credentials");
                    }
                    break;

                case "Employee":
                    if (username == "emp" && password == "1234")
                    {
                        Employee_Dash emp = new Employee_Dash();
                        emp.FormClosed += (s, args) =>
                        {
                            ResetLoginForm();
                            this.Show();
                        };
                        emp.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Invalid Employee credentials");
                    }
                    break;

                case "Client":
                    if (username == "client" && password == "1234")
                    {
                        Client_Dash cli = new Client_Dash();
                        cli.FormClosed += (s, args) =>
                        {
                            ResetLoginForm();
                            this.Show();
                        };
                        cli.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Invalid Client credentials");
                    }
                    break;

                default:
                    MessageBox.Show("Invalid role selected");
                    break;
            }
        
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void Log_in_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            comboBox1.Items.Add("HR Manager");
            comboBox1.Items.Add("Employee");
            comboBox1.Items.Add("Client");

            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;

            comboBox1.SelectedIndex = 2;
        }
    }
}
