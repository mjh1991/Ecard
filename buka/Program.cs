using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Net;
using EcardFuli.EcardService;

namespace EcardFuli
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            string ipAddr = "";
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

            Service s = new Service();
            string user = "err!";
            try
            {
                user = s.LoginIP(ipAddr);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            if (user != "err!")
            {
                Application.Run(new FrmMain(user, s.getUserDept(user), ipAddr));
            }
            else
            {
                FrmLogin fm = new FrmLogin();

                if (fm.ShowDialog() == DialogResult.Yes) Application.Run(new FrmMain(fm.UserName, fm.UserDept, ipAddr));
            }
        }
    }
}
