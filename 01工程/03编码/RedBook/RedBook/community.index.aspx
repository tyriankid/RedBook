<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="community.index.aspx.cs" Inherits="RedBook.community_index" %>

<%@ Register Src="Controls/ucFoot.ascx" TagName="ucFoot" TagPrefix="uc1" %>
<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>
<%@ Register TagPrefix="UI" Namespace="Taotaole.Common" Assembly="Taotaole.Common" %>
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
    <link href="css/comm.css" rel="stylesheet" type="text/css" />
    <link href="css/member.css" rel="stylesheet" type="text/css" />
    <link href="css/community.css" rel="stylesheet" type="text/css" />
    <script src="js/jquery190.js" type="text/javascript"></script>
    <%--<script id="pageJS" data="js/userbuylist.js" type="text/javascript"></script>--%>
    <!-- 中奖确认js -->
    <script src="http://res.wx.qq.com/open/js/jweixin-1.0.0.js"></script>
    <script src="js/wxShare.js" type="text/javascript"></script>
    <script src="js/WinRemind.js" type="text/javascript"></script>
    <script src="js/fastclick.js"></script>
    <script src="internalapi/common.js" type="text/javascript"></script>
</head>

<body>
    <%--<header class="header">
        <h2>爽乐购</h2>
        <a class="cefenlei" onclick="window.location.href=document.referrer; " href="javascript:;">
            <img width="30" height="30" src="images/fanhui.png">
        </a>
        <div class="head-r"><a href="/user.index.aspx" class="fr_Shezhi"></a></div>
    </header>--%>

    <!--个人基本信息-->

    <div id="fixedBox">
        <div id="navBox" class="g-snav m_listNav">
            <div class="g-snav-lst z-sgl-crt" state="-1">
                <a id="bList" class="gray9">全部参与</a><b></b>
            </div>
            <div class="g-snav-lst" state="1">
                <a id="wList" class="gray9">中奖记录</a><b></b>
            </div>
            <div class="g-snav-lst" state="3">
                <a id="pList" class="gray9" href="/index.goodspost.aspx">晒单记录</a>
            </div>
        </div>
        <div class="borderMoveBox clearfix">
            <div class="borderMove"></div>
        </div>
    </div>
    <!--全部参与 Start -->
    <section id="myBuyList" class="clearfix mBuyRecord">
        <!--<ul class="clearfix commun-list">
			        </ul>-->
        <div class="canyu-listbox ListBox">
        </div>
        <div id="divGoodsLoading" class="loading"><b></b>正在加载</div>
    </section>

    <!--全部参与 End -->

    <!-- 中奖记录  Start -->
    <section id="myWonList" class="clearfix mBuyRecord zhongj-listbox" style="display: none;">
        <div class="clearfix commun-list zhongj-list">
        </div>

        <div id="WinLoadind" class="loading"><b></b>正在加载</div>
    </section>

    <!-- 中奖记录 End -->

    <!--晒单纪录 Start -->
    <div id="myPostList" class="MycSingleCon" style="display: none;">
        <div class="WxPostCon">
            <img class="sorry" src="images/post/postsorry.png" />
            <p>
                抱歉，此页面暂不支持晒单功能<br />
                完整功能请见“爽乐购”客户端
            </p>
        </div>
    </div>
    <uc1:ucFoot ID="ucFoot1" runat="server" />
    <!--晒单纪录 End -->
    <input id="BuyPagenum" value="0" type="hidden" /><!--已加载到第几页-->
    <input id="WonPagenum" value="0" type="hidden" /><!--已加载到第几页-->
    <UI:WeixinSet ID="weixin" runat="server" />
</body>
<script type="text/javascript">
    var bList = $("#myBuyList"),
        wList = $("#myWonList");
    var Isloading = false;
    var IsloadingWon = false;
    //页面传参
    var uid;
    function getUrlParam(name) {
        var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)"); //构造一个含有目标参数的正则表达式对象
        var r = window.location.search.substr(1).match(reg);  //匹配目标参数
        if (r != null) return unescape(r[2]); return null; //返回参数值
    }
    //页面加载
    $(function () {
        FastClick.attach(document.body);

        uid = getUrlParam("uid");
        $("#BuyPagenum").val("0");
        $("#WonPagenum").val("0");
        Isloading = true;
        IsloadingWon = false;
        UserInfo();
        pageInit();
        WinRemind();
    });
    //点击显示隐藏
    function hideShow(cur) {
        var borderMove = $(".borderMove");
        var pos = borderMove.width();
        curId = cur.attr("id");
        cur.closest("div").addClass("z-sgl-crt");
        cur.closest("div").siblings().removeClass("z-sgl-crt");
        if (curId == "bList") {
            if (Isloading == true) return;
            Isloading = true;
            IsloadingWon = false;
            bList.show(), wList.hide();
            wList.find(".zhongj-list").html("");
            AllBuyList();
            borderMove.animate({
                left: 0
            }, "fast");

        } else if (curId == "wList") {
            if (IsloadingWon == true) return;
            IsloadingWon = true;
            Isloading = false;
            wList.show(), bList.hide();
            bList.find(".canyu-listbox").html("");
            WonList();
            borderMove.animate({
                left: pos
            }, "fast");

        }
        //else if (curId == "pList") {
        //    pList.show(), wList.hide(), bList.hide();
        //    wList.find("ul").html("");
        //    borderMove.animate({
        //        left: pos * 2
        //    }, "fast");
        //}
    }

    function pageInit() {
        $("#navBox a").click(function () {
            hideShow($(this));
        });
        bList.show(), AllBuyList(), wList.hide();
    }
    //自动加载
    $(window).scroll(function () {
        if ($(window).scrollTop() == $(document).height() - $(window).height()) {
            if ($("#myBuyList").is(":visible")) {
                AllBuyList();
            } else if ($("#myWonList").is(":visible")) {
                WonList();
            }
        }
    });

    //全部参与记录
    var nextPageBuy = 1;
    var nextPageWon = 1;
    var pagecount = 1;//总页数
    function AllBuyList() {
        Isloading = true;
        for (var i = 0; i < timer.length; i++) {
            clearInterval(timer[i]);
        }
        $("#WonPagenum").val("0");//初始化中奖纪录的分页
        nextPageWon = 1;
        $("#WinLoadind").text("正在努力加载中...");
        if (parseInt($("#BuyPagenum").val()) >= nextPageBuy) { return; }//防止重复加载
        if (nextPageBuy == 0 && nextPageBuy>=10) {//表示加载完毕
            return;
        }
        var ajaxUrl = "http://" + window.location.host + "/ilerrorder?action=mylist&uid=" + uid + "&typeid=0&p=" + nextPageBuy;
        var li = "";
        $.ajax({
            type: 'get', dataType: 'json', timeout: 10000,
            url: ajaxUrl,
            success: function (e) {
                if (e.state == 0) {
                    //暂无数据
                    $("#myAvaList .nojilu").hide();
                    $("#BuyPagenum").val(nextPageBuy);//当前页
                    nextPageBuy = e.nextPage;//下一页
                    pagecount = e.pagecount;//总页数
                    
                    //参与记录列表
                    for (var i = 0; i < e.data.length; i++) {
                        var wangbaIcon = "";
                        if (e.data[i].recordcode) {
                            if (e.data[i].recordcode.substring(0, 1) == "W") {
                                //对于普通的日期，如2016-09-19，使用new Date(xxx)的话，只需要将2016-09-19改为2016/09/19就行，这样ios和安卓都没问题
                                //但是对于题中所说的格式，-改成/，都没用了，所以这种格式的怎么处理呢？如 2016-09-18T08:01:01.000+0000
                                var strDate = e.data[i].time.replace('T', ' ').replace(/\-/g, '/').split(' ');
                                var BuyTime = new Date(strDate[0]);//购买时间 
                                var dateStr = e.data[i].time.replace(' ', '-').replace(/\:/g, '-').replace('.', '-');
                                var dateArray = dateStr.split('-');
                                var BuyTime2 = new Date(dateArray[0], dateArray[1], dateArray[2], dateArray[3], dateArray[4], dateArray[5]);//购买时间  
                                var NowTime = new Date();
                                if ((NowTime.getTime() - BuyTime.getTime()) / 1000 / 60 / 60 / 24 <= 12) {
                                    wangbaIcon = '<span class="wangba"></span>';
                                }
                            }
                        }
                        if (e.data[i].yungouState == 1) {//进行中
                            li += '<ul id="' + e.data[i].yid + '">' +
                                    '<li class="mBuyRecordL">' +
                                        '<a class="touxianga" href="/index.item.aspx?shopid=' + e.data[i].yid + '"><img src="' + e.imgroot + e.data[i].thumb + '"></a>' +
                                    '</li>' +
                                    '<li class="mBuyRecordR">' +
                                        '<a class="tt" href="/index.item.aspx?shopid=' + e.data[i].yid + '">' +
                                            '<div class="title">' + e.data[i].title + '</div>' +
                                            '<p class="txt">期号：' + e.data[i].qishu + '</p>' +
                                            '<p class="txt">本期参与：<b>' + e.data[i].quantity + '</b>人次</p>' +
                                             wangbaIcon +
                                        '</a>' +
                                    '</li>' +
                                    '<li class="canyubox">' +
                                        '<div class="pRate">' +
                                            '<div class="Progress-bar">' +
                                                '<p class="u-progress"><span class="pgbar" style="width: ' + e.data[i].canyurenshu / e.data[i].zongrenshu * 100 + '%;"><span class="pging"></span></span>' +
                                                '</p>' +
                                                '<ul class="Pro-bar-li">' +
                                                    '<li class="P-yicanyu">已参与<br />' +
                                                        '<b>' + e.data[i].canyurenshu + '</b></li>' +
                                                    '<li class="P-zongxu">总需<br />' +
                                                        e.data[i].zongrenshu + '</li>' +
                                                    '<li class="P-shengyu">剩余<br />' +
                                                    '<b>' + e.data[i].shengyurenshu + '</b></li>' +
                                                '</ul>' +
                                            '</div>' +
                                        '</div>' +
                                        '<a class="link-canyu" href="/index.item.aspx?jump=true&type=isactivity&operation=' + e.data[i].operation + '&shopid=' + e.data[i].yid + '">再次参与</a>' +
                                    '</li>' +
                                '</ul>';
                        }
                        else if (e.data[i].yungouState == 3) {
                            if (e.data[i].yungouState == 3 && e.data[i].q_showtime == 1) {//揭晓中
                                li += '<ul id="' + e.data[i].yid + '">' +
                                    '<li class="mBuyRecordL">' +
                                        '<a class="touxianga" href="/index.item.aspx?shopid=' + e.data[i].yid + '"><img src="' + e.imgroot + e.data[i].thumb + '"></a>' +
                                    '</li>' +
                                    '<li class="mBuyRecordR">' +
                                        '<a class="tt" href="/index.item.aspx?shopid=' + e.data[i].yid + '">' +
                                            '<div class="title">' + e.data[i].title + '</div>' +
                                            '<p class="txt">期号：' + e.data[i].qishu + '</p>' +
                                            '<p class="txt">本期参与：<b>' + e.data[i].quantity + '</b>人次</p>' +
                                             wangbaIcon +
                                        '</a>' +
                                    '</li>' +
                                   '<li class="canyubox">' +
                                       '<div class="daojishibox">' +
                                           '揭晓倒计时：<time class="daojishi" name="' + e.data[i].haomiao + '" id="' + e.data[i].yid + '"></time>' +
                                       '</div>' +
                                   '</li>' +
                                '</ul>';
                            }
                            else if (e.data[i].yungouState == 3 && e.data[i].q_showtime != 1) {//已揭晓
                                li += '<ul id="' + e.data[i].yid + '">' +
                                    '<li class="mBuyRecordL">' +
                                        '<a class="touxianga" href="/index.item.aspx?operation=' + e.data[i].operation + '&shopid=' + e.data[i].yid + '"><img src="' + e.imgroot + e.data[i].thumb + '"></a>' +
                                    '</li>' +
                                    '<li class="mBuyRecordR">' +
                                        '<a class="tt" href="/index.item.aspx?shopid=' + e.data[i].yid + '">' +
                                            '<div class="title">' + e.data[i].title + '</div>' +
                                            '<p class="txt">期号：' + e.data[i].qishu + '</p>' +
                                            '<p class="txt">本期参与：<b>' + e.data[i].quantity + '</b>人次</p>' +
                                             wangbaIcon +
                                        '</a>' +
                                    '</li>' +
                                   '<li class="canyubox">' +
                                       '<div class="pzhongj">获得者：' + e.data[i].username + '<span class="renci"><b>' + e.data[i].q_sumquantity + '</b>人次</span></div>' +
                                       '<a class="link-canyu" href="/index.item.aspx?jump=true&operation=' + e.data[i].operation + '&productid=' + e.data[i].productid + '">再次参与</a>' +
                                   '</li>' +
                                '</ul>';
                            }
                        }
                    }
                    $("#myBuyList .canyu-listbox").append(li);
                    countdownLoterry();//开始倒计时,并在倒计时结束后关闭动画获取中奖人信息
                    if (nextPageBuy == 0) {
                        $("#divGoodsLoading").text("加载完成");
                    }
                } else if (e.state == 1) {//暂无数据
                    $("#divGoodsLoading").text("暂无数据");
                } else {
                    $("#divGoodsLoading").text("获取数据出错~");
                }
            },
            error: function (me) {
            }
        })
    }
    ///调用倒计时效果,common.js内倒计时结束后,更新动画状态为停止
    function countdownLoterry() {
        $(".daojishi").each(function () {
            var countDownMS = parseInt($(this).attr("name"));
            var ag = new startTimeOut($(this), countDownMS, "mylist");//测试倒计时
            ag.countdown();
        });
    }
    //我的中奖记录
    function WonList() {
        IsloadingWon = true;
        $("#BuyPagenum").val("0");//初始化中奖纪录的分页
        nextPageBuy = 1;
        $("#divGoodsLoading").text("正在努力加载中...");
        if (parseInt($("#WonPagenum").val()) >= nextPageWon) { return; }//防止重复加载
        if (nextPageWon == 0) {//表示加载完毕
            return;
        }
        var ajaxUrl = "http://" + window.location.host + "/ilerrorder?action=mywinninglist&typeid=0&p=" + nextPageWon;
        var li = "";
        $.ajax({
            type: 'get', dataType: 'json', timeout: 10000,
            url: ajaxUrl,
            success: function (e) {
                if (e.state == 0) {
                    //暂无数据
                    $("#myAvaList .nojilu").hide();
                    $("#WonPagenum").val(nextPageWon);//当前页
                    nextPageWon = e.nextPage;//下一页
                    //记录列表
                    for (var i = 0; i < e.data.length; i++) {
                        //var slideurl = "window.location=" + "'" + e.data[i].slidelink + "'";
                        var hrefUrl = 'window.location="/user.winningconfirm.aspx?shopid=' + e.data[i].yid + '&orderid=' + e.data[i].orderId + '&productid=' + e.data[i].productid + '"';
                        li += '<ul class="BuyRecordList" id="' + e.data[i].yid + '">' +
                                    '<li class="mBuyRecordL">' +
                                        '<a class="touxianga" href="/index.item.aspx?shopid=' + e.data[i].yid + '"><img src="' + e.imgroot + e.data[i].thumb + '"></a>' +
                                    '</li>' +
                                    '<li class="mBuyRecordR">' +
                                            '<div onclick=' + hrefUrl + '>' +
                                            '<div class="title"><a>' + e.data[i].title + '</a></div>' +
                                            '<p class="txt">期号：' + e.data[i].qishu + '</p>' +
                                            '<p class="txt">本期参与：<b>' + e.data[i].q_sumquantity + '</b>人次</p>' +
                                             '<span class="gongxi"></span></div>' +
                                            //'<div class="gongxi"></div>' +
                                    '</li>' +
                                 '</ul>';
                        //li += '<div class="Shaidanbox">'+
                        //	    '<a href="/index.goodspost.aspx">我要晒单<b class="fr z-arrow"></b></a>'+
                        //    '</div>' +
                        //'</li>';
                    }
                    $("#myWonList .zhongj-list").append(li);
                    if (nextPageWon == 0) {
                        $("#WinLoadind").text("加载完成");
                    }
                } else if (e.state == 1) {//暂无数据
                    $("#WinLoadind").text("暂无数据");
                } else {
                    $("#WinLoadind").text("获取数据出错~");
                }
            },
            error: function (me) {
            }
        })
    }

    //个人信息
    function UserInfo() {
        var ajaxUrl = "http://" + window.location.host + "/ilerruser?action=visituser",
            UserInfoBox = "";
        $.ajax({
            type: 'get', dataType: 'json', timeout: 10000,
            url: ajaxUrl,
            success: function (e) {
                console.log(e);
                UserInfoBox += '<div class="user-photo-box">' +
                    '<div class="user-photo clearfix">' +
                        '<a href="javascript:;">' +
                            '<img src="' + e.imgroot + e.touxiang + '" border="0">' +
                        '</a>' +
                        '<b>' + e.username + '</b>' +
                        '<dd class="chongzhi">' +
                            '<a href="/user.recharge.aspx">我要充值</a>' +
                        '</dd>' +
                    '</div>' +
                    '<dl class="user-caozuo clearfix">' +
                        '<dd>' +
                            '<a href="/user.userbalance.aspx"><b>' + parseInt(e.money) + '</b>' +
                                '<p>爽乐币</p>' +
                            '</a>' +
                        '</dd>' +

                         '<dd>' +
                            '<a href="/gift.index.aspx#/Record"><b>' + parseInt(e.score) + '</b>' +
                                '<p>积分</p>' +
                            '</a>' +
                        '</dd>' +
                        '<dd>' +
                            '<a href="/redpack.index.aspx"><b><span id="spanredcount"></span>&nbsp;个</b>' +
                                '<p>淘红包</p>' +
                            '</a>' +
                        '</dd>' +
                    '</dl>' +
                '</div>';
                $("#fixedBox").before(UserInfoBox);

                //红包个数
                ajaxUrl = "http://" + window.location.host + "/ilerrredpack?action=redpackcount";
                $.ajax({
                    type: 'get', dataType: 'json', timeout: 10000,
                    url: ajaxUrl,
                    success: function (e) {
                        $("#spanredcount").html(parseInt(e.count));
                    }
                });
            }
        });
    }

    //开启右上角微信菜单
    document.addEventListener('WeixinJSBridgeReady', function onBridgeReady() {
        WeixinJSBridge.call('showOptionMenu');
    });
</script>
</html>
