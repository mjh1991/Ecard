using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Security 的摘要说明

/// </summary>
/// 

public class Security
{


	private int userID;
	private int isAdmin;
	private string userCode;
	private string userGroup;
	private string userDept;

	public Security (int uI, int i,string uC,string uN,string uD )
	{
		userID = uI;
		isAdmin = i;
		userCode = uC;
		userGroup = uN;
		userDept = uD;
	}

	public Security (int uI, string uC, string uN,string uD)
	{
		userID = uI;
		isAdmin = 0;
		userCode = uC;
		userGroup = uN;
		userDept = uD;
	}

	public Security(int uI,string uC, string uN)
	{
		userID = uI;
		isAdmin = 0;
		userCode = uC;
		userGroup = uN;
		userDept = "";
	}

	public Security ()
	{
		isAdmin = 0;
		userCode = "";
		userGroup = "";
		userDept = "";
		userID = 0;
	}
	public int getSecurity ()
	{
		return isAdmin;
	}

	public void setSecurity ( int c )
	{
		isAdmin = c;
	}

	public void setUserCode ( string c )
	{
		userCode = c;
	}

	public string getUserCode ()
	{
		return userCode;
	}

	public void setUserGroup ( string n )
	{
		userGroup = n;
	}

	public string getUserGroup ()
	{
		return userGroup;
	}

	public string getUserDept()
	{
		return userDept;
	}

	public void setUserDept(string n)
	{
		userDept = n;
	}

	public void setUserId(int uI)
	{
		userID = uI;
	}

	public int getUserId()
	{
		return userID;
	}
}
