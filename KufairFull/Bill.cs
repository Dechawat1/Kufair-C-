using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace KufairFull
{
    public partial class Bill : Form
    {
        DbConnect dbcon = new DbConnect();
        SqlDataReader dr;
        string title = "KUFAIR";
        bool check = false;

        public Bill()
        {
            InitializeComponent();
            cbCategory.SelectedIndex = 0;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            CheckField();
            if (!check) return;

            if (MessageBox.Show("คุณต้องการที่จะบันทึกข้อมูล?", "แจ้งเตือนจากระบบ !!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            using (SqlConnection con = dbcon.GetConnection())
            {
                try
                {
                    con.Open();

                    // ตรวจสอบว่ามีข้อมูลซ้ำหรือไม่
                    string checkQuery = "SELECT * FROM tblBill WHERE CustName = @CN AND pcategory = @PC AND pqty = @PQ AND pprice = @PP";
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, con))
                    {
                        checkCmd.Parameters.AddWithValue("@CN", txtCsname.Text);
                        checkCmd.Parameters.AddWithValue("@PC", cbCategory.Text);
                        checkCmd.Parameters.AddWithValue("@PQ", int.Parse(txtQty.Text));
                        checkCmd.Parameters.AddWithValue("@PP", double.Parse(txtPrice.Text));

                        dr = checkCmd.ExecuteReader();
                        if (dr.Read())
                        {
                            MessageBox.Show("ข้อมูลนี้มีอยู่ในระบบแล้ว!", title);
                            return;
                        }
                        dr.Close();
                    }

                    // ถ้าไม่มีข้อมูลซ้ำ ให้เพิ่มข้อมูล
                    string insertQuery = "INSERT INTO tblBill (CustName, pcategory, pqty, pprice, EmpName) VALUES (@CN, @PC, @PQ, @PP, @EM)";
                    using (SqlCommand insertCmd = new SqlCommand(insertQuery, con))
                    {
                        insertCmd.Parameters.AddWithValue("@CN", txtCsname.Text);
                        insertCmd.Parameters.AddWithValue("@PC", cbCategory.Text);
                        insertCmd.Parameters.AddWithValue("@PQ", int.Parse(txtQty.Text));
                        insertCmd.Parameters.AddWithValue("@PP", double.Parse(txtPrice.Text));
                        insertCmd.Parameters.AddWithValue("@EM", txtEmp.Text);

                        insertCmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("บันทึกข้อมูลเสร็จสิ้น!", title);
                    Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("เกิดข้อผิดพลาด: " + ex.Message, title);
                }
            }
        }

        #region Method

        public void Clear()
        {
            txtCsname.Clear();
            txtPrice.Clear();
            txtQty.Clear();
            txtEmp.Clear();
            cbCategory.SelectedIndex = 0;

            btnUpdate.Enabled = false;
        }

        public void CheckField()
        {
            if (txtCsname.Text == "" || txtPrice.Text == "" || txtQty.Text == "" || txtEmp.Text == "")
            {
                MessageBox.Show("โปรดใส่ข้อมูลให้ครบ!", "Warning");
                check = false;
            }
            else
            {
                check = true;
            }
        }

        #endregion

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Bill2 b2 = new Bill2();
            b2.Show();
            this.Hide();
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
