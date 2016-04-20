<%@ page language="C#" autoeventwireup="true" inherits="bzdr, App_Web_xtkx2qhk" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div>
            &nbsp; &nbsp;
        </div>
       <div height="700px" align="center" valign="middle">
        <table><tr height="200px"><td></td></tr></table>    
        <table width="550" style="border-left-color: aqua;  border-bottom-color: aqua; border-top-style: solid; border-top-color: aqua; border-right-style: solid; border-left-style: solid; border-right-color: aqua; border-bottom-style: solid; font-size: 12px;" border="1">
            <tr>

                <td align="center">
            <asp:Label ID="lblMessage" runat="server" ForeColor="red" Text=""></asp:Label></td>

            </tr>
           <%-- <tr>

                <td align="left">
             选择要导入的项目：<asp:TextBox ID="txtBat" runat="server"></asp:TextBox></td>

            </tr>--%>
            <tr>
                <td align="left">
                    选择要导入的文件：<asp:FileUpload ID="FileUpload1" runat="server" />&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;<asp:Button ID="btnDaoRu" runat="server" OnClick="btnDaoRu_Click" Text="开始导入" /></td>
                
            </tr><%--
                        <tr>
              
            <td>
                &nbsp; &nbsp; &nbsp;
                
                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                <asp:Button ID="btnDaoChuCuoWu" runat="server" Text="导出错误信息" OnClick="btnDaoChuCuoWu_Click" /> </td>
                </tr>--%>
                
        </table>
    </div>
    </div>
    </form>
</body>
</html>
