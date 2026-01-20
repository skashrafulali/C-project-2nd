using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C__project.Client
{
    public partial class Billings : Form
    {
        private DataAccess dataAccess = new DataAccess();

        public Billings()
        {
            InitializeComponent();
            LoadOrderDetails();
        }

        private void LoadOrderDetails()
        {
            try
            {
                string query = @"SELECT 
                                    mo.[Client Id],
                                    mo.[Order item], 
                                    mo.[Total price],
                                    ISNULL(b.[Payable], mo.[Total price]) AS [Payable]
                                FROM [office management studio].[dbo].[Make Order] mo
                                LEFT JOIN [office management studio].[dbo].[Billings] b 
                                ON mo.[Client Id] = b.[Client Id]";
                
                DataTable orderData = dataAccess.ExecuteQueryTable(query);
                dataGridView1.DataSource = orderData;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading order details: {ex.Message}", "Database Error", 
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Search and Update Payment
            try
            {
                // Validate Client ID input
                if (string.IsNullOrWhiteSpace(textBox1.Text))
                {
                    MessageBox.Show("Please enter Client ID.", "Validation Error", 
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Validate Payment input
                if (string.IsNullOrWhiteSpace(textBox2.Text) || 
                    !decimal.TryParse(textBox2.Text, out decimal paymentAmount) || 
                    paymentAmount <= 0)
                {
                    MessageBox.Show("Please enter a valid payment amount (positive number).", 
                                  "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string clientId = textBox1.Text;

                // First, get the total price from the Make Order table
                string getTotalPriceQuery = @"SELECT [Total price] 
                                            FROM [office management studio].[dbo].[Make Order] 
                                            WHERE [Client Id] = '" + clientId.Replace("'", "''") + "'";

                DataTable orderData = dataAccess.ExecuteQueryTable(getTotalPriceQuery);

                if (orderData.Rows.Count == 0)
                {
                    MessageBox.Show("No order found for this Client ID.", "Not Found", 
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                decimal totalPrice = Convert.ToDecimal(orderData.Rows[0]["Total price"]);

                // Check if billing record exists for this client
                string checkBillingQuery = @"SELECT [Payment], [Payable] 
                                           FROM [office management studio].[dbo].[Billings] 
                                           WHERE [Client Id] = '" + clientId.Replace("'", "''") + "'";

                DataTable billingData = dataAccess.ExecuteQueryTable(checkBillingQuery);

                decimal currentPayment = 0;
                decimal currentPayable = totalPrice;

                if (billingData.Rows.Count > 0)
                {
                    // Update existing billing record
                    currentPayment = Convert.ToDecimal(billingData.Rows[0]["Payment"]);
                    decimal newPayment = currentPayment + paymentAmount;
                    decimal newPayable = totalPrice - newPayment;

                    // Ensure payable doesn't go negative
                    if (newPayable < 0)
                    {
                        MessageBox.Show($"Payment amount exceeds remaining balance. Maximum payable amount is ${totalPrice - currentPayment:F2}", 
                                      "Payment Exceeds Balance", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    string updateQuery = $@"UPDATE [office management studio].[dbo].[Billings] 
                                          SET [Payment] = {newPayment}, [Payable] = {newPayable} 
                                          WHERE [Client Id] = '{clientId.Replace("'", "''")}'";

                    int rowsAffected = dataAccess.ExecuteDMLQuery(updateQuery);

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show($"Payment updated successfully!\nTotal Payment: ${newPayment:F2}\nRemaining Payable: ${newPayable:F2}", 
                                      "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    // Create new billing record
                    decimal newPayable = totalPrice - paymentAmount;

                    // Ensure payable doesn't go negative
                    if (newPayable < 0)
                    {
                        MessageBox.Show($"Payment amount exceeds total order amount. Maximum payable amount is ${totalPrice:F2}", 
                                      "Payment Exceeds Total", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    string insertQuery = $@"INSERT INTO [office management studio].[dbo].[Billings] 
                                          ([Client Id], [Payment], [Payable]) 
                                          VALUES ('{clientId.Replace("'", "''")}', {paymentAmount}, {newPayable})";

                    int rowsAffected = dataAccess.ExecuteDMLQuery(insertQuery);

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show($"Payment recorded successfully!\nTotal Payment: ${paymentAmount:F2}\nRemaining Payable: ${newPayable:F2}", 
                                      "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                // Clear input fields and refresh data
                textBox1.Text = "";
                textBox2.Text = "";
                LoadOrderDetails();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error processing payment: {ex.Message}", "Database Error", 
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Client_Dash client_Dash = new Client_Dash();
            client_Dash.Show();
            this.Hide();
        }

        private void Billings_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
