using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Reflection;

namespace CardFuncBase
{
	class CardFunc
	{

		

		/// <summary>
		/// 读取卡内余额
		/// </summary>
		/// <param name="port">读卡器端口号</param>
		/// <param name="kh">块号,4块是个人金额，8块是补贴金额</param>
		/// <returns>金额16进制，单位分</returns>
		[DllImport("project2.dll", CharSet = CharSet.Ansi)]
		private static extern StringBuilder            dqnewreadje(
			int port,  
			int kh
			);

		/// <summary>
		/// 读块内容
		/// </summary>
		/// <param name="port">读卡器端口号</param>
		/// <param name="kh">块号</param>
		/// <param name="ks">块数(1-3)</param>
		/// <returns>块内容，16进制</returns>
		[DllImport("project2.dll", CharSet = CharSet.Ansi)]
		private static extern StringBuilder newreadcard(
			int port,  
			string kh,
			int ks
			);

        /// <summary>
        /// 获取卡号
        /// </summary>
        /// <param name="port"></param>
        /// <returns>卡号，如果是为初始化的新卡返回-1</returns>
        [DllImport("project2.dll",EntryPoint="readkh", CharSet = CharSet.Ansi,CallingConvention = CallingConvention.StdCall)]
        private static extern string readkh(
            int port
            );

		/// <summary>
		/// 写块内容
		/// </summary>
		/// <param name="port">读卡器端口号</param>
		/// <param name="kh">块号</param>
		/// <param name="ks">块数(1-3)</param>
		/// <param name="data">写入内容，16进制</param>
		/// <returns>是否成功</returns>
		[DllImport("project2.dll", CharSet = CharSet.Ansi)]
		private static extern bool newwritecard(
			int port,  
			string kh,
			int ks,
			string data
			);


		/// <summary>
		/// 余额清零
		/// </summary>
		/// <param name="port">读卡器端口号</param>
		/// <param name="kh">块号，4块是个人金额，8块是补贴金额</param>
		/// <returns>是否成功</returns>
		[DllImport("project2.dll", CharSet = CharSet.Ansi)]
		private static extern bool dqmoney(
			int port,
			int kh
			);

		/// <summary>
		/// 增加金额
		/// </summary>
		/// <param name="port">款口号</param>
		/// <param name="kh">块号，4块是个人金额，8块是补贴金额</param>
		/// <param name="je">金额，单位分</param>
		/// <returns></returns>
		[DllImport("project2.dll", CharSet = CharSet.Ansi)]
		private static extern bool dqaddje(
			int port,
			int kh,
			int je
			);

		/// <summary>
		/// 扣除金额
		/// </summary>
		/// <param name="port">端口号</param>
		/// <param name="kh">块号，4块是个人金额，8块是补贴金额</param>
		/// <param name="je">金额，单位分</param>
		/// <returns></returns>
		[DllImport("project2.dll", CharSet = CharSet.Ansi)]
		private static extern bool dqjk(
			int port,
			int kh,
			int je
		);


		/// <summary>
		/// 设置读卡器密码
		/// </summary>
		/// <param name="port">端口号</param>
		/// <param name="mm">密码</param>
		/// <returns></returns>
		[DllImport("project2.dll", CharSet = CharSet.Ansi)]
		private static extern bool downmmre(
			int port,
			string mm
		);

        /// <summary>
        /// 写新开卡信息
        /// </summary>
        /// <param name="port">端口号</param>
        /// <param name="kkh">卡块号</param>
        /// <param name="kh">卡号</param>
        /// <param name="xm">姓名</param>
        /// <param name="mm">消费密码</param>
        /// <param name="zxrq">注销日期</param>
        /// <param name="gn">功能</param>
        /// <param name="zdye">最低余额</param>
        /// <param name="daymaxje">每日最大消费金额</param>
        /// <param name="xc1">1时段消费限次</param>
        /// <param name="xc2">1时段消费限次</param>
        /// <param name="xc3">1时段消费限次</param>
        /// <param name="xc4">1时段消费限次</param>
        /// <param name="lx">卡类型（一个字节41-4A）</param>
        /// <returns></returns>
        [DllImport("project2.dll",EntryPoint="kaika" ,  CharSet = CharSet.Ansi,CallingConvention=CallingConvention.StdCall)]
        private static extern bool buka(int port, string kh, string xm, string zxrq);


        [DllImport("project2.dll", EntryPoint = "allcardpassword", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        private static extern bool allcardpassword(int port, string mm);


        public static bool initCard(int port, string mm)
        {
            return allcardpassword(port, mm);
        }

        public static bool buka1(int port,  int kh, string xm, string zxrq)
        {
            if (kh > 999999) return false;
            return buka(port,  kh.ToString().PadLeft(6,'0'), xm, zxrq);
        }

		public static string getMoney(int port,int kh)
		{
			return dqnewreadje(port, kh).ToString();
			
		}

		public static string getBlocks(int port,int kh)
		{
		    StringBuilder sb = newreadcard(port, kh.ToString(), 1);

            return sb.ToString();
		}

		public static bool setBolcks(int port, int kh,string data)
		{
			return newwritecard(port, kh.ToString(), 1, data);
		}

		public static bool clearMoney(int port, int flag)
		{
			int kh;
			if (flag == 1) //余额
			{
				kh = 4;
			}
			else if (flag == 2)
			{
				kh = 8;
			}
			else
			{
				return false;
			}
			return dqmoney(port, kh);
		}

		public static bool addMoney(int port, int flag, int count)
		{
			int kh;
			if (flag == 1) //余额
			{
				kh = 4;
			}
			else if (flag == 2)
			{
				kh = 8;
			}
			else
			{
				return false;
			}
			return dqaddje(port, kh, count);
		}

		public static bool minusMoney(int port, int flag, int count)
		{
			int kh;
			if (flag == 1) //余额
			{
				kh = 4;
			}
			else if (flag == 2)
			{
				kh = 8;
			}
			else
			{
				return false;
			}
			return dqjk(port, kh, count);
		}

		public static bool setPassword(int port, string pass)
		{
			return downmmre(port, pass);
		}






        internal static string getCardNo(int iPort)
        {
            return readkh(iPort).ToString();

        }
    }

	class CardOper
	{
		public int iPort;
        private string passwd = "78ACC0957066";

		public bool initCard()
		{
            string oldPass = "FFFFFFFFFFFF";
            if (CardFunc.setPassword(iPort, oldPass) == false)
            {
                return false;
            }
            if (CardFunc.initCard(iPort, passwd) == false)
            {
                return false;
            }
            if (CardFunc.setPassword(iPort, passwd) == false)
            {
                return false;
            }
            return true;
		}

        public bool initCard1()
        {
            string oldPass = "78ACC0957066";
            if (CardFunc.setPassword(iPort, oldPass) == false)
            {
                return false;
            }
            if (CardFunc.initCard(iPort, passwd) == false)
            {
                return false;
            }
            if (CardFunc.setPassword(iPort, passwd) == false)
            {
                return false;
            }
            return true;
        }

		public bool init()
		{
			for (int i = 1; i < 16; i++)
			{
				iPort = i;
				if (CardFunc.setPassword(iPort, passwd) == true)
				{


					return true;
				}
			}
			return false;
		}

		public CardOper(int port)
		{
			iPort = port;

		}
		public CardOper()
		{
			iPort = 1;
		}

		private static int HexToInt(string hex)
		{
			if (hex.Length != 8) return -1;
			hex = hex.Substring(6, 2) + hex.Substring(4, 2) + hex.Substring(2, 2) + hex.Substring(0, 2);
			return Convert.ToInt32(hex, 16);
		}

		public double getMoney()
		{
			return Math.Round( HexToInt(CardFunc.getMoney(iPort, 4)) / 100.0 ,2);
		}

		public string getCardno()
		{
		    return CardFunc.getCardNo(iPort);

            string allBlock = CardFunc.getBlocks(iPort, 2);
            //if (allBlock.Length != 32) return "error";
            string cardNo;

            if (allBlock == null || allBlock.Equals("")) return null;
            if (allBlock == "-4" || allBlock == "-1") return null;
            try
            {
                cardNo = allBlock.Substring(8, 6);
            }
            catch
            {
                return null;
            }
           
			return cardNo;
		}

		public bool addMoney(double money)
		{
			int count = Convert.ToInt32(money * 100);
			return CardFunc.addMoney(iPort, 1, count);
		}
		public bool minusMoney(double money)
		{
			int count = Convert.ToInt32(money * 100);
			return CardFunc.minusMoney(iPort, 1, count);
		}
		public bool clearMoney()
		{
			return CardFunc.clearMoney(iPort, 1);
		}

		public string getBlock(int block)
		{
			return CardFunc.getBlocks(iPort, block);
		}

		public bool setBlock(int block , string data)
		{
			if (data.Length != 32) return false;
			return CardFunc.setBolcks(iPort, block, data);
		}

		public DateTime getValidDate()
		{
			string str = this.getBlock(1);
            str = "20" + str.Substring(6, 2) + "-" + str.Substring(8, 2) + "-" + str.Substring(10, 2);
            try
            {
                
                return Convert.ToDateTime(str);
            }
            catch
            {
                return new DateTime(0);
            }

		}

		public bool setValidDate(DateTime dt)
		{



			if (dt > Convert.ToDateTime("2099-12-31") || dt < Convert.ToDateTime("2013-1-1"))
			{
				throw new Exception("指定的日期不在允许范围内。");
			}
			string date = dt.ToString("yyyyMMdd").Substring(2,6);
			string str = this.getBlock(1);
			str = str.Substring(0, 32);
			str = str.Substring(0, 6) + date + str.Substring(12, 20);
			return setBlock(1, str);
		}

        public bool newCard(int kh, string xm,DateTime yxq)
        {
            return CardFunc.buka1(iPort,kh, xm,yxq.ToString("yyMMdd"));
        }

        

	}



}
