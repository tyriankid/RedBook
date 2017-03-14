<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="memberDetail.aspx.cs" Inherits="RedBookPlatform.member.memberDetail" %>

<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>
<!DOCTYPE html>

<html>
<head id="Head1" runat="server">
    <meta charset="UTF-8">
    <title>用户详情</title>
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
                    <li><a href="#">用户详情</a></li>
                </ul>
            </div>
            <div class="content">
                <span><a class="btn" href="javascript :"  onclick="javascript :history.back(-1);" >返回</a></span>
                <table class="table table-striped">
                    <tbody>
                        <tr>
                            <td>收货地址：</td>
                            <td >
                                <asp:Literal runat="server" ID="litDiZhi"></asp:Literal></td>
                        </tr>
                        <tr>
                            <td>邮编：</td>
                            <td>
                                <asp:Literal runat="server" ID="litEmail"></asp:Literal></td>
                        </tr>
                        <tr>
                            <td>收货人：</td>
                            <td>
                                <asp:Literal ID="litshouhuoren" runat="server"></asp:Literal></td>

                        </tr>
                        <tr>
                            <td>收货人手机：</td>
                            <td>
                                <asp:Literal ID="litMobile" runat="server"></asp:Literal></td>

                        </tr>
                        <tr>
                            <td>收货人电话：</td>
                            <td>
                                <asp:Literal ID="litTell" runat="server"></asp:Literal></td>

                        </tr>
                        <tr>
                            <td>收货人QQ：</td>
                            <td>
                                <asp:Literal ID="litQQ" runat="server"></asp:Literal></td>

                        </tr>

                        <tr>
                            <td>游戏收货人：</td>
                            <td>
                                <asp:Literal ID="litshouhuoren1" runat="server"></asp:Literal></td>
                        </tr>
                        <tr>
                            <td>游戏收货人手机：</td>
                            <td>
                                <asp:Literal ID="litMobile1" runat="server"></asp:Literal></td>
                        </tr>
                        <tr>
                            <td>游戏收货人电话</td>
                            <td>
                                <asp:Literal ID="litTell1" runat="server"></asp:Literal></td>

                        </tr>
                        <tr>
                            <td>游戏收货人QQ</td>
                            <td>
                                <asp:Literal ID="litQQ1" runat="server"></asp:Literal></td>
                        </tr>
                        <tr>
                            <td>游戏名：</td>
                            <td>
                                <asp:Literal ID="litGameName" runat="server"></asp:Literal></td>
                        </tr>
                        <tr>
                            <td>游戏大区：</td>

                            <td>
                                <asp:Literal ID="litGameZone" runat="server"></asp:Literal></td>
                        </tr>
                        <tr>
                            <td>游戏服务器：</td>

                            <td>
                                <asp:Literal ID="litServer" runat="server"></asp:Literal></td>
                        </tr>
                        <tr>
                            <td>游戏类型：</td>

                            <td>
                                <asp:Literal ID="litGameType" runat="server"></asp:Literal></td>

                        </tr>
                        <tr>
                            <td>游戏帐号：</td>

                            <td>
                                <asp:Literal ID="litgameAccount" runat="server"></asp:Literal></td>
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
                if ($("#Money").val() == "") {
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
