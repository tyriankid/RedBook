<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="updateNewUserRecharge.aspx.cs" Inherits="RedBookPlatform.operative.updateNewUserRecharge" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>拉新充值送红包活动</title>
    <link rel="stylesheet" type="text/css" href="/Resources/css/comm.css"/>
        <script src="/Resources/js/jquery.min.js"></script>
    <script src="/Resources/laydate/laydate.dev.js"></script>
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
                                <asp:TextBox ID="tboxTitle" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style1">充值金额</td>
                            <td>
                                <asp:TextBox ID="tboxAmount" runat="server" onKeyUp="value=value.replace(/\D/g,'')"></asp:TextBox>元
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style1">时段</td>
                            <td>
                               <asp:TextBox ID="tboxStarttime" runat="server"  onclick="laydate({ istime: true, format: 'YYYY-MM-DD hh:mm:ss' })"></asp:TextBox>—<asp:TextBox ID="tboxEndtime" runat="server" onclick="laydate({ istime: true, format: 'YYYY-MM-DD hh:mm:ss' })"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style1">参与次数</td>
                            <td>
                                <asp:TextBox ID="tboxCount" runat="server" onKeyUp="value=value.replace(/\D/g,'')"></asp:TextBox>次
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style1">红包有效天数</td>
                            <td>
                                <asp:TextBox ID="tboxRedpackday" runat="server" onKeyUp="value=value.replace(/\D/g,'')"></asp:TextBox>天
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style1">发放红包</td>
                            <td>
                                <table class="table table-striped tabel-textalign">
                                    <tr><td rowspan="3" class="auto-style2">
                                充值后立得
                                        </td><td>
                                            <asp:DropDownList ID="DropDownList1" runat="server">
                                            </asp:DropDownList>&nbsp;&nbsp;/&nbsp;&nbsp;
                                            <asp:DropDownList ID="DropDownList2" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                       </tr>
                                    <tr><td style="text-align:left">
                                        <asp:DropDownList ID="DropDownList3" runat="server">
                                        </asp:DropDownList>&nbsp;&nbsp;/&nbsp;&nbsp;
                                        <asp:DropDownList ID="DropDownList4" runat="server">
                                        </asp:DropDownList>
                                        </td></tr>
                                    <tr><td style="text-align:left">
                                        <asp:DropDownList ID="DropDownList5" runat="server">
                                        </asp:DropDownList>&nbsp;&nbsp;/&nbsp;&nbsp;
                                        <asp:DropDownList ID="DropDownList6" runat="server">
                                        </asp:DropDownList>
                                        </td></tr>
                                    
                                  <tr><td class="auto-style2">第二天</td>
                                      <td>
                                          <asp:DropDownList ID="DropDownList7" runat="server">
                                          </asp:DropDownList>&nbsp;&nbsp;/&nbsp;&nbsp;
                                          <asp:DropDownList ID="DropDownList8" runat="server">
                                          </asp:DropDownList>
                                      </td></tr>
                                  <tr><td class="auto-style2">第四天</td>
                                      <td>
                                          <asp:DropDownList ID="DropDownList9" runat="server">
                                          </asp:DropDownList>&nbsp;&nbsp;/&nbsp;&nbsp;
                                          <asp:DropDownList ID="DropDownList10" runat="server">
                                          </asp:DropDownList>
                                      </td></tr>
                                  <tr><td class="auto-style2">第2周周一</td>
                                      <td>
                                          <asp:DropDownList ID="DropDownList11" runat="server">
                                          </asp:DropDownList>&nbsp;&nbsp;/&nbsp;&nbsp;
                                          <asp:DropDownList ID="DropDownList12" runat="server">
                                          </asp:DropDownList>
                                      </td></tr>
                                  <tr><td class="auto-style2">第3周周一</td>
                                      <td>
                                          <asp:DropDownList ID="DropDownList13" runat="server">
                                          </asp:DropDownList>&nbsp;&nbsp;/&nbsp;&nbsp;
                                          <asp:DropDownList ID="DropDownList14" runat="server">
                                          </asp:DropDownList>
                                      </td></tr>
                                  <tr><td class="auto-style2">第4周周一</td>
                                      <td>
                                          <asp:DropDownList ID="DropDownList15" runat="server">
                                          </asp:DropDownList>&nbsp;&nbsp;/&nbsp;&nbsp;
                                          <asp:DropDownList ID="DropDownList16" runat="server">
                                          </asp:DropDownList>
                                      </td></tr>
                                  <tr><td class="auto-style2">第5周周一</td>
                                      <td>
                                          <asp:DropDownList ID="DropDownList17" runat="server">
                                          </asp:DropDownList>&nbsp;&nbsp;/&nbsp;&nbsp;
                                          <asp:DropDownList ID="DropDownList18" runat="server">
                                          </asp:DropDownList>
                                      </td></tr>
                                  <tr><td class="auto-style2">第6周周一</td>
                                      <td>
                                          <asp:DropDownList ID="DropDownList19" runat="server">
                                          </asp:DropDownList>&nbsp;&nbsp;/&nbsp;&nbsp;
                                          <asp:DropDownList ID="DropDownList20" runat="server">
                                          </asp:DropDownList>
                                      </td></tr>
                                  <tr><td class="auto-style2">第7周周一</td>
                                      <td>
                                          <asp:DropDownList ID="DropDownList21" runat="server">
                                          </asp:DropDownList>&nbsp;&nbsp;/&nbsp;&nbsp;
                                          <asp:DropDownList ID="DropDownList22" runat="server">
                                          </asp:DropDownList>
                                      </td></tr>
                                  <tr><td class="auto-style2">第8周周一</td>
                                      <td>
                                          <asp:DropDownList ID="DropDownList23" runat="server">
                                          </asp:DropDownList>&nbsp;&nbsp;/&nbsp;&nbsp;
                                          <asp:DropDownList ID="DropDownList24" runat="server">
                                          </asp:DropDownList>
                                      </td></tr>
                                  <tr><td class="auto-style2">第9周周一</td>
                                      <td>
                                          <asp:DropDownList ID="DropDownList25" runat="server">
                                          </asp:DropDownList>&nbsp;&nbsp;/&nbsp;&nbsp;
                                          <asp:DropDownList ID="DropDownList26" runat="server">
                                          </asp:DropDownList>
                                      </td></tr>
                                  <tr><td class="auto-style2">第10周周一</td>
                                      <td>
                                          <asp:DropDownList ID="DropDownList27" runat="server">
                                          </asp:DropDownList>&nbsp;&nbsp;/&nbsp;&nbsp;
                                          <asp:DropDownList ID="DropDownList28" runat="server">
                                          </asp:DropDownList>
                                      </td></tr>
                                  <tr><td class="auto-style2">第11周周一</td>
                                      <td>
                                          <asp:DropDownList ID="DropDownList29" runat="server">
                                          </asp:DropDownList>&nbsp;&nbsp;/&nbsp;&nbsp;
                                          <asp:DropDownList ID="DropDownList30" runat="server">
                                          </asp:DropDownList>
                                      </td></tr>
                                  <tr><td class="auto-style2">第12周周一</td>
                                      <td>
                                          <asp:DropDownList ID="DropDownList31" runat="server">
                                          </asp:DropDownList>&nbsp;&nbsp;/&nbsp;&nbsp;
                                          <asp:DropDownList ID="DropDownList32" runat="server">
                                          </asp:DropDownList>
                                      </td></tr>
                                  <tr><td class="auto-style2">第13周周一</td>
                                      <td>
                                          <asp:DropDownList ID="DropDownList33" runat="server">
                                          </asp:DropDownList>&nbsp;&nbsp;/&nbsp;&nbsp;
                                          <asp:DropDownList ID="DropDownList34" runat="server">
                                          </asp:DropDownList>
                                      </td></tr>
                                  <tr><td class="auto-style2">第14周周一</td>
                                      <td>
                                          <asp:DropDownList ID="DropDownList35" runat="server">
                                          </asp:DropDownList>&nbsp;&nbsp;/&nbsp;&nbsp;
                                          <asp:DropDownList ID="DropDownList36" runat="server">
                                          </asp:DropDownList>
                                      </td></tr>
                                  <tr><td class="auto-style2">第15周周一</td>
                                      <td>
                                          <asp:DropDownList ID="DropDownList37" runat="server">
                                          </asp:DropDownList>&nbsp;&nbsp;/&nbsp;&nbsp;
                                          <asp:DropDownList ID="DropDownList38" runat="server">
                                          </asp:DropDownList>
                                      </td></tr>
                                       
                                </table>
                            </td>
                        </tr>
                        
                        <tr>
                            <td class="auto-style1">用户显示信息</td>
                            <td>
                                <asp:TextBox ID="tboxUsercodeinfo" runat="server"></asp:TextBox>例：“（首付红包）”
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align:center">
                                 <asp:Button runat="server" ID="btnSave"  OnClientClick="return validation();" class="btn btn-primary" Text="保存" OnClick="btnSave_Click" />&nbsp;&nbsp;
                                <asp:Button runat="server" ID="returnlast"  class="btn btn-primary" Text="返回" OnClick="returnlast_Click" />
                            </td>
                        </tr>
                    </tbody>
                </table>
                    </form></div>
</body>
            <script type="text/javascript">
                function validation() {
                    var flag = true;
                    var msg = "";
                    if ($("#tboxTitle").val().length == 0) {
                        msg = "请输入标题";
                        flag = false;
                    }
                    if ($("#tboxRedpackday").val().length == 0) {
                        msg = "请输入有效天数";
                        flag = false;
                    }
                    if ($("#tboxAmount").val().length == 0) {
                        msg = "请输入充值金额";
                        flag = false;
                    }
                    if ($("#tboxStarttime").val().length == 0) {
                        msg = "请输入开始时间";
                        flag = false;
                    }
                    if ($("#tboxEndtime").val().length == 0) {
                        msg = "请输入结束时间";
                        flag = false;
                    }
                    if (!flag) { alert(msg); }
                    return flag;

                }
        </script>
</html>
