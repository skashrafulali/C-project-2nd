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

            string userId = textBox3.Text.Trim();
            string fullName = textBox1.Text.Trim();
            string password = textBox2.Text.Trim();

            if (userId == "" || fullName == "" || password == "")
            {
                MessageBox.Show("All fields are required");
                return;
            }

            DataAccess da = new DataAccess();

            // 1️⃣ check user exists
            string checkQuery = $@"
        SELECT * FROM Users 
        WHERE UserId = '{userId.Replace("'", "''")}'";

            DataTable dt = da.ExecuteQueryTable(checkQuery);

            if (dt.Rows.Count > 0)
            {
                MessageBox.Show("UserId already exists");
                return;
            }

            // 2️⃣ insert into Users table
            string insertQuery = $@"
        INSERT INTO Users
        (UserId, Password, FullName, CreatedBy, IsEmployee, CreatedDate)
        VALUES
        (
            '{userId.Replace("'", "''")}',
            '{password.Replace("'", "''")}',
            '{fullName.Replace("'", "''")}',
            'SELF',
            0,
            GETDATE()
        )";

            int row = da.ExecuteDMLQuery(insertQuery);

            if (row > 0)
            {
                MessageBox.Show("Sign Up successful!");

                Log_in form = this.FindForm() as Log_in;
                if (form != null)
                {
                    form.LoadControl(new SignIn());
                }
            }


        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
