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
public partial class cqffcx : System.Web.UI.Page
{
	private EcardService.Service ser = new EcardService.Service();
    protected void Page_Load(object sender, EventArgs e)
    {
		Security s = Session["sec"] as Security;
		if (s == null)
		{
			Response.Redirect("error.aspx");
		}


		
    }



	void binddata()
	{
		string strStart = txtStart.Text ;
		string strEnd = txtEnd.Text;
		DataTable dt = ser.getCZJL(strStart, strEnd, Session["udept"] as string);
		dt.Columns.Add("xh", typeof(int));
		int allCount = 0;
		double allMoney = 0;
		for (int i = 0; i < dt.Rows.Count; i++)
		{
			dt.Rows[i]["xh"] = i + 1;
			allCount++;
			allMoney += Convert.ToDouble(dt.Rows[i]["lqje"]);
		}
		GridView1.DataSource = dt;
		GridView1.DataBind();
		GridView1.Caption = "共计充值" + allCount.ToString() + "次，金额" + allMoney.ToString("C") + "元";
	}


    protected void saveBtn_Click(object sender, EventArgs e)
    {
		object missing = System.Reflection.Missing.Value;
		string fujian = "att3.xls";

		string strDept = Session["udept"] as string;



		Excel.Application m_objExcel = null;
		Excel.Workbook m_objBook = null;
		Excel.Worksheet m_objSheet = null;
		m_objExcel = new Excel.Application();
		try
		{



			m_objBook = (Excel.Workbook)m_objExcel.Workbooks.Open(HttpContext.Current.Request.PhysicalApplicationPath + fujian, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing);
			m_objSheet = (Excel.Worksheet)(((Excel.Sheets)m_objBook.Worksheets).get_Item(1));
			string sYear = txtEnd.Text.Substring(0, 4);
			string sMonth = txtEnd.Text.Substring(5, 2);


			if (!strDept.Equals("All"))
			{
				m_objSheet.Cells[3, 1] = "发放地点：" + strDept;
				m_objSheet.Cells[2, 1] = strDept + sYear + "年" + sMonth + "月机车乘务员就餐券发放登记簿";
			}
			else
			{
				m_objSheet.Cells[2, 1] = "新乡机务段" + sYear + "年" + sMonth + "月机车乘务员就餐券发放登记簿";
			}

			string strStart = txtStart.Text;
			string strEnd = txtEnd.Text;
			DataTable dt = ser.getCZJL(strStart, strEnd, strDept);
			dt.Columns.Add("xh", typeof(string));
			double allCount = 0;
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				dt.Rows[i]["xh"] = (i + 1);
				if (dt.Rows[i]["lqje"].ToString() != "") allCount += Convert.ToDouble(dt.Rows[i]["lqje"]);
				else dt.Rows[i]["lqje"] = 0;
			}

			Excel.Range r;
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				m_objSheet.Cells[5 + i, 1] = dt.Rows[i]["xh"];
				r = (Excel.Range)m_objSheet.Cells[5 + i, 1];
				r.Font.Size = 12;
				r.Font.Name = "宋体";
				r.Font.Underline = false;
				r.Font.Strikethrough = false;
				r.Font.Bold = false;
				r.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
				r.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
				r.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;


				m_objSheet.Cells[5 + i, 2] = dt.Rows[i]["lqrq"];
				r = (Excel.Range)m_objSheet.Cells[5 + i, 2];
				r.Font.Size = 12;
				r.Font.Name = "宋体";
				r.Font.Underline = false;
				r.Font.Strikethrough = false;
				r.Font.Bold = false;
				r.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
				r.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
				r.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;


				m_objSheet.Cells[5 + i, 3] = dt.Rows[i]["roadway"];
				r = (Excel.Range)m_objSheet.Cells[5 + i, 3];
				r.Font.Size = 12;
				r.Font.Name = "宋体";
				r.Font.Underline = false;
				r.Font.Strikethrough = false;
				r.Font.Bold = false;
				r.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
				r.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
				r.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;

				m_objSheet.Cells[5 + i, 4] = dt.Rows[i]["lqsj"];
				r = (Excel.Range)m_objSheet.Cells[5 + i, 4];
				r.Font.Size = 12;
				r.Font.Name = "宋体";
				r.Font.Underline = false;
				r.Font.Strikethrough = false;
				r.Font.Bold = false;
				r.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
				r.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
				r.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;



				m_objSheet.Cells[5 + i, 5] = dt.Rows[i]["sjxm"];
				r = (Excel.Range)m_objSheet.Cells[5 + i, 5];
				r.Font.Size = 12;
				r.Font.Name = "宋体";
				r.Font.Underline = false;
				r.Font.Strikethrough = false;
				r.Font.Bold = false;
				r.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
				r.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
				r.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;


				m_objSheet.Cells[5 + i, 6] = dt.Rows[i]["xxjwd"];
				r = (Excel.Range)m_objSheet.Cells[5 + i, 6];
				r.Font.Size = 12;
				r.Font.Name = "宋体";
				r.Font.Underline = false;
				r.Font.Strikethrough = false;
				r.Font.Bold = false;
				r.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
				r.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
				r.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;


				m_objSheet.Cells[5 + i, 7] = dt.Rows[i]["lqje"];
				r = (Excel.Range)m_objSheet.Cells[5 + i, 7];
				r.Font.Size = 12;
				r.Font.Name = "宋体";
				r.Font.Underline = false;
				r.Font.Strikethrough = false;
				r.Font.Bold = false;
				r.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
				r.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
				r.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
				r.NumberFormatLocal = "0.00";

				m_objSheet.Cells[5 + i, 8] = dt.Rows[i]["dept"];
				r = (Excel.Range)m_objSheet.Cells[5 + i, 8];
				r.Font.Size = 12;
				r.Font.Name = "宋体";
				r.Font.Underline = false;
				r.Font.Strikethrough = false;
				r.Font.Bold = false;
				r.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
				r.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
				r.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
			}





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




	protected void GridView1_DataBound(object sender, EventArgs e)
	{

	}
	protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
	{

	}
	protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
	{
		
	}
	protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		GridView1.PageIndex = e.NewPageIndex;
		binddata();
		
	}
	protected void Button1_Click(object sender, EventArgs e)
	{

		TextBox tb_gopage = (TextBox)GridView1.BottomPagerRow.Cells[0].FindControl("txtGoPage");
		try
		{
			GridView1.PageIndex = Convert.ToInt32(tb_gopage.Text) - 1;
		}
		catch
		{
			Response.Write(" <script> alert( '请输入正确的数字！ ') </script> ");
		}
	}
	protected void Button2_Click(object sender, EventArgs e)
	{
		binddata();
	}
}
