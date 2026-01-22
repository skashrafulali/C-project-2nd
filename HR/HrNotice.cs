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
    public partial class HrNotice : UserControl
    {
        public HrNotice()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hr_Dash form = this.FindForm() as Hr_Dash;
            if (form != null)
            {
                form.LoadControl(new welcome());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string noticeText = richTextBox1.Text.Trim();
            DateTime noticeDate = dateTimePicker1.Value;

            if (string.IsNullOrEmpty(noticeText))
            {
                MessageBox.Show("Please write a notice");
                return;
            }

            DataAccess da = new DataAccess();

            string query = $@"
        INSERT INTO Notices (NoticeText, NoticeDate, CreatedBy)
        VALUES
        (
            '{noticeText.Replace("'", "''")}',
            '{noticeDate:yyyy-MM-dd}',
            '{Session.UserId}'
        )";

            int row = da.ExecuteDMLQuery(query);

            if (row > 0)
            {
                MessageBox.Show("Notice posted successfully");
                richTextBox1.Clear();
                dateTimePicker1.Value = DateTime.Now;
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void HrNotice_Load(object sender, EventArgs e)
        {

        }
    }
}
