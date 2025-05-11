** โหลด Database ไปไว้ในเซิฟเวอร์ Mssql server 


** แก้ไข app.config  ในส่วนของการเชื่อมฐานข้อมูล 
ข้อควรระวัง ตรง name ให้ตรงกันกลับ dbconnect 
 <connectionStrings>
        <add name="KufairFull"
			 connectionString="Data Source={ใส่ชื่อเซิฟเวอร์ของคุณ};Initial Catalog={ใส่ชิ้อฐานข้อมูล};User ID={ใส่ชิ้อผู้ใช้ของเซิฟเวอร์ database };Password={ใส่รหัสผ่านผู้ใช้ของเซิฟเวอร์ database };Pooling=False" providerName="System.Data.SqlClient" />
    </connectionStrings>


** จากนั้นถึงจะลองใช้งานได้

** Login ด้วย  user: Admin password: 0




