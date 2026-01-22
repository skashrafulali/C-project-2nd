using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C__project.Employee
{
    public partial class Update_Profile : Form
    {
        private DataAccess dataAccess;
        private Employee_Dash _dash;

        public Update_Profile(Employee_Dash dash)
        {
            InitializeComponent();
            _dash = dash;
            dataAccess = new DataAccess();
            textBox1.Leave += LoadEmployeeData;
        }

        private void LoadEmployeeData(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox1.Text))
            {
                LoadEmployeeDetails();
            }
        }

        private void LoadEmployeeDetails()
        {
            try
            {
                string userId = Session.UserId;

                string query = $@"
        SELECT FullName, Address, DateOfBirth
        FROM Users
        WHERE UserId = '{userId.Replace("'", "''")}'";

                DataTable dt = dataAccess.ExecuteQueryTable(query);

                if (dt.Rows.Count == 1)
                {
                    DataRow row = dt.Rows[0];

                    textBox2.Text = row["FullName"].ToString();
                    textBox3.Text = row["Address"].ToString();

                    if (row["DateOfBirth"] != DBNull.Value)
                    {
                        dateTimePicker1.Value = Convert.ToDateTime(row["DateOfBirth"]);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading profile: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string userId = Session.UserId;
                string name = textBox2.Text.Trim();
                string address = textBox3.Text.Trim();
                string dob = dateTimePicker1.Value.ToString("yyyy-MM-dd");

                if (string.IsNullOrWhiteSpace(name))
                {
                    MessageBox.Show("Name required");
                    return;
                }

                string updateQuery = $@"
        UPDATE Users
        SET 
            FullName = '{name.Replace("'", "''")}',
            Address = '{address.Replace("'", "''")}',
            DateOfBirth = '{dob}'
        WHERE UserId = '{userId.Replace("'", "''")}'";

                int row = dataAccess.ExecuteDMLQuery(updateQuery);

                if (row > 0)
                {
                    MessageBox.Show("Profile updated successfully");
                }
                else
                {
                    MessageBox.Show("Update failed");
                }
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
            textBox3.Clear();
            dateTimePicker1.Value = DateTime.Now;
            textBox1.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _dash.Show();  
            this.Close();
        }

        private void Update_Profile_Load(object sender, EventArgs e)
        {
            textBox1.Text = Session.UserId;   // UserId auto
            textBox1.ReadOnly = true;

            LoadEmployeeDetails();
        }
    }
}
