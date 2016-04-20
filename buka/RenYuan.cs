using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EcardFuli
{
    class RenYuan
    {
        public string GongHao { get; set; }
        public string XingMing { get; set; }
        public string BuMen { get; set; }
        public int KaHao { get; set; }
        public double YuE { get; set; }
        public string YouXiaoQi { get; set; }
        public string ZhuangTai { get; set; }
        public RenYuan()
        {
            GongHao = string.Empty;
            XingMing = string.Empty;
            BuMen = string.Empty;
            KaHao = -1;
            YuE = 0;
            YouXiaoQi = string.Empty;
            ZhuangTai = string.Empty;
        }
        public RenYuan(string gh, string xm, string bm, int kh)
        {
            GongHao = gh;
            XingMing = xm;
            BuMen = bm;
            KaHao = kh;
            YuE = 0;
            YouXiaoQi = string.Empty;
            ZhuangTai = string.Empty;
        }

        public RenYuan(string gh, string xm, string bm, int kh,double ye,string yxq,string zt)
        {
            GongHao = gh;
            XingMing = xm;
            BuMen = bm;
            KaHao = kh;
            YuE = ye;
            YouXiaoQi = yxq;
            ZhuangTai = zt;
        }
    }
}
