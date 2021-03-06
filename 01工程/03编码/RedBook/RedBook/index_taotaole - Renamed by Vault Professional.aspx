﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index_taotaole.aspx.cs" Inherits="RedBook.index" %>

<%@ Register Src="Controls/ucFoot.ascx" TagName="ucFoot" TagPrefix="uc1" %>
<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>
<%@ Register TagPrefix="UI" Namespace="Taotaole.Common" Assembly="Taotaole.Common" %>
<!DOCTYPE html>
<html>
<head>
    <meta property="qc:admins" content="7545416270657117166375777">
    <!--<base href="/">-->
    <meta charset="utf-8">
    <meta http-equiv="x-ua-compatible" content="ie=edge">
    <meta name="renderer" content="webkit">
    <title>爽乐购</title>
    <META HTTP-EQUIV="pragma" CONTENT="no-cache"> 
    <META HTTP-EQUIV="Cache-Control" CONTENT="no-cache, must-revalidate"> 
    <META HTTP-EQUIV="expires" CONTENT="0">
    <meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no">
    <!-- development CSS -->
    <%--
    <link href="css/comm.css" rel="stylesheet" type="text/css">
    <link rel="stylesheet" href="css/zdy.css">
    <link href="css/aui-slide.css" rel="stylesheet" type="text/css">
    --%>
    <!-- product CSS -->
    <link href="css/export.index.css" rel="stylesheet" />

    <!-- garbage JS / i don't konw why these -->
    <!--
    <script src="js/lodash.js"></script>
    <script src="js/response.js"></script>
    <script src="js/swiper.min.js"></script>
    <script src="js/simpop.min.js"></script>
    <script src="js/js.cookie.js"></script>
    <script src="js/jquery.lazyload.min.js"></script>
    <script src="js/jquery.cookie.js"></script>
    <script language="javascript" type="text/javascript" src="js/Comm.js"></script>
    <script src="js/countdown.js"></script>
    -->  
</head>

<body>
    <form id="form1" runat="server">
        <main class="db-main db-main-index">
			<!-- 焦点图 -->
            <div id="aui-slide">
		    	<div class="aui-slide-wrap"></div>
                <div class="aui-slide-page-wrap"><!--分页容器--></div>
			</div>
			<div class="banner_top">
				<ul>
					<li class="chongzhi"><a href="/user.recharge.aspx"><em>充值</em></a></li>
					<li class="bian2"><a href="secondsOpen.aspx"><em>秒开</em></a></li>
                    <li class="fenlei"><a href="/WxZhuanti/zhongjiangmiji.aspx"><em>最新活动</em></a></li>
					<li class="zhongjiang"><a href="gift.index.aspx"><em>幸运转盘</em></a></li>
				</ul>
			</div>
			<!-- <a id="gd" href=""></a> -->
			<div class="zxjx kaij">
				<i class="toutiao"></i>
				<div class="zxjx_1" style="position: relative;">
					<div class="txtScroll-top" id="demo">
						<div class="bd">
							<ul id="singleLottery" style="top:0"></ul>
						</div>
					</div>
				</div>
			</div>
			<style>
				#fldh { position: relative; z-index: 999; width: 100%; }
			</style>
			<!--商品列表 Start-->
			<div class="tabs-wrap" style=" padding-bottom:0">
				<div id="fldh" class="zxjx"></div>
				<div class="goodsList">
					<div id="divGoodsLoading" class="loading" style="display:none;height: 80px;"></div>
				</div>
			</div>
			<input id="urladdress" value="" type="hidden" />
			<input id="pagenum" value="1" type="hidden" />
			<input id="pagesum" value="" type="hidden" />
			<!--商品列表 End-->
		</main>
        <div id="pullUp">
            <b></b><span class="pullUpLabel">加载中</span>
        </div>
        <ul class="alityBtn">
            <li onclick="goBackTop()">
                <img src="/images/goBackTop.png" alt="返回顶部" />
            </li>
            <li onclick="refresh()">
                <img src="/images/refresh.png" alt="页面刷新" id="imgref" />
            </li>
        </ul>

        <script src="js/jquery.js"></script>
        <uc1:ucFoot ID="ucFoot1" runat="server" />
        <!-- libs -->
        <script src="js/jquery.cookie.js"></script>
        <script src="js/aui-slide.js"></script><!-- 轮播图 -->
        <script src="js/jquery.SuperSlide.2.1.1.source.js"></script><!-- 中奖快报轮播 -->
        <script src="js/jquery.rotate.js"></script><!-- 刷新按钮旋转 -->
        <!-- JS -->
        <%--<script src="js/export.index.js"></script>--%>
        <script src="/js/QuickPay.js?v=201701041520"></script><!-- 支付 -->
        <script src="/js/AlertBox.js?v=201701041520"></script><!-- 引导分享弹出层 -->
        <script src="js/guanzhu.js?v=201701041520"></script><!-- 提醒关注 -->
        <script src="internalapi/common.js?v=201701041520" type="text/javascript"></script><!-- 公用方法 -->
        <script src="js/WinRemind.js?v=201701041520" language="javascript" type="text/javascript"></script><!-- 中奖确认 -->

        <!-- 获取ip地址及省市 -->
        <!--<script src="js/userlocation.js"></script>-->
        <!-- js获取用户所在位置(城市)操作 -->
        <!--<script type="text/javascript" src="api.map.baidu.com/api?v=2.0&ak=AIhNTuZzyAq5RAAbd1xRZcwulju0K0pB"></script>-->
        <!--<UI:WeixinSet ID="weixin" runat="server" />-->
        <script type="text/ecmascript">
            //返回顶部
            function goBackTop() {
                $("html,body").animate({
                    scrollTop: 0
                }, 0);
            }
            //左侧按钮刷新处理
            var refvalue = 0;
            function refresh() {
                refvalue += 360;
                $("#imgref").rotate({ animateTo: refvalue });
                $('.pullUpLabel').text('加载中');
                $('.pullUpLabel').prev().show();
                getLottery(true);
                getrefdata();
                //getdata(true);
            }

            window.onload = function () {
                FastClick.attach(document.body);
                setWxShareInfoDefault();
                //打开页面加载数据

                getdata(true);
                getLottery();
                //跳转页面
                var gt = '.goodsList span,.goodsList h2,.goodsList .Progress-bar';
                $(document).on('click', gt, function () {
                    var id = $(this).attr('id');
                    if (id) {
                        var src = "/index.item.aspx?productid=" + id + "&operation=" + $(this).attr('name');
                        var page = $("#pagenum").val();
                        var pos = $("body").scrollTop();
                        SetCookie('sitPos', '1')
                        SetCookie("page", page);
                        SetCookie("pos", pos);
                        window.location.href = src;
                    }
                });
                //WinRemind();//中奖确认方法
                getSlidePic();


            }

            function SetCookie(sName, sValue) {
                date = new Date();
                s = date.getDate();
                date.setDate(s + 1);
                document.cookie = sName + "=" + escape(sValue) + "; expires=" + date.toGMTString();
            }

            function GetCookie(sName) {
                var aCookie = document.cookie.split("; ");
                for (var i = 0; i < aCookie.length; i++) {
                    var aCrumb = aCookie[i].split("=");
                    if (sName == aCrumb[0]) {
                        return unescape(aCrumb[1]);
                    }
                }
                return null;
            }

            function setPaymentParms(e) {
                //设置好快速支付弹出层的属性
                var $productLi = e.closest('li');
                imgUrl = $productLi.find('img').attr('src'); //商品图片
                codeId = e.attr('codeid'); //商品编号
                yunJiage = e.attr('yunjiage'); //商品单价
                priceRangeArray = e.attr('pricerange').split(","); //商品购买数量区间
                renshuLeft = $productLi.find("[role='renshuLeft" + e.attr('productid') + "']").html(); //剩余购买数量
                //设置好支付事件需要的属性
                paytype = "ajaxPay";
                showQuickPayDiv();
            }

            //获取商品列表
            var nextPage = 1;
            var refp = 1;
            function getdata(isRefreshProductList) {
                var sitPos = GetCookie("sitPos");
                var page = GetCookie("page");
                var pos = GetCookie("pos");
                if (isRefreshProductList) {
                    var mydate = new Date();
                    var t = mydate.toLocaleDateString() + ' ' + mydate.getHours() + ':' + mydate.getMinutes() + ':' + mydate.getSeconds();
                    $.cookie("reftime", t);
                    nextPage = 1;
                    $(".goodsList ul").remove();
                }
                if (nextPage == 0) {
                    return;
                }
                if (sitPos == '1') {
                    var ajaxUrl = "http://" + window.location.host + "/ilerrshop?action=listinfo&typeid=list&order=10&p=1&pageSize=" + (page - 1);
                } else {
                    var ajaxUrl = "http://" + window.location.host + "/ilerrshop?action=listinfo&typeid=list&order=10&p=" + nextPage;
                }
                console.log(ajaxUrl);
                $.ajax({
                    type: 'get', dataType: 'json', timeout: 10000,
                    url: ajaxUrl,
                    success: function (e) {
                        console.log(e);
                        if (e.nextPage != 0) {
                            $("#pagenum").val(e.nextPage);
                        } else if (e.pageCount > 1 && e.nextPage == 0) {
                            var num = Number($("#pagenum").val());
                            $("#pagenum").val(num + 1);
                        }
                        nextPage = e.nextPage;
                        if (nextPage == 0) {
                            $('.pullUpLabel').text('已加载完全部商品');
                            $('.pullUpLabel').prev().hide();
                        }
                        if (nextPage != 0) {
                            refp = nextPage - 1;
                        } else {
                            refp = refp + 1;
                        }
                        for (var i = 0; i < e.data.length; i++) {
                            var num = (e.data[i].canyurenshu / e.data[i].zongrenshu) * 100;
                            num = num.toFixed(2);
                            var Datalist = '';
                            Datalist += '<ul>' +
                                '<li><span id="' + e.data[i].productid + '" name="' + e.data[i].operation + '" class="z-Limg" style="position:relative;display:block;"><img src="' + e.imgroot + e.data[i].thumb + '"></span>' +
                                    '<div style="margin-top:15px;" class="goodsListR">' +
                                        '<h2 id="' + e.data[i].productid + '">' + e.data[i].title + '</h2>' +
                                    '</div>' +
                                    '<div class="pRate item_bottom_container">' +
                                        '<div class="goods-num">' +
                                            '<div class="Progress-bar" id="' + e.data[i].productid + '">' +
                                                '<p class="u-progress" style="margin-left:0;"><span class="pgbar" style="width:' + num + '%;" id="pgbar' + e.data[i].productid + '"><span class="pging"></span></span>' +
                                                '</p>' +
                                                '<div class="Pro-bar-li">' +
                                                    '<div class="P-bar02"><em>' + e.data[i].zongrenshu + '</em>总需</div>' +
                                                    '<div class="P-bar03"><em role="renshuLeft' + e.data[i].productid + '">' + e.data[i].shengyurenshu + '</em>剩余</div>' +
                                                '</div>' +
                                            '</div>' +
                                        '</div>' +
                                        '<div class="link-playbox">' +
                                            '<a onclick="setPaymentParms($(this))" class="link-play" productid="' + e.data[i].productid + '" codeid="' + e.data[i].yId + '" pricerange="' + e.data[i].pricerange + '" yunjiage="' + e.data[i].yunjiage + '" href="javascript:void(0)">参与</a>' +
                                        '</div>' +
                                    '</div>' +
                                '</li>' +
                            '</ul>';
                            $("#divGoodsLoading").before(Datalist);
                        }
                        //myScroll.refresh();//DOM改变，iscroll刷新
                        if (sitPos == '1') {
                            $("html,body").scrollTop(pos);
                        }
                        SetCookie('sitPos', '0');
                    }
                })

            }
            function getrefdata() {
                var ajaxUrl = "http://" + window.location.host + "/ilerrshop?action=reflistinfo&p=" + refp + "&reftime" + $.cookie("reftime");
                $.ajax({
                    type: 'get', dataType: 'json', timeout: 10000,
                    url: ajaxUrl,
                    success: function (e) {
                        if (e.state == "0") {
                            for (var i = 0; i < e.data.length; i++) {
                                if ($("em[role=renshuLeft" + e.data[i].productid + "]")) {
                                    var num = (e.data[i].canyurenshu / e.data[i].zongrenshu) * 100;
                                    num = num.toFixed(2);
                                    $("em[role=renshuLeft" + e.data[i].productid + "]").text(e.data[i].shengyurenshu);
                                    $("#pgbar" + e.data[i].productid).width(num + "%");
                                } else {
                                    getdata(true);
                                }
                            }
                        } else {
                            getdata(true);
                        }
                        setTimeout(function () {
                            var i = document.createElement("i");
                            i.style.cssText = "position:fixed;top:50%;left:50%;transform:translate(-50%,-50%);-webkit-transform:translate(-50%,-50%);padding:10px;font-style:normal;color:#fff;background:#000;opacity:0.65;filter:Alpha(opacity=65);border-radius:5px;box-shadow: 0 0 10px #fff;-webkit-box-shadow: 0 0 10px #fff;z-index:9999;";
                            i.innerText = "刷新成功";
                            var body = document.getElementsByTagName("body")[0];
                            body.insertBefore(i, body.childNodes[0]);
                            setTimeout(function () {
                                body.removeChild(body.firstChild);
                            }, 150);
                        },1000)
                        //myScroll.refresh();//DOM改变，iscroll刷新
                    }
                })

            }
            //获取最新揭晓中的信息
            function getLottery(isRefreshLottery) {
                if (isRefreshLottery) {
                    $("#singleLottery").html('');
                    $("#singleLottery").attr("style", "top:0;position:relative;padding:0;margin:0;");//将滚动条还原到第一行
                }
                var ajaxUrl = "http://" + window.location.host + "/ilerrbuy?action=listwill&num=1";
                var jjjx = "";
                $.ajax({
                    type: 'get', dataType: 'json', timeout: 10000, url: ajaxUrl, success: function (data) {
                        //如果暂无正在揭晓中的数据,先将最新已揭晓的信息放入
                        if (data.state == 1) {
                            getLotteryList();
                            return;
                        }
                        //将最新的一条正在揭晓中的信息插入,并停止滚动
                        jjjx += '<li class="JJJX">' +
                                    '<a href="index.item.aspx?shopid=' + data.data[0].yId + '">即将揭晓<b>' + data.data[0].title + '</b><span class="daojis">倒计时<time role="daojishi" id="' + data.data[0].yId + '" leftTime="' + data.data[0].timeSpan + '"></time></span><i class="z-arrow"></i></a>' +
                                    '</li>';
                        $("#singleLottery").append(jjjx);
                        if (tSlide != null) tSlide.slide.customType = 'stop';//滚动停止
                        var $countDown = $("[role='daojishi']");
                        var countDownMS = parseInt($countDown.attr("leftTime"));
                        var ag = new startTimeOut($countDown, countDownMS, "singleLottery");//开始倒计时

                        ag.countdown();
                    }
                });
            }

            //获取最新已开奖信息
            var lotteryScroll;
            function getLotteryList(iswon) {
                var ajaxUrl = "http://" + window.location.host + "/ilerrbuy?action=listinfo";
                var yjjx = "";
                $.ajax({
                    type: 'get', dataType: 'json', timeout: 10000, url: ajaxUrl, success: function (data) {
                        if (data.count == 0) return;
                        for (var i = 0; i < data.data.length; i++) {
                            yjjx += '<li><a href="index.item.aspx?shopid=' + data.data[i].yId + '">恭喜<i>' + data.data[i].username + '</i>获得<b>' + data.data[i].title + '</b><i class="z-arrow"></i></a></li>';
                        }
                        $("#singleLottery").append(yjjx);
                        //加载完成之后,开始滚动
                        setTimeout("timer()", 1000);
                        if (iswon == "won") { tSlide = null; }
                    }
                });

            }

            //中奖快报倒计时插件
            var tDataVer = 1;
            var tSlide = null;
            function timer() {
                if (tSlide == null) {
                    tSlide = jQuery(".txtScroll-top").slide({
                        titCell: ".hd ul",
                        mainCell: ".bd ul",
                        autoPage: true,
                        effect: "top",
                        autoPlay: true,
                        vis: 1,
                        mouseOverStop: true
                    });
                }
                var obj = $("#singleLottery").get(0);
                var top = Math.abs(obj.style.top.replace("px", ""));
                var h = top / 25;
                setTimeout("timer()", 3000);
            }

            //未中奖前的默认分享描述
            function setWxShareInfoDefault() {
                var shareImg = "http://" + window.location.host + "/images/icon/taotaole.png";
                var shareTitleDes = "爽乐购一元夺宝";
                var shareDes = "每天淘宝，每天爽乐购，全场商品1元钱！";
                var link = "http://" + window.location.host + "/index.aspx";
                WinXinShareMessage(shareTitleDes, shareDes, link, shareImg);
                //开启右上角微信菜单
                document.addEventListener('WeixinJSBridgeReady', function onBridgeReady() {
                    WeixinJSBridge.call('showOptionMenu');
                });
            }

            //获取首页幻灯片
            /*document.addEventListener('touchmove', function (e) {
                e.preventDefault();
            }, false);*/
            function getSlidePic() {
                var ajaxUrl = "http://" + window.location.host + "/ilerrarticle?action=slides&slidetype=1";
                $.ajax({
                    type: 'get', dataType: 'json', timeout: 10000,
                    url: ajaxUrl,
                    success: function (e) {
                        for (var i = 0; i < e.data.length; i++) {
                            var slideurl = "window.location=" + "'" + e.data[i].slidelink + "'";
                            //var slideurl = e.data[i].slidelink;//  href="' + slideurl + '" 
                            var slide = '';
                            slide += '<div  class="aui-slide-node"  onclick="' + slideurl + '" >' +
                                            '<img alt="" src="' + e.imgroot + e.data[i].slideimg + '">' +
                                    '</div>';
                            $(".aui-slide-wrap").append(slide);
                        }
                        var sli = $("#aui-slide");
                        var sliImg = $("#aui-slide img").eq(0);
                        //图片宽高比获取轮播图层高度
                        var sliImgH = $(sliImg).width() * 189 / 600;
                        var slide = new auiSlide({
                            container: document.getElementById("aui-slide"),
                            // "width":300,
                            "height": sliImgH,
                            "speed": 300,
                            "autoPlay": 3000, //自动播放
                            "loop": true,
                            "pageShow": true,
                            "pageStyle": 'dot',
                            'dotPosition': 'center'
                        })
                    }
                });
            }

            //下拉加载
            $(window).scroll(function () {
                var sitPos = GetCookie("sitPos");
                if ($(window).scrollTop() == $(document).height() - $(window).height()) {
                    if (sitPos != '1') {
                        getdata();
                    }
                    if (nextPage == 0) {
                        $('.pullUpLabel').text('已加载完全部商品');
                        $('.pullUpLabel').prev().hide();
                        return;
                    }
                }
            });
        </script>
        <script src="js/fastclick.js"></script>
        <script src="http://res.wx.qq.com/open/js/jweixin-1.0.0.js"></script>
        <script src="js/wxShare.js" type="text/javascript"></script>
        <script type="text/ecmascript">
            FastClick.attach(document.body);
            setWxShareInfoDefault();
        </script>
</form>

</body>

</html>
