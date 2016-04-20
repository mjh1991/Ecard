<%@ page language="C#" autoeventwireup="true" inherits="ssxfmx, App_Web_xtkx2qhk" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title></title>
    <link href="css/reset.css" rel="stylesheet" type="text/css" />
    <link href="css/common.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript" src="js/DateClock.js"></script>
    <meta http-equiv="refresh" content="5" />
    </head>
<body style=" background-color :#F7Feff ">
    <form id="form1" runat="server">
     
                            <hr align="center" color="#ff0000" noshade="noshade" width="96%" />
       
                                <div width="100%" align="center"><asp:DropDownList ID="ddlDept" runat="server" AutoPostBack="True" 
                                                onselectedindexchanged="ddlDept_SelectedIndexChanged">
                                            </asp:DropDownList></div>
                                            

                            <hr align="center" color="#00cc00" noshade="noshade" width="96%" />
                           <div width="100%" align="center">
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
            BorderStyle="Solid" CaptionAlign="Right" CellPadding="0"
             ForeColor="#333333"
            Width="90%"  PageSize="15" >
     
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
          
            <PagerStyle HorizontalAlign="Center" />
            <EmptyDataTemplate>
               未找到符合条件的数据
            </EmptyDataTemplate>
            <SelectedRowStyle BackColor="#6699CC" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#6699CC" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="Azure" />
            <Columns>
         
                <asp:BoundField DataField="lqrq" HeaderText="消费日期" />
                <asp:BoundField DataField="lqsj" HeaderText="消费时间" />
                <asp:BoundField DataField="xfdd" HeaderText="消费地点" />
                <asp:BoundField DataField="sjxm" HeaderText="值乘司机" />
                <asp:BoundField DataField="dept" HeaderText="车间" />
                <asp:TemplateField HeaderText="金额">
                    <ItemTemplate>
                        <%#toNumberGroup(Convert.ToDouble(Eval("lqje"))) %>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>

        </asp:GridView>
        </div>
                     </form>
</body>
</html>
