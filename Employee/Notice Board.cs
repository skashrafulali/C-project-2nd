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
        private DataAccess dataAccess;
        private Employee_Dash _dash;

        public Notice_Board(Employee_Dash dash)
        {
            InitializeComponent();
            _dash = dash;
            dataAccess = new DataAccess();
            LoadNoticeData();
        }

        private void LoadNoticeData()
        {
            try
            {
                string query = @"
            SELECT 
                NoticeText,
                NoticeDate,
                CreatedBy
            FROM Notices
            ORDER BY NoticeDate DESC";

                DataTable noticeData = dataAccess.ExecuteQueryTable(query);
                dataGridView1.DataSource = noticeData;

                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView1.ReadOnly = true;
                dataGridView1.AllowUserToAddRows = false;

                // Header rename
                dataGridView1.Columns["NoticeText"].HeaderText = "Notice";
                dataGridView1.Columns["NoticeDate"].HeaderText = "Date";
                dataGridView1.Columns["CreatedBy"].HeaderText = "Posted By";
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error loading notices:\n" + ex.Message,
                    "Database Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _dash.Show();  
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void Notice_Board_Load(object sender, EventArgs e)
        {

        }
    }
}
