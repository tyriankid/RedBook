<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="tuan.Collection.aspx.cs" Inherits="RedBook.WxTuan.tuan_Collection" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
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
    <script src="/js/jquery-1.8.2.js" language="javascript" type="text/javascript"></script>
    <script src="/js/fastclick.js"></script>
</head>
<body>
    <header class="header">
        <h2>我的收藏</h2>
        <a id="fanhui" class="cefenlei" onclick="history.go(-1)" href="javascript:;">
            <img width="30" height="30" src="/images/fanhui.png">
        </a>
        <div class="head-r"><a href="/index.aspx" class="fr_Home"></a></div>
    </header>

    <!-- 内页顶部 -->
    <!-- 焦点图 -->
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

    <script>
        window.onload = function () {
            FastClick.attach(document.body);
            getdata(true);
        }


        //获取商品列表
        var nextPage = 1;
        function getdata(isRefreshProductList) {
            if (isRefreshProductList) {
                nextPage = 1;
                $(".goodsList ul").html("");
            }
            if (parseInt($("#pagenum").val()) >= nextPage) { return; }//防止重复加载
            var ajaxUrl = "http://" + window.location.host + "/ilerrtuan?action=tuankeeplist&p=" + nextPage;
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

        //禁用右上角微信菜单
        document.addEventListener('WeixinJSBridgeReady', function onBridgeReady() {
            WeixinJSBridge.call('hideOptionMenu');
        });

    </script>
</body>
</html>
