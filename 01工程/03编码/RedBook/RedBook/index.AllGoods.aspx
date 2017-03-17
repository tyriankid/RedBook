<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.AllGoods.aspx.cs" Inherits="RedBook.index_AllGoods" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta http-equiv="x-ua-compatible" content="ie=edge">
    <meta name="renderer" content="webkit">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, user-scalable=no, maximum-scale=1.0" />
    <meta content="yes" name="apple-mobile-web-app-capable" />
    <meta content="black" name="apple-mobile-web-app-status-bar-style" />
    <meta name="msapplication-tap-highlight" content="no" />
    <title>爽乐购-全部分类</title>
    <script src="/js/jquery.js"></script>
    <!-- 中奖确认js -->
    <script src="http://res.wx.qq.com/open/js/jweixin-1.0.0.js"></script>
    <script src="/js/wxShare.js" type="text/javascript"></script>
    <link href="/css/comm.css" rel="stylesheet" type="text/css" />
    <link href="/css/main.css" rel="stylesheet" type="text/css">
    <link href="/css/all.goods.css" rel="stylesheet" type="text/css" />
    <script src="/js/jquery190.js" language="javascript" type="text/javascript"></script>
    <script src="js/fastclick.js"></script>
    <script src="/js/QuickPay.js?v=2.6"></script>
    <script src="/js/AlertBox.js?v=2.4"></script>
    <script src="/js/vue.min.js"></script>
    <script src="/js/vue-resource.min.js"></script>
    <style type="text/css">
        .goods-num .price{
            color:#f00;
        }
        .goods-num .price span{
            font-size:16px;
        }
        .link-playbox a{
            display:inline;
            padding: 5px;
            border: 1px solid #FF5152;
            border-radius: 5px;
            color: #FF5152;
        }
        .g-subimglist li{
            position:relative;
        }
        .classifySmBox{
            display:none;
            position:relative;
            width:100%;
            border-bottom: 1px solid #F5F5F5;
            overflow-x:scroll;
            overflow-y:hidden;
        }
        .classifySmBox ul{
            overflow:hidden;
        }
        .classifySmBox li{
            float:left;
            width:80px;
            line-height:36px;
            border-right:1px solid #f5f5f5;
            text-align:center;
        }
        .classifySmBox li:last-child{
            border-right:0;
        }
        .classifySmBox li.select{
            border-bottom:2px solid #FF5152;
        }
        .g-subimglist{
            position:relative;
            overflow:hidden;
        }
        .g-subimglist .baokuan,
        .g-subimglist .tejia{
            position:absolute;
            top:5px;
            padding: 2px 20px;
            color:#fff;
            background-color:#FF5152;
            z-index:2;
        }
        .g-subimglist .baokuan{
            left:-20px;
            transform:rotate(-45deg);
        }
        .g-subimglist .tejia{
            right:-20px;
            transform:rotate(45deg);
        }
    </style>
</head>
<body>
    <div id="app">
        <header class="header headerFix">
            <h2>所有商品</h2>
            <a class="cefenlei" href="quan.index.aspx">
                <img width="30" height="30" src="images/fanhui.png">
            </a>
            <div class="head-r"><a href="/index.aspx" class="fr_Home"></a></div>
        </header>
        <section class="goodsCon" style="position: relative; padding-top: 45px;">
            <!-- 导航 -->
            <section class="classify-lbox">
                <div class="cla-goodsbox">
                    <ul class="cla-goods">
                        <li id="list" type="0" class="li-all sOrange" v-on:click="chooseCate(99,$event.currentTarget)">
                            <a href="javascript:;"><em class="icon-all"></em><span>全部商品</span></a>
                        </li>
                        <li v-for="(cate,index) in classify" :id="cate.cateid" v-on:click="chooseCate(index,$event.currentTarget,cate.cateid)">
                            <a href="javascript:;">
                                <em v-bind:style="styleObject(index,cate.weixuanzhong,cate.xuanzhong)"></em>
                                <span>{{ cate.name }}</span>
                            </a>
                        </li> 
                    </ul>
                </div>
            </section>
            <!-- 列表 -->
            <section class="classify-rbox">
                <div class="classifySmBox">
                    <ul>
                        <li v-on:click="classifySmDetial($event.currentTarget)">全部</li>
                        <li v-for="c in classifySm" v-on:click="classifySmDetial($event.currentTarget,c.id)">{{ c.name }}</li>
                    </ul>
                </div>
                <div class="goodsList">
                    <ul class="g-subimglist" v-for="good in products">
                        <span v-if="good.baokuan" class="baokuan">爆款</span>
                        <span v-if="good.tejia" class="tejia">特价</span>
                        <li v-on:click="goodsDetial(good.QuanId)">
                            <span id="Span3" class="z-Limg"><img :src="imgroot + good.thumb"></span>
                            <div class="goodsListR">
                                <h2 id="good.productid">{{ good.title }}</h2>
                                <div class="pRate item_bottom_container">
                                    <div class="goods-num">
                                        <p class="price">￥<span>{{ good.money }}</span>元</p>
                                    </div>
                                </div>
                                <div class="link-playbox">
                                    <a href="javascript:void(0)">立即抢购</a>
                                </div>
                            </div>
                        </li>
                    </ul>
                </div>
                <div id="divLoad"><b></b>正在加载...</div>
            </section>
        </section>
        <ul class="alityBtn">
            <li onclick="goBackTop()">
                <img src="/images/goBackTop.png" alt="返回顶部" /></li>
            <li onclick="refresh()">
                <img src="/images/refresh.png" alt="页面刷新" /></li>
        </ul>
    </div>
    <input id="urladdress" value="" type="hidden" />
    <input id="pagenum" value="" type="hidden" /><!--已加载到第几页-->
    <script type="text/javascript">
        //页面传参
        function getUrlParam(name) {
            var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)"); //构造一个含有目标参数的正则表达式对象
            var r = window.location.search.substr(1).match(reg);  //匹配目标参数
            if (r != null) return unescape(r[2]); return null; //返回参数值
        }

        var vm = new Vue({
            el: "#app",
            data: {
                imgroot:"",
                classify: null,
                bgIndex:-1,
                styleObject: function (index, wxz, xz) {
                    var bg = {};
                    var imgUrl = "";
                    var _this = this;
                    if (index == _this.bgIndex) {
                        var imgUrl = "url('" + _this.imgroot + xz + "')";
                    } else {
                        var imgUrl = "url('" + _this.imgroot + wxz + "')";
                    }
                    bg.backgroundImage = imgUrl;
                    return bg;
                },
                nextPage: 1,
                products: null,
                category: null,
                classifySm: null
            },
            created: function () {
                this.fetchClassify();
            },
            methods: {
                fetchClassify: function () {
                    //商品一级分类
                    var ajaxUrl = "http://" + window.location.host + "/ilerrshop?action=getshopcategory";
                    this.$http.get(ajaxUrl).then(function (response) {
                        //console.log(response.data);
                        this.imgroot = response.data.imgroot;
                        this.classify = response.data.data;

                        var _this = this;
                        setTimeout(function () {
                            var type = getUrlParam("type");
                            if (type) {
                                var ele = "";
                                switch (type) {
                                    case "phone":
                                        ele = $(".cla-goods li span:contains(手机)");
                                        var li = $(ele).parents("li");
                                        var index = $(li).index() - 1;
                                        var id = $(li).attr("id");
                                        _this.chooseCate(index, li, id);
                                        break;
                                    case "overseas":
                                        ele = $(".cla-goods li span:contains(海外)");
                                        var li = $(ele).parents("li");
                                        var index = $(li).index() - 1;
                                        var id = $(li).attr("id");
                                        _this.chooseCate(index, li, id);
                                        break;
                                    case "person":
                                        ele = $(".cla-goods li span:contains(个人)");
                                        var li = $(ele).parents("li");
                                        var index = $(li).index() - 1;
                                        var id = $(li).attr("id");
                                        _this.chooseCate(index, li, id);
                                        break;
                                    default:
                                        _this.fetchProducts();
                                        break;
                                }
                                
                            } else {
                                _this.fetchProducts();
                            }
                        })
                    });
                },
                fetchProducts: function () {
                    //商品列表
                    var ajaxUrl = "";
                    if (arguments.length == 0) {
                        ajaxUrl = "http://" + window.location.host + "/ilerrshop?action=quanProductList&p=" + this.nextPage;
                    }
                    if (arguments.length == 1) {
                        ajaxUrl = "http://" + window.location.host + "/ilerrshop?action=quanProductList&category=" + arguments[0] + "&p=" + this.nextPage;
                    }
                    if (arguments.length == 2) {
                        ajaxUrl = "http://" + window.location.host + "/ilerrshop?action=quanProductList&category=" + arguments[0] + "&brandId=" + arguments[1] + "&p=" + this.nextPage;
                    }
                    this.$http.get(ajaxUrl).then(function (response) {
                        this.imgroot = response.data.imgroot;
                        this.products = response.data.data;
                        this.nextPage = response.data.nextPage;
                        if (this.nextPage == 0) {
                            $("#divLoad").text("加载完成");
                        }
                    });
                },
                fetchClassifySm:function(){
                    var ajaxUrl = "http://" + window.location.host + "/ilerrshop?action=getshopBrand&cateId=" + arguments[0];
                    console.log(ajaxUrl);
                    this.$http.get(ajaxUrl).then(function (response) {
                        //console.log(response.data);
                        this.classifySm = response.data.data;
                        $(".classifySmBox ul").width((response.data.data.length + 1) * 80);
                    });
                },
                classifySmDetial: function (e, id) {
                    $(arguments[0]).addClass("select");
                    $(arguments[0]).siblings().removeClass("select");
                    if (arguments.length == 1) {
                        this.fetchProducts(this.category);
                    }
                    if (arguments.length == 2) {
                        this.fetchProducts(this.category, arguments[1]);
                    }
                },
                loadMore: function () {
                    if (this.nextPage == 0) {
                        $("#divLoad").text("加载完成");
                        return false;
                    }
                    var ajaxUrl = "http://" + window.location.host + "/ilerrshop?action=quanProductList&p=" + this.nextPage;
                    this.$http.get(ajaxUrl).then(function (response) {
                        if (response.data.state == 0) {
                            //console.log(response.data);
                            this.nextPage = response.data.nextPage;
                            //this.products = this.products.concat(response.data.data);
                            this.$set(this, 'products', this.products.concat(response.data.data));
                        }
                    });
                },
                chooseCate: function (index, e, id) {
                    var _this = this;
                    if (index != 99) {
                        //切换选中样式
                        this.bgIndex = index;
                        //一级分类下全部商品
                        _this.nextPage = 1;
                        _this.fetchProducts(id);
                        //获取一级分类下二级分类
                        _this.fetchClassifySm(id);
                        _this.category = id;
                        $(".classifySmBox li").removeClass("select");
                        $(".classifySmBox li").eq(0).addClass("select");
                        $(".classifySmBox").show();
                        $("html,body").scrollTop(0);
                        
                    } else {
                        _this.fetchProducts();
                        $(".classifySmBox").hide();
                    }
                    $(e).addClass("sOrange");
                    $(e).siblings().removeClass("sOrange");

                },
                goodsDetial: function (id) {
                    window.location.href = "/quan.item.aspx?quanid=" + id;
                },
            }
        })

        function setPaymentParms(e) {
            //设置好快速支付弹出层的属性
            var $productLi = e.closest('li');
            imgUrl = $productLi.find('img').attr('src');//商品图片
            codeId = e.attr('codeid');//商品编号
            yunJiage = e.attr('yunjiage');//商品单价
            priceRangeArray = e.attr('pricerange').split(",");//商品购买数量区间
            renshuLeft = $productLi.find("[role='renshuLeft']").html();//剩余购买数量
            //设置好支付事件需要的属性
            leftmoney = parseInt(""); paytype = "ajaxPay"; redpacknum = "";
            paytype = "allgoods";
            showQuickPayDiv();
        }
        //打开页面加载数据
        window.onload = function () {
            
            FastClick.attach(document.body);

        };
        //返回顶部
        function goBackTop() {
            $("html,body").animate({
                scrollTop: 0
            }, 0);
        }
        //下拉刷新处理
        function refresh() {
            $(".classify-rbox .goodsList ul").remove();
            nextPage = 1;
            $("#pagenum").val("0");
            $('#divLoad').html('<b></b>正在加载...');
            if (!id) {
                id = "list";
            }
            vm.fetchProducts();
        }

        //获取数据
        //var nextPage = 1;
        //function glist_json(parm, isRefresh) {
        //    if (isRefresh) {
        //        $(".classify-rbox .goodsList ul").remove();
        //        nextPage = 1;
        //    }
        //    $("#pagenum").val("0");
        //    if (parseInt($("#pagenum").val()) >= nextPage) { return; }//防止重复加载
        //    var ajaxUrl = "http://" + window.location.host + "/ilerrshop?action=listinfo&typeid=" + parm + "&order=10&p=" + nextPage;
        //    $.ajax({
        //        type: 'get', dataType: 'json', timeout: 10000,
        //        url: ajaxUrl,
        //        success: function (e) {
        //            if (e.count <= 0) {
        //                $("#divLoad").text("暂无数据");
        //            }
        //            if (e.count <= 10) {
        //                $("#divLoad").text("加载完成");
        //                //$("#divLoad").hide();
        //            }//不足10条隐藏下拉刷新
        //            $("#pagenum").val(nextPage);//当前页
        //            nextPage = e.nextPage;
        //            for (var i = 0; i < e.data.length; i++) {
        //                var num = (e.data[i].canyurenshu / e.data[i].zongrenshu) * 100;
        //                num = num.toFixed(2);
        //                var ul = '';
        //                ul += '<ul class="g-subimglist">' +
        //                        '<li><span id="Span3" class="z-Limg">' +
        //                            '<img src="' + e.imgroot + e.data[i].thumb + '"></span><div class="goodsListR">' +
        //                                '<h2 id="' + e.data[i].productid + '">' + e.data[i].title + '</h2>' +
        //                                '<div class="pRate item_bottom_container">' +
        //                                    '<div class="goods-num">' +
        //                                        '<div class="Progress-bar" id="Div4">' +
        //                                            '<p class="u-progress" style="margin-left: 0;"><span class="pgbar" style="width: ' + num + '%;"><span class="pging"></span></span></p>' +
        //                                            '<div class="Pro-bar-li">' +
        //                                                '<div class="P-bar02">总需<em>' + e.data[i].zongrenshu + '</em></div>' +
        //                                                '<div class="P-bar03">剩余<em role="renshuLeft">' + e.data[i].shengyurenshu + '</em></div>' +
        //                                            '</div>' +
        //                                        '</div>' +
        //                                    '</div>' +
        //                                '</div>' +
        //                                '<div class="link-playbox">' +
        //                                    '<a onclick="setPaymentParms($(this))" class="link-play" codeid="' + e.data[i].yId + '" pricerange="' + e.data[i].pricerange + '" yunjiage="' + e.data[i].yunjiage + '" href="javascript:void(0)">参与</a>' +
        //                                '</div>' +
        //                             '</div>' +
        //                        '</li>' +
        //                    '</ul>';
        //                $(".classify-rbox .goodsList").append(ul);
        //            }
        //            if (nextPage == 0) {
        //                $("#divLoad").text("加载完成");
        //            }
        //        }
        //    });
        //}

        //返回顶部
        $(window).scroll(function () {
            if ($(window).scrollTop() > 50) {
                $("#btnTop").show();
            } else {
                $("#btnTop").hide();
            }
        });
        $("#btnTop").click(function () {
            $("body").animate({
                scrollTop: 0
            }, 10);
        });


        //获取商品分类
        //function getGoodsFenlei(me) {
        //    var ajaxUrl = "http://" + window.location.host + "/ilerrshop?action=getshopcategory";

        //    $.ajax({
        //        type: 'get', dataType: 'json', timeout: 10000,
        //        url: ajaxUrl,
        //        success: function (e) {
        //            if (e.state == 2) {
        //                $(".goodsList").html("<div style='text-align:center;padding:10px;color:#999;font-size:14px;'>暂无数据</div>");
        //                return;
        //            }
        //            else if (e.state == 0) {
        //                var li = "";
        //                for (var i = 0; i < e.data.length ; i++) {
        //                    li += '<li type="0" id="' + e.data[i].cateid + '">' +
        //                            '<a href="javascript:;">' +
        //                                '<em style="background: url(' + e.imgroot + e.data[i].weixuanzhong + ') center center no-repeat;" graybg="' + e.imgroot + e.data[i].weixuanzhong + '" redbg="' + e.imgroot + e.data[i].xuanzhong + '" class="icon-' + e.data[i].cateid + '"></em><span>' + e.data[i].name + '</span></a>' +
        //                        '</li>';
        //                }
        //                $(".classify-lbox .cla-goods ").append(li);

        //                //设置点击事件
        //                $(".cla-goods li").click(function () {
        //                    var graybg = $(this).find("em").attr("graybg");
        //                    var redbg = $(this).find("em").attr("redbg");
        //                    $(this).addClass("sOrange");
        //                    if (!$(this).hasClass("li-all")) {
        //                        $(this).find("em").css("background-image", "url(" + redbg + ")");
        //                    }
        //                    var sib = $(this).siblings();
        //                    $(this).siblings().removeClass("sOrange");
        //                    $(sib).each(function () {
        //                        if (!$(this).hasClass("li-all")) {
        //                            $(this).find("em").css("background-image", "url(" + $(this).find("em").attr("graybg") + ")");
        //                        }
        //                    });
        //                    //$(".classify-rbox .goodsList ul").remove();
        //                    //nextPage = 1;
        //                    if (id == $(this).attr("id")) return;//避免重复点击导致重复加载
        //                    id = $(this).attr("id");


        //                    if (!id) {
        //                        id = "list";
        //                    }
        //                    glist_json(id, true);
        //                });
        //            }
        //        }
        //    })
        //}

        $(window).scroll(function () {
            if ($(window).scrollTop() == $(document).height() - window.screen.height) {
                //避免在没有刷新出新的商品之前进行重复刷新,同时避免再切换商品分类的时候做重复刷新,故做了当前值和加载后值的变化判断
                //if (currentPage == nextPage || (currentId != id && currentId != "")) return; 
                vm.loadMore();
            }
        });

    </script>
</body>
</html>
