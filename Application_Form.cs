using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C__project
{
    public partial class Application_Form : Form
    {
        public Application_Form()
        {
            InitializeComponent();
        }

        private void bBack_Click(object sender, EventArgs e)
        {
            Employee_Dash ed = new Employee_Dash();
            ed.Show();
            this.Hide();
        }

        private void bSubmit_Click(object sender, EventArgs e)
        {
            string id = txtId.Text;
            string name = txtName.Text;
            DateTime date = dtpDate.Value;
            string applicationText = rtbAppBox.Text;

            if(id==""||name==""||applicationText=="")
            {
                MessageBox.Show("Please fill all the fields.");
                return;
            }
            else
            {
                try
                { 
                    var connection = new SqlConnection();
                    connection.ConnectionString = @"Data Source=LAPTOP-HGRTIL1F\SQLEXPRESS01 ;Initial Catalog=UpdateProfile ;Integrated Security=True ;Encrypt=False";
                    connection.Open();

                    var cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandText = $"INSERT INTO Application (User_Id,Name,Date,Application) Values ('{id}','{name}','{date}','{applicationText}')";
                    cmd.ExecuteNonQuery();

                    connection.Close();
                    MessageBox.Show("Application Submitted Successfully.");
                    txtId.Text = "";
                    txtName.Text = "";
                    rtbAppBox.Text = "";
                    dtpDate.Value = DateTime.Now;
                    txtId.Focus();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }
    }
}
