<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="redpack.index.aspx.cs" Inherits="RedBook.redpack_index" %>

<!DOCTYPE html>
<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>爽乐购</title>
    <meta content="app-id=518966501" name="apple-itunes-app" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, user-scalable=no, maximum-scale=1.0" />
    <meta content="yes" name="apple-mobile-web-app-capable" />
    <meta content="black" name="apple-mobile-web-app-status-bar-style" />
    <meta content="telephone=no" name="format-detection" />
    <link href="css/comm.css?v=20150129" rel="stylesheet" type="text/css" />
    <script src="http://res.wx.qq.com/open/js/jweixin-1.0.0.js"></script>
    <script src="js/jquery190.js" language="javascript" type="text/javascript"></script>
    <script src="/js/fastclick.js"></script>
    <link href="css/redpack.css" rel="stylesheet" type="text/css" />

</head>
<body>
    <header class="header">
        <h2>我的红包</h2>
        <a class="cefenlei" onclick="history.go(-1)" href="javascript:;">
            <img width="30" height="30" src="images/fanhui.png">
        </a>
        <div class="head-r"><a href="/index.aspx" class="fr_Home"></a></div>
    </header>
    <section class="clear">
        <div id="navBox" class="g-snav m_listNav">
            <div class="g-snav-lst z-sgl-crt" state="-1"><a id="AvaList" class="gray9">可用(0)</a><b></b></div>
            <div class="g-snav-lst" state="1"><a id="WaitList" class="gray9">待派发(0) </a><b></b></div>
            <div class="g-snav-lst" state="3"><a id="UnAvaList" class="gray9">用完/过期</a></div>
        </div>
        <div class="borderMoveBox clearfix">
            <div class="borderMove"></div>
        </div>
        <section id="myAvaList">
            <div class='clearfix nojilu' style="display:none;"><img src='images/icon-nojilu.png' /><p>您暂无可用红包</p></div>
            <ul class="redPackList clear">
            </ul>
		    <div class="loading"><b></b>正在加载</div>
        </section>
        <section id="myWaitList">
            <div class='clearfix nojilu' style="display:none;"><img src='images/icon-nojilu.png' /><p>您暂无待发红包</p></div>
            <ul class="redPackList clear">
            </ul>
		    <div class="loading"><b></b>正在加载</div>
        </section>
        <section id="myUnAvaList">
            <div class='clearfix nojilu' style="display:none;"><img src='images/icon-nojilu.png' /><p>暂无使用和过期记录</p></div>
            <ul class="redPackList clear">
            </ul>
		    <div class="loading"><b></b>正在加载</div>
        </section>
    </section>
    <%--<section class="redpbtmFixbox">
        <div class="redpbtmFix">
        <p>用爽乐币支付，<span class="colorful">比红包更方便</span></p>
        <a class="link-chongzhi" href="{WEB_PATH}/mobile/home/userrecharge">充值爽乐币</a>
        </div>
    </section>--%>
	<input id="AvaNextPage" value="0" type="hidden" /><!--可用红包--已加载到第几页-->
	<input id="WaitNextPage" value="0" type="hidden" /><!-- 不可用红包 --已加载到第几页-->
	<input id="UnAvaNextPage" value="0" type="hidden" /><!-- 已过期红包--已加载到第几页-->
    <script type="text/javascript">
        //打开页面加载数据
        $(function () {
            FastClick.attach(document.body);
            $("#AvaNextPage").val("0");
            $("#WaitNextPage").val("0");
            $("#UnAvaNextPage").val("0");
            pageInit();
            ReadNum();
        });
        function hideShow(cur) {
            var borderMove = $(".borderMove");
            var pos = borderMove.width();
            curId = cur.attr("id");
            cur.closest("div").addClass("z-sgl-crt");
            cur.closest("div").siblings().removeClass("z-sgl-crt");
            if (curId == "AvaList") {
                $("#myAvaList").show();
                $("#myWaitList").hide();
                $("#myUnAvaList").hide();
                $("#myWaitList").find(".redPackList").html("");
                $("#myUnAvaList").find(".redPackList").html("");
                AvaRedpack();
                borderMove.animate({
                    left: 0
                }, "fast");
            } else if (curId == "WaitList") {
                $("#myWaitList").show();
                $("#myAvaList").hide();
                $("#myUnAvaList").hide();
                $("#myAvaList").find(".redPackList").html("");
                $("#myUnAvaList").find(".redPackList").html("");
                WaitRedpack();
                borderMove.animate({
                    left: pos
                }, "fast");
            } else if (curId == "UnAvaList") {
                $("#myUnAvaList").show();
                UnAvaRedpack();
                $("#myAvaList").hide();
                $("#myWaitList").hide();
                $("#myAvaList").find(".redPackList").html("");
                $("#myWaitList").find(".redPackList").html("");
                borderMove.animate({
                    left: pos * 2
                }, "fast");
            }
        }

        function pageInit() {
            $("#navBox a").click(function () {
                hideShow($(this));
            });
            $("#myAvaList").show(), AvaRedpack(), $("#myWaitList").hide(), $("#myUnAvaList").hide();
        }
        //自动加载
        $(window).scroll(function () {
            if ($(window).scrollTop() == $(document).height() - $(window).height()) {
                if ($("#myAvaList").is(":visible")) {
                    AvaRedpack();
                }
                else if ($("#myWaitList").is(":visible")) {
                    WaitRedpack();
                }
                else if ($("#myUnAvaList").is(":visible")) {
                    UnAvaRedpack();
                }
            }
        });

        //读取红包个数
        function ReadNum() {
            var ajaxUrl = "http://" + window.location.host + "/ilerrredpack?action=redpackinfo&typeid=1&p=1";
            $.ajax({
                type: 'get', dataType: 'json', timeout: 10000,
                url: ajaxUrl,
                success: function (e) {
                    $("#AvaList").html("可用(" + e.docount + ")");
                    $("#WaitList").html("待派发(" + e.savecount + ")");
                }
            })
        }

        var AvaNextPage = 1;
        var WaitNextPage = 1;
        var UnAvaNextPage = 1;
        var pagecount = 1;//总页数

        //可用红包
        function AvaRedpack() {
            //初始化其他红包状态
            $("#WaitNextPage").val("0");
            WaitNextPage = 1;
            $("#UnAvaNextPage").val("0");
            UnAvaNextPage = 1;
            //$("#AvaNextPage").val("0");
            //AvaNextPage = 1;
            if (parseInt($("#AvaNextPage").val()) >= AvaNextPage) { return; }//防止重复加载
            var ajaxUrl = "http://" + window.location.host + "/ilerrredpack?action=redpackinfo&typeid=1&p=" + AvaNextPage;
            var li = "";
            $.ajax({
                type: 'get', dataType: 'json', timeout: 10000,
                url: ajaxUrl,
                success: function (e) {
                    $("#AvaList").html("可用(" + e.count + ")");
                    $("#myAvaList .loading").html("<b></b>加载中...");
                    //暂无数据
                    if (e.count == 0) {
                        $("#myAvaList .nojilu").show();
                        $("#myAvaList .loading").hide();
                        return;
                    }
                    $("#AvaNextPage").val(AvaNextPage);//当前页
                    AvaNextPage = e.nextPage;//下一页
                    //购买人数列表
                    for (var i = 0; i < e.data.length; i++) {
                        codelefttitle = e.data[i].amount != 0 ? '满' + e.data[i].amount + '减' + e.data[i].discount : '抵扣' + e.data[i].discount + '元';
                        li +='<li>'+
                            '<b>' + codelefttitle + '</b>' +
                            '<div class="txt">'+
                                '<h1>' + e.data[i].codetitle + '</h1>' +
                                '<p>生效时间：' + e.data[i].senttime.replace('T', ' ').replace(/\-/g, '/') + '</p>' +
                                '<p>截止日期：' + e.data[i].overtime.replace('T', ' ').replace(/\-/g, '/') + '</p>' +
                                '<p>适用商品：全场通用</p>' +
                            '</div>'+
                            '<i>未使用</i>' +
                         '</li>';
                    }
                    $("#myAvaList .redPackList").append(li);

                    if (AvaNextPage == 0) {
                        $("#myAvaList .loading").text("加载完成");
                    }
                },
                error: function (me) {
                }
            })
        }

        //待派发红包
        function WaitRedpack() {
            //初始化其他红包状态
            $("#AvaNextPage").val("0");
            AvaNextPage = 1;
            $("#UnAvaNextPage").val("0");
            UnAvaNextPage = 1;
            if (parseInt($("#WaitNextPage").val()) >= WaitNextPage) { return; }//防止重复加载
            var ajaxUrl = "http://" + window.location.host + "/ilerrredpack?action=redpackinfo&typeid=2&p=" + WaitNextPage;
            var li = "";
            $.ajax({
                type: 'get', dataType: 'json', timeout: 10000,
                url: ajaxUrl,
                success: function (e) {
                    //if (e.state == 0) { return; }
                    $("#WaitList").html("待派发(" + e.count + ")");
                    $("#myWaitList .loading").html("<b></b>加载中...");
                    //暂无数据
                    if (e.count == 0) {
                        $("#myWaitList .nojilu").show();
                        $("#myWaitList .loading").hide();
                        return;
                    }
                    $("#WaitNextPage").val(WaitNextPage);//当前页
                    WaitNextPage = e.nextPage;//下一页
                    //购买人数列表
                    for (var i = 0; i < e.data.length; i++) {
                        codelefttitle = e.data[i].amount != 0 ? '满' + e.data[i].amount + '减' + e.data[i].discount : '抵扣' + e.data[i].discount + '元';
                        li += '<li>' +
                            '<b>' + codelefttitle + '</b>' +
                            '<div class="txt">' +
                                '<h1>' + e.data[i].codetitle + '</h1>' +
                                '<p>生效时间：' + e.data[i].senttime.replace('T', ' ').replace(/\-/g, '/') + '</p>' +
                                '<p>截止日期：' + e.data[i].overtime.replace('T', ' ').replace(/\-/g, '/') + '</p>' +
                                '<p>适用商品：全场通用</p>' +
                            '</div>' +
                            '<i>待派发</i>' +
                         '</li>';
                    }
                    $("#myWaitList .redPackList").append(li);

                    if (WaitNextPage == 0) {
                        $("#myWaitList .loading").text("加载完成");
                    }
                },
                error: function (me) {
                }
            })
        }

        //不可用红包
        function UnAvaRedpack() {
            //初始化其他红包状态
            $("#AvaNextPage").val("0");
            AvaNextPage = 1;
            $("#WaitNextPage").val("0");
            WaitNextPage = 1;
            //$("#UnAvaNextPage").val("0");
            //UnAvaNextPage = 1;
            if (parseInt($("#UnAvaNextPage").val()) >= UnAvaNextPage) { return; }//防止重复加载
            var ajaxUrl = "http://" + window.location.host + "/ilerrredpack?action=redpackinfo&typeid=3&p=" + UnAvaNextPage;
            var li = "";
            $.ajax({
                type: 'get', dataType: 'json', timeout: 10000,
                url: ajaxUrl,
                success: function (e) {
                    $("#myUnAvaList .loading").html("<b></b>加载中...");
                    //暂无数据
                    if (e.count == 0) {
                        $("#myUnAvaList .nojilu").show();
                        $("#myUnAvaList .loading").hide();
                        return;
                    }
                    $("#UnAvaNextPage").val(UnAvaNextPage);//当前页
                    UnAvaNextPage = e.nextPage;//下一页
                    //购买人数列表
                    for (var i = 0; i < e.data.length; i++) {
                        var state = e.data[i].state;
                        if (state == "2") {
                            state = "已使用";
                        } else {
                            state = "已过期";
                        }
                        codelefttitle = e.data[i].amount != 0 ? '满' + e.data[i].amount + '减' + e.data[i].discount : '抵扣' + e.data[i].discount + '元';
                        li += '<li>' +
                            '<b>' + codelefttitle + '</b>' +
                            '<div class="txt">' +
                                '<h1>' + e.data[i].codetitle + '</h1>' +
                                '<p>生效时间：' + e.data[i].senttime.replace('T', ' ').replace(/\-/g, '/') + '</p>' +
                                '<p>截止日期：' + e.data[i].overtime.replace('T', ' ').replace(/\-/g, '/') + '</p>' +
                                '<p>适用商品：全场通用</p>' +
                            '</div>' +
                            '<i>' + state + '</i>' +
                         '</li>';
                    }
                    $("#myUnAvaList .redPackList").append(li);

                    if (UnAvaNextPage == 0) {
                        $("#myUnAvaList .loading").text("加载完成");
                    }
                } ,
                error: function (me) {
                }
            })
        }
        //禁用右上角微信菜单
        document.addEventListener('WeixinJSBridgeReady', function onBridgeReady() {
            WeixinJSBridge.call('hideOptionMenu');
        });
    </script>
</body>
</html>
