using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace EcardMonitor
{

	public partial class frmBu : Form
	{
		const int maxMoney = 20;
		const int minMoney = 1;
        bool doing = false;

		public frmBu()
		{
			InitializeComponent();
		}

        public void setPbFen(int min , int max)
        {
            pbFen.Minimum = min;
            pbFen.Maximum = max;
        }
        public void setPbFen(int val)
        {
            pbFen.Value = val;
        }

        private void writeLog(string text)
        {
            txtLog.AppendText(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " ：  " + text + "\r\n");
        }

		private void btnStart_Click(object sender, EventArgs e)
        {
            if (doing) return;
            doing = true;
            dal d = new dal();
            DataTable dt = d.getDataToFill();
            pbAll.Maximum = dt.Rows.Count + 1;
            pbAll.Minimum = 1;
            for(int i = 0; i < dt.Rows.Count; i++)
			{
                lblProgressAll.Text = "总进度：" + (i + 1).ToString() + "/" + dt.Rows.Count.ToString();
                
                int posId = Convert.ToInt32(dt.Rows[i]["jh"]);
                int buid = Convert.ToInt32(dt.Rows[i]["id"]);
                double money = Convert.ToDouble(dt.Rows[i]["je"]);
                DateTime sDate = Convert.ToDateTime(dt.Rows[i]["ks"]);
                DateTime eDate = Convert.ToDateTime(dt.Rows[i]["js"]);
                lblProgressAll.Text += "，当前pos机号：" + posId.ToString() + "，当前月份：" + eDate.ToString("yyyy年MM月");
                pbAll.Value = i + 1;
                Application.DoEvents();
                double buMoney = bu(posId, money, sDate, eDate,buid);
                if (buMoney == -1)
                {
                    writeLog(posId.ToString() + "号机" + sDate.ToString("yyyy-MM-dd HH:mm:ss") + "到" + eDate.ToString("yyyy-MM-dd HH:mm:ss") + "补数据失败。");

                }
                else
                {
                    writeLog(posId.ToString() + "号机" + sDate.ToString("yyyy-MM-dd HH:mm:ss") + "到" + eDate.ToString("yyyy-MM-dd HH:mm:ss") + "补数据成功，共补入：" + buMoney.ToString() + "元。");

                }
			}
            doing = false;
            MessageBox.Show("数据全部补充完毕。");
            #region 旧的单独pos机补数据代码
            /*
            int posId;
            double money;
            DateTime sDate, eDate;
            try
            {
                posId = Convert.ToInt32(txtPosId.Text);
            }
            catch
            {
                MessageBox.Show("pos机号格式不正确。");
                txtPosId.SelectAll();
                txtPosId.Focus();
                return;
            }
            try
            {
                money = Convert.ToDouble(txtMoney.Text);
            }
            catch
            {
                MessageBox.Show("金额格式不正确。");
                txtMoney.SelectAll();
                txtMoney.Focus();
                return;
            }
            sDate = dtpS.Value;
            eDate = dtpE.Value;
            dal d = new dal();
            //步骤1：初始化该pos机上消费的人员
            List<work> works = d.getWorksByPosId(posId);
            pos p = new pos(posId);
            p.setWorks(works);
            List<rec> records = new List<rec>();
            //步骤2：生成要补充的记录的时间、金额、人员
            setRecords(ref records, p, money, sDate, eDate);
            //步骤3：把要补充的记录插入数据库
            dgv.Rows.Clear();
            int i = 0;
            foreach (rec record in records)
            {
                int index = dgv.Rows.Add();

                dgv.Rows[index].Cells[0].Value = (++i).ToString();
                dgv.Rows[index].Cells[1].Value = record.xfry.rygh;
                dgv.Rows[index].Cells[2].Value = record.xfsj;
                dgv.Rows[index].Cells[3].Value = record.money;
            }
            if (d.insertRecords(records, p) == true)
            {
                MessageBox.Show("补记录成功。");
            }
            else
            {
                MessageBox.Show("补记录失败。");
            }
            */
            #endregion
        }

        private double bu(int posId , double money,DateTime sDate, DateTime eDate,int buid)
        {

            dal d = new dal();
            //步骤1：初始化该pos机上消费的人员
            lblFen.Text = "当前进度：初始化该pos机上消费的人员。";
            List<work> works = d.getWorksByPosId(posId);
            pos p = new pos(posId);
            p.setWorks(works);
            List<rec> records = new List<rec>();
            //步骤2：生成要补充的记录的时间、金额、人员
            lblFen.Text = "当前进度：生成要补充的记录的时间、金额、人员。";
            Application.DoEvents();
            double trueMoney = setRecords(ref records, p, money, sDate, eDate);
            //步骤3：把要补充的记录插入数据库
            dgv.Rows.Clear();
            int i = 0;
            foreach (rec record in records)
            {
                int index = dgv.Rows.Add();

                dgv.Rows[index].Cells[0].Value = (++i).ToString();
                dgv.Rows[index].Cells[1].Value = record.xfry.rygh;
                dgv.Rows[index].Cells[2].Value = record.xfsj;
                dgv.Rows[index].Cells[3].Value = record.money;
                
                Application.DoEvents();
            }
            lblFen.Text = "当前进度：将生成的记录插入数据库。";
            setPbFen(1, records.Count);
            if (d.insertRecords(records, p, buid) == true)
            {
                //MessageBox.Show("补记录成功。");
                return trueMoney;
            }
            else
            {
                //MessageBox.Show("补记录失败。");
                return -1;
            }
        }

		private double setRecords(ref List<rec> records ,pos p,double money,DateTime sDate,DateTime eDate)
		{
			Random r = new Random();
			int startMinute;
			TimeSpan ts = eDate - sDate;
            dal d = new dal();
            pbFen.Value = 1;
            double hadMoney = 0;// d.getHadMoney(p.posId, sDate, eDate);
            money = money - hadMoney;
            double trueMoney = money;
            if (money <= 0) return -1;
            pbFen.Maximum = 1000;
            int progress = 1;
			while (money > maxMoney)
			{
                Application.DoEvents();
                pbFen.Value = progress++;
                if (progress >= 1000) progress = 1;
                Application.DoEvents();
				rec record = new rec();
				int i = r.Next(1, 3);
				switch (i)
				{
					case 1 :
						startMinute = 360;//6:00
						break;
					case 2:
						startMinute = 660;//11:00
						break;
					case 3:
						startMinute = 1020;//17:00;
						break;
					default:
						startMinute = 0;
						break;
				}
				startMinute += r.Next(180);//6:00-9:00,11:00-14:00,17:00-20:00

				
				if (i == 1)//早餐时间
				{
					//早餐3-6元
					record.money = r.Next(3, 6);
				}
				else//午餐和晚餐时间
				{
					//午餐和晚餐5-20元
					i = r.Next(1,100);
					if (1 <= i && i <= 40)
					{
						record.money = r.Next(5, 8);
					}
					else if (41 <= i && i <= 80)
					{
						record.money = r.Next(9, 12);
					}
					else if (81 <= i && i <= 90)
					{
						record.money = r.Next(13, 16);
					}
					else
					{
						record.money = r.Next(17, 20);
					}
				}
				record.xfsj = sDate.AddDays(r.Next(ts.Days)).AddMinutes(startMinute);
				record.xfry = p.getRandomWork();
				records.Add(record);
				money -= record.money;
			}
			rec lastRecord = new rec();
			lastRecord.money = money;
			lastRecord.xfry = p.getRandomWork();
			lastRecord.xfsj = sDate.AddDays(r.Next(ts.Days)).AddMinutes(r.Next(360, 1200));
			records.Add(lastRecord);
            return trueMoney;
		}
	}
	public struct rec
	{
		public DateTime xfsj;
		public double money;
		public work xfry;
	}
	public class work
	{
		public string rygh { get; set; }
		public string rfkh { get; set; }
		public int ryid { get; set; }
	}

	public class dal
	{
		SqlConnection conn;
		public dal()
		{
			conn = new SqlConnection("Data Source=120.194.214.53;User ID=icsoft;Password=ecard2013;Initial Catalog=icsoft_newGPRS;Persist Security Info=True");
            //conn = new SqlConnection("Data Source=.;User ID=icsoft;Password=ecard2013;Initial Catalog=icsoft_newGPRS;Persist Security Info=True");
		}

        public DataTable getDataToFill()
        {
            
            SqlCommand comm = new SqlCommand();

            comm.Connection = conn;
            comm.CommandText = "select id,jh,je,ks,js from bushuju where isok = 0 order by id desc";
            comm.Parameters.Clear();
            
            SqlDataAdapter sda = new SqlDataAdapter(comm);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count == 0) return null;
            return dt;
        }

		public List<work> getWorksByPosId(int id)
		{
			SqlCommand comm = new SqlCommand();

			comm.Connection = conn;
            //comm.CommandText = "select ryid,rybh,rfkh from ryxx where rybh in (select distinct rybh from lssj,ryxx where lssj.rfkh=ryxx.rfkh and lssj.xfjh=@posid)";
            comm.CommandText = "select ryid,rybh,rfkh from icsoft.t_jh_ry where jh=@posid";
			comm.Parameters.Clear();
			comm.Parameters.AddWithValue("posid", id);
			SqlDataAdapter sda = new SqlDataAdapter(comm);
			DataTable dt = new DataTable();
			sda.Fill(dt);
			if (dt.Rows.Count == 0) return null;
			List<work> works = new List<work>();
            frmBu fm = (frmBu)Application.OpenForms["frmBu"];
            fm.setPbFen(1, dt.Rows.Count);
			for (int i = 0; i < dt.Rows.Count; i++)
			{
                
                if (dt.Rows[i]["rybh"].ToString().Length > 4) continue;
				work w = new work();
				w.rfkh = dt.Rows[i]["rfkh"].ToString();
				w.rygh = dt.Rows[i]["rybh"].ToString();
				w.ryid = Convert.ToInt32(dt.Rows[i]["ryid"]);
				works.Add(w);
                fm.setPbFen(i + 1);
                Application.DoEvents();
			}
			return works;
		}

		public bool insertRecords(List<rec> records, pos p,int buId)
		{
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.CommandText = "insert into lssj_bu(ryid,rfkh,xfjh,xfje,rfye,xfsj,xffs,xfzl,sky) values(@ryid,@rfkh,@xfjh,@xfje,0,@xfsj,'补充消费','消费','管理员')";
			conn.Open();
			SqlTransaction tran = conn.BeginTransaction();
			comm.Transaction = tran;
            frmBu fm = (frmBu)Application.OpenForms["frmBu"];
            int i = 1;
			foreach (rec record in records)
			{
				comm.Parameters.Clear();
				comm.Parameters.AddWithValue("ryid", record.xfry.ryid);
				comm.Parameters.AddWithValue("rfkh", record.xfry.rfkh);
				comm.Parameters.AddWithValue("xfje", record.money);
				comm.Parameters.AddWithValue("xfsj", record.xfsj);
				comm.Parameters.AddWithValue("xfjh", p.posId);
				try
				{
                    Application.DoEvents();
					comm.ExecuteNonQuery();
                    fm.setPbFen(i++);
                    
				}
				catch
				{
					tran.Rollback();
					conn.Close();
					return false;
				}
				finally
				{
					
				}
			}
            comm.CommandText = "update bushuju set isok = 1 where id = @id";
            comm.Parameters.Clear();
            comm.Parameters.AddWithValue("id", buId);
            try
            {
                Application.DoEvents();
                comm.ExecuteNonQuery();
            }
            catch
            {
                tran.Rollback();
                conn.Close();
                return false;
            }
            finally
            {

            }

			tran.Commit();
			conn.Close();



			return true;
		}

        internal double getHadMoney(int jh, DateTime sDate, DateTime eDate)
        {
            SqlCommand comm = new SqlCommand();
            comm.Connection = conn;
            comm.CommandText = "select sum(xfje) from v_xfjl where jh=@jh and xfsj>@kssj and xfsj<@jssj";
            comm.Parameters.Clear();
            comm.Parameters.AddWithValue("kssj", sDate);
            comm.Parameters.AddWithValue("jssj", eDate);
            comm.Parameters.AddWithValue("jh", jh);
            double money = -1;
            try
            {
                conn.Open();
                money = Convert.ToDouble(comm.ExecuteScalar());
            }
            catch
            {
                return -1;
            }
            finally
            {
                conn.Close();
            }
            return money;
        }
    }

	public class pos
	{
		int id;
		List<work> works;
		public pos(int _id)
		{
			id = _id;
			works = null;
		}
		public int posId
		{
			get { return this.id;}
			set { this.id = value; }
		}
		public void setWorks(List<work> works)
		{
			this.works = works;
		}
		public work getRandomWork()
		{
			if (works.Count == 0) return null;
			byte[] bytes = new byte[4];
			new System.Security.Cryptography.RNGCryptoServiceProvider().GetBytes(bytes);

			Random r = new Random(BitConverter.ToInt32(bytes,0));
			int i = r.Next(1, works.Count);
			return works[i];
		}
	}
}
