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

        }
    }
}
