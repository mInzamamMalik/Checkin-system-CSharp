using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Finisar.SQLite; // added for sqlite
using System.Threading;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public const int IN = 0;
        public const int OUT = 1;
        public const int NAME = 2;
        public const int EXT = 3;
        public const int HOURWORKED = 4;
        public const int TIME1 = 5;
        public const int NOTES = 6;
        public const int PASSCODE = 7;
        public const int secondIncrement = 1;
        private DBAttendance attendance;
        private int iy, ix;
        private bool clicked = false;

        SortedDictionary<string, int> dTimeWorked = new SortedDictionary<string, int>();  //{name, second worked}  total time worked per person
        SortedDictionary<string, int> dTimeSection = new SortedDictionary<string, int>();  //{name, second worked} total time per check-in/out
        SortedDictionary<string, bool> dCheckedIn = new SortedDictionary<string, bool>(); //{name, status}  in = true, out = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            loadDGViewFromXML("employees.xml");
            attendance = new DBAttendance();
            dataGridView2.DataSource = attendance.atbl;
            chkBoxViewBatch.Checked = true; // default to true
            chkBoxSecurity.Checked = false;       // default to true; enforece security
            timerStart();
         }

        private void timerStart()
        {
            timer1.Interval = 1000 * secondIncrement;
            timer1.Enabled = true;
            timer1.Start();
        }
        public void loadDGViewFromXML(string filePath)
        {
            DataSet dsRows = new DataSet("ROW");
            try
            {
                dsRows.ReadXml(filePath);
                dataGridView1.DataSource = dsRows;
                dataGridView1.DataMember = "ROW";
                setdataGridView1ColumnWidth();

                //tally();
            }
            catch (System.IO.IOException error)
            {
                MessageBox.Show(error.Message);
            }
        }
        public void setdataGridView1ColumnWidth()
        {
            int i;

            for (i = 0; i < dataGridView1.ColumnCount; i++)
            {
                dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridView1.Columns[i].ReadOnly = true;
                switch (i)
                {
                    case IN:
                    case OUT:
                        dataGridView1.Columns[i].Width = 32;
                        break;
                    case EXT:
                        dataGridView1.Columns[i].Width = 50;
                        break;
                    case NAME:
                        dataGridView1.Columns[i].Width = 100;
                        break;
                    case NOTES:
                        dataGridView1.Columns[i].Width = 140;
                        break;
                    case PASSCODE:
                        dataGridView1.Columns[i].Width = 0; //hide passcode
                        break;
                }
            }
            dataGridView1.AllowUserToAddRows = false;
        }

        private void checkPassCode()
        {
            lblPassCode.Text = "";
            Form2 subForm = new Form2(this);
            subForm.Show();
        }

        private void swapValue(int row, int col1, int col2)  //'X' between in/out
        {
            string temp = dataGridView1.Rows[row].Cells[col1].Value.ToString();
            dataGridView1.Rows[row].Cells[col1].Value = dataGridView1.Rows[row].Cells[col2].Value;
            dataGridView1.Rows[row].Cells[col2].Value = temp;
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            lblWarn.Text = "dataGridView1_CellMouseClick " + e.RowIndex + ":" +e.ColumnIndex;
            clicked = true; // let timer know a mouse click event occurred.
            ix = e.ColumnIndex;
            iy = e.RowIndex;
            if (chkBoxSecurity.Checked) // check identity
                checkPassCode();
        }       
        private void updateCheckInOut()
        {
            try
            {
                string name = "";
                switch (ix)
                {
                    case IN:
                    case OUT:
                        swapValue(iy, IN, OUT); //swap IN OUT 'X'
                        dataGridView1.Rows[iy].Cells[TIME1].Value = DateTime.Now.ToLongTimeString();  //record time of the check-in, check-out
                        name = dataGridView1.Rows[iy].Cells[NAME].Value.ToString();
                        if (dTimeWorked.ContainsKey(name))  // if person already checked-in or out at least one time today
                        {
                            if (dataGridView1.Rows[iy].Cells[OUT].Value.ToString() == "X") //update in-out stat
                                dCheckedIn[name] = false;  // person is check-out
                            else
                                dCheckedIn[name] = true;   // check-in
                        }
                        else  // name was not existed in table, add name to table, person 1st time check-in
                        {
                            dCheckedIn.Add(name, true);   // when person check in the first time
                            dTimeWorked.Add(name, 0); // start the time tracker
                         }
                        break;
                    case NAME:
                        // display the detail of check-in, out record
                        break;
                    default:
                        //lblWarn.Text = "Invalid selection. Please select 1, 2, or 3.";
                        break;
                }
                switch (ix)
                {
                    case IN:
                        attendance.atbl.Rows.Add(name, dataGridView1.Rows[iy].Cells[TIME1].Value.ToString(), "+", 0);
                        if (dTimeSection.ContainsKey(name))
                            dTimeSection[name] = 0; // reset the seconds to 0 for each check-in, out
                        else
                            dTimeSection.Add(name, 0);
                        break;
                    case OUT:
                        for (int i = 0; i < attendance.atbl.Rows.Count; i++)
                        {
                            if (attendance.atbl.Rows[i][0] == name && attendance.atbl.Rows[i][2] == "+")
                            {
                                attendance.atbl.Rows[i][2] = dataGridView1.Rows[iy].Cells[TIME1].Value.ToString();
                                attendance.atbl.Rows[i][3] = dTimeSection[name]; //seconds elapse between check-in, out
                                break;
                            }
                        }
                        break;
                 }
 
                //tally();
            }
            catch (System.DataMisalignedException error)
            {
                MessageBox.Show(error.Message);
            }
        }

        private void chkBoxSecurity_CheckStateChanged(object sender, EventArgs e)
        {
           attendance.aforeground = chkBoxViewBatch.Checked;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (clicked)
            {
                if (chkBoxSecurity.Checked == true)
                {
                    if (lblPassCode.Text.Length > 0)
                    {
                        if (dataGridView1[PASSCODE, iy].Value.ToString() == lblPassCode.Text)
                        {
                            updateCheckInOut();
                        }
                        else
                        {
                            clicked = false;
                            lblPassCode.Text = "";
                            MessageBox.Show("You entered wrong Pass Code!");
                        }
                        clicked = false;
                        lblPassCode.Text = "";
                    }
                }
                else
                {
                    updateCheckInOut();
                    clicked = false;
                }
            }
            updateTime();
        }
        
        private void updateTime()
        {
  	        foreach (KeyValuePair<string, bool> pair in dCheckedIn)
	        {
		       if (pair.Value) // true: in (if true, means in, then do increment time worked
                {
                    dTimeWorked[pair.Key] += secondIncrement;   // update grand total
                    dTimeSection[pair.Key] += secondIncrement;  // update current check-in, out
                }
            }
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                string name = dataGridView1.Rows[i].Cells[NAME].Value.ToString();
                if (dCheckedIn.ContainsKey(name) && dCheckedIn[name]) 
                {
                    dataGridView1.Rows[i].Cells[HOURWORKED].Value = secToHourMin(dTimeWorked[name]); //convert secord to viewable hh:mm:ss
                }
            }
            lblTime.Text = DateTime.Now.ToLongTimeString();
        }
        //convert secord to viewable hh:mm:ss
        private string secToHourMin(int sec)
        {
            int minute = sec / 60;
            int hour = minute/60;
            sec = sec - (minute*60);
            minute = minute - (hour * 60);
            string s = string.Format("{0}:{1}:{2}", hour, minute, sec);
            return s;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            attendance.saveLogData();
        }
    }
}
