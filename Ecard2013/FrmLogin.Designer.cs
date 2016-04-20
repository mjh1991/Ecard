namespace Ecard2013
{
	partial class FrmLogin
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
			this.BtnQuit = new System.Windows.Forms.Button();
			this.BtnLogin = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.txtPass = new System.Windows.Forms.TextBox();
			this.txtName = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// BtnQuit
			// 
			this.BtnQuit.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.BtnQuit.Location = new System.Drawing.Point(145, 92);
			this.BtnQuit.Name = "BtnQuit";
			this.BtnQuit.Size = new System.Drawing.Size(84, 28);
			this.BtnQuit.TabIndex = 3;
			this.BtnQuit.Text = "退出(&Q)";
			this.BtnQuit.Click += new System.EventHandler(this.BtnQuit_Click);
			// 
			// BtnLogin
			// 
			this.BtnLogin.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.BtnLogin.Location = new System.Drawing.Point(23, 92);
			this.BtnLogin.Name = "BtnLogin";
			this.BtnLogin.Size = new System.Drawing.Size(84, 28);
			this.BtnLogin.TabIndex = 2;
			this.BtnLogin.Text = "登录(&O)";
			this.BtnLogin.Click += new System.EventHandler(this.BtnLogin_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label2.Location = new System.Drawing.Point(20, 60);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(63, 14);
			this.label2.TabIndex = 11;
			this.label2.Text = "密  码：";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label1.Location = new System.Drawing.Point(20, 28);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(63, 14);
			this.label1.TabIndex = 9;
			this.label1.Text = "用户名：";
			this.label1.Click += new System.EventHandler(this.label1_Click);
			// 
			// txtPass
			// 
			this.txtPass.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.txtPass.Location = new System.Drawing.Point(85, 57);
			this.txtPass.Name = "txtPass";
			this.txtPass.PasswordChar = '#';
			this.txtPass.Size = new System.Drawing.Size(144, 23);
			this.txtPass.TabIndex = 1;
			this.txtPass.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPass_KeyDown);
			// 
			// txtName
			// 
			this.txtName.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.txtName.Location = new System.Drawing.Point(85, 25);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(144, 23);
			this.txtName.TabIndex = 0;
			this.txtName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtName_KeyDown);
			// 
			// FrmLogin
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(259, 144);
			this.Controls.Add(this.BtnQuit);
			this.Controls.Add(this.BtnLogin);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.txtPass);
			this.Controls.Add(this.txtName);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "FrmLogin";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "自助充值登陆";
			this.Load += new System.EventHandler(this.FrmLogin_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button BtnQuit;
		private System.Windows.Forms.Button BtnLogin;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtPass;
		private System.Windows.Forms.TextBox txtName;
	}
}