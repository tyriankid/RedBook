<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="quan.index.aspx.cs" Inherits="RedBook.quan_index" %>
<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>
<%@ Register TagPrefix="UI" Namespace="Taotaole.Common" Assembly="Taotaole.Common" %>
<%@ Register Src="/Controls/ucFoot.ascx" TagName="ucFoot" TagPrefix="uc1" %>

<!DOCTYPE html>
<html>
<head>
    <meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
    <title>直购专区</title>
    <meta content="app-id=518966501" name="apple-itunes-app" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, user-scalable=no, maximum-scale=1.0" />
    <meta content="yes" name="apple-mobile-web-app-capable" />
    <meta content="black" name="apple-mobile-web-app-status-bar-style" />
    <meta content="telephone=no" name="format-detection" />
    <META HTTP-EQUIV="pragma" CONTENT="no-cache"> 
    <META HTTP-EQUIV="Cache-Control" CONTENT="no-cache, must-revalidate"> 
    <META HTTP-EQUIV="expires" CONTENT="0">
    <link href="/css/comm.css" rel="stylesheet" type="text/css" />
    <link href="/css/tuan.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="/css/style.css">
    <script src="/js/swiper.min.js"></script>
    <script src="http://res.wx.qq.com/open/js/jweixin-1.0.0.js"></script>
    <script src="/js/wxShare.js" type="text/javascript"></script>
    <script src="/js/jquery-1.8.2.js" language="javascript" type="text/javascript"></script>
    <script src="/js/iscroll.js" language="javascript" type="text/javascript"></script>
    <script src="/js/fastclick.js"></script>
    <script src="/js/vue.min.js"></script>
    <script src="/js/vue-resource.min.js"></script>
    <style type="text/css">
        .quanlist{
            background-color:#ffffff;
            overflow:hidden;
        }
        .quanlist ul{
            border-top:1px solid #F4F4F4;
            border-left:1px solid #f4f4f4;
            overflow:hidden;
        }
        .quanlist ul li{
            float:left;
            width:50%;
            border-right:1px solid #F4F4F4;
            border-bottom:1px solid #f4f4f4;
        }
        .noPro{
            width:100%;
            text-align:center;
            padding: 20px 0;
            border-top:1px solid #F4F4F4;
            border-bottom:1px solid #f4f4f4;
        }
        .quanlist ul li a{
            position:relative;
            display:block;
            padding:10px;
            overflow:hidden;
        }
        .quanlist ul li a .imgBox{
            width: 8rem;
            height: 8rem;
            margin: 1rem auto;
            background: url("/images/icon/default_square.png") no-repeat;
            background-size: 100% 100%;
        }
        .quanlist ul li a .baokuan,
        .quanlist ul li a .tejia{
            position:absolute;
            top:5px;
            padding: 2px 20px;
            color:#fff;
            background-color:#FF5152;
        }
        .quanlist ul li a .baokuan{
            left:-20px;
            transform:rotate(-45deg);
        }
        .quanlist ul li a .tejia{
            right:-20px;
            transform:rotate(45deg);
        }
        .quanlist ul li a .imgBox img{
            width:100%;
            height:100%;
            vertical-align:middle;
        }
        .quanlist ul li a .goodInfo .title{
            text-align:center;
            line-height:20px;
            padding:0 10px;
            font-size:14px;
            white-space:nowrap;
            text-overflow:ellipsis;
            overflow:hidden;
        }
        .quanlist ul li a .goodInfo .price{
            padding: 5px 0;
            text-align:left;
            color:#FF5152
        }
        .quanlist ul li a .goodInfo .price span{
            position:absolute;
            right:10px;
            bottom:10px;
            padding:5px;
            border:1px solid #FF5152;
            border-radius:5px;
            color:#FF5152;
        }
        .quanlist ul li a .goodInfo .price em{
            display:inline-block;
            margin-top:5px;
            font-size:16px;
            font-weight:bold;
        }
        .search{
            position:relative;
            padding:10px 15px;
            background-color:#fff;
            border-bottom:1px solid #F4F4F4;
            overflow:hidden;
        }
        .search input{
            width:100%;
            height:36px;
            line-height:36px;
            background-color:#fff;
            padding: 0 70px 0 10px;
            border:none;
            outline:none;
            border: 1px solid #FF5152;
            border-radius:5px;
        }
        .search span{
            position:absolute;
            top:10px;
            right:15px;
            width:20%;
            min-width: 71px;
            padding:0 10px;
            line-height:36px;
            font-size:13px;
            color:#ffffff;
            text-align:center;
            border-radius:0 5px 5px 0;
            background-color:#FF5152;
        }
        .search span img{
            position:relative;
            top:-1px;
            width:20px;
            display:inline-block;
            margin-right:5px;
            vertical-align:middle;
        }
        .nav{
            width: 100%;
            text-align: center;
            position: relative;
            margin-top: 0;
            padding-top: 10px;
            background-color: #FFF;
        }
        .nav ul{
            overflow:hidden;
        }
        .nav ul li{
            float: left;
            width:25%;
            margin-bottom: 5px;
        }
        .nav ul li a{
            position: relative;
            display: block;
            height: 60px;
            font-size: 12px;
            width: 60px;
            text-align: center;
            margin: 0 auto;
            color: #676565;
            background-size:40px auto;
            background-repeat:no-repeat;
            background-position:top center;
        }
        .nav ul li a em{
            display: block;
            width: 60px;
            height: 18px;
            position: absolute;
            bottom: 0;
        }
        .nav ul li a.xinshou{
            background-image:url(/images/tuan_xinshou_icon.png);  
        }
        .nav ul li a.baokuan{
            background-image:url(/images/tuan_baokuan_icon.png);  
        }
        .nav ul li a.tejia{
            background-image:url(/images/tuan_tejia_icon.png);  
        }
        .nav ul li a.youhui{
            background-image:url(/images/tuan_youhui_icon.png);  
        }
        .nav ul li a.shouji{
            background-image:url(/images/tuan_shouji_icon.png);  
        }
        .nav ul li a.haiwai{
            background-image:url(/images/tuan_haiwai_icon.png);  
        }
        .nav ul li a.geren{
            background-image:url(/images/tuan_geren_icon.png);  
        }
        .nav ul li a.fenlei{
            background-image:url(/images/tuan_fenlei_icon.png);  
        }
        .classify{
            background: #fff;
            border-top: 1px solid #f4f4f4;
            overflow:hidden;
        }
        .classify ul{
            overflow:hidden;
        }
        .classify ul li{
            padding:10px 15px;
            overflow:hidden;
        }
        .classify ul li div{
            float:left;
            font-size:14px;
        }
        .classify ul li div:first-child{
            width:25%;
            font-weight:bold;
        }
        .classify ul li div:last-child{
            width:75%;
        }
        .classify ul li div:last-child i{
            display:inline-block;
            font-style:normal;
            margin-right:5px;
        }
        .classify ul li div:last-child i.hideCate{
            display:none;
        }
        .classify ul li div:last-child i.moreCategory{
            text-decoration:underline;
        }
    </style>
</head>
<body>
    <div id="app">
        <%--<header class="header">
            <h2>直购专区</h2>
            <a id="fanhui" class="cefenlei" onclick="history.go(-1)" href="javascript:;">
                <img width="30" height="30" src="/images/fanhui.png">
            </a>
            <div class="head-r"><a href="/index.aspx" class="fr_Home"></a></div>
        </header>--%>

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
        <!-- 搜索 -->
        <section class="search">
            <input class="searchTxt" type="text" name="name" value=" " />
            <span onclick="doSearch()"><img src="/images/tuan_search_icon.png" />搜索</span>
        </section>
        <!-- 导航 -->
        <section class="nav">
            <ul>
                <li><a href="quan.category.aspx?type=xinshou" class="xinshou"><em>新手</em></a></li>
                <li><a href="quan.category.aspx?type=baokuan" class="baokuan"><em>爆款</em></a></li>
                <li><a href="quan.category.aspx?type=tejia" class="tejia"><em>特价</em></a></li>
                <li><a href="quan.category.aspx?type=youhui" class="youhui"><em>优惠</em></a></li>
                <li><a href="index.AllGoods.aspx?type=phone" class="shouji"><em>手机数码</em></a></li>
                <li><a href="index.AllGoods.aspx?type=overseas" class="haiwai"><em>海外爽购</em></a></li>
                <li><a href="index.AllGoods.aspx?type=person" class="geren"><em>个人护理</em></a></li>
                <li><a href="index.AllGoods.aspx" class="fenlei"><em>分类</em></a></li>
            </ul>
        </section>
        <%--<section class="classify">
            <ul>
                <li v-for="(cate,X) in category">
                    <div v-on:click="goAllGoods(cate.cateid)">{{ cate.name }}</div>
                    <div v-if="cate.brand.length <= 4">
                        <template v-for="b in cate.brand">
                            <i v-on:click="refreshProducts(b.id)">{{ b.name }}</i>
                        </template>
                    </div>
                    <div v-else>
                        <i v-for="(b,index) in cate.brand" v-on:click="refreshProducts(b.id)" v-bind:class="{a:index>=4,hideCate:index>=4 }">{{ b.name }}</i>
                        <i v-on:click="showCategory(event)" class="moreCategory">更多</i>
                    </div>
                </li>
            </ul>
        </section>--%>
        <!--团购商品列表-->
        <section class="quanlist">
            <template v-if="products.length > 0">
                <ul>
                    <li v-for="good in products">
                        <a v-on:click="goodsDetial(good.QuanId)">
                            <span v-if="good.baokuan" class="baokuan">爆款</span>
                            <span v-if="good.tejia" class="tejia">特价</span>
                            <div class="imgBox">
                                <img class="tlist-img" :src="imgroot + good.thumb">
                            </div>
                            <div class="goodInfo">
                                <div class="title">{{ good.title }}</div>
                                <div class="price">￥<em>{{parseInt(good.QuanMoney)}}</em>元<span>立即抢购</span></div>
                            </div>
                        </a>
                    </li>
                </ul>
                <div id="divGoodsLoading" class="loading"><b></b>正在加载</div>
            </template>
            <template v-else>
                <div class="noPro">暂无产品</div>
            </template>
        </section>
        <input id="urladdress" value="" type="hidden" />
        <input id="pagenum" value="1" type="hidden" />
    </div>
    <uc1:ucFoot ID="ucFoot1" runat="server" />
    <UI:WeixinSet ID="weixin" runat="server" />
    <script>
        var vm = new Vue({
            el: "#app",
            data: {
                category:null,
                imgroot:'',
                products: [],
                nextPage: 1,
                more:false
            },
            created: function () {
                this.fetchData();
            },
            methods: {  //quanCategoryList  //brandId
                fetchData: function () {
                    //分类
                    var ajaxUrl1 = "http://" + window.location.host + "/ilerrshop?action=quanCategoryList";
                    this.$http.get(ajaxUrl1).then(function (response) {
                        //console.log(response.data);
                        this.category = response.data.category;
                        var _this = this;
                        for (var i in this.category) {
                            _this.category[i].more = false;
                        }
                    })

                    //商品详情
                    var ajaxUrl2 = "http://" + window.location.host + "/ilerrshop?action=quanProductList&p=" + this.nextPage;
                    this.$http.get(ajaxUrl2).then(function (response) {
                        this.imgroot = response.data.imgroot;
                        this.products = response.data.data;
                        this.nextPage = response.data.nextPage;
                        if (response.data.nextPage == 0) {
                            setTimeout(function () {
                                $("#divGoodsLoading").text("加载完成");
                            },500)
                        }
                    });
                },
                loadMore: function () {
                    if (this.nextPage == 0) {
                        $("#divGoodsLoading").text("加载完成");
                        return false;
                    }
                    var ajaxUrl = "http://" + window.location.host + "/ilerrshop?action=quanProductList&p=" + this.nextPage;
                    this.$http.get(ajaxUrl).then(function (response) {
                        if (response.data.state == 0) {
                            this.nextPage = response.data.nextPage;
                            //this.products = this.products.concat(response.data.data);
                            this.$set(this, 'products', this.products.concat(response.data.data));
                        }
                    });
                },
                goodsDetial: function (id) {
                    window.location.href = "/quan.item.aspx?quanid=" + id;
                },
                refreshProducts: function (id) {
                    $("#divGoodsLoading").html("<b></b>正在加载");
                    this.nextPage = 1;
                    var ajaxUrl = "http://" + window.location.host + "/ilerrshop?action=quanProductList&brandId=" + id + "&p=" + this.nextPage;
                    console.log(ajaxUrl);
                    this.$http.get(ajaxUrl).then(function (response) {
                        if (response.data.state == 0) {
                            this.imgroot = response.data.imgroot;
                            this.products = response.data.data;
                            this.nextPage = response.data.nextPage;
                            if (this.nextPage == 0) {
                                $("#divGoodsLoading").text("加载完成");
                            }
                        } else {
                            this.products = null;
                            $("#divGoodsLoading").text("加载完成");
                        }
                    });
                },
                showCategory: function (e) {
                    var i = e.target;
                    if (i.innerText == "更多") {
                        i.innerText = "收起";
                        var span = $(i).siblings();
                        $(span).each(function () {
                            if (this.className.indexOf("a") >= 0) {
                                this.className = "a";
                            }
                        });
                        
                    } else {
                        i.innerText = "更多";
                        var span = $(i).siblings();
                        $(span).each(function () {
                            if(this.className.indexOf("a") >= 0){
                                this.className += " hideCate";
                            }
                        })
                    }
                },
                goAllGoods: function (id) {
                    window.location.href = "/index.AllGoods.aspx?category=" + id;
                }
            }
        })

        function doSearch() {
            var val = $.trim($(".searchTxt").val());
            window.location.href = "quan.category.aspx?type=search&val=" + escape(val);
        }

        window.onload = function () {
            FastClick.attach(document.body);
            setWxShareInfoDefault();
            //getdata(true);
            getSlidePic();
            
        }
        
        function cleanWhitespace(element) {
            for (var i = 0; i < element.childNodes.length; i++) {
                var node = element.childNodes[i];
                if (node.nodeType == 3 && !/\S/.test(node.nodeValue)) {
                    node.parentNode.removeChild(node);
                }
            }
        }

        function setWxShareInfoDefault() {
            /*
            var shareImg = "http://" + window.location.host + "/images/icon/taotaole.png";
            var shareTitleDes = "来来来，老司机带你来拼团";
            var shareDes = "iPhone，话费，游戏皮肤，统统只要0.1元拼，呼朋唤友一起来吧！";
            var link = "http://" + window.location.host + "/WxTuan/tuan.index.aspx";
            WinXinShareMessage(shareTitleDes, shareDes, link, shareImg);
            */
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

        function getdata(isRefreshProductList) {
            if (isRefreshProductList) {
                nextPage = 1;
                $(".goodsList ul").html("");
            }
            if (parseInt($("#pagenum").val()) >= nextPage) { return;}//防止重复加载
            var ajaxUrl = "http://" + window.location.host + "/ilerrshop?action=quanProductList&p=" + nextPage;
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
                        li += '<li><a href="http://' + window.location.host + '/quan.item.aspx?quanid=' + e.data[i].QuanId + '"><span id="' + e.data[i].QuanId + '" class="z-Limg">' +
                                imgsrc + '</span><div class="goodsListR">' +
                                    '<div class="tt">' + e.data[i].title + '</div>' +
                                    '<div class="tlist-info2"><span class="money">直购价￥<em>' +parseInt(e.data[i].QuanMoney) + '</em></span></div>' +
                                    '<div class="btn-tuan"><span class="btn">购买</span></div>' +
                                    //'</div></div><div class="tlist-info"><span class="num"><i></i>' + e.data[i].total_num + '人团</span></div>' +
                                    
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
            if ($(window).scrollTop() == $(document).height() - $(window).height()) {
                vm.loadMore();
            }
        });

        //开启右上角微信菜单
        document.addEventListener('WeixinJSBridgeReady', function onBridgeReady() {
            WeixinJSBridge.call('showOptionMenu');
        });
    </script>

</body>


</html>
