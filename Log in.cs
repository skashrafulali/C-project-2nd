using C__project.HR;
using C__project.LogIn;
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

        private void Log_in_Load(object sender, EventArgs e)
        {
            LoadControl(new SignIn());
        }

        public void LoadControl(UserControl uc)
        {
            panel2.Controls.Clear();
            uc.Dock = DockStyle.Fill;
            panel2.Controls.Add(uc);
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

        

        private void label6_Click(object sender, EventArgs e)
        {
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
           
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

        private void Log_in_Load_1(object sender, EventArgs e)
        {
            LoadControl(new SignIn());
        }
    }
}
