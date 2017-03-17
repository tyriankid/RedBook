<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="quan.category.aspx.cs" Inherits="RedBook.quan_category" %>
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
    <script src="js/jquery190.js" type="text/javascript"></script>
    <script src="http://res.wx.qq.com/open/js/jweixin-1.0.0.js"></script>
    <script src="/js/fastclick.js"></script>
    <script src="/js/vue.min.js"></script>
    <script src="/js/vue-resource.min.js"></script>
    <style type="text/css">
        *{
            box-sizing:border-box;
            -webkit-box-sizing:border-box;
        }
        .quanlist{
            overflow:hidden;
        }
        .quanlist ul{
            border-top:1px solid #F4F4F4;
            border-left:1px solid #f4f4f4;
            background-color:#ffffff;
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
            border-right:none;
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
            border:none;
            outline:none;
            padding: 0 70px 0 10px;
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
    </style>
</head>
<body>
    <div id="app">
        <header class="header">
            <h2></h2>
            <a class="cefenlei" href="quan.index.aspx">
                <img width="30" height="30" src="images/fanhui.png">
            </a>
            <div class="head-r"><a href="/index.aspx" class="fr_Home"></a></div>
        </header>
        <!-- 搜索 -->
        <section class="search">
            <input class="searchTxt" type="text" name="name" value="" />
            <span v-on:click="doSearch"><img src="/images/tuan_search_icon.png" />搜索</span>
        </section>
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
    </div>
    <uc1:ucFoot ID="ucFoot1" runat="server" />
    <UI:WeixinSet ID="weixin" runat="server" />
    <script>
        
        var vm = new Vue({
            el: "#app",
            data: {
                imgroot: '',
                type:'',
                products: [],
                val:'',
                nextPage: 1
            },
            created: function () {
                this.type = getUrlParam("type");
                this.val = getUrlParam("val");
                this.fetchData();
            },
            methods: {  //quanCategoryList  //brandId
                fetchData: function () {
                    //商品详情
                    var _this = this;
                    var ajaxUrl = '';
                    if (this.type == "search") {
                        ajaxUrl = "http://" + window.location.host + "/ilerrshop?action=quanProductList&productName=" + escape($.trim(_this.val)) + "&p=" + _this.nextPage;
                    } else {
                        if (_this.val) {
                            ajaxUrl = "http://" + window.location.host + "/ilerrshop?action=quanProductList&type=" + _this.type + "&productName=" + escape($.trim(_this.val)) + "&p=" + _this.nextPage;
                        } else {
                            ajaxUrl = "http://" + window.location.host + "/ilerrshop?action=quanProductList&type=" + _this.type + "&p=" + _this.nextPage;
                        }
                    }
                    console.log(ajaxUrl);
                    this.$http.get(ajaxUrl).then(function (response) {
                        var _this = this;
                        this.imgroot = response.data.imgroot;
                        this.products = response.data.data;
                        this.nextPage = response.data.nextPage;
                        if (response.data.nextPage == 0) {
                            setTimeout(function () {
                                $("#divGoodsLoading").html("加载完成");
                            },500)
                        }
                        if (this.type == "search") {
                            $(".searchTxt").val(_this.val);
                        }
                    });
                },
                loadMore: function () {
                    if (this.nextPage == 0) {
                        $("#divGoodsLoading").text("加载完成");
                        return false;
                    }
                    var ajaxUrl = "http://" + window.location.host + "/ilerrshop?action=quanProductList&type=" + this.type + "&p=" + this.nextPage;
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
                doSearch: function () {
                    var _this = this;
                    this.nextPage = 1;
                    this.val = $(".searchTxt").val();
                    if (this.type == "youhui") {
                        _this.type = "search";
                    }
                    this.fetchData();
                }
            }
        })

        window.onload = function () {
            var type = getUrlParam("type");
            switch (type) {
                case "baokuan":
                    $("header h2").html("爆款");
                    break;
                case "tejia":
                    $("header h2").html("特价");
                    break;
                case "youhui":
                    $("header h2").html("优惠");
                    break;
                case "search":
                    $("header h2").html("搜索");
                    break;
                case "新手":
                    $("header h2").html("新手");
                    break;
            }
            FastClick.attach(document.body);
        }


        //自动加载
        $(window).scroll(function () {
            if ($(window).scrollTop() == $(document).height() - $(window).height()) {
                vm.loadMore();
            }
        });

        //页面传参
        function getUrlParam(name) {
            var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)"); //构造一个含有目标参数的正则表达式对象
            var r = window.location.search.substr(1).match(reg);  //匹配目标参数
            if (r != null) return unescape(r[2]); return null; //返回参数值
        }
    </script>

</body>


</html>
