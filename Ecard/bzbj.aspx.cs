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
public partial class bzbj : System.Web.UI.Page
{
	private EcardService.Service ser = new EcardService.Service();
    protected void Page_Load(object sender, EventArgs e)
    {

		Security s = Session["sec"] as Security;
		if (s == null)
		{
			Response.Redirect("error.aspx");
		}
		if (!IsPostBack)
		{
			binddata();
		}
		string id = Request["id"];
		if (id == "" || id == null)
		{
		}
		else
		{
			ser.deleteOtherRecordById(Convert.ToInt32(id));
			binddata();
		}
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

	void binddata()
	{

		DataTable dt = ser.getUnReceivedOther(Session["usercode"] as string);
		dt.Columns.Add("xh", typeof(int));
		
		GridView1.DataSource = dt;
		GridView1.DataBind();
		
	}

	protected void Button2_Click(object sender, EventArgs e)
	{
		binddata();
	}
	protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		GridView1.PageIndex = e.NewPageIndex;
		binddata();
	}
	protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
	{
		
	}
}
