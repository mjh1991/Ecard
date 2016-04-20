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
public partial class ssxfmx : System.Web.UI.Page
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
			DataTable dt = ser.getAllRecordByJh(ddlDept.SelectedItem.Value);
			GridView1.DataSource = dt;
			GridView1.DataBind();
		}

		

    }






	public string toNumberGroup(double d)
	{
		return d.ToString("n", System.Globalization.CultureInfo.CurrentUICulture.NumberFormat);
	}






	protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
	{
		Session["jihao"] = ddlDept.SelectedItem.Value;
		DataTable dt = ser.getAllRecordByJh(ddlDept.SelectedItem.Value);
		GridView1.DataSource = dt;
		GridView1.DataBind();
		
	}
}
