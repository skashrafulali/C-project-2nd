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
        }

        private void LoadEmployeeCombo()
        {
            DataAccess da = new DataAccess();

            string query = "SELECT EmpId, Name FROM Employee";
            DataTable dt = da.ExecuteQueryTable(query);

            // ComboBox1 → EmpId
            comboBox1.DataSource = dt;
            comboBox1.DisplayMember = "EmpId";
            comboBox1.ValueMember = "EmpId";

            // ComboBox2 → Name
            comboBox2.DataSource = dt.Copy();   // same data, different control
            comboBox2.DisplayMember = "Name";
            comboBox2.ValueMember = "EmpId";    // hidden EmpId
        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedValue != null)
            {
                comboBox1.SelectedValue = comboBox2.SelectedValue;
            }
        }

        private void LoadSalaryGrid()
        {
            DataAccess da = new DataAccess();
            string query = "SELECT * FROM SalaryBonus";
            dataGridView1.DataSource = da.ExecuteQueryTable(query);
        }


        private void ClearSalaryForm()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            comboBox1.SelectedIndex = -1;
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
            if (comboBox1.SelectedValue == null)
            {
                MessageBox.Show("Please select employee");
                return;
            }

            string empId = comboBox1.SelectedValue.ToString();
            string basic = textBox1.Text;
            string bonus = textBox2.Text;
            string deduction = textBox3.Text;
            string total = textBox4.Text;

            DataAccess da = new DataAccess();

            // 🔹 STEP 1: Check employee already has salary or not
            string checkQuery =
                $"SELECT * FROM SalaryBonus WHERE [Emp Id] = '{empId}'";

            DataTable dt = da.ExecuteQueryTable(checkQuery);

            string query;

            // 🔹 STEP 2: Decide INSERT or UPDATE
            if (dt.Rows.Count > 0)
            {
                // UPDATE (already exists)
                query = $@"
        UPDATE SalaryBonus
        SET
            Salary = '{basic}',
            Bonus = '{bonus}',
            Deduction = '{deduction}',
            TotalSalary = '{total}'
        WHERE [Emp Id] = '{empId}'";
            }
            else
            {
                // INSERT (first time)
                query = $@"
        INSERT INTO SalaryBonus
        ([Emp Id], Salary, Bonus, Deduction, TotalSalary)
        VALUES
        (
            '{empId}',
            '{basic}',
            '{bonus}',
            '{deduction}',
            '{total}'
        )";
            }

            int row = da.ExecuteDMLQuery(query);

            if (row > 0)
            {
                MessageBox.Show("Salary saved / updated successfully");
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
    MessageBox.Show("Total Salary Calculated");
        }
    }
}
