using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CardFuncBase
{
    internal class CardFunc
    {




        #region 调用dll部分
        /// <summary>
        /// 获取卡内金额
        /// </summary>
        /// <param name="port">端口号</param>
        /// <returns>金额数，单位（分）</returns>
        [DllImport("project2.dll", CharSet = CharSet.Ansi)]
        private static extern int readje(int port);


        /// <summary>
        /// 获取卡号
        /// </summary>
        /// <param name="port">端口号</param>
        /// <returns>卡号，如果是为初始化的新卡返回-1</returns>
        [DllImport("project2.dll", EntryPoint = "readkh", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        private static extern int readkh(int port);


        /// <summary>
        /// 获取有效期
        /// </summary>
        /// <param name="port">端口号</param>
        /// <returns>6数字形式的有效期，格式为yyMMdd</returns>
        [DllImport("project2.dll", EntryPoint = "readyxq", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        private static extern int readyxq(int port);

        /// <summary>
        /// 写入新有效期
        /// </summary>
        /// <param name="port">端口号</param>
        /// <param name="yxq">字符形式的有效期，格式为yyMMdd</param>
        /// <returns>是否写入成功</returns>
        [DllImport("project2.dll", EntryPoint = "writeyxq", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        private static extern bool writeyxq(int port, string yxq);


        /// <summary>
        /// 余额清零
        /// </summary>
        /// <param name="port">读卡器端口号</param>
        /// <param name="kh">块号，4块是个人金额，8块是补贴金额</param>
        /// <returns>是否成功</returns>
        [DllImport("project2.dll", CharSet = CharSet.Ansi)]
        private static extern bool dqmoney(int port, int kh);

        /// <summary>
        /// 增加金额
        /// </summary>
        /// <param name="port">款口号</param>
        /// <param name="kh">块号，4块是个人金额，8块是补贴金额</param>
        /// <param name="je">金额，单位分</param>
        /// <returns>是否加款成功</returns>
        [DllImport("project2.dll", CharSet = CharSet.Ansi)]
        private static extern bool dqaddje(int port, int kh, int je);

        /// <summary>
        /// 扣除金额
        /// </summary>
        /// <param name="port">端口号</param>
        /// <param name="kh">块号，4块是个人金额，8块是补贴金额</param>
        /// <param name="je">金额，单位分</param>
        /// <returns>是否扣款成功</returns>
        [DllImport("project2.dll", CharSet = CharSet.Ansi)]
        private static extern bool dqjk(int port, int kh, int je);


        /// <summary>
        /// 设置读卡器密码
        /// </summary>
        /// <param name="port">端口号</param>
        /// <param name="mm">密码</param>
        /// <returns></returns>
        [DllImport("project2.dll", EntryPoint = "downmmre", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        private static extern int downmmre(int port, string mm);

        /// <summary>
        /// 写新开卡信息
        /// </summary>
        /// <param name="port">端口号</param>
        /// <param name="kh">卡号</param>
        /// <param name="xm">姓名</param>
        /// <param name="zxrq">注销日期</param>
        /// <returns>是否开卡成功</returns>
        [DllImport("project2.dll", EntryPoint = "kaika", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        private static extern bool buka(int port, string kh, string xm, string zxrq);


        /// <summary>
        /// 初始化卡
        /// </summary>
        /// <param name="port">端口号</param>
        /// <param name="mm">密码</param>
        /// <returns>是否初始化成功</returns>
        [DllImport("project2.dll", EntryPoint = "allcardpassword", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        private static extern bool allcardpassword(int port, string mm);


        [DllImport("project2.dll", EntryPoint = "adddata", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        private static extern bool adddata(string a, int b);

        #endregion




        public static bool add(int x, int y)
        {
            
            return adddata(x.ToString(), y);
        }


        public static bool InitCard(int port, string mm)
        {
            return allcardpassword(port, mm);
        }

        public static bool Buka(int port, int kh, string xm, string zxrq)
        {
            if (kh > 999999) return false;
            return buka(port, kh.ToString().PadLeft(6, '0'), xm, zxrq);
        }

        public static int GetMoney(int port, int kh)
        {
            //return dqnewreadje(port, kh).ToString();
            
            return readje(port);
            
        }



        public static bool ClearMoney(int port, int flag)
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

        public static bool AddMoney(int port, int flag, int count)
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

        public static bool MinusMoney(int port, int flag, int count)
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

        public static int SetPassword(int port, string pass)
        {
            return downmmre(port, pass);
        }






        internal static string GetCardNo(int iPort)
        {
            int kh = readkh(iPort);
            if (kh < 0) return null;
            else if (kh > 820000) return null;
            else return kh.ToString();
        }


        internal static bool SetValidDate(int iPort, string yxq)
        {
            return writeyxq(iPort, yxq);

        }

        internal static string GetValidDate(int port)
        {
            return readyxq(port).ToString();
        }
    }

    enum CardType
    {
        CanBu, YuanGong
    }

    class CardOper
    {
        private int _iPort;
        private string _passwd = string.Empty;


        public void SetType(CardType type)
        {
            switch (type)
            {
                case CardType.CanBu:
                    _passwd = "78ACC0957066";
                    
                    break;
                case CardType.YuanGong:
                    _passwd = "F832E49BD558";
                    break;
            }
            CardFunc.SetPassword(_iPort, _passwd);
        }

        public bool InitCard()
        {
            string oldPass = "FFFFFFFFFFFF";
            if (CardFunc.SetPassword(_iPort, oldPass) <=0)
            {
                return false;
            }
            if (CardFunc.InitCard(_iPort, _passwd) == false)
            {
                return false;
            }
            if (CardFunc.SetPassword(_iPort, _passwd) <=0)
            {
                return false;
            }
            return true;
        }

        public bool Init()
        {
            for (int i = 1; i < 16; i++)
            {
                _iPort = i;
                int j = CardFunc.SetPassword(_iPort, _passwd);
                if (j >= 0)
                {
                    return true;
                }

            }
            return false;
        }


        public CardOper()
        {
            _iPort = -1;
            SetType(CardType.CanBu);
        }

        public CardOper(CardType type)
        {
            _iPort = -1;
            SetType(type);
        }

        //private static int HexToInt(string hex)
        //{
        //	if (hex.Length != 8) return -1;
        //	hex = hex.Substring(6, 2) + hex.Substring(4, 2) + hex.Substring(2, 2) + hex.Substring(0, 2);
        //	return Convert.ToInt32(hex, 16);
        //}

        public double GetMoney()
        {
            return CardFunc.GetMoney(_iPort, 4) / 100.0;
            //return Math.Round( HexToInt(CardFunc.getMoney(iPort, 4)) / 100.0 ,2);
        }

        public string GetCardno()
        {
            string kh = CardFunc.GetCardNo(_iPort);
            if (string.IsNullOrEmpty(kh)) return null;
            return kh.PadLeft(6, '0');


        }

        public bool AddMoney(double money)
        {
            int count = Convert.ToInt32(money * 100);
            return CardFunc.AddMoney(_iPort, 1, count);
        }
        public bool MinusMoney(double money)
        {
            int count = Convert.ToInt32(money * 100);
            return CardFunc.MinusMoney(_iPort, 1, count);
        }
        public bool ClearMoney()
        {
            return CardFunc.ClearMoney(_iPort, 1);
        }



        public DateTime GetValidDate()
        {
            string str = CardFunc.GetValidDate(_iPort); //this.getBlock(1);
            str = str.Substring(0, 4) + "-" + str.Substring(4, 2) + "-" + str.Substring(6, 2);
            try
            {

                return Convert.ToDateTime(str);
            }
            catch
            {
                return new DateTime(0);
            }

        }

        public bool SetValidDate(DateTime dt)
        {



            if (dt > Convert.ToDateTime("2099-12-31") || dt < Convert.ToDateTime("2013-1-1"))
            {
                throw new Exception("指定的日期不在允许范围内。");
            }
            string yxq = dt.ToString("yyMMdd");

            return CardFunc.SetValidDate(_iPort, yxq);
        }

        public bool NewCard(int kh, string xm, DateTime yxq)
        {
            return CardFunc.Buka(_iPort, kh, xm, yxq.ToString("yyMMdd"));
        }


        public static bool adddata(int x, int y)
        {
            return CardFunc.add(x, y);

        }

        internal bool InitCardFromCanBuToYuanGong()
        {
            if (CardFunc.SetPassword(_iPort, "78ACC0957066") <=0)
            {
                return false;
            }
            if (CardFunc.InitCard(_iPort, "F832E49BD558") == false)
            {
                return false;
            }
            if (CardFunc.SetPassword(_iPort, "F832E49BD558")<=0)
            {
                return false;
            }
            return true;
        }

        internal bool InitCardFromYuanGongToYuanGong()
        {
            if (CardFunc.SetPassword(_iPort, "F832E49BD558") <= 0)
            {
                return false;
            }
            if (CardFunc.InitCard(_iPort, "F832E49BD558") == false)
            {
                return false;
            }
            return true;
        }

    }



}
