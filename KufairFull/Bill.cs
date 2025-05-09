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
    public partial class Bill : Form
    {

        SqlConnection cn = new SqlConnection(@"Data Source=DESKTOP-SSC2FCL;Initial Catalog=KUFAIR;User ID=sa;Password=181244;Pooling=False");
        SqlCommand cm = new SqlCommand();
        DbConnect dbcon = new DbConnect();
        DataSet ds = new DataSet();
        SqlDataReader dr;
        string title = "KUFAIR";
        bool check = false;


        public Bill()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.connection());
            cbCategory.SelectedIndex = 0;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                CheckField();
                if (check)
                {
                    if (MessageBox.Show("คุณต้องการที่จะบันทึกข้อมูล?", "แจ้งเตือนจากระบบ !!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        cm = new SqlCommand("INSERT INTO tblBill (CustName,pcategory,pqty,pprice,EmpName) VALUES (@CN,@PC,@PQ,@PP,@EM)", cn);
                        cm.Parameters.AddWithValue("@CN", txtCsname.Text);
                        cm.Parameters.AddWithValue("@PC", cbCategory.Text);
                        cm.Parameters.AddWithValue("@PQ", int.Parse(txtQty.Text));
                        cm.Parameters.AddWithValue("@PP", double.Parse(txtPrice.Text));
                        cm.Parameters.AddWithValue("@EM", txtEmp.Text);

                        cn.Open();
                        cm.ExecuteNonQuery();
                        cn.Close();
                        MessageBox.Show("บันทึกข้อมูลเสร็จสิ้น!", title);
                        Clear();  
                        

                    }

                }

            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, title);
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
            if (txtCsname.Text == "" | txtPrice.Text == "" | txtQty.Text == "" | txtEmp.Text == "")
            {
                MessageBox.Show("โปรดใส่ข้อมูลให้ครบ!", "Warning");
                return;
            }
            check = true;
        }

        #endregion Method

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Bill2 b2 = new Bill2();
            b2.Show();
            this.Hide();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Bill_Load(object sender, EventArgs e)
        {

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
