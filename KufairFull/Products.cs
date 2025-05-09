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
    public partial class Products : Form
    {
        public Products()
        {
            InitializeComponent();
            lblUser.Text = Login.Employee;
            DisplayProduct();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=DESKTOP-SSC2FCL;Initial Catalog=KUFAIR;User ID=sa;Password=181244");
        private void DisplayProduct()
        {
            Con.Open();
            string Query = "Select * from tbProduct";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ProductDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void Clear()
        {
            Pname.Text = "";
            pqty.Text = "";
            pprice.Text = "";
            cbpd.SelectedIndex = 0;
        }
        int Key = 0;
        private void ProductDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Pname.Text = ProductDGV.SelectedRows[0].Cells[1].Value.ToString();
            cbpd.Text = ProductDGV.SelectedRows[0].Cells[2].Value.ToString();
            pqty.Text = ProductDGV.SelectedRows[0].Cells[3].Value.ToString();
            pprice.Text = ProductDGV.SelectedRows[0].Cells[4].Value.ToString();
            if (Pname.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(ProductDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void btnLogin_Click(object sender, EventArgs e) //btnSave
        {
            if (Pname.Text == "" || cbpd.SelectedIndex == -1 || pprice.Text == "" || pqty.Text == "")
            {
                MessageBox.Show("โปรดกรอกข้อมูลให้ครบ","ข้อความจากระบบ",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into tbProduct (pname,pcategory,pqty,pprice) values(@PN,@PC,@PQ,@PP)", Con);
                    cmd.Parameters.AddWithValue("@PN", Pname.Text);
                    cmd.Parameters.AddWithValue("@PC", cbpd.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@PQ", pprice.Text);
                    cmd.Parameters.AddWithValue("@PP", pqty.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("เพิ่มสินค้าเสร็จสิ้น","ข้อความจากระบบ",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    Con.Close();
                    DisplayProduct();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        } 

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (Pname.Text == "" || cbpd.SelectedIndex == -1 || pprice.Text == "" || pqty.Text == "")
            {
                MessageBox.Show("โปรดกรอกข้อมูลให้ครบ", "ข้อความจากระบบ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Update tbProduct set pname=@PN,pcategory=@PC,pqty=@PQ,pprice=@PP where pid=@PKey", Con);
                    cmd.Parameters.AddWithValue("@PN", Pname.Text);
                    cmd.Parameters.AddWithValue("@PC", cbpd.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@PP", pprice.Text);
                    cmd.Parameters.AddWithValue("@PQ", pqty.Text);
                    cmd.Parameters.AddWithValue("@PKey", Key);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("อัพเดทสินค้าเรียบร้อย", "ข้อความจากระบบ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Con.Close();
                    DisplayProduct();
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

        private void btnCs_Click(object sender, EventArgs e)
        {
            CustomerFm cf = new CustomerFm();
            cf.Show();
            this.Hide();
        }

        private void btnEp_Click(object sender, EventArgs e)
        {
            if (lblUser.Text == "Admin")
            {
                Employees ep = new Employees();
                ep.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("คุณไม่มีสิทธิ์เข้าถึง", "แจ้งเตือนจากระบบ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
