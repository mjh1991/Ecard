using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace EcardFuli
{
    class CustomerInfo
    {
        private string workno;
        private string workname;
        private string dept;
        private string cardtype;
        private int state;
        private DateTime nousedate;
        private double money;
        private string cardno;
        private int custoemrid;
		private double thisMonthMoney;

        public string Work_no
        {
            get
            {
                return workno;
            }
            set
            {
                workno = value;
            }
        }
        public string Work_name
        {
            get
            {
                return workname;
            }
            set
            {
                workname = value;
            }
        }

        public string CustDept
        {
            get
            {
                return dept;
            }
            set
            {
                dept = value;
            }
        }

        public string CardNo
        {
            get
            {
                return cardno;
            }
            set
            {
                cardno = value;
            }
        }



        public string CardType
        {
            get
            {
                return cardtype;
            }
            set
            {
                cardtype = value;
            }
        }

        public int State
        {
            get
            {
                return state;
            }
            set
            {
                state = value;
            }
        }

        public double Money
        {
            get
            {
                return money;
            }
            set
            {
                money = value;
            }
        }

        public int CustomerId
        {
            get
            {
                return custoemrid;
            }
            set
            {
                custoemrid = value;
            }
        }

        public DateTime NoUseDate
        {
            get
            {
                return nousedate;
            }
            set
            {
                nousedate = value;
            }
        }

		public double ThisMonthMoney
		{
			get
			{
				return thisMonthMoney;
			}
			set
			{
				thisMonthMoney = value;
			}
		}

        public CustomerInfo()
        {
            workno = "";
            workname = "";
            dept = "";
            cardtype = "";
            state = 0;

            money = 0;
            cardno = "";
            custoemrid = 0;
        }


        public bool hasUnReceivedMoney(int flag,int _place)
        {
            EcardService.Service s = new EcardService.Service();
            return s.hasUnReceivedMoney(workno,flag,_place);
        }
        public bool getFullInfo()
        {

            EcardService.Service s = new EcardService.Service();

            DataTable dt = s.getCustomerInfoByCardNo(cardno);
            if (dt.TableName.Equals("customerinfo") && dt.Rows.Count == 1)
            {
                custoemrid = Convert.ToInt32(dt.Rows[0]["customerid"]);
                money = Convert.ToDouble(dt.Rows[0]["money"]);
                workno = Convert.ToString(dt.Rows[0]["workno"]);
                workname = Convert.ToString(dt.Rows[0]["workname"]);
                dept = Convert.ToString(dt.Rows[0]["custdept"]);
                cardtype = Convert.ToString(dt.Rows[0]["cardtype"]);
                state = Convert.ToInt32(dt.Rows[0]["state"]);
				try
				{
					nousedate = Convert.ToDateTime(Convert.ToDateTime(dt.Rows[0]["nousedate"]).ToString("yyyy-MM-dd"));
				}
				catch
				{
					nousedate = Convert.ToDateTime("1900-1-1");
				}

				thisMonthMoney = s.getMoneyThisMonth(workno);

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
