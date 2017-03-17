<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="tuan.orderInfo.aspx.cs" Inherits="RedBook.WxTuan.tuan_orderInfo" %>

<!DOCTYPE html>
<html>

<head>
    <meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
    <title>订单详情</title>
    <meta content="app-id=518966501" name="apple-itunes-app" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, user-scalable=no, maximum-scale=1.0" />
    <meta content="yes" name="apple-mobile-web-app-capable" />
    <meta content="black" name="apple-mobile-web-app-status-bar-style" />
    <meta content="telephone=no" name="format-detection" />
    <link href="/css/comm.css" rel="stylesheet" type="text/css" />
    <link href="/css/tuan.css" rel="stylesheet" type="text/css" />
    <script src="/js/fastclick.js"></script>
    <script src="/js/jquery-1.8.2.js" language="javascript" type="text/javascript"></script>
</head>

<body>
    <header class="header">
        <h2>订单详情</h2>
        <a id="A1" class="cefenlei" onclick="history.go(-1)" href="javascript:;">
            <img width="30" height="30" src="/images/fanhui.png">
        </a>
        <div class="head-r"><a href="/index.aspx" class="fr_Home"></a></div>
    </header>
    <!--1001=>中奖 1002=>组团中 1003=>很遗憾您此次未中奖,已退款 1004=>组团失败,已退款 1005=>待开奖 -->
    <section class="orderInfobox">
        <!--组团中  class="order-ttbg"-->
        <section class="order-ttbg">
            <div class="tt">{wc:$info['status_name']}</div>
        </section>
        <!--组团失败  class="order-failttbg"-->
        <section class="order-failttbg">
            <div class="tt">{wc:$info['status_name']}</div>
        </section>
        <!--组团成功  class="order-sussttbg"-->
        <section class="order-sussttbg">
            <div class="tt">{wc:$info['status_name']}</div>
        </section>


        <section id="oc-address" class="oc-address oc-has-address">
            <div class="oc-address-info">
                <div class="oc-address-receiver">
                    收货人:{wc:$addressInfo['shouhuoren']}&nbsp;&nbsp;&nbsp;电话:{wc:$addressInfo['mobile']}
                </div>
                <div class="oc-address-detail">
                    {wc:$addressInfo['sheng']}&nbsp;{wc:$addressInfo['shi']}&nbsp;{wc:$addressInfo['xian']}&nbsp;{wc:$addressInfo['jiedao']}
                </div>
            </div>
        </section>
        <section class="order-goodsbox">
            <div class="order-goods">
                <div class="order-img">
                    <img src="{wc:$info['thumb']}">
                </div>
                <div class="order-ginfo">
                    <h1>{wc:$info['title']}</h1>
                    <h2><b>¥</b><span>{wc:$info['moneycount']}</span></h2>
                    <div class="info-status">
                        <a class="more" href="{WEB_PATH}/mobile/tuan/join/{wc:$tuanlistInfo['id']}">参团详情</a>
							<a class="more-Win" href="{WEB_PATH}/mobile/tuan/WinningInformation/{wc:$tuanlistInfo['id']}">中奖信息</a>
                    </div>



                </div>
            </div>
            <div class="info-numbox">共1件商品，合计：<em>￥{wc:$info['moneycount']}</em>（免运费）</div>
            <!--<div class="infobtn-box clear">-->
            <!--<a href=""><em class="mes"></em>联系卖家</a>-->
            <!--<a href=""><em class="tel"></em>拨打电话</a>-->
            <!--</div>-->
        </section>
        <section class="order-wbg">
            <p>订单编号：{wc:$info['order_id']}</p>
            <p>支付方式：{wc:$info['pay_type_name']}</p>
            <p>下单时间：{wc:$info['order_time']}</p>
        </section>
    </section>
    <script type="text/javascript">
        $(function () {
            FastClick.attach(document.body);

            getOrderInfo();
        });

        //得到订单信息
        function getOrderInfo() {
            var ajaxUrl = "http://" + window.location.host + "/ilerrtuan?action=tuanorderinfo&orderid=" + getUrlParam("orderid");
            $.ajax({
                type: 'get', dataType: 'json', timeout: 10000,
                url: ajaxUrl,
                success: function (e) {
                    var div = '';
                    //div += '<div class="user-photo-box">' +
                    //    '<div class="user-photo">' +
                    //        '<a href="/community.index.aspx" class="z-Himg">' +
                    //            '<img style="border-radius: 110px;" src="' + e.imgroot + e.touxiang + '" border="0">' +
                    //        '</a>' +
                    //        '<em style="font-size: 14px; color: #fff; display: block; margin-top: 6px;">' + e.username + '</em>' +
                    //    '</div>' +
                    //    '<p style="text-align: center; color: #fff; font-size: 14px; margin-top: 5px;">' +
                    //        '签名：<span>我的签名还没想好呢</span>' +
                    //    '</p>' +
                    //    '</div>';
                    $("#myinfo").html(div);
                }
            });
        }

        //页面传参
        function getUrlParam(name) {
            var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)"); //构造一个含有目标参数的正则表达式对象
            var r = window.location.search.substr(1).match(reg);  //匹配目标参数
            if (r != null) return unescape(r[2]); return null; //返回参数值
        }

    </script>
</body>

</html>
