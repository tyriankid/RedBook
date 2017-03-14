<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="addmember.aspx.cs" Inherits="RedBookPlatform.member.addmember" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
		<meta charset="UTF-8">
		<title>添加用户</title>
		<link rel="stylesheet" type="text/css" href="/Resources/css/comm.css"/>
        <script src="../Resources/js/jquery.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
		<div class="wrapper">
			<div class="crumbs">
				<ul id="breadcrumbs" class="breadcrumb"> 
	                <li><a href="#">主页</a></li>
	                <li><a href="#">管理员管理</a></li>
	                <li class="active"><a href="#" title=""><%--<%=active %>--%></a></li>
	            </ul>
			</div>
		    <div class="content">
		    	<table class="table table-striped">

                    <tbody>
                        <tr>
                            <td>昵称：</td>
                            <td><input type="text" id="UserName" runat="server" name="UserName" /></td>
                        </tr>
          
                        <tr>
                            <td>邮箱：</td>
                            <td><input type="text" id="Email" name="Email" runat="server"/></td>
                        </tr>
                        <tr>
                            <td>手机：</td>
                            <td><input type="text" id="Mobile" name="Mobile" runat="server"/></td>
                        </tr>
                        <tr>
                            <td>密码：</td>
                            <td><input type="password" id="Pass" name="Pass"  runat="server"/>&nbsp;不填写，默认为原始密码！（123456）</td>
                        </tr>
                        <tr>
                            <td>账户金额：</td>
                            <td><input type="text" id="Money" name="Money" value="0"  runat="server"/>&nbsp;元</td>
                        </tr>
                        <tr>
                            <td>经验值：</td>
                            <td><input type="text" id="Jingyan" disabled="disabled" name="Jingyan" value="0" runat="server"/></td>
                        </tr>
                        <tr>
                            <td>积分：</td>
                            <td><input type="text" id="Score" disabled="disabled" name="Score" value="0"  runat="server"/></td>
                        </tr>
                         <tr>
                            <td>签名：</td>
                            <td><textarea rows="5" cols="5" name="Qianming" id="Qianming" runat="server"  class="span12" style="width: 50%;"></textarea></td>
                        </tr>
                        <tr>
                            <td>用户权限：</td>
                            <td><asp:DropDownList ID="Groupid" runat="server" Width="100"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td>手机认证：</td>
                            <td>
                                <asp:RadioButtonList ID="Mobilecode" runat="server" RepeatLayout="UnorderedList" CssClass="choosebox">
                                    <asp:ListItem Value="0" Selected="True">已认证</asp:ListItem>
                                    <asp:ListItem Value="1">未认证</asp:ListItem>
                                </asp:RadioButtonList></td>
                        </tr>
                         <tr>
                            <td>App唤醒：</td>
                            <td>  
                                <asp:RadioButtonList ID="Approuse" runat="server" RepeatLayout="UnorderedList" CssClass="choosebox">
                                    <asp:ListItem Value="0" Selected="True">已认证</asp:ListItem>
                                    <asp:ListItem Value="1">未认证</asp:ListItem>
                                </asp:RadioButtonList></td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                 <asp:Button runat="server" ID="btnSave" Text="保 存" OnClientClick="return validation();"   CssClass="btn btn-primary" OnClick="btnSave_Click" />
                            </td>
                        </tr>
                    </tbody>
                </table>
		    </div>
		</div>
        <script type="text/javascript">
            var editor = new baidu.editor.ui.Editor();
            editor.render("content");
            function validation() {
                var flag = true;
                var msg = "";
                //分类和品牌不能为未选中
                if ($("#UserName").val() == "") {
                    flag = false; msg = "请填写昵称!";
                }
                if ($("#Money").val() == "")
                {
                    flag = false; msg = "请填写金额!";
                }
                if (!flag) { alert(msg); }
                return flag;
            }
        </script>
        <script type="text/javascript">

            $("#Money").blur(function () {
                if ($("#Money").val().search("^-?\\d+$") == 0) {
                }
                else {
                    $("#Money").val("0");
                }
            });
        </script>
    </form>
</body>
</html>
