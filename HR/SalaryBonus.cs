using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace C__project.HR
{
    public partial class SalaryBonus : UserControl
    {
        public SalaryBonus()
        {
            InitializeComponent();

            this.Load += SalaryBonus_Load;
        }


        private void SalaryBonus_Load(object sender, EventArgs e)
        {
            LoadEmployeeCombo();
            LoadSalaryGrid();
        }

        private void LoadEmployeeCombo()
        {
            DataAccess da = new DataAccess();

            string query = @"
        SELECT UserId, FullName 
        FROM Users 
        WHERE IsEmployee = 1";

            DataTable dt = da.ExecuteQueryTable(query);

            comboBox2.DataSource = dt;
            comboBox2.DisplayMember = "UserId";   // show Employee ID
            comboBox2.ValueMember = "UserId";

            comboBox2.SelectedIndex = -1;
        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedItem == null) return;

            DataRowView drv = comboBox2.SelectedItem as DataRowView;

           
        }

        private void LoadSalaryGrid()
        {
            DataAccess da = new DataAccess();

            string query = @"
        SELECT 
            sb.UserId,
            u.FullName,
            sb.Salary,
            sb.Bonus,
            sb.Deduction,
            sb.TotalSalary
        FROM SalaryBonus sb
        INNER JOIN Users u ON sb.UserId = u.UserId";

            dataGridView1.DataSource = da.ExecuteQueryTable(query);

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }


        private void ClearSalaryForm()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            comboBox2.SelectedIndex = -1;
            
        }


        private void CalculateTotalSalary()
        {
            double basic = 0;
            double bonus = 0;
            double deduction = 0;

            double.TryParse(textBox1.Text, out basic);
            double.TryParse(textBox2.Text, out bonus);
            double.TryParse(textBox3.Text, out deduction);

            double total = basic + bonus - deduction;
            textBox4.Text = total.ToString();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            CalculateTotalSalary();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            CalculateTotalSalary();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            CalculateTotalSalary();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Hr_Dash form = this.FindForm() as Hr_Dash;
            if (form != null)
            {
                form.LoadControl(new welcome());
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!decimal.TryParse(textBox1.Text, out decimal basic) ||
     !decimal.TryParse(textBox2.Text, out decimal bonus) ||
     !decimal.TryParse(textBox3.Text, out decimal deduction))
            {
                MessageBox.Show("Please enter valid numeric values");
                return;
            }

            decimal total = basic + bonus - deduction;
            textBox4.Text = total.ToString();

            string userId = comboBox2.SelectedValue.ToString();

            DataAccess da = new DataAccess();

            string query = $@"
UPDATE SalaryBonus
SET
    Salary = {basic},
    Bonus = {bonus},
    Deduction = {deduction},
    TotalSalary = {total}
WHERE UserId = '{userId.Replace("'", "''")}'";

            int row = da.ExecuteDMLQuery(query);

            if (row > 0)
            {
                MessageBox.Show("Salary updated successfully");
                LoadSalaryGrid();
                ClearSalaryForm();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ClearSalaryForm();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            double basic = 0;
            double bonus = 0;
            double deduction = 0;

            double.TryParse(textBox1.Text, out basic);
            double.TryParse(textBox2.Text, out bonus);
            double.TryParse(textBox3.Text, out deduction);

            double total = basic + bonus - deduction;

            textBox4.Text = total.ToString();

            MessageBox.Show("Total Salary Calculated");
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            CalculateTotalSalary();
    
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click_2(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

       

        private void SalaryBonus_Load_1(object sender, EventArgs e)
        {

        }
    }
}
