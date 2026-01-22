using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace C__project.LogIn
{
    public partial class SignIn : UserControl
    {
        public SignIn()
        {

            InitializeComponent();

           
        }


       

        private void button1_Click(object sender, EventArgs e)
        {
            string userId = textBox1.Text.Trim();
            string password = textBox2.Text.Trim();

            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("User ID and Password required");
                return;
            }

            DataAccess da = new DataAccess();

            string query = $@"
        SELECT CreatedBy, IsEmployee
        FROM Users
        WHERE UserId = '{userId.Replace("'", "''")}'
        AND Password = '{password.Replace("'", "''")}'";

            DataTable dt = da.ExecuteQueryTable(query);

            if (dt.Rows.Count == 1)
            {
                // ✅ Save session
                Session.UserId = userId;
                Session.CreatedBy = dt.Rows[0]["CreatedBy"].ToString();
                Session.IsEmployee = Convert.ToBoolean(dt.Rows[0]["IsEmployee"]);

                // ✅ Open correct dashboard
                if (Session.CreatedBy == "HR")
                {
                    new Hr_Dash().Show();
                }
                else if (Session.IsEmployee)
                {
                    new Employee_Dash().Show();
                }
                else
                {
                    new Client_Dash().Show();
                }

                // ✅ Hide login form ONCE
                this.FindForm().Hide();
            }
            else
            {
                MessageBox.Show("Invalid login");
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
