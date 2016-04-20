namespace EcardMonitor
{
	partial class frmBu
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
            this.btnStart = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPosId = new System.Windows.Forms.TextBox();
            this.txtMoney = new System.Windows.Forms.TextBox();
            this.dtpS = new System.Windows.Forms.DateTimePicker();
            this.dtpE = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.pbAll = new System.Windows.Forms.ProgressBar();
            this.lblProgressAll = new System.Windows.Forms.Label();
            this.lblFen = new System.Windows.Forms.Label();
            this.pbFen = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(67, 144);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 4;
            this.btnStart.Text = "开始(&S)";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(245, 144);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "关闭(&C)";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 84);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "总金额：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "机器号：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(186, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "开始时间：";
            // 
            // txtPosId
            // 
            this.txtPosId.Location = new System.Drawing.Point(92, 33);
            this.txtPosId.Name = "txtPosId";
            this.txtPosId.Size = new System.Drawing.Size(76, 21);
            this.txtPosId.TabIndex = 0;
            // 
            // txtMoney
            // 
            this.txtMoney.Location = new System.Drawing.Point(93, 80);
            this.txtMoney.Name = "txtMoney";
            this.txtMoney.Size = new System.Drawing.Size(75, 21);
            this.txtMoney.TabIndex = 1;
            // 
            // dtpS
            // 
            this.dtpS.CustomFormat = "yyyy-MM-dd HH:mm";
            this.dtpS.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpS.Location = new System.Drawing.Point(258, 32);
            this.dtpS.Name = "dtpS";
            this.dtpS.Size = new System.Drawing.Size(160, 21);
            this.dtpS.TabIndex = 2;
            // 
            // dtpE
            // 
            this.dtpE.CustomFormat = "yyyy-MM-dd HH:mm";
            this.dtpE.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpE.Location = new System.Drawing.Point(258, 79);
            this.dtpE.Name = "dtpE";
            this.dtpE.Size = new System.Drawing.Size(160, 21);
            this.dtpE.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(186, 83);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "截至时间：";
            // 
            // dgv
            // 
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4});
            this.dgv.Location = new System.Drawing.Point(12, 183);
            this.dgv.Name = "dgv";
            this.dgv.RowTemplate.Height = 23;
            this.dgv.Size = new System.Drawing.Size(447, 276);
            this.dgv.TabIndex = 11;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "序号";
            this.Column1.Name = "Column1";
            this.Column1.Width = 40;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "消费人员";
            this.Column2.Name = "Column2";
            this.Column2.Width = 80;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "消费时间";
            this.Column3.Name = "Column3";
            this.Column3.Width = 200;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "消费金额";
            this.Column4.Name = "Column4";
            this.Column4.Width = 80;
            // 
            // txtLog
            // 
            this.txtLog.Location = new System.Drawing.Point(497, 125);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.Size = new System.Drawing.Size(447, 334);
            this.txtLog.TabIndex = 12;
            // 
            // pbAll
            // 
            this.pbAll.Location = new System.Drawing.Point(497, 48);
            this.pbAll.Maximum = 192;
            this.pbAll.Minimum = 1;
            this.pbAll.Name = "pbAll";
            this.pbAll.Size = new System.Drawing.Size(447, 23);
            this.pbAll.Step = 1;
            this.pbAll.TabIndex = 13;
            this.pbAll.Value = 100;
            // 
            // lblProgressAll
            // 
            this.lblProgressAll.AutoSize = true;
            this.lblProgressAll.Location = new System.Drawing.Point(495, 33);
            this.lblProgressAll.Name = "lblProgressAll";
            this.lblProgressAll.Size = new System.Drawing.Size(53, 12);
            this.lblProgressAll.TabIndex = 14;
            this.lblProgressAll.Text = "总进度：";
            // 
            // lblFen
            // 
            this.lblFen.AutoSize = true;
            this.lblFen.Location = new System.Drawing.Point(495, 80);
            this.lblFen.Name = "lblFen";
            this.lblFen.Size = new System.Drawing.Size(65, 12);
            this.lblFen.TabIndex = 15;
            this.lblFen.Text = "当前进度：";
            // 
            // pbFen
            // 
            this.pbFen.Location = new System.Drawing.Point(497, 96);
            this.pbFen.Maximum = 192;
            this.pbFen.Minimum = 1;
            this.pbFen.Name = "pbFen";
            this.pbFen.Size = new System.Drawing.Size(447, 23);
            this.pbFen.Step = 1;
            this.pbFen.TabIndex = 16;
            this.pbFen.Value = 100;
            // 
            // frmBu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(956, 471);
            this.Controls.Add(this.pbFen);
            this.Controls.Add(this.lblFen);
            this.Controls.Add(this.lblProgressAll);
            this.Controls.Add(this.pbAll);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.dgv);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dtpE);
            this.Controls.Add(this.dtpS);
            this.Controls.Add(this.txtMoney);
            this.Controls.Add(this.txtPosId);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnStart);
            this.Name = "frmBu";
            this.Text = "餐补记录补充";
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnStart;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txtPosId;
		private System.Windows.Forms.TextBox txtMoney;
		private System.Windows.Forms.DateTimePicker dtpS;
		private System.Windows.Forms.DateTimePicker dtpE;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.DataGridView dgv;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.ProgressBar pbAll;
        private System.Windows.Forms.Label lblProgressAll;
        private System.Windows.Forms.Label lblFen;
        private System.Windows.Forms.ProgressBar pbFen;
	}
}