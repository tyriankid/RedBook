<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="errPage.aspx.cs" Inherits="RedBook.errPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
<body>
    <header class="header">
        <h2>错误提示</h2>
        <a id="fanhui" class="cefenlei" onclick="history.go(-1)" href="javascript:;">
            <img width="30" height="30" src="images/fanhui.png">
        </a>
        <div class="head-r"><a href="/index.aspx" class="fr_Home"></a></div>
    </header>

    <section id="shopResultBox" class="clearfix">
        <div class="g-pay-auto">
            <img style="width: 50%; display: block; margin: 30px auto 0 auto;" src="/images/discover/errpage.png" />
            <div class="z-pay-tips"><b><em class="gray6" id="errMsg">错误提示语</em></b></div>
        </div>
        <div class="u-BtnBox">
            <div class="u-Btn">
                <div class="u-Btn-li"><a href="/community.index.aspx" class="z-CloseBtn">查看记录</a></div>
                <div class="u-Btn-li"><a href="/index.aspx" class="z-DefineBtn">继续购物</a></div>
            </div>
        </div>
    </section>

    <script>
        var errMsg = "<%=errMsg%>";
        window.onload = function () {
            document.getElementById("errMsg").innerHTML = errMsg;
            
        }
    </script>
</body>
</html>
