<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="RedBookPlatform.channel.ChangePassword" %>

<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8">
    <title>修改密码</title>
    <link rel="stylesheet" type="text/css" href="/Resources/css/comm.css" />
    <script src="/Resources/js/jquery.min.js" type="text/javascript" charset="utf-8"></script>
    <script src="/Resources/js/comm.js" type="text/javascript" charset="utf-8"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="wrapper">
            <div class="crumbs">
                <ul id="breadcrumbs" class="breadcrumb">
                    <li><a href="#">主页</a></li>
                    <li><a href="#">系统设置</a></li>
                    <li class="active"><a href="#" title="">修改密码</a></li>
                </ul>
            </div>
            <div class="content">
                <table  class="table table-striped tabel-textalign" style="width:560px" style="width:560px">

                    <tbody>
                        <tr>
                            <td style="width:150px">用 户 名：</td>
                            <td ><%=this.UserName %></td>
                        </tr>
                        <%--关于 class="show" 为是否隐藏密码栏目，勿删--%>
                        <tr class="show">
                            <td>原始密码：</td>
                            <td>
                                <input id="OldPass" type="password" style="width: 50%;" /></td>
                        </tr>
                        <tr class="show">
                            <td>新 密 码：</td>
                            <td>
                                <input id="NewPass" type="password" style="width: 50%;" /></td>
                        </tr>
                        <tr>
                            <td>再次输入：</td>
                            <td>
                                <input id="okNewPass" type="password" style="width: 50%;" /></td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align: center">
                                <input type="button" onclick="javascript: Resetting()" value=" 保  存 " class="btn btn-primary" />
                            </td>
                        </tr>
                    </tbody>
                </table>


            </div>
        </div>
        <script type="text/javascript">
            function Resetting() {
                if ($("#NewPass").val() == "") {
                    alert("新密码不能为空");
                    return;
                }
                else if ($("#okNewPass").val() == "") {
                    alert("确认密码不能为空");
                    return;
                }
                if ($("#NewPass").val() == $("#okNewPass").val()) {
                    $.ajax({
                        type: 'post', dataType: 'json', timeout: 10000,
                        async: false,
                        url: location.href,
                        data: {
                            action: "ResettingPass",
                            Oldpass: $("#OldPass").val(),
                            NewPass: $("#NewPass").val()
                        },
                        success: function (resultData) {
                            if (resultData.success == true) {
                                alert(resultData.msg);
                                $("#OldPass").val("");
                                $("#NewPass").val("");
                                $("#okNewPass").val("");
                            }
                            if (resultData.success == false) {
                                alert(resultData.msg);
                            }
                        }
                    });
                }
                else {
                    alert("两次输入密码不一致")
                }
            }
        </script>
    </form>
</body>
</html>
