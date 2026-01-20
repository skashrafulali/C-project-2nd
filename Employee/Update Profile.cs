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

        public Update_Profile()
        {
            InitializeComponent();
            dataAccess = new DataAccess();
            
            // Add event handler to load employee data when Employee ID is entered
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
                string empId = textBox1.Text.Trim();

                if (string.IsNullOrWhiteSpace(empId))
                {
                    MessageBox.Show("Please enter Employee ID.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string query = $@"SELECT Name, Address, DateOfBirth 
                                FROM Employee 
                                WHERE EmpId = '{empId.Replace("'", "''")}'";

                DataTable dt = dataAccess.ExecuteQueryTable(query);

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    
                    // Populate the form fields with existing data
                    textBox2.Text = row["Name"].ToString();
                    textBox3.Text = row["Address"].ToString();
                    
                    // Handle DateOfBirth conversion
                    if (DateTime.TryParse(row["DateOfBirth"].ToString(), out DateTime dob))
                    {
                        dateTimePicker1.Value = dob;
                    }
                }
                else
                {
                    MessageBox.Show("Employee ID not found. Please check the Employee ID.", 
                                  "Employee Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ClearForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading employee data: {ex.Message}", 
                              "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate input fields
                if (string.IsNullOrWhiteSpace(textBox1.Text))
                {
                    MessageBox.Show("Please enter Employee ID.", "Validation Error", 
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox1.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(textBox2.Text))
                {
                    MessageBox.Show("Please enter Employee Name.", "Validation Error", 
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox2.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(textBox3.Text))
                {
                    MessageBox.Show("Please enter Employee Address.", "Validation Error", 
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox3.Focus();
                    return;
                }

                // Prepare data for update
                string empId = textBox1.Text.Trim();
                string name = textBox2.Text.Trim();
                string address = textBox3.Text.Trim();
                string dateOfBirth = dateTimePicker1.Value.ToString("yyyy-MM-dd");

                // First check if employee exists
                string checkQuery = $@"SELECT COUNT(*) FROM Employee 
                                     WHERE EmpId = '{empId.Replace("'", "''")}'";

                DataTable checkDt = dataAccess.ExecuteQueryTable(checkQuery);
                int employeeCount = Convert.ToInt32(checkDt.Rows[0][0]);

                if (employeeCount == 0)
                {
                    MessageBox.Show("Employee ID not found. Cannot update non-existing employee.", 
                                  "Employee Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Perform the update
                string updateQuery = $@"UPDATE Employee 
                                      SET Name = '{name.Replace("'", "''")}', 
                                          Address = '{address.Replace("'", "''")}', 
                                          DateOfBirth = '{dateOfBirth}'
                                      WHERE EmpId = '{empId.Replace("'", "''")}'";

                int rowsAffected = dataAccess.ExecuteDMLQuery(updateQuery);

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Employee profile updated successfully!", 
                                  "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    // Optionally clear the form after successful update
                    ClearForm();
                }
                else
                {
                    MessageBox.Show("Failed to update employee profile. Please try again.", 
                                  "Update Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while updating employee profile: {ex.Message}", 
                              "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            Employee_Dash employeeDash = new Employee_Dash();
            employeeDash.Show();
            this.Hide();
        }

        private void Update_Profile_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }
    }
}
