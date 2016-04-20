namespace Ecard2013
{
	partial class FrmMain
	{
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		/// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows 窗体设计器生成的代码

		/// <summary>
		/// 设计器支持所需的方法 - 不要
		/// 使用代码编辑器修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.btnQuery = new System.Windows.Forms.Button();
			this.btnWrite = new System.Windows.Forms.Button();
			this.btnReadInfo = new System.Windows.Forms.Button();
			this.grpPersonal = new System.Windows.Forms.GroupBox();
			this.txtLastMoney = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.txtValid = new System.Windows.Forms.TextBox();
			this.lblValid = new System.Windows.Forms.Label();
			this.txtMoney = new System.Windows.Forms.TextBox();
			this.txtDept = new System.Windows.Forms.TextBox();
			this.txtName = new System.Windows.Forms.TextBox();
			this.txtNo = new System.Windows.Forms.TextBox();
			this.lblMoney = new System.Windows.Forms.Label();
			this.lblDept = new System.Windows.Forms.Label();
			this.lblName = new System.Windows.Forms.Label();
			this.lblNo = new System.Windows.Forms.Label();
			this.grpList = new System.Windows.Forms.GroupBox();
			this.lvBu = new System.Windows.Forms.ListView();
			this.colBuType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colBuAddTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colBuReson = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colBuMoney = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colBuReceiveTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.lvBaoDan = new System.Windows.Forms.ListView();
			this.colOpenTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colRoadway = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colEngiBrand = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colEngiNo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colLine = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colDutyTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colReturn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colMoney = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colReceiveTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.tsslName = new System.Windows.Forms.ToolStripStatusLabel();
			this.tsslOper = new System.Windows.Forms.ToolStripStatusLabel();
			this.tsslDept = new System.Windows.Forms.ToolStripStatusLabel();
			this.tsslwlzx = new System.Windows.Forms.ToolStripStatusLabel();
			this.tsslTel = new System.Windows.Forms.ToolStripStatusLabel();
			this.grpTotal = new System.Windows.Forms.GroupBox();
			this.txtTotalMoney = new System.Windows.Forms.TextBox();
			this.txtTotalCount = new System.Windows.Forms.TextBox();
			this.lblTotalCount = new System.Windows.Forms.Label();
			this.lblTotalMoney = new System.Windows.Forms.Label();
			this.lblTotalTime = new System.Windows.Forms.Label();
			this.txtTotalTime = new System.Windows.Forms.TextBox();
			this.btnClear = new System.Windows.Forms.Button();
			this.dtpS = new System.Windows.Forms.DateTimePicker();
			this.dtpE = new System.Windows.Forms.DateTimePicker();
			this.label1 = new System.Windows.Forms.Label();
			this.btnXu = new System.Windows.Forms.Button();
			this.radioCheng = new System.Windows.Forms.RadioButton();
			this.radioOther = new System.Windows.Forms.RadioButton();
			this.grpPersonal.SuspendLayout();
			this.grpList.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.grpTotal.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnQuery
			// 
			this.btnQuery.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.btnQuery.Location = new System.Drawing.Point(699, 522);
			this.btnQuery.Name = "btnQuery";
			this.btnQuery.Size = new System.Drawing.Size(120, 80);
			this.btnQuery.TabIndex = 6;
			this.btnQuery.Text = "查询(&Q)";
			this.btnQuery.UseVisualStyleBackColor = true;
			this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
			// 
			// btnWrite
			// 
			this.btnWrite.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.btnWrite.Location = new System.Drawing.Point(273, 522);
			this.btnWrite.Name = "btnWrite";
			this.btnWrite.Size = new System.Drawing.Size(120, 80);
			this.btnWrite.TabIndex = 3;
			this.btnWrite.Text = "领取(&G)";
			this.btnWrite.UseVisualStyleBackColor = true;
			this.btnWrite.Click += new System.EventHandler(this.btnWrite_Click);
			// 
			// btnReadInfo
			// 
			this.btnReadInfo.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.btnReadInfo.Location = new System.Drawing.Point(131, 522);
			this.btnReadInfo.Name = "btnReadInfo";
			this.btnReadInfo.Size = new System.Drawing.Size(120, 80);
			this.btnReadInfo.TabIndex = 2;
			this.btnReadInfo.Text = "读卡(&R)";
			this.btnReadInfo.UseVisualStyleBackColor = true;
			this.btnReadInfo.Click += new System.EventHandler(this.btnReadInfo_Click);
			// 
			// grpPersonal
			// 
			this.grpPersonal.Controls.Add(this.txtLastMoney);
			this.grpPersonal.Controls.Add(this.label2);
			this.grpPersonal.Controls.Add(this.txtValid);
			this.grpPersonal.Controls.Add(this.lblValid);
			this.grpPersonal.Controls.Add(this.txtMoney);
			this.grpPersonal.Controls.Add(this.txtDept);
			this.grpPersonal.Controls.Add(this.txtName);
			this.grpPersonal.Controls.Add(this.txtNo);
			this.grpPersonal.Controls.Add(this.lblMoney);
			this.grpPersonal.Controls.Add(this.lblDept);
			this.grpPersonal.Controls.Add(this.lblName);
			this.grpPersonal.Controls.Add(this.lblNo);
			this.grpPersonal.Dock = System.Windows.Forms.DockStyle.Top;
			this.grpPersonal.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.grpPersonal.Location = new System.Drawing.Point(0, 0);
			this.grpPersonal.Name = "grpPersonal";
			this.grpPersonal.Size = new System.Drawing.Size(1018, 74);
			this.grpPersonal.TabIndex = 4;
			this.grpPersonal.TabStop = false;
			this.grpPersonal.Text = "持卡人信息：";
			// 
			// txtLastMoney
			// 
			this.txtLastMoney.ForeColor = System.Drawing.Color.Red;
			this.txtLastMoney.Location = new System.Drawing.Point(916, 25);
			this.txtLastMoney.Name = "txtLastMoney";
			this.txtLastMoney.ReadOnly = true;
			this.txtLastMoney.Size = new System.Drawing.Size(90, 26);
			this.txtLastMoney.TabIndex = 11;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(821, 25);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(90, 26);
			this.label2.TabIndex = 12;
			this.label2.Text = "上月余额：";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// txtValid
			// 
			this.txtValid.ForeColor = System.Drawing.Color.Black;
			this.txtValid.Location = new System.Drawing.Point(566, 25);
			this.txtValid.Name = "txtValid";
			this.txtValid.ReadOnly = true;
			this.txtValid.Size = new System.Drawing.Size(90, 26);
			this.txtValid.TabIndex = 10;
			// 
			// lblValid
			// 
			this.lblValid.Location = new System.Drawing.Point(486, 25);
			this.lblValid.Name = "lblValid";
			this.lblValid.Size = new System.Drawing.Size(75, 26);
			this.lblValid.TabIndex = 9;
			this.lblValid.Text = "有效期：";
			this.lblValid.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// txtMoney
			// 
			this.txtMoney.ForeColor = System.Drawing.Color.Black;
			this.txtMoney.Location = new System.Drawing.Point(726, 25);
			this.txtMoney.Name = "txtMoney";
			this.txtMoney.ReadOnly = true;
			this.txtMoney.Size = new System.Drawing.Size(90, 26);
			this.txtMoney.TabIndex = 3;
			// 
			// txtDept
			// 
			this.txtDept.ForeColor = System.Drawing.Color.Black;
			this.txtDept.Location = new System.Drawing.Point(391, 25);
			this.txtDept.Name = "txtDept";
			this.txtDept.ReadOnly = true;
			this.txtDept.Size = new System.Drawing.Size(90, 26);
			this.txtDept.TabIndex = 2;
			// 
			// txtName
			// 
			this.txtName.ForeColor = System.Drawing.Color.Black;
			this.txtName.Location = new System.Drawing.Point(231, 25);
			this.txtName.Name = "txtName";
			this.txtName.ReadOnly = true;
			this.txtName.Size = new System.Drawing.Size(90, 26);
			this.txtName.TabIndex = 1;
			// 
			// txtNo
			// 
			this.txtNo.ForeColor = System.Drawing.Color.Black;
			this.txtNo.Location = new System.Drawing.Point(71, 25);
			this.txtNo.Name = "txtNo";
			this.txtNo.ReadOnly = true;
			this.txtNo.Size = new System.Drawing.Size(90, 26);
			this.txtNo.TabIndex = 0;
			// 
			// lblMoney
			// 
			this.lblMoney.Location = new System.Drawing.Point(661, 25);
			this.lblMoney.Name = "lblMoney";
			this.lblMoney.Size = new System.Drawing.Size(60, 26);
			this.lblMoney.TabIndex = 6;
			this.lblMoney.Text = "金额：";
			this.lblMoney.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lblDept
			// 
			this.lblDept.Location = new System.Drawing.Point(326, 25);
			this.lblDept.Name = "lblDept";
			this.lblDept.Size = new System.Drawing.Size(60, 26);
			this.lblDept.TabIndex = 2;
			this.lblDept.Text = "车间：";
			this.lblDept.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lblName
			// 
			this.lblName.Location = new System.Drawing.Point(166, 25);
			this.lblName.Name = "lblName";
			this.lblName.Size = new System.Drawing.Size(60, 26);
			this.lblName.TabIndex = 1;
			this.lblName.Text = "姓名：";
			this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lblNo
			// 
			this.lblNo.Location = new System.Drawing.Point(6, 25);
			this.lblNo.Name = "lblNo";
			this.lblNo.Size = new System.Drawing.Size(60, 26);
			this.lblNo.TabIndex = 0;
			this.lblNo.Text = "工号：";
			this.lblNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// grpList
			// 
			this.grpList.Controls.Add(this.lvBu);
			this.grpList.Controls.Add(this.lvBaoDan);
			this.grpList.Dock = System.Windows.Forms.DockStyle.Top;
			this.grpList.Font = new System.Drawing.Font("宋体", 12F);
			this.grpList.Location = new System.Drawing.Point(0, 74);
			this.grpList.Name = "grpList";
			this.grpList.Size = new System.Drawing.Size(1018, 352);
			this.grpList.TabIndex = 5;
			this.grpList.TabStop = false;
			this.grpList.Text = "报单信息：";
			// 
			// lvBu
			// 
			this.lvBu.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colBuType,
            this.colBuAddTime,
            this.colBuReson,
            this.colBuMoney,
            this.colBuReceiveTime});
			this.lvBu.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvBu.FullRowSelect = true;
			this.lvBu.GridLines = true;
			this.lvBu.Location = new System.Drawing.Point(3, 22);
			this.lvBu.Name = "lvBu";
			this.lvBu.Size = new System.Drawing.Size(1012, 327);
			this.lvBu.TabIndex = 1;
			this.lvBu.UseCompatibleStateImageBehavior = false;
			this.lvBu.View = System.Windows.Forms.View.Details;
			// 
			// colBuType
			// 
			this.colBuType.Text = "类型";
			this.colBuType.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.colBuType.Width = 50;
			// 
			// colBuAddTime
			// 
			this.colBuAddTime.Text = "添加时间";
			this.colBuAddTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.colBuAddTime.Width = 200;
			// 
			// colBuReson
			// 
			this.colBuReson.Text = "补贴说明";
			this.colBuReson.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.colBuReson.Width = 500;
			// 
			// colBuMoney
			// 
			this.colBuMoney.Text = "补贴金额";
			this.colBuMoney.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.colBuMoney.Width = 100;
			// 
			// colBuReceiveTime
			// 
			this.colBuReceiveTime.Text = "领取时间";
			this.colBuReceiveTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.colBuReceiveTime.Width = 200;
			// 
			// lvBaoDan
			// 
			this.lvBaoDan.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colOpenTime,
            this.colRoadway,
            this.colEngiBrand,
            this.colEngiNo,
            this.colLine,
            this.colDutyTime,
            this.colReturn,
            this.colTime,
            this.colMoney,
            this.colReceiveTime,
            this.colType});
			this.lvBaoDan.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvBaoDan.FullRowSelect = true;
			this.lvBaoDan.GridLines = true;
			this.lvBaoDan.Location = new System.Drawing.Point(3, 22);
			this.lvBaoDan.Name = "lvBaoDan";
			this.lvBaoDan.Size = new System.Drawing.Size(1012, 327);
			this.lvBaoDan.TabIndex = 0;
			this.lvBaoDan.UseCompatibleStateImageBehavior = false;
			this.lvBaoDan.View = System.Windows.Forms.View.Details;
			// 
			// colOpenTime
			// 
			this.colOpenTime.DisplayIndex = 1;
			this.colOpenTime.Text = "开点";
			this.colOpenTime.Width = 0;
			// 
			// colRoadway
			// 
			this.colRoadway.DisplayIndex = 2;
			this.colRoadway.Text = "车次";
			this.colRoadway.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.colRoadway.Width = 70;
			// 
			// colEngiBrand
			// 
			this.colEngiBrand.DisplayIndex = 3;
			this.colEngiBrand.Text = "车型";
			this.colEngiBrand.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// colEngiNo
			// 
			this.colEngiNo.DisplayIndex = 4;
			this.colEngiNo.Text = "车号";
			this.colEngiNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// colLine
			// 
			this.colLine.DisplayIndex = 5;
			this.colLine.Text = "线路";
			this.colLine.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.colLine.Width = 170;
			// 
			// colDutyTime
			// 
			this.colDutyTime.DisplayIndex = 6;
			this.colDutyTime.Text = "出勤(开车)点";
			this.colDutyTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.colDutyTime.Width = 150;
			// 
			// colReturn
			// 
			this.colReturn.DisplayIndex = 7;
			this.colReturn.Text = "退勤(到站)点";
			this.colReturn.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.colReturn.Width = 150;
			// 
			// colTime
			// 
			this.colTime.DisplayIndex = 8;
			this.colTime.Text = "时长";
			this.colTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.colTime.Width = 80;
			// 
			// colMoney
			// 
			this.colMoney.DisplayIndex = 9;
			this.colMoney.Text = "金额";
			this.colMoney.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// colReceiveTime
			// 
			this.colReceiveTime.DisplayIndex = 10;
			this.colReceiveTime.Text = "领取时间";
			this.colReceiveTime.Width = 125;
			// 
			// colType
			// 
			this.colType.DisplayIndex = 0;
			this.colType.Text = "类型";
			this.colType.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.colType.Width = 50;
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslName,
            this.tsslOper,
            this.tsslDept,
            this.tsslwlzx,
            this.tsslTel});
			this.statusStrip1.Location = new System.Drawing.Point(0, 642);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(1018, 26);
			this.statusStrip1.TabIndex = 6;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// tsslName
			// 
			this.tsslName.AutoSize = false;
			this.tsslName.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
						| System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
						| System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
			this.tsslName.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
			this.tsslName.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.tsslName.Name = "tsslName";
			this.tsslName.Size = new System.Drawing.Size(300, 21);
			this.tsslName.Text = "新乡机务段乘务员就餐系统自助充值";
			// 
			// tsslOper
			// 
			this.tsslOper.AutoSize = false;
			this.tsslOper.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
						| System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
						| System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
			this.tsslOper.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
			this.tsslOper.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.tsslOper.Name = "tsslOper";
			this.tsslOper.Size = new System.Drawing.Size(150, 21);
			// 
			// tsslDept
			// 
			this.tsslDept.AutoSize = false;
			this.tsslDept.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
						| System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
						| System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
			this.tsslDept.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
			this.tsslDept.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.tsslDept.Name = "tsslDept";
			this.tsslDept.Size = new System.Drawing.Size(150, 21);
			// 
			// tsslwlzx
			// 
			this.tsslwlzx.AutoSize = false;
			this.tsslwlzx.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
						| System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
						| System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
			this.tsslwlzx.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
			this.tsslwlzx.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.tsslwlzx.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.tsslwlzx.Name = "tsslwlzx";
			this.tsslwlzx.Size = new System.Drawing.Size(240, 21);
			this.tsslwlzx.Text = "程序开发：新乡机务段网络中心";
			// 
			// tsslTel
			// 
			this.tsslTel.AutoSize = false;
			this.tsslTel.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
						| System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
						| System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
			this.tsslTel.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
			this.tsslTel.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.tsslTel.Name = "tsslTel";
			this.tsslTel.Size = new System.Drawing.Size(163, 21);
			this.tsslTel.Spring = true;
			this.tsslTel.Text = "联系方式：24533";
			// 
			// grpTotal
			// 
			this.grpTotal.Controls.Add(this.txtTotalMoney);
			this.grpTotal.Controls.Add(this.txtTotalCount);
			this.grpTotal.Controls.Add(this.lblTotalCount);
			this.grpTotal.Controls.Add(this.lblTotalMoney);
			this.grpTotal.Controls.Add(this.lblTotalTime);
			this.grpTotal.Controls.Add(this.txtTotalTime);
			this.grpTotal.Dock = System.Windows.Forms.DockStyle.Top;
			this.grpTotal.Font = new System.Drawing.Font("宋体", 12F);
			this.grpTotal.Location = new System.Drawing.Point(0, 426);
			this.grpTotal.Name = "grpTotal";
			this.grpTotal.Size = new System.Drawing.Size(1018, 57);
			this.grpTotal.TabIndex = 7;
			this.grpTotal.TabStop = false;
			this.grpTotal.Text = "合计：";
			this.grpTotal.Enter += new System.EventHandler(this.grpTotal_Enter);
			// 
			// txtTotalMoney
			// 
			this.txtTotalMoney.ForeColor = System.Drawing.Color.Red;
			this.txtTotalMoney.Location = new System.Drawing.Point(884, 18);
			this.txtTotalMoney.Name = "txtTotalMoney";
			this.txtTotalMoney.ReadOnly = true;
			this.txtTotalMoney.Size = new System.Drawing.Size(100, 26);
			this.txtTotalMoney.TabIndex = 17;
			// 
			// txtTotalCount
			// 
			this.txtTotalCount.ForeColor = System.Drawing.Color.Red;
			this.txtTotalCount.Location = new System.Drawing.Point(133, 18);
			this.txtTotalCount.Name = "txtTotalCount";
			this.txtTotalCount.ReadOnly = true;
			this.txtTotalCount.Size = new System.Drawing.Size(100, 26);
			this.txtTotalCount.TabIndex = 13;
			// 
			// lblTotalCount
			// 
			this.lblTotalCount.AutoSize = true;
			this.lblTotalCount.Location = new System.Drawing.Point(82, 21);
			this.lblTotalCount.Name = "lblTotalCount";
			this.lblTotalCount.Size = new System.Drawing.Size(56, 16);
			this.lblTotalCount.TabIndex = 12;
			this.lblTotalCount.Text = "趟数：";
			// 
			// lblTotalMoney
			// 
			this.lblTotalMoney.AutoSize = true;
			this.lblTotalMoney.Location = new System.Drawing.Point(778, 22);
			this.lblTotalMoney.Name = "lblTotalMoney";
			this.lblTotalMoney.Size = new System.Drawing.Size(88, 16);
			this.lblTotalMoney.TabIndex = 16;
			this.lblTotalMoney.Text = "补贴金额：";
			// 
			// lblTotalTime
			// 
			this.lblTotalTime.AutoSize = true;
			this.lblTotalTime.Location = new System.Drawing.Point(388, 22);
			this.lblTotalTime.Name = "lblTotalTime";
			this.lblTotalTime.Size = new System.Drawing.Size(88, 16);
			this.lblTotalTime.TabIndex = 14;
			this.lblTotalTime.Text = "工作时长：";
			// 
			// txtTotalTime
			// 
			this.txtTotalTime.ForeColor = System.Drawing.Color.Red;
			this.txtTotalTime.Location = new System.Drawing.Point(476, 19);
			this.txtTotalTime.Name = "txtTotalTime";
			this.txtTotalTime.ReadOnly = true;
			this.txtTotalTime.Size = new System.Drawing.Size(100, 26);
			this.txtTotalTime.TabIndex = 15;
			// 
			// btnClear
			// 
			this.btnClear.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.btnClear.Location = new System.Drawing.Point(557, 522);
			this.btnClear.Name = "btnClear";
			this.btnClear.Size = new System.Drawing.Size(120, 80);
			this.btnClear.TabIndex = 5;
			this.btnClear.Text = "重置(&C)";
			this.btnClear.UseVisualStyleBackColor = true;
			this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
			// 
			// dtpS
			// 
			this.dtpS.CustomFormat = "yyyy年MM月dd日";
			this.dtpS.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.dtpS.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtpS.Location = new System.Drawing.Point(843, 525);
			this.dtpS.Name = "dtpS";
			this.dtpS.Size = new System.Drawing.Size(163, 26);
			this.dtpS.TabIndex = 7;
			// 
			// dtpE
			// 
			this.dtpE.CustomFormat = "yyyy年MM月dd日";
			this.dtpE.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.dtpE.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtpE.Location = new System.Drawing.Point(843, 572);
			this.dtpE.Name = "dtpE";
			this.dtpE.Size = new System.Drawing.Size(163, 26);
			this.dtpE.TabIndex = 8;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label1.Location = new System.Drawing.Point(1055, 512);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(24, 16);
			this.label1.TabIndex = 15;
			this.label1.Text = "到";
			// 
			// btnXu
			// 
			this.btnXu.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.btnXu.Location = new System.Drawing.Point(415, 522);
			this.btnXu.Name = "btnXu";
			this.btnXu.Size = new System.Drawing.Size(120, 80);
			this.btnXu.TabIndex = 4;
			this.btnXu.Text = "延期清零(&X)";
			this.btnXu.UseVisualStyleBackColor = true;
			this.btnXu.Click += new System.EventHandler(this.btnXu_Click);
			// 
			// radioCheng
			// 
			this.radioCheng.AutoSize = true;
			this.radioCheng.Checked = true;
			this.radioCheng.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.radioCheng.Location = new System.Drawing.Point(14, 531);
			this.radioCheng.Name = "radioCheng";
			this.radioCheng.Size = new System.Drawing.Size(90, 20);
			this.radioCheng.TabIndex = 0;
			this.radioCheng.TabStop = true;
			this.radioCheng.Text = "乘务补贴";
			this.radioCheng.UseVisualStyleBackColor = true;
			this.radioCheng.CheckedChanged += new System.EventHandler(this.radioCheng_CheckedChanged);
			// 
			// radioOther
			// 
			this.radioOther.AutoSize = true;
			this.radioOther.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.radioOther.Location = new System.Drawing.Point(14, 575);
			this.radioOther.Name = "radioOther";
			this.radioOther.Size = new System.Drawing.Size(90, 20);
			this.radioOther.TabIndex = 1;
			this.radioOther.Text = "其他补贴";
			this.radioOther.UseVisualStyleBackColor = true;
			// 
			// FrmMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1018, 668);
			this.Controls.Add(this.radioOther);
			this.Controls.Add(this.radioCheng);
			this.Controls.Add(this.btnXu);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.dtpE);
			this.Controls.Add(this.dtpS);
			this.Controls.Add(this.btnClear);
			this.Controls.Add(this.grpTotal);
			this.Controls.Add(this.grpList);
			this.Controls.Add(this.grpPersonal);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.btnReadInfo);
			this.Controls.Add(this.btnWrite);
			this.Controls.Add(this.btnQuery);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "FrmMain";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "餐补系统自助充值";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmMain_FormClosed);
			this.Load += new System.EventHandler(this.FrmMain_Load);
			this.grpPersonal.ResumeLayout(false);
			this.grpPersonal.PerformLayout();
			this.grpList.ResumeLayout(false);
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.grpTotal.ResumeLayout(false);
			this.grpTotal.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

        private System.Windows.Forms.Button btnQuery;
		private System.Windows.Forms.Button btnWrite;
		private System.Windows.Forms.Button btnReadInfo;
		private System.Windows.Forms.GroupBox grpPersonal;
		private System.Windows.Forms.TextBox txtMoney;
		private System.Windows.Forms.TextBox txtDept;
		private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.TextBox txtNo;
		private System.Windows.Forms.GroupBox grpList;
		private System.Windows.Forms.ListView lvBaoDan;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel tsslName;
		private System.Windows.Forms.ToolStripStatusLabel tsslwlzx;
		private System.Windows.Forms.ToolStripStatusLabel tsslTel;
		private System.Windows.Forms.ColumnHeader colOpenTime;
		private System.Windows.Forms.ColumnHeader colRoadway;
		private System.Windows.Forms.ColumnHeader colEngiBrand;
		private System.Windows.Forms.ColumnHeader colEngiNo;
		private System.Windows.Forms.ColumnHeader colLine;
		private System.Windows.Forms.ColumnHeader colDutyTime;
		private System.Windows.Forms.ColumnHeader colReturn;
		private System.Windows.Forms.ColumnHeader colTime;
		private System.Windows.Forms.ColumnHeader colMoney;
		private System.Windows.Forms.ColumnHeader colReceiveTime;
		private System.Windows.Forms.GroupBox grpTotal;
		private System.Windows.Forms.TextBox txtTotalMoney;
		private System.Windows.Forms.TextBox txtTotalCount;
		private System.Windows.Forms.Label lblTotalCount;
		private System.Windows.Forms.Label lblTotalMoney;
		private System.Windows.Forms.Label lblTotalTime;
        private System.Windows.Forms.TextBox txtTotalTime;
		private System.Windows.Forms.ColumnHeader colType;
        private System.Windows.Forms.ToolStripStatusLabel tsslOper;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.DateTimePicker dtpS;
        private System.Windows.Forms.DateTimePicker dtpE;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnXu;
		private System.Windows.Forms.TextBox txtValid;
		private System.Windows.Forms.ListView lvBu;
		private System.Windows.Forms.ColumnHeader colBuAddTime;
		private System.Windows.Forms.ColumnHeader colBuReson;
		private System.Windows.Forms.ColumnHeader colBuMoney;
		private System.Windows.Forms.ColumnHeader colBuReceiveTime;
		private System.Windows.Forms.ColumnHeader colBuType;
		private System.Windows.Forms.RadioButton radioCheng;
		private System.Windows.Forms.RadioButton radioOther;
        private System.Windows.Forms.ToolStripStatusLabel tsslDept;
		private System.Windows.Forms.TextBox txtLastMoney;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label lblValid;
		private System.Windows.Forms.Label lblMoney;
		private System.Windows.Forms.Label lblDept;
		private System.Windows.Forms.Label lblName;
		private System.Windows.Forms.Label lblNo;
	}
}

