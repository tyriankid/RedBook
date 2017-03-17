<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="tuan.WinningInformation.aspx.cs" Inherits="taotaole.WxTuan.tuan_WinningInformation" %>

<!DOCTYPE html>
<html>

<head>
    <meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
    <title>{wc:$title}</title>
    <meta content="app-id=518966501" name="apple-itunes-app" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, user-scalable=no, maximum-scale=1.0" />
    <meta content="yes" name="apple-mobile-web-app-capable" />
    <meta content="black" name="apple-mobile-web-app-status-bar-style" />
    <meta content="telephone=no" name="format-detection" />
    <link href="/css/comm.css" rel="stylesheet" type="text/css" />
    <link href="/css/tuan.css" rel="stylesheet" type="text/css" />
    <script src="/js/jquery-1.8.2.js" language="javascript" type="text/javascript"></script>
</head>

<body>
    <header class="header">
        <h2>中奖确认</h2>
        <a id="A1" class="cefenlei" onclick="history.go(-1)" href="javascript:;">
            <img width="30" height="30" src="/images/fanhui.png">
        </a>
        <div class="head-r"><a href="/index.aspx" class="fr_Home"></a></div>
    </header>

    <section class="WinListInfoBox">
        <section class="WinningList-box">
            <div class="win-goods">
                <div class="win-img">
                    <img src="">
                </div>
                <div class="win-ginfo">
                    <h1>标题</h1>
                    <h2><b>¥</b><span>50.00</span><i>￥70</i></h2>

                    <a class="link-kaited" href="javascript:;">已开奖</a>

                </div>
            </div>
        </section>
        <div class="WinPer-lmtt">中奖名单</div>
        <section class="WinPerList">
            <ul class="WinPer-tt">
                <li class="one">幸运儿</li>
                <li class="two">订单编号</li>
                <li class="three">电话</li>
            </ul>
            <ul>
                <li class="one">
                    <img src="">
                    <b>中奖人</b>
                </li>
                <li class="two">中奖吗</li>
                <li class="three">电话</li>
            </ul>
            <ul>
                <li class="one">
                    <img src="">
                    <b>中奖人</b>
                </li>
                <li class="two">中奖吗</li>
                <li class="three">电话</li>
            </ul>
        </section>
    </section>

</body>

</html>
