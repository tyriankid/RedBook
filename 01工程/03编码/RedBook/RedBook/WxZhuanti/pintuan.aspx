<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="pintuan.aspx.cs" Inherits="RedBook.WxZhuanti.pintuan" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" id="pintuanHtml">
<head id="Head1" runat="server">
    <meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
    <title>低价拼团</title>
    <meta content="app-id=518966501" name="apple-itunes-app" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, user-scalable=no, maximum-scale=1.0" />
    <meta content="yes" name="apple-mobile-web-app-capable" />
    <meta content="black" name="apple-mobile-web-app-status-bar-style" />
    <meta content="telephone=no" name="format-detection" />
    <meta name="msapplication-tap-highlight" content="no">
    <%--<link href="/css/comm.css" rel="stylesheet" type="text/css" />--%>
    <link href="css/zhuanti.css" rel="stylesheet" type="text/css" />
    <script src="/js/jquery190.js" type="text/javascript"></script>
    <script src="/js/AlertBox.js?v=2.4"></script>
    <script src="/js/fastclick.js"></script>
    <script>
        $(function () {
            FastClick.attach(document.body);
        });
    </script>
</head>
<body>
    <header class="head_zhuanti">
        <h2>活动详情</h2>
        <a class="close" href="/index.aspx">关闭
        </a>
    </header>
    <section class="pintuanbody">
        <div class="Picbox">
            <img src="images/pintuan/pintuan_01.gif" />
        </div>
        <div class="Picbox">
            <img id="btn-chongzhi" src="images/pintuan/pintuan_02.gif" />
        </div>
        <div class="Picbox">
            <img src="images/pintuan/pintuan_03.gif" />
        </div>
        <div class="Picbox">
            <a href="/WxTuan/tuan.index.aspx"><img src="images/pintuan/pintuan_04.gif" /></a>
        </div>
        <div class="Picbox">
            <img src="images/pintuan/pintuan_05.gif" />
        </div>
        <div class="h30"></div>
    </section>

    <script>
        $(function () {
        });

    </script>
</body>
</html>
