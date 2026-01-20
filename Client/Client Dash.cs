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
    public partial class Client_Dash : Form
    {
        public Client_Dash()
        {
            InitializeComponent();
        }
       /* private void LoadControlInPanel(UserControl control)
        {
            panel1.Controls.Clear();        // Clear old content
            control.Dock = DockStyle.Fill;  // Fill the panel
            panel1.Controls.Add(control);   // Add the control
            control.BringToFront();         // Make sure it's visible
        }
       */


        private void button1_Click(object sender, EventArgs e)
        {
            Client.Make_Order makeOrderForm = new Client.Make_Order();
            makeOrderForm.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Client.Order_details orderDetailsForm = new Client.Order_details();
            orderDetailsForm.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Client.Billings billingsForm = new Client.Billings();
            billingsForm.Show();
            this.Hide();
        }

        private void Client_Dash_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Log_in log_In = new Log_in();
            log_In.Show();
            this.Hide();
        }
    }
}
