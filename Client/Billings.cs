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
                string query = @"
            SELECT
                OrderId,
                UserId,
                OrderItem,
                Quality,
                Quantity,
                TotalPrice,
                ISNULL(Payable, TotalPrice) AS Payable,
                ISNULL(Payment, 0) AS Payment,
                PaymentDate,
                Status,
                OrderDate,
                Deadline
            FROM OfficeManagement.dbo.Orders
            ORDER BY OrderId DESC";

                DataTable dt = dataAccess.ExecuteQueryTable(query);
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading orders: " + ex.Message);
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // OrderId must be number
                if (!int.TryParse(textBox1.Text.Trim(), out int orderId))
                {
                    MessageBox.Show("Please enter a valid Order ID (number).");
                    return;
                }

                // Payment must be > 0
                if (!decimal.TryParse(textBox2.Text.Trim(), out decimal payNow) || payNow <= 0)
                {
                    MessageBox.Show("Please enter a valid payment amount.");
                    return;
                }

                // Get TotalPrice and current Payment
                string getQuery = $@"
            SELECT TotalPrice, ISNULL(Payment,0) AS Payment
            FROM OfficeManagement.dbo.Orders
            WHERE OrderId = {orderId}";

                DataTable dt = dataAccess.ExecuteQueryTable(getQuery);

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("No order found with this Order ID.");
                    return;
                }

                decimal totalPrice = Convert.ToDecimal(dt.Rows[0]["TotalPrice"]);
                decimal oldPayment = Convert.ToDecimal(dt.Rows[0]["Payment"]);

                decimal newPayment = oldPayment + payNow;
                decimal newPayable = totalPrice - newPayment;

                // Prevent overpayment
                if (newPayable < 0)
                {
                    decimal maxPay = totalPrice - oldPayment;
                    MessageBox.Show($"Payment exceeds remaining balance. Max you can pay: {maxPay:F2}");
                    return;
                }

                // Update Orders
                string updateQuery = $@"
            UPDATE OfficeManagement.dbo.Orders
            SET
                Payment = {newPayment},
                Payable = {newPayable},
                PaymentDate = GETDATE(),
                Status = CASE WHEN {newPayable} = 0 THEN 'Paid' ELSE Status END
            WHERE OrderId = {orderId}";

                int rows = dataAccess.ExecuteDMLQuery(updateQuery);

                if (rows > 0)
                    MessageBox.Show($"Payment updated!\nTotal Paid: {newPayment:F2}\nRemaining Payable: {newPayable:F2}");
                else
                    MessageBox.Show("Update failed.");

                textBox1.Clear();
                textBox2.Clear();
                LoadOrderDetails();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error processing payment: " + ex.Message);
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells["OrderId"].Value.ToString();
            }
        }

    }
}
