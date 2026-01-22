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
    public partial class EmpDetails : UserControl

        
    {


        private void SearchEmployee(string keyword)
        {
            string query = $@"
SELECT 
    u.UserId        AS [Employee ID],
    u.FullName      AS [Name],
    u.Address,
    u.DateOfBirth   AS [Date Of Birth],
    u.CreatedDate   AS [Joined Date],
    sb.Salary,
    sb.Bonus,
    sb.Deduction,
    sb.TotalSalary
FROM Users u
LEFT JOIN SalaryBonus sb
    ON u.UserId = sb.UserId
WHERE u.IsEmployee = 1
AND (
    u.UserId LIKE '%{keyword.Replace("'", "''")}%'
    OR u.FullName LIKE '%{keyword.Replace("'", "''")}%'
)";
            dataGridView1.DataSource = da.ExecuteQueryTable(query);
        }

        private DataAccess da = new DataAccess();
        public EmpDetails()
        {
            InitializeComponent();
            LoadEmployeeData();
        }
        private void LoadEmployeeData()
        {
            string query = @"
SELECT 
    u.UserId        AS [Employee ID],
    u.FullName      AS [Name],
    u.Address,
    u.DateOfBirth   AS [Date Of Birth],
    u.CreatedDate   AS [Joined Date],
    sb.Salary,
    sb.Bonus,
    sb.Deduction,
    sb.TotalSalary
FROM Users u
LEFT JOIN SalaryBonus sb
    ON u.UserId = sb.UserId
WHERE u.IsEmployee = 1";

            dataGridView1.DataSource = da.ExecuteQueryTable(query);

            


        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hr_Dash form = this.FindForm() as Hr_Dash;
            if (form != null)
            {
                form.LoadControl(new welcome());
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void EmpDetails_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
           
        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string keyword = textBox1.Text.Trim();

    if (keyword == "")
        LoadEmployeeData();
    else
        SearchEmployee(keyword);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
