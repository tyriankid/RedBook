<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="tuanbuy.aspx.cs" Inherits="RedBookPlatform.operative.tuanbuy" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>团购不中送红包</title>
    <link rel="stylesheet" type="text/css" href="/Resources/css/comm.css"/>
        <script src="/Resources/js/jquery.min.js"></script>
    <script src="/Resources/laydate/laydate.dev.js"></script>
    <style type="text/css">
        .auto-style1 {
            width: 187px;
        }
        </style>
</head>
<body>
    <div class="wrapper">
			<div class="crumbs">
				<ul id="breadcrumbs" class="breadcrumb"> 
	                <li><a href="#">主页</a></li>
	                <li><a href="javascript:void(0);">运营活动</a></li>
	                <li class="active"><a href="javascript:void(0);" title=""><asp:Literal runat="server" Text="团购不中送红包" ID="litTitle"></asp:Literal></a></li>
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
                            <td class="auto-style1">时段</td>
                            <td>
                               <asp:TextBox ID="tboxStarttime" runat="server" onclick="laydate({ istime: true, format: 'YYYY-MM-DD hh:mm:ss' })"></asp:TextBox>—<asp:TextBox ID="tboxEndtime" runat="server" onclick="laydate({ istime: true, format: 'YYYY-MM-DD hh:mm:ss' })"></asp:TextBox>
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
                                          <asp:DropDownList ID="DropDownList1" runat="server">
                                          </asp:DropDownList>&nbsp;&nbsp;/&nbsp;&nbsp;
                                          <asp:DropDownList ID="DropDownList2" runat="server">
                                          </asp:DropDownList>
                                      </td></tr>
                                  
                        <tr>
                            <td class="auto-style1">用户显示信息</td>
                            <td>
                                <asp:TextBox ID="tboxUsercodeinfo" runat="server"></asp:TextBox>例：“（首付红包）”
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align:center">
                                 <asp:Button runat="server" ID="btnSave" OnClientClick="return validation();" class="btn btn-primary" Text="保存" OnClick="btnSave_Click" />&nbsp;&nbsp;
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
              if (!flag) { alert(msg); }
              return flag;

          }
        </script>
</html>
