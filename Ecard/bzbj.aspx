<%@ Page Language="C#" AutoEventWireup="true" CodeFile="bzbj.aspx.cs" Inherits="bzbj" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title></title>
    <link href="css/reset.css" rel="stylesheet" type="text/css" />
    <link href="css/common.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript" src="js/WebCalendar.js"></script>
</head>
<body style="background-color: #F7Feff">
    <form id="form1" runat="server">
   
    <div align="center">
        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
            BorderStyle="Solid" BorderWidth="2px" CaptionAlign="Right" CellPadding="4"
            ForeColor="#333333" Width="100%" PageSize="20" 
            onpageindexchanging="GridView1_PageIndexChanging" 
            onrowdeleting="GridView1_RowDeleting">
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerTemplate>
                <table width="100%">
                    <tr>
                        <td align="left" width="30%">
                            第<asp:Label ID="lblcurPage" runat="server" Text='<%# ((GridView)Container.Parent.Parent).PageIndex+1      %>'></asp:Label>页/共<asp:Label
                                ID="lblPageCount" runat="server" Text='<%# ((GridView)Container.Parent.Parent).PageCount %>'></asp:Label>页
                        </td>
                        <td align="center" width="40%">
                            <asp:LinkButton ID="cmdFirstPage" runat="server" CommandArgument="First" CommandName="Page"
                                Enabled="<%# ((GridView)Container.Parent.Parent).PageIndex!=0 %>">首页</asp:LinkButton>
                            <asp:LinkButton ID="cmdPreview" runat="server" CommandArgument="Prev" CommandName="Page"
                                Enabled="<%# ((GridView)Container.Parent.Parent).PageIndex!=0 %>">前页</asp:LinkButton>
                            <asp:LinkButton ID="cmdNext" runat="server" CommandArgument="Next" CommandName="Page"
                                Enabled="<%# ((GridView)Container.Parent.Parent).PageIndex!=((GridView)Container.Parent.Parent).PageCount-1 %>">后页</asp:LinkButton>
                            <asp:LinkButton ID="cmdLastPage" runat="server" CommandArgument="Last" CommandName="Page"
                                Enabled="<%# ((GridView)Container.Parent.Parent).PageIndex!=((GridView)Container.Parent.Parent).PageCount-1 %>">尾页</asp:LinkButton>
                            转第<asp:TextBox ID="txtGoPage" runat="server" CssClass="inputmini" Text='<%# ((GridView)Container.Parent.Parent).PageIndex+1 %>'
                                Width="32px"></asp:TextBox><asp:Button ID="Button1" runat="server" OnClick="Button1_Click"
                                    Text="页" />
                        </td>
                    </tr>
                </table>
            </PagerTemplate>
            <PagerStyle HorizontalAlign="Center" />
            <EmptyDataTemplate>
                未找到符合条件的数据
            </EmptyDataTemplate>
            <SelectedRowStyle BackColor="#6699CC" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#6699CC" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="Azure" />
            <Columns>
                <asp:BoundField DataField="addtime" HeaderText="导入时间" />
                <asp:BoundField DataField="work_no" HeaderText="工号" />
                <asp:BoundField DataField="work_name" HeaderText="姓名" />
                <asp:BoundField DataField="account" HeaderText="金额" />
                <asp:BoundField DataField="type" HeaderText="备注" />
               

               
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:HyperLink ID="HyperLink1" runat="server" 
                          OnClientClick="return confirm('确认删除吗？')" NavigateUrl='<%# Eval("id", "bzbj.aspx?id={0}") %>' Text="删除"></asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
               

               
            </Columns>
        </asp:GridView>
    </div>
    </form>
</body>
</html>
