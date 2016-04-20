<%@ page language="C#" autoeventwireup="true" inherits="grcx, App_Web_xtkx2qhk" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title></title>
    <link href="css/reset.css" rel="stylesheet" type="text/css" />
    <link href="css/common.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript" src="js/WebCalendar.js"></script>
</head>
<body style=" background-color :#F7Feff ">
    <form id="form1" runat="server">
        


                            <hr align="center" color="#ff0000" noshade="noshade" width="96%" />
     <table align="center"  width="300">
                                <tr>
                                    <td align="left"  valign="middle">
                                        开始时间：<asp:TextBox ID="txtStart" runat="server"  onclick="SelectDate(this,'yyyy-MM-dd hh:mm')"></asp:TextBox>
                                    </td>

                                             <td align="center"><asp:Button ID="Button2" runat="server" Text="查  询" OnClick="Button2_Click" /></td>
                                </tr>
                                <tr> <td align="left"  valign="middle" >
                                        截至时间：<asp:TextBox ID="txtEnd" runat="server"  onclick="SelectDate(this,'yyyy-MM-dd hh:mm')"></asp:TextBox>
                                    </td><td align="center">
                                    <asp:Button runat="server" ID="saveBtn" Text="导  出" OnClick="saveBtn_Click" />
                                    </td></tr>
                            </table>
                            <hr align="center" color="#00cc00" noshade="noshade" width="96%" />
                            <div align="center">     
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
            BorderStyle="Solid" BorderWidth="2px" CaptionAlign="Right" CellPadding="4"
             ForeColor="#333333"
            Width="90%"  PageSize="25" >
     
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
       
            <PagerStyle HorizontalAlign="Center" />

            <SelectedRowStyle BackColor="#6699CC" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#6699CC" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="Azure" />
            <Columns>
            <asp:BoundField DataField="xh" HeaderText="序 号" >
                <ItemStyle Width="20%" />
                </asp:BoundField>
                <asp:BoundField DataField="zm" HeaderText="单   位" >
                <ItemStyle Width="40%" />
                </asp:BoundField>
                <asp:BoundField DataField="xfje" HeaderText="消 费 金 额" >
                <ItemStyle Width="40%" />
                </asp:BoundField>
            </Columns>

        </asp:GridView></div>
       
                     </form>
</body>
</html>
