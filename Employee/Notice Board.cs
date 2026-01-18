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
    public partial class Notice_Board : Form
    {
        public Notice_Board()
        {
            InitializeComponent();
        }

        private void bClear_Click(object sender, EventArgs e)
        {
            rtbViewNotice.Text = "";    
        }
    }
}
