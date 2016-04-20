<%@ Page Language="C#" AutoEventWireup="true" CodeFile="cqffcx.aspx.cs" Inherits="cqffcx" %>

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
                                        ��ʼʱ�䣺<asp:TextBox ID="txtStart" runat="server"  onclick="SelectDate(this,'yyyy-MM-dd hh:mm')"></asp:TextBox>
                                    </td>

                                             <td align="center"><asp:Button ID="Button2" runat="server" Text="��  ѯ" OnClick="Button2_Click" /></td>
                                </tr>
                                <tr> <td align="left"  valign="middle" >
                                        ����ʱ�䣺<asp:TextBox ID="txtEnd" runat="server"  onclick="SelectDate(this,'yyyy-MM-dd hh:mm')"></asp:TextBox>
                                    </td><td align="center">
                                    <asp:Button runat="server" ID="saveBtn" Text="��  ��" OnClick="saveBtn_Click" />
                                    </td></tr>
                            </table>
                            <hr align="center" color="#00cc00" noshade="noshade" width="96%" />
        <div align="center">
        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
            BorderStyle="Solid" BorderWidth="2px" CaptionAlign="Right" CellPadding="4"
             ForeColor="#333333"
            Width="90%" OnRowDataBound="GridView1_RowDataBound" 
                OnSorting="GridView1_Sorting" PageSize="20" 
                OnPageIndexChanging="GridView1_PageIndexChanging">
     
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerTemplate>
                <table width="100%">
                    <tr>
                        <td align="left" width="30%">
                            ��<asp:Label ID="lblcurPage" runat="server" Text='<%# ((GridView)Container.Parent.Parent).PageIndex+1      %>'></asp:Label>ҳ/��<asp:Label
                                ID="lblPageCount" runat="server" Text='<%# ((GridView)Container.Parent.Parent).PageCount %>'></asp:Label>ҳ</td>
                        <td align="center" width="40%">
                            <asp:LinkButton ID="cmdFirstPage" runat="server" CommandArgument="First" CommandName="Page"
                                Enabled="<%# ((GridView)Container.Parent.Parent).PageIndex!=0 %>">��ҳ</asp:LinkButton>
                            <asp:LinkButton ID="cmdPreview" runat="server" CommandArgument="Prev" CommandName="Page"
                                Enabled="<%# ((GridView)Container.Parent.Parent).PageIndex!=0 %>">ǰҳ</asp:LinkButton>
                            <asp:LinkButton ID="cmdNext" runat="server" CommandArgument="Next" CommandName="Page"
                                Enabled="<%# ((GridView)Container.Parent.Parent).PageIndex!=((GridView)Container.Parent.Parent).PageCount-1 %>">��ҳ</asp:LinkButton>
                            <asp:LinkButton ID="cmdLastPage" runat="server" CommandArgument="Last" CommandName="Page"
                                Enabled="<%# ((GridView)Container.Parent.Parent).PageIndex!=((GridView)Container.Parent.Parent).PageCount-1 %>">βҳ</asp:LinkButton>
                            ת��<asp:TextBox ID="txtGoPage" runat="server" CssClass="inputmini" Text='<%# ((GridView)Container.Parent.Parent).PageIndex+1 %>'
                                Width="32px"></asp:TextBox><asp:Button ID="Button1" runat="server" 
                                OnClick="Button1_Click"     Text="ҳ" /></td>
                    </tr>
                </table>
            </PagerTemplate>
            <PagerStyle HorizontalAlign="Center" />
            <EmptyDataTemplate>
               δ�ҵ���������������
            </EmptyDataTemplate>
            <SelectedRowStyle BackColor="#6699CC" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#6699CC" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="Azure" />
            <Columns>
            <asp:BoundField DataField="xh" HeaderText="���" ReadOnly="True" />
                <asp:BoundField DataField="lqrq" HeaderText="����" />
                <asp:BoundField DataField="roadway" HeaderText="����" />
                <asp:BoundField DataField="lqsj" HeaderText="����" />
                <asp:BoundField DataField="arrive_time" HeaderText="����" />
                <asp:BoundField DataField="sjxm" HeaderText="ֵ��˾��" />
                <asp:BoundField DataField="xxjwd" HeaderText="���������" />
                <asp:BoundField DataField="dept" HeaderText="����" />
                
                <asp:BoundField DataField="lqje" HeaderText="��ȡ���" />
                <asp:BoundField DataField="addtime" HeaderText="��ȡʱ��" />
                <asp:BoundField DataField="oper" HeaderText="������" />
            </Columns>

        </asp:GridView>
        </div>
                     </form>
</body>
</html>
