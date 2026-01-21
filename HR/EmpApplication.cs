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
        EmpId,
        EmpName,
        AppliTXT,
        AppDate
    FROM Application
    ORDER BY AppDate DESC";

            dataGridView1.DataSource = da.ExecuteQueryTable(query);

           
            dataGridView1.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Hr_Dash form = this.FindForm() as Hr_Dash;
            if (form != null)
            {
                form.LoadControl(new welcome());
            }
        }
    }
}
