<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="cart.paysuccess.aspx.cs" Inherits="RedBook.cart_paysuccess" %>


<!DOCTYPE html>
<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>支付状态</title>
    <meta content="app-id=518966501" name="apple-itunes-app" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, user-scalable=no, maximum-scale=1.0" />
    <meta content="yes" name="apple-mobile-web-app-capable" />
    <meta content="black" name="apple-mobile-web-app-status-bar-style" />
    <meta content="telephone=no" name="format-detection" />
    <meta name="CMBNETPAYMENT" content="China Merchants Bank">
    <link href="/css/comm.css" rel="stylesheet" type="text/css" />
    <link href="/css/cartList.css" rel="stylesheet" type="text/css" />
</head>
<body style="background: #fff;">
    <header class="header">
        <h2>支付成功！</h2>
        <div class="head-r"><a href="/index.aspx" class="fr_Home"></a></div>
    </header>
    <input name="hidShopID" type="hidden" id="hidShopID" value="131118151938166319">
    <section id="shopResultBox" class="clearfix">
        <div class="g-pay-auto">
            <img style="width: 50%; display: block; margin: 50px auto 30px auto;" src="/images/discover/paysuccess.png" />
            <h1>支付成功！</h1>
            <%--<a class="link-goback" onclick="/community.index.aspx" href="javascript:;">进入个人中心</a>--%>
        </div>
    </section>
    <div class="u-BtnBox">
        <div class="u-Btn">
            <div class="u-Btn-li"><a href="/community.index.aspx" class="z-CloseBtn">查看记录</a></div>
            <div class="u-Btn-li"><a href="/index.aspx" class="z-DefineBtn">继续购物</a></div>
        </div>
    </div>
</body>
</html>
