using System;
using System.Windows.Forms;
using CardFuncBase;
using System.Data;
using System.Data.SqlClient;

namespace EcardTest
{
    public partial class Form1 : Form
    {
        readonly CardOper _co = new CardOper(CardType.YuanGong);


        DataTable getCustomerInfoByCardNo(string rfkh)
        {
            SqlConnection conn = new SqlConnection("Data Source=10.99.81.220;User ID=icsoft;Password=ecard2013;Initial Catalog=icsoft_newGPRS;Persist Security Info=True");
            SqlCommand comm = new SqlCommand();
            comm.CommandText = "SELECT rybh as workno, ryxm as workname, yxq as nousedate FROM ryxx where rfzt=32 and  rfkh='" + rfkh + "'";
            comm.Connection = conn;
            SqlDataAdapter sda = new SqlDataAdapter(comm);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            return dt;
            int i = 1;
        }

        double getMoneyFromDataBase(string rfkh)
        {
            SqlConnection conn = new SqlConnection("Data Source=10.99.81.220;User ID=icsoft;Password=ecard2013;Initial Catalog=icsoft_newGPRS;Persist Security Info=True");
            SqlCommand comm = new SqlCommand();
            comm.CommandText = "SELECT sum(je) FROM V_AddMoneyFL where sflq = 0 and rfkh='" + rfkh + "'";
            comm.Connection = conn;
            try
            {
                conn.Open();
                return Convert.ToDouble(comm.ExecuteScalar());
            }
            catch (Exception)
            {
                return 0;
            }
            finally
            {
                conn.Close();
            }
        }

        bool setMoneyReceive(string rfkh)
        {
            SqlConnection conn = new SqlConnection("Data Source=10.99.81.220;User ID=icsoft;Password=ecard2013;Initial Catalog=icsoft_newGPRS;Persist Security Info=True");
            SqlCommand comm = new SqlCommand();
            comm.Connection = conn;
            comm.CommandText = "select rybh from ryxx where rfzt=32 and rfkh='" + rfkh + "'";
            string rybh;
            try
            {
                conn.Open();
                rybh = comm.ExecuteScalar().ToString();
            }
            catch (Exception)
            {

                return false;
            }
            finally
            {
                conn.Close();
            }


            comm.CommandText = "update T_AddMoneyFL set sflq=1,lqsj=getdate() where sflq = 0 and rybh='" + rybh + "'";
            try
            {
                conn.Open();
                comm.ExecuteNonQuery();
            }
            catch (Exception)
            {

                return false;
            }
            finally
            {
                conn.Close();
            }
            return true;
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void InitForm()
        {
            label5.Text = "金额：";
            textBox2.Text = "";
            textBox4.Text = "";
            textBox1.Text = "";
            textBox3.Text = "";
            textBox1.ReadOnly = false;
        }
        private void GetBasicInfo(string cardno)
        {


            if (string.IsNullOrEmpty(cardno))
            {
                MessageBox.Show("读卡错误");
                return;
            }
            if (cardno.Equals("000000"))
            {
                MessageBox.Show("新卡无信息");
                return;
            }
            DataTable dt = getCustomerInfoByCardNo(cardno);
            if (dt.Rows.Count <= 0)
            {
                MessageBox.Show("库内无此卡号信息");
                return;
            }
            string xm = dt.Rows[0]["workname"].ToString();
            DateTime yxq = Convert.ToDateTime(dt.Rows[0]["nousedate"]);
            string gh = dt.Rows[0]["workno"].ToString();
            if (gh.Length == 6)
            {
                gh = gh.Substring(2, 4);
            }
            textBox1.Text = cardno;
            textBox3.Text = xm;
            textBox2.Text = gh;
            textBox4.Text = yxq.ToString("yyyy-MM-dd");
            label5.Text = "金额:" + _co.GetMoney().ToString();
            textBox1.ReadOnly = true;
        }

        private bool MakeCard()
        {
            if (textBox1.ReadOnly == false)
            {
                MessageBox.Show("请先读卡或输入卡号");
                return false;
            }
            string cardno = textBox1.Text.Trim();
            
            string xm = textBox3.Text.Trim();
            DateTime yxq = Convert.ToDateTime(textBox4.Text.Trim());
            _co.SetType(CardType.YuanGong);
            if (_co.NewCard(Convert.ToInt32(cardno), xm, yxq))
            {
             return _co.ClearMoney();
            }
            else
            {
                return false;
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _co.SetType(CardType.CanBu);
            string cardno = _co.GetCardno();
            GetBasicInfo(cardno);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            _co.SetType(CardType.YuanGong);
            string cardno = _co.GetCardno();
            GetBasicInfo(cardno);
            textBox1.ReadOnly = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _co.Init();
            InitForm();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (_co.InitCardFromCanBuToYuanGong())
            {
                MessageBox.Show("初始化成功。");
                InitForm();
            }
            else
            {
                MessageBox.Show("初始化失败");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (_co.InitCardFromYuanGongToYuanGong())
            {
                MessageBox.Show("初始化成功。");
                InitForm();
            }
            else
            {
                MessageBox.Show("初始化失败");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (MakeCard())
            {
                MessageBox.Show("做卡成功，请读卡2确认信息");
                InitForm();
            }
            else
            {
                MessageBox.Show("做卡失败");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {

            _co.SetType(CardType.YuanGong);
            string cardno = _co.GetCardno();
            if (string.IsNullOrEmpty(cardno))
            {
                MessageBox.Show("读卡错误");
                return;
            }
            if (cardno.Equals("000000"))
            {
                MessageBox.Show("新卡无信息");
                return;
            }
            double money = getMoneyFromDataBase(cardno);
            if (money == 0)
            {
                MessageBox.Show("无充值信息");
                return;
            }
            if (setMoneyReceive(cardno) == true)
            {
                _co.ClearMoney();
                _co.AddMoney(money);
                MessageBox.Show("充值成功，请读卡2确认信息");
                InitForm();
            }
            else
            {
                MessageBox.Show("充值失败");
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            InitForm();
            _co.SetType(CardType.CanBu);
            _co.NewCard(800020, "测试20", Convert.ToDateTime("2012-12-31"));
        }

        private void button7_Click(object sender, EventArgs e)
        {
            _co.SetType(CardType.CanBu);
            string cardno = _co.GetCardno();
            GetBasicInfo(cardno);
            if (_co.InitCardFromCanBuToYuanGong())
            {
                
            }
            else
            {
                MessageBox.Show("初始化失败");
                return;
            }
            if (MakeCard())
            {
                
            }
            else
            {
                MessageBox.Show("做卡失败");
                return;
            }
            if (string.IsNullOrEmpty(cardno))
            {
                MessageBox.Show("读卡错误");
                return;
            }
            if (cardno.Equals("000000"))
            {
                MessageBox.Show("新卡无信息");
                return;
            }
            double money = getMoneyFromDataBase(cardno);
            if (money == 0)
            {
                MessageBox.Show("无充值信息");
                return;
            }
            if (setMoneyReceive(cardno) == true)
            {
                _co.ClearMoney();
                _co.AddMoney(money);
                MessageBox.Show("充值成功，请读卡2确认信息");
                InitForm();
            }
            else
            {
                MessageBox.Show("充值失败");
                return;
            }
        }



        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                GetBasicInfo(textBox1.Text);
            }
        }
    }
}
