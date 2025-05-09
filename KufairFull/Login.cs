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
        SqlConnection Con = new SqlConnection(@"Data Source=DESKTOP-SSC2FCL;Initial Catalog=KUFAIR;User ID=sa;Password=181244");
        SqlCommand cm = new SqlCommand();
        DbConnect dbcon = new DbConnect();
        SqlDataReader dr;
        string title = "KU FAIR";
        public static string Employee;
        public Login()
        {
            InitializeComponent();
            Con = new SqlConnection(dbcon.connection());
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtname.Text == "" || txtpass.Text == "")
            {
                MessageBox.Show("กรอกข้อมูลให้ครบ","แจ้งเตือนจากระบบ",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlDataAdapter sda = new SqlDataAdapter("Select count(*) from EmployeeTbl where EmpName='" + txtname.Text + "' and EmpPass='" + txtpass.Text + "'", Con);
                    DataTable d = new DataTable();
                    sda.Fill(d);
                    if (d.Rows[0][0].ToString() == "1")
                    {
                        Employee = txtname.Text;
                        MessageBox.Show("ยินดีต้อนรับเข้าสู่ระบบ", "แจ้งเตือนจากระบบ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Home main = new Home();
                        main.Show();
                        this.Hide();
                    }else
                    {
                        MessageBox.Show("โปรดตรวจสอบชื่อผู้ใช้หรือรหัสผ่านของคุณ","แจ้งเตือนจากระบบ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    Con.Close();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
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
