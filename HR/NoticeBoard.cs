using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C__project.HR
{
    public partial class NoticeBoard : UserControl
    {
        public NoticeBoard()
        {
            InitializeComponent();
        }
        // created by nafis
        
        private void button1_MouseEnter(object sender, EventArgs e)
        {
            button1.BackColor = Color.Tomato;
            button1.ForeColor = Color.White;
            pictureBox2.BackColor = Color.Tomato;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.BackColor = Color.FromArgb(95, 168, 211);
            pictureBox2.BackColor = Color.FromArgb(95, 168, 211);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hr_Dash form = this.FindForm() as Hr_Dash;
            if (form != null)
            {
                form.LoadControl(new welcome());
            }


        }
       


        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button2_MouseEnter(object sender, EventArgs e)
        {
            button2.BackColor = Color.MediumSpringGreen;
            button2.ForeColor = Color.White;
            pictureBox1.BackColor = Color.MediumSpringGreen;
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            button2.BackColor = Color.FromArgb(95, 168, 211); ;
            pictureBox1.BackColor = Color.FromArgb(95, 168, 211);
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void NoticeBoard_Load(object sender, EventArgs e)
        {

        }
    }
    
}
