<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="tuan.index.aspx.cs" Inherits="RedBook.WxTuan.tuan_index" %>
<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>
<%@ Register TagPrefix="UI" Namespace="Taotaole.Common" Assembly="Taotaole.Common" %>
<%@ Register Src="/Controls/ucFoot.ascx" TagName="ucFoot" TagPrefix="uc1" %>

<!DOCTYPE html>
<html>
<head>
    <meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
    <title>团购专区</title>
    <meta content="app-id=518966501" name="apple-itunes-app" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, user-scalable=no, maximum-scale=1.0" />
    <meta content="yes" name="apple-mobile-web-app-capable" />
    <meta content="black" name="apple-mobile-web-app-status-bar-style" />
    <meta content="telephone=no" name="format-detection" />
    <link href="/css/comm.css" rel="stylesheet" type="text/css" />
    <link href="/css/tuan.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="/css/style.css">
    <script src="/js/swiper.min.js"></script>
    <script src="http://res.wx.qq.com/open/js/jweixin-1.0.0.js"></script>
    <script src="/js/wxShare.js" type="text/javascript"></script>
    <script src="/js/jquery-1.8.2.js" language="javascript" type="text/javascript"></script>
    <script src="/js/iscroll.js" language="javascript" type="text/javascript"></script>
    <script src="/js/fastclick.js"></script>
</head>
<body>
    <header class="header">
        <h2>团购专区</h2>
        <a id="fanhui" class="cefenlei" onclick="history.go(-1)" href="javascript:;">
            <img width="30" height="30" src="/images/fanhui.png">
        </a>
        <div class="head-r"><a href="/index.aspx" class="fr_Home"></a></div>
    </header>

    <!-- 内页顶部 -->
    <!-- 焦点图 -->
    <div class="swiper-container">
        <div class="swiper-wrapper">
        </div>
        <div class="swiper-pagination swiper-pagination-clickable">
        </div>
    </div>
    <script>
        var mySwiper = new Swiper('.swiper-container', {
            autoplay: 3000, //可选选项，自动滑动
            pagination: '.swiper-pagination',
            paginationClickable: true,
        })
    </script>
    <%--<section class="banner">
        <img src="/banner/20160516/tuanbanner.jpg">
    </section>--%>
    <section class="tuanlistbox">
        <div class="tabs-wrap">
            <div id="fldh" class="zxjx"></div>
            <!--团购商品列表-->
            <div class="goodsList" style="clear: both; overflow: hidden;">

                <ul class="tuanlist"></ul>
                <div id="divGoodsLoading" class="loading"><b></b>正在加载</div>
            </div>

        </div>
        <input id="urladdress" value="" type="hidden" />
        <input id="pagenum" value="" type="hidden" />
    </section>

    <uc1:ucFoot ID="ucFoot1" runat="server" />
    <UI:WeixinSet ID="weixin" runat="server" />
    <script>
        window.onload = function () {
            FastClick.attach(document.body);
            setWxShareInfoDefault();
            getdata(true);
            getSlidePic();
            
        }

        function setWxShareInfoDefault() {
            var shareImg = "http://" + window.location.host + "/images/icon/taotaole.png";
            var shareTitleDes = "来来来，老司机带你来拼团";
            var shareDes = "iPhone，话费，游戏皮肤，统统只要0.1元拼，呼朋唤友一起来吧！";
            var link = "http://" + window.location.host + "/WxTuan/tuan.index.aspx";
            WinXinShareMessage(shareTitleDes, shareDes, link, shareImg);
        }
        

        //获取首页幻灯片
        function getSlidePic() {
            var ajaxUrl = "http://" + window.location.host + "/ilerrarticle?action=slides&slidetype=3";
            $.ajax({
                type: 'get', dataType: 'json', timeout: 10000,
                url: ajaxUrl,
                success: function (e) {
                    for (var i = 0; i < e.data.length; i++) {
                        //var slideurl = "window.location=" + "'" + e.data[i].slidelink + "'";
                        var slideurl = e.data[i].slidelink;
                        var slide = '';
                        slide += '<a  class="swiper-slide"  U="' + slideurl + '">' +
                                    '<li>' +
                                        '<img alt="" src="' + e.imgroot + e.data[i].slideimg + '"></li>' +
                                '</a>';
                        $(".swiper-wrapper").append(slide);
                    }

                    $(".swiper - wrapper").unbind();
                    var mySwiper = new Swiper('.swiper-container', {
                        autoplay: 5000, //可选选项，自动滑动
                        pagination: '.swiper-pagination',
                        paginationClickable: true,
                        observer: true, //修改swiper自己或子元素时，自动初始化swiper
                        observeParents: true, //修改swiper的父元素时，自动初始化swiper
                        preventLinks: true
                       , onTouchStart: function () {
                       }, onTouchEnd: function (swiper, even) {
                       },
                        onClick: function (swiper) {
                            window.location = $(".swiper-slide-active").attr("U");
                        }
                    }
                    )
                }
            });
        }

        //获取商品列表
        var nextPage = 1;
        function getdata(isRefreshProductList) {
            if (isRefreshProductList) {
                nextPage = 1;
                $(".goodsList ul").html("");
            }
            if (parseInt($("#pagenum").val()) >= nextPage) { return;}//防止重复加载
            var ajaxUrl = "http://" + window.location.host + "/ilerrtuan?action=tuanlist&p=" + nextPage;
            $.ajax({
                type: 'get', dataType: 'json', timeout: 10000,
                url: ajaxUrl,
                success: function (e) {
                    //暂无数据
                    if (e.count <= 0) {
                        $("#divGoodsLoading").text("暂无数据");
                        return;
                    }
                    nextPage = e.nextPage;
                    $("#pagenum").val(nextPage);//当前页
                    for (var i = 0; i < e.data.length; i++) {
                        var li = '';
                        var imgsrc = "";
                        if (e.data[i].thumb) { imgsrc = '<img class="tlist-img" src="' + e.imgroot + e.data[i].thumb + '">' }
                        li += '<li><a href="http://' + window.location.host + '/WxTuan/tuan.item.aspx?tuanid=' + e.data[i].tId + '"><span id="' + e.data[i].tId + '" class="z-Limg">' +
                                imgsrc + '</span><div class="goodsListR">' +
                                    '<div class="tt">' + e.data[i].title + '</div>' +
                                    '<div class="tlist-info2"><span class="money">团购价￥<em>' + e.data[i].per_price + '</em></span><span class="yjia">单卖价<i>￥' + parseInt(e.data[i].money) + '</i></span></div>' +
                                    '<div class="btn-tuan"><span class="btn">去开团</span></div>' +
                                    '</div></div><div class="tlist-info"><span class="num"><i></i>' + e.data[i].total_num + '人团</span></div>' +
                                    
                                '</div></a></li>';
                        $(".tuanlist").append(li);
                        if (nextPage == 0) {
                            $("#divGoodsLoading").text("加载完成");
                        }
                    }
                }
            })

        }


        //自动加载
        $(window).scroll(function () {
            if ($(window).scrollTop() == $(document).height() - $(window).height() && nextPage > 1) {
                getdata();
            }
        });

        //开启右上角微信菜单
        document.addEventListener('WeixinJSBridgeReady', function onBridgeReady() {
            WeixinJSBridge.call('showOptionMenu');
        });
    </script>

</body>


</html>
