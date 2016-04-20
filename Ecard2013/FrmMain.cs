using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using CardFuncBase;
using System.Net;

namespace Ecard2013
{
	public partial class FrmMain : Form
	{
		
		private CustomerInfo ci;
		private string oper = "";
		private string ipAddr = "";
        private bool operAuto;
        private string operDept = "";
		private DataTable dt;
		private CardOper co;
		private EcardService.Service s;
		private bool canClose =false;
		private string cPath = Environment.CurrentDirectory + "\\Project2.dll";
		private static int clearDay = 25;//卡金额清零的日期
		private int _place;
		FrmManual fmm;
		private bool isRead;
		private bool isYanXu;




        public FrmMain(string user,string userDept,bool automatic,int place)
        {
            InitializeComponent();
            oper = user;
            operDept = userDept;
            operAuto = automatic;
			if (operAuto == false)
			{
				fmm = new FrmManual(oper);
			}
			_place = place;
			isRead = false;
			isYanXu = false;
        }



		private void WriteLog(string strContent)
		{
			FileInfo f = new FileInfo("d:\\log.txt");
			//f.Open( FileMode.OpenOrCreate , FileAccess.Write , FileShare.Read );
			StreamWriter sw;
			if (f.Exists)
			{
				sw = f.AppendText();
			}
			else
			{
				//if (!System.IO.Directory.Exists(strLogFilePath)) System.IO.Directory.CreateDirectory(strLogFilePath);
				sw = f.CreateText();
			}
			//sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + "--" + strContent);
			sw.WriteLine(strContent);
			sw.Close();
		}

		private void fillBaseInfo()
		{
			txtNo.Text = ci.Work_no;
			txtName.Text = ci.Work_name;
			txtDept.Text = ci.CustDept;
			txtMoney.Text = ci.Money.ToString();
			txtValid.Text = ci.NoUseDate.AddDays(-2).ToString("yyyy-MM-dd");
			if (ci.Money < ci.ThisMonthMoney)
			{
				txtLastMoney.Text = "0";
				
			}
			else
			{
				txtLastMoney.Text = (ci.Money - ci.ThisMonthMoney).ToString();
				
			}
		}

		#region 填充列表

		private void FillListViewShow()
		{
			switchListViewToBaoDan();
			Color bkc = new Color();
			int totalCount = 0;
			double totalTime = 0;
			double totalMoney = 0;
			if (dt.Rows.Count == 0) return;
			lvBaoDan.Items.Clear();
			ListViewItem[] li = new ListViewItem[dt.Rows.Count];
			
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				string[] subitem = new string[11];
				string goTime, backTime, receivetime;
				int pos, flag;
				double time, money;
				flag = Convert.ToInt32(dt.Rows[i]["flag"]);
				pos = Convert.ToInt32(dt.Rows[i]["position"]);
				subitem[0] = Convert.ToDateTime(dt.Rows[i]["open_time"]).ToString("MM月dd日 HH:mm");
				subitem[1] = dt.Rows[i]["Roadway"].ToString();
				subitem[2] = dt.Rows[i]["Engi_brand"].ToString();
				subitem[3] = dt.Rows[i]["Engi_no"].ToString();
				subitem[4] = dt.Rows[i]["qduan"].ToString();


				switch (flag)
				{
					case 1://客车
						subitem[10] = "客";
						goTime = dt.Rows[i]["open_time"].ToString();

						break;
					case 2://货车
						subitem[10] = "货";
						goTime = dt.Rows[i]["whole_time"].ToString();
						backTime = dt.Rows[i]["Return_time"].ToString();
						break;
					case 3:
					case 7:
							case 6://调车
						subitem[10] = "调";
						goTime = dt.Rows[i]["whole_time"].ToString();
						backTime = dt.Rows[i]["Return_time"].ToString();
						break;
					default :
						subitem[10] = "修";
						goTime = dt.Rows[i]["whole_time"].ToString();
						backTime = dt.Rows[i]["Return_time"].ToString();
						break;
				}
				backTime = s.getBackTime(dt.Rows[i]["Return_time"].ToString(), dt.Rows[i]["Return_time2"].ToString(), dt.Rows[i]["arrive_time"].ToString(), dt.Rows[i]["arrive_time2"].ToString(), flag, pos).ToString("yyyy-MM-dd HH:mm:ss");


				subitem[5] = Convert.ToDateTime(goTime).ToString("MM月dd日 HH:mm:ss");

				subitem[6] = Convert.ToDateTime(backTime).ToString("MM月dd日 HH:mm:ss");
				int ryCount = Convert.ToInt32(dt.Rows[i]["ryCount"]);
				time = s.getWorkTime(Convert.ToDateTime(goTime), Convert.ToDateTime(backTime), pos, flag);
				subitem[7] = Convert.ToString(time);
				money = s.getWorkMoney(Convert.ToDateTime(goTime), Convert.ToDateTime(backTime), pos, flag,ryCount);
				subitem[8] = Convert.ToString(money);
				receivetime = dt.Rows[i]["receiveTime"].ToString();
				int isReceived = Convert.ToInt32(dt.Rows[i]["isReceived"]);
				if (isReceived == 0)
				{
					subitem[9] = "未领取";
					bkc = Color.Blue;
				}
				else if (isReceived == 1)
				{
					subitem[9] = Convert.ToDateTime(receivetime).ToString("MM月dd日 HH:mm");
					bkc = Color.Green;
				}
				else
				{
					subitem[9] = "已过期";
					bkc = Color.Red;
				}

				totalCount += 1;
				totalMoney += money;
				totalTime += time;
				li[i] = new ListViewItem(subitem);
				li[i].ForeColor = bkc;
			}
			lvBaoDan.Items.AddRange(li);
			txtTotalMoney.Text = totalMoney.ToString();
			txtTotalTime.Text = totalTime.ToString();
			txtTotalCount.Text = totalCount.ToString();
		}


		private void FillListViewBu()
		{
			switchListViewToBuTie();
			Color bkc = new Color();
			int totalCount = 0;
			double totalMoney = 0;
			if (dt.Rows.Count == 0) return;
			lvBu.Items.Clear();
			ListViewItem[] li = new ListViewItem[dt.Rows.Count];
			Calc c = new Calc();
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				string[] subitem = new string[5];
				string receivetime;

				double money;

				subitem[0] = "补";
				subitem[1] = dt.Rows[i]["AddTime"].ToString();
				subitem[2] = dt.Rows[i]["type"].ToString();
				subitem[3] = dt.Rows[i]["account"].ToString();

				money = Convert.ToDouble(dt.Rows[i]["account"]);

				receivetime = dt.Rows[i]["receiveTime"].ToString();
				int isReceived = Convert.ToInt32(dt.Rows[i]["isReceived"]);
				if (isReceived == 0)
				{
					subitem[4] = "未领取";
					bkc = Color.Blue;
				}
				else if (isReceived == 1)
				{
					subitem[4] = Convert.ToDateTime(receivetime).ToString("MM月dd日 HH:mm");
					bkc = Color.Green;
				}
				else
				{
					subitem[4] = "已过期";
					bkc = Color.Red;
				}

				totalCount += 1;
				totalMoney += money;

				li[i] = new ListViewItem(subitem);
				li[i].ForeColor = bkc;
			}
			lvBu.Items.AddRange(li);
			txtTotalMoney.Text = totalMoney.ToString();

			txtTotalCount.Text = totalCount.ToString();
		}

		#endregion

		private void readChengWu()
		{
			init();

			string cardno = "";
			try
			{
                cardno = co.getCardno();
                
                //MessageBox.Show(cardno);
            }
			catch
			{
				MessageBox.Show(this, "初始化读卡器失败,读卡器未正确安装。", "核心错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			if (cardno == "error")
			{
				MessageBox.Show(this, "无法检测到卡片，请检查卡片是否放在读卡器上或读卡器是否正确连接。", "读卡错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			else
			{
				
				ci.CardNo = cardno;

				if (ci.getFullInfo() == true)
				{
                    //消费机号32个F才正确
                    string xfjh = "FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF";//co.getBlock(6);
					if (xfjh != "FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF")
					{
						//MessageBox.Show(xfjh);
						co.setBlock(6, "FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF");
					}

                    double iMoney = 100;//co.getMoney();
					if (iMoney != ci.Money)
					{
						//MessageBox.Show(this, "卡内金额与服务器数据不符。", "数据错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
						ci.Money = iMoney;
						txtMoney.BackColor = Color.Red;
					}
                    DateTime vDate = Convert.ToDateTime("2016-4-7"); //co.getValidDate();
					if (vDate != ci.NoUseDate)
					{
						//MessageBox.Show(this, "卡内有效期与服务器数据不符。", "数据错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
						ci.NoUseDate = vDate;
						txtValid.BackColor = Color.Red;
					}

					fillBaseInfo();
					int t = checkValid();
					if (t == 0)
					{
						MessageBox.Show(this, "有效期已经过期，请延续有效期，该操作将清零上月余额。", "数据错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
						isYanXu = true;
						return;
					}
					else if (t == 2)
					{
						MessageBox.Show(this, "有效期即将过期，您可以延续有效期，该操作将清零上月余额。", "数据错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						isYanXu = true;
					}
					else if (t == -1)
					{
						setNewValidOnly(s.getNewValidDate());
						init();
						return;
					}

					if (!ci.CustDept.Equals(operDept) && !ci.CustDept.Equals("管理人员"))
					{
						if (operDept == "新乡地区" && (ci.CustDept == "新乡运用" || ci.CustDept == "新南运用"))
						{
						}
						else
						{
							MessageBox.Show(this, "部门不一致，本卡不能在本机充值。", "数据错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
							init();
							return;
						}
					}

					//自动充值
					if (operAuto == true)
					{
						dt = s.getUnfinishedRecord(ci.Work_no,_place);
						if (!dt.TableName.Equals("unfinishedRecord"))
						{
							MessageBox.Show(this, "无法连接数据库服务器，获取报单信息错误。", "网络错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
						}
						else if (dt.Rows.Count == 0)
						{
							if (MessageBox.Show(this, "没有找到未领取的报单记录，是否显示所有数据。", "数据为空", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
							{
                                dt = s.getAllRecord(ci.Work_no, _place);
								isRead = false;
								FillListViewShow();
							}
							else
							{
								
								return;
							}
						}
						else
						{
							isRead = true;
							FillListViewShow();
						}
					}
					else//手动充值
					{
						
						
						if (fmm.ShowDialog(this) == System.Windows.Forms.DialogResult.Yes)
						{
							dt = fmm.dtResult;
							isRead = true;
							FillListViewShow();
						}
						else
						{
							isRead = false;
						}
					}
				}
				else
				{
					MessageBox.Show(this, "读卡失败，该卡片无效。", "读卡错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}


		private void readOther()
		{
			init();




			string cardno = "";
			try
			{
				cardno = co.getCardno();
			}
			catch
			{
				MessageBox.Show(this, "初始化读卡器失败,读卡器未正确安装。", "核心错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			if (cardno == "error")
			{
				MessageBox.Show(this, "无法检测到卡片，请检查卡片是否放在读卡器上或读卡器是否正确连接。", "读卡错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			else
			{


				ci.CardNo = cardno;

				if (ci.getFullInfo() == true)
				{

					double iMoney = co.getMoney();
					if (iMoney != ci.Money)
					{
						//MessageBox.Show(this, "卡内金额与服务器数据不符。", "数据错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
						ci.Money = iMoney;
						txtMoney.BackColor = Color.Red;
					}
					DateTime vDate = co.getValidDate();
					if (vDate != ci.NoUseDate)
					{
						//MessageBox.Show(this, "卡内有效期与服务器数据不符。", "数据错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
						ci.NoUseDate = vDate;
						txtValid.BackColor = Color.Red;
					}

					fillBaseInfo();
					int t = checkValid();
					if (t == 0)
					{
						MessageBox.Show(this, "有效期即将或已经过期，请点击延续按钮申请延长有效期。", "数据错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						isYanXu = true;
						return;
					}
					else if (t == -1)
					{
						setNewValidOnly(s.getNewValidDate());
						isYanXu = true;
						return;
					}


					dt = s.getOtherRecord(ci.Work_no);
					if (!dt.TableName.Equals("otherRecord"))
					{
						MessageBox.Show(this, "无法连接数据库服务器，获取报单信息错误。", "网络错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
					else if (dt.Rows.Count == 0)
					{
						if (MessageBox.Show(this, "没有找到未领取的补贴记录，是否显示所有数据。", "数据为空", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
						{
							dt = s.getAllOtherRecord(ci.Work_no);

							FillListViewBu();
						}
						else
						{
							return;
						}
					}
					else
					{
						isRead = true;
						FillListViewBu();
					}
				}
				else
				{
					MessageBox.Show(this, "读卡失败，该卡片无效。", "读卡错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}


		private void btnReadInfo_Click(object sender, EventArgs e)
		{
			//for (int i = 0; i < 16; i++)
			//{
			//    WriteLog(co.getBlock(i));
			//}
            ////int currKh = Convert.ToInt32( co.getCardno());


            //co.setBlock(6, "FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF");
            ////    lastKh = currKh;
            ////if (currKh == lastKh )
            ////{
            ////    MessageBox.Show("本卡与上一张卡号相同，请换卡");
            ////}
            ////else if (currKh == lastKh + 1 || currKh == lastKh - 1 || lastKh == 0)
            ////{
            ////    co.setBlock(6, "FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF");
            ////    lastKh = currKh;
            ////}
            ////else
            ////{
            ////    MessageBox.Show("本卡号" + currKh.ToString() + "与前一张" + lastKh.ToString() + "不连续，请检查");
            ////    co.setBlock(6, "FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF");
            ////    lastKh = currKh;
            ////}
            //return;
			//
			if (radioCheng.Checked == true)
			{
				readChengWu();
			}
			else
			{
				readOther();
			}
		}

		
		#region 初始化
		private void initBase()
		{
			ci = new CustomerInfo();
			dt = new DataTable();
			txtDept.Text = "";
			txtMoney.Text = "";
			txtMoney.BackColor = txtDept.BackColor;
			txtValid.BackColor = txtDept.BackColor;
			txtName.Text = "";
			txtNo.Text = "";
			txtTotalCount.Text = "";
			txtTotalMoney.Text = "";
			txtTotalTime.Text = "";
			txtValid.Text = "";
			txtLastMoney.Text = "";
			lvBaoDan.Items.Clear();
			lvBu.Items.Clear();
		}

		private void init()
		{
			DateTime now = s.getServerTime();
			initBase();
			dtpS.Value = Convert.ToDateTime(now.ToString("yyyy-MM") + "-" + clearDay.ToString());
			if (now.Day < clearDay)
			{
				dtpS.Value = dtpS.Value.AddMonths(-1);
			}

			dtpE.Value = now;
			isRead = false;
			isYanXu = false;
		}


		private void FrmMain_Load(object sender, EventArgs e)
		{
			s = new EcardService.Service();
			s.setPlace(_place);
			//byte[] bytes = Properties.Resources.Project2;
			//if (File.Exists(cPath)) File.Delete(cPath);
			//FileStream fs = new FileStream(cPath, FileMode.CreateNew);
			//fs.Write(bytes, 0, bytes.Length);
			//fs.Close();

			tsslOper.Text = "当前操作员：" + oper;
            tsslDept.Text = "当前车间：" + operDept;
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
			co = new CardOper();
			if (co.init() == false)
			{
				MessageBox.Show(this, "初始化读卡器失败，请检查读卡器是否正确连接。", Properties.Resources.strWriteFailed, MessageBoxButtons.OK, MessageBoxIcon.Error);
				canClose = true;
				Close();
			}
			
			init();
			switchListViewToBaoDan();
			radioCheng.Checked = true;
			radioOther.Checked = false;
		}
		#endregion

		private bool preWrite()
		{
            
			if (ci.Work_no == "" || isRead == false)
			{
				MessageBox.Show(this, "尚未读卡，请先读卡。", Properties.Resources.strWriteFailed , MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}

			if (!ci.CardNo.Equals(co.getCardno()))
			{
				MessageBox.Show(this, "当前信息与卡片内容不符，请先读卡。", Properties.Resources.strWriteFailed, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}
			return true;
		}

		private bool preWriteYan()
		{

			if (ci.Work_no == "" || isYanXu == false)
			{
				MessageBox.Show(this, "尚未读卡或当前不在可续期时间。", Properties.Resources.strWriteFailed, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}

			if (!ci.CardNo.Equals(co.getCardno()))
			{
				MessageBox.Show(this, "当前信息与卡片内容不符，请先读卡。", Properties.Resources.strWriteFailed, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}
			return true;
		}

		private bool preRead()
		{
			return true;
		}

		static public void listViewToDataTable(ListView lv, DataTable dt)
		{
			int i, j;
			DataRow dr;
			dt.Clear();
			dt.Columns.Clear();
			//生成DataTable列头
			for (i = 0; i < lv.Columns.Count; i++)
			{
				dt.Columns.Add(lv.Columns[i].Text.Trim(), typeof(String));
			}
			//每行内容
			for (i = 0; i < lv.Items.Count; i++)
			{
				dr = dt.NewRow();
				for (j = 0; j < lv.Columns.Count; j++)
				{
					dr[j] = lv.Items[i].SubItems[j].Text.Trim();
				}
				dt.Rows.Add(dr);
			}
		}


		private void btnWrite_Click(object sender, EventArgs e)
		{
			int flag;
			if (lvBaoDan.Visible == true)
			{
				flag = 1;
			}
			else
			{
				flag = 2;
			}

			if (flag == 1 && operAuto == false)
			//手动充值16小时内只能充值1次
			{
				if (s.isLastReceiveMoreThan16(ci.CardNo) == false)
				{
					MessageBox.Show(this, "距离上一次领取补贴不足16小时，不能再次领取。", "写卡失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					init();
					return;
				}
			}

			if (preWrite() == false)
			{
				return;
			}

            if (ci.hasUnReceivedMoney(flag, _place) == false && operAuto == true)
			{
				MessageBox.Show(this, "没有未写卡数据。", "写卡失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			double iCount = 0;
			try
			{
				iCount = Convert.ToDouble(txtTotalMoney.Text);
			}
			catch
			{
				MessageBox.Show(this, "充值金额计算错误，请重新读卡。", "写卡失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			string ids = "";
			int[] ids1 = new int[dt.Rows.Count];
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				if (i != 0) ids += ",";
				ids += dt.Rows[i]["id"].ToString();
				ids1[i] = Convert.ToInt32(dt.Rows[i]["id"]);
			}
			bool addResult = false;
			DataTable listview = new DataTable();
			if (lvBaoDan.Visible == true)
			{
				listViewToDataTable(lvBaoDan, listview);
				listview.TableName = "bd";
			}
			else
			{
				listViewToDataTable(lvBu, listview);
				listview.TableName = "bu";
			}


            addResult = s.addMoney(ci.CustomerId, ci.CardNo, ci.Work_no, ci.Work_name, ids, listview, Convert.ToDouble(txtTotalMoney.Text), Convert.ToDouble(txtTotalMoney.Text) + Convert.ToDouble(txtMoney.Text), ipAddr, oper, flag, _place);

			if (addResult == true)
			{
				if (co.addMoney(iCount) == true)
				{
					MessageBox.Show(this, "充值成功，本次充值金额" + txtTotalMoney.Text + "元。", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
					init();
		
					return;
				}
				else
				{
					MessageBox.Show(this, "充值失败，请确认卡片放在读卡器上。", "充值失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}
			}
			else
			{
				MessageBox.Show(this, "充值失败，数据库操作错误。", "充值失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
		}

		private void queryChengWu()
		{
			initBase();

			string cardno = "";
			try
			{
				cardno = co.getCardno();
			}
			catch
			{
				MessageBox.Show(this, "初始化读卡器失败,读卡器未正确安装。", "核心错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			if (cardno == "error")
			{
				MessageBox.Show(this, "无法检测到卡片，请检查卡片是否放在读卡器上或读卡器是否正确连接。", "读卡错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			else
			{


				ci.CardNo = cardno;

				if (ci.getFullInfo() == true)
				{

					double iMoney = co.getMoney();
					if (iMoney != ci.Money)
					{
						//MessageBox.Show(this, "卡内金额与服务器数据不符。", "数据错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
						ci.Money = iMoney;
						txtMoney.BackColor = Color.Red;
					}
					fillBaseInfo();

                    dt = s.getRecordByTime(ci.Work_no, dtpS.Value.ToString("yyyy-MM-dd"), dtpE.Value.AddDays(1).ToString("yyyy-MM-dd"), _place);
					
					if (!dt.TableName.Equals("recordByTime"))
					{
						MessageBox.Show(this, "无法连接数据库服务器，获取报单信息错误。", "网络错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
					else if (dt.Rows.Count == 0)
					{
						MessageBox.Show(this, "没有找到符合条件的报单记录。", "数据为空", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

					}
					else
					{
						FillListViewShow();
					}
				}
				else
				{
					MessageBox.Show(this, "读卡失败，该卡片无效。", "读卡错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		private void queryOther()
		{
			initBase();

			string cardno = "";
			try
			{
				cardno = co.getCardno();
			}
			catch
			{
				MessageBox.Show(this, "初始化读卡器失败,读卡器未正确安装。", "核心错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			if (cardno == "error")
			{
				MessageBox.Show(this, "无法检测到卡片，请检查卡片是否放在读卡器上或读卡器是否正确连接。", "读卡错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			else
			{


				ci.CardNo = cardno;

				if (ci.getFullInfo() == true)
				{

					double iMoney = co.getMoney();
					if (iMoney != ci.Money)
					{
						//MessageBox.Show(this, "卡内金额与服务器数据不符。", "数据错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
						ci.Money = iMoney;
						txtMoney.BackColor = Color.Red;
					}
					fillBaseInfo();

					dt = s.getOtherRecordByTime(ci.Work_no, dtpS.Value.ToString("yyyy-MM-dd"), dtpE.Value.AddDays(1).ToString("yyyy-MM-dd"));
					if (!dt.TableName.Equals("otherRecordByTime"))
					{
						MessageBox.Show(this, "无法连接数据库服务器，获取补贴信息错误。", "网络错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
					else if (dt.Rows.Count == 0)
					{
						MessageBox.Show(this, "没有找到符合条件的报单记录。", "数据为空", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

					}
					else
					{
						FillListViewBu();
					}
				}
				else
				{
					MessageBox.Show(this, "读卡失败，该卡片无效。", "读卡错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		private void btnQuery_Click(object sender, EventArgs e)
		{

			if (radioCheng.Checked)
			{
				queryChengWu();
			}
			else
			{
				queryOther();
			}
			ci.Work_no = "";
			isRead = false;
		}

		private void btnClear_Click(object sender, EventArgs e)
		{
			//init();
            //co.setValidDate(Convert.ToDateTime("2014-5-7"));
			co.addMoney(26.7);
			//co.minusMoney(2);
			
			
		}

		private int checkValid()
		{

			DateTime now = s.getServerTime();
			return checkValid(now);
		}

		private int checkValid(DateTime now)
		{
			if (ci.NoUseDate == Convert.ToDateTime("1900-1-1"))
			{
				return -1;
			}
			if (now > Convert.ToDateTime(ci.NoUseDate.ToString("yyyy-MM") + "-5"))//大于5号
			{
				return 0;//需要更新有效期
			}
			else if ( now > Convert.ToDateTime(ci.NoUseDate.ToString("yyyy-MM") + "-1") )
			{
				return 2;//提示更新有效期
			}
			else if (now < Convert.ToDateTime(ci.NoUseDate.ToString("yyyy-MM") + "-1").AddMonths(-1))//现在的日期小于有效期前一个月，如现在2013-2-4，有效期是2013-4-5日
			{
				return -1;//有效期异常
			}
			else
			{
				return 1;//有效期正常
			}


		}

		private bool setNewValidOnly(DateTime newValidDate)
		{

			//MessageBox.Show(this, "根据管理规定，该卡有效期有误，点击确定重新设置有效期，该操作不会清楚卡内余额。", "延续有效期", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			if (co.setValidDate(newValidDate) == false)
			{
				MessageBox.Show(this, "写卡错误，写入新有效期失败，请联系管理员！", "操作错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}
			else
			{
				if (s.setValidDateOnly(ci.Work_no, newValidDate) == false)
				{
					MessageBox.Show(this, "写入数据库时发生错误，请联系管理员！", "操作错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return false;
				}
				else
				{
					MessageBox.Show(this, "重新设置有效期成功，请重新读卡，现在该卡有效期到" + newValidDate.AddDays(-2).ToString("yyyy-MM-dd"), "操作成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
					ci.NoUseDate = newValidDate;
					return true;
				}
			}

		}

		private void switchListViewToBaoDan()
		{
			lvBaoDan.Visible = true;
				lvBaoDan.Dock = DockStyle.Fill;
				lvBu.Visible = false;
				lvBu.Dock = DockStyle.None;
				grpList.Text = "报单信息：";
		

		}

		private void switchListViewToBuTie()
		{

			lvBu.Dock = DockStyle.Fill;
			lvBu.Visible = true;
			lvBaoDan.Visible = false;
			lvBaoDan.Dock = DockStyle.None;
			grpList.Text = "补贴信息：";
		}

		private void btnXu_Click(object sender, EventArgs e)
		{
			if (preWriteYan() == false)
			{
				return;
			}


			DateTime now = s.getServerTime();


			DateTime newValidDate = s.getNewValidDate();
			int cv = checkValid(now);
			if ( cv == 0 || cv==2)
			{
				double cMoney = Convert.ToDouble(txtLastMoney.Text);
				if ( cMoney==0 || MessageBox.Show(this, "根据管理规定，该操作将清除上月余额" + txtLastMoney.Text + "元，是否确定要进行延期。", "确认操作", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
				{
					if (cMoney == 0 || MessageBox.Show(this, "本月5日24点之前都可以执行该延期操作，是否真的确定要进行延期。", "再次确认", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
					{
						if (cMoney == 0 || MessageBox.Show(this, "即将清除上月余额" + txtLastMoney.Text + "元，该操作不可撤销，请慎重考虑后最后确认是否执行。", "最后确认", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
						{
							MessageBox.Show(this, "在弹出续期成功提示以前严禁把餐卡从读卡器移开！", "严重警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
							if (cMoney != 0)
							{
								if (co.minusMoney(cMoney) == false)
								{
									MessageBox.Show(this, "写卡错误，清除卡内余额失败，请联系管理员！", "操作错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
									init();
									return;
								}
							}
							if (co.setValidDate(newValidDate) == false)
							{
								MessageBox.Show(this, "写卡错误，写入新有效期失败，请联系管理员！", "操作错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
								init();
								return;
							}

							if (s.setValidDate(ci.Work_no, ci.NoUseDate, cMoney, Convert.ToDouble(txtMoney.Text) - cMoney, ci.CustomerId, ci.CardNo, ipAddr, oper, _place) != 1)
							{
								MessageBox.Show(this, "写入数据库时发生错误，请联系管理员！", "操作错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
								init();
								return;
							}

							MessageBox.Show(this, "延续有效期成功，现在该卡有效期到！" + newValidDate.AddDays(-2).ToString("yyyy-MM-dd"), "操作成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
						}
					}
				}
				init();
			}
			else
			{
				isYanXu = false;
				MessageBox.Show(this, "该卡目前不需要延续有效期！", "延续有效期", MessageBoxButtons.OK, MessageBoxIcon.Information);

			}
		}

		private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (canClose == true)
			{
				e.Cancel = false;
				return;
			}
			if (MessageBox.Show(this, "确定要关闭本程序吗?", "关闭程序", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
			{
				e.Cancel = true;
			}
		}

		private void btnBu_Click(object sender, EventArgs e)
		{
			
		}

		private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
		{
			//if (File.Exists(cPath)) File.Delete(cPath);

		}

		private void grpTotal_Enter(object sender, EventArgs e)
		{

		}

		private void radioCheng_CheckedChanged(object sender, EventArgs e)
		{

		}
	}
}