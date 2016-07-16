namespace WindowsFormsApplication1
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.lblWarn = new System.Windows.Forms.Label();
            this.chkBoxSecurity = new System.Windows.Forms.CheckBox();
            this.chkBoxViewBatch = new System.Windows.Forms.CheckBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lblTime = new System.Windows.Forms.Label();
            this.lblPassCode = new System.Windows.Forms.Label();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 61);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(682, 243);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseClick);
            // 
            // lblWarn
            // 
            this.lblWarn.AutoSize = true;
            this.lblWarn.Location = new System.Drawing.Point(12, 442);
            this.lblWarn.Name = "lblWarn";
            this.lblWarn.Size = new System.Drawing.Size(56, 17);
            this.lblWarn.TabIndex = 1;
            this.lblWarn.Text = "lblWarn";
            // 
            // chkBoxSecurity
            // 
            this.chkBoxSecurity.AutoSize = true;
            this.chkBoxSecurity.Location = new System.Drawing.Point(12, 12);
            this.chkBoxSecurity.Name = "chkBoxSecurity";
            this.chkBoxSecurity.Size = new System.Drawing.Size(333, 21);
            this.chkBoxSecurity.TabIndex = 2;
            this.chkBoxSecurity.Text = "Enforce identity.  Allow only self to check in-out. ";
            this.chkBoxSecurity.UseVisualStyleBackColor = true;
            this.chkBoxSecurity.CheckStateChanged += new System.EventHandler(this.chkBoxSecurity_CheckStateChanged);
            // 
            // chkBoxViewBatch
            // 
            this.chkBoxViewBatch.AutoSize = true;
            this.chkBoxViewBatch.Location = new System.Drawing.Point(12, 34);
            this.chkBoxViewBatch.Name = "chkBoxViewBatch";
            this.chkBoxViewBatch.Size = new System.Drawing.Size(400, 21);
            this.chkBoxViewBatch.TabIndex = 3;
            this.chkBoxViewBatch.Text = "Run batch sql job interactive.  Uncheck to run background.";
            this.chkBoxViewBatch.UseVisualStyleBackColor = true;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Location = new System.Drawing.Point(507, 34);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(53, 17);
            this.lblTime.TabIndex = 4;
            this.lblTime.Text = "lblTime";
            // 
            // lblPassCode
            // 
            this.lblPassCode.AutoSize = true;
            this.lblPassCode.Location = new System.Drawing.Point(635, 9);
            this.lblPassCode.Name = "lblPassCode";
            this.lblPassCode.Size = new System.Drawing.Size(71, 17);
            this.lblPassCode.TabIndex = 6;
            this.lblPassCode.Text = "passCode";
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(12, 325);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowTemplate.Height = 24;
            this.dataGridView2.Size = new System.Drawing.Size(682, 114);
            this.dataGridView2.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 307);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(155, 17);
            this.label1.TabIndex = 8;
            this.label1.Text = "Check in out history log";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(371, 442);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(323, 31);
            this.btnSave.TabIndex = 9;
            this.btnSave.Text = "save log to sqlite database";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(706, 468);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.lblPassCode);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.chkBoxViewBatch);
            this.Controls.Add(this.chkBoxSecurity);
            this.Controls.Add(this.lblWarn);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Form1";
            this.Text = "employee attendance tracker";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label lblWarn;
        private System.Windows.Forms.CheckBox chkBoxSecurity;
        private System.Windows.Forms.CheckBox chkBoxViewBatch;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lblTime;
        public System.Windows.Forms.Label lblPassCode;
        private System.Windows.Forms.DataGridView dataGridView2;
        public System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSave;
    }
}

