using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Ecard2013
{
    public partial class FrmManual : Form
    {
        public DataTable dtResult;
		string oper;
        public FrmManual(string user)
        {
            InitializeComponent();
			this.oper = user;
			init();
        }

		private bool isValidTime(string str)
		{
			if (str.Length > 4 || str.Length < 3) return false;
			try
			{
				Convert.ToInt32(str);
			}
			catch
			{
				return false;
			}
			int shi, fen;
			try
			{
				shi = Convert.ToInt32(str.Substring(0, str.Length - 2));
				fen = Convert.ToInt32(str.Substring(str.Length - 2, 2));
			}
			catch
			{
				return false;
			}
			if (shi < 0 || shi > 23) return false;
			if (fen < 0 || fen > 59) return false;
			return true;
		}

		private void init()
		{
			cmbType.SelectedIndex = 2;
			txtCC.Text = "";
			txtPosition.Text = "1";
			txtRyCount.Text = "2";
			txtOpen.Text = "";
			txtArive.Text = "";
			txtRyCount.Visible = false;
			txtPosition.Visible = false;
			lblRyCount.Visible = false;
			lblPos.Visible = false;
		}

        private void FrmManual_Load(object sender, EventArgs e)
        {
			//init();
        }



		private void txtTime_Leave(object sender, EventArgs e)
		{
			TextBox tb = (TextBox)sender;
			string tbstr = tb.Text.Trim();
			if (tbstr == "")
			{
				tb.Text = "";
				return;
			}
			tbstr = tbstr.Replace(":", "");
			if (!isValidTime(tbstr))
			{
				tb.SelectAll();
				tb.Focus();
				MessageBox.Show("不是正确时间格式");
			}
			else
			{
				tb.Text = tbstr.Insert(tbstr.Length - 2, ":");
			}
	
		}

		private void FrmManual_Shown(object sender, EventArgs e)
		{
			//cmbType.SelectedIndex = 0;
			//dtpOpen.Value = DateTime.Now;
			txtCC.Focus();

		}

		private void btnYes_Click(object sender, EventArgs e)
		{
			int ryCount = 2;
			int pos = 1;
			if (txtCC.Text.Trim().Length == 0)
			{
				MessageBox.Show("请输入车次");
				txtCC.Focus();
				return;
			}
			if (cmbType.SelectedIndex == 2)
			{
				try
				{
					if(txtRyCount.Text.Trim() == "")

					{
						ryCount = 0;
					}
					else
					{

					ryCount = Convert.ToInt32(txtRyCount.Text);
					}
				}
				catch
				{
					MessageBox.Show("请输入正确的值乘人数");
					txtRyCount.SelectAll();
					txtRyCount.Focus();
					return;
				}
				try
				{
					pos = Convert.ToInt32(txtPosition.Text);
				}
				catch
				{
					MessageBox.Show("请输入正确的值乘位置");
					txtPosition.SelectAll();
					txtPosition.Focus();
					return;
				}
			}


			dtResult = new DataTable("unfinishedRecord");
			dtResult.Columns.Add("id", typeof(int));
			dtResult.Columns.Add("flag", typeof(int));//
			dtResult.Columns.Add("position", typeof(int));//
			dtResult.Columns.Add("open_time", typeof(string));//
			dtResult.Columns.Add("Roadway", typeof(string));//
			dtResult.Columns.Add("Engi_Brand", typeof(string));//
			dtResult.Columns.Add("Engi_no", typeof(string));//
			dtResult.Columns.Add("qduan", typeof(string));//
			dtResult.Columns.Add("whole_time", typeof(string));//
			dtResult.Columns.Add("Return_time", typeof(string));
			dtResult.Columns.Add("Return_time2", typeof(string));
			dtResult.Columns.Add("arrive_time", typeof(string));
			dtResult.Columns.Add("arrive_time2", typeof(string));
			dtResult.Columns.Add("isReceived", typeof(int));
			dtResult.Columns.Add("receiveTime", typeof(string));
			dtResult.Columns.Add("ryCount",typeof(int));

			DataRow dr = dtResult.NewRow();
			dr["id"] = 0;
			
			dr["position"] = pos;
			
			dr["Roadway"] = txtCC.Text.Trim();
			dr["qduan"] = "";
			dr["Engi_Brand"] = "";
			dr["Engi_no"] = "";
			string strOpen = dtpOpen.Value.ToString("yyyy-MM-dd") + " " + txtOpen.Text.Trim();
			dr["open_time"] = strOpen;
			dr["whole_time"] = strOpen;
			string strArive = dtpArive.Value.ToString("yyyy-MM-dd") + " " + txtArive.Text.Trim();
			dr["Return_time"] = strArive;
			dr["Return_time2"] = strArive;
			dr["arrive_time"] = strArive;
			dr["arrive_time2"] = strArive;
			dr["isReceived"] = 0;
			dr["receiveTime"] = "";
			dr["ryCount"] = ryCount;
			if (cmbType.SelectedIndex == 0)//货车
			{
				dr["flag"] = 2;
			}
			else if (cmbType.SelectedIndex == 1)//客车
			{
				dr["flag"] = 1;
			}
			else
			{
				TimeSpan ts = Convert.ToDateTime(strArive) - Convert.ToDateTime(strOpen);
				if (oper == "cbyy")
				{
					if (ts.TotalHours > 10 && Convert.ToDateTime(strArive).Hour < 10)
					{

						dr["flag"] = 6;
					}
					else
					{
						if (txtCC.Text.Trim() == "调车")
						{
							dr["flag"] = 3;
						}
						else
						{
							dr["flag"] = 6;
						}
					}
				}
				else if (oper == "ayyy")
				{
					if (ts.TotalHours > 10 && Convert.ToDateTime(strArive).Hour < 10)
					{

						dr["flag"] = 6;
					}
					else
					{
						dr["flag"] = 3;
					}
				}
				else
				{
					MessageBox.Show("部门错误");
				}
			}
			
			

			dtResult.Rows.Add(dr);
			DialogResult = System.Windows.Forms.DialogResult.Yes;
			//Close();
			Hide();
		}

		private void txtOpen_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				dtpArive.Focus();
			}
		}

		private void txtArive_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				if (txtRyCount.Visible)
				{
					txtRyCount.Focus();
				}
				else
				{
					btnYes.Focus();
				}
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			DialogResult = System.Windows.Forms.DialogResult.Cancel;
			Close();
		}



		private void dtpArive_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				txtArive.Focus();

			}
		}

		private void txtCC_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				dtpOpen.Focus();
			}
		}

		private void dtpOpen_CloseUp(object sender, EventArgs e)
		{
			txtOpen.Focus();
		}

		private void dtpArive_CloseUp(object sender, EventArgs e)
		{
			txtArive.Focus();
		}

		private void dtpOpen_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				txtOpen.Focus();

			}
		}

		private void txtRyCount_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				if (txtPosition.Visible)
				{
					txtPosition.Focus();
				}
				else
				{
					btnYes.Focus();
				}

			}
		}

		private void txtPosition_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				btnYes.Focus();

			}
		}

		private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (cmbType.SelectedIndex == 2)
			{
				txtRyCount.Visible = true;
				txtPosition.Visible = true;
				lblRyCount.Visible = true ;
				lblPos.Visible = true;
			}
			else
			{
				txtRyCount.Visible = false;
				txtPosition.Visible = false;
				lblRyCount.Visible = false;
				lblPos.Visible = false;
			}
		}
    }
}
