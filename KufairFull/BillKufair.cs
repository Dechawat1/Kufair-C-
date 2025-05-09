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
    public partial class BillKufair : Form
    {
        DbConnect dbcon = new DbConnect();

        public BillKufair()
        {
            
                InitializeComponent();
                EmpNameLbl.Text = Login.Employee;
                GetCustomers();
                DisplayProduct();
                DisplayTransactions();
            
        }
        private void GetCustomers()
        {
            using (SqlConnection Con = dbcon.GetConnection())
            {
                try
                {
                    Con.Open();
                    string Query = "Select * from CustomerTbl";
                    using (SqlCommand cmd = new SqlCommand(Query, Con))
                    {
                        SqlDataAdapter sda = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        CustIdCb.DataSource = dt;
                        CustIdCb.DisplayMember = "CustName";
                        CustIdCb.ValueMember = "CustId";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }
        private void DisplayProduct()
        {
            using (SqlConnection Con = dbcon.GetConnection())
            {
                try
                {
                    Con.Open();
                    string selectQuery = "Select * from tbProduct";
                    using (SqlCommand cmd = new SqlCommand(selectQuery, Con))
                    {
                        SqlDataAdapter sda = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ProductsDGV.DataSource = dt;
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }

        }
        private void DisplayTransactions()
        {
            using (SqlConnection Con = dbcon.GetConnection())
            {
                if (EmpNameLbl.Text == "Admin")
                {
                    try
                    {
                        Con.Open();
                        string Query = "Select * from BillTbl";
                        SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
                        SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
                        var ds = new DataSet();
                        sda.Fill(ds);
                        TransactionsDGV.DataSource = ds.Tables[0];
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                    finally
                    {
                        Con.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Access Denied", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void GetCustName()
        {
            using (SqlConnection Con = dbcon.GetConnection()) // Use 'using' to ensure proper disposal
            {
                try
                {
                    Con.Open();
                    string Query = "Select * from CustomerTbl where CustId='" + CustIdCb.SelectedValue.ToString() + "'";
                    SqlCommand cmd = new SqlCommand(Query, Con);
                    DataTable dt = new DataTable();
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    sda.Fill(dt);
                    foreach (DataRow dr in dt.Rows)
                    {
                        CustNameTb.Text = dr["CustName"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        int Key = 0, Stock = 0;
        private void UpdateStock()
        {
            try
            {
                int NewQty = Stock - Convert.ToInt32(QtyTb.Text);
                using (SqlConnection Con = dbcon.GetConnection()) // Use 'using' to ensure proper disposal
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Update tbProduct set pqty=@PQ where pid=@PKey", Con);
                    cmd.Parameters.AddWithValue("@PQ", NewQty);
                    cmd.Parameters.AddWithValue("@PKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("อัพเดทสินค้า", "ข้อความจากรบบ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                DisplayProduct();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        

        private void CustIdCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetCustName();
        }
        private void Reset()
        {
            PrNameTb.Text = "";
            QtyTb.Text = "";
            Stock = 0;
            Key = 0;
        }
        

       
        

        private void ResetBtn_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void BillDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        int prodid, prodqty, prodprice, tottal, pos = 60;
        int n = 0, GrdTotal = 0;
        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {  
        }

        private void Addbtn_Click(object sender, EventArgs e)
        {
            if (QtyTb.Text == "" || Convert.ToInt32(QtyTb.Text) > Stock)
            {
                MessageBox.Show("มีจำนวนสืนค้าคงเหลือไม่พอ","แจ้งเตือนจากระบบ",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            else if (QtyTb.Text == "" || Key == 0)
            {
                MessageBox.Show("โปรดระบุจำนวนล็อคที่คุณต้องการ","ข้อความจากระบบ",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
            else
            {
                int total = Convert.ToInt32(QtyTb.Text) * Convert.ToInt32(PrPriceTb.Text);
                DataGridViewRow newRow = new DataGridViewRow();
                newRow.CreateCells(BillDGV);
                newRow.Cells[0].Value = n + 1;
                newRow.Cells[1].Value = PrNameTb.Text;
                newRow.Cells[2].Value = PrPriceTb.Text;
                newRow.Cells[3].Value = QtyTb.Text;
                newRow.Cells[4].Value = total;
                newRow.Cells[5].Value = dto.Value.Date;

                GrdTotal = GrdTotal + total;
                BillDGV.Rows.Add(newRow);
                n++;
                TotalLbl.Text = "Total" + GrdTotal;
                UpdateStock();
                Reset();
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

        private void btnEp_Click(object sender, EventArgs e)
        {
            if (EmpNameLbl.Text == "Admin")
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

        private void pictureBox12_Click(object sender, EventArgs e)
        {

        }

        private void TransactionsDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            using (SqlConnection cn = dbcon.GetConnection()) // Ensure 'cn' is properly defined
            {
                try
                {
                    cn.Open();
                    string searchQuery = "Select * from BillTbl where EmpName LIKE @EN + '%'";
                    using (SqlCommand cm = new SqlCommand(searchQuery, cn)) // Ensure 'cm' is properly defined
                    {
                        cm.Parameters.AddWithValue("@EN", txtSearch.Text); // Correct parameter name with '@'
                        SqlDataAdapter sda = new SqlDataAdapter();
                        sda.SelectCommand = cm;
                        var dt = new DataTable();
                        dt.Clear();
                        sda.Fill(dt);
                        TransactionsDGV.DataSource = dt;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void CustIdCb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void CustNameTb_TextChanged(object sender, EventArgs e)
        {

        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("*****KUFAIR*******", new Font("Century Gothic", 10, FontStyle.Bold), Brushes.Green, new Point(75));
            e.Graphics.DrawString("ID โซนที่จอง ราคา จำนวน ราคารวม", new Font("Century Gothic", 10, FontStyle.Bold), Brushes.Black, new Point(26, 40));
            foreach (DataGridViewRow row in BillDGV.Rows)
            {

                prodid = Convert.ToInt32(row.Cells["Column1"].Value);
                prodname = "" + row.Cells["Column2"].Value;
                prodprice = Convert.ToInt32(row.Cells["Column3"].Value);
                prodqty = Convert.ToInt32(row.Cells["Column4"].Value);
                tottal = Convert.ToInt32(row.Cells["Column5"].Value);
                e.Graphics.DrawString("" + prodid, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Black, new Point(26, pos));
                e.Graphics.DrawString("" + prodname, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Black, new Point(45, pos));
                e.Graphics.DrawString("" + prodprice, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Black, new Point(120, pos));
                e.Graphics.DrawString("" + prodqty, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Black, new Point(170, pos));
                e.Graphics.DrawString("" + tottal, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Black, new Point(235, pos));
                pos = pos + 20;

            }
            e.Graphics.DrawString("Grand Total: " + GrdTotal, new Font("Century Gothic", 10, FontStyle.Bold), Brushes.Green, new Point(60, pos + 70));
            e.Graphics.DrawString("วันที่ครบสัญญาเช่า " + dto.Value.Date, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Black, new Point(10, pos + 95));
            e.Graphics.DrawString("ผู้เช่าคือ " + CustNameTb.Text, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Black, new Point(10, pos + 105 ));
            e.Graphics.DrawString("พนักงานที่รับเรื่อง " + EmpNameLbl.Text, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Black, new Point(10, pos + 125));
            e.Graphics.DrawString("************ KUFAIR BY S09.IT *********", new Font("Century Gothic", 10, FontStyle.Bold), Brushes.Green, new Point(10, pos + 140));
            BillDGV.Rows.Clear();
            BillDGV.Refresh();
            pos = 100;
            GrdTotal = 0;
            n = 0;
        }

        private void ProductsDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            PrNameTb.Text = ProductsDGV.SelectedRows[0].Cells[1].Value.ToString();
            Stock = Convert.ToInt32(ProductsDGV.SelectedRows[0].Cells[3].Value.ToString());
            PrPriceTb.Text = ProductsDGV.SelectedRows[0].Cells[4].Value.ToString();
            if (PrNameTb.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(ProductsDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        string prodname;
        private void PrintBtn_Click(object sender, EventArgs e)
        {
            InsertBill();
            printDocument1.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("pprnm", 285, 600);
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }

        private void BillKufair_Load(object sender, EventArgs e)
        {

        }
        private void InsertBill()
        {
            try
            {

                using (SqlConnection Con = dbcon.GetConnection())
                {
                    Con.Open();
                    string insertQuery = "insert into BillTbl (BDate,CustId,CustName,EmpName,Amt) values(@BD,@CI,@CN,@EN,@Am";
                    SqlCommand cmds = new SqlCommand(insertQuery, Con);
                    cmds.Parameters.AddWithValue("@BD", DateTime.Today.Date);
                    cmds.Parameters.AddWithValue("@CI", CustIdCb.SelectedValue.ToString());
                    cmds.Parameters.AddWithValue("@CN", CustNameTb.Text);
                    cmds.Parameters.AddWithValue("@EN", EmpNameLbl.Text);
                    cmds.Parameters.AddWithValue("@Am", GrdTotal);
                    cmds.ExecuteNonQuery();
                }
                MessageBox.Show("เพิ่มคำสั่งจองเรียบร้อย","ข้อความจากระบบ",MessageBoxButtons.OK,MessageBoxIcon.Information);
                DisplayTransactions();
               
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }
        

    }
}
