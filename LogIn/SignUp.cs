using System;
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
    public partial class SignUp : UserControl
    {
        public SignUp()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Log_in form = this.FindForm() as Log_in;
            if (form != null)
            {
                form.LoadControl(new SignIn());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string clientId = textBox3.Text.Trim();   
            string username = textBox1.Text.Trim();   
            string password = textBox2.Text.Trim();   

            if (clientId == "" || username == "" || password == "")
            {
                MessageBox.Show("All fields are required");
                return;
            }

            DataAccess da = new DataAccess();

            
            string checkQuery =
                $"SELECT * FROM dbo.Client WHERE ClientId = '{clientId.Replace("'", "''")}'";

            DataTable checkDt = da.ExecuteQueryTable(checkQuery);

            if (checkDt.Rows.Count > 0)
            {
                MessageBox.Show("This UserID already exists");
                return;
            }

            
            string insertQuery =
                $"INSERT INTO dbo.Client (ClientId, Username, Password) " +
                $"VALUES ('{clientId.Replace("'", "''")}', " +
                $"'{username.Replace("'", "''")}', " +
                $"'{password.Replace("'", "''")}')";

            int result = da.ExecuteDMLQuery(insertQuery);

            if (result > 0)
            {
                MessageBox.Show("Sign Up Successful! You can now Sign In.");

                
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();

                
                Log_in form = this.FindForm() as Log_in;
                if (form != null)
                {
                    form.LoadControl(new SignIn());
                }
            }
            else
            {
                MessageBox.Show("Sign Up Failed");
            }
        }
    }
}
