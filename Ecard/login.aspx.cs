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

public partial class login : System.Web.UI.Page
{
	private EcardService.Service ser = new EcardService.Service();
    protected void Page_Load(object sender, EventArgs e)
    {

    }




	protected void loginButton_Click ( object sender , EventArgs e )
	{
       

		pbModule pm = new pbModule();
		string uCode = txtUcode.Text.Trim();
		string uPass = txtUpass.Text.Trim();
		if ( !pm.isValidString( uCode) || !pm.isValidString( uPass ) )
		{
			errors.Text = "请输入格式正确的用户名和密码！";
			return;
		}
           
        int result = 0;
        Security s = new Security();
        string url = "mdefault.aspx";
        result = ser.LoginWeb(uCode, uPass);
      
		switch (result)
		{
			case 1://管理员
			case 2://超级用户
			case 3://一般用户
			case 4://查询用户
				url = "mdefault.aspx";

				break;
		
			default:
				errors.Text = "用户名或密码错误，请检查后重新输入！";
				txtUpass.Text = "";
                return;
				break;


		}
		s.setSecurity(result);
		s.setUserCode(uCode);
		s.setUserDept(ser.getUserDept(uCode));
		//s.setUserId(dm.getUIdByUCode(uCode));
		s.setUserGroup(ser.getUserGroup(uCode));
        if (result == -1) return;
        Session["sec"] = s;
		Session["usec"] = s.getSecurity();
		Session["usercode"] = s.getUserCode();
		Session["udept"] = s.getUserDept();
		Session["uGroup"] = s.getUserGroup();
        Response.Redirect(url);
	}
}
