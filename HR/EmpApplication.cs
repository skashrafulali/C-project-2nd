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
    public partial class EmpApplication : UserControl
    {
        public EmpApplication()
        {
            InitializeComponent();
            this.Load += EmpApplication_Load;
        }
        private void EmpApplication_Load(object sender, EventArgs e)
        {
            LoadApplicationData();
        }
        private void LoadApplicationData()
        {
            DataAccess da = new DataAccess();

            string query = @"
        SELECT
            ea.ApplicationId,
            ea.UserId      AS [Employee ID],
            u.FullName     AS [Employee Name],
            ea.ApplicationText AS [Application],
            ea.ApplicationDate AS [Date],
            ea.Status
        FROM EmployeeApplication ea
        INNER JOIN Users u
            ON ea.UserId = u.UserId
        WHERE u.IsEmployee = 1
        ORDER BY ea.ApplicationDate DESC";

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

        private void EmpApplication_Load_1(object sender, EventArgs e)
        {

        }
    }
}
