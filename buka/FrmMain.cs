using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CardFuncBase;
using System.IO;

namespace EcardFuli
{
    public partial class FrmMain : Form
    {
        private CardOper _co;
        EcardService.Service _s = new EcardService.Service();
        readonly string _ipAddr;
        readonly string _bmmc;
        readonly string _user;
        bool _chengwu;
        RenYuan _buKaRen = new RenYuan();
        RenYuan _kaiKaREn = new RenYuan();
        private string _cPath = Environment.CurrentDirectory + "\\Project2.dll";
        private bool _canClose;
        public FrmMain(string user,string bm,string ip)
        {
            InitializeComponent();
            _ipAddr = ip;
            this._user = user ;
            _bmmc = bm;
            _chengwu = false;
            tsslOper.Text = "操作员：" + user;
            tsslDept.Text = "当前部门：" + bm;

        }



        private void FrmMain_Load(object sender, EventArgs e)
        {
            byte[] bytes = Properties.Resources.Project2;
            if (File.Exists(_cPath)) File.Delete(_cPath);
            FileStream fs = new FileStream(_cPath, FileMode.CreateNew);
            fs.Write(bytes, 0, bytes.Length);
            fs.Close();
            //if (CardOper.adddata(30, 20))
            //    txtBuNewKh.Text = "a";
            //else
            //{
            //    txtBuNewKh.Text = "b";
            //}


            //return;
            _co = new CardOper();
            
            bool r = _co.Init();
            _canClose = false;
            if (r == false)
            {
                MessageBox.Show(this, "初始化读卡器失败，请检查读卡器是否正确连接。");
                _canClose = true;
                Close();
            }
            _chengwu = _s.canBuChengWuKa(_bmmc);

            if (_bmmc.Equals("充值"))
            {
                SetChongZhi();
                InitChongZhi();
            }
            else
            {
                SetKaiKa();
                InitBuKa();
            }

            
        }

        private void SetChongZhi()
        {
            
            
            grpBu.Dispose();
            grpKai.Dispose();
            menuStrip1.Dispose();
        }


        private void SetKaiKa()
        {
            grpChong.Dispose();
        }

        #region 补卡

        private void ToolStripMenuItemBuKa_Click(object sender, EventArgs e)
        {

            InitBuKa();
        }

        private void InitBuKaLabels()
        {
            lblBuBuMen.Text = "部门：";
            lblBuGongHao.Text = "工号：";
            lblBuKaHao.Text = "卡号：";
            lblBuXingMing.Text = "姓名：";
            txtBuNewKh.Text = "";
            grpBu.BringToFront();
        }

        private void InitBuKa()
        {
            ToolStripMenuItemBuKa.Checked = true;
            ToolStripMenuItemKaiKa.Checked = false;
            InitBuKaLabels();
            lblBuShuoMing.Text = "补卡步骤说明：" + System.Environment.NewLine + System.Environment.NewLine;
            lblBuShuoMing.Text += "1.在左侧人员列表点选或输入工号快速定位要补卡人员信息；" + System.Environment.NewLine + System.Environment.NewLine;
            lblBuShuoMing.Text += "2.输入新卡的卡号（打印在卡面上），将新卡放在读卡器上；" + System.Environment.NewLine + System.Environment.NewLine;
            lblBuShuoMing.Text += "3.点击补卡，查看弹出提示框，信息无误后点击确定；" + System.Environment.NewLine + System.Environment.NewLine;
            lblBuShuoMing.Text += "4.在程序提示补卡成功前务必不要移动读卡器和卡，不要关闭程序；" + System.Environment.NewLine + System.Environment.NewLine;
            lblBuShuoMing.Text += "5.补卡成功后再点击读卡，确认卡片信息无误。" ;
            cbBuKaLeiXing.Items.Clear();
            if (_chengwu)
            {
                cbBuKaLeiXing.Items.Add("乘务卡");

            }
            cbBuKaLeiXing.Items.Add("员工卡");
            cbBuKaLeiXing.SelectedIndex = 0;
            DataTable dt = _s.getRyxxFL(_bmmc);
            if (!dt.TableName.Equals("getRyxxFL"))
            {
                MessageBox.Show("获取人员信息失败。");
            }
             dgvBuRyxx.DataSource = dt;
             _buKaRen = new RenYuan();
        }

        private void btnBuQuery_Click(object sender, EventArgs e)
        {
            string str = txtBuQuery.Text.Trim();
            int index = -1;
            for (int i = 0; i < dgvBuRyxx.Rows.Count; i++)
            {
                if (dgvBuRyxx.Rows[i].Cells["gh"].Value.ToString().Equals(str))
                {
                    index = i;
                    break;
                }
            }
            if (index == -1)
            {
                MessageBox.Show("没有找到符合条件的数据。");
            }
            SelectBuRyxx(index, false);
        }

        private void SelectBuRyxx(int index, bool clicked)
        {
            _buKaRen.BuMen = dgvBuRyxx.Rows[index].Cells["bm"].Value.ToString();
            _buKaRen.GongHao = dgvBuRyxx.Rows[index].Cells["gh"].Value.ToString();
            _buKaRen.XingMing = dgvBuRyxx.Rows[index].Cells["xm"].Value.ToString();
            _buKaRen.KaHao = Convert.ToInt32(dgvBuRyxx.Rows[index].Cells["kh"].Value.ToString());
            lblBuBuMen.Text = "部门：" + dgvBuRyxx.Rows[index].Cells["bm"].Value.ToString();
            lblBuGongHao.Text = "工号：" + dgvBuRyxx.Rows[index].Cells["gh"].Value.ToString();
            lblBuKaHao.Text = "卡号：" + dgvBuRyxx.Rows[index].Cells["kh"].Value.ToString();
            lblBuXingMing.Text = "姓名：" + dgvBuRyxx.Rows[index].Cells["xm"].Value.ToString();
            dgvBuRyxx.Rows[index].Selected = true;
            if (clicked == false)
                dgvBuRyxx.FirstDisplayedScrollingRowIndex = index;
            dgvBuRyxx.Focus();
        }

        private void dgvBuRyxx_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
                SelectBuRyxx(e.RowIndex, true);
        }

        private void txtBuQuery_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnBuQuery_Click(this, new EventArgs());
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            InitBuKa();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

            string kh = _co.GetCardno();
            if (string.IsNullOrEmpty(kh))
            {
                MessageBox.Show("未将卡片放在读卡器上或该卡片已损坏。");
                return;
            }
            DataTable dt = _s.getCustomerInfoByCardNo(kh);
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("读卡错误或该卡信息已失效。");
                return;
            }
            lblBuBuMen.Text = "部门：" + dt.Rows[0]["custdept"].ToString();
            lblBuGongHao.Text = "工号：" + dt.Rows[0]["workno"].ToString();
            lblBuKaHao.Text = "卡号：" + dt.Rows[0]["cardno"].ToString();
            lblBuXingMing.Text = "姓名：" + dt.Rows[0]["workname"].ToString();
        }

        private void btnBuComfirm_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(_buKaRen.GongHao))
            {
                MessageBox.Show("请先选择补卡人信息。");
                return;
            }
            string strKh = txtBuNewKh.Text.Trim();
            int iKh;
            try
            {
                iKh = Convert.ToInt32(strKh);
            }
            catch
            {
                MessageBox.Show("请输入正确格式的卡号。");
                return;
            }
            if (iKh == 1)
            {
                MessageBox.Show("请输入正确格式的卡号。");
                return;
            }
            if (_s.existKh(iKh))
            {
                MessageBox.Show("该卡号已经被使用。");
                return;
            }
            string cardno = _co.GetCardno();
            if (string.IsNullOrEmpty(cardno))
            {
                MessageBox.Show("未将卡片放在读卡器上或该卡片已损坏。");
                return;
            }
            try
            {
                if (Convert.ToInt32(cardno) != 0)
                {
                    MessageBox.Show("该卡片已经使用，无法写入信息。");
                    return;
                }
            }
            catch
            {
                MessageBox.Show("卡片格式损坏，无法使用。");
                return;
            }
            //return;
            DateTime yxq;
            if (cbBuKaLeiXing.SelectedItem.ToString().Equals("乘务卡"))
            {
                yxq = _s.getNewValidDate();
            }
            else
            {
                yxq = _s.getNewValidDateFL();
            }
            string messageStr = "即将为" + _buKaRen.BuMen + "，" + _buKaRen.XingMing + "(" + _buKaRen.GongHao + ")补新卡，卡号为：" + strKh + "，请确认信息无误。";
            if (MessageBox.Show(messageStr, "补卡", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                if (_s.setNewCardFL(_buKaRen.GongHao, _buKaRen.XingMing, _buKaRen.BuMen, _buKaRen.KaHao, iKh, yxq, _user, _ipAddr) == true)
                {
                    if (_co.NewCard(iKh, _buKaRen.XingMing, yxq) == true && _co.ClearMoney() == true)
                    {
                        MessageBox.Show("补卡成功，请读卡验证卡片信息。");
                        InitBuKa();
                    }
                    else
                    {
                        MessageBox.Show("写卡信息出现错误，请检查读卡器，联系网络中心解决。");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("办理新卡错误，请检查数据库。");
                    return;
                }
            }
            else
            {
                InitBuKa();
            }
        }

        #endregion

        #region 开卡
        private void ToolStripMenuItemKaiKa_Click(object sender, EventArgs e)
        {

            InitKaiKa();
        }

        private void InitNextKaiKa()
        {
            //kaiKaREn.ZhuangTai = "正常";
            // dgvKaiRyxx.SelectedRows[0].Cells["kaiZt"].Value = 32;
            
            //int index = dgvKaiRyxx.SelectedRows[0].Index + 1;
            //if (index >= dgvKaiRyxx.Rows.Count)
            //{
            //    MessageBox.Show("已经到达最后一条人员信息。");
            //    return;
            //}
            //selectKaiRyxx(dgvKaiRyxx.SelectedRows[0].Index + 1, true);
            txtKaiKaHao.Focus();
            txtKaiKaHao.SelectAll();
        }

        private void InitKaiKa()
        {
            ToolStripMenuItemBuKa.Checked = false;
            ToolStripMenuItemKaiKa.Checked = true;
            InitKaiKaLabels();
            DataTable dt = _s.getRyxxFLKai(_bmmc,chkKaiAllRyxx.Checked);
            if (!dt.TableName.Equals("getRyxxFLKai"))
            {
                MessageBox.Show("获取人员信息失败。");
            }
            dgvKaiRyxx.DataSource = dt;
            _kaiKaREn = new RenYuan();
        }

        private void InitKaiKaLabels()
        {
            grpKai.BringToFront();
            chkKaiLianXu.Checked = true;
            cbKaiKaLeiXing.Items.Clear();
            if (_chengwu)
            {
                cbKaiKaLeiXing.Items.Add("乘务卡");

            }
            cbKaiKaLeiXing.Items.Add("员工卡");
            cbKaiKaLeiXing.SelectedIndex = 0;
            lblKaiYuE.ForeColor = Color.Black;
        }

        private void SelectKaiRyxx(int index, bool clicked)
        {
            if (index < 0) return;
            _kaiKaREn.BuMen = dgvKaiRyxx.Rows[index].Cells["kaiBm"].Value.ToString();
            _kaiKaREn.GongHao = dgvKaiRyxx.Rows[index].Cells["kaiGh"].Value.ToString();
            _kaiKaREn.XingMing = dgvKaiRyxx.Rows[index].Cells["kaiXm"].Value.ToString();
            _kaiKaREn.KaHao = Convert.ToInt32(dgvKaiRyxx.Rows[index].Cells["kaiKh"].Value.ToString());
            _kaiKaREn.YuE = Convert.ToDouble(dgvKaiRyxx.Rows[index].Cells["kaiYe"].Value);
            if (dgvKaiRyxx.Rows[index].Cells["kaiYxq"].Value.ToString().Equals(""))
            {
                _kaiKaREn.YouXiaoQi = "N/A";
            }
            else
            {
                _kaiKaREn.YouXiaoQi = Convert.ToDateTime(dgvKaiRyxx.Rows[index].Cells["kaiYxq"].Value).ToString("yyyy-MM-dd");
            }

            switch (Convert.ToInt32(dgvKaiRyxx.Rows[index].Cells["kaiZt"].Value))
            {
                case 0:
                _kaiKaREn.ZhuangTai = "未发卡";
                    break;

                case 32:
                    _kaiKaREn.ZhuangTai = "正常";
                    break;
                default:
                    _kaiKaREn.ZhuangTai = "异常";
                    break;
            }
            
            
                
            lblKaiBuMen.Text = "部门：" + _kaiKaREn.BuMen;
            lblKaiGongHao.Text = "工号：" + _kaiKaREn.GongHao;
            txtKaiKaHao.Text = _kaiKaREn.KaHao.ToString().PadLeft(6,'0');
            lblKaiXingMing.Text = "姓名：" + _kaiKaREn.XingMing;

            lblKaiZhuangTai.Text = "状态：" + _kaiKaREn.ZhuangTai;
            lblKaiYuE.Text = "余额：" + Math.Round ( _kaiKaREn.YuE,2).ToString();
            lblKaiYouXiaoQi.Text = "有效期：" + _kaiKaREn.YouXiaoQi;
            dgvKaiRyxx.Rows[index].Selected = true;
            if (clicked == false)
                dgvKaiRyxx.FirstDisplayedScrollingRowIndex = index;
            dgvKaiRyxx.Focus();
            btnKaiOK.Focus();
        }

        private void dgvKaiRyxx_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (dgvKaiRyxx.Rows[e.RowIndex].Cells["kaiZt"].Value.ToString().Equals("0"))
            {
                e.CellStyle.ForeColor = Color.Red;
            }
            else
            {
                e.CellStyle.ForeColor = Color.Green;
            }
        }

        private void btnKaiShuaXin_Click(object sender, EventArgs e)
        {
            InitKaiKa();
        }

        private void dgvKaiRyxx_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
                SelectKaiRyxx(e.RowIndex, true);
        }

        private void btnKaiDingWei_Click(object sender, EventArgs e)
        {
            string str = txtKaiKaHao.Text.Trim();
            if (str.Length == 2)
            {
                if (str.Equals("00"))
                {
                    str = (_kaiKaREn.KaHao + 100).ToString().Substring(0, 4) + str;
                }
                else

                {
                str = _kaiKaREn.KaHao.ToString().Substring(0, 4) + str;
                }
            }
            txtKaiKaHao.Text = str;
            int index = -1;
            for (int i = 0; i < dgvKaiRyxx.Rows.Count; i++)
            {
                if (dgvKaiRyxx.Rows[i].Cells["kaiKh"].Value.ToString().Equals(str))
                {
                    index = i;
                    break;
                }
            }
            if (index == -1)
            {
                MessageBox.Show("没有找到符合条件的数据。");
                return;
            }
            SelectKaiRyxx(index, false);
        }


        private void txtKaiKaHao_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnKaiDingWei_Click(this, new EventArgs());
            }
        }

        private void btnKaiOK_Click(object sender, EventArgs e)
        {


            if (String.IsNullOrEmpty(_kaiKaREn.GongHao))
            {
                MessageBox.Show("请先选择开卡人信息。");
                return;
            }
            if (!_kaiKaREn.ZhuangTai.Equals("未发卡"))
            {
                MessageBox.Show("不能重复开卡。");
                return;
            }
            string cardno = _co.GetCardno();
            //if (string.IsNullOrEmpty(cardno))
            //{
            //    MessageBox.Show("未将卡片放在读卡器上或该卡片已损坏。");
            //    return;
            //}
            try
            {
                if (!string.IsNullOrEmpty(cardno))
                {
                    if (!cardno.Equals("000000"))
                    {
                    MessageBox.Show("该卡片已经使用，无法写入信息。");
                    return;
                    }
                }
            }
            catch
            {
                MessageBox.Show("卡片格式损坏，无法使用。");
                return;
            }
            DateTime yxq;
            if (_kaiKaREn.YouXiaoQi.Equals("N/A"))
            {
                if (cbBuKaLeiXing.SelectedItem.ToString().Equals("乘务卡"))
                {
                    yxq = _s.getNewValidDate();
                }
                else
                {
                    yxq = _s.getNewValidDateFL();
                }
            }
            else
            {
                yxq = Convert.ToDateTime(_kaiKaREn.YouXiaoQi);
            }
            string messageStr = "即将为" + _kaiKaREn.BuMen + "，" + _kaiKaREn.XingMing + "(" + _kaiKaREn.GongHao + ")开卡，卡号为：" + _kaiKaREn.KaHao.ToString().PadLeft(6,'0') + "，初始金额：" + _kaiKaREn.YuE + "元， 请确认信息无误。";


            if (MessageBox.Show(messageStr, "开卡", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                if (chkChuShiHua.Checked)
                {
                    if (_co.InitCard() == false)
                    {
                        MessageBox.Show("初始化卡失败。");
                        //initKaiKa();
                        return;
                    }
                }
                if (_s.KaiXinKaFLExists(_kaiKaREn.KaHao, _user, _ipAddr) == true)
                {

                    if (_co.NewCard(_kaiKaREn.KaHao, _kaiKaREn.XingMing, yxq) == true && _co.ClearMoney() == true && _co.AddMoney(_kaiKaREn.YuE))
                    {
                        MessageBox.Show("开卡成功，请读卡验证卡片信息。");
                        if (chkKaiLianXu.Checked)
                        {
                            InitNextKaiKa();
                        }
                        else
                        {
                            InitKaiKa();
                        }
                    }
                    else
                    {
                        MessageBox.Show("写卡信息出现错误，请检查读卡器，联系网络中心解决。");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("开新卡错误，请检查数据库。");
                    return;
                }
            }
            else
            {
                InitKaiKa();
            }
        }

        private void btnKaiDu_Click(object sender, EventArgs e)
        {
            string kh = _co.GetCardno();
          
            
            if (string.IsNullOrEmpty(kh) )
            {
                MessageBox.Show("未将卡片放在读卡器上或该卡片已损坏。");
                return;
            }
            DataTable dt = _s.getCustomerInfoByCardNo(kh);
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("读卡错误或该卡信息已失效。");
                return;
            }
            txtKaiKaHao.Text = kh;
            lblKaiBuMen.Text = "部门：" + dt.Rows[0]["custdept"].ToString();
            lblKaiGongHao.Text = "工号：" + dt.Rows[0]["workno"].ToString();
            lblKaiXingMing.Text = "姓名：" + dt.Rows[0]["workname"].ToString();
            lblKaiZhuangTai.Text = "状态：" + ( Convert.ToInt32(dt.Rows[0]["state"]) == 32?"正常":"异常");
            
            double ye1 = _co.GetMoney();
            double ye2 = Convert.ToDouble(dt.Rows[0]["money"]);
            lblKaiYuE.Text = "余额：" +ye1;
            if (ye1 != ye2) lblKaiYuE.ForeColor = Color.Red;
            lblKaiYouXiaoQi.Text = "有效期：" + _co.GetValidDate().AddDays(-2).ToString("yyyy-MM-dd");
        }


        private void lblKaiZhuangTai_TextChanged(object sender, EventArgs e)
        {
            switch (lblKaiZhuangTai.Text)
            {
                case "状态：":
                    lblKaiZhuangTai.ForeColor = Color.Black;
                    break;
                case "状态：正常":
                    lblKaiZhuangTai.ForeColor = Color.Green;
                    break;
                default:
                    lblKaiZhuangTai.ForeColor = Color.Red;
                    break;
            }
        }
        #endregion

        #region 充值


        private void InitChongZhi()
        {
            InitChongZhiLabels();
            _co.SetType(CardType.YuanGong);
        }

        private void InitChongZhiLabels()
        {
            grpChong.BringToFront();
            txtChongJe.Text = "";
            txtChongGh.Text = "";
            txtChongId.Text = "";
            txtChongZhiJinE.Text = "";
            txtChongBm.Text = "";
            txtChongXm.Text = "";
            txtChongZhiXinYouXiaoQi.Text = "";
            txtChongKh.Text = "";
            txtChongYxq.Text = "";
            txtChongZhiBeiZhu.Text = "";
        }
        #endregion









       

        private void ToolStripMenuItemExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_canClose == true)
            {
                e.Cancel = false;
                return;
            }
            if (MessageBox.Show("确定要退出程序吗？", "退出", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                e.Cancel = false;
            }
        }

        #region 充值

        private bool SetNewValidOnly(string workNo, DateTime newValidDate)
        {

            //MessageBox.Show(this, "根据管理规定，该卡有效期有误，点击确定重新设置有效期，该操作不会清楚卡内余额。", "延续有效期", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            if (_co.SetValidDate(newValidDate) == false)
            {
                MessageBox.Show(this, "写卡错误，写入新有效期失败，请联系管理员！", "操作错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
            {
                if (_s.setValidDateOnly(workNo, newValidDate) == false)
                {
                    MessageBox.Show(this, "写入数据库时发生错误，请联系管理员！", "操作错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                else
                {
                    return true;
                }
            }

        }


        /// <summary>
        /// 检查一卡通有效期是否过期
        /// </summary>
        /// <param name="noUseDate">卡内有效期</param>
        /// <returns>-1:有效期异常，需更新有效期；0:已到期，需要更新有效期；2：快到有效期，需要提示更新有效期；1:无需更新有效期</returns>
        private int CheckValid(DateTime noUseDate)
        {
            DateTime now = _s.getServerTime();
            if (noUseDate == Convert.ToDateTime("1900-1-1"))
            {
                return -1;
            }
            if (now > noUseDate)
            {
                return 0;//需要更新有效期
            }
            else if (now > Convert.ToDateTime(noUseDate.ToString("yyyy-MM") + "-1"))
            {
                return 2;//提示更新有效期
            }
            else if (now < Convert.ToDateTime(noUseDate.ToString("yyyy-MM") + "-1").AddMonths(-1))//现在的日期小于有效期前一个月，如现在2013-2-4，有效期是2013-4-5日
            {
                return -1;//有效期异常
            }
            else
            {
                return 1;//有效期正常
            }


        }















        private void btnReadInfo_Click(object sender, EventArgs e)
        {
            //init();


            
            CustomerInfo ci = new CustomerInfo();

            string cardno = "";
            try
            {
                cardno = _co.GetCardno();
            }
            catch
            {
                MessageBox.Show(this, "初始化读卡器失败,读卡器未正确安装。", "核心错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(cardno) )
            {
                MessageBox.Show(this, "无法检测到卡片，请检查卡片是否放在读卡器上或读卡器是否正确连接。", "读卡错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {


                ci.CardNo = cardno;

                if (ci.getFullInfo() == true)
                {

                    double iMoney = _co.GetMoney();
                    txtChongGh.Text = ci.Work_no;
                    txtChongXm.Text = ci.Work_name;
                    txtChongBm.Text = ci.CustDept;
                    txtChongJe.Text = iMoney.ToString();
                    txtChongYxq.Text = ci.NoUseDate.ToString("yyyy-MM-dd");
                    
                    //int t = CheckValid(ci.NoUseDate);
                    //if (t == 0)
                    //{
                    //    MessageBox.Show(this, "该卡已经过期，请先延长有效期。", "卡过期", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //    return;
                    //}
                    //else if (t == 2)
                    //{
                    //    MessageBox.Show(this, "该卡即将过期，请先将卡内余额消费完，延长有效期后再充值。", "卡过期", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //    return;
                    //}
                    //else if (t == -1)
                    //{
                    //    SetNewValidOnly(ci.Work_no ,_s.getNewValidDateFL());
                        
                    //    return;
                    //}
                    //else 

                    DataTable dt = _s.getChongZhi(ci.CardNo);
                    if (!dt.TableName.Equals("getChongZhi"))
                    {
                        MessageBox.Show(this, "无法连接数据库服务器，获取报单信息错误。", "网络错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show(this, "没有找到补贴记录。", "数据为空", MessageBoxButtons.OK);
                        return;

                    }
                    else
                    {
                        txtChongZhiJinE.Text = dt.Rows[0]["je"].ToString();
                        txtChongZhiBeiZhu.Text = dt.Rows[0]["bz"].ToString();
                        txtChongZhiXinYouXiaoQi.Text = Convert.ToDateTime(dt.Rows[0]["yxq"]).ToString("yyyy-MM-dd");
                        txtChongKh.Text = cardno;
                        txtChongId.Text = dt.Rows[0]["id"].ToString();
                    }
                }
                else
                {
                    MessageBox.Show(this, "读卡失败，该卡片无效。", "读卡错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnWrite_Click(object sender, EventArgs e)
        {
            string cardno = "";
            try
            {
                cardno = _co.GetCardno();
            }
            catch
            {
                MessageBox.Show(this, "初始化读卡器失败,读卡器未正确安装。", "核心错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(cardno))
            {
                MessageBox.Show(this, "无法检测到卡片，请检查卡片是否放在读卡器上或读卡器是否正确连接。", "读卡错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!cardno.Equals(txtChongKh.Text))
            {
                MessageBox.Show(this, "充值前请先读卡。", "读卡错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (Convert.ToDouble(txtChongJe.Text) > 0)
            {
                if (MessageBox.Show("卡上还有余额" + txtChongJe.Text + "元，充值操作将对现有余额清零，请确认是否充值", "充值", MessageBoxButtons.YesNo) !=
                    System.Windows.Forms.DialogResult.Yes)
                {
                    return;
                }
            }

            MessageBox.Show("在程序提示充值成功前请务必不要移动卡片，否则将有可能导致充值失败。", "充值");
    
            

            if (!_s.setFLMoneyReceived(Convert.ToInt32(cardno), Convert.ToInt32(txtChongGh.Text),Convert.ToInt32(txtChongId.Text), Convert.ToDouble(txtChongJe.Text),
                Convert.ToDouble(txtChongZhiJinE.Text), Convert.ToDateTime(txtChongZhiXinYouXiaoQi.Text), this._user, this._ipAddr))
            {
                MessageBox.Show(this, "充值失败，操作数据库失败。", "数据错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!_co.ClearMoney())
            {
                MessageBox.Show(this, "清卡金额失败。", "写卡错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!_co.SetValidDate(Convert.ToDateTime(txtChongZhiXinYouXiaoQi.Text)))
            {
                MessageBox.Show(this, "写卡有效期失败。", "写卡错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            if (!_co.AddMoney(Convert.ToDouble(txtChongZhiJinE.Text)))
            {
                MessageBox.Show(this, "写卡金额失败。", "写卡错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            MessageBox.Show("充值成功，本次充值金额" + txtChongZhiJinE.Text + "元");
            InitChongZhiLabels();
            
        }









        #endregion

        private void cbBuKaLeiXing_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (cbBuKaLeiXing.Text.Equals("乘务卡"))
            {
                _co.SetType(CardType.CanBu);
            }
            else
            {
                _co.SetType(CardType.YuanGong);
            }
        }

        private void cbKaiKaLeiXing_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbKaiKaLeiXing.Text.Equals("乘务卡"))
            {
                _co.SetType(CardType.CanBu);
            }
            else
            {
                _co.SetType(CardType.YuanGong);
            }
        }
    }
}
