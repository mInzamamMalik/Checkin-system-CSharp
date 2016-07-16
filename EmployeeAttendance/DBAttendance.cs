using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Finisar.SQLite; // added for sqlite
using System.IO;
using System.Data;

namespace WindowsFormsApplication1
{
    class DBAttendance
    {
        private SQLiteConnection asql_con;
        private SQLiteCommand asql_cmd;
        private SQLiteDataAdapter aDB;
        private DataSet aDS = new DataSet();
        private DataTable aDT = new DataTable();
        private DataSet aDS_Topic = new DataSet();
        private DataSet aDS_Subtopic = new DataSet();

        public string a_dbname = "employeeattendance.db";
        private int ascriptcount = 0;
        public bool aforeground = true;
        public DataTable atbl;

        public List<string> a_sqls = new List<string>();  //store sql statements

        public DBAttendance()
        {
            dbInitSetup();
        }

        public void dbInitSetup()
        {

            if (File.Exists(a_dbname))
            {
                //previousEnteredDirList();
            }
            else
            {   // 1st time use, will create employeeattendance.db by execute 
                // the following sqlite3 command which is embedded in batchjob.bat.
                // sqlite3 employeeattendance.db < setup.sql
                if (!File.Exists("batchjob.bat")) // create a batchjob.bat if not existed
                {
                    using (StreamWriter outfile = new StreamWriter("batchjob.bat"))
                    {
                        outfile.WriteLine("sqlite3 %1 < %2");
                        outfile.WriteLine("echo end of sqlite3 %1 %2");
                        outfile.WriteLine("type %2 >> sqlstmt_history.txt");
                        outfile.WriteLine("pause");
                        outfile.Close();
                    }
                }
                string parameters = a_dbname + " setup.sql"; // //"employeeattendance.db setup.sql";
                runBatchJob("batchjob.bat", parameters);
            }
            atbl = GetTable();
        }
        
        public void saveLogData()
        {
            string sql = "";
            a_sqls.Clear();
            for (int i = 0; i < atbl.Rows.Count; i++)
            {
                sql = "insert into worklog values (null, '" + atbl.Rows[i][0] + "', '" 
                                                            + atbl.Rows[i][1] + "', '" 
                                                            + atbl.Rows[i][2] + "'," 
                                                            + atbl.Rows[i][3] + ");";
                a_sqls.Add(sql);
            }
            sqlstmtWrt();
        }

        static DataTable GetTable()
        {
            //
            // Here we create a DataTable with four columns.
            //
            DataTable table = new DataTable();
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("In", typeof(string));
            table.Columns.Add("Out", typeof(string));
            table.Columns.Add("WorkSeconds", typeof(int));

            //
            // Here we add five DataRows.
            /*
            table.Rows.Add(25, "Indocin", "David", DateTime.Now);
            table.Rows.Add(50, "Enebrel", "Sam", DateTime.Now);
            table.Rows.Add(10, "Hydralazine", "Christoff", DateTime.Now);
            table.Rows.Add(21, "Combivent", "Janet", DateTime.Now);
            table.Rows.Add(100, "Dilantin", "Melanie", DateTime.Now);
             */
            return table;
        }
        private void setConnection(string db)
        {   // for example, db = "employeeattendance.db"
            string conn = @"Data Source=" + db + ";Version=3;New=False;Compress=True;";
            asql_con = new SQLiteConnection(conn);
        }
        private void sqlQueryToTable(string CommandText)
        {
            setConnection(a_dbname);
            asql_con.Open();
            asql_cmd = asql_con.CreateCommand();
            aDB = new SQLiteDataAdapter(CommandText, asql_con);
            asql_con.Close();
            aDS.Reset();
            aDB.Fill(aDS);
            aDT = aDS.Tables[0];
        }
        private void getCatalog()
        {
            /*
            string sql = "select ckey as 'catalog', sumdir as 'total_dir', sumfile as 'total_file' from statistic_page where dirpath = '" + aDir + "' order by ckey desc;";
            sqlQueryToTable(sql);
            a_ckeys.Clear();
            if (aDT.Rows.Count > 0)
                a_table2 = aDT;
             */
        }

        private void sqlstmtWrt()
        {
            string sqlscript = DateTime.Now.ToString(@"_yyyy_MMdd_HHmm_ss_") + ascriptcount++.ToString() + ".sql";

            using (StreamWriter outfile = new StreamWriter(sqlscript))
            {
                foreach (string s in a_sqls)
                {
                    outfile.WriteLine(s);
                }
                outfile.Close();
            }
            if (File.Exists(sqlscript))
            {
                string args = a_dbname + " " + sqlscript;
                runBatchJob("batchjob.bat", args);
            }
        }
        private void runBatchJob(string prog, string args)
        {
            string setupProg = prog;  // program to run
  

            if (File.Exists(prog))
            {
                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                proc.StartInfo.FileName = prog;
                proc.StartInfo.Arguments = args; //"employeeattendance.db setup.sql";
                proc.StartInfo.RedirectStandardError = false;
                proc.StartInfo.RedirectStandardOutput = false;
                proc.StartInfo.UseShellExecute = aforeground;  // run in background (invisible)
                proc.StartInfo.CreateNoWindow = true;
                proc.Start();
                proc.WaitForExit();
            }
        }
    }
}
