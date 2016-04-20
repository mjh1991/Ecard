﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

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
		private static extern string dqnewreadje(
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
		private static extern string newreadcard(
			int port,  
			string kh,
			int ks
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



		public static string getMoney(int port,int kh)
		{
			return dqnewreadje(port, kh);
			
		}

		public static string getBlocks(int port,int kh)
		{
			return newreadcard(port, kh.ToString(), 1);
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





	}

	class CardOper
	{
		public int iPort;

		public void initCard()
		{
			
		}

		public bool init()
		{
			string passwd = "78ACC0957066";
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
            return "003210";
			string allBlock = CardFunc.getBlocks(iPort, 2);
			//if (allBlock.Length != 32) return "error";
			return allBlock.Substring(8, 6);
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
			return Convert.ToDateTime(str);
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



	}



}
