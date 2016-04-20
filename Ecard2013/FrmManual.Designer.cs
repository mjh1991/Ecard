namespace Ecard2013
{
    partial class FrmManual
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
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.txtArive = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.dtpArive = new System.Windows.Forms.DateTimePicker();
			this.txtOpen = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.txtCC = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.cmbType = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.dtpOpen = new System.Windows.Forms.DateTimePicker();
			this.btnYes = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.txtPosition = new System.Windows.Forms.TextBox();
			this.lblPos = new System.Windows.Forms.Label();
			this.txtRyCount = new System.Windows.Forms.TextBox();
			this.lblRyCount = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.txtPosition);
			this.groupBox1.Controls.Add(this.lblPos);
			this.groupBox1.Controls.Add(this.txtRyCount);
			this.groupBox1.Controls.Add(this.lblRyCount);
			this.groupBox1.Controls.Add(this.txtArive);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.dtpArive);
			this.groupBox1.Controls.Add(this.txtOpen);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.txtCC);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.cmbType);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.dtpOpen);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
			this.groupBox1.Location = new System.Drawing.Point(0, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(465, 141);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "输入乘务信息：";
			// 
			// txtArive
			// 
			this.txtArive.Location = new System.Drawing.Point(348, 106);
			this.txtArive.Name = "txtArive";
			this.txtArive.Size = new System.Drawing.Size(100, 21);
			this.txtArive.TabIndex = 9;
			this.txtArive.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtArive_KeyDown);
			this.txtArive.Leave += new System.EventHandler(this.txtTime_Leave);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(229, 110);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(113, 12);
			this.label5.TabIndex = 11;
			this.label5.Text = "到站（退勤）时间：";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(18, 110);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(65, 12);
			this.label6.TabIndex = 10;
			this.label6.Text = "到站日期：";
			// 
			// dtpArive
			// 
			this.dtpArive.CustomFormat = "yyyy-MM-dd";
			this.dtpArive.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtpArive.Location = new System.Drawing.Point(89, 106);
			this.dtpArive.Name = "dtpArive";
			this.dtpArive.Size = new System.Drawing.Size(121, 21);
			this.dtpArive.TabIndex = 8;
			this.dtpArive.CloseUp += new System.EventHandler(this.dtpArive_CloseUp);
			this.dtpArive.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpArive_KeyDown);
			// 
			// txtOpen
			// 
			this.txtOpen.Location = new System.Drawing.Point(348, 67);
			this.txtOpen.Name = "txtOpen";
			this.txtOpen.Size = new System.Drawing.Size(100, 21);
			this.txtOpen.TabIndex = 3;
			this.txtOpen.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtOpen_KeyDown);
			this.txtOpen.Leave += new System.EventHandler(this.txtTime_Leave);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(229, 71);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(113, 12);
			this.label4.TabIndex = 7;
			this.label4.Text = "开车（出勤）时间：";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(18, 71);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(65, 12);
			this.label3.TabIndex = 6;
			this.label3.Text = "开车日期：";
			// 
			// txtCC
			// 
			this.txtCC.Location = new System.Drawing.Point(348, 28);
			this.txtCC.Name = "txtCC";
			this.txtCC.Size = new System.Drawing.Size(100, 21);
			this.txtCC.TabIndex = 1;
			this.txtCC.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCC_KeyDown);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(250, 32);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(65, 12);
			this.label2.TabIndex = 4;
			this.label2.Text = "值乘车次：";
			// 
			// cmbType
			// 
			this.cmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbType.FormattingEnabled = true;
			this.cmbType.Items.AddRange(new object[] {
            "货  车",
            "客  车",
            "调  车"});
			this.cmbType.Location = new System.Drawing.Point(89, 28);
			this.cmbType.Name = "cmbType";
			this.cmbType.Size = new System.Drawing.Size(121, 20);
			this.cmbType.TabIndex = 0;
			this.cmbType.SelectedIndexChanged += new System.EventHandler(this.cmbType_SelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(27, 32);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(41, 12);
			this.label1.TabIndex = 2;
			this.label1.Text = "类型：";
			// 
			// dtpOpen
			// 
			this.dtpOpen.CustomFormat = "yyyy-MM-dd";
			this.dtpOpen.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtpOpen.Location = new System.Drawing.Point(89, 67);
			this.dtpOpen.Name = "dtpOpen";
			this.dtpOpen.Size = new System.Drawing.Size(121, 21);
			this.dtpOpen.TabIndex = 2;
			this.dtpOpen.CloseUp += new System.EventHandler(this.dtpOpen_CloseUp);
			this.dtpOpen.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpOpen_KeyDown);
			// 
			// btnYes
			// 
			this.btnYes.Location = new System.Drawing.Point(110, 163);
			this.btnYes.Name = "btnYes";
			this.btnYes.Size = new System.Drawing.Size(75, 23);
			this.btnYes.TabIndex = 2;
			this.btnYes.Text = "确定(&Y)";
			this.btnYes.UseVisualStyleBackColor = true;
			this.btnYes.Click += new System.EventHandler(this.btnYes_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(267, 163);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 3;
			this.btnCancel.Text = "取消(&C)";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// txtPosition
			// 
			this.txtPosition.Location = new System.Drawing.Point(348, 149);
			this.txtPosition.Name = "txtPosition";
			this.txtPosition.Size = new System.Drawing.Size(100, 21);
			this.txtPosition.TabIndex = 14;
			this.txtPosition.Visible = false;
			this.txtPosition.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPosition_KeyDown);
			// 
			// lblPos
			// 
			this.lblPos.AutoSize = true;
			this.lblPos.Location = new System.Drawing.Point(250, 153);
			this.lblPos.Name = "lblPos";
			this.lblPos.Size = new System.Drawing.Size(65, 12);
			this.lblPos.TabIndex = 15;
			this.lblPos.Text = "值乘位置：";
			this.lblPos.Visible = false;
			// 
			// txtRyCount
			// 
			this.txtRyCount.Location = new System.Drawing.Point(89, 148);
			this.txtRyCount.Name = "txtRyCount";
			this.txtRyCount.Size = new System.Drawing.Size(122, 21);
			this.txtRyCount.TabIndex = 12;
			this.txtRyCount.Visible = false;
			this.txtRyCount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRyCount_KeyDown);
			// 
			// lblRyCount
			// 
			this.lblRyCount.AutoSize = true;
			this.lblRyCount.Location = new System.Drawing.Point(13, 152);
			this.lblRyCount.Name = "lblRyCount";
			this.lblRyCount.Size = new System.Drawing.Size(65, 12);
			this.lblRyCount.TabIndex = 13;
			this.lblRyCount.Text = "值乘人数：";
			this.lblRyCount.Visible = false;
			// 
			// FrmManual
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(465, 208);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnYes);
			this.Controls.Add(this.groupBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FrmManual";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "手动充值";
			this.Load += new System.EventHandler(this.FrmManual_Load);
			this.Shown += new System.EventHandler(this.FrmManual_Shown);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DateTimePicker dtpOpen;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbType;
		private System.Windows.Forms.Button btnYes;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtCC;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txtOpen;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.TextBox txtArive;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.DateTimePicker dtpArive;
		private System.Windows.Forms.TextBox txtPosition;
		private System.Windows.Forms.Label lblPos;
		private System.Windows.Forms.TextBox txtRyCount;
		private System.Windows.Forms.Label lblRyCount;
    }
}