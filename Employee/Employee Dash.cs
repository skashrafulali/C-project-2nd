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
    public partial class Employee_Dash : Form
    {
        public Employee_Dash()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Create and show the Application form
            Employee.Application applicationForm = new Employee.Application();
            applicationForm.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Show confirmation dialog before logout
            DialogResult result = MessageBox.Show(
                "Are you sure you want to logout and return to login?", 
                "Confirm Logout", 
                MessageBoxButtons.YesNo, 
                MessageBoxIcon.Question);
            
            if (result == DialogResult.Yes)
            {
                // Try to find an existing Log_in form (reuse) to avoid creating multiples
                var existingLogin = Application.OpenForms.OfType<Log_in>().FirstOrDefault();
                if (existingLogin != null)
                {
                    existingLogin.LoadControl(new LogIn.SignIn());
                    existingLogin.Show();
                    existingLogin.BringToFront();
                }
                else
                {
                    var loginForm = new Log_in();
                    loginForm.Show();
                }
                
                // Close the current Employee Dashboard form
                this.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Create and show the Notice Board form
            Notice_Board noticeBoardForm = new Notice_Board();
            noticeBoardForm.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Create and show the Update Profile form
            Employee.Update_Profile updateProfileForm = new Employee.Update_Profile();
            updateProfileForm.Show();
        }

        private void Employee_Dash_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
