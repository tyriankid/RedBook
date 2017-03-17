<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.quan_Item.aspx.cs" Inherits="RedBook.quan_item" %>
<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>
<%@ Register TagPrefix="UI" Namespace="Taotaole.Common" Assembly="Taotaole.Common" %>
<html>

<head>
    <meta name="msapplication-tap-highlight" content="no"> 
    <meta property="qc:admins" content="7545416270657117166375777">
    <meta charset="utf-8">
    <meta http-equiv="x-ua-compatible" content="ie=edge">
    <meta name="renderer" content="webkit">
    <title>爽乐购</title>
    <meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no">
    <link href="css/comm.css?v=20150129" rel="stylesheet" type="text/css" />
    <link href="css/goods.css" rel="stylesheet" type="text/css" />
    <link href="css/style.css" rel="stylesheet" type="text/css" />
    <script src="http://res.wx.qq.com/open/js/jweixin-1.0.0.js"></script>
    <script src="js/jquery190.js" type="text/javascript"></script>
    <script src="js/swiper.min.js"></script>
    <%--获取ip地址及省市--%>
    <script src="js/QuickPay.js?v=1.1"></script>
    <script src="js/guanzhu.js?v=1.1"></script>
    <script src="internalapi/common.js" type="text/javascript"></script>
    <script src="js/fastclick.js"></script>
    <script src="js/AlertBox.js?v=1"></script>
	<script src="http://res.wx.qq.com/open/js/jweixin-1.0.0.js"></script>
	<script src="js/wxShare.js" type="text/javascript"></script>
    <script src="js/jquery.rotate.js"></script>
    <style>
        #buyContent{
            padding:10px 0;
            background-color:#fff;
        }
        #buyContent img{
            width:100%;
            vertical-align:middle;
        }
        .pDetails p.title{
            padding-top: 20px;
        }
        .pDetails p.price{
            margin-bottom:0;
            font-size:24px;
            color:#ff6a00;
        }
    </style>
</head>

<body>
    <header class="header">
        <h2>商品详情</h2>
        <a class="cefenlei" onclick="window.location.href=document.referrer;" href="javascript:;">
            <img width="30" height="30" src="images/fanhui.png">
        </a>
        <div class="head-r"><a href="/index.aspx" class="fr_Home"></a></div>
    </header>

    <!-- 详情内容 Start -->
    <div id="ItemWrapper">
        <section class="goodsCon pCon">
            <!-- 焦点图 -->
            <div class="swiper-container swiper-pPicBor">
                <div class="swiper-wrapper">
                </div>
                <div class="swiper-pagination swiper-pagination-clickable">
                </div>
            </div>
            <script>
                var mySwiper = new Swiper('.swiper-container', {
                    autoplay: 5000, //可选选项，自动滑动
                    pagination: '.swiper-pagination',
                    paginationClickable: true,
                    observer: true, //修改swiper自己或子元素时，自动初始化swiper
                    observeParents: true //修改swiper的父元素时，自动初始化swiper
                })
			</script>
            <!-- 焦点图 End -->

            <!-- 参与记录，奖品详细，晒单导航 -->
            <div class="joinAndGet">
                <dl>
                    <a class="detail" href="javascript:void(0);">图文详情<em>（建议WIFI下使用）</em> </a>
                </dl>
            </div>
            <div id="buyContent">

            </div>
            <p class="goodsCheckbox">
                <a href="#">我已阅读并同意《爽乐购服务协议》</a>
            </p>
            <div class="part-box">
                <!-- 参与记录，奖品详细，晒单导航 End -->
            </div>
        </section>
    </div>
    <ul class="alityBtn">
        <li onclick="goBackTop()">
            <img src="/images/goBackTop.png" alt="返回顶部" /></li>
    </ul>
    <UI:WeixinSet ID="weixin" runat="server" />
    <!-- 详情内容 End -->
    <script type="text/javascript">
        //打开页面加载数据
        window.onload = function () {
            FastClick.attach(document.body);

            $("#pagenum").val("0");
            quanid = getUrlParam("quanid");
            getGoodsInfo();
            reloadPage = true;

            //document.addEventListener('touchmove', function (e) {
            //    e.preventDefault();
            //}, false);
        }

        
        //返回顶部
        function goBackTop() {
            $("html,body").animate({
                scrollTop: 0
            }, 0);
        }

        //直购流程
        
        function setQuanPaymentParms(e) {
            var ajaxUrl = "http://" + window.location.host + '/ilerrpay?action=userpaymentQuan&quanid=' + quanid;
            var isQuickPay = false;
            var isoutpay;
            $.ajax({
                type: 'get', dataType: 'json', timeout: 10000, async: false, url: ajaxUrl, success: function (data) {
                    if (data.isPayOut == "11") {
                        if (!confirm("支付失败,您今日已消费达5000元,确定继续支付吗?")) {
                            isoutpay = true;
                        }
                    }
                    /*
                    if (data.moneyEnough == 1) {//余额足够快速支付
                        isQuickPay = true;
                    }
                    else {
                        isQuickPay = false;
                    }
                    */
                    //去掉直购商品的余额支付流程,一律在线支付
                    isQuickPay = false;
                }
            });
            if (isoutpay) {
                return;
            }
           
            if (parseInt(stock) <= 0) {
                alert("商品库存不足！"); return;
            }

            if (isQuickPay) {
                e.html("正在支付中...");
                var ajaxUrl = "http://" + window.location.host + '/ilerrpay?action=userpaymentQuanSubmit&quanid=' + quanid + '&paytype=' + paytype + '&ip=' + ip;
                var ip = "";// returnCitySN["cname"] + returnCitySN["cip"];
                $.ajax({
                    type: 'get', dataType: 'json', timeout: 10000, async: false, url: ajaxUrl, success: function (data) {
                        switch (data.state) {
                            case 0://验证通过
                                alert("支付成功!");
                                location.href = "user.quanconfirm.aspx?orderid=" + data.orderid;
                                break;
                            case 5:
                                alert("库存不足!");
                            default:
                                alert("支付失败!错误码：" + data.state);
                                break;
                        }
                    }
                });
            }
                //否则跳转到付款页面
            else {
                location.href = "http://" + window.location.host + "/WxPay/cart.payment.aspx?quanid=" + quanid;
            }
        }


        var quanid;
        var productid;
        var quanProductid;
        var stock;
        //页面传参
        function getUrlParam(name) {
            var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)"); //构造一个含有目标参数的正则表达式对象
            var r = window.location.search.substr(1).match(reg);  //匹配目标参数
            if (r != null) return unescape(r[2]); return null; //返回参数值
        }

        function setWxShareInfo(data) {
            /*
            var shareImg = data.imgroot + data.Thumb;
            var shareTitleDes = '超赞！这里的商品只用1元钱就可以获得了，快来一起参与吧！';
            var shareDes = "最新一期" + data.Title + "，" + data.Description;
            var link = "http://" + window.location.host + "/index.item.aspx?productid=" + data.Productid;
            WinXinShareMessage(shareTitleDes, shareDes, link, shareImg);
            */
        }

        //获取商品详情
        function getGoodsInfo() {
            var ajaxUrl = "http://" + window.location.host + "/ilerrshop?action=quanDetail&QuanId=" + quanid;
            console.log(ajaxUrl);
            $.ajax({
                type: 'get', dataType: 'json', timeout: 10000,
                url: ajaxUrl,
                success: function (data) {
                    console.log(data);
                    wxdetail(data);
                    $("#buyContent").append(data.contents);
                }
            })
        }

        function wxdetail(e) {
            setWxShareInfo(e);
            stock = e.stock;
            productid = e.Productid;
            yunJiage = e.QuanMoney;

            quanProductid = e.QuanProductid;//直购
            var DetailSrc = e.picarr.split(','), //幻灯片路径
                swiperContent = "";//幻灯片容器

            for (var i = 0; i < DetailSrc.length; i++) {
                if (DetailSrc[i] == "") continue;
                swiperContent += '<div class="swiper-slide" style="width:100%;">' +
                                    '<li><img src="' + e.imgroot + DetailSrc[i] + '" class="animClass"></li>' +
                                '</div>';
            }
            $(".swiper-wrapper").append(swiperContent);

            GoodState(e);
        }


        function GoodState(e) {
            //是否买过奔商品
            var ISBoughtTxt = "您本期没有参加哦，快到最新期试试手气吧！";
            var DetailContent = ""; //商品详情容器
            var BottomBtnBox = ""; //底部按钮框状态内容
            //正在进行中的状态
            BottomBtnBox +='<div id="btnBuyBox" class="pBtn" codeid="">' +
                                '<a style="width: 55%;" onclick="setQuanPaymentParms($(this))" productid="' + e.Productid + '" href="javascript:;" class="buyBtn">直接购买</a>' +
                            '</div>';
            DetailContent += '<div class="pDetails"><p class="title">' + e.title + '</p>' +
                '<p class="price">￥' + e.QuanMoney + '</p>' + '<p>(库存:' + e.stock + '件)</p>'+
                '<div class="dec">' + e.description + '</div>' + BottomBtnBox

                '</div>' ;

            $(".pDetails").remove();
            $(".trybox").remove();
            $(".swiper-container").after(DetailContent);
        }


        //开启右上角微信菜单
        document.addEventListener('WeixinJSBridgeReady', function onBridgeReady() {
            WeixinJSBridge.call('showOptionMenu');
        });
		</script>
</body>

</html>
