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

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate input fields
                if (string.IsNullOrWhiteSpace(textBox1.Text))
                {
                    MessageBox.Show("Please enter Employee ID.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox1.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(textBox2.Text))
                {
                    MessageBox.Show("Please enter Employee Name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox2.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(richTextBox1.Text))
                {
                    MessageBox.Show("Please enter Application Text.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    richTextBox1.Focus();
                    return;
                }

                // Parse Employee ID
                if (!int.TryParse(textBox1.Text.Trim(), out int empId))
                {
                    MessageBox.Show("Employee ID must be a valid number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox1.Focus();
                    return;
                }

                // Get connection string from App.config
                string connectionString = ConfigurationManager.ConnectionStrings["OfficeDB"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string insertQuery = @"INSERT INTO Application (EmpId, EmpName, AppliTXT, AppDate) 
                                         VALUES (@EmpId, @EmpName, @AppliTXT, @AppDate)";

                    using (SqlCommand command = new SqlCommand(insertQuery, connection))
                    {
                        // Add parameters to prevent SQL injection
                        command.Parameters.AddWithValue("@EmpId", empId);
                        command.Parameters.AddWithValue("@EmpName", textBox2.Text.Trim());
                        command.Parameters.AddWithValue("@AppliTXT", richTextBox1.Text.Trim());
                        command.Parameters.AddWithValue("@AppDate", dateTimePicker1.Value.Date);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Application submitted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            
                            // Clear the form after successful submission
                            ClearForm();
                        }
                        else
                        {
                            MessageBox.Show("Failed to submit application. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show($"Database error: {sqlEx.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
    }
}
