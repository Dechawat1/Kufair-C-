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
        DbConnect dbcon = new DbConnect();
        DataSet ds = new DataSet();


        public Bill2()
        {
            InitializeComponent();
            DisplayEmployess();
        }

        private void DisplayEmployess()
        {
            try
            {
                using (SqlConnection cn = dbcon.GetConnection())
                {
                    cn.Open();
                    string query = "SELECT * FROM tblBill";
                    using (SqlCommand cmd = new SqlCommand(query, cn))
                    {
                        SqlDataAdapter sda1 = new SqlDataAdapter(cmd); // Renamed 'sda' to 'sda1' to avoid conflict  
                        DataTable dt = new DataTable();
                        sda1.Fill(dt);
                        dgvCash.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Bill2_Load(object sender, EventArgs e)
        {
            using (SqlConnection cn = dbcon.GetConnection()) // Added 'using' to ensure proper resource disposal  
            {
                string sql = "SELECT * FROM tblBill";
                SqlDataAdapter da = new SqlDataAdapter(sql, cn);
                da.Fill(ds, "dtblBill");
                dgvCash.DataSource = ds.Tables["dtblBill"];
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
