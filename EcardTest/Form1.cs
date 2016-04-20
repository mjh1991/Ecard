using System;
using System.Windows.Forms;
using CardFuncBase;

namespace EcardTest
{
    public partial class Form1 : Form
    {
        readonly CardOper _co = new CardOper(CardType.YuanGong);
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _co.NewCard(803974, "测试50", Convert.ToDateTime("2020-12-31"));
            _co.AddMoney(1000);
            //_co.SetValidDate(Convert.ToDateTime("2017-1-1"));
            textBox1.Text = _co.GetValidDate().ToString("yyyy-MM-dd");
            //textBox1.Text = co.getMoney().ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //co.addMoney(0.1);
            //co.initCard2();
            textBox1.Text = _co.GetCardno();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _co.Init();
        }
    }
}
