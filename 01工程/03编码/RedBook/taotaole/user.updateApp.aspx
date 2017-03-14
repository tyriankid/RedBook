<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="user.updateApp.aspx.cs" Inherits="RedBook.user_updateApp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, user-scalable=no, maximum-scale=1.0" />
    <title>App下载</title>
    <meta content="yes" name="apple-mobile-web-app-capable" />
    <meta content="black" name="apple-mobile-web-app-status-bar-style" />
    <meta content="telephone=no" name="format-detection" />
    <script src="http://res.wx.qq.com/open/js/jweixin-1.0.0.js"></script>
    <link href="css/comm.css?v=130715" rel="stylesheet" type="text/css" />
    <link href="css/member.css?v=130726" rel="stylesheet" type="text/css" />
    <script src="js/jquery190.js" language="javascript" type="text/javascript"></script>
    <script src="/js/fastclick.js"></script>
    <style>
        img {
            width:100%;
            vertical-align: middle;
        }
    </style>
    <script>
        $(function () {
            FastClick.attach(document.body);
        })
    </script>
</head>
<body style="background-color: #8ECFED;">
    <form id="form1" runat="server">
    <header class="header">
        <h2>APP下载</h2>
        <a id="fanhui" class="cefenlei" onclick="history.go(-1)" href="javascript:;">
            <img width="30" height="30" src="images/fanhui.png">
        </a>
        <div class="head-r"><a href="/index.aspx" class="fr_Home"></a></div>
    </header>
        <img src="/images/updateApp1.jpg" /><img src="/images/updateApp2.jpg" />
        <img src="/images/updateApp3.jpg" />
        <img src="/images/updateApp4.jpg" />
        <img src="/images/updateApp5.jpg" />
        <img onclick="alert('敬请期待')" src="/images/updateApp6.jpg" />
        <img onclick="alert('敬请期待')" src="/images/updateApp7.jpg" />
    </form>
</body>
</html>
