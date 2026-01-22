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
    public partial class Order_details : Form
    {
        private readonly DataAccess dataAccess = new DataAccess();

        public Order_details()
        {
            InitializeComponent();
            LoadOrderDetails();
        }

        private void LoadOrderDetails()
        {
            try
            {
                string query = $@"
                    SELECT
                        OrderId,
                        OrderItem      AS [Item],
                        Quality,
                        Quantity,
                        PricePerUnit   AS [Unit Price],
                        TotalPrice     AS [Total],
                        Deadline,
                        OrderDate,
                        Status,
                        Payable,
                        Payment,
                        PaymentDate
                    FROM Orders
                    WHERE UserId = '{Session.UserId}'
                    ORDER BY OrderDate DESC";

                DataTable orderData = dataAccess.ExecuteQueryTable(query);
                dataGridView1.DataSource = orderData;

                // optional: make it look nicer
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading order details: " + ex.Message, "Database Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            Client_Dash client_Dash = new Client_Dash();
            client_Dash.Show();
            this.Hide();
        }

        private void Order_details_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        
        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

            
        }
    }
}
