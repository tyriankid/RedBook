<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="secondsOpen.aspx.cs" Inherits="RedBook.secondsOpen" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
    <title></title>
    <meta content="app-id=518966501" name="apple-itunes-app" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, user-scalable=no, maximum-scale=1.0" />
    <meta content="yes" name="apple-mobile-web-app-capable" />
    <meta content="black" name="apple-mobile-web-app-status-bar-style" />
    <meta content="telephone=no" name="format-detection" />
    <META HTTP-EQUIV="pragma" CONTENT="no-cache"> 
    <META HTTP-EQUIV="Cache-Control" CONTENT="no-cache, must-revalidate"> 
    <META HTTP-EQUIV="expires" CONTENT="0">
    <link href="/css/comm.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="css/zdy.css">
    <script src="js/jquery190.js" type="text/javascript"></script>
    <script src="http://res.wx.qq.com/open/js/jweixin-1.0.0.js"></script>
    <script src="/js/fastclick.js"></script>
    <script src="/js/vue.min.js"></script>
    <script src="/js/vue-resource.min.js"></script>
    <script src="/js/QuickPay.js?v=2.6"></script>
    <style type="text/css">
        .loading b{
            display:inline-block;
            margin: -3px 5px;
        }
    </style>
</head>
<body>
    <div id="app">
        <header class="header">
            <h2>秒开商品</h2>
            <a class="cefenlei" href="index.aspx">
                <img width="30" height="30" src="images/fanhui.png">
            </a>
            <div class="head-r"><a href="/index.aspx" class="fr_Home"></a></div>
        </header>
        <div class="tabs-wrap">
            <div class="goodsList">
                <ul v-for="(good,index) in products">
                    <li @click="goodsDetial(good.productid,good.operation)">
                        <span :id="good.productid" :name="good.operation" class="z-Limg" style="position:relative;display:block;">
                            <img :src="imgroot + good.thumb">
                        </span>
                        <div style="margin-top:15px;" class="goodsListR">
                            <h2 id="H1">{{ good.title }}</h2>
                        </div>
                        <div class="pRate item_bottom_container">
                            <div class="goods-num">
                                <div class="Progress-bar" id="Div1">
                                    <p class="u-progress" style="margin-left:0;"><span class="pgbar"  v-bind:style="styleObject(good.canyurenshu,good.zongrenshu)" :id="'pgbar'+good.productid"><span class="pging"></span></span>
                                    </p>
                                    <div class="Pro-bar-li">
                                        <div class="P-bar02"><em>{{ good.zongrenshu }}</em>总需</div>
                                        <div class="P-bar03"><em>{{ good.shengyurenshu }}</em>剩余</div>
                                    </div>
                                </div>
                            </div>
                            <div class="link-playbox">
                                <a @click="setPaymentParms($event,index)" class="link-play" href="javascript:void(0)">参与</a>
                            </div>
                        </div>
                    </li>
                </ul>
			    <div id="divGoodsLoading" class="loading"><b></b>正在加载</div>
		    </div>
        </div>
    </div>
    <form id="form1" runat="server"></form>
    <script>
        var vm = new Vue({
            el: "#app",
            data: {
                imgroot: '',
                products: [],
                styleObject: function (c,z) {
                    var style = {};
                    var w = c / z * 100;
                    style = {
                        width:w+"%"
                    }
                    return style;
                },
                nextPage: 1
            },
            created: function () {
                this.fetchData();
            },
            methods: {
                fetchData: function () {
                    //商品详情
                    var ajaxUrl = "http://" + window.location.host + "/ilerrshop?action=listinfo&typeid=list&order=10&secsOpen=100&p=" + this.nextPage;
                    this.$http.get(ajaxUrl).then(function (response) {
                        console.log(response.data);
                        this.imgroot = response.data.imgroot;
                        this.products = response.data.data;
                        this.nextPage = response.data.nextPage;
                        if (response.data.nextPage == 0) {
                            setTimeout(function () {
                                $("#divGoodsLoading").html("加载完成");
                            }, 500)
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
                goodsDetial: function (id, operation) {
                    window.location.href = "/index.item.aspx?productid=" + id + "&operation=" + operation;
                },
                setPaymentParms: function (event, index) {
                    var e = event.currentTarget;
                    var good = this.products[index];
                    imgUrl = this.imgroot+good.thumb; //商品图片
                    codeId = good.yId; //商品编号
                    yunJiage = good.yunjiage; //商品单价
                    priceRangeArray = good.pricerange.split(","); //商品购买数量区间
                    renshuLeft = good.shengyurenshu; //剩余购买数量
                    //设置好支付事件需要的属性
                    paytype = "ajaxPay";
                    showQuickPayDiv();

                    event.stopPropagation();
                }
            }
        })

        window.onload = function () {
            FastClick.attach(document.body);
        }

        //自动加载
        $(window).scroll(function () {
            if ($(window).scrollTop() == $(document).height() - $(window).height()) {
                vm.loadMore();
            }
        });
    </script>
</body>
</html>
