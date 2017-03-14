<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.userindex.aspx.cs" Inherits="RedBook.index_userindex" %>

<!DOCTYPE html>
<html>

<head>
    <meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
    <title>个人主页</title>
    <meta content="app-id=518966501" name="apple-itunes-app" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, user-scalable=no, maximum-scale=1.0" />
    <meta content="yes" name="apple-mobile-web-app-capable" />
    <meta content="black" name="apple-mobile-web-app-status-bar-style" />
    <meta content="telephone=no" name="format-detection" />
    <link href="css/comm.css?v=130715" rel="stylesheet" type="text/css" />
    <link href="css/userindex.css" rel="stylesheet" type="text/css" />
    <script src="js/jquery190.js" language="javascript" type="text/javascript"></script>
    <script id="pageJS" data="js/userindex.js" language="javascript" type="text/javascript"></script>
    <script src="/js/fastclick.js"></script>
</head>

<body id="loadingPicBlock">
    <header class="header">
        <h2>个人主页</h2>
        <a class="cefenlei" onclick="window.location.href=document.referrer;" href="javascript:;">
            <img width="30" height="30" src="images/fanhui.png">
        </a>
        <div class="head-r"><a href="/index.aspx" class="fr_Home"></a></div>
    </header>

    <section class="clearfix">

        <div class="home-head userCenter">
        </div>

        <div id="fixedBox">
            <div id="navBox" class="g-snav m_listNav">
                <div class="g-snav-lst g-huan z-sgl-crt"><a id="Canyu" class="gray9">参与记录</a><b></b></div>
                <div class="g-snav-lst g-huan"><a id="Lucky" class="gray9">幸运记录</a><b></b></div>
                <div class="g-snav-lst"><a class="gray9" href="/index.goodspost.aspx">晒单记录</a></div>
            </div>
            <div class="borderMoveBox clearfix">
                <div class="borderMove"></div>
            </div>
        </div>

        <div class="mainCon">
            <div id="divBuyRecord" class="mBuyRecord">
                <div class="ListBox">
                </div>
                <div id="divGoodsLoading" class="loading"><b></b>正在加载...</div>
            </div>
            <!--获得商品-->
            <div id="divGetGoods" class="mBuyRecord" style="display: none">
                <div class="ListBox"></div>
                <div id="WinLoadind" class="loading"><b></b>正在加载...</div>
            </div>
            <!--晒单-->
            <div id="divSingle" class="mSingle" style="display: none"></div>
        </div>


        <input id="BuyPagenum" value="0" type="hidden" /><!--参与记录已加载到第几页-->
        <input id="WonPagenum" value="0" type="hidden" /><!--中奖记录已加载到第几页-->
    </section>
</body>
<script>
    var Isloading = false;
    var IsloadingWon = false;
    //首次加载
    $(function () {
        FastClick.attach(document.body);

        $("#BuyPagenum").val("0");
        $("#WonPagenum").val("0");
        Isloading = true;
        IsloadingWon = false;
        pageInit();
        GetUserInfo();
        //跳转页面
        var gt = '.mBuyRecord ul';
        $(document).on('click', gt, function () {
            var yid = $(this).attr('id');
            if (yid) {
                window.location.href = "/index.item.aspx?shopid=" + yid;
            }
        });

    });
    //页面传参
    function getUrlParam(name) {
        var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)"); //构造一个含有目标参数的正则表达式对象
        var r = window.location.search.substr(1).match(reg);  //匹配目标参数
        if (r != null) return unescape(r[2]); return null; //返回参数值
    }

    //点击显示隐藏
    function hideShow(cur) {
        var borderMove = $(".borderMove");
        var pos = borderMove.width();
        curId = cur.attr("id");
        cur.closest("div").addClass("mCurr");
        cur.closest("div").siblings().removeClass("mCurr");
        if (curId == "Canyu") {
            if (Isloading == true) return;
            Isloading = true;
            IsloadingWon = false;
            $("#divBuyRecord").show();
            $("#divGetGoods").hide();
            $("#divGetGoods .ListBox").html("");
            GetBuyRecord();
            borderMove.animate({
                left: 0
            }, "fast");
        } else if (curId == "Lucky") {
            if (IsloadingWon == true) return;
            IsloadingWon = true;
            Isloading = false;
            $("#divBuyRecord").hide();
            $("#divBuyRecord .ListBox").html("");
            $("#divGetGoods").show();
            GetWonList();
            borderMove.animate({
                left: pos
            }, "fast");
        }
    }
    function pageInit() {
        $("#navBox .g-huan a").click(function () {
            hideShow($(this));
        });
        $("#divBuyRecord").show();
        GetBuyRecord();
        $("#divGetGoods").hide();
        if (getUrlParam("luckyListOpen") == "1") {
            hideShow($("#Lucky"));
        }
    }


    //自动加载
    $(window).scroll(function () {
        if ($(window).scrollTop() == $(document).height() - $(window).height()) {
            if ($("#divBuyRecord").is(":visible")) {
                GetBuyRecord();
            } else if ($("#divGetGoods").is(":visible")) {
                GetWonList();
            }
        }
    });
    //个人信息
    function GetUserInfo() {
        var ajaxUrl = "http://" + window.location.host + "/ilerruser?action=visituser&otherid=" + getUrlParam("uid"),
            UserInfoBox = "";
        $.ajax({
            type: 'get', dataType: 'json', timeout: 10000,
            url: ajaxUrl,
            success: function (e) {
                UserInfoBox += '<div class="user-photo-box">' +
                    '<div class="user-photo clearfix">' +
                        '<a class="z-Himg" href="javascript:;">' +
                            '<img src="' + e.imgroot + e.touxiang + '" border="0">' +
                        '</a>' +
                    '</div>' +
                    '<div class="user-name">' +
                        '<b class="z-name gray01"></b>' +
                        '<span>' + e.username + '</span>' +
                    '</div>' +
                '</div>';
                $(".userCenter").append(UserInfoBox);
            }
        });
    }

    var nextPageBuy = 1;
    var nextPageWon = 1;
    //参与记录
    function GetBuyRecord() {
        Isloading = true;
        $("#WonPagenum").val("0");//初始化中奖纪录的分页
        nextPageWon = 1;
        $("#WinLoadind").text("正在努力加载中...");
        if (nextPageBuy >= 6) {
            return;
        }
        if (parseInt($("#BuyPagenum").val()) >= nextPageBuy) { return; }//防止重复加载
        var ajaxUrl = "http://" + window.location.host + "/ilerrorder?action=otheroneselfpartake&uid=" + getUrlParam("uid") + "&p=" + nextPageBuy,
            RecordListBox = "";
        $.ajax({
            type: 'get', dataType: 'json', timeout: 10000,
            url: ajaxUrl,
            success: function (e) {
                if (e.state == 0) {
                    $("#myAvaList .nojilu").hide();
                    $("#BuyPagenum").val(nextPageBuy);//当前页
                    nextPageBuy = e.nextPage;//下一页
                    //参与记录列表
                    for (var i = 0; i < e.data.length; i++) {
                        var yungouState = e.yungouState;
                        var commontxt = '<ul id="' + e.data[i].yid + '">' +
                                '<li class="mBuyRecordL">' +
                                    '<img src="' + e.imgroot + e.data[i].thumb + '"></li>' +
                                '<li class="mBuyRecordR">' +
                                    '<div class="title">' + e.data[i].title + '</div>' +
                                    '<p class="txt">期号：' + e.data[i].qishu + '</p>' +
                                    '<p class="txt">本期参与：<b>' + e.data[i].quantity + '</b>人次</p>' +
                                '</li>';
                        if (e.data[i].yungouState == 1) { //进行中
                            RecordListBox += commontxt +
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
                                    '<a class="link-canyu" href="/index.item.aspx?jump=true&shopid=' + e.data[i].yid + '">立即参与</a>' +
                                '</li>' +
                            '</ul>';
                        }
                        else {
                            RecordListBox += commontxt +
                                '<li class="canyubox">' +
                                    '<div class="pzhongj">获得者：' + e.data[i].username + '<span class="renci"><b>' + e.data[i].quantity + '</b>人次</span></div>' +
                                    '<a class="link-canyu" href="/index.item.aspx?jump=true&productid=' + e.data[i].productid + '">立即参与</a>' +
                                '</li>' +
                             '</ul>';
                        }
                    }
                    $("#divBuyRecord .ListBox").append(RecordListBox);

                    if (nextPageBuy == 0) {
                        $("#divGoodsLoading").text("加载完成");
                    }
                    else if (nextPageBuy >= 6) {
                        $("#divGoodsLoading").show();
                        $("#divGoodsLoading").text("最多只能看用户一年内最新的50条参与记录哦~");
                    }
                } else if (e.state == 1) {//暂无数据
                    $("#divGoodsLoading").text("暂无数据");
                } else {
                    $("#divGoodsLoading").text("获取数据出错~");
                }
            }
        });
    }

    //中奖记录
    function GetWonList() {
        IsloadingWon = true;
        $("#BuyPagenum").val("0");//初始化中奖纪录的分页
        nextPageBuy = 1;
        $("#divGoodsLoading").text("正在努力加载中...");
        if (nextPageWon >= 6) {
            return;
        }
        if (parseInt($("#WonPagenum").val()) >= nextPageWon) { return; }//防止重复加载
        var ajaxUrl = "http://" + window.location.host + "/ilerrorder?action=otherfpartakewin&uid=" + getUrlParam("uid") + "&p=" + nextPageWon;
        var li = "";
        $.ajax({
            type: 'get', dataType: 'json', timeout: 10000,
            url: ajaxUrl,
            success: function (e) {
                if (e.state == 0) {
                    $("#myAvaList .nojilu").hide();
                    $("#WonPagenum").val(nextPageWon);//当前页
                    nextPageWon = e.nextPage;//下一页
                    //参与记录列表
                    for (var i = 0; i < e.data.length; i++) {
                        li += '<ul class="BuyRecordList" id="' + e.data[i].yid + '">' +
                            '<li class="mBuyRecordL"><img src="' + e.imgroot + e.data[i].thumb + '"></li>' +
                            '<li class="mBuyRecordR">' +
                                '<div class="title">' + e.data[i].title + '</div>' +
                                '<p class="txt">期号：' + e.data[i].qishu + '</p>' +
                                '<p class="txt">本期参与：<b>' + e.data[i].q_sumquantity + '</b>人次</p>' +
                                '<div class="gongxi"></div>' +
                            '</li>' +
                        '</ul>';
                    }
                    $("#divGetGoods .ListBox").append(li);
                    if (nextPageWon == 0) {
                        $("#WinLoadind").text("加载完成");
                    }
                    else if (nextPageWon >= 6) {
                        $("#WinLoadind").show();
                        $("#WinLoadind").text("最多只能看该用户一年内的最新50条参与记录哦~");
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



</script>
</html>
