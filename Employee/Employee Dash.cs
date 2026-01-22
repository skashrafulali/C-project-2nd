using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C__project
{
    public partial class Employee_Dash : Form
    {

        public Employee_Dash()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Employee.Application app = new Employee.Application(this);
            app.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
                "Are you sure you want to logout?",
                "Confirm Logout",
                MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                Log_in loginForm = Application.OpenForms
                    .OfType<Log_in>()
                    .FirstOrDefault();

                if (loginForm != null)
                {
                    loginForm.Show();
                    loginForm.ShowSignIn();
                }

                this.Close();
                Session.Clear();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Notice_Board notice = new Notice_Board(this);
            notice.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Employee.Update_Profile updateProfileForm =
                new Employee.Update_Profile(this);

            updateProfileForm.Show();
            this.Hide();

        }

       
    }
}
