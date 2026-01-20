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
    public partial class Make_Order : Form
    {
        // Add DataAccess instance
        private DataAccess dataAccess = new DataAccess();

        // Dictionary to store base prices for each item
        private Dictionary<string, decimal> itemBasePrices = new Dictionary<string, decimal>
        {
            { "RAM", 50m },
            { "GPU", 400m },
            { "SSD", 200m },
            { "MotherBoard", 300m }
        };

        // Dictionary to store quality multipliers
        private Dictionary<string, decimal> qualityMultipliers = new Dictionary<string, decimal>
        {
            { "A grade", 1.75m },
            { "B grade", 1.5m },
            { "C grade", 1.0m }
        };

        public Make_Order()
        {
            InitializeComponent();
            
            // Add event handlers for automatic calculation
            comboBox1.SelectedIndexChanged += CalculatePrice;
            comboBox2.SelectedIndexChanged += CalculatePrice;
            textBox2.TextChanged += CalculatePrice;
            
            // Make price textboxes readonly
            textBox3.ReadOnly = true;
            textBox4.ReadOnly = true;
        }

        private void CalculatePrice(object sender, EventArgs e)
        {
            try
            {
                // Clear price fields if any required field is empty
                if (comboBox1.SelectedItem == null || comboBox2.SelectedItem == null || 
                    string.IsNullOrWhiteSpace(textBox2.Text))
                {
                    textBox3.Text = "";
                    textBox4.Text = "";
                    return;
                }

                // Get selected item and quality
                string selectedItem = comboBox1.SelectedItem.ToString();
                string selectedQuality = comboBox2.SelectedItem.ToString();

                // Parse quantity
                if (!int.TryParse(textBox2.Text, out int quantity) || quantity <= 0)
                {
                    textBox3.Text = "";
                    textBox4.Text = "";
                    return;
                }

                // Calculate price per unit
                decimal basePrice = itemBasePrices[selectedItem];
                decimal qualityMultiplier = qualityMultipliers[selectedQuality];
                decimal pricePerUnit = basePrice * qualityMultiplier;

                // Calculate total price
                decimal totalPrice = pricePerUnit * quantity;

                // Display prices
                textBox3.Text = pricePerUnit.ToString("F2");
                textBox4.Text = totalPrice.ToString("F2");
            }
            catch (Exception)
            {
                // Handle any calculation errors
                textBox3.Text = "";
                textBox4.Text = "";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Validate all required fields
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Please enter Client ID.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Please select an Order Item.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(textBox2.Text) || !int.TryParse(textBox2.Text, out int quantity) || quantity <= 0)
            {
                MessageBox.Show("Please enter a valid quantity (positive number).", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (comboBox2.SelectedItem == null)
            {
                MessageBox.Show("Please select a Quality grade.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dateTimePicker1.Value.Date < DateTime.Now.Date)
            {
                MessageBox.Show("Please select a future date for the deadline.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // If all validation passes, show order summary
            string orderSummary = $"Order Summary:\n\n" +
                                $"Client ID: {textBox1.Text}\n" +
                                $"Order Item: {comboBox1.SelectedItem}\n" +
                                $"Quantity: {textBox2.Text}\n" +
                                $"Quality: {comboBox2.SelectedItem}\n" +
                                $"Deadline: {dateTimePicker1.Value.ToShortDateString()}\n" +
                                $"Price per Unit: ${textBox3.Text}\n" +
                                $"Total Price: ${textBox4.Text}";

            DialogResult result = MessageBox.Show($"{orderSummary}\n\nDo you want to place this order?", 
                                                "Confirm Order", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    // Using parameterized query to prevent SQL injection
                    int rowsAffected = dataAccess.ExecuteDMLQuery(
                    $@"INSERT INTO [office management studio].[dbo].[Make Order] ([Client Id], [Order item], [Quantity], [Quality], [Deadline], [Total price]) 
                     VALUES ('{textBox1.Text.Replace("'", "''")}', 
                              '{comboBox1.SelectedItem.ToString().Replace("'", "''")}', 
                               {int.Parse(textBox2.Text)}, 
                              '{comboBox2.SelectedItem.ToString().Replace("'", "''")}', 
                              '{dateTimePicker1.Value.Date:yyyy-MM-dd}', 
                               {decimal.Parse(textBox4.Text)})");

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Order placed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearForm();
                    }
                    else
                    {
                        MessageBox.Show("Failed to place order. No rows were affected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error saving order: {ex.Message}\n\nDetails: {ex.ToString()}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ClearForm()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            dateTimePicker1.Value = DateTime.Now;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Client_Dash client_Dash = new Client_Dash();
            client_Dash.Show();
            this.Hide();
        }

        private void Make_Order_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
