using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            textBox1.Text = "";
        }

        Form1 mainForm;

        public Form2(Form1 mainForm)
        {
            this.mainForm = mainForm;
            InitializeComponent();

        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (textBox1.Text.Length == 4)
            {
                mainForm.lblPassCode.Text = textBox1.Text;
                this.Close();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 4)
            {
                mainForm.lblPassCode.Text = textBox1.Text;
            }
            this.Close();
        }

     }
}
