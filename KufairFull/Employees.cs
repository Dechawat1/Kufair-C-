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

namespace KufairFull
{
    public partial class Employees : Form
    {
        DbConnect dbcon = new DbConnect();

        public Employees()
        {

            InitializeComponent();
            lblUser.Text = Login.Employee;

            DisplayEmployess();
        }
        
void DisplayEmployess()
        {
            try
            {
                using (SqlConnection con = dbcon.GetConnection())
                {
                    con.Open();
                    string query = "SELECT * FROM EmployeeTbl";
                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    EmployeeDGV.DataSource = dt;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }
        private void Clear()
        {
            EmpName.Text = "";
            EmpAdd.Text = "";
            EmpPhone.Text = "";
            Password.Text = "";
        }
        int key = 0;

        private void Save_Click(object sender, EventArgs e)
        {
            if (EmpName.Text == "" || EmpAdd.Text == "" || EmpPhone.Text == "" || Password.Text == "")
            {
                MessageBox.Show("โปรดกรอกข้อมูลให้ครบ", "เเจ้งเตือนจากระบบ", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }else
            {
                try
                {
                    using (SqlConnection con = dbcon.GetConnection())
                    {
                        con.Open();
                        string insertQuery = "insert into EmployeeTbl (EmpName,EmpAdd,EmpDOB,EmpPhone,EmpPass) values(@EN,@EA,@ED,@EP,@EPa";
                        using (SqlCommand cmd = new SqlCommand(insertQuery, con))
                        {
                            cmd.Parameters.AddWithValue("@EN", EmpName.Text);
                            cmd.Parameters.AddWithValue("@EA", EmpAdd.Text);
                            cmd.Parameters.AddWithValue("@ED", EmpDOB.Value.Date);
                            cmd.Parameters.AddWithValue("@EP", EmpPhone.Text);
                            cmd.Parameters.AddWithValue("@EPa", Password.Text);
                            cmd.ExecuteNonQuery();

                        }
                        MessageBox.Show("เพิ่มข้อมูลเรียบร้อย", "เเจ้งเตือนจากระบบ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        DisplayEmployess();
                        Clear();

                    }                    
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
                
            }
        }

        
       private void EmployeeDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
            EmpName.Text = EmployeeDGV.SelectedRows[0].Cells[1].Value.ToString();
            EmpAdd.Text = EmployeeDGV.SelectedRows[0].Cells[2].Value.ToString();
            EmpDOB.Text = EmployeeDGV.SelectedRows[0].Cells[3].Value.ToString();
            EmpPhone.Text = EmployeeDGV.SelectedRows[0].Cells[4].Value.ToString();
            Password.Text = EmployeeDGV.SelectedRows[0].Cells[5].Value.ToString();
            if(EmpName.Text == "")
            {
                key = 0;
            } else
            {
                key = Convert.ToInt32(EmployeeDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
            
        }

        private void Employees_Load(object sender, EventArgs e)
        {
            
        }

        private void Edit_Click(object sender, EventArgs e)
        {
            if (EmpName.Text == "" || EmpPhone.Text == "" || Password.Text == "" || EmpAdd.Text == "")
            {
                MessageBox.Show("โปรดใส่ข้อมูลให้ครบ!!!", "แจ้งเตือนจากระบบ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    using (SqlConnection Con = dbcon.GetConnection())
                    {
                        Con.Open();
                        string editQuery = "Update EmployeeTbl set EmpName=@EN,EmpAdd=@EA,EmpDOB=@ED,EmpPhone=@EP,EmpPass=@EPa where EmpNum=@EKey";
                        using (SqlCommand editcmd = new SqlCommand(editQuery, Con))
                        {
                            editcmd.Parameters.AddWithValue("@EN", EmpName.Text);
                            editcmd.Parameters.AddWithValue("@EA", EmpAdd.Text);
                            editcmd.Parameters.AddWithValue("@ED", EmpDOB.Value.Date);
                            editcmd.Parameters.AddWithValue("@EP", EmpPhone.Text);
                            editcmd.Parameters.AddWithValue("@EPa", Password.Text);
                            editcmd.Parameters.AddWithValue("@EKey", key);
                            editcmd.ExecuteNonQuery();
                        }
                        MessageBox.Show("อัพเดทข้อมูลเรียบร้อย!!!", "แจ้งเตือนจากระบบ", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                        DisplayEmployess();
                        Clear();
                    }
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("โปรดเลือกข้อมูลที่ต้องการจะลบ !!!", "แจ้งเตือนจากระบบ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    using (SqlConnection Con = dbcon.GetConnection())
                    {
                        Con.Open();
                        string deleteQuery = "Delete from EmployeeTbl where EmpNum=@EKey";
                        using (SqlCommand cmd = new SqlCommand(deleteQuery, Con))
                        {
                            cmd.Parameters.AddWithValue("@EKey", key);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    MessageBox.Show("ลบข้อมูลเรียบร้อย!!!", "แจ้งเตือนจากระบบ", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    DisplayEmployess();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void btnHm_Click(object sender, EventArgs e)
        {
            Home Ho = new Home();
            Ho.Show();
            this.Hide();
        }

        private void btnpd_Click(object sender, EventArgs e)
        {
            Products pd = new Products();
            pd.Show();
            this.Hide();
        }

        private void btnCs_Click(object sender, EventArgs e)
        {
            CustomerFm cf = new CustomerFm();
            cf.Show();
            this.Hide();
        }

        private void btnBilling_Click(object sender, EventArgs e)
        {
            BillKufair bb = new BillKufair();
            bb.Show();
            this.Hide();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("ต้องการออกจากระบบหรือไม่", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Login log = new Login();
                log.Show();
                this.Hide();
                MessageBox.Show("ออกจากระบบเสร็จสิ้น", "แจ้งเตือนจากระบบ", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("ต้องการปิดโปรแกรม", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}
