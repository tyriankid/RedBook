<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="user.recharge.aspx.cs" Inherits="RedBook.user_recharge" %>

<!DOCTYPE html>
<html>
<head>
    <title>帐户充值
    </title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta content="app-id=518966501" name="apple-itunes-app" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, user-scalable=no, maximum-scale=1.0" />
    <meta content="yes" name="apple-mobile-web-app-capable" />
    <meta content="black" name="apple-mobile-web-app-status-bar-style" />
    <meta content="telephone=no" name="format-detection" />
    <link href="css/comm.css?v=130715" rel="stylesheet" type="text/css" />
    <link href="css/login.css" rel="stylesheet" type="text/css" />
    <link href="css/member.css?v=130726" rel="stylesheet" type="text/css" />
    <script src="js/jquery190.js" language="javascript" type="text/javascript"></script>
    <script src="/js/fastclick.js"></script>
</head>
<body>
    <header class="header">
        <h2>账户充值</h2>
        <a class="cefenlei" onclick="history.go(-1)" href="javascript:;">
            <img width="30" height="30" src="images/fanhui.png">
        </a>
        <div class="head-r"><a href="/index.aspx" class="fr_Home"></a></div>
    </header>

    <section class="clearfix g-member Recharge-me">
        <!--<div class="g-Total gray9" >您的剩余爽乐币：<span class="orange arial">{wc:$member['money']}</span>个</div>-->
        <p style="text-align: center; line-height: 46px; color: #b1924e; letter-spacing: 2px; font-size: 14px;">选择充值金额(元)</p>
        <div class="g-Recharge">
            <ul id="ulOption">
                <li money="10"><a href="javascript:;" class="z-sel">10<s></s></a></li>
                <li money="20"><a href="javascript:;">20<s></s></a></li>
                <li money="30"><a href="javascript:;">30<s></s></a></li>
                <li money="100" style="margin-bottom: 0;"><a href="javascript:;">100<s></s></a></li>
                <li money="200" style="margin-bottom: 0;"><a href="javascript:;">200<s></s></a></li>
                <li style="margin-bottom: 0;">
                    <b>
                        <input type="text" class="z-init" id="txtMoney" onblur="javascript:rechargeMoney = $(this).val() ;" onkeyup="value=value.replace(/\D/g,'')" placeholder="输入金额" maxlength="8" />
                        <!--<s></s>-->
                    </b>
                </li>
            </ul>
        </div>
        <p style="text-align: center; line-height: 46px; color: #b1924e; letter-spacing: 2px; font-size: 14px;">选择支付方式</p>
        <article class="clearfix g-pay-ment g-bank-ct">
            <ul id="ulBankList" style="padding-left: 15px;">
                <li class="gray6" style="height: 30px; line-height: 30px; display: none;">选择平台充值<em class="orange">10.00</em>元</li>
                <li class="gray9" urm="9">
                    <span class="imgbox" style="background-image: url(images/weixin.png);"></span>
                    <div class="tishi"><span class="p1">微信支付微信端</span><br>
                        <span class="p2">微信支付微信端</span></div>
                    <i class="z-bank-Roundsel"><s></s></i>
                    <!--微信支付微信端 CMBCHINA-WAP -->
                </li>
            </ul>
        </article>
        <div class="mt10 f-Recharge-btn">
            <a id="btnSubmit" href="javascript:;" class="orgBtn">立即支付</a>
        </div>
    </section>


 

    <!-- 支付跳转加载框 Start -->
    <div class="sk-wave-bg"></div>
    <div class="sk-wave">
        <div class="sk-rect sk-rect1"></div>
        <div class="sk-rect sk-rect2"></div>
        <div class="sk-rect sk-rect3"></div>
        <div class="sk-rect sk-rect4"></div>
        <div class="sk-rect sk-rect5"></div>
        <div class="sk-txt">正在支付中...</div>
    </div>
    <!-- 支付跳转加载框 End -->

    <script>
        var rechargeMoney = 0;

        $(function () {
            FastClick.attach(document.body);
            $("#ulOption li").eq(0).click();
        })

        function onBridgeReady() {
            WeixinJSBridge.call('hideOptionMenu');
        }



        $("#btnSubmit").click(function () {
            if (rechargeMoney != "" && !isNaN(rechargeMoney)) {
                location.href = "/WxPay/wx_Submit.aspx?businesstype=add&money="+ rechargeMoney;
            }
            else {
                alert("请输入合法数字！");
            }
                    
            
        });
        $("#ulOption li").click(function () {
            if ($(this).attr("id") == "txtMoney") return;
            $(this).find("a").addClass("z-sel");
            $(this).siblings().find("a").removeClass("z-sel");
            //设置充值金额
            rechargeMoney = parseInt($(this).attr("money"));
        });

        //禁用右上角微信菜单
        document.addEventListener('WeixinJSBridgeReady', function onBridgeReady() {
            WeixinJSBridge.call('hideOptionMenu');
        });
    </script>
</body>
</html>
