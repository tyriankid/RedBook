<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WangbaV2.aspx.cs" Inherits="RedBook.WxZhuanti.WangbaV2" %>

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
    <link rel="stylesheet" href="css/demo.css" type="text/css" />
    <script type="text/javascript" src="js/jquery.min.js"></script>
    <script type="text/javascript" src="js/awardRotate.js"></script>
    <%--<script src="/js/fastclick.js"></script>--%>
    <style type="text/css">
        .link-close {
            position: absolute;
            top: 20px;
            right: -20px;
            width: 60px;
            height: 40px;
            line-height: 40px;
            -webkit-transform: rotate(45deg);
            font-size: 28px;
            color: #A3A3A3;
        }
    </style>
</head>
<body>
    <header class="head_wangba">
        <h2>活动详情</h2>
        <a class="close" href="/index.aspx">关闭
        </a>
    </header>
    <div class="wrapper">
        <img src="images/wangbav2/lucky_01.jpg">
        <img src="images/wangbav2/lucky_02.jpg">
    </div>
    <div class="turntable-bg">
        <div class="play">
            <img class="imgBg" src="images/wangbav2/turntable-bg2.png">
        </div>
        <div class="pointer" id="TakePart">
            <img src="images/wangbav2/pointer.png" />
        </div>
        <div class="pointer" id="TakeParted" style="display: none;">
            <img src="images/wangbav2/pointer2.png" />
        </div>
        <div class="rotate">
            <img id="rotate" src="images/wangbav2/turntable2.png" />
        </div>
    </div>
    <div style="margin-top: -46%;">
        <img src="images/wangbav2/lucky_03.gif">
        <img src="images/wangbav2/lucky_04.gif">
        <img src="images/wangbav2/lucky_05.jpg">
        <img src="images/wangbav2/lucky_06.jpg">
    </div>
    <%--<div class="rule">
        <div>
            <h3>爽乐购夺宝规则</h3>
            <p>1、每件商品参考市场价平分成相应“等份”，每份1元，1份对应一个夺宝号码。</p>
            <p>2、同一件商品可以购买多次或一次购买多份</p>
            <p>3、当一件商品所有“等份”全部售出后计算出“幸运夺宝号码”，拥有“幸运夺宝号码”，者即可获得此商品。</p>
            <p>4、抽取商品为概率事件，并不是百分百能获得奖品。</p>
        </div>
    </div>--%>


    <div class="rule">
        <div>
            <h3>幸运大抽奖规则</h3>
            <p>1：该游戏仅限新用户参与一次。</p>
            <p>2：每件商品价格平分相应“等份”，每份1元，1份对应1个幸运号码。</p>
            <p>3：当一件商品所有“等份”全部售出后，计算出“幸运号码”，拥有幸运号码者可获得此商品。</p>
            <p>4：抽取商品为概率事件，并不是百分百能获得奖品。</p>
        </div>
    </div>
    <div>
        <img src="images/wangbav2/lucky_07.jpg">
    </div>

    <script>
        var awards,//奖品序号
            id,//奖品productid
            txt;//奖品名称
        var payWay = "";//根据是否有余额判断使用支付方式
        $(function () {
            //FastClick.attach(document.body);
            //IsPass();
            var rotateTimeOut = function () {
                $('#rotate').rotate({
                    angle: 0,
                    animateTo: 2160,
                    duration: 8000,
                    callback: function () {
                        showAlertDiv('网络超时，请检查您的网络设置！');
                    }
                });
            };

            var bRotate = false;//为true，不能转动
            var rotateFn = function (awards, angles, id, txt) {
                //alert(txt);
                bRotate = !bRotate;
                $('#rotate').stopRotate();
                $('#rotate').rotate({
                    angle: 0,
                    animateTo: angles + 1800,
                    duration: 3000,
                    callback: function () {
                        //showWBAlertDiv(txt, "removeWBAlertDiv()");
                        showWBAlertDiv(txt, "removeWBAlertDiv()");
                        bRotate = !bRotate;
                    }
                })
            };

            $('#TakePart').click(function () {
                if (bRotate) return;//为true，不能转动
                //if (!IsPass()) return;
                var item = rnd(0, 25);
                var angle;
                if (item == 0) {
                    angle = rnd(301, 360);
                } else if (item < 6 && item != 0) {
                    angle = rnd(item * 60 - 59, item * 60);
                } else {
                    item = 3;
                    angle = rnd(121, 180);
                }
                switch (item) {
                    //var angle = [26, 88, 137, 185, 235, 287, 337];
                    case 0:
                        //30元话费卡
                        awards = 0;
                        id = 4;
                        txt = "30元话费卡";
                        rotateFn(0, angle, 4, "30元话费卡");
                        break;
                    case 5:
                        //ipad MiNi
                        awards = 1;
                        id = 23;
                        txt = "ipad MiNi";
                        rotateFn(1, angle, 23, "ipad MiNi");
                        break;
                    case 4:
                        //100元话费卡
                        awards = 2;
                        id = 7;
                        txt = "100元话费卡";
                        rotateFn(2, angle, 1, "100元话费卡");
                        break;
                    case 3:
                        //vivo X7
                        awards = 3;
                        id = 35;
                        txt = "vivo X7";
                        rotateFn(3, angle, 35, "vivo X7");
                        break;
                    case 2:
                        //50元话费卡
                        awards = 4;
                        id = 5;
                        txt = "50元话费卡";
                        rotateFn(4, angle, 5, "50元话费卡");
                        break;
                    case 1:
                        //iPhone7
                        awards = 5;
                        id = 66;
                        txt = "iPhone7";
                        rotateFn(5, angle, 66, "iPhone7");
                        break;
                }
                console.log(item);
            });
        });
        function rnd(n, m) {
            return Math.floor(Math.random() * (m - n + 1) + n);
        }


        //弹出层
        function wangbaAlertDiv(awards, id, txt, eventname) {
            var txtBox = '<div class="PopupBg clearfix" id="WbInfoPopupBg"></div>' +
                        '<div class="PopupLayer" id="WbInfoPopup">' +
                            '<h1>操作提示</h1><a href="javascript:;" class="link-close" onclick="removeWBAlertDiv();">+</a>' +
                            '<div class="po-nrbox">' +
                                //'<h2>666</h2>' +
                                '<h3><img src="images/wangbav2/' + awards + '.jpg" /></h3>' +
                                '<p>恭喜您抽中：'+
                                '<p>获得 “<span>' + txt + '</span>” 的机会</p>' +
                            '</div>' +
                            '<div class="OnebtnBox">' +
                                '<div class="btn">立即参与</div>' +
                                '<p>参与后，可去网吧前台领水</p>'+
                            '</div>' +
                        '</div>';
            $("body").append(txtBox);
            $("#WbInfoPopup .btn").click(function () {
                //alert(payWay);
                if (payWay == "WxPay") {//没有余额
                    WxPay(id, "removeWBAlertDiv()");

                } else if (payWay == "YuePay") {//有账户余额
                    YuePay(id, "removeWBAlertDiv()");
                }
                //removeWBAlertDiv(eventname);
            });
        }


        var windowWidth = document.documentElement.clientWidth;
        var windowHeight = document.documentElement.clientHeight;
        function showWBAlertDiv(element, eventname) {
            wangbaAlertDiv(awards, id, txt, eventname);
            $("#WbInfoPopupBg").show();
            $("#WbInfoPopup").show();
            $("#WbInfoPopup .po-nrbox h2").text(element);
            var popupHeight = $("#WbInfoPopup").height();
            var popupWidth = $("#WbInfoPopup").width();
            $("#WbInfoPopup").css({
                "top": (windowHeight - popupHeight) / 2,
                "left": (windowWidth - popupWidth) / 2
            });
            return false;
        }

        function removeWBAlertDiv(eventname) {
            $("#WbInfoPopupBg").remove();
            $("#WbInfoPopup").remove();
            if (eventname !== null || eventname !== undefined || eventname !== '') {
                eval(eventname);
            }
            return false;
        }

        //判断用户参与网吧活动的资格
        function IsPass() {
            var IsTrue = true;//有参赛资格
            var ajaxUrl = "http://" + window.location.host + "/ilerrarticle?action=ispass";
            $.ajax({
                type: 'get', dataType: 'json', timeout: 10000,
                url: ajaxUrl,
                success: function (e) {
                    if (e.state == 0) {//0有资格
                        $("#TakePart").show();
                        $("#TakeParted").hide();
                        payWay = "WxPay";
                    }
                    else if (e.state == 1) {//有参与资格并且余额足够
                        $("#TakePart").show();
                        $("#TakeParted").hide();
                        payWay = "YuePay";
                    }
                    else if (e.state == 6) {//活动已过期，针对终止合作的网吧
                        $("#TakePart").hide();
                        $("#TakeParted").show();
                        showAlertDiv("活动已过期！", "calBack()");
                        IsTrue = false;//没有参赛资格
                    }
                    else if (e.state == 3) {//已经参与过
                        $("#TakePart").hide();
                        $("#TakeParted").show();
                        //showAlertDiv("您已经参与过了！");
                        IsTrue = false;//没有参赛资格
                    }
                    else if (e.state == 4) {//已兑换
                        $("#TakePart").hide();
                        $("#TakeParted").show();
                        IsTrue = false;//没有参赛资格
                    }
                    else {//没有参赛资格
                        $("#TakePart").hide();
                        $("#TakeParted").show();
                        IsTrue = false;//没有参赛资格
                        showAlertDiv("您没有参赛资格！");
                    }
                },
                error: function (me) {
                    showAlertDiv("sorry，获取数据失败~", "calBack()");
                }
            });
            return IsTrue;
        }
        function calBack() {//活动已过期或者没有参赛资格跳转回首页
            window.location = "/index.aspx";
        }

        //微信支付
        function WxPay(id, eventname) {
            location.href = "http://" + window.location.host + "/WxPay/wx_Submit.aspx?businessType=yuan&money=1&productid=" + id + "&quantity=1&paytype=2&redpaper=0&otype=1";
            if (eventname !== null || eventname !== undefined || eventname !== '') {
                //var str = "alert('testtesttest');";
                eval(eventname);
            }
        }

        //余额支付
        function YuePay(id, eventname) {
            $.getJSON('http://' + window.location.host + '/ilerrpay?action=userpaymentsubmit&productid=' + id + '&quantity=1&paytype=0&redpaper=0&APIsubmitcode=0', function (data) {
                switch (data.state) {
                    case 0://验证通过
                        //location.href = "/WxZhuanti/WangbaV2.aspx";//后续若有其它业务需要通过商品原始ID进行支付，可扩冲一个类型变量实现无限扩展
                        location.href = "/community.index.aspx";//
                        break;
                    default:
                        alert("一元购订单产生错误,错误码:" + data.state + ",支付成功的爽乐币已经加入账户余额!");
                        break;
                }
            });
            if (eventname !== null || eventname !== undefined || eventname !== '') {
                //var str = "alert('testtesttest');";
                eval(eventname);
            }
        }

    </script>
</body>
</html>
