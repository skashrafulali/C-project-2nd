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
    public partial class Application_Form : Form
    {
        public Application_Form()
        {
            InitializeComponent();
        }

        private void bBack_Click(object sender, EventArgs e)
        {
            Employee_Dash ed = new Employee_Dash();
            ed.Show();
            this.Hide();
        }
    }
}
