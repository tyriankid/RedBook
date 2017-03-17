<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="baokuan.aspx.cs" Inherits="RedBook.WxZhuanti.baokuan" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" id="baokuan-html">
<head id="Head1" runat="server">
    <meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
    <title>买爆款不中包赔</title>
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
</head>
<body>
    <header class="head_zhuanti">
        <h2>活动详情</h2>
        <a class="close" href="/index.aspx">关闭
        </a>
    </header>
    <section class="zhuantibody">
        <div class="Picbox">
            <img src="images/baokuan/baokuan_01.gif" />
        </div>
        <div class="Picbox">
            <img id="btn-chongzhi" src="images/baokuan/baokuan_02.gif" />
        </div>
        <div class="Picbox">
            <img src="images/baokuan/baokuan_03.gif" />
        </div>
        <div class="Picbox">
            <img src="images/baokuan/baokuan_04.gif" />
        </div>
        <div class="Picbox baokuan_btnbox">
            <%--<img src="images/baokuan/baokuan_05.gif" />--%>
            <img class="btn-canyu" onclick="javascript:partake()" src="images/baokuan/btn-canyu.gif" />
            <img class="btn-canyued" src="images/baokuan/btn-canyued.gif" />
            <img class="btn-nozige" src="images/baokuan/btn-nozige.gif" />
        </div>
        <div class="Picbox">
            <img src="images/baokuan/baokuan_06.gif" />
        </div>
        <div class="Picbox">
            <img src="images/baokuan/baokuan_07.gif" />
        </div>
        <div class="Picbox">
            <img src="images/baokuan/baokuan_08.gif" />
        </div>
    </section>

    <script>
             function partake() {
                 var ajaxUrl = "http://" + window.location.host + "/ilerrredpack?action=valactivityqualification&aid=20";
                 $.ajax({
                     type: 'get', dataType: 'json', timeout: 10000,
                     url: ajaxUrl,
                     success: function (e) {
                         switch (e.state) {
                             case 0:
                                 var ajaxUrl = "http://" + window.location.host + "/ilerrredpack?action=activityqualification&aid=20";
                                 $.ajax({
                                     type: 'get', dataType: 'json', timeout: 10000,
                                     url: ajaxUrl,
                                     success: function (p) {
                                         if (p.state == 0) {
                                             showAlertDiv("领取成功！赶紧去参与吧！");
                                         }
                                     }
                                 })
                                 break;
                             case 1:
                                 showAlertDiv("您已成功领取资格！赶紧去参加吧！");
                                 break;
                             case 2:
                                 showAlertDiv("您已达到参与上限！请期待下次活动");
                                 break;
                         }
                     }
                 })
             }
    </script>
</body>
</html>
