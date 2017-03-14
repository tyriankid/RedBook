<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="baseInformation.aspx.cs" Inherits="RedBookPlatform.channel.baseInformation" %>

<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8">
    <title>个人基本信息</title>

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
                    <li class="active"><a href="#" title="">个人基本信息</a></li>
                </ul>
            </div>
            <div class="content">
                <table class="table table-striped">

                    <tbody>
                        <tr>
                            <td>用户名:<asp:Label ID="lbUserName" runat="server" style="width: 50%;"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>服务商名称:<asp:Label ID="lbRealName" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>联系人:<asp:Label ID="lbContacts" runat="server"></asp:Label>&nbsp;&nbsp;联系手机：<asp:Label ID="lbUserMobile" runat="server"></asp:Label>&nbsp;&nbsp;联系邮箱：<asp:Label ID="lbUserEmail" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>所在城市:<asp:Label ID="lbProvince" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>返佣比例:<asp:Label ID="lbRebateratio" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>未提现金额:<asp:Label ID="lbNowithdrawcash" runat="server"></asp:Label>元</td>
                        </tr>
                        <tr>
                            <td>已提现金额:<asp:Label ID="lbWithdrawcash" runat="server"></asp:Label>元</td>
                        </tr>
                        <tr>
                            <td>推广用户数:<asp:Label ID="lbUserCount" runat="server"></asp:Label></td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    </form>
</body>
</html>
