<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.goodsdesc.aspx.cs" Inherits="RedBook.index_goodsdesc" %>
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
        .sharebtn{
            padding:10px 0;
            font-size: 15px;
        }
        .sharebtn span{
            display:inline-block;
            border:1px solid #E2E2E1;
            padding:2px 10px;
            margin:0 5px;
            background-color:#fff;
        }
        .sharebtn span.select{
            color:#f00;
            border-color:#f00;
        }
    </style>
</head>

<body>
    <header class="header">
        <h2>商品详情</h2>
        <a class="cefenlei" href="javascript:history.go(-1);">
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
                    <a class="detail" href=""><b class="fr z-arrow"></b>图文详情<em>（建议WIFI下使用）</em> </a>
                    <a class="shaidan" href="/index.goodspost.aspx"><b class="fr z-arrow"></b>晒单分享</a>
                    <a class="wangqi" href=""><b class="fr z-arrow"></b>往期揭晓</a>
                </dl>
            </div>
            <p class="goodsCheckbox">
                <a href="#">我已阅读并同意《爽乐购服务协议》</a>
            </p>
            <div class="part-box">
                <!-- 参与记录，奖品详细，晒单导航 End -->
                <div class="part-tt" style="border-color: #efefef; box-shadow: 0px 0px 0px #e7e7e7;"><b class="fr stime"></b>本期参与记录</div>
                <div id="divRecordList" class="recordCon z-minheight" style="display: block; height: auto; margin-top: 0px; border-left: none; min-height: 0px; background: #FFF;">
                    <div class="goodsList"></div>
                </div>
            </div>
            <div id="divLoad"><b></b>正在加载...</div>
            <input id="pagenum" value="0" type="hidden" /><!--已加载到第几页-->
        </section>
    </div>
    <ul class="alityBtn">
        <li onclick="goBackTop()">
            <img src="/images/goBackTop.png" alt="返回顶部" /></li>
        <li onclick="refresh()">
            <img src="/images/refresh.png" alt="页面刷新" id="imgref" /></li>
    </ul>
    <UI:WeixinSet ID="weixin" runat="server" />
    <!-- 详情内容 End -->
    <script type="text/javascript">
        
        //打开页面加载数据
        window.onload = function () {
            FastClick.attach(document.body);
            $("#pagenum").val("0");
            shopid = getUrlParam("shopid");
            productid = getUrlParam("productid");
            Isloading = true;
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
        //右侧按钮刷新处理
        var refvalue = 0;
        var Isloading = false;
        function refresh() {
            if (Isloading == true) return;
            Isloading = true;
            refvalue += 360;
            $("#imgref").rotate({ animateTo: refvalue });
            refreshEx();
            $("#divRecordList .goodsList").html("");
            $("#pagenum").val("0");
            $("#divLoad").html("加载中...");
            currentPage = 1;
            nextPage = 1;
            getBuyRecord();
        }
        //右侧按钮刷新处理进度条
        function refreshEx() {
            var ajaxUrl = "http://" + window.location.host + "/ilerrshop?action=detailrenovate&shopid=" + shopid;
            $.ajax({
                type: 'get', dataType: 'json', timeout: 10000,
                url: ajaxUrl,
                success: function (e) {
                    GoodState(e);
                }
            })

        }

        
        function setPaymentParms(e) {
            var alertboxHtml = "<div class='mask'></div>";
            alertboxHtml += "<div class='alertBox'>";
            alertboxHtml += "<p class='content'>请选择支付方式</p>";
            alertboxHtml += "<p class='close' onclick='closeAlertBox()'>关闭</p></div>";
            $("body").append(alertboxHtml);
            //设置好快速支付弹出层的属性
            imgUrl = $(".swiper-wrapper").find("img :first").attr('src');//商品图片
            codeId = shopid;
            //设置好支付事件需要的属性
            paytype = "singleAjaxPay";
            //判断是否从个人中心跳转过来
            var type = getUrlParam("type");
            var operation = getUrlParam("operation");
            switch (operation) {
                case "0"://团购，一元购商品
                    break;
                case "1"://活动商品
                    switch (type) {
                        case "isactivity":
                            break;
                        case null:
                            alert("此商品为活动商品 !");
                            return;
                            break;
                    }
                    break;
            }
            showQuickPayDiv();


            //quickpay.init(imgUrl, shopid, yunJiage, priceRangeArray, renshuLeft, "singleAjaxPay");
        }
        /*
        //直购流程
        function setQuanPaymentParms(e) {
            var ajaxUrl = "http://" + window.location.host + '/ilerrpay?action=userpaymentQuan&productid=' + quanProductid;
            var isQuickPay;
            $.ajax({
                type: 'get', dataType: 'json', timeout: 10000, async: false, url: ajaxUrl, success: function (data) {
                    if (data.moneyEnough==1) {//余额足够快速支付
                        isQuickPay = true;
                    }
                    else {
                        isQuickPay = false;
                    }
                }
            });

            //如果余额足够直接用余额支付
            if (isQuickPay) {
                e.html("正在支付中...");
                var ajaxUrl = "http://" + window.location.host + '/ilerrpay?action=userpaymentQuanSubmit&productid=' + quanProductid + '&paytype=' + paytype + '&ip=' + ip;
                var ip = "";// returnCitySN["cname"] + returnCitySN["cip"];
                $.ajax({
                    type: 'get', dataType: 'json', timeout: 10000, async: false, url: ajaxUrl, success: function (data) {
                        switch (data.state) {
                            case 0://验证通过
                                alert("支付成功!");
                                location.href = "user.quanconfirm.aspx?orderid=" + data.orderid;
                                break;
                            default:
                                alert("支付失败!错误码：" + data.state);
                                break;
                        }
                    }
                });
            }
                //否则跳转到付款页面
            else {
                location.href = "http://" + window.location.host + "/WxPay/cart.payment.aspx?productid=" + quanProductid;
            }
        }
        */

        var shopid;
        var productid;
        var quanProductid;
        //页面传参
        function getUrlParam(name) {
            var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)"); //构造一个含有目标参数的正则表达式对象
            var r = window.location.search.substr(1).match(reg);  //匹配目标参数
            if (r != null) return unescape(r[2]); return null; //返回参数值
        }

        function SetCookie(sName, sValue) {
            date = new Date();
            s = date.getDate();
            date.setDate(s + 1);
            document.cookie = sName + "=" + escape(sValue) + "; expires=" + date.toGMTString();
        }

        function setWxShareInfo(data) {
            var shareImg = data.imgroot + data.Thumb;
            var shareTitleDes = '超赞！这里的商品只用1元钱就可以获得了，快来一起参与吧！' ;
            var shareDes = "最新一期" + data.Title + "，"+data.Description;
            var link = "http://" + window.location.host + "/index.item.aspx?productid=" + data.Productid;
            WinXinShareMessage(shareTitleDes, shareDes, link, shareImg);
        }

        //获取商品详情
        function getGoodsInfo() {
            var ajaxUrl = "http://" + window.location.host + "/ilerrshop?action=detail&shopid=" + shopid +"&productid="+productid;
            $.ajax({
                type: 'get', dataType: 'json', timeout: 10000,
                url: ajaxUrl,
                success: function (data) {
                    wxdetail(data);
                    $(".joinAndGet .detail").attr('href', '/index.goodsdesc.aspx?shopid=' + shopid);
                    $(".joinAndGet .wangqi").attr('href', '/index.wangqi.aspx?productid=' + productid);
                    getBuyRecord();
                }
            })
        }

        function wxdetail(e) {
            setWxShareInfo(e);
            
            shopid = e.YId;
            productid = e.Productid;
            yunJiage = e.Yunjiage;
            priceRangeArray = e.Pricerange.split(',');
            renshuLeft = e.Shengyurenshu;
            
            quanProductid = e.QuanProductid;//直购
            var DetailSrc = e.Picarr.split(","), //幻灯片路径
                swiperContent = "";//幻灯片容器

            for (var i = 0; i < DetailSrc.length; i++) {
                if (DetailSrc[i]=="") continue;
                swiperContent += '<div class="swiper-slide" style="width:100%;">' +
                                    '<li><img src="' + e.imgroot + DetailSrc[i] + '" class="animClass"></li>' +
                                '</div>';
            }
            $(".swiper-wrapper").append(swiperContent);
            //商品详情
            GoodState(e);

            
            //如果通过点击新一期正在火爆进行中的立即参与按钮跳转过来的,则立即弹出支付框
            if (getUrlParam("jump") === "true") {
                setPaymentParms();
            }
        }


        function GoodState(e) {
            //是否买过奔商品
            var ISBoughtTxt = "您本期没有参加哦，快到最新期试试手气吧！";
            var DetailContent = ""; //商品详情容器
            var BottomBtnBox = ""; //底部按钮框状态内容
            var LuckyZhe = ""; //已揭晓中奖用户信息
            var PublishedTimeBox = ""; //正在揭晓的时间倒计时容器
            //正在进行中的状态
            $(".alityBtn").hide();
            if (e.Q_showstate == "1") {
                $(".alityBtn").show();
                DetailContent += '<div class="pDetails">' +
                    '<b>' +
                    '<i>进行中</i>';
                if (e.ismybuy == "0") {
                    ISBoughtTxt = "你还没有参加哦，赶快试试吧，万一中了呢？";
                }
                if (e.ismybuy == "1") {
                    ISBoughtTxt = "买的越多，中奖几率越大哟！";
                }
                /*
                //立即参与一元购
                BottomBtnBox +=
                    '<div id="btnBuyBox" class="pBtn" codeid="937">' +
                        '<a style="width: 60%;margin:0 auto; " onclick="setPaymentParms($(this))" codeid="' + e.YId + '" pricerange="' + e.Pricerange + '" yunjiage="' + e.Yunjiage + '" href="javascript:;" class="buyBtn">立即参与</a>' +
                    '</div>';
                //直购按钮
                */
                BottomBtnBox +=
                    '<div id="btnBuyBox" class="pBtn" codeid="937">' +
                        '<a style="width: 35%;margin-right: 5%;" onclick="setPaymentParms($(this))" codeid="' + e.YId + '" pricerange="' + e.Pricerange + '" yunjiage="' + e.Yunjiage + '" href="javascript:;" class="buyBtn">立即参与</a>' +
                        //直接购买功能移至直购商城 '<a style="width: 35%;" onclick="setQuanPaymentParms($(this))" productid="' + e.Productid + '" href="javascript:;" class="buyBtn">直接购买</a>' +
                    '</div>';
                
            } else {
                //正在揭晓中的状态
                if (e.Q_showstate == "2") {
                    $(".newqi").hide();
                    if (e.ismybuy == "0") {
                        ISBoughtTxt = "您本期没有参加哦，快到最新期试试手气吧！";
                    }
                    if (e.ismybuy == "1") {
                        ISBoughtTxt = "您本期已经购买过本商品，马上就开奖啦！";
                    }
                    DetailContent += '<div class="pDetails">' +
                        '<b>' +
                        '<i class="jiexiaoing">正在揭晓</i>';
                    PublishedTimeBox += '<div id="divAutoRTime" class="pSurplus" time="" timealt=""><p><span>正在揭晓</span>剩余时间：<time class="daojishi" >00:23:12</time></p></div>';

                    //开始倒计时

                    var ajaxUrl = "http://" + window.location.host + "/ilerrbuy?action=listwill&yid=" + e.YId;
                    $.ajax({
                        type: 'get', dataType: 'json', timeout: 10000, url: ajaxUrl, success: function (data) {
                            if (data.state == 1) {
                                return;
                            }
                            var $countDown = $(".daojishi");
                            var countDownMS = parseInt(data.data[0].timeSpan);
                            var ag = new startTimeOut($countDown, countDownMS, "itemLottery");
                            ag.countdown();
                        }
                    });

                }
                    //已揭晓的状态
                else {
                    //BottomBtnBox += '<div class="pOngoing" codeid="'+ Productid +'">新一期正在火爆进行中<div class="jxing">立即参与</div></div>';
                    BottomBtnBox += '<div id="btnBuyBox" class="pBtn" codeid="e.Productid">' +
                        '<div class="newqi">' +
                        '<span>新一期正在火爆进行中…</span><a style="width: 30%;float: right; " href="index.item.aspx?jump=true&operation=' + getUrlParam("operation") + '&productid=' + productid + '" class="buyBtn">立即参与</a>' +
                        '</div>' +
                        '</div>';
                    if (e.ismywon == "1") {
                        ISBoughtTxt = "恭喜您中奖啦，继续买下一期积攒更多运气！";
                    } else {
                        if (e.ismybuy == "1") {
                            ISBoughtTxt = "很遗憾您没有中奖，再接再厉哦！";
                        }
                    }

                    DetailContent += '<div class="pDetails pDetails-end">' +
                        '<b>' +
                        '<i class="jiexiaoed">已揭晓</i>';
                    LuckyZhe += '<section class="JXDetails">' +
                        '<div class="jiex_detail">' +
                        '<h1>幸运号码：' + e.Q_code + '<span>0000000</span><a href="/index.calResult.aspx?shopid=' + shopid + '">计算详情</a> </h1>' +
                        '<div class="infobg">';
                    if (e.ismywon == '1') {
                        LuckyZhe += '<div class="huoder"></div>';
                    }
                    LuckyZhe += '<a href="/index.userindex.aspx?uid=' + e.Q_uid + '"><img style="border-width: 0px;" src="' + e.q_img + '"></a>' +
                        '<div class="info">' +
                        '<div class="name"><a href="/index.userindex.aspx?uid=' + e.Q_uid + '" style="color: #68A0D4;">' + e.q_username + '<em></em></a></div>' +
                        '<p class="time">用户ID：' + e.Q_uid + '</p>' +
                        '<p class="time">揭晓时间：' + e.Q_end_time + '</p>' +
                        '<p class="num">本期参与：<em>' + e.q_sumquantity + '</em>人次</p>' +
                        '<a class="link-detail" href="/user.buyDetail.aspx?yid=' + e.YId + '&otherid=' + e.Q_uid + '">参与详情 &gt;</a>' +
                        '</div>' +
                        '</div>' +
                        '</div>' +
                        '</section>';
                }
            } DetailContent += '<span>' + e.Title + '</span>' +
                '</b>' +
                '<div class="dec">' + e.Description + '</div>' +
                '<div class="qihao">期号：' + e.Qishu + '</div>' +
                '<div class="Progress-bar">' +
                '<p class="u-progress">' +
                '<span class="pgbar" style="width:' + e.Canyurenshu / e.Zongrenshu * 100 + '%;">' +
                '<span class="pging"></span>' +
                '</span>' +
                '</p>' +
                '<ul class="Pro-bar-li">' +
                '<li class="P-bar02">总需人次：' + e.Zongrenshu + '</li>' +
                '<li class="P-bar03">剩余人次：<em role="shengyu"> ' + e.Shengyurenshu + '</em></li>' +
                '</ul>' +
                '</div>' +
                PublishedTimeBox +
                BottomBtnBox +
                '</div>' +
                LuckyZhe;

            if (e.ismybuy == "0") {
                DetailContent += '<div class="trybox">' +
                    '<div class="try">' + ISBoughtTxt + '</div>' +
                    '</div>';
            }

            $(".pDetails").remove();
            $(".trybox").remove();
            $(".swiper-container").after(DetailContent);
        }


        //获取本期参与记录
        var nextPage = 1;
        function getBuyRecord(me) {
            if (parseInt($("#pagenum").val()) >= nextPage) { return; }//防止重复加载
            if (nextPage == 0) {//表示加载完毕
                return;
            }
            var ajaxUrl = "http://" + window.location.host + "/ilerrshop?action=buyrecord&shopid=" + shopid + "&p=" + nextPage;
            $.ajax({
                type: 'get', dataType: 'json', timeout: 10000,
                url: ajaxUrl,
                success: function (e) {
                    Isloading = false;
                    if (e.count <= 0) {
                        $("#divLoad").text("暂无数据");
                    }
                    //wxbuyrecord(data);
                    if (e.state == "3") {
                        //$(".goodsList").html("<div style='text-align:center;padding:10px;color:#999;font-size:14px;'>暂无数据</div>");
                        //me.resetload();
                        //me.lock();
                        $("#divLoad").text("暂无数据");
                        return;
                    }
                    $(".part-tt .stime").html(e.firsttime + " (开始)");
                    $("#pagenum").val(nextPage);//当前页
                    nextPage = e.nextPage;//下一页
                    var PartList = "";
                    if (e.state == 0) {
                        PartList += '';
                        for (var i = 0; i < e.data.length ; i++) {
                            PartList += '<ul style="overflow:visible;border-top:none;margin-top:0;width:100%;border-left:1px solid #efefef;">' +
                                '<li class="rBg" style="z-index:15;padding:0;top:0px;">' +
                                    '<a href="/index.userindex.aspx?uid=' + e.data[i].uid + '"><img style="border-radius:100px;" src="' + e.imgroot + e.data[i].img + '"></a>' +
                                '</li>' +
                                '<li uid="' + e.data[i].uid + '" id="' + e.data[i].yid + '"  class="rInfo" style="padding:0;">' +
                                    '<a class="link" href="/user.buyDetail.aspx?yid=' + e.data[i].yid + '&otherid=' + e.data[i].uid + '"><i>' + e.data[i].username + '</i><br><strong>' + e.data[i].ip + '</strong><br><span><b class="orange">' + e.data[i].quantity + '</b>人次</span><em class="arial">' + e.data[i].time + '</em></a></li>' +
                            '</ul>';
                        }
                        PartList += '</div>' +
                        '</div>';
                    }
                    $(".goodsList").append(PartList);
                    if (nextPage == 0) {
                        $("#divLoad").text("加载完成");
                    }

                },
                error: function (xhr, type) {
                }
            })
        }
        function wxbuyrecord(e) {
        }
        //自动加载
        var currentPage=1;
        $(window).scroll(function () {
            if ($(window).scrollTop() >= $(document).height() - $(window).height()) {
                if (currentPage == nextPage) return;
                getBuyRecord();
                currentPage = nextPage;
            }
        });
        //开启右上角微信菜单
        document.addEventListener('WeixinJSBridgeReady', function onBridgeReady() {
            WeixinJSBridge.call('showOptionMenu');
        });
		</script>
</body>

</html>
