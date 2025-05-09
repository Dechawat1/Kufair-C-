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
        
        DbConnect dbcon = new DbConnect();
        SqlDataReader dr;

        bool check = false;
        public CustomerFm()
        {
            InitializeComponent();
            using(SqlConnection con = dbcon.GetConnection())
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
            try
            {
                using (SqlConnection con = dbcon.GetConnection())
                {
                    string query = "SELECT * FROM CustomerTbl";
                    SqlDataAdapter sda = new SqlDataAdapter(query, con);
                    var ds = new DataSet();
                    sda.Fill(ds);
                    dgvCus.DataSource = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
            CheckField();
            if (!check)
                return;

            if (MessageBox.Show("คุณต้องการที่จะบันทึกข้อมูล?", "แจ้งเตือนจากระบบ !!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            using (SqlConnection con = dbcon.GetConnection())
            {
                try
                {
                    con.Open();

                    string insertQuery = "INSERT INTO CustomerTbl (CustName, CustAdd, CustPhone, CustEmail) VALUES(@CN, @CA, @CP, @CM)";
                    using (SqlCommand insertCmd = new SqlCommand(insertQuery, con))
                    {
                        insertCmd.Parameters.AddWithValue("@CN", txtname.Text);
                        insertCmd.Parameters.AddWithValue("@CA", txtAddress.Text);
                        insertCmd.Parameters.AddWithValue("@CP", txtphone.Text);
                        insertCmd.Parameters.AddWithValue("@CM", txtemail.Text);
                        insertCmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("บันทึกข้อมูลเสร็จสิ้น!", "แจ้งเตือนจากระบบ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DisplayCustomer();
                    Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("เกิดข้อผิดพลาด: " + ex.Message);
                }
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
                return;
            }

            if (MessageBox.Show("คุณแน่ใจหรือไม่ว่าต้องการลบข้อมูลนี้?", "ยืนยันการลบ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection con = dbcon.GetConnection())
                    {
                        con.Open();
                        string deleteQuery = "DELETE FROM CustomerTbl WHERE CustId = @CKey";
                        using (SqlCommand cmd = new SqlCommand(deleteQuery, con))
                        {
                            cmd.Parameters.AddWithValue("@CKey", Key);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("ลบข้อมูลเรียบร้อยแล้ว!", "แจ้งเตือนจากระบบ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DisplayCustomer();
                    Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("เกิดข้อผิดพลาด: " + ex.Message, "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            try
            {
                using (SqlConnection cn = dbcon.GetConnection())
                {
                    cn.Open();
                    string searchQuery = "SELECT * FROM CustomerTbl WHERE CustName LIKE @CN";
                    using (SqlCommand cmd = new SqlCommand(searchQuery, cn))
                    {
                        // เพิ่ม wildcard '%' ในค่าที่ส่งเข้า parameter
                        cmd.Parameters.AddWithValue("@CN", txtSearch.Text + "%");

                        SqlDataAdapter sda = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        dgvCus.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("เกิดข้อผิดพลาด: " + ex.Message);
            }
        }

        private void btnEdits_Click(object sender, EventArgs e)
        {

            CheckField();
            if (check) return;
            if (MessageBox.Show("คุณต้องการจะแก้ไขข้อมูล ?", "แจ้งเตือนจากระบบ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                return;

                try
            {
               
                using (SqlConnection con = dbcon.GetConnection())
                    {
                    con.Open();
                    string checkQuery = "SELECT * FROM CustomerTbl WHERE CustName = @CN AND CustAdd = @CA AND CustPhone = @CP AND CustEmail = @CM";
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, con))
                    {
                        checkCmd.Parameters.AddWithValue("@CN", txtname.Text);
                        checkCmd.Parameters.AddWithValue("@CA", txtAddress.Text);
                        checkCmd.Parameters.AddWithValue("@CP", txtphone.Text);
                        checkCmd.Parameters.AddWithValue("@CM", txtemail.Text);
                        dr = checkCmd.ExecuteReader();
                        if (dr.Read())
                        {
                            MessageBox.Show("ข้อมูลนี้มีอยู่ในระบบแล้ว!", "แจ้งเตือนจากระบบ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        dr.Close();
                    }
                    // ถ้าไม่มีข้อมูลซ้ำ ให้เพิ่มข้อมูล
                    string updateQuery = "UPDATE CustomerTbl SET CustName=@CN, CustAdd=@CA, CustPhone=@CP, CustEmail=@CM WHERE CustId=@CID";
                    using (SqlCommand updateCmd = new SqlCommand(updateQuery, con))
                    {
                        updateCmd.Parameters.AddWithValue("@CN", txtname.Text);
                        updateCmd.Parameters.AddWithValue("@CA", txtAddress.Text);
                        updateCmd.Parameters.AddWithValue("@CP", txtphone.Text);
                        updateCmd.Parameters.AddWithValue("@CM", txtemail.Text);
                        updateCmd.Parameters.AddWithValue("@CID", Key);
                        updateCmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("อัพเดทข้อมูลเรียบร้อย", "เเจ้งเตือนจากระบบ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DisplayCustomer();
                    Clear();

                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }
    }

}

