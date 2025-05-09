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
    public partial class CustomerFm : Form
    {
        SqlConnection cn = new SqlConnection(@"Data Source=DESKTOP-SSC2FCL;Initial Catalog=KUFAIR;User ID=sa;Password=181244;Pooling=False");
        SqlCommand cm = new SqlCommand();
        DbConnect dbcon = new DbConnect();
        SqlDataReader dr;

        bool check = false;
        public CustomerFm()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.connection());
            lblUser.Text = Login.Employee;
            DisplayCustomer();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {


        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            
        }
     /*  public void LoadCustomer()
        {
            int i = 0;
           // dgvCus.Rows.Clear();
            cm = new SqlCommand("SELECT * FROM CustomerTbl WHERE CONCAT(CustName,CustAdd,CustPhone,CustEmail) LIKE '%" + txtSearch.Text + "%'", cn);
            cn.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvCus.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString());
            }
            dr.Close();
            cn.Close();
        }  */
        private void Clear()
        {
            txtname.Clear();
            txtAddress.Clear();
            txtphone.Clear();
            txtemail.Clear();

            btnsave.Enabled = true;
            btnEdits.Enabled = true;
        }
        private void DisplayCustomer()
        {
            cn.Open();
            string Query = "Select * from CustomerTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, cn);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            dgvCus.DataSource = ds.Tables[0];
            cn.Close();

        }

        public void CheckField()
        {
            if (txtname.Text == "" | txtAddress.Text == "" | txtphone.Text == "" | txtemail.Text == "")
            {
                MessageBox.Show("โปรดกรอกข้อมูล!", "เเจ้งเตือนจากระบบ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            check = true;
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                CheckField();
                if (check)
                {
                    if (MessageBox.Show("คุณต้องการที่จะบันทึกข้อมูล?", "แจ้งเตือนจากระบบ !!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        cm = new SqlCommand("INSERT INTO CustomerTbl (CustName,CustAdd,CustPhone,CustEmail) VALUES (@CN,@CA,@CP,@CM)", cn);
                        cm.Parameters.AddWithValue("@CN", txtname.Text);
                        cm.Parameters.AddWithValue("@CA", txtAddress.Text);
                        cm.Parameters.AddWithValue("@CP", txtphone.Text);
                        cm.Parameters.AddWithValue("@CM", txtemail.Text);

                        cn.Open();
                        cm.ExecuteNonQuery();
                        MessageBox.Show("บันถึงข้อมูลเสร็จสิ้น!", "เเจ้งเตือนจากระบบ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cn.Close();
                        DisplayCustomer();   
                        Clear();

                    }

                }

            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message);
            }
        }
        int Key = 0;
        private void dgvCus_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtname.Text = dgvCus.SelectedRows[0].Cells[1].Value.ToString();
            txtAddress.Text = dgvCus.SelectedRows[0].Cells[2].Value.ToString();
            txtphone.Text = dgvCus.SelectedRows[0].Cells[3].Value.ToString();
            txtemail.Text = dgvCus.SelectedRows[0].Cells[4].Value.ToString();
            if (txtname.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(dgvCus.SelectedRows[0].Cells[0].Value.ToString());
            } 
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("โปรดเลือกข้อมูลที่ต้องการจะลบ !!!", "แจ้งเตือนจากระบบ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("delete from CustomerTbl where CustId = @CKey", cn);
                    cmd.Parameters.AddWithValue("@CKey", Key);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("ลบข้อมูลเรียบร้อย!!!", "แจ้งเตือนจากระบบ", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    cn.Close();
                    DisplayCustomer();
                    Clear();
                   // LoadCustomer();
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

        private void btnEp_Click(object sender, EventArgs e)
        {
            Employees em = new Employees();
            em.Show();
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
            
            
            
        }

        private void lblUser_Click(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("ต้องการปิดโปรแกรม", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnHm_Click_1(object sender, EventArgs e)
        {
            Home Ho = new Home();
            Ho.Show();
            this.Hide();
        }

        private void btnpd_Click_1(object sender, EventArgs e)
        {
            Products pd = new Products();
            pd.Show();
            this.Hide();
        }

        private void btnCs_Click(object sender, EventArgs e)
        {

        }

        private void btnEp_Click_1(object sender, EventArgs e)
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

        private void btnBilling_Click_1(object sender, EventArgs e)
        {
            BillKufair bb = new BillKufair();
            bb.Show();
            this.Hide();
        }

        private void btnLogout_Click_1(object sender, EventArgs e)
        {

            if (MessageBox.Show("ต้องการออกจากระบบหรือไม่", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Login log = new Login();
                log.Show();
                this.Hide();
                MessageBox.Show("ออกจากระบบเสร็จสิ้น", "แจ้งเตือนจากระบบ", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }



        private void btnSearch_Click(object sender, EventArgs e)
        {
            cm = new SqlCommand("Select * from CustomerTbl where CustName LIKE @CN+ '%'", cn);
            cm.Parameters.AddWithValue("CN", txtSearch.Text);
            SqlDataAdapter sda = new SqlDataAdapter();
            sda.SelectCommand = cm;
            var dt = new DataTable();
            dt.Clear();
            sda.Fill(dt);
            dgvCus.DataSource = dt;
        }

        private void btnEdits_Click(object sender, EventArgs e)
        {
            try
            {
                CheckField();
                if (check)
                {
                    if (MessageBox.Show("คุณต้องการจะแก้ไขข้อมูล ?", "แจ้งเตือนจากระบบ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {

                        cm = new SqlCommand("UPDATE CustomerTbl SET CustName=@CN, CustAdd=@CA, CustPhone=@CP ,CustEmail=@CM WHERE Custid=@CID", cn);
                        cm.Parameters.AddWithValue("@CN", txtname.Text);
                        cm.Parameters.AddWithValue("@CA", txtAddress.Text);
                        cm.Parameters.AddWithValue("@CP", txtphone.Text);
                        cm.Parameters.AddWithValue("@CM", txtemail.Text);
                        cm.Parameters.AddWithValue("@CID", Key);

                        cn.Open();
                        cm.ExecuteNonQuery();
                        MessageBox.Show("อัพเดทข้อมูลเรียบร้อย", "เเจ้งเตือนจากระบบ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cn.Close();
                        DisplayCustomer();
                        Clear();
                    }

                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }
    }

}

