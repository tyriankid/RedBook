<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="user.userbalance.aspx.cs" Inherits="RedBook.user_userbalance" %>

<!DOCTYPE html>
<html>

<head>
    <title>账户明细</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta content="app-id=518966501" name="apple-itunes-app" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, user-scalable=no, maximum-scale=1.0" />
    <meta content="yes" name="apple-mobile-web-app-capable" />
    <meta content="black" name="apple-mobile-web-app-status-bar-style" />
    <meta content="telephone=no" name="format-detection" />
    <link href="css/comm.css" rel="stylesheet" type="text/css" />
    <link href="css/member.css" rel="stylesheet" type="text/css" />

    <script src="js/jquery190.js" language="javascript" type="text/javascript"></script>

</head>

<body>
    <header class="header">
        <h2>账户明细</h2>
        <a class="cefenlei" onclick="history.go(-1)" href="javascript:;">
            <img width="30" height="30" src="images/fanhui.png">
        </a>
        <div class="head-r"><a href="/index.aspx" class="fr_Home"></a></div>
    </header>
    <article class="clearfix g-userMoney">
        <b>0</b>
        <a href="/user.recharge.aspx" class="z-Recharge-btn">去充值</a>
    </article>


    <div id="fixedBox" class="RechargeBox">
        <div id="navBox" class="g-snav m_listNav">
            <div class="g-snav-lst z-sgl-crt" state="-1">
                <a id="btnConsumption" class="gray9">参与明细</a><b></b>
            </div>
            <div class="g-snav-lst" id="btnRechargebox" state="1">
                <a id="btnRecharge" class="gray9">充值明细</a><b></b>
            </div>
        </div>
        <div class="borderMoveBox clearfix">
            <div class="borderMove"></div>
        </div>
    </div>

    <article class="m-round">
        <!-- 参与明细 S -->
        <ul id="ulConsumption" class="m-userMoneylst m-Consumption">
        </ul>
        <!-- 参与明细 E -->
        <!-- 充值明细 S -->
        <ul id="ulRechage" class="m-userMoneylst" style="display: none;">
        </ul>
        <!-- 充值明细 E -->

        <div id="LoadCanYu" class="loading"><b></b>正在加载</div>
        <div id="LoadRechage" class="loading" style="display:none;"><b></b>正在加载</div>

	    <input id="nextPageCanYu" value="0" type="hidden" /><!-- 参与明细 已加载到第几页-->
	    <input id="nextPageRechage" value="0" type="hidden" /><!-- 充值明细 已加载到第几页-->

    </article>

</body>
<script type="text/javascript">
    $(".m-userMoneyNav li").click(function () {
        $(".m-userMoneyNav li a").toggleClass("z-MoneyNav-crt01").siblings().removeClass("z-MoneyNav-crt01");
        if ($("#ulConsumption").css('display') == "none") {
            $("#ulConsumption").show().siblings("#ulRechage").hide();
        } else {
            $("#ulConsumption").hide("#ulRechage").siblings().show();
        }
    });
</script>

<script type="text/javascript">
    //pList = $("#myPostList");
    var shownum;
    $(function () {
        if (getUrlParam("showzhifu") == 0) {
            hideShow($("#btnRechargebox"));
            $(".borderMove").animate({
                left: $(".borderMove").width()
            }, "fast");
            GetRechage();
            $("#navBox a").click(function () {
                hideShow($(this));
            });
        } else {
            pageInit();
        }
        UserInfo();//个人信息
    });
    //页面传参
    function getUrlParam(name) {
        var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)"); //构造一个含有目标参数的正则表达式对象
        var r = window.location.search.substr(1).match(reg);  //匹配目标参数
        if (r != null) return unescape(r[2]); return null; //返回参数值
    }

    //账户余额
    function UserInfo() {
        var ajaxUrl = "http://" + window.location.host + "/ilerruser?action=visituser";
        $.ajax({
            type: 'get', dataType: 'json', timeout: 10000,
            url: ajaxUrl,
            success: function (e) {
                $(".g-userMoney b").text(parseInt(e.money));
            },
            error: function (me) {
            }
        });
    }
    //点击显示隐藏
    function hideShow(cur) {
        var borderMove = $(".borderMove");
        var pos = borderMove.width();
        curId = cur.attr("id");
        cur.closest("div").addClass("z-sgl-crt");
        cur.closest("div").siblings().removeClass("z-sgl-crt");
        if (curId == "btnConsumption") {
            $("#ulRechage").hide();
            $("#LoadRecharge").hide();
            $("#ulRechage").html("");
            $("#ulConsumption").show();
            $("#LoadCanYu").show();
            GetConsumption();
            borderMove.animate({
                left: 0
            }, "fast");
        } else if (curId == "btnRecharge") {
            $("#ulConsumption").hide();
            $("#LoadCanYu").hide();
            $("#ulConsumption").html("");
            $("#ulRechage").show();
            $("#LoadRecharge").show();
            borderMove.animate({
                left: pos
            }, "fast");
            GetRechage();
            
        }
    }

    function pageInit() {
        $("#navBox a").click(function () {
            hideShow($(this));
        });
        $("#ulConsumption").show(); GetConsumption();
        $("#ulRechage").hide();
    }
    //自动加载
    $(window).scroll(function () {
        if ($(window).scrollTop() == $(document).height() - $(window).height()) {
            if ($("#ulConsumption").is(":visible")) {
                GetConsumption();
            } else if ($("#ulRechage").is(":visible")) {
                GetRechage();
            }
        }
    });

    var nextPageCanYu = 1;
    var nextPageRechage = 1;
    //参与明细
    function GetConsumption() {
        $("#nextPageRechage").val("0");//初始化中奖纪录的分页
        nextPageRechage = 1;
        $("#LoadRechage").hide();
        if (parseInt($("#nextPageCanYu").val()) >= nextPageCanYu) { return; }//防止重复加载
        var ajaxUrl = "http://" + window.location.host + "/ilerrbuy?action=myconsume&p=" + nextPageCanYu;
        var li = "";
        $.ajax({
            type: 'get', dataType: 'json', timeout: 10000,
            url: ajaxUrl,
            success: function (e) {
                $("#LoadCanYu").show();
                $("#LoadCanYu").text("加载中...");
                //暂无数据
                if (e.data.length <= 0) {
                    $("#LoadCanYu").text("暂无数据");
                    return;
                }
                $("#myAvaList .nojilu").hide();
                $("#nextPageCanYu").val(nextPageCanYu);//当前页
                nextPageCanYu = e.nextPage;//下一页
                //参与记录列表
                for (var i = 0; i < e.data.length; i++) {
                    li += '<li><h2>' + e.data[i].content + '</h2><p>' + e.data[i].time + '</p><b class="chu">-' + e.data[i].money + '</b></li>';
                }
                $("#ulConsumption").append(li);

                if (nextPageCanYu == 0) {
                    $("#LoadCanYu").text("加载完成");
                }
            },
            error: function (me) {
            }
        })
    }

    //充值明细
    function GetRechage() {
        $("#nextPageCanYu").val("0");//初始化中奖纪录的分页
        nextPageCanYu = 1;
        $("#LoadCanYu").hide();
        if (parseInt($("#nextPageRechage").val()) >= nextPageRechage) { return; }//防止重复加载
        var ajaxUrl = "http://" + window.location.host + "/ilerrbuy?action=mycharge&p=" + nextPageRechage;
        var li = "";
        $.ajax({
            type: 'get', dataType: 'json', timeout: 10000,
            url: ajaxUrl,
            success: function (e) {
                $("#LoadRechage").show();
                $("#LoadRechage").text("加载中...");
                //暂无数据
                if (e.data.length <= 0) {
                    $("#LoadRechage").text("暂无数据");
                    return;
                }
                //$("#myAvaList .nojilu").hide();
                $("#nextPageRechage").val(nextPageRechage);//当前页
                nextPageRechage = e.nextPage;//下一页
                for (var i = 0; i < e.data.length; i++) {
                    li += '<li><h2>' + e.data[i].pay_type + '充值</h2><p>' + e.data[i].time + '</p><b class="ru">+' + e.data[i].money + '</b></li>';
                }
                $("#ulRechage").append(li);
                if (nextPageRechage == 0) {
                    $("#LoadRechage").text("加载完成");
                }
            },
            error: function (me) {
            }
        })
    }
    //禁用右上角微信菜单
    document.addEventListener('WeixinJSBridgeReady', function onBridgeReady() {
        WeixinJSBridge.call('hideOptionMenu');
    });
</script>
</html>
