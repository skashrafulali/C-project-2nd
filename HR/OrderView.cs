using C__project.LogIn;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C__project.HR
{
    public partial class OrderView : UserControl
    {
        public OrderView()
        {
            InitializeComponent();
            this.Load += OrderView_Load;
        }

        private void OrderView_Load(object sender, EventArgs e)
        {
            LoadOrders();
        }

        private void LoadOrders()
        {
            DataAccess da = new DataAccess();

            string query = @"
        SELECT
            OrderId,
            UserId,
            OrderItem,
            Quality,
            Quantity,
            PricePerUnit,
            TotalPrice,
            Deadline,
            OrderDate,
            Status
        FROM Orders
        ORDER BY OrderDate DESC";

            dataGridView1.DataSource = da.ExecuteQueryTable(query);
        }

        

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_MouseEnter(object sender, EventArgs e)
        {
            button2.BackColor = Color.Tomato;
            button2.ForeColor = Color.White;
            pictureBox1.BackColor = Color.Tomato;
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            button2.BackColor = Color.FromArgb(95, 168, 211);
            pictureBox1.BackColor = Color.FromArgb(95, 168, 211);
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            Hr_Dash form = this.FindForm() as Hr_Dash;
            if (form != null)
            {
                form.LoadControl(new welcome());
            }
        }
    }
    
}
