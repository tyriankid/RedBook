<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="spreadLink.aspx.cs" Inherits="RedBookPlatform.channel.spreadLink" %>

<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8">
    <title>推广链接</title>

    <link rel="stylesheet" type="text/css" href="/Resources/css/comm.css" />
    <script src="/Resources/js/jquery.min.js" type="text/javascript" charset="utf-8"></script>
    <script src="/Resources/js/comm.js" type="text/javascript" charset="utf-8"></script>
    <script src="../Resources/js/clipboard.min.js"></script>
    <style type="text/css">
        .auto-style1 { width: 100%; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="wrapper">
            <div class="crumbs">
                <ul id="breadcrumbs" class="breadcrumb">
                    <li><a href="#">主页</a></li>
                    <li><a href="#">系统设置</a></li>
                    <li class="active"><a href="#" title="">推广链接</a></li>
                </ul>
            </div>
            <div class="content">
                <table class="table table-striped">
                    <tr>
                        <td>复制链接邀请好友赚提成！</td>
                    </tr>
                    <tr>
                        <td style="width:70%">
                          <input id="txtLink" runat="server"  type="text" style="width:90%"/></td>
                        <td>
                         <button class="btn" data-clipboard-action="copy" data-clipboard-target="#txtLink">复制链接</button></td>
                        <td>
                            <asp:Button runat="server" ID="btnDown" Text="下载推广码" CssClass="btn"  OnClick="btnDown_Click" /></td>

                    </tr>
                    <asp:Image ID="ImQrCode" runat="server" />
                    <tr>

                    </tr>
                </table>
            </div>
        </div>
    </form>

    <script>
        var clipboard = new Clipboard('.btn');
        clipboard.on('success', function (e) {
            alert('复制成功');
            console.log(e);
        });
        clipboard.on('error', function (e) {
            console.log(e);
        });
    </script>
</body>
</html>
