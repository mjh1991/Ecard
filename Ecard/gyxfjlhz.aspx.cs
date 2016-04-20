using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Text;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
public partial class gyxfjlhz : System.Web.UI.Page
{
	private EcardService.Service ser = new EcardService.Service();
	
    protected void Page_Load(object sender, EventArgs e)
    {
		Security s = Session["sec"] as Security;
		if (s == null)
		{
			Response.Redirect("error.aspx");
		}
		string usec = Session["usec"] as string;
		if (usec == "4")//餐厅查询
		{
			GridView1.Columns[1].HeaderText = "用餐单位";
		}
		else//段查询
		{
			GridView1.Columns[1].HeaderText = "单位";
		}

		if (!IsPostBack)
		{
			string groups = Session["uGroup"] as string;
			DataTable dtDdl = ser.getJhByGroup(groups);
			DataRow dr = dtDdl.NewRow();
			dr["jh"] = "000";
			dr["wz"] = "所有公寓食堂";
			dtDdl.Rows.InsertAt(dr, 0);
			ddlDept.DataValueField = "jh";
			ddlDept.DataTextField = "wz";
			ddlDept.DataSource = dtDdl;
			ddlDept.DataBind();
			string jh = Session["jihao"] as string;
			if (jh != null && jh != "")
			{
				ListItem li = ddlDept.Items.FindByValue(jh);
				if (li != null)
				{
					ddlDept.SelectedIndex = (ddlDept.Items.IndexOf(li));
				}
				else
				{
					ddlDept.SelectedIndex = 0;
				}
			}
		}
    }


	



    protected void saveBtn_Click(object sender, EventArgs e)
    {
		object missing = System.Reflection.Missing.Value;

		int usec = Convert.ToInt32(Session["usec"]) ;
		string fujian;
		if (usec == 4)//餐厅查询
		{
			fujian = "att5.xls";
		}
		else//段查询
		{
			fujian = "att6.xls";
		}
		Excel.Application m_objExcel = null;
		Excel.Workbook m_objBook = null;
		Excel.Worksheet m_objSheet = null;
		m_objExcel = new Excel.Application();
		try
		{



			m_objBook = (Excel.Workbook)m_objExcel.Workbooks.Open(HttpContext.Current.Request.PhysicalApplicationPath + fujian, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing);
			m_objSheet = (Excel.Worksheet)(((Excel.Sheets)m_objBook.Worksheets).get_Item(1));
			string sYear = txtStart.Text.Substring(0, 4);
			string sMonth = txtStart.Text.Substring(5, 2);
			string sDay = txtStart.Text.Substring(8, 2);
			string eYear = txtEnd.Text.Substring(0, 4);
			string eMonth = txtEnd.Text.Substring(5, 2);
			string eDay = txtEnd.Text.Substring(8, 2);
			m_objSheet.Cells[3, 3] = "起止日期：" + sYear + "年" + sMonth + "月" + sDay + "日 —— " + eYear + "年" + eMonth + "月" + eDay + "日";

			string strStart = txtStart.Text;
			string strEnd = txtEnd.Text;
			DataTable dt = ser.getXFHZ(strStart, strEnd, Session["uGroup"] as string);
			dt.Columns.Add("xh", typeof(string));
			double allCount = 0;
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				dt.Rows[i]["xh"] = (i + 1);
				if (dt.Rows[i]["xfje"].ToString() != "") allCount += Convert.ToDouble(dt.Rows[i]["xfje"]);
				else dt.Rows[i]["xfje"] = 0;
			}
			DataRow dr = dt.NewRow();
			dr["zm"] = "总计：";
			dr["xfje"] = allCount;
			dt.Rows.Add(dr);
			Excel.Range r;
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				m_objSheet.Cells[ 5 + i,1] = dt.Rows[i]["xh"];
				r = (Excel.Range)m_objSheet.Cells[5 + i, 1];
				r.Font.Size = 12;
				r.Font.Name = "宋体";
				r.Font.Underline = false;
				r.Font.Strikethrough = false;
				r.Font.Bold = false;
				r.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
				r.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
				r.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
				m_objSheet.Cells[ 5 + i,2] = dt.Rows[i]["zm"];
				r = (Excel.Range)m_objSheet.Cells[5 + i, 2];
				r.Font.Size = 12;
				r.Font.Name = "宋体";
				r.Font.Underline = false;
				r.Font.Strikethrough = false;
				r.Font.Bold = false;
				r.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
				r.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
				r.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
				m_objSheet.Cells[ 5 + i,3] = dt.Rows[i]["xfje"];
				r = (Excel.Range)m_objSheet.Cells[5 + i, 3];
				r.Font.Size = 12;
				r.Font.Name = "宋体";
				r.Font.Underline = false;
				r.Font.Strikethrough = false;
				r.Font.Bold = false;
				r.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
				r.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
				r.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
				r.NumberFormatLocal = "0.00";
			}

			m_objSheet.Cells[5 + dt.Rows.Count + 1, 1] = "经办人（签字）：";
			r = (Excel.Range)m_objSheet.Cells[5 + dt.Rows.Count + 1, 1];
			r.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
			r.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
			r.Font.Size = 12;
			r.Font.Name = "宋体";
			r.Font.Underline = false;
			r.Font.Strikethrough = false;
			r.Font.Bold = false;



			m_objSheet.Cells[5 + dt.Rows.Count + 1, 2] = "财务科长（签字）：";
			r = (Excel.Range)m_objSheet.Cells[5 + dt.Rows.Count + 1, 2];
			r.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
			r.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
			r.Font.Size = 12;
			r.Font.Name = "宋体";
			r.Font.Underline = false;
			r.Font.Strikethrough = false;
			r.Font.Bold = false;


			if (usec == 2)
			{
				m_objSheet.Cells[5 + dt.Rows.Count + 1, 3] = "段长（签字）：";
				r = (Excel.Range)m_objSheet.Cells[5 + dt.Rows.Count + 1, 3];
				r.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
				r.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
				
				r.Font.Size = 12;
				r.Font.Name = "宋体";
				r.Font.Underline = false;
				r.Font.Strikethrough = false;
				r.Font.Bold = false;
			}
			m_objSheet.Cells[5 + dt.Rows.Count + 3, 1] = "填报日期：";
			r = (Excel.Range)m_objSheet.Cells[5 + dt.Rows.Count + 3, 1];
			r.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
			r.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
			r.Font.Size = 12;
			r.Font.Name = "宋体";
			r.Font.Underline = false;
			r.Font.Strikethrough = false;
			r.Font.Bold = false;



			string FileSaveAsName = HttpContext.Current.Request.PhysicalApplicationPath + "downExcel\\" + fujian;
			FileInfo f = new FileInfo(FileSaveAsName);
			if (f.Exists) f.Delete();
			m_objBook.SaveAs(FileSaveAsName, Excel.XlFileFormat.xlWorkbookNormal,
				missing, missing, missing, missing, Excel.XlSaveAsAccessMode.xlNoChange, missing, missing,
				missing, missing, missing);
		}
		finally
		{
			m_objBook.Close(false, missing, missing);
			m_objExcel.Quit();
			System.Runtime.InteropServices.Marshal.ReleaseComObject(m_objSheet);
			System.Runtime.InteropServices.Marshal.ReleaseComObject(m_objBook);
			System.Runtime.InteropServices.Marshal.ReleaseComObject(m_objExcel);
			GC.Collect();
		}
		Response.ContentType = "application/x-msdownload";
		string filename = "attachment;filename=" + fujian;
		Response.AddHeader("Content-Disposition", filename);
		Response.TransmitFile(HttpContext.Current.Request.PhysicalApplicationPath + "downExcel\\" + fujian);

    }



	protected void Button2_Click(object sender, EventArgs e)
	{
		string strStart = txtStart.Text  ;
		string strEnd = txtEnd.Text  ;
		DataTable dt = ser.getGYXFHZ(strStart, strEnd, Session["uGroup"] as string,ddlDept.SelectedItem.Value);
		dt.Columns.Add("xh", typeof(string));
		double allCount = 0;
		for (int i = 0; i < dt.Rows.Count; i++)
		{
			dt.Rows[i]["xh"] = (i + 1);
			if (dt.Rows[i]["xfje"].ToString() != "") allCount += Convert.ToDouble(dt.Rows[i]["xfje"]);
			else dt.Rows[i]["xfje"] = 0;
		}
		DataRow dr = dt.NewRow();
		dr["xh"] = "总计：";
		dr["xfje"] = allCount;
		dt.Rows.Add(dr);
		GridView1.DataSource = dt;
		GridView1.DataBind();
	}
}
