<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="20bian118.aspx.cs" Inherits="RedBook.WxZhuanti._20bian118" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" id="bian118-html">
<head runat="server">
    <meta name="msapplication-tap-highlight" content="no">
    <meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
    <title>20变118</title>
    <meta content="app-id=518966501" name="apple-itunes-app" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, user-scalable=no, maximum-scale=1.0" />
    <meta content="yes" name="apple-mobile-web-app-capable" />
    <meta content="black" name="apple-mobile-web-app-status-bar-style" />
    <meta content="telephone=no" name="format-detection" />
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
            <img src="images/20bian118/20_118_01.jpg" />
        </div>
        <div class="Picbox">
            <img onclick="javascript:partake()" id="btn-chongzhi" src="images/20bian118/20_118_02.gif" />
        </div>
        <div class="Picbox">
            <img src="images/20bian118/20_118_03.gif" />
        </div>
        <div class="Picbox">
            <img src="images/20bian118/20_118_04.gif" />
        </div>
        <div class="Picbox">
            <img src="images/20bian118/20_118_05.gif" />
        </div>
        <div class="Picbox">
            <img src="images/20bian118/20_118_06.gif" />
        </div>
        <div class="Picbox">
            <img src="images/20bian118/20_118_07.gif" />
        </div>
        <div class="Picbox">
            <img src="images/20bian118/20_118_08.gif" />
        </div>
        <div class="Picbox">
            <img src="images/20bian118/20_118_09.gif" />
            <div class="Picbox">
                <img src="images/20bian118/20_118_10.gif" />
                <div class="Picbox">
                    <img src="images/20bian118/20_118_11.gif" />
                </div>
            </div>
        </div>
        <div class="h30" style="background:#FBCD89;"></div>
    </section>

    <script>
        function partake() {
            var ajaxUrl = "http://" + window.location.host + "/ilerrredpack?action=valactivityqualification&aid=17";
            $.ajax({
                type: 'get', dataType: 'json', timeout: 10000,
                url: ajaxUrl,
                success: function (e) {
                    switch (e.state) {
                        case 0:
                            var ajaxUrl = "http://" + window.location.host + "/ilerrredpack?action=activityqualification&aid=17";
                            $.ajax({
                                type: 'get', dataType: 'json', timeout: 10000,
                                url: ajaxUrl,
                                success: function (p) {
                                    if (p.state == 0) {
                                        window.location.href = "/WxPay/wx_Submit.aspx?businessType=add&money=20";
                                    }
                                }
                            })
                            break;
                        case 1:
                            window.location.href = "/WxPay/wx_Submit.aspx?businessType=add&money=20";
                            break;
                        case 2:
                            showAlertDiv("你已经参与过哦！敬请期待下一次活动！");
                            break;
                    }
                }
            })
        }
    </script>
</body>
</html>
