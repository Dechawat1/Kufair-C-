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
    public partial class Login : Form
    {
        
        DbConnect dbcon = new DbConnect();
        public static string Employee;
        public Login()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtname.Text.Trim() == "" || txtpass.Text.Trim() == "")
            {
                MessageBox.Show("กรอกข้อมูลให้ครบ", "แจ้งเตือนจากระบบ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (SqlConnection con = dbcon.GetConnection())
            {
                try
                {
                    con.Open();
                    string query = "SELECT COUNT(*) FROM EmployeeTbl WHERE EmpName = @name AND EmpPass = @pass";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@name", txtname.Text.Trim());
                        cmd.Parameters.AddWithValue("@pass", txtpass.Text.Trim());

                        int count = (int)cmd.ExecuteScalar();

                        if (count == 1)
                        {
                            Employee = txtname.Text.Trim();
                            MessageBox.Show("ยินดีต้อนรับเข้าสู่ระบบ", "แจ้งเตือนจากระบบ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Home main = new Home();
                            main.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("โปรดตรวจสอบชื่อผู้ใช้หรือรหัสผ่านของคุณ", "แจ้งเตือนจากระบบ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("เกิดข้อผิดพลาด: " + ex.Message);
                }
            }
        }





        private void btnClose_Click(object sender, EventArgs e)
        {

        }

        private void btnForget_Click(object sender, EventArgs e)
        {
            MessageBox.Show("กรุณาติดต่อฝ่ายเทคนิค!", "FORGET PASSWORD", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtpass_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("ต้องการปิดโปรแกรม", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}
