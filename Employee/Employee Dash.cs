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

        private void bNotice_Click(object sender, EventArgs e)
        {
            Notice_Board nb = new Notice_Board();
            nb.Show();
            this.Hide();
        }


private void bUpdateProfile_Click(object sender, EventArgs e)
        {
         Update_Profile up = new Update_Profile();
         up.Show();
         this.Hide();    
        }

        private void Employee_Dash_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void bApplication_Click(object sender, EventArgs e)
        {
            Application_Form ap = new Application_Form();
            ap.Show();
            this.Hide();
        }
    }
}
