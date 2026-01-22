using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using static System.Collections.Specialized.BitVector32;

namespace C__project.Client
{
    public partial class Make_Order : Form
    {

        //abcd
        
        private DataAccess dataAccess = new DataAccess();

        
        private Dictionary<string, decimal> itemBasePrices = new Dictionary<string, decimal>
        {
            { "RAM", 50m },
            { "GPU", 400m },
            { "SSD", 200m },
            { "MotherBoard", 300m }
        };

        
        private Dictionary<string, decimal> qualityMultipliers = new Dictionary<string, decimal>
        {
            { "A grade", 1.75m },
            { "B grade", 1.5m },
            { "C grade", 1.0m }
        };

        public Make_Order()
        {
            InitializeComponent();

            
            comboBox1.SelectedIndexChanged += CalculatePrice;
            comboBox2.SelectedIndexChanged += CalculatePrice;
            textBox2.TextChanged += CalculatePrice;

            
            textBox3.ReadOnly = true;
            textBox4.ReadOnly = true;
        }

        private void CalculatePrice(object sender, EventArgs e)
        {
            try
            {
                
                if (comboBox1.SelectedItem == null || comboBox2.SelectedItem == null ||
                    string.IsNullOrWhiteSpace(textBox2.Text))
                {
                    textBox3.Text = "";
                    textBox4.Text = "";
                    return;
                }

                
                string selectedItem = comboBox1.SelectedItem.ToString();
                string selectedQuality = comboBox2.SelectedItem.ToString();

                
                if (!int.TryParse(textBox2.Text, out int quantity) || quantity <= 0)
                {
                    textBox3.Text = "";
                    textBox4.Text = "";
                    return;
                }

                
                decimal basePrice = itemBasePrices[selectedItem];
                decimal qualityMultiplier = qualityMultipliers[selectedQuality];
                decimal pricePerUnit = basePrice * qualityMultiplier;

                
                decimal totalPrice = pricePerUnit * quantity;

                
                textBox3.Text = pricePerUnit.ToString("F2");
                textBox4.Text = totalPrice.ToString("F2");
            }
            catch (Exception)
            {
                
                textBox3.Text = "";
                textBox4.Text = "";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("Please add at least one item");
                return;
            }

            string userId = Session.UserId;
            DateTime deadline = dateTimePicker1.Value;

            decimal grandTotal = 0;
            decimal lastPricePerUnit = 0;

            DataAccess da = new DataAccess();

            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.IsNewRow) continue;

                    string item = row.Cells[0].Value.ToString();
                    string quality = row.Cells[1].Value.ToString();
                    int quantity = Convert.ToInt32(row.Cells[2].Value);
                    decimal pricePerUnit = Convert.ToDecimal(row.Cells[3].Value);
                    decimal totalPrice = Convert.ToDecimal(row.Cells[4].Value);

                    // 🔹 calculate totals
                    grandTotal += totalPrice;
                    lastPricePerUnit = pricePerUnit;

                    // 🔹 insert each item
                    string insertQuery = $@"
INSERT INTO OfficeManagement.dbo.Orders
(
    UserId,
    OrderItem,
    Quality,
    Quantity,
    PricePerUnit,
    TotalPrice,
    Deadline,
    OrderDate,
    Status,
    Payable,
    Payment
)
VALUES
(
    '{userId}',
    '{item.Replace("'", "''")}',
    '{quality.Replace("'", "''")}',
    {quantity},
    {pricePerUnit},
    {totalPrice},
    '{deadline:yyyy-MM-dd}',
    GETDATE(),
    'Pending',
    {totalPrice},  -- Payable starts as total
    0              -- Payment starts 0
)";


                    da.ExecuteDMLQuery(insertQuery);
                }

                // 🟢 2. Show totals in textbox
                textBox3.Text = lastPricePerUnit.ToString("F2");
                textBox4.Text = grandTotal.ToString("F2");

                MessageBox.Show("Order placed successfully");

                // 🧹 clear after order
                dataGridView1.Rows.Clear();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error placing order:\n" + ex.Message);
            }
        }

        private void ClearForm()
        {
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            textBox2.Clear(); // quantity
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
        Graphics g = e.Graphics;

        Font titleFont = new Font("Arial", 18, FontStyle.Bold);
        Font headerFont = new Font("Arial", 14, FontStyle.Bold);
        Font normalFont = new Font("Arial", 12, FontStyle.Regular);

        float y = 50;
        float left = 80;
        float right = 750;

        // ✅ Company header
        g.DrawString("Blah Blah company ltd", titleFont, Brushes.Black, left, y);
        y += 35;
        g.DrawString("Invoice", headerFont, Brushes.Black, left, y);
        y += 25;

        g.DrawLine(Pens.Black, left, y, right, y);
        y += 20;

        // ✅ Info
        g.DrawString("Bill Date: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), normalFont, Brushes.Black, left, y);
        y += 20;
        g.DrawString("Client ID: " + Session.UserId, normalFont, Brushes.Black, left, y);
        y += 20;
        g.DrawString("Delivery Deadline: " + dateTimePicker1.Value.ToShortDateString(), normalFont, Brushes.Black, left, y);
        y += 30;

        // ✅ Table header
        g.DrawString("Item", normalFont, Brushes.Black, left, y);
        g.DrawString("Quality", normalFont, Brushes.Black, left + 170, y);
        g.DrawString("Qty", normalFont, Brushes.Black, left + 320, y);
        g.DrawString("Unit (Tk)", normalFont, Brushes.Black, left + 400, y);
        g.DrawString("Total (Tk)", normalFont, Brushes.Black, left + 540, y);
        y += 18;

        g.DrawLine(Pens.Black, left, y, right, y);
        y += 10;

        // ✅ Print all rows from DataGridView
        decimal grandTotal = 0;

        foreach (DataGridViewRow row in dataGridView1.Rows)
        {
            if (row.IsNewRow) continue;

            string item = row.Cells["OrderItem"].Value?.ToString() ?? "";
            string quality = row.Cells["Quality"].Value?.ToString() ?? "";
            string qty = row.Cells["Quantity"].Value?.ToString() ?? "0";

            decimal unit = Convert.ToDecimal(row.Cells["PricePerUnit"].Value);
            decimal total = Convert.ToDecimal(row.Cells["TotalPrice"].Value);

            grandTotal += total;

            g.DrawString(item, normalFont, Brushes.Black, left, y);
            g.DrawString(quality, normalFont, Brushes.Black, left + 170, y);
            g.DrawString(qty, normalFont, Brushes.Black, left + 320, y);
            g.DrawString("Tk " + unit.ToString("F2"), normalFont, Brushes.Black, left + 400, y);
            g.DrawString("Tk " + total.ToString("F2"), normalFont, Brushes.Black, left + 540, y);

            y += 20;

            // Simple page overflow protection
            if (y > e.MarginBounds.Bottom - 120)
            {
                e.HasMorePages = true;
                return;
            }
        }

        y += 10;
        g.DrawLine(Pens.Black, left, y, right, y);
        y += 20;

        // ✅ Summary (no tax unless you want)
        g.DrawString("Grand Total:", headerFont, Brushes.Black, left + 350, y);
        g.DrawString("Tk " + grandTotal.ToString("F2"), headerFont, Brushes.Black, left + 540, y);
        y += 40;

        g.DrawString("Thank you for your business!", normalFont, Brushes.Black, left, y);

        titleFont.Dispose();
        headerFont.Dispose();
        normalFont.Dispose();
    }
    catch (Exception ex)
    {
        MessageBox.Show("Error during printing: " + ex.Message, "Print Error",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}


        private void Make_Order_Load(object sender, EventArgs e)
        {
            textBox1.Text = Session.UserId;
            textBox1.ReadOnly = true;

            // Clear old columns (important)
            dataGridView1.Columns.Clear();

            // Add columns
            dataGridView1.Columns.Add("OrderItem", "Order Item");
            dataGridView1.Columns.Add("Quality", "Quality");
            dataGridView1.Columns.Add("Quantity", "Quantity");
            dataGridView1.Columns.Add("PricePerUnit", "Price Per Unit");
            dataGridView1.Columns.Add("TotalPrice", "Total Price");

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AllowUserToAddRows = false;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null || comboBox2.SelectedItem == null)
            {
                MessageBox.Show("Select item and quality");
                return;
            }

            if (!int.TryParse(textBox2.Text, out int quantity) || quantity <= 0)
            {
                MessageBox.Show("Invalid quantity");
                return;
            }

            string item = comboBox1.SelectedItem.ToString();
            string quality = comboBox2.SelectedItem.ToString();
            decimal pricePerUnit = decimal.Parse(textBox3.Text);
            decimal totalPrice = decimal.Parse(textBox4.Text);

            dataGridView1.Rows.Add(
                item,
                quality,
                quantity,
                pricePerUnit,
                totalPrice
            );

            // clear inputs for next item
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
