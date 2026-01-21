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
                string query = @"SELECT TOP (1000) [Description], [NoticeDate] 
                               FROM [office management studio].[dbo].[Notice]";

                DataTable noticeData = dataAccess.ExecuteQueryTable(query);
                dataGridView1.DataSource = noticeData;

                // Optional: Format the DataGridView for better appearance
                if (dataGridView1.Columns.Count > 0)
                {
                    dataGridView1.Columns["Description"].HeaderText = "Notice Description";
                    dataGridView1.Columns["NoticeDate"].HeaderText = "Notice Date";
                    
                    // Auto resize columns to fit content
                    dataGridView1.AutoResizeColumns();
                    
                    // Make the Description column wider if needed
                    if (dataGridView1.Columns["Description"] != null)
                    {
                        dataGridView1.Columns["Description"].Width = 400;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading notice data: {ex.Message}", "Database Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
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
    }
}
