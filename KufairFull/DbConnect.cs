using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Configuration;

namespace KufairFull
{
    public class DbConnect
    {
        // ดึง Connection String จาก App.config
        private static readonly string conStr = ConfigurationManager.ConnectionStrings["KufairDb"].ConnectionString;

        // คืนค่า SqlConnection object
        public SqlConnection GetConnection()
        {
            return new SqlConnection(conStr);
        }

        // Execute SQL ที่ไม่คืนค่า เช่น INSERT, UPDATE, DELETE
        public void ExecuteQuery(string sql)
        {
            using (SqlConnection cn = new SqlConnection(conStr))
            {
                try
                {
                    cn.Open();
                    SqlCommand cm = new SqlCommand(sql, cn);
                    cm.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("เกิดข้อผิดพลาด: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // ทดสอบการเชื่อมต่อกับฐานข้อมูล
        public bool TestConnection()
        {
            using (SqlConnection cn = new SqlConnection(conStr))
            {
                try
                {
                    cn.Open();
                    return cn.State == System.Data.ConnectionState.Open;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("การเชื่อมต่อไม่สำเร็จ: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }
    }
}
