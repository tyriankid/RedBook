<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="newUserRecharge.aspx.cs" Inherits="RedBookPlatform.operative.newUserRecharge" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>拉新充值送红包活动</title>
    <link rel="stylesheet" type="text/css" href="/Resources/css/comm.css"/>
        <script src="/Resources/js/jquery.min.js"></script>
    <script src="/Resources/js/comm.js" type="text/javascript" charset="utf-8"></script>
    <style type="text/css">
        .auto-style1 {
            width: 187px;
        }
        .auto-style2 {
            width: 90px;
        }
    </style>
</head>
<body>
    <div class="wrapper">
			<div class="crumbs">
				<ul id="breadcrumbs" class="breadcrumb"> 
	                <li><a href="#">主页</a></li>
	                <li><a href="javascript:void(0);">充值拉新活动</a></li>
	                <li class="active"><a href="javascript:void(0);" title=""><asp:Literal runat="server" Text="查看活动详情" ID="litTitle"></asp:Literal></a></li>
	            </ul>
			</div></div>
        <div class="content">
    <form runat="server" id="form1">
		    	<table class="table table-striped tabel-textalign">
                    <tbody>
                        <tr>
                            <td class="auto-style1">标题</td>
                            <td>
                                <asp:Label ID="labTitle" runat="server" Text="Label"></asp:Label></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style1">充值金额</td>
                            <td>
                                <asp:Label ID="labAmount" runat="server" Text="Label"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style1">参与次数</td>
                            <td>
                                <asp:Label ID="labCount" runat="server" Text="Label"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style1">时段</td>
                            <td>
                                <asp:Label ID="labDatatime" runat="server" Text="Label"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style1">红包有效天数</td>
                            <td>
                                <asp:Label ID="labRedpackday" runat="server" Text="Label"></asp:Label>
                            </td>
                        </tr><tr>
                            <td class="auto-style1">用户显示信息</td>
                            <td>
                                <asp:Label ID="labUsercodeinfo" runat="server" Text="Label"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style1">发放红包</td>
                            <td>
                                <table class="table table-striped tabel-textalign">
                                    <tr><td rowspan="3" class="auto-style2">
                                充值后立得
                                        </td><td><asp:Label ID="labRed1" runat="server" Text=""></asp:Label>&nbsp;&nbsp;/&nbsp;&nbsp;<asp:Label ID="labRed2" runat="server" Text=""></asp:Label></td>
                                       </tr>
                                    <tr><td style="text-align:left"><asp:Label ID="labRed3" runat="server" Text=""></asp:Label>&nbsp;&nbsp;/&nbsp;&nbsp;<asp:Label ID="labRed4" runat="server" Text=""></asp:Label></td></tr>
                                    <tr><td style="text-align:left"><asp:Label ID="labRed5" runat="server" Text=""></asp:Label>&nbsp;&nbsp;/&nbsp;&nbsp;<asp:Label ID="labRed6" runat="server" Text=""></asp:Label></td></tr>
                                    
                                  <tr><td class="auto-style2">第二天</td>
                                      <td><asp:Label ID="labRed7" runat="server" Text=""></asp:Label>&nbsp;&nbsp;/&nbsp;&nbsp;<asp:Label ID="labRed8" runat="server" Text=""></asp:Label></td></tr>
                                  <tr><td class="auto-style2">第四天</td>
                                      <td><asp:Label ID="labRed9" runat="server" Text=""></asp:Label>&nbsp;&nbsp;/&nbsp;&nbsp;<asp:Label ID="labRed10" runat="server" Text=""></asp:Label></td></tr>
                                  <tr><td class="auto-style2">第2周周一</td>
                                      <td><asp:Label ID="labRed11" runat="server" Text=""></asp:Label>&nbsp;&nbsp;/&nbsp;&nbsp;<asp:Label ID="labRed12" runat="server" Text=""></asp:Label></td></tr>
                                  <tr><td class="auto-style2">第3周周一</td>
                                      <td><asp:Label ID="labRed13" runat="server" Text=""></asp:Label>&nbsp;&nbsp;/&nbsp;&nbsp;<asp:Label ID="labRed14" runat="server" Text=""></asp:Label></td></tr>
                                  <tr><td class="auto-style2">第4周周一</td>
                                      <td><asp:Label ID="labRed15" runat="server" Text=""></asp:Label>&nbsp;&nbsp;/&nbsp;&nbsp;<asp:Label ID="labRed16" runat="server" Text=""></asp:Label></td></tr>
                                  <tr><td class="auto-style2">第5周周一</td>
                                      <td><asp:Label ID="labRed17" runat="server" Text=""></asp:Label>&nbsp;&nbsp;/&nbsp;&nbsp;<asp:Label ID="labRed18" runat="server" Text=""></asp:Label></td></tr>
                                  <tr><td class="auto-style2">第6周周一</td>
                                      <td><asp:Label ID="labRed19" runat="server" Text=""></asp:Label>&nbsp;&nbsp;/&nbsp;&nbsp;<asp:Label ID="labRed20" runat="server" Text=""></asp:Label></td></tr>
                                  <tr><td class="auto-style2">第7周周一</td>
                                      <td><asp:Label ID="labRed21" runat="server" Text=""></asp:Label>&nbsp;&nbsp;/&nbsp;&nbsp;<asp:Label ID="labRed22" runat="server" Text=""></asp:Label></td></tr>
                                  <tr><td class="auto-style2">第8周周一</td>
                                      <td><asp:Label ID="labRed23" runat="server" Text=""></asp:Label>&nbsp;&nbsp;/&nbsp;&nbsp;<asp:Label ID="labRed24" runat="server" Text=""></asp:Label></td></tr>
                                  <tr><td class="auto-style2">第9周周一</td>
                                      <td><asp:Label ID="labRed25" runat="server" Text=""></asp:Label>&nbsp;&nbsp;/&nbsp;&nbsp;<asp:Label ID="labRed26" runat="server" Text=""></asp:Label></td></tr>
                                  <tr><td class="auto-style2">第10周周一</td>
                                      <td><asp:Label ID="labRed27" runat="server" Text=""></asp:Label>&nbsp;&nbsp;/&nbsp;&nbsp;<asp:Label ID="labRed28" runat="server" Text=""></asp:Label></td></tr>
                                  <tr><td class="auto-style2">第11周周一</td>
                                      <td><asp:Label ID="labRed29" runat="server" Text=""></asp:Label>&nbsp;&nbsp;/&nbsp;&nbsp;<asp:Label ID="labRed30" runat="server" Text=""></asp:Label></td></tr>
                                  <tr><td class="auto-style2">第12周周一</td>
                                      <td><asp:Label ID="labRed31" runat="server" Text=""></asp:Label>&nbsp;&nbsp;/&nbsp;&nbsp;<asp:Label ID="labRed32" runat="server" Text=""></asp:Label></td></tr>
                                  <tr><td class="auto-style2">第13周周一</td>
                                      <td><asp:Label ID="labRed33" runat="server" Text=""></asp:Label>&nbsp;&nbsp;/&nbsp;&nbsp;<asp:Label ID="labRed34" runat="server" Text=""></asp:Label></td></tr>
                                  <tr><td class="auto-style2">第14周周一</td>
                                      <td><asp:Label ID="labRed35" runat="server" Text=""></asp:Label>&nbsp;&nbsp;/&nbsp;&nbsp;<asp:Label ID="labRed36" runat="server" Text=""></asp:Label></td></tr>
                                  <tr><td class="auto-style2">第15周周一</td>
                                      <td><asp:Label ID="labRed37" runat="server" Text=""></asp:Label>&nbsp;&nbsp;/&nbsp;&nbsp;<asp:Label ID="labRed38" runat="server" Text=""></asp:Label></td></tr>
                                       
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align:center">
                                <a class="btn" href="javascript:;" onclick="openNewWin('/operative/updateNewUserRecharge.aspx')">编辑</a>
                            </td>
                        </tr>
                    </tbody>
                </table>
                    </form></div>
    <!-- <div id="divref" style="background:black; filter:alpha(opacity:30);opacity:0.3;position:absolute;padding-left:20px;padding-top:15px;display:none;"><font color="#ffffff">已刷新</font></div>-->
</body>
    <!--<script>
                $('#divref').css({
                    position: "fixed",
                    width: "80px",
                    height: "50px",
                    bottom: "200px",
                    left: "45%"
                }).show(100).delay(1000).hide(300);
    </script>-->
</html>
