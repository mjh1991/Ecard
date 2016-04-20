using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data;
using System.Data.SqlClient;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class Service : System.Web.Services.WebService
{
	private static string eConnStr = System.Configuration.ConfigurationManager.ConnectionStrings["ecardConnectionString"].ConnectionString;
	private static string pbConnStr;// = System.Configuration.ConfigurationManager.ConnectionStrings["xPBConnectionString"].ConnectionString;


	private static int ValidDay = 7; //5;
	private static int clearDay = 25;//卡金额清零的日期


	private static double assTime = 1;
	private static double breakfast = 2.0;
	private static double lunch = 7.0;
	private static double supper = 7.0;
	private static double bs = 6.0;
	private static double be = 8.99;
	private static double ls = 11.0;
	private static double le = 13.99;
	private static double ss = 18.0;
	private static double se = 20.99;



	public Service()
	{
		//setPlace(0);
	}





	[WebMethod]
	public void setPlace(int place)
	{
		if (place == 0) //新乡
		{
			pbConnStr = System.Configuration.ConfigurationManager.ConnectionStrings["xPBConnectionString"].ConnectionString;
		}
		else // if (place == 1) //月山
		{
			pbConnStr = System.Configuration.ConfigurationManager.ConnectionStrings["yPBConnectionString"].ConnectionString;
		}
	}

	[WebMethod]
	public double getWorkTime(DateTime go, DateTime back, int pos, int flag)
	{
		TimeSpan ts = back - go;
		if (flag == 2)//货车
		{
			return  Math.Truncate( ts.TotalHours * 100) /100.0  + assTime;
		}
		else
		{
			return Math.Truncate(ts.TotalHours * 100) / 100.0;
		}
	}

	[WebMethod]
	public double getWorkMoney(DateTime go, DateTime back, int pos, int flag, int ryCount)
	{
		TimeSpan ts = back - go;
		double workMoney = 0.0;
		if (flag == 1)//客车或者途调5275
		{
			int allTime = Convert.ToInt32(Math.Truncate(ts.TotalHours));
			workMoney = (allTime / 24) * (breakfast + lunch + supper);
			double g = go.Hour + go.Minute / 60.0;
			double b = back.Hour + back.Minute / 60.0;
			if (g > b)
			{
				if (g <= be) //9点钱开车
				{
					if (b < bs)//如 8点开，4点到
					{
						//+1早1午1晚
						workMoney += (breakfast + lunch + supper);
					}
					else // bs < b < be 如8点开7点到
					{
						//+2早1午1晚
						workMoney += (2 * breakfast + lunch + supper);
					}
				}
				else if (g <= le) //14点前开车
				{
					if (b < bs) //如 13点开 4点到
					{
						//+1午1晚
						workMoney += (lunch + supper);
					}
					else if (b < ls) // 如13点开10点到
					{
						//+1午1晚1早
						workMoney += (breakfast + lunch + supper);
					}
					else // 如13点开 12点到
					{
						//+2午1晚1早
						workMoney += (breakfast + 2 * lunch + supper);
					}
				}
				else if (g <= se) //21点前开车
				{
					if (b < bs) //如 20点开 4点到
					{
						//+1晚
						workMoney += (supper);
					}
					else if (b < ls) // 如20点开10点到
					{
						//+1晚1早
						workMoney += (breakfast + supper);
					}
					else if (b < ss) // 如20点开 16点到
					{
						//+1晚1早1午
						workMoney += (breakfast + lunch + supper);
					}
					else //如20点开19点到
					{
						//+2晚1早1午
						workMoney += (breakfast + lunch + 2 * supper);
					}
				}
				else // g> se 21点后开车
				{
					if (b < bs) //如 22点开 4点到
					{
						//啥也不加
						workMoney += 0;
					}
					else if (b < ls) // 如22点开10点到
					{
						//1早
						workMoney += (breakfast);
					}
					else if (b < ss) // 如22点开 16点到
					{
						//+1早1午
						workMoney += (breakfast + lunch);
					}
					else //如22点开19点到
					{
						//+1晚1早1午
						workMoney += (breakfast + lunch + supper);
					}
				}
			}
			else if (g < b)
			{
				if (g > se) //21点后开车
				{
					//啥也不加 如21点开23点到
				}
				else if (g > le) //14点后开车
				{
					if (b < ss) //如15点开17点到
					{
						//啥也不加
					}
					else //如15点开20点或22点到
					{
						//加1晚
						workMoney += supper;
					}


				}
				else if (g > be) //9点后开车
				{
					if (b < ls) //如9点半开10点到
					{
						//啥也不加
					}
					else if (b < ss) //如9点半开12点到或15点到
					{
						//加1午
						workMoney += lunch;
					}
					else //如9点半开20点到或22点到
					{
						//加1午1晚
						workMoney += lunch + supper;
					}
				}
				else// if (g > bs) //9点之前开车
				{
					if (b < bs) //如3点开4点到，因为大前提g < b ，所以这里b < ss，g更<ss
					{
						//啥也不加
					}
					else if (b < ls) //如3点开10点到
					{
						//+1早
						workMoney += breakfast;
					}
					else if (b < ss) //如3点开16点到
					{
						//+1早1午
						workMoney += breakfast + lunch;
					}
					else //如3点到20点或 22点
					{
						workMoney += (breakfast + lunch + supper);
					}
				}

			}
			else //g=b
			{
				//正好都算到整除里了，啥也不加
			}
		}
		else if (flag == 2 || flag == 7)//货车
		{
			int allTime = Convert.ToInt32(Math.Truncate(ts.TotalHours + assTime));
			if (allTime < 8)
			{
				workMoney = breakfast;
			}
			else if (allTime >= 8 && allTime < 16)
			{
				workMoney = lunch;
			}
			else if (allTime >= 16 && allTime < 24)
			{
				workMoney = breakfast + lunch;
			}
			else
			{
				workMoney = (allTime / 24) * (breakfast + lunch + supper);
				switch ((allTime % 24) / 8)
				{
					case 0:
						break;
					case 1:
						workMoney += breakfast;
						break;
					case 2:
						workMoney += breakfast + lunch;
						break;
				}
			}
		}
		else if (flag == 3) //调车
		{
			workMoney = 5;//不论一班2人或3人，均按每人5元标准发补助
			//switch (ryCount)
			//{
			//    case 2:
			//        workMoney = 5;
			//        break;
			//    case 3:
			//        if (pos == 1)
			//        {
			//            workMoney = 4;
			//        }
			//        else
			//        {
			//            workMoney = 3;
			//        }
			//        break;
			//    case 4:
			//        workMoney = 2.5;
			//        break;
			//    case 5:
			//        workMoney = 2;
			//        break;
			//    default:
			//        workMoney = 0;
			//        break;
			//}
		}
		else if (flag == 6) //北调，途调给7块
		{
			workMoney = 7;
		}
		else if (flag == 4)//半趟小辅修临修
		{
			workMoney = 5;
		}
		else //if (flag == 5)//整趟小辅修临修
		{
			workMoney = 10;
		}
		return workMoney;
	}

	[WebMethod]
	public DateTime getBackTime(string r1, string r2, string a1, string a2, int flag, int pos)
	{
		DateTime aTime, bTime;
		if (1==1)//不管客货车，都按照到站点算//if (flag == 1)//客车 
		{

			if (pos == 3)//3位学员
			{
				if (r2 == null || r2 == "")//跟司机一起回来
				{
					aTime = Convert.ToDateTime(a1);
					bTime = Convert.ToDateTime(r1);
				}
				else //自己单独回来
				{
					aTime = Convert.ToDateTime(a2);
					bTime = Convert.ToDateTime(r2);
				}
			}
			else //1,2,4位司机
			{
				aTime = Convert.ToDateTime(a1);
				bTime = Convert.ToDateTime(r1);
			}
			if (aTime > Convert.ToDateTime(bTime.ToString("HH:mm:ss")))
			{
				bTime = Convert.ToDateTime(bTime.AddDays(-1).ToString("yyyy-MM-dd") + " " + aTime.ToString("HH:mm:ss"));
			}
			else
			{
				bTime = Convert.ToDateTime(bTime.ToString("yyyy-MM-dd") + " " + aTime.ToString("HH:mm:ss"));
			}
		}
		else //货车、调车 //实际上else后面不可能执行到
		{
			if (pos == 3)//3位学员
			{
				if (r2 == null || r2 == "")//跟司机一起回来
				{
					bTime = Convert.ToDateTime(r1);
				}
				else //自己单独回来
				{
					bTime = Convert.ToDateTime(r2);
				}
			}
			else//1,2,4位司机
			{
				bTime = Convert.ToDateTime(r1);
			}
		}
		return bTime;
	}


	[WebMethod]
	public double getMoneyThisMonth(string workno)
	{
		DateTime now = getServerTime();
		DateTime dt = Convert.ToDateTime(now.ToString("yyyy-MM") + "-" + clearDay.ToString() + " 18:00:00" );
		//if ( now.Day <clearDay)
		//{
			dt = dt.AddMonths(-1);
		//}
		SqlConnection conn = new SqlConnection(eConnStr);
		SqlCommand comm = new SqlCommand();

		comm.Connection = conn;
		comm.CommandText = "select sum(je) from T_AddMoney where rybh=@workno and arrive_time > @arrive_time";
		comm.Parameters.Clear();
		comm.Parameters.AddWithValue("workno", workno);
		comm.Parameters.AddWithValue("arrive_time", dt);
		try
		{
			conn.Open();
			return Convert.ToDouble(comm.ExecuteScalar());
		}
		catch
		{
			return 0;
		}
		finally
		{
			conn.Close();
		}
		
	}


    [WebMethod]
    public bool hasUnReceivedMoney(string workno,int flag,int _place)
    {
        setPlace(_place);
		SqlConnection conn = new SqlConnection();
		string str;
		if (flag == 1)//报单补贴
		{
			str = "select count(*) from V_Ecard_Details where work_no='" + workno + "' and isreceived=0";
			conn.ConnectionString = pbConnStr;
		}
		else//其他补贴
		{
			str = "select count(*) from  V_Ecard_Other where work_no='" + workno + "' and isreceived=0";
			conn.ConnectionString = eConnStr;
		}
        SqlCommand comm = new SqlCommand( str,conn);
        try
        {
            conn.Open();
            
            if (Convert.ToInt32(comm.ExecuteScalar()) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch
        {
            return false;
        }
        finally

        {
            conn.Close();
        }
    }
	[WebMethod]
	public bool isLastReceiveMoreThan16(string kh)
	{
		SqlConnection conn = new SqlConnection(eConnStr);
		SqlCommand comm = new SqlCommand();
		comm.Connection = conn;
		comm.CommandText = "select xfsj from lssj where rfkh=@kh and xffs='补贴充值' order by xfsj desc";
		comm.Parameters.Clear();
		comm.Parameters.AddWithValue("kh", kh);
		conn.Open();
		DateTime dt = Convert.ToDateTime( comm.ExecuteScalar());
		TimeSpan ts =  DateTime.Now - dt;
		if (ts.TotalHours > 16) return true;
		else return false;
	}

	[WebMethod]
	public bool updatePasswd(string sqlstr)
	{
		SqlConnection conn = new SqlConnection(eConnStr);
		conn.Open();
		SqlCommand comm = new SqlCommand(sqlstr, conn);
		if (comm.ExecuteNonQuery() > 0)
		{
			conn.Close();
			return true;
		}
		else
		{
			conn.Close();
			return false;
		}

	}


	[WebMethod]
	public string LoginIP(string ipAddr)
	{
				SqlConnection conn = new SqlConnection(eConnStr);
		SqlDataAdapter sda = new SqlDataAdapter("select  username from v_qx_ip where memo='一般用户' and IpAddr='" + ipAddr + "'", conn);
		DataTable dt = new DataTable();
		sda.Fill(dt);
		if (dt.Rows.Count != 1)
		{
			return "err!";
		}
		else
		{
			return dt.Rows[0]["username"].ToString();	
		}
		
	}

	[WebMethod]
	public bool Login(string user, string pass)
	{
		SqlConnection conn = new SqlConnection(eConnStr);
		SqlDataAdapter sda = new SqlDataAdapter("select  password from qx where memo='一般用户' and username='" + user + "'", conn);
		DataTable dt = new DataTable();
		sda.Fill(dt);
		if (dt.Rows.Count != 1)
		{
			return false;
		}
		else
		{
			if (!pass.Equals(dt.Rows[0]["password"].ToString()))
			{
				return false;
			}
		}
		return true;
	}



	[WebMethod]
	public int LoginWeb(string user, string pass)
	{
		int result = 0;
		SqlConnection conn = new SqlConnection(eConnStr);
		SqlDataAdapter sda = new SqlDataAdapter("select  password,memo from qx where username='" + user + "'", conn);
		DataTable dt = new DataTable();
		sda.Fill(dt);
		if (dt.Rows.Count != 1)
		{
			return -1;
		}
		else
		{
			if (!pass.Equals(dt.Rows[0]["password"].ToString()))
			{
				return -1;
			}
		}
		switch (dt.Rows[0]["memo"].ToString().Trim())
		{
			case "管理员":
				result = 1;
				break;
			case "超级用户":
				result = 2;
				break;
			case "一般用户":
				result = 3;
				break;
			case "查询用户":
				result = 4;
				break;
			default:
			
				result = -1;
				break;
		}
		return result;
	}

    [WebMethod]
    public string getUserDept(string user)
    {
        SqlConnection conn = new SqlConnection(eConnStr);
        SqlCommand sdr = new SqlCommand("select dept from qx where username='" + user + "'", conn);
        try
        {
            conn.Open();
            return sdr.ExecuteScalar().ToString();
        }
        finally
        {
            conn.Close();
        }
    }

	[WebMethod]
	public DataTable getJhByGroup(string group)
	{
		SqlConnection conn = new SqlConnection(eConnStr);
		SqlDataAdapter sda = new SqlDataAdapter("SELECT jh,wz from v_jhsz  where (zm='" + group + "' or 'All'='" + group + "') order by jh", conn);
		DataTable dt = new DataTable();
		try
		{
			sda.Fill(dt);
			dt.TableName = "getJhByGroup";
		}
		catch
		{
			dt.TableName = "err";
		}
		return dt;
	}

	[WebMethod]
	public DataTable getAllRecordByJh(string jh)
	{
		SqlConnection conn = new SqlConnection(eConnStr);
		SqlDataAdapter sda = new SqlDataAdapter("SELECT top 100  rybh, ryxm AS sjxm, xfje as lqje, SUBSTRING(CONVERT(nvarchar(20), xfsj, 120), 1, 10) AS lqrq, SUBSTRING(CONVERT(nvarchar(20),  xfsj, 120), 12, 5) AS lqsj,  dept,xfdd from v_xfjl where (jh= " + jh + " or 0=" + jh + ")  order by xfsj desc", conn);
		DataTable dt = new DataTable();
		try
		{
			sda.Fill(dt);
			dt.TableName = "getAllJh";
		}
		catch
		{
			dt.TableName = "err";
		}
		return dt;
	}

	[WebMethod]
	public string getUserGroup(string user)
	{
		SqlConnection conn = new SqlConnection(eConnStr);
		SqlCommand sdr = new SqlCommand("select groups from qx where username='" + user + "'", conn);
		try
		{
			conn.Open();
			return sdr.ExecuteScalar().ToString();
		}
		finally
		{
			conn.Close();
		}
	}


    [WebMethod]
    public bool getAuto(string user)
    {
        SqlConnection conn = new SqlConnection(eConnStr);
        SqlCommand sdr = new SqlCommand("select isAuto from qx where username='" + user + "'", conn);
        try
        {
            conn.Open();
            int a = Convert.ToInt32(sdr.ExecuteScalar());
            return (a == 1?true:false);
        }
        finally
        {
            conn.Close();
        }
    }

	[WebMethod]
	public int getPlace(string user)
	{
		SqlConnection conn = new SqlConnection(eConnStr);
		SqlCommand sdr = new SqlCommand("select place from qx where username='" + user + "'", conn);
		try
		{
			conn.Open();
			int a = Convert.ToInt32(sdr.ExecuteScalar());
			return a;
		}
		finally
		{
			conn.Close();
		}
	}

	[WebMethod]
	public DataTable getCZJL(string startDate, string endDate, string dept)
	{
		SqlConnection conn = new SqlConnection(eConnStr);
		SqlDataAdapter sda = new SqlDataAdapter("SELECT     '新乡机务段' AS xxjwd, rybh, ryxm AS sjxm, xfje as lqje, SUBSTRING(CONVERT(nvarchar(20), xfsj, 120), 1, 10) AS lqrq, open_time AS lqsj, username as oper, dept,roadway,arrive_time,xfsj as addtime FROM V_CZJL where xfsj >=@kssj and xfsj<@jssj and (dept=@dept or @dept='All') order by xfsj", conn);
		sda.SelectCommand.Parameters.Clear();
		sda.SelectCommand.Parameters.AddWithValue("kssj", Convert.ToDateTime(startDate));
		sda.SelectCommand.Parameters.AddWithValue("jssj", Convert.ToDateTime(endDate));
		sda.SelectCommand.Parameters.AddWithValue("dept", dept);
		DataTable dt = new DataTable();
		try
		{
			sda.Fill(dt);
			dt.TableName = "CZJL";
		}
		catch
		{
			dt.TableName = "err";
		}
		return dt;
	}

	[WebMethod]
	public DataTable getXFJL(string startDate, string endDate, string dept,string groups,string jh)
	{
		SqlConnection conn = new SqlConnection(eConnStr);
		SqlDataAdapter sda = new SqlDataAdapter("SELECT  xfdd, '' as bz,  '新乡机务段' AS xxjwd, rybh, ryxm AS sjxm, xfje as lqje, SUBSTRING(CONVERT(nvarchar(20), xfsj, 120), 1, 10) AS lqrq, SUBSTRING(CONVERT(nvarchar(20),  xfsj, 120), 12, 5) AS lqsj,  dept FROM V_XFJL where xfsj >=@kssj and xfsj<@jssj and (dept=@dept or @dept='All') and (zm=@groups or @groups='All') and (jh=@jh or @jh='000') order by xfsj desc", conn);
		sda.SelectCommand.Parameters.Clear();
		sda.SelectCommand.Parameters.AddWithValue("kssj", Convert.ToDateTime(startDate));
		sda.SelectCommand.Parameters.AddWithValue("jssj", Convert.ToDateTime(endDate));
		sda.SelectCommand.Parameters.AddWithValue("dept", dept);
		sda.SelectCommand.Parameters.AddWithValue("groups", groups);
		sda.SelectCommand.Parameters.AddWithValue("jh", jh);
		DataTable dt = new DataTable();
		try
		{
			sda.Fill(dt);
			dt.TableName = "XFJL";
		}
		catch
		{
			dt.TableName = "err";
		}
		return dt;
	}
    
 	[WebMethod]
	public DataTable getXFHZ(string startDate, string endDate, string groups)
	{
		SqlConnection conn = new SqlConnection(eConnStr);
		SqlDataAdapter sda = new SqlDataAdapter("SELECT dbo.fz.zm,CAST(a.xfje AS decimal(10, 2)) as xfje FROM  (SELECT zm, SUM(xfje) AS xfje  FROM V_XFJL  where xfsj >=@kssj and xfsj<@jssj GROUP BY zm) AS a RIGHT OUTER JOIN  dbo.fz ON dbo.fz.zm = a.zm where (fz.zm=@groups or @groups='All')", conn);
		sda.SelectCommand.Parameters.Clear();
		sda.SelectCommand.Parameters.AddWithValue("kssj", Convert.ToDateTime(startDate));
		sda.SelectCommand.Parameters.AddWithValue("jssj", Convert.ToDateTime(endDate));
		sda.SelectCommand.Parameters.AddWithValue("groups", groups);
		DataTable dt = new DataTable();
		try
		{
			sda.Fill(dt);
			dt.TableName = "CZJL";
		}
		catch
		{
			dt.TableName = "err";
		}
		return dt;
	}

	[WebMethod]
	public DataTable getGYXFHZ(string startDate, string endDate, string groups,string jh)
	{
		SqlConnection conn = new SqlConnection(eConnStr);
		SqlDataAdapter sda = new SqlDataAdapter("SELECT b.jh, b.wz as xfdd,CAST(a.xfje AS decimal(10, 2)) as xfje FROM (SELECT xfdd,zm, SUM(xfje) AS xfje  FROM V_XFJL  where xfsj >=@kssj and xfsj<@jssj and (zm=@groups) GROUP BY xfdd,zm) AS a right join (select jh,wz from v_jhsz where (zm=@groups)) as b on a.xfdd=b.wz where (b.jh=@jh or @jh='000')", conn);
		sda.SelectCommand.Parameters.Clear();
		sda.SelectCommand.Parameters.AddWithValue("kssj", Convert.ToDateTime(startDate));
		sda.SelectCommand.Parameters.AddWithValue("jssj", Convert.ToDateTime(endDate));
		sda.SelectCommand.Parameters.AddWithValue("groups", groups);
		sda.SelectCommand.Parameters.AddWithValue("jh", jh);
		DataTable dt = new DataTable();
		try
		{
			sda.Fill(dt);
			dt.TableName = "CZJL";
		}
		catch
		{
			dt.TableName = "err";
		}
		return dt;
	}


	[WebMethod]
	public DataTable getCustomerInfoByCardNo(string cardno)
	{
		SqlConnection conn = new SqlConnection(eConnStr);
		SqlDataAdapter sda = new SqlDataAdapter("SELECT ryid as customerid, rfzt as state, bmmc as custdept, rybh as workno, ryxm as workname, rfkh as cardno, rfye as money,  yj, khrq as opendate, klb as cardtype, yxq as nousedate FROM ryxx where rfzt=32 and  rfkh='" + cardno + "'", conn);
		DataTable dt = new DataTable();
		try
		{
			sda.Fill(dt);
			dt.TableName = "customerinfo";
		}
		catch
		{
			dt.TableName = "err";
		}
		return dt;
	}



	[WebMethod]
	public DataTable getOtherRecord(string workno)
	{
		SqlConnection conn = new SqlConnection(eConnStr);
		SqlDataAdapter sda = new SqlDataAdapter("select receiveTime,work_no, account, type, isReceived, addTime, ipAddress, oper, id, Work_name from V_Ecard_Other where isreceived=0 and work_no='" + workno + "' order by addTime desc", conn);
		DataTable dt = new DataTable();
		try
		{
			sda.Fill(dt);
			dt.TableName = "otherRecord";
		}
		catch
		{
			dt.TableName = "err";
		}
		return dt;
	}

	[WebMethod]
	public DataTable getUnReceivedOther(string user)
	{
		SqlConnection conn = new SqlConnection(eConnStr);
		SqlDataAdapter sda = new SqlDataAdapter("select work_no, account, type,  addTime, id, Work_name from V_Ecard_Other where isreceived=0 and addoper='" + user + "' order by addTime desc", conn);
		DataTable dt = new DataTable();
		try
		{
			sda.Fill(dt);
			dt.TableName = "unReceivedOther";
		}
		catch
		{
			dt.TableName = "err";
		}
		return dt;
	}
	[WebMethod]
	public DataTable getAllOtherRecord(string workno)
	{
		string start = getServerTime().ToString("yyyy-MM") + "-1";
		string end = getServerTime().AddDays(1).ToString("yyyy-MM-dd");
		SqlConnection conn = new SqlConnection(eConnStr);
		SqlDataAdapter sda = new SqlDataAdapter("select receiveTime,work_no, account, type, isReceived, addTime, ipAddress, oper, id, Work_name from V_Ecard_Other where (addTime between '" + start + "' and '" + end +  "' ) and  work_no='" + workno + "' order by addTime desc", conn);
		DataTable dt = new DataTable();
		try
		{
			sda.Fill(dt);
			dt.TableName = "allOtherRecord";
		}
		catch
		{
			dt.TableName = "err";
		}
		return dt;
	}

	[WebMethod]
	public DataTable getOtherRecordByTime(string workno,string start,string end)
	{

		SqlConnection conn = new SqlConnection(eConnStr);
		SqlDataAdapter sda = new SqlDataAdapter("select receiveTime,work_no, account, type, isReceived, addTime, ipAddress, oper, id, Work_name from V_Ecard_Other where (addTime between '" + start + "' and '" + end + "' ) and  work_no='" + workno + "' order by addTime desc", conn);
		DataTable dt = new DataTable();
		try
		{
			sda.Fill(dt);
			dt.TableName = "otherRecordByTime";
		}
		catch
		{
			dt.TableName = "err";
		}
		return dt;
	}


	[WebMethod]
	public DataTable getUnfinishedRecord(string workno,int _place)
	{
        setPlace(_place);
		SqlConnection conn = new SqlConnection(pbConnStr);
		workno = workno.PadLeft(4, '0');
		SqlDataAdapter sda = new SqlDataAdapter("select isReceived, flag,arrive_time2, id, work_no, account, receiveTime, IpAddress, oper, Work_name, Department, Handset, Engi_brand, Engi_no,open_time, Roadway, qduan, Return_time, Return_time2, Arrive_time, whole_time, position,ryCount FROM V_Ecard_Details where isreceived=0 and work_no='" + workno + "' order by open_time desc", conn);
		DataTable dt = new DataTable();
		try
		{
			sda.Fill(dt);
			dt.TableName = "unfinishedRecord";
		}
		catch
		{
			dt.TableName = "err";
		}
		return dt;
	}

    [WebMethod]
    public DataTable getRecordByTime(string workno,string start,string end,int _place)
    {
        setPlace(_place);
        SqlConnection conn = new SqlConnection(pbConnStr);
		SqlDataAdapter sda = new SqlDataAdapter("select isReceived, flag,arrive_time2, id, work_no, account, receiveTime, IpAddress, oper, Work_name, Department, Handset, Engi_brand, Engi_no,open_time, Roadway, qduan, Return_time, Return_time2, Arrive_time, whole_time, position,ryCount FROM V_Ecard_Details where ( open_time between '" + start + " ' and '" + end + "' ) and  work_no='" + workno + "' order by open_time desc", conn);
        DataTable dt = new DataTable();
        try
        {
            sda.Fill(dt);
            dt.TableName = "recordByTime";
        }
        catch
        {
            dt.TableName = "err";
        }
        return dt;
    }

	[WebMethod]
	public DataTable getAllRecord(string workno,int _place)
	{
        setPlace(_place);
		string start = getServerTime().ToString("yyyy-MM") + "-1";
		string end = getServerTime().AddDays(1).ToString("yyyy-MM-dd");
		SqlConnection conn = new SqlConnection(pbConnStr);
		SqlDataAdapter sda = new SqlDataAdapter("select isReceived,flag,arrive_time2,  id, work_no, account, receiveTime, IpAddress, oper, Work_name, Department, Handset, Engi_brand, Engi_no,open_time, Roadway, qduan, Return_time, Return_time2, Arrive_time, whole_time, position,ryCount FROM V_Ecard_Details where ( open_time between '" + start + " ' and '" + end + "' ) and  work_no='" + workno + "' order by open_time desc", conn);
		DataTable dt = new DataTable();
		try
		{
			sda.Fill(dt);
			dt.TableName = "allRecord";
		}
		catch
		{
			dt.TableName = "err";
		}
		return dt;
	}

	[WebMethod]
	public bool addMoney(int customerid , string cardno,string workno,string workname, string ids, DataTable dt , double Money, double totalMoney, string ip, string oper,int flag,int _place)
	{
        setPlace(_place);
		SqlConnection pConn = new SqlConnection();
		SqlConnection eConn = new SqlConnection(eConnStr);
		SqlCommand pComm = new SqlCommand();
		pComm.Connection = pConn;
		if (flag == 1)
		{
			pComm.CommandText = "update T_Ecard_Receive_Details set isReceived=  1 ,receiveTime=getdate(),ipAddress=@ip,oper=@oper where id in (" + ids + " )";
			pConn.ConnectionString = pbConnStr;
		}
		else
		{
			pComm.CommandText = "update T_ECard_Other set isReceived=  1 ,receiveTime=getdate() where id in (" + ids + " )";
			pConn.ConnectionString = eConnStr;
		}
		pComm.Parameters.Clear();
		pComm.Parameters.AddWithValue("ip", ip);
		pComm.Parameters.AddWithValue("oper", oper);
		SqlCommand eComm = new SqlCommand();
		eComm.Connection = eConn;
		

		SqlTransaction pTran,eTran;
		try
		{
			pConn.Open();
			eConn.Open();
			pTran = pConn.BeginTransaction(IsolationLevel.ReadCommitted);
			eTran = eConn.BeginTransaction(IsolationLevel.ReadCommitted);
		}
		catch
		{
			return false;
		}
		try
		{ 
			
			
			pComm.Transaction = pTran;
			pComm.ExecuteNonQuery();

			eComm.Transaction = eTran;

			foreach (DataRow dr in dt.Rows)
			{
				string cc, kd,dd;
				double je;
				DateTime now = getServerTime();
				if (flag == 1)//乘务补贴
				{
					je = Convert.ToDouble(dr["金额"]);
					cc = Convert.ToString( dr["车次"] );

					kd = Convert.ToString(dr["出勤(开车)点"]).Replace("月","-").Replace("日","");

					dd = Convert.ToString(dr["退勤(到站)点"]).Replace("月", "-").Replace("日", "");
					
					kd = now.Year.ToString() + "-" + kd;
					dd = now.Year.ToString() + "-" + dd;
				}
				else//其它补贴
				{
					je = Convert.ToDouble(dr["补贴金额"]);
					cc = Convert.ToString(dr["补贴说明"]);
					kd = Convert.ToString(dr["添加时间"]).Replace("月", "-").Replace("日", "");
					dd = now.ToString("yy-MM-dd HH:mm:ss");
				}
				
				if (Convert.ToDateTime(kd) > now.AddDays(1))//说明退勤点到读卡点跨年了
				{
					kd = Convert.ToDateTime(kd).AddYears(-1).ToString("yyyy-MM-dd HH:mm:ss");
					dd = Convert.ToDateTime(dd).AddYears(-1).ToString("yyyy-MM-dd HH:mm:ss");
				}

				eComm.CommandText = "insert into T_AddMoney(rybh,ryxm,je,roadway,open_time,arrive_time,addType,oper,ipAddr) values(@workno,@workname,@addMoney,@roadway,@open_time,@arrive_time,@flag,@oper,@ipAddr)";
				eComm.Parameters.Clear();
				eComm.Parameters.AddWithValue("addMoney", je);
				eComm.Parameters.AddWithValue("oper", oper);
				eComm.Parameters.AddWithValue("workno", workno);
				eComm.Parameters.AddWithValue("workname", workname);
				eComm.Parameters.AddWithValue("roadway", cc);
				eComm.Parameters.AddWithValue("open_time", kd);
				eComm.Parameters.AddWithValue("flag", flag);
				eComm.Parameters.AddWithValue("ipAddr", ip);
				eComm.Parameters.AddWithValue("arrive_time", dd);
				eComm.ExecuteNonQuery();
			}
			eComm.CommandText = "update ryxx set rfye=rfye+" + Money.ToString() + " where ryid='" + customerid.ToString() + "'" ;
			eComm.ExecuteNonQuery();
			eComm.CommandText = "insert into lssj(ryid,rfkh,xfjh,xfje,rfye,xfsj,xffs,xfzl,sky) values(@customerid,@cardno,0,@addMoney,@totalMoney,getdate(),'补贴充值      ','增款  ',@oper)";
			eComm.Parameters.Clear();
			eComm.Parameters.AddWithValue("customerid", customerid);
			eComm.Parameters.AddWithValue("cardno", cardno);
			eComm.Parameters.AddWithValue("addMoney", Money);
			eComm.Parameters.AddWithValue("oper", oper);
			eComm.Parameters.AddWithValue("totalMoney", totalMoney);
			eComm.ExecuteNonQuery();
			pTran.Commit();
			eTran.Commit();
		}
		catch
		{
			pTran.Rollback();
			eTran.Rollback();
			return false;
		}
		finally
		{
			pConn.Close();
			eConn.Close();
		}
		return true;
	}

	[WebMethod]
	public bool AddMoneyManual(int customerid, string cardno, string workno, string workname,string cc, string kd, double Money, double totalMoney, string ipAddr, string oper, int flag)
	{
		SqlConnection eConn = new SqlConnection(eConnStr);
		SqlCommand eComm = new SqlCommand();
		eComm.Connection = eConn;
		SqlTransaction eTran;
		try
		{
			eConn.Open();
			eTran = eConn.BeginTransaction(IsolationLevel.ReadCommitted);
		}
		catch
		{
			return false;
		}

		try
		{
			kd = Convert.ToDateTime(kd).ToString("HH:mm");
			eComm.Transaction = eTran;

			eComm.CommandText = "insert into T_AddMoney(rybh,ryxm,je,roadway,open_time,addType,oper,ipAddr) values(@workno,@workname,@addMoney,@roadway,@open_time,@flag,@oper,@ipAddr)";
			eComm.Parameters.Clear();
			eComm.Parameters.AddWithValue("addMoney", Money);
			eComm.Parameters.AddWithValue("oper", oper);
			eComm.Parameters.AddWithValue("workno", workno);
			eComm.Parameters.AddWithValue("workname", workname);
			eComm.Parameters.AddWithValue("roadway", cc);
			eComm.Parameters.AddWithValue("open_time", kd);
			eComm.Parameters.AddWithValue("flag", flag);
			eComm.Parameters.AddWithValue("ipAddr", ipAddr);
			eComm.ExecuteNonQuery();

			eComm.CommandText = "update ryxx set rfye=rfye+" + Money.ToString() + " where ryid='" + customerid.ToString() + "'";
			eComm.ExecuteNonQuery();
			eComm.CommandText = "insert into lssj(ryid,rfkh,xfjh,xfje,rfye,xfsj,xffs,xfzl,sky) values(@customerid,@cardno,0,@addMoney,@totalMoney,getdate(),'补贴充值      ','增款  ',@oper)";
			eComm.Parameters.Clear();
			eComm.Parameters.AddWithValue("customerid", customerid);
			eComm.Parameters.AddWithValue("cardno", cardno);
			eComm.Parameters.AddWithValue("addMoney", Money);
			eComm.Parameters.AddWithValue("oper", oper);
			eComm.Parameters.AddWithValue("totalMoney", totalMoney);
			eComm.ExecuteNonQuery();
			eTran.Commit();
		}
		catch
		{
			eTran.Rollback();
			return false;
		}
		finally
		{
			eConn.Close();
		}
		return true;
	}


	[WebMethod]
	public DateTime getServerTime()
	{
		SqlConnection eConn = new SqlConnection(eConnStr);
		SqlCommand comm = new SqlCommand("select getdate()", eConn);
		eConn.Open();
		DateTime dt = Convert.ToDateTime(comm.ExecuteScalar());
		eConn.Close();
		return dt;
	}

	[WebMethod]
	public DateTime getValidDate(string workno)
	{
		SqlConnection eConn = new SqlConnection(eConnStr);
		SqlCommand comm = new SqlCommand("select newValidDate from  T_NewValidDate where work_no='" + workno + "' order by newValidDate desc", eConn);

		DateTime date;
		try
		{
			eConn.Open();
			date = Convert.ToDateTime(comm.ExecuteScalar());
		}
		catch
		{
			date = Convert.ToDateTime("2013-1-1");
		}
		finally
		{
			eConn.Close();
		}
		return date;
	}

	[WebMethod]
	public DateTime getNewValidDate()
	{
		DateTime date = getServerTime();
		return Convert.ToDateTime(date.AddMonths(1).ToString("yyyy-MM") + "-" + ValidDay);
	}

	private DateTime getMonth(DateTime date)
	{
		return Convert.ToDateTime((date.ToString("yyyy-MM") + "-1"));
	}

	[WebMethod]
	public bool setValidDateOnly(string workno, DateTime vDate)
	{
		SqlConnection eConn = new SqlConnection(eConnStr);
		SqlCommand eComm = new SqlCommand();
		eComm.Connection = eConn;
		eComm.CommandText = "update ryxx set yxq=@newValidDate where rybh=@workno";
		eComm.Parameters.Clear();
		eComm.Parameters.AddWithValue("workno", workno);
		eComm.Parameters.AddWithValue("newValidDate", vDate);
		try
		{
			eConn.Open();
			eComm.ExecuteNonQuery();
		}
		catch
		{
			return false;
		}
		finally
		{
			eConn.Close();
		}
		return true;
	}

	[WebMethod]
	public int  setValidDate(string workno,DateTime ov,double cMoney,double money, int customerid,string cardno,string ip,string oper,int _place)
	{
        setPlace(_place);
		SqlConnection eConn = new SqlConnection(eConnStr);
		SqlConnection pConn = new SqlConnection(pbConnStr);
		
		DateTime now = getServerTime();
		DateTime oldValid = ov; //getValidDate(workno);
		
		if (getMonth(now) < getMonth(oldValid)) return -2;//比如现在是2013年2月，卡有效期是2013年3月，返回-2不允许修改
		DateTime newValid = getNewValidDate();

		SqlCommand pComm = new SqlCommand();
		SqlCommand eComm = new SqlCommand();
		pComm.Connection = pConn;
		eComm.Connection = eConn;
		SqlTransaction pTran, eTran;
		try
		{
			pConn.Open();
			eConn.Open();
			pTran = pConn.BeginTransaction(IsolationLevel.ReadCommitted);
			eTran = eConn.BeginTransaction(IsolationLevel.ReadCommitted);
		}
		catch
		{
			return -1;
		}

		try
		{
			pComm.Transaction = pTran;
			eComm.Transaction = eTran;
			//未写卡金额清零，从上个月25号算起
			pComm.CommandText = "update T_Ecard_Receive_Details set isReceived=-1,receiveTime=getdate() where work_no=@workno and isReceived=0 and AddTime < @thisMonthFirst ";
			pComm.Parameters.Clear();
			pComm.Parameters.AddWithValue("workno", workno);
			pComm.Parameters.AddWithValue("thisMonthFirst",Convert.ToDateTime( now.ToString("yyyy-MM") + "-" + clearDay.ToString() + " 18:00:00").AddMonths(-1));
			pComm.ExecuteNonQuery();
			if (cMoney != 0)
			{
				//卡内金额清零
				eComm.CommandText = "update ryxx set rfye=@money,yxq=@newValidDate where rybh=@workno";
				eComm.Parameters.Clear();
				eComm.Parameters.AddWithValue("workno", workno);
				eComm.Parameters.AddWithValue("newValidDate", newValid);
				eComm.Parameters.AddWithValue("money", money);
				eComm.ExecuteNonQuery();
				//写消费充值记录表
				eComm.CommandText = "insert into lssj(ryid,rfkh,xfjh,xfje,rfye,xfsj,xffs,xfzl,sky) values(@customerid,@cardno,0,@addMoney,@money,getdate(),'补贴清零      ','减款  ',@oper)";
				eComm.Parameters.Clear();
				eComm.Parameters.AddWithValue("customerid", customerid);
				eComm.Parameters.AddWithValue("cardno", cardno);
				eComm.Parameters.AddWithValue("addMoney", -1 * cMoney);
				eComm.Parameters.AddWithValue("money", money);
				eComm.Parameters.AddWithValue("oper", oper);
				eComm.ExecuteNonQuery();
				//写清零记录表
				eComm.CommandText = "insert into T_NewValidDate( work_no, clearMoney,newValidDate, ipAddress, oper) values(@workno, @clearMoney,@newValidDate, @ipAddress, @oper)";
				eComm.Parameters.Clear();
				eComm.Parameters.AddWithValue("workno", workno);
				eComm.Parameters.AddWithValue("newValidDate", newValid);
				eComm.Parameters.AddWithValue("clearMoney", cMoney);
				eComm.Parameters.AddWithValue("ipAddress", ip);
				eComm.Parameters.AddWithValue("oper", oper);
				eComm.ExecuteNonQuery();
			}
			else
			{
				eComm.CommandText = "update ryxx set yxq=@newValidDate where rybh=@workno";
				eComm.Parameters.Clear();
				eComm.Parameters.AddWithValue("workno", workno);
				eComm.Parameters.AddWithValue("newValidDate", newValid);
				eComm.ExecuteNonQuery();
			}
			pTran.Commit();
			eTran.Commit();
		}
		catch
		{
			pTran.Rollback();
			eTran.Rollback();
			return -1;
		}

		
		return 1;
	}




	[WebMethod]
	public bool deleteOtherRecord(int batid,string workno)

	{
		SqlConnection pConn = new SqlConnection(eConnStr);
		SqlCommand comm = new SqlCommand();
		comm.Connection = pConn;
		comm.CommandText = "update T_ECard_Other set isValid=0 where work_no=@workno and bat_id=@batid";
		comm.Parameters.Clear();
		comm.Parameters.AddWithValue("batid", batid);
		comm.Parameters.AddWithValue("workno", workno);
		try
		{
			pConn.Open();
			comm.ExecuteNonQuery();
		}
		catch
		{
			return false;
		}
		finally
		{
			pConn.Close();
		}
		return true;
	}

	[WebMethod]
	public bool deleteOtherRecordById(int id)
	{
		SqlConnection pConn = new SqlConnection(eConnStr);
		SqlCommand comm = new SqlCommand();
		comm.Connection = pConn;
		comm.CommandText = "update T_ECard_Other set isValid=0 where id=@id";
		comm.Parameters.Clear();
		comm.Parameters.AddWithValue("id", id);
		try
		{
			pConn.Open();
			comm.ExecuteNonQuery();
		}
		catch
		{
			return false;
		}
		finally
		{
			pConn.Close();
		}
		return true;
	}

	[WebMethod]
	public int importOtherRecord(DataTable dt ,string bat,string oper,string Ip)
	{
		SqlConnection pConn = new SqlConnection(eConnStr);
		if (dt.TableName.Equals("importOtherRecord"))
		{
			SqlCommand comm = new SqlCommand();
			comm.Connection = pConn;
			comm.CommandText = "insert into T_ECard_Other_Bat (batname,oper,ipaddress) values(@bat,@oper,@ip);select @@identity";
			SqlTransaction pTran;
			int bat_id;
			try
			{
				pConn.Open();
				pTran = pConn.BeginTransaction();
				comm.Transaction = pTran;
				comm.Parameters.Clear();
				comm.Parameters.AddWithValue("bat", bat);
				comm.Parameters.AddWithValue("oper", oper);
				comm.Parameters.AddWithValue("ip", Ip);
				bat_id = Convert.ToInt32( comm.ExecuteScalar());
			}
			catch
			{
				return 0;
			}
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				string work_no = Convert.ToString(dt.Rows[i]["工号"]);
				string txt = Convert.ToString(dt.Rows[i]["备注"]);
				double money = Convert.ToDouble(dt.Rows[i]["金额"]);
				comm.CommandText = "insert into T_ECard_Other (bat_id,work_no,account,type,oper,ipaddress) values(@batid,@workno,@money,@txt,@oper,@ip);select @@identity";
				comm.Parameters.Clear();
				comm.Parameters.AddWithValue("batid", bat_id);
				comm.Parameters.AddWithValue("workno", work_no);
				comm.Parameters.AddWithValue("money", money);
				comm.Parameters.AddWithValue("txt", txt);
				comm.Parameters.AddWithValue("oper", oper);
				comm.Parameters.AddWithValue("ip", Ip);
				try
				{
					comm.ExecuteNonQuery();
				}
				catch
				{
					pTran.Rollback();
					return 0;
				}
			}
			pTran.Commit();
			return dt.Rows.Count;
		}
		else
		{
			return -1;
		}
	}


    [WebMethod]
    public int importFLRecord(DataTable dt, string bat, string oper, string Ip)
    {
        SqlConnection pConn = new SqlConnection(eConnStr);
        if (dt.TableName.Equals("importOtherRecord"))
        {
            SqlCommand comm = new SqlCommand();
            comm.Connection = pConn;
            comm.CommandText = "insert into T_AddMoneyFL_Bat (batname,oper,ipaddress) values(@bat,@oper,@ip);select @@identity";
            SqlTransaction pTran;
            int bat_id;
            try
            {
                pConn.Open();
                pTran = pConn.BeginTransaction();
                comm.Transaction = pTran;
                comm.Parameters.Clear();
                comm.Parameters.AddWithValue("bat", bat);
                comm.Parameters.AddWithValue("oper", oper);
                comm.Parameters.AddWithValue("ip", Ip);
                bat_id = Convert.ToInt32(comm.ExecuteScalar());
            }
            catch
            {
                return 0;
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string work_no = Convert.ToString(dt.Rows[i]["工号"]);
                string txt = Convert.ToString(dt.Rows[i]["备注"]);
                double money = Convert.ToDouble(dt.Rows[i]["金额"]);
                comm.CommandText = "insert into T_AddMoneyFL (batid,rybh,je,bz,tjr) values(@batid,@workno,@money,@txt,@oper)";
                comm.Parameters.Clear();
                comm.Parameters.AddWithValue("batid", bat_id);
                comm.Parameters.AddWithValue("workno", work_no);
                comm.Parameters.AddWithValue("money", money);
                comm.Parameters.AddWithValue("txt", txt);
                comm.Parameters.AddWithValue("oper", oper);
                try
                {
                    comm.ExecuteNonQuery();
                }
                catch
                {
                    pTran.Rollback();
                    return 0;
                }
            }
            pTran.Commit();
            return dt.Rows.Count;
        }
        else
        {
            return -1;
        }
    }


    [WebMethod]
    public DataTable getCanBu(string work_no, DateTime kssj, DateTime jssj)
    {
        SqlConnection conn = new SqlConnection(eConnStr);
        SqlCommand comm = new SqlCommand();
        comm.Connection = conn;
        comm.CommandText = "select  id, rybh, ryxm, je, addtime, engi_brand, engi_no, roadway, open_time, arrive_time, addType, oper, ipAddr, CAST(arrive_time - open_time AS float) * 24 AS shichang from T_AddMoney where rybh=@work_no and arrive_time > @kssj and arrive_time <= @jssj order by arrive_time desc";
        comm.Parameters.Clear();
        comm.Parameters.AddWithValue("work_no", work_no);
        comm.Parameters.AddWithValue("kssj", kssj);
        comm.Parameters.AddWithValue("jssj", jssj);

        SqlDataAdapter sda = new SqlDataAdapter(comm);
        DataTable dt = new DataTable();
        try
        {
            sda.Fill(dt);
        }
        catch
        {
            dt.TableName = "error!";
            return dt;
        }
        dt.TableName = "getCanBu";
        return dt;
    }


    [WebMethod]
    public bool canBuChengWuKa(string bmmc)
    {
        switch (bmmc)
        {
            case "All":
            case "all":
            case "安阳运用":
            case "新乡运用":
            case "新南运用":
            case "月山运用":
            case "长北运用":
                return true;
            default:
                return false;
        }
    }

    
    #region 福利卡相关


    [WebMethod]
    public DataTable getChongZhi(string rfkh)
    {
        SqlConnection conn = new SqlConnection(eConnStr);
        SqlCommand comm = new SqlCommand();
        comm.Connection = conn;
        comm.CommandText = "SELECT  id, addtime, je, bz, lqsj,yxq FROM V_AddMoneyFL WHERE sflq=0 and rfkh=@rfkh order by sflq ,addtime desc";
        comm.Parameters.Clear();
        comm.Parameters.AddWithValue("rfkh", rfkh);

        SqlDataAdapter sda = new SqlDataAdapter(comm);
        DataTable dt = new DataTable();
        try
        {
            sda.Fill(dt);
        }
        catch
        {
            dt.TableName = "error!";
            return dt;
        }
        dt.TableName = "getChongZhi";
        return dt;
    }


    [WebMethod]
    public DataTable getRyxxFL(string bmmc)
    {
        SqlConnection conn = new SqlConnection(eConnStr);
        SqlCommand comm = new SqlCommand();
        comm.Connection = conn;
        comm.CommandText = "select rybh as gh,ryxm as xm,bmmc as bm,rfkh as kh,khrq from ryxx where rfzt <> -1 and (@bmmc='All' or bmmc=@bmmc) order by rybh";
        comm.Parameters.Clear();
        comm.Parameters.AddWithValue("bmmc", bmmc);

        SqlDataAdapter sda = new SqlDataAdapter(comm);
        DataTable dt = new DataTable();
        try
        {
            sda.Fill(dt);
        }
        catch
        {
            dt.TableName = "error!";
            return dt;
        }
        dt.TableName = "getRyxxFL";
        return dt;
    }

    [WebMethod]
    public DataTable getRyxxFLKai(string bmmc,bool showAll)
    {
        SqlConnection conn = new SqlConnection(eConnStr);
        SqlCommand comm = new SqlCommand();
        comm.Connection = conn;
        if (showAll)
        {
            comm.CommandText = "select rybh as gh,ryxm as xm,bmmc as bm,rfkh as kh,khrq,rfzt as zt,yxq,rfye from ryxx where rfzt <> -1 and (@bmmc='All' or bmmc=@bmmc) order by rybh";
        }
        else
        {
            comm.CommandText = "select rybh as gh,ryxm as xm,bmmc as bm,rfkh as kh,khrq,rfzt as zt,yxq,rfye from ryxx where rfzt=0 and (@bmmc='All' or bmmc=@bmmc) order by rybh";
        }
        comm.Parameters.Clear();
        comm.Parameters.AddWithValue("bmmc", bmmc);

        SqlDataAdapter sda = new SqlDataAdapter(comm);
        DataTable dt = new DataTable();
        try
        {
            sda.Fill(dt);
        }
        catch
        {
            dt.TableName = "error!";
            return dt;
        }
        dt.TableName = "getRyxxFLKai";
        return dt;
    }

    [WebMethod]
    public DataTable getRyxxByOneFL(string rybh)
    {
        SqlConnection conn = new SqlConnection(eConnStr);
        SqlCommand comm = new SqlCommand();
        comm.Connection = conn;
        comm.CommandText = "select rybh,ryxm,bmmc,rfkh,khrq from ryxx where (rybh=@rybh)";
        comm.Parameters.Clear();
        comm.Parameters.AddWithValue("rybh", rybh);

        SqlDataAdapter sda = new SqlDataAdapter(comm);
        DataTable dt = new DataTable();
        try
        {
            sda.Fill(dt);
        }
        catch
        {
            dt.TableName = "error!";
            return dt;
        }
        dt.TableName = "getRyxxByOneFL";
        return dt;
    }

    [WebMethod]
    public bool existKh(int kh)
    {
        SqlConnection conn = new SqlConnection(eConnStr);
        SqlCommand comm = new SqlCommand();
        comm.Connection = conn;
        comm.CommandText = "select count(*) from ryxx where rfkh=@kh";
        comm.Parameters.Clear();
        comm.Parameters.AddWithValue("kh", kh);
        conn.Open();
        return Convert.ToInt32(comm.ExecuteScalar()) > 0 ;
    }

    [WebMethod]
    public DateTime getNewValidDateFL()
    {
        DateTime date = getServerTime();
        int add = 0;
        switch (date.Month % 3)
        {
            case 0:
                add = 1;
                break;
            case 1:
                add = 3;
                break;
            case 2:
                add = 2;
                break;
        }
    
        return Convert.ToDateTime(date.AddMonths(add).ToString("yyyy-MM") + "-" + ValidDay);
    }

    [WebMethod]
    public bool setNewCardFL (string rybh,string ryxm,string bmmc,int jkh,int xkh,DateTime yxq,string czr,string ip)
    {
        SqlConnection conn = new SqlConnection(eConnStr);
        SqlCommand comm = new SqlCommand();
        comm.Connection = conn;



        SqlTransaction tran;
        try
        {
            conn.Open();
            tran = conn.BeginTransaction(IsolationLevel.ReadCommitted);
            comm.Transaction = tran;
        }
        catch
        {
            return false;
        }
        try
        {
            
            comm.CommandText = "update ryxx set rfzt=-1 where rfkh=@jkh";
            comm.Parameters.Clear();
            comm.Parameters.AddWithValue("jkh", jkh);
            comm.ExecuteNonQuery();
            comm.CommandText = "select max(ryid) from ryxx";
            int maxid = Convert.ToInt32(comm.ExecuteScalar());
            comm.CommandText = "insert into ryxx (ryid,rfzt,rybh,ryxm,bmmc,rfkh,rfye,yxq,khrq) values(@ryid,32,@rybh,@ryxm,@bmmc,@rfkh,0,@yxq,getdate())";
            comm.Parameters.Clear();
            comm.Parameters.AddWithValue("ryid", maxid+1);
            comm.Parameters.AddWithValue("rybh", rybh);
            comm.Parameters.AddWithValue("ryxm", ryxm);
            comm.Parameters.AddWithValue("bmmc", bmmc);
            comm.Parameters.AddWithValue("rfkh", xkh.ToString().PadLeft(6,'0'));
            comm.Parameters.AddWithValue("yxq", yxq);
            comm.ExecuteNonQuery();
            comm.CommandText = "insert into T_NewCard(rybh,ryxm,jkh,xkh,bmmc,yxq,czr,ip) values(@rybh,@ryxm,@jkh,@xkh,@bmmc,@yxq,@czr,@ip)";
            comm.Parameters.Clear();
            comm.Parameters.AddWithValue("rybh", rybh);
            comm.Parameters.AddWithValue("ryxm", ryxm);
            comm.Parameters.AddWithValue("bmmc", bmmc);
            comm.Parameters.AddWithValue("jkh", jkh.ToString().PadLeft(6, '0'));
            comm.Parameters.AddWithValue("xkh", xkh.ToString().PadLeft(6, '0'));
            comm.Parameters.AddWithValue("yxq", yxq);
            comm.Parameters.AddWithValue("czr", czr);
            comm.Parameters.AddWithValue("ip", ip);
            comm.ExecuteNonQuery();
        }
        catch
        {
            tran.Rollback();
            return false;
        }
        tran.Commit();
        return true;
    }


    /// <summary>
    /// 开新卡，已经事先把人员信息导入数据库中的
    /// </summary>
    /// <param name="kh">卡号</param>  
    /// <param name="czr">操作人</param>
    /// <param name="ip">ip地址</param>
    /// <returns>是否成功</returns>
    [WebMethod]
    public bool KaiXinKaFLExists(int kh, string czr, string ip)
    {
        SqlConnection conn = new SqlConnection(eConnStr);
        SqlCommand comm = new SqlCommand();
        comm.Connection = conn;



        SqlTransaction tran;
        try
        {
            conn.Open();
            tran = conn.BeginTransaction(IsolationLevel.ReadCommitted);
            comm.Transaction = tran;
        }
        catch
        {
            return false;
        }
        try
        {

            comm.CommandText = "select count(*) from ryxx where rfzt=0 and rfkh=@kh";
            comm.Parameters.Clear();
            comm.Parameters.AddWithValue("kh", kh);
            if (Convert.ToInt32(comm.ExecuteScalar()) != 1)
            {
                tran.Rollback();
                return false;
            }
            comm.CommandText = "select max(ryid) from ryxx";
            int maxid = Convert.ToInt32(comm.ExecuteScalar());
            comm.CommandText = "update ryxx set ryid=@ryid,rfzt=32,khrq=getdate() where rfzt=0 and rfkh=@kh";
            comm.Parameters.Clear();
            comm.Parameters.AddWithValue("ryid", maxid + 1);
            
            comm.Parameters.AddWithValue("kh", kh.ToString().PadLeft(6, '0'));
            
            comm.ExecuteNonQuery();
            comm.CommandText = "insert into T_NewCard(rybh,ryxm,jkh,xkh,bmmc,yxq,czr,ip,type) select rybh,ryxm,0,rfkh,bmmc,yxq,@czr,@ip,'新' from ryxx where ryid=@ryid";
            comm.Parameters.Clear();
            comm.Parameters.AddWithValue("ryid", maxid + 1);
            comm.Parameters.AddWithValue("czr", czr);
            comm.Parameters.AddWithValue("ip", ip);
            comm.ExecuteNonQuery();

            comm.CommandText = "insert into lssj (ryid,rfkh,xfjh,xfje,rfye,xfsj,xffs,xfzl,sky) select ryid,rfkh,0,rfye,rfye,getdate(),'开卡预存','增款',@czr from ryxx where ryid=@ryid";
            comm.Parameters.Clear();
            comm.Parameters.AddWithValue("ryid", maxid + 1);
            comm.Parameters.AddWithValue("czr", czr);
            comm.ExecuteNonQuery();
            comm.CommandText = "insert into t_addMoneyFL (batid,rybh,je,bz,sflq,lqsj,tjsj,tjr,isvalid) select 1,rybh,rfye,'第一批开卡预存',1,getdate(),getdate(),@czr,1 from ryxx where ryid=@ryid";
            comm.Parameters.Clear();
            comm.Parameters.AddWithValue("ryid", maxid + 1);
            comm.Parameters.AddWithValue("czr", czr);
            comm.ExecuteNonQuery();

        }
        catch
        {
            tran.Rollback();
            return false;
        }
        tran.Commit();
        return true;
        
    }


    [WebMethod]
    public bool KaiXinKaFL(string rybh, string ryxm, string bmmc, int kh, DateTime yxq, string czr, string ip)
    {
        //SqlConnection conn = new SqlConnection(eConnStr);
        //SqlCommand comm = new SqlCommand();
        //comm.Connection = conn;



        //SqlTransaction tran;
        //try
        //{
        //    conn.Open();
        //    tran = conn.BeginTransaction(IsolationLevel.ReadCommitted);
        //    comm.Transaction = tran;
        //}
        //catch
        //{
        //    return false;
        //}
        //try
        //{

        //    comm.CommandText = "select rfzt from ryxx where rfkh=@kh";
        //    comm.Parameters.Clear();
        //    comm.Parameters.AddWithValue("kh", kh);
            
        //    comm.CommandText = "select max(ryid) from ryxx";
        //    int maxid = Convert.ToInt32(comm.ExecuteScalar());
        //    comm.CommandText = "insert into ryxx (ryid,rfzt,rybh,ryxm,bmmc,rfkh,rfye,yxq,khrq) values(@ryid,32,@rybh,@ryxm,@bmmc,@rfkh,0,@yxq,getdate())";
        //    comm.Parameters.Clear();
        //    comm.Parameters.AddWithValue("ryid", maxid + 1);
        //    comm.Parameters.AddWithValue("rybh", rybh);
        //    comm.Parameters.AddWithValue("ryxm", ryxm);
        //    comm.Parameters.AddWithValue("bmmc", bmmc);
        //    comm.Parameters.AddWithValue("rfkh", xkh.ToString().PadLeft(6, '0'));
        //    comm.Parameters.AddWithValue("yxq", yxq);
        //    comm.ExecuteNonQuery();
        //    comm.CommandText = "insert into T_NewCard(rybh,ryxm,jkh,xkh,bmmc,yxq,czr,ip) values(@rybh,@ryxm,@jkh,@xkh,@bmmc,@yxq,@czr,@ip)";
        //    comm.Parameters.Clear();
        //    comm.Parameters.AddWithValue("rybh", rybh);
        //    comm.Parameters.AddWithValue("ryxm", ryxm);
        //    comm.Parameters.AddWithValue("bmmc", bmmc);
        //    comm.Parameters.AddWithValue("jkh", jkh.ToString().PadLeft(6, '0'));
        //    comm.Parameters.AddWithValue("xkh", xkh.ToString().PadLeft(6, '0'));
        //    comm.Parameters.AddWithValue("yxq", yxq);
        //    comm.Parameters.AddWithValue("czr", czr);
        //    comm.Parameters.AddWithValue("ip", ip);
        //    comm.ExecuteNonQuery();
        //}
        //catch
        //{
        //    tran.Rollback();
        //    return false;
        //}
        //tran.Commit();
        return true;
    }

    [WebMethod]
    public bool setFLMoneyReceived(int rfkh, int gh, int id,double oldje,double je, DateTime yxq ,string oper, string ip)
    {
        SqlConnection eConn = new SqlConnection(eConnStr);
        SqlCommand eComm = new SqlCommand();
        eComm.Connection = eConn;
        SqlTransaction eTran;
        try
        {
            eConn.Open();
            eTran = eConn.BeginTransaction(IsolationLevel.ReadCommitted);
        }
        catch
        {
            return false;
        }

        try
        {
            eComm.Transaction = eTran;

            eComm.CommandText = "update T_AddMoneyFL set sflq=1,lqsj=getdate(),oper=@oper,ipaddress=@ip where id=@id";
            eComm.Parameters.Clear();
            eComm.Parameters.AddWithValue("oper", oper);
            eComm.Parameters.AddWithValue("ip", ip);
            eComm.Parameters.AddWithValue("id", id);
            eComm.ExecuteNonQuery();

            eComm.CommandText = "update ryxx set yxq=@yxq where rfkh=@rfkh";
            eComm.Parameters.Clear();
            eComm.Parameters.AddWithValue("yxq", yxq);
            eComm.Parameters.AddWithValue("rfkh", rfkh);
            eComm.ExecuteNonQuery();

            if (oldje > 0)
            {
                eComm.CommandText = "insert into T_NewValidDate( work_no, clearMoney,newValidDate, ipAddress, oper) values(@workno, @clearMoney,@newValidDate, @ipAddress, @oper)";
                eComm.Parameters.Clear();
                eComm.Parameters.AddWithValue("workno", gh);
                eComm.Parameters.AddWithValue("newValidDate", yxq);
                eComm.Parameters.AddWithValue("clearMoney", oldje);
                eComm.Parameters.AddWithValue("ipAddress", ip);
                eComm.Parameters.AddWithValue("oper", oper);
                eComm.ExecuteNonQuery();
            }


            eTran.Commit();
        }
        catch
        {
            eTran.Rollback();
            return false;
        }
        finally
        {
            eConn.Close();
        }
        return true;
    }

    #endregion
}
