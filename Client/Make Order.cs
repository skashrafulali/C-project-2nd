using System;
using System.Collections.Generic;
using System.Drawing;
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

        private void button3_Click(object sender, EventArgs e)
        {
            // Validate that all required fields are filled before printing
            if (!ValidateFormForPrint())
            {
                return;
            }

            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
        }

        private bool ValidateFormForPrint()
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Please enter Client ID before printing.", "Print Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Please select an Order Item before printing.", "Print Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(textBox2.Text) || !int.TryParse(textBox2.Text, out int quantity) || quantity <= 0)
            {
                MessageBox.Show("Please enter a valid quantity before printing.", "Print Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (comboBox2.SelectedItem == null)
            {
                MessageBox.Show("Please select a Quality grade before printing.", "Print Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(textBox3.Text) || string.IsNullOrWhiteSpace(textBox4.Text))
            {
                MessageBox.Show("Price calculation is incomplete. Please check all fields.", "Print Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            try
            {
                Graphics graphics = e.Graphics;
                Font titleFont = new Font("Arial", 18, FontStyle.Bold);
                Font headerFont = new Font("Arial", 14, FontStyle.Bold);
                Font normalFont = new Font("Arial", 12, FontStyle.Regular);
                Font smallFont = new Font("Arial", 10, FontStyle.Regular);

                float yPos = 50;
                float leftMargin = 100;
                float rightMargin = 700;

                // Company Header
                graphics.DrawString("OFFICE MANAGEMENT STUDIO", titleFont, Brushes.Black, leftMargin, yPos);
                yPos += 40;
                graphics.DrawString("Order Bill Slip", headerFont, Brushes.Black, leftMargin, yPos);
                yPos += 30;

                // Draw line separator
                graphics.DrawLine(new Pen(Color.Black, 2), leftMargin, yPos, rightMargin, yPos);
                yPos += 30;

                // Bill Information
                graphics.DrawString("Bill Date: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), normalFont, Brushes.Black, leftMargin, yPos);
                yPos += 25;
                graphics.DrawString("Client ID: " + textBox1.Text, normalFont, Brushes.Black, leftMargin, yPos);
                yPos += 25;
                graphics.DrawString("Order Date: " + DateTime.Now.ToShortDateString(), normalFont, Brushes.Black, leftMargin, yPos);
                yPos += 25;
                graphics.DrawString("Delivery Deadline: " + dateTimePicker1.Value.ToShortDateString(), normalFont, Brushes.Black, leftMargin, yPos);
                yPos += 40;

                // Product Details Header
                graphics.DrawString("PRODUCT DETAILS", headerFont, Brushes.Black, leftMargin, yPos);
                yPos += 30;

                // Table headers
                graphics.DrawString("Item", normalFont, Brushes.Black, leftMargin, yPos);
                graphics.DrawString("Quality", normalFont, Brushes.Black, leftMargin + 150, yPos);
                graphics.DrawString("Quantity", normalFont, Brushes.Black, leftMargin + 250, yPos);
                graphics.DrawString("Unit Price", normalFont, Brushes.Black, leftMargin + 350, yPos);
                graphics.DrawString("Total Price", normalFont, Brushes.Black, leftMargin + 450, yPos);
                yPos += 25;

                // Draw line under headers
                graphics.DrawLine(new Pen(Color.Black, 1), leftMargin, yPos, rightMargin - 50, yPos);
                yPos += 20;

                // Product data
                graphics.DrawString(comboBox1.SelectedItem.ToString(), normalFont, Brushes.Black, leftMargin, yPos);
                graphics.DrawString(comboBox2.SelectedItem.ToString(), normalFont, Brushes.Black, leftMargin + 150, yPos);
                graphics.DrawString(textBox2.Text, normalFont, Brushes.Black, leftMargin + 250, yPos);
                graphics.DrawString("$" + textBox3.Text, normalFont, Brushes.Black, leftMargin + 350, yPos);
                graphics.DrawString("$" + textBox4.Text, normalFont, Brushes.Black, leftMargin + 450, yPos);
                yPos += 30;

                // Draw line under product data
                graphics.DrawLine(new Pen(Color.Black, 1), leftMargin, yPos, rightMargin - 50, yPos);
                yPos += 30;

                // Price Summary
                graphics.DrawString("PRICE SUMMARY", headerFont, Brushes.Black, leftMargin, yPos);
                yPos += 30;

                graphics.DrawString("Subtotal:", normalFont, Brushes.Black, leftMargin + 300, yPos);
                graphics.DrawString("$" + textBox4.Text, normalFont, Brushes.Black, leftMargin + 450, yPos);
                yPos += 25;

                // Calculate tax (assuming 10% tax)
                decimal subtotal = decimal.Parse(textBox4.Text);
                decimal tax = subtotal * 0.10m;
                decimal finalTotal = subtotal + tax;

                graphics.DrawString("Tax (10%):", normalFont, Brushes.Black, leftMargin + 300, yPos);
                graphics.DrawString("$" + tax.ToString("F2"), normalFont, Brushes.Black, leftMargin + 450, yPos);
                yPos += 25;

                // Draw line before total
                graphics.DrawLine(new Pen(Color.Black, 2), leftMargin + 300, yPos, rightMargin - 50, yPos);
                yPos += 15;

                graphics.DrawString("TOTAL AMOUNT:", headerFont, Brushes.Black, leftMargin + 300, yPos);
                graphics.DrawString("$" + finalTotal.ToString("F2"), headerFont, Brushes.Black, leftMargin + 450, yPos);
                yPos += 50;

                // Footer
                graphics.DrawString("Thank you for your business!", normalFont, Brushes.Black, leftMargin, yPos);
                yPos += 20;
                graphics.DrawString("For any queries, please contact our support team.", smallFont, Brushes.Gray, leftMargin, yPos);

                // Cleanup fonts
                titleFont.Dispose();
                headerFont.Dispose();
                normalFont.Dispose();
                smallFont.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during printing: {ex.Message}", "Print Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
