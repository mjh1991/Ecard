<%@ Page Language="C#" AutoEventWireup="true"   CodeFile="mDefault.aspx.cs" Inherits="mDefault" %>



<html>
<head runat="server">
    <title>�������γ���Ա�Ͳ�ϵͳ����ƽ̨</title>
    <link href="css/Style.css" rel="stylesheet" type="text/css" />
 <%--   <script language="javascript" type="text/javascript" src="js/madminMain.js" charset="GB2312"></script>--%>
<script language="javascript" type="text/javascript">
<!--
//window.onload = function() {
//	init(2);
//}
window.onresize = function() {
	if(!wResizeEnd) return false;
	wResizeEnd = false;
	resizeTime = window.setTimeout("reSize()", 50);
}
-->
</script>
</head>
<body>
    <form id="form1" runat="server">
<div id="wrap">
	        <!-- ���� -->
	        <div id="top">
		        <div id="top_left_img"></div>
	        </div>
	        <table width="100%" border="0" cellspacing="0" cellpadding="0" style="border: 1px solid #95B6CE;">
	          <tr>
			        <!-- ��� -->
			        <td id="left" class="left_border">
				        <div id="left_toolbar" class="toolbarBg">
					        <a href="mDefault.aspx" id="btnHome" rel="Home" target="main_data">������ҳ</a><span class="split ltSplit" ></span>
                            <a href="logout.aspx"  onclick="return confirm('ȷ���˳�ϵͳ��');">�˳�ϵͳ</a>				</div>
				        <!-- ��ർ���˵���ʼ -->
				        <div id="menuWarp">
					        <%--<div class="left_menu_open">
						        <div class="title toolbarBg"><a href="javascript:void(0);">ϵͳ��Ϣ</a></div>
						        <div class="link">
							        <ul>
								        <li>��Ȩ���У�K</li>
								        <li>���������K</li>
								        <li>����֧�֣�K</li>
							        </ul>
						        </div>
					        </div>--%>
					        <!-- ��ർ���˵����� -->
					        <div id="message"></div>
				        </div>
			        </td>
			        <td id="center"></td>
			        <!-- ���� -->
			        <td id="main" class="main_border">
				        <div id="main_toolbar" class="toolbarBg">
					        <div id="showDate" class="right"></div>
					        <a href="javascript:void(0);" onclick="goTo(this);" rel="showLeft" id="btnShowLeft" class="sh_left_open" title="������ർ���˵�"><span class="btnIco btnShLeft"></span><span id="manage_title">
                                <asp:Label ID="Label1" runat="server" Text=""></asp:Label></span></a>
				        </div>
				        <iframe src="" id="main_data" name="main_data" marginwidth="0" marginheight="0" frameborder="0" scrolling="no" onload="reSize();"></iframe>
				        <div id="footer" class="toolbarBg"><span class="left"> 2013 �������İ�Ȩ����.</span></div>
			        </td>
		        </tr>
	        </table>
        </div>
    </form>
</body>
</html>
