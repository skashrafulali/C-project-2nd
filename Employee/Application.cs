using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C__project.Employee
{
    public partial class Application : Form
    {
        private Employee_Dash _dash;
        public Application(Employee_Dash dash)
        {
            InitializeComponent();
            _dash = dash;
        }

        private void LoadEmployeeName()
        {
            DataAccess da = new DataAccess();

            string query = $@"
        SELECT FullName 
        FROM Users 
        WHERE UserId = '{Session.UserId.Replace("'", "''")}'";

            DataTable dt = da.ExecuteQueryTable(query);

            if (dt.Rows.Count == 1)
            {
                textBox2.Text = dt.Rows[0]["FullName"].ToString();
                textBox2.ReadOnly = true;
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(richTextBox1.Text))
            {
                MessageBox.Show("Please write your application.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query = @"
        INSERT INTO EmployeeApplication
        (UserId, ApplicationText, ApplicationDate)
        VALUES
        (@UserId, @Text, @Date)";

            try
            {
                using (SqlConnection con = new SqlConnection(
                    ConfigurationManager.ConnectionStrings["OfficeDB"].ConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@UserId", Session.UserId);
                    cmd.Parameters.AddWithValue("@Text", richTextBox1.Text.Trim());
                    cmd.Parameters.AddWithValue("@Date", dateTimePicker1.Value.Date);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Application submitted successfully ✅");
                richTextBox1.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void ClearForm()
        {
            textBox1.Clear();
            textBox2.Clear();
            richTextBox1.Clear();
            dateTimePicker1.Value = DateTime.Now;
            textBox1.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _dash.Show();   
            this.Close();   
        }

        private void Application_Load(object sender, EventArgs e)
        {
            textBox1.Text = Session.UserId;
            textBox1.ReadOnly = true;

            // 🔹 Auto Full Name from Users table
            LoadEmployeeName();
        }
    }
}
