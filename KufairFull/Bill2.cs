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
    public partial class Bill2 : Form
    {

        SqlConnection cn = new SqlConnection(@"Data Source=DESKTOP-SSC2FCL;Initial Catalog=KUFAIR;User ID=sa;Password=181244;Pooling=False");
        SqlCommand cm = new SqlCommand();
        DbConnect dbcon = new DbConnect();
        DataSet ds = new DataSet();
        SqlDataReader dr;
        string title = "KUFAIR";

        public Bill2()
        {
            InitializeComponent();
            DisplayEmployess();
        }
        private void DisplayEmployess()
        {
            cn.Open();
            string Query = "Select * from tblBill";
            SqlDataAdapter sda = new SqlDataAdapter(Query, cn);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            dgvCash.DataSource = ds.Tables[0];
            cn.Close();

        }

        private void Bill2_Load(object sender, EventArgs e)
        {
            string sql = "SELECT * FROM tblBill";
            SqlDataAdapter da = new SqlDataAdapter(sql, cn);//เป็นตัวแปลงข้อมูลจากในฐานข้อมูล  โดยต้องระบุ2อย่าง  คุณจะทำไร,เอาที่ไหน   ดึงข้อมูล sql จาก con คือฐานข้อมูล
            da.Fill(ds, "dtblBill");//เรียกว่า DataTable
            dgvCash.DataSource = ds.Tables["dtblBill"];
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
