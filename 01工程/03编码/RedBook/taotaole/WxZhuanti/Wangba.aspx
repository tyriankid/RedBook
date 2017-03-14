<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Wangba.aspx.cs" Inherits="RedBook.WxZhuanti.Wangba" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
    <title>网吧专题</title>
    <meta content="app-id=518966501" name="apple-itunes-app" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, user-scalable=no, maximum-scale=1.0" />
    <meta content="yes" name="apple-mobile-web-app-capable" />
    <meta content="black" name="apple-mobile-web-app-status-bar-style" />
    <meta content="telephone=no" name="format-detection" />
    <meta name="msapplication-tap-highlight" content="no">
    <%--<link href="/css/comm.css" rel="stylesheet" type="text/css" />--%>
    <link href="css/wangba.css?v=825" rel="stylesheet" type="text/css" />
    <script src="/js/jquery190.js" type="text/javascript"></script>
    <script src="/js/AlertBox.js?v=2.4"></script>
</head>
<body>
    <header class="head_wangba">
        <h2>活动详情</h2>
        <a class="close" href="/index.aspx">关闭
        </a>
    </header>
    <section class="WangbaBox">
        <div class="Picbox">
            <img src="images/wangba/drank_1.gif" />
        </div>
        <div class="Picbox">
            <img src="images/wangba/drank_2.gif" />
        </div>
        <div class="CanyuBox">
            <a class="btn-canyu btn-canyuing" id="PayLink" href="">
                <img src="images/wangba/btn-canyu.gif" /></a>
            <div class="btn-canyu btn-canyued" href="">
                <img src="images/wangba/btn-canyued.gif" />
            </div>
            <div class="btn-canyu btn-nozige" href="">
                <img src="images/wangba/btn-canyunone.gif" />
            </div>
            <div class="btn-canyu btn-yiduihuan" href="">
                <img src="images/wangba/btn-yiduihuan.gif" />
            </div>
        </div>
        <div class="Picbox">
            <img src="images/wangba/drank_3.gif" />
        </div>
        <div class="DrinkListbox">
            <%--<img class="drankbox_bg" src="images/wangba/drankbox-bg.gif" />--%>
            <ul class="DrinkList" id="WeiDuihuan">
                <li>
                    <img class="weidui" src="images/wangba/drank-img-1.png" /><img class="yidui" src="images/wangba/icon-yi.png" /></li>
                <li>
                    <img class="weidui" src="images/wangba/drank-img-2.png" /><img class="yidui" src="images/wangba/icon-dui.png" /></li>
                <li>
                    <img class="weidui" src="images/wangba/drank-img-3.png" /><img class="yidui" src="images/wangba/icon-jiang.png" /></li>
            </ul>
        </div>
        <div class="Picbox">
            <img src="images/wangba/drank_4.gif" />
        </div>
        <%--<div class="Picbox">
            <img src="images/wangba/drank_2.jpg" />
        </div>--%>
        <%--        <div class="btnList">
            <a href="">
                <img src="images/wangba/drank-link-1.png" /></a>
            <a href="">
                <img src="images/wangba/drank-link-2.png" /></a>
            <a href="">
                <img src="images/wangba/drank-link-3.png" /></a>
            <a href="">
                <img src="images/wangba/drank-link-4.png" /></a>
        </div>
        <div class="Picbox">
            <img src="images/wangba/drank_4.jpg" />
            <a class="btn-down" href="">
                <img src="images/wangba/btn-down.png" /></a>
        </div>--%>
    </section>

    <script>
        $(function () {
            //$("#WeiDuihuan li").height($("#WeiDuihuan li img").height());
            IsPass();
        });
        //判断用户参与网吧活动的资格
        function IsPass() {
            var ajaxUrl = "http://" + window.location.host + "/ilerrarticle?action=ispass";
            $.ajax({
                type: 'get', dataType: 'json', timeout: 10000,
                url: ajaxUrl,
                success: function (e) {
                    //alert("0有资格,3已经参与过,4已兑换,目前state是：" + e.state);
                    if (e.state == 0) {//0有资格
                        $(".btn-canyuing").show();
                        $("#PayLink").attr("href", "http://" + window.location.host + "/WxPay/wx_Submit.aspx?businessType=yuan&money=1&productid=1&quantity=1&paytype=2&redpaper=0&otype=1");
                    }
                    else if (e.state == 6) {//活动已过期，针对终止合作的网吧
                        $(".btn-canyuing").hide();
                        $(".btn-nozige").show();
                        showAlertDiv("活动已过期！", "calBack()");
                        $("#PayLink").attr("href", "#");
                    }
                    else if (e.state == 3) {//已经参与过
                        $(".btn-canyuing").hide();
                        $(".btn-canyued").show();
                        DuijiangClick();
                        $("#PayLink").attr("href","#");
                    }
                    else if (e.state == 4) {//已兑换
                        $(".btn-canyuing").hide();
                        $(".btn-yiduihuan").show();
                        $("#WeiDuihuan li .weidui").hide();
                        $("#WeiDuihuan li .yidui").show();
                        $("#PayLink").attr("href", "#");
                    }
                    else if (e.state == 1) {//有参与资格并且余额足够
                        //
                        $(".btn-canyuing").show();
                        $("#PayLink").attr("href", "javascript:pay()");
                    }
                    else {//没有参赛资格
                        $(".btn-canyuing").hide();
                        $(".btn-nozige").show();
                        showAlertDiv("您没有参赛资格！");
                        $("#PayLink").attr("href", "#");

                    }
                },
                error: function (me) {
                    showAlertDiv("获取失败！", "calBack()");
                    $("#PayLink").attr("href", "#");
                }
            });
        }
        function calBack() {//活动已过期或者没有参赛资格跳转回首页
            window.location = "/index.aspx";
        }

        //余额支付
        function pay() {
            $("#PayLink").attr("href", "#");
            $.getJSON('http://' + window.location.host + '/ilerrpay?action=userpaymentsubmit&productid=1&quantity=1&paytype=0&redpaper=0&APIsubmitcode=0', function (data) {
                switch (data.state) {
                    case 0://验证通过
                        location.href = "/WxZhuanti/Wangba.aspx";//后续若有其它业务需要通过商品原始ID进行支付，可扩冲一个类型变量实现无限扩展
                        break;
                    default:
                        alert("一元购订单产生错误,错误码:" + data.state + ",支付成功的爽乐币已经加入账户余额!");
                        break;
                }
            });
        }

        //点击变成已兑奖
        function DuijiangClick() {
            $("#WeiDuihuan li").click(function () {
                $(this).find(".weidui").hide();
                $(this).find(".yidui").show();
                //三个全部隐藏，触发兑奖
                if ($("#WeiDuihuan li .weidui:hidden").length == 3) {
                    WagnbaDuijiang();
                }
            });
        }

        //网吧兑换接口
        function WagnbaDuijiang() {
            var ajaxUrl = "http://" + window.location.host + "/ilerrarticle?action=exchange";
            $.ajax({
                type: 'get', dataType: 'json', timeout: 10000,
                url: ajaxUrl,
                success: function (e) {
                    if (e.state == 0) {//0兑奖成功
                        showAlertDiv("兑奖成功");
                    }
                    else if (e.state == 3) {//您已经兑过奖了
                        showAlertDiv("您已经兑过奖了！");
                    }
                }
            });
        }
    </script>
</body>
</html>
