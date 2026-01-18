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
    public partial class Update_Profile : Form
    {
        public Update_Profile()
        {
            InitializeComponent();
        }

        private void Update_Profile_Load(object sender, EventArgs e)
        {
            this.LoadData();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void LoadData()
        {
            try
            {
                var connection = new SqlConnection();
                connection.ConnectionString = @"Data Source=LAPTOP-HGRTIL1F\SQLEXPRESS01 ;Initial Catalog=UpdateProfile ;Integrated Security=True ;Encrypt=False";
                connection.Open();

                var cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = "select * from Profile";

                DataSet ds = new DataSet();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);

                DataTable dt = ds.Tables[0];

                dgvData.AutoGenerateColumns = false;
                dgvData.DataSource = dt;
                dgvData.Refresh();
                dgvData.ClearSelection();
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void bBack_Click(object sender, EventArgs e)
        {
            Employee_Dash ed = new Employee_Dash();
            ed.Show();
            this.Hide();
        }

        private void bRefresh_Click(object sender, EventArgs e)
        {
            this.LoadData();
        }

        private void dgvData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtId.Text = dgvData.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtName.Text = dgvData.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtPass.Text = dgvData.Rows[e.RowIndex].Cells[2].Value.ToString();
                rtbAddress.Text = dgvData.Rows[e.RowIndex].Cells[3].Value.ToString();
                dtpDOB.Value = Convert.ToDateTime(dgvData.Rows[e.RowIndex].Cells[4].Value);
                txtJobPost.Text = dgvData.Rows[e.RowIndex].Cells[5].Value.ToString();
            }
        }

        private void bUpdate_Click(object sender, EventArgs e)
        {
            string id = txtId.Text;
            string name = txtName.Text;
            string address = rtbAddress.Text;
            DateTime dob = dtpDOB.Value;

            if (name == "" || address == "")
            {
                MessageBox.Show("Fill up Fields");
                return;
            }
            else
            {
                string query = "";
                if(name != "" && address != "")
                {
                    query = $"update Profile set Name='{name}', Address='{address}', DOB='{dob}' where User_Id='{id}'";
                }
                try
                {
                    var connection = new SqlConnection();
                    connection.ConnectionString = @"Data Source=LAPTOP-HGRTIL1F\SQLEXPRESS01 ;Initial Catalog=UpdateProfile ;Integrated Security=True ;Encrypt=False";
                    connection.Open();

                    var cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandText = query;
                    cmd.ExecuteNonQuery();

                    connection.Close();
                    MessageBox.Show("Profile Updated Successfully");
                    this.LoadData();


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void bClear_Click(object sender, EventArgs e)
        {
            txtId.Text = "";
            txtName.Text = "";
            txtPass.Text = "";
            rtbAddress.Text = "";
            dtpDOB.Value = DateTime.Now;
            txtJobPost.Text = "";
            dgvData.ClearSelection();
        }
    }
}
