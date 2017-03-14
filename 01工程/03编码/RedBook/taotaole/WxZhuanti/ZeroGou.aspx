<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ZeroGou.aspx.cs" Inherits="RedBook.WxZhuanti.ZeroGou" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
    <title>1元就能买IPhone 6哦，快去看看吧！</title>
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
        <a class="close" href="/index.aspx">关闭</a>
    </header>
    <section class="zhuantibody">
        <div class="Picbox">
            <img src="images/ZeroGou/ZeroGou_01.gif" />
        </div>
        <div class="Picbox">
            <img id="btn-chongzhi" onclick="javascript:partake()" src="images/ZeroGou/ZeroGou_02.png" />
        </div>
        <div class="Picbox">
            <img src="images/ZeroGou/ZeroGou_03.gif" />
        </div>
        <div class="Picbox">
            <img src="images/ZeroGou/ZeroGou_04.gif" />
        </div>
        <div class="h20"></div>
        <div class="Picbox">
            <a class="onelink" href="/index.item.aspx?productid=40">
                <img src="images/ZeroGou/img-0.gif" />
            </a>
        </div>
        <div class="doublelink">
            <a href="/index.item.aspx?productid=9"><img src="images/ZeroGou/img-500.gif" /></a>
            <a href="/index.item.aspx?productid=8"><img src="images/ZeroGou/img-200.gif" /></a>
        </div>
        <div class="doublelink">
            <a href="/index.item.aspx?productid=6"><img src="images/ZeroGou/img-100.gif" /></a>
            <a href="/index.item.aspx?productid=16"><img src="images/ZeroGou/img-50.gif" /></a>
        </div>
        
        <div class="Picbox">
            <img src="images/ZeroGou/ZeroGou_05.gif" />
        </div>
        <div class="Picbox">
            <img src="images/ZeroGou/ZeroGou_06.gif" />
        </div>
        <div class="h30" style="background:#F0EFED;"></div>
    </section>

        <script>
            var vardate = 1;
            var ajaxUrl = "http://" + window.location.host + "/ilerrredpack?action=valactivitydate&aid=19";
            $.ajax({
                type: 'get', dataType: 'json', timeout: 10000,
                url: ajaxUrl,
                success: function (e) {
                    switch (e.state) {
                        case 0:
                            vardate = 1;
                            break;
                        case 1:
                            vardate = 0;
                            break;
                       
                    }
                }
            })
            function partake() {
                if (vardate == 1) {
                    var ajaxUrl = "http://" + window.location.host + "/ilerrredpack?action=valactivityqualification&aid=19";
                    $.ajax({
                        type: 'get', dataType: 'json', timeout: 10000,
                        url: ajaxUrl,
                        success: function (e) {
                            switch (e.state) {
                                case 0:
                                    var ajaxUrl = "http://" + window.location.host + "/ilerrredpack?action=activityqualification&aid=19";
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
                } else {
                    showAlertDiv("不在活动时间范围内！");
                }
            }
    </script>
</body>
</html>
