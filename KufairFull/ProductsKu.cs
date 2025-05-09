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
    public partial class ProductsKu : Form
    {
        SqlConnection cn = new SqlConnection(@"Data Source=Dechawat;Initial Catalog=dataKUFAIR;User ID=adminKufair;Password=181244;Pooling=False");
        SqlCommand cm = new SqlCommand();
        DbConnect dbcon = new DbConnect();
        DataSet ds = new DataSet();
        SqlDataReader dr;
        string title = "KUFAIR";
        public ProductsKu()
        {
            InitializeComponent();
            DisplayEmployess();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=Dechawat;Initial Catalog=dataKUFAIR;User ID=adminKufair;Password=181244;Pooling=False");
        private void DisplayEmployess()
        {
            Con.Open();
            string Query = "Select * from tbProduct";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            dgvPd.DataSource = ds.Tables[0];
            Con.Close();

        }

        private void ProductsKu_Load(object sender, EventArgs e)
        {
            string sql = "SELECT * FROM tbProduct";
            SqlDataAdapter da = new SqlDataAdapter(sql, cn);//เป็นตัวแปลงข้อมูลจากในฐานข้อมูล  โดยต้องระบุ2อย่าง  คุณจะทำไร,เอาที่ไหน   ดึงข้อมูล sql จาก con คือฐานข้อมูล
            da.Fill(ds, "dtProduct");//เรียกว่า DataTable
            dgvPd.DataSource = ds.Tables["dtProduct"];
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
            Employees ep = new Employees();
            ep.Show();
            this.Hide();
        }

        private void btnBilling_Click(object sender, EventArgs e)
        {
            Bill bb = new Bill();
            bb.Show();
            this.Hide();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Login lo = new Login();
            lo.Show();
            this.Hide();
        }

        private void btnpd_Click(object sender, EventArgs e)
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
