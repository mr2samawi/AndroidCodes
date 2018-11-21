--------- Data Base --------
// Restore
SqlConnection RestoreConnection = new SqlConnection("Server = .; Database = Master; Integrated Security = true");
using (OpenFileDialog OFD = new OpenFileDialog())
{
	OFD.Filter = "ملفات قاعدة البيانات |*mdf";
	if(OFD.ShowDialog() == DialogResult.OK)
	{	
		string SQLQuery = "IF db_ID ('DBName') IS NULL CREATE DATABASE DBName; ALTER DATABASE DBName SET OFFLINE WITH ROLLBACK IMMEDIATE ; RESTORE DATABASE DBName FROM DISK= '" + OFD.FileName + "' WITH REPLACE";
		SqlCommand Command = new SqlCommand(SQLQuery,RestoreConnection);
		RestoreConnection.Open();
		Command.ExecuteNonQuery();
		RestoreConnection.Close();
		MessageBox.Show("تم استعادة النسخة الاحتياطية بنجاح", " استعادة نسخة احتياطية", MessageBoxButtons.OK, MessageBoxIcon.Information);
	
	}
}


// BackUp
SqlConnection BackupConnection = new SqlConnection("Server = .; Database = DBName; Integrated Security = true");

using(FolderBrowserDialog FBD = new FolderBrowserDialog())
{
	if(FBD.ShowDialog() == DialogResult.OK)
	{
		string BackUpPath = FBD.SelectedPath + "\\DB_BackUp - " + DateTime.Now.ToString().Replace("/","-").Replace(":","-") + ".bak";
		string BacupString = "Backup Database DBName to Disk = '" + BackUpPath + "'";
		SqlCommand Command = new SqlCommand(BacupString, BackupConnection);
		BackupConnection.Open();
		Command.ExecuteNonQuery();
		BackupConnection.Close();
		MessageBox.Show("تم انشاء النسخة الاحتياطية بنجاح", "نسخة احتياطية", MessageBoxButtons.OK, MessageBoxIcon.Information);
	
	}
}
