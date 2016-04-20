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

public partial class xgmm : System.Web.UI.Page
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
    protected void Button1_Click(object sender, EventArgs e)
    {
       
        pbModule pm = new pbModule();
        string oldpass = oldpassTextbox.Text.Trim();
        string newpass1 = newpassTextbox1.Text.Trim();
        string newpass2 = newpassTextbox2.Text.Trim();
        if ( !pm.isValidString( oldpass) || !pm.isValidString( newpass1 )||!pm.isValidString(newpass2) )
		{
			errors.Text = "密码格式不正确！";
			return;
		}

        if (pm.isNullString(newpass1))
        {
			errors.Text = "";
        }
        Security s1 = Session["sec"] as Security;
        if (s1 == null)
        {
            errors.Text = "sorry";
            return;
        }
        string username = s1.getUserCode();
        int uid = s1.getUserId();
        if (ser.Login ( username , oldpass )==true)
        {
            if (newpass1==newpass2)
            {
				String sqlstr = "update qx set password='" + newpass1 + "' where username= '" + username + "'";
                      
                if (ser.updatePasswd(sqlstr)==true)
                    Response.Write("密码修改成功！");
                else
                    Response.Write("密码修改失败！");
               
            }
            else
               Response.Write("两次密码输入不一致！");
        }
        else
            Response.Write("旧密码错误！");
    }

    //private int adminLogin(string p, string oldpass)
    //{
    //    throw new Exception("The method or operation is not implemented.");
    //}
    protected void TextBox3_TextChanged(object sender, EventArgs e)
    {

    }
}
