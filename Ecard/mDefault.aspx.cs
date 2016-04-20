using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class mDefault : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		
		Security s = Session["sec"] as Security;
		if (s == null)
		{
			Response.Redirect("error.aspx");
		}

        int sec = Convert.ToInt32(Session["usec"]);
        switch (sec)
        {
            case 1:
                Label1.Text = "系统管理员";
                break;
            case 2:
                Label1.Text = "超级管理员";
                break;
            case 3:
                Label1.Text = "车间管理员";
                break;
            case 4:
                Label1.Text = "餐厅管理员";
                break;
            default:
                Response.Redirect("error.aspx");
                break;
        }
		int admin = Convert.ToInt32(Session["usec"]);
		Response.Write("<script language='javascript' type='text/javascript' src='js/madminMain.js' charset='GB2312'></script>");
		Response.Write("<script language='javascript' type='text/javascript'>window.onload = function() { init(" + admin.ToString() + ");}</script>");

    }
}
