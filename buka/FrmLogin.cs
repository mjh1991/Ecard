using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using EcardFuli.EcardService;
using System.Net;

namespace EcardFuli
{
	public partial class FrmLogin : Form
	{
		public FrmLogin()
		{
			InitializeComponent();

		}

		private string user;

		public string UserName
		{
			get { return user; }
			set { user = value; }
		}

        private string dept;
        public string UserDept
        {
            get { return dept; }
            set { dept = value; }
        }

        private bool isAuto;
        public bool Automatic
        {
            get { return isAuto; }
            set { isAuto = value; }
        }

		private int place;
		public int Place
		{
			get { return place; }
			set { place = value; }
		}

		private void Login(string user, string pass,int type)
		{
			Service s = new Service();
			if (type == 0) //普通登录
			{
				if (s.Login(user, pass) == true)
				{
					this.DialogResult = DialogResult.Yes;
					this.user = user;
					this.dept = s.getUserDept(user);
					this.isAuto = s.getAuto(user);
					this.place = s.getPlace(user);
					MessageBox.Show("登陆成功，欢迎" + txtName.Text.Trim());
					Close();

				}
				else
				{
					MessageBox.Show("用户名不存在或密码错误，登录失败。");
					txtPass.Text = "";
					txtPass.Focus();

				}
			}
			else //if (type ==1) //IP认证免手工登录
			{
				user = s.LoginIP(pass);
				if (user != "err!")
				{
					this.DialogResult = DialogResult.Yes;
					this.user = user;
					this.dept = s.getUserDept(user);
					this.isAuto = s.getAuto(user);
					this.place = s.getPlace(user);
					Close();
				}
			}
		}

		private void BtnQuit_Click(object sender, EventArgs e)
		{

			Close();
		}

		private void BtnLogin_Click(object sender, EventArgs e)
		{
			if (txtName.Text.Trim() == "")
			{
				MessageBox.Show("请输入用户名。");
				txtName.Focus();
				return;
			}
			if (txtPass.Text.Trim() == "")
			{
				MessageBox.Show("请输入密码。");
				txtPass.Focus();
				return;
			}
			Login(txtName.Text.Trim(), txtPass.Text.Trim(),0);
			
		}

		private void txtName_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				txtPass.SelectAll();
				txtPass.Focus();
			}
		}

		private void txtPass_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				BtnLogin_Click(this, new EventArgs());
			}
		}

		private void label1_Click(object sender, EventArgs e)
		{
			//login("xxyy", "123456");
		}

		private void FrmLogin_Load(object sender, EventArgs e)
		{
			string ipAddr = "";
			IPAddress[] ips = Dns.GetHostAddresses(Dns.GetHostName());
			foreach (IPAddress ip in ips)
			{
				if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
				{
					if (ip.ToString().Substring(0, 5).Equals("10.99"))
					{
						ipAddr += ip.ToString();
					}
				}
			}
			Login(ipAddr, ipAddr, 1);
		}
	}
}