using C__project;
using C__project.HR;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace C__project
{
    public partial class Hr_Dash : Form
    {
        
        

        // guard to prevent re-entrancy when logging out
        private bool _logoutInProgress;
        private void LoadControlInPanel(UserControl control)
        {
            panel1.Controls.Clear();        // Clear old content
            control.Dock = DockStyle.Fill;  // Fill the panel
            panel1.Controls.Add(control);   // Add the control
            control.BringToFront();         // Make sure it's visible
        }

        



        public Hr_Dash()
        {
            InitializeComponent();

            // Unsubscribe first to avoid duplicate hdfksdfcs
            this.button1.Click -= this.button1_Click;
            this.button1.Click += this.button1_Click;

            this.button3.Click -= this.button3_Click;
            this.button3.Click += this.button3_Click;

            this.button4.Click -= this.button4_Click;
            this.button4.Click += this.button4_Click;

            this.button5.Click -= this.button5_Click;
            this.button5.Click += this.button5_Click;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadControlInPanel(new regEmp());



        }

        private void button3_Click(object sender, EventArgs e)
        {
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {

        }

       

    

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            button1.BackColor = Color.Crimson;
            button1.ForeColor = Color.White;

        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.BackColor = Color.FromArgb(21, 27, 46); ;
            pictureBox1.BackColor = Color.Transparent;
        }

        private void button2_MouseEnter(object sender, EventArgs e)
        {
            button2.BackColor = Color.Crimson;
            button2.ForeColor = Color.White;
        
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            button2.BackColor = Color.FromArgb(21, 27, 46); ;
            pictureBox2.BackColor = Color.Transparent;
        }

        private void button3_MouseLeave(object sender, EventArgs e)
        {
            button3.BackColor = Color.FromArgb(21, 27, 46); ;
            pictureBox3.BackColor = Color.Transparent;
        }

        private void button3_MouseEnter(object sender, EventArgs e)
        {
            button3.BackColor = Color.Crimson;
            button3.ForeColor = Color.White;

        }

        private void button4_MouseEnter(object sender, EventArgs e)
        {
            button4.BackColor = Color.Crimson;
            button4.ForeColor = Color.White;
        }

        private void button4_MouseLeave(object sender, EventArgs e)
        {
            button4.BackColor = Color.FromArgb(21, 27, 46); ;
            pictureBox4.BackColor = Color.Transparent;
        }

        private void button5_MouseEnter(object sender, EventArgs e)
        {
            button5.BackColor = Color.Crimson;
            button5.ForeColor = Color.White;
        }

        private void button5_MouseLeave(object sender, EventArgs e)
        {
            button5.BackColor = Color.FromArgb(21, 27, 46); ;
            pictureBox5.BackColor = Color.Transparent;
        }

        private void button6_MouseEnter(object sender, EventArgs e)
        {
            button6.BackColor = Color.Crimson;
            button6.ForeColor = Color.White;
        }

     

        private void button6_MouseLeave(object sender, EventArgs e)
        {
            button6.BackColor = Color.FromArgb(21, 27, 46); ;
            pictureBox6.BackColor = Color.Transparent;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Hr_Dash_Load(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            // Prevent re-entrancy / multiple message boxes
            if (_logoutInProgress)
                return;

            _logoutInProgress = true;
            this.button5.Enabled = false;

            try
            {
                var confirm = MessageBox.Show(
                    "Are you sure you want to log out?",
                    "Confirm",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2);

                if (confirm == DialogResult.Yes)
                {
                    // Try to find an existing Log_in form (reuse) to avoid creating multiples
                    var existingLogin = Application.OpenForms.OfType<Log_in>().FirstOrDefault();
                    if (existingLogin != null)
                    {
                        existingLogin.Show();
                        existingLogin.BringToFront();
                    }
                    else
                    {
                        var login = new Log_in();
                        login.Show();
                    }

                    // Close this dashboard (this will dispose it)
                    this.Close();
                }
            }
            finally
            {
                if (!this.IsDisposed)
                {
                    this.button5.Enabled = true;
                    _logoutInProgress = false;
                }
            }

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            LoadControlInPanel(new OrderView());

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            LoadControlInPanel(new SalaryBonus());
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            LoadControlInPanel(new NoticeBoard());
            

        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {

        }
    }
}
