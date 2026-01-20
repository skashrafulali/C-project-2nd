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

        public Notice_Board()
        {
            InitializeComponent();
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
            // Create and show the Employee Dashboard form
            Employee_Dash employeeDashForm = new Employee_Dash();
            employeeDashForm.Show();
            
            // Close the current Notice Board form
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Optional: Handle cell clicks if you want to implement functionality
            // like viewing full notice details when a cell is clicked
        }
    }
}
