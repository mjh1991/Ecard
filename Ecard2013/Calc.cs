using System;
using System.Collections.Generic;
using System.Text;

namespace Ecard2013
{
	class Calc
	{
		private static double assTime = 1;
		private static double breakfast = 2.0;
		private static double lunch = 7.0;
		private static double supper = 7.0;
		private static double bs = 6.0;
		private static double be = 9.0;
		private static double ls = 11.0;
		private static double le = 14.0;
		private static double ss = 18.0;
		private static double se = 21.0;

		public double getWorkTime(DateTime go, DateTime back, int pos,int flag)
		{
			TimeSpan ts = back-go;
			if (flag == 2)//货车
			{
				return Math.Round(ts.TotalHours, 1) + assTime;
			}
			else
			{
				return Math.Round(ts.TotalHours, 1);
			}
		}

		public double getWorkMoney(DateTime go, DateTime back, int pos,int flag,int ryCount)
		{
			TimeSpan ts = back - go;
			double workMoney = 0.0;
			if (flag == 1 )//客车或者途调5275
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
							workMoney += (2* breakfast + lunch + supper);
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
							workMoney += ( breakfast + 2* lunch + supper);
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
							workMoney += (breakfast + lunch + 2* supper);
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
							workMoney += (breakfast + lunch );
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
				else if (allTime >= 16 && allTime <= 24)
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
			else  if (flag==3) //调车
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

		public DateTime getBackTime(string r1, string r2, string a1, string a2, int flag, int pos)
		{
			DateTime aTime,bTime;
			if (flag == 1)//客车
			{

				if (pos == 3)//3位学员
				{
					if (r2 == null || r2=="")//跟司机一起回来
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
			else //货车、调车
			{
				if (pos == 3)//3位学员
				{
					if (r2 == null || r2=="")//跟司机一起回来
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
	}
}
