<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="user.winningconfirm.aspx.cs" Inherits="RedBook.user_WinningInformation" %>

<!DOCTYPE html>
<html>
<head>
    <meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
    <title>中奖确认</title>
    <meta content="app-id=518966501" name="apple-itunes-app" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, user-scalable=no, maximum-scale=1.0" />
    <meta content="yes" name="apple-mobile-web-app-capable" />
    <meta content="black" name="apple-mobile-web-app-status-bar-style" />
    <meta content="telephone=no" name="format-detection" />
    <link href="css/comm.css?v=130715" rel="stylesheet" type="text/css" />
    <link href="css/winning.css" rel="stylesheet" type="text/css" />
    <script src="js/jquery190.js" language="javascript" type="text/javascript"></script>
    <script src="/js/fastclick.js"></script>

</head>
<body>
    <header class="header">
        <h2>中奖确认</h2>
        <a class="cefenlei" href="/community.index.aspx">
            <img width="30" height="30" src="images/fanhui.png">
        </a>
        <div class="head-r"><a href="/index.aspx" class="fr_Home"></a></div>
    </header>

    <section class="genzong-box">
        <div class="gztt">奖品跟踪</div>
        <ul class="genzong-listbox">
        </ul>
    </section>
    <section style="height: 45px;"></section>

    <section class="fixbox">
        <div class="fixnr"><span>手气不错，要乘胜追击哦！</span><a class="link-canyu" href="">继续参与</a></div>
    </section>

    <input type="hidden" id="kaka" value=" " />
    <script type="text/javascript">
        var code;
        var windowWidth = document.documentElement.clientWidth;
        var windowHeight = document.documentElement.clientHeight;
        //显示div
        function showDiv(div1, div2) {
            $(div1).show();
            $(div2).show();
        }
        //隐藏div
        function hideDiv(div1, div2) {
            $(div1).hide();
            $(div2).hide();
        }
        //页面加载
        $(function () {
            FastClick.attach(document.body);

            CanyuAgain();
            GetGoodsInfo();
            GoodsTrack1();
        });
        //页面传参
        function getUrlParam(name) {
            var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)"); //构造一个含有目标参数的正则表达式对象
            var r = window.location.search.substr(1).match(reg);  //匹配目标参数
            if (r != null) return unescape(r[2]); return null; //返回参数值
        }

        var isMobile = /^(((13[0-9]{1})|(15[0-9]{1})|(18[0-9]{1})|(17[0-9]{1})|(14[0-9]{1}))+\d{8})$/;
        var isMailbox = /^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/;
        var isQq = /^[1-9][0-9]{4,}$/;

        var shopid = getUrlParam("shopid");
        var orderid = getUrlParam("orderid");
        var productid = getUrlParam("productid");

        //加载奖品信息及状态
        function GetGoodsInfo() {
            var ajaxUrl = "http://" + window.location.host + "/ilerrshareorder?action=productaffirm&orderid=" + orderid + "&shopid=" + shopid;
            //alert(ajaxUrl);
            var goodsinfo = "",
                status = "";
            $.ajax({
                type: 'get', dataType: 'json', timeout: 10000, url: ajaxUrl,
                error: function (err) {
                    alert("亲，您目前的网络好像不太稳定哦~！");
                    window.location = "/community.index.aspx";
                }, success: function (e) {
                    if (e.state != 0) {
                        switch (e.state) {
                            case "2":
                                alert("未知的签名参数！");
                                break;
                            case "3":
                                alert("订单ID错误！");
                                break;
                            case "4":
                                alert("获取订单信息失败！");
                                break;
                        }
                        window.location = history.go(-1);
                        return;
                    }
                    status = e.status;
                    $("#kaka").val(e.info);
                    //alert(e.info);
                    $(".mobile").val(e.mobile);
                    $(".mobile").attr("id", e.mobile);
                    goodsinfo += '<section class=" hjgoods-box">' +
                        '<div class=" hjgoods-list">' +
                            '<a href="" class="picbox">' +
                                '<img class="goods-pic" src="' + e.imgroot + e.thumb + '" alt="">' +
                            '</a>' +
                            '<div class="info">' +
                                '<a href="javascript:;" class="tt">' + e.title + '</a>' +
                                '<p>本次参与：<b>' + e.quantity + '</b>人次</p>' +
                            '</div>' +
                            '<span href="javascript:;" class="gongxi"></span>' +
                        '</div>' +
                        '<div class="zhuangt">奖品状态：<b>' + status + '</b></div>' +
                    '</section>';
                    $(".header").after(goodsinfo);
                    code = e.code;
                }
            });
        }

        //奖品跟踪信息 
        function GoodsTrack1() {
            var ajaxUrl = "http://" + window.location.host + "/ilerrshareorder?action=productaffirm&orderid=" + orderid + "&shopid=" + shopid;
            $.ajax({
                type: 'get', dataType: 'json', timeout: 10000, url: ajaxUrl, success: function (e) {
                    GoodsTrackEx(e);
                }
            });
        }

        //奖品跟踪信息_结果处理
        function GoodsTrackEx(e) {
            var GoodsTrackli = GetStep0(e);
            switch (e.status) {
                case "待确认":
                    switch (parseInt(e.typeid))//奖品类型（0实体，1话费，2游戏，3Q币）
                    {
                        case 0:
                            if (e.isdefault == "Y") {
                                GoodsTrackli += GetMorenAddr();
                            }
                            else {
                                GoodsTrackli += WuMorenAddr();
                            }
                            break;
                        case 1:
                            GetZhifubaoPopup(e);
                            GetHuafeiPopup(e);
                            GoodsTrackli += GetHuafeiStep1(e);
                            break;
                        case 2:
                            if (e.isdefault == "Y") {
                                GoodsTrackli += GetGameMorenAddr();
                            }
                            else {
                                GoodsTrackli += WuMorenGameAddr();
                            }
                            break;
                        case 3:
                            GoodsTrackli += GetQbiStep1(e);
                            break;
                    }
                    break;
                case "待发货":
                    switch (parseInt(e.typeid))//奖品类型（0实体，1话费，2游戏，3Q币）
                    {
                        case 0:
                            GoodsTrackli += GetShouhuoStep1(e) + GetShouhuoStep2();
                            break;
                        case 2:
                            GoodsTrackli += GetGameStep1(e) + GetGameStep2();
                            break;
                    }
                    break;
                case "已发货":
                    switch (parseInt(e.typeid))//奖品类型（0实体，1话费，2游戏，3Q币）
                    {
                        case 0:
                            GoodsTrackli += GetShouhuoStep1(e) + GetShouhuoStep3(e);
                            break;
                        case 2:
                            GoodsTrackli += GetGameStep1(e) + GetGameStep3(e);
                            break;
                    }
                    break;
                case "已收货":
                    switch (parseInt(e.typeid))//奖品类型（0实体，1话费，2游戏，3Q币）
                    {
                        case 0:
                            GoodsTrackli += GetShouhuoStep1(e) + GetShouhuoStep4(e);
                            break;
                        case 1:
                            GoodsTrackli += GetHuafeiStep2(e);
                            break;
                        case 2:
                            GoodsTrackli += GetGameStep1(e) + GetGameStep4(e);
                            break;
                        case 3:
                            GoodsTrackli += GetQbiStep2(e);
                            break;
                    }
                    break;
            }
            $(".genzong-listbox").append(GoodsTrackli);
            BindEvent(e);//为刚填充的界面元素绑定事件
        }

        //绑定事件
        function BindEvent(e) {

            //充值到话费
            $("#link-huafei").on("click", function () {
                $('html,body').animate({
                    scrollTop: 0
                }, 0)
                showDiv("#HuafeiPopBg", "#HuafeiPop");
                //var popupHeight = $("#HuafeiPop").height();
                //var popupWidth = $("#HuafeiPop").width();
                //$("#HuafeiPop").css({
                //    "top": (windowHeight - popupHeight) / 2,
                //    "left": (windowWidth - popupWidth) / 2
                //});
                //取消选择
                $("#HuafeiPop .Cancelbtn").click(function () {
                    hideDiv("#HuafeiPopBg", "#HuafeiPop");
                });
                //确认选择
                $("#HuafeiPop .Surebtn").click(function () {
                    if ($("#HuafeiPop .mobile").val().trim() == "") {
                        alert("请输入号码！");
                        return false;
                    } else if (!isMobile.test($("#HuafeiPop .mobile").val())) {
                        alert("您输入的号码有误！");
                        return false;
                    }
                        //else if ($("#HuafeiPop .mobagain").val().trim() == "") {
                        //    alert("请重复输入号码！");
                        //    return false;
                        //} else if ($("#HuafeiPop .mobagain").val() != $("#HuafeiPop .mobile").val()) {
                        //    alert("两次输入的号码不一致！");
                        //    return false;
                        //}
                    else if ($("#HuafeiPop .mobagain").val() == $("#HuafeiPop .mobile").val() || isMobile.test($("#HuafeiPop .mobile").val())) {
                        usenum = $("#HuafeiPop .mobile").val();
                        SubmitData("&kahao=" + e.kahao + "&kami=" + e.kami + "&usetype=1&usenum=" + usenum);
                        //alert("&kahao=" + e.kahao + "&kami=" + e.kami + "&usetype=" + way + "&usenum=" + usenum);
                        window.location.reload();
                    }
                });
            });

            //充值到支付宝
            $("#link-zhifbao").on("click", function () {
                $('html,body').animate({
                    scrollTop: 0
                }, 0)
                showDiv("#zhifubaoPopBg", "#zhifubaoPop");
                //var popupHeight = $("#zhifubaoPop").height();
                //var popupWidth = $("#zhifubaoPop").width();
                //$("#zhifubaoPop").css({
                //    "top": (windowHeight - popupHeight) / 2,
                //    "left": (windowWidth - popupWidth) / 2
                //});
                //取消选择
                $("#zhifubaoPop .Cancelbtn").click(function () {
                    hideDiv("#zhifubaoPopBg", "#zhifubaoPop");
                });
                //确认选择
                $("#zhifubaoPop .Surebtn").click(function () {
                    if ($("#zhifubaoPop .mobile").val().trim() == "") {
                        alert("请输入账号！");
                        return false;
                    }
                    else if (!isMobile.test($("#zhifubaoPop .mobile").val()) && !isMailbox.test($("#zhifubaoPop .mobile").val())) {
                        alert("您输入的账号有误！");
                        return false;
                    }
                    else
                        if ($("#zhifubaoPop .usename").val().trim() == "") {
                            alert("请输入真实姓名！");
                            return false;
                        }
                            //else if ($("#zhifubaoPop .mobagain").val().trim() == "") {
                            //    alert("请重复输入账号！");
                            //    return false;
                            //} else if ($("#zhifubaoPop .mobagain").val() != $("#zhifubaoPop .mobile").val()) {
                            //    alert("两次输入的账号不一致！");
                            //    return false;
                            //}
                        else {
                            usenum = $("#zhifubaoPop .mobile").val();
                            var usename = $("#zhifubaoPop .usename").val();
                            SubmitData("kahao=" + e.kahao + "&kami=" + e.kami + "&usetype=2&usenum=" + usenum + "&usename=" + usename + "&code="+code);
                            window.location.reload();
                        }
                });
            });

            //充值到爽乐购
            $("#link-taolebi").on("click", function () {
                showDiv("#TaolebiPopBg", "#TaolebiPop");
                var popupHeight = $("#TaolebiPop").height();
                var popupWidth = $("#TaolebiPop").width();
                $("#TaolebiPop").css({
                    "top": (windowHeight - popupHeight) / 2,
                    "left": (windowWidth - popupWidth) / 2
                });
                //取消选择
                $("#TaolebiPop .Cancelbtn").click(function () {
                    hideDiv("#TaolebiPopBg", "#TaolebiPop");
                });
                //确认选择
                $("#TaolebiPop .Surebtn").click(function () {
                    SubmitData("&kahao=" + e.kahao + "&kami=" + e.kami + "&usetype=3&usenum=爽乐币&code=" + code);
                    //SubmitData("&kahao=" + e.kahao + "&kami=" + e.kami + "&usetype=3");
                    window.location.reload();
                });
            });

            //充值QQ币
            $("#shiyQQbi").on("click", function () {
                showDiv("#QqPopBg", "#QqPop");
                var popupHeight = $("#QqPop").height();
                var popupWidth = $("#QqPop").width();
                $("#QqPop").css({
                    "top": (windowHeight - popupHeight) / 2,
                    "left": (windowWidth - popupWidth) / 2
                });
                //取消选择
                $("#QqPop .Cancelbtn").click(function () {
                    hideDiv("#QqPopBg", "#QqPop");
                });
                //确认选择
                $("#QqPop .Surebtn").click(function () {
                    if ($("#QqPop .mobile").val().trim() == "") {
                        alert("请输入QQ号！");
                        return false;
                    } else if (!isQq.test($("#QqPop .mobile").val())) {
                        alert("您输入的QQ号有误！");
                        return false;
                    } else if ($("#QqPop .mobagain").val().trim() == "") {
                        alert("请重复输入QQ号！");
                        return false;
                    } else if ($("#QqPop .mobagain").val() != $("#QqPop .mobile").val()) {
                        alert("两次输入的QQ号不一致！");
                        return false;
                    } else if ($("#QqPop .mobagain").val() == $("#QqPop .mobile").val() || isMobile.test($("#QqPop .mobile").val())) {
                        usenum = $("#QqPop .mobile").val();
                        SubmitData("&kahao=" + e.kahao + "&kami=" + e.kami + "&usetype=4&usenum=" + usenum + "&code=" + code);
                        window.location.reload();
                    }
                });
            });

            //确认收货
            $("#QuerenShouh").on("click", function () {
                showDiv("#ShouhuoPopBg", "#ShouhuoPop");
                var popupHeight = $("#ShouhuoPop").height();
                var popupWidth = $("#ShouhuoPop").width();
                $("#ShouhuoPop").css({
                    "top": (windowHeight - popupHeight) / 2,
                    "left": (windowWidth - popupWidth) / 2
                });
                //取消选择
                $("#ShouhuoPop .Cancelbtn").click(function () {
                    hideDiv("#ShouhuoPopBg", "#ShouhuoPop");
                });
                //确认选择
                $("#ShouhuoPop .Surebtn").click(function () {
                    SubmitShouhuoData();
                    window.location.reload();
                });
            });

            //确定使用默认地址
            $("#shiydizhi").on("click", function () {
                showDiv("#DizhiPopBg", "#DizhiPop");
                var popupHeight = $("#DizhiPop").height();
                var popupWidth = $("#DizhiPop").width();
                $("#DizhiPop").css({
                    "top": (windowHeight - popupHeight) / 2,
                    "left": (windowWidth - popupWidth) / 2
                });
                //取消选择
                $("#DizhiPop .Cancelbtn").click(function () {
                    hideDiv("#DizhiPopBg", "#DizhiPop");
                });
                //确认选择
                $("#DizhiPop .Surebtn").click(function () {
                    SubmitData("&mobile=" + shouhuoMobile + "&info=" + shouhuoRenDiz);
                    window.location.reload();
                });
            });
        }

        //提交数据_卡密使用信息，Q币信息，收货地址，游戏收货地址
        function SubmitData(params) {
            var ajaxUrl = "http://" + window.location.host + "/ilerrshareorder?action=affirminfo&orderid=" + orderid;
            $.ajax({
                cache: true,
                type: "POST",
                url: ajaxUrl,
                data: params,
                async: false,
                error: function (e) {
                },
                success: function (data) {
                    if (data.state == 0 || data.state == 10) {
                        alert("提交成功！");
                    }
                    else if (data.state == 102) {
                        alert("本平台已接入天网安全系统，已记录IP，严禁任何非正常登入");
                        window.open("http://www.cyberpolice.cn/wfjb/html/flfg/20120314/635.shtml");
                    }

                    else {
                        alert("提交失败,错误码是" + data.state + "！");
                    }
                }
            });
        }

        //提交数据_确认收货接口
        function SubmitShouhuoData() {
            var ajaxUrl = "http://" + window.location.host + "/ilerrshareorder?action=confirmshouhuo&orderid=" + orderid;
            $.ajax({
                cache: true,
                type: "POST",
                url: ajaxUrl,
                data: "",
                async: false,
                error: function (e) {
                },
                success: function (data) {
                    if (data.state == 0) {
                        alert("确认收货成功！");
                    } else {
                        alert("确认收货失败,错误码是" + data.state + "！");
                    }
                }
            });
        }

        //获取中奖信息
        function GetStep0(e) {
            return '<li style="display:block;">' +
                '<h1>恭喜您获得该奖品<time>' + e.q_end_time + '</time></h1>' +
            '</li>';
        }

        //获取确认话费信息_1
        function GetHuafeiStep1(e) {
            var btnlist = '<a id="link-huafei" href="javascript:;">手机充值</a>' +
                        '<a id="link-taolebi" href="javascript:;">兑爽乐币</a>';
            if (parseInt(e.money) > 20) {
                btnlist = '<a id="link-huafei" href="javascript:;">手机充值</a>' +
                        '<a id="link-zhifbao" href="javascript:;">话费转让</a>' +
                        '<a id="link-taolebi" href="javascript:;">兑爽乐币</a>';
            }
            return '<li id="yipaifa">' +
                '<h1>卡密已派发<time></time></h1>' +
                '<p>派发时间' + e.q_end_time + '</p>' +
                '<p>兑换信息' +
                //'<a class="arrtxt blue" href="/user.chongzhiInfo.aspx">卡密使用说明<i class="z-arrow"></i></a>' +
                '</p>' +
                '<div class="kuang">' +
                    //'<div class="knr">卡号：' + e.kahao + '</div>' +
                    //'<div class="knr">密码：' + e.kami + '</div>' +
                    '<div class="knr knr2">选择兑换方式</div>' +
                    '<div class="knr" id="link-selway">' +
                        '<span class="linkbox">' +
                           btnlist +
                        '</span>' +
                    '</div>' +
                '</div>' +
            '</li>';
        }

        //获取确认话费信息_2
        function GetHuafeiStep2(e) {
            var usetype = "";
            var usetypetxt = "";
            switch (e.usetype) {
                case "1": usetype = "话费";
                    usetypetxt = '<div class="knr">已充值到：' + usetype + '&nbsp;&nbsp;' + e.usebeizhu + '</div>';
                    break;
                case "2": usetype = "支付宝";
                    usetypetxt = '<div class="knr">已充值到：' + usetype + '&nbsp;&nbsp;' + e.usebeizhu + '</div>';
                    break;
                case "3": usetype = "爽乐币";
                    usetypetxt = '<div class="knr">已充值到：' + usetype + '</div>';
                    break;
                default:
                    usetypetxt = '<div class="knr">已充值到：' + usetype + '&nbsp;&nbsp;' + e.usebeizhu + '</div>';
                    break;
            }
            var GetHuafeili = '<li id="yipaifa">' +
                '<h1>卡密已派发<time></time></h1>' +
                '<p>派发时间' + e.q_end_time + '</p>' +
                '<p>兑换信息' +
               // '<a class="arrtxt blue" href="/user.chongzhiInfo.aspx">卡密使用说明<i class="z-arrow"></i></a>' +
                '</p>' +
                //'<h1>卡密已派发<time>' + e.q_end_time + '</time></h1>' +
                //'<p>卡密信息，长按可复制' +
                //'<a class="arrtxt blue" href="/user.chongzhiInfo.aspx">卡密使用说明<i class="z-arrow"></i></a>' +
                //'</p>' +
                '<div class="kuang">' +
                    '<div class="knr">卡号：' + e.kahao.replace(kahaoTxt.substring(6, 10), "****") + '</div>' +
                    '<div class="knr">密码：' + e.kami.replace(kahaoTxt.substring(6, 10), "****") + '</div>' +
                '</div>' +
            '</li>' +
            '<li id="ShiyongWay">' +
                '<h1>卡密已使用<time></time></h1>' +
                '<p>使用时间' + e.usetime + '</p>' +
                '<div class="kuang">' + usetypetxt +
                '</div>' +
            '</li>';

            return GetHuafeili;
        }

        //充值到支付宝弹出层信息
        function GetZhifubaoPopup(e) {
            var money = parseInt(e.money);
            var html = '<div class="iputxtbox">' +
                            '<li class="mianzhi">面值<span>' + money + '元(到账' + money * 0.95 + '元 | 服务费' + money * 0.05 + '元)</span></li>' +
                        '</div>';
            var kaka = $('#kaka').val();
            //alert(kaka);
            var kakasplit = kaka.split('★');
            for (var i = 0; i < kakasplit.length - 1; i++) {
                var kahaoTxt = kakasplit[i].split('☆')[0];
                var kamiTxt = kakasplit[i].split('☆')[1];
                kahaoTxt = kahaoTxt.replace(kahaoTxt.substring(6, 10), '****');
                kamiTxt = kahaoTxt.replace(kamiTxt.substring(6, 10), '****');
                html += '<div class="iputxtbox">' +
                            '<li>卡号<span>' + kahaoTxt + '</span></li>' +
                            '<li>卡密<span>' + kamiTxt + '</span></li>' +
                        '</div>';
            }
            $("#zhifubaoPop .mobilebox").after(html);
        }

        //充值到话费弹出层信息
        function GetHuafeiPopup(e) {
            var money = parseInt(e.money);
            $(".MianZhiList li").each(function () {
                if ($(this).attr("money") == money) {
                    $(this).addClass("cur");
                }
            });
        }

        //获取确认Q币信息_1
        function GetQbiStep1(e) {
            return '<li id="yipaifa">' +
                '<h1>卡密已派发<time></time></h1>' +
                '<p>派发时间' + e.q_end_time + '</p>' +
                '<p>卡密信息，长按可复制' +
               // '<a class="arrtxt blue" href="/user.chongzhiInfo.aspx">卡密使用说明<i class="z-arrow"></i></a>' +
                '</p>' +
                '<div class="kuang">' +
                    '<div class="knr">卡号：' + e.kahao + '</div>' +
                    '<div class="knr">密码：' + e.kami + '</div>' +
                    '<div class="btn-style" id="shiyQQbi">立即使用</div>' +
                '</div>' +
            '</li>';
        }

        //获取确认Q币信息_2
        function GetQbiStep2(e) {
            return '<li id="yipaifa">' +
                '<h1>卡密已派发<time></time></h1>' +
                '<p>派发时间：' +
                e.q_end_time +
                '</p>' +
                '<p>卡密信息，长按可复制' +
                //'<a class="arrtxt blue" href="/user.chongzhiInfo.aspx">卡密使用说明<i class="z-arrow"></i></a>' +
                '</p>' +
                '<div class="kuang">' +
                    '<div class="knr">卡号：' + e.kahao + '</div>' +
                    '<div class="knr">密码：' + e.kami + '</div>' +
                '</div>' +
            '</li>' +
            '<li id="ShiyongWay">' +
                '<h1>卡密已使用<time></time></h1>' +
                '<div class="kuang">' +
                    '<div class="knr">已充值到：Q币</div>' +
                    '<div class="knr">充值备注：' + e.usebeizhu + '</div>' +
                '</div>' +
            '</li>';
        }

        var shouhuoMobile = "",
            shouhuoRenDiz = "";
        //实物_1_待确认_读取默认收货地址
        function GetMorenAddr() {
            var li = "";
            //$.ajaxSetup({ async: false });
            var ShouhuoUrl = "http://" + window.location.host + "/ilerruser?action=useraddresslist";
            $.ajax({
                type: 'get', dataType: 'json', timeout: 10000, async: false,//局部同步
                url: ShouhuoUrl,
                success: function (addr) {
                    if (addr.default == "Y") {//有默认收货地址
                        for (var i = 0; i < addr.data.length; i++) {
                            if (addr.data[i].isdefault == "Y") {
                                li = '<li>' +
                                    '<h1>请确认收货地址</h1>' +
                                    '<a id="AddrKuang" class="kuang" href="/user.address.aspx?orderid=' + orderid + '&shopid=' + shopid + '&productid=' + productid + '">' +
                                        '<div class="knr knrbigfont">' + addr.data[i].shouhuoren + '&nbsp;&nbsp;' + addr.data[i].mobile + '<span class="rtxt blue knrsmallfont">编辑<i class="z-arrow"></i></span></div>' +
                                        '<div class="knr">' + addr.data[i].sheng + addr.data[i].shi + addr.data[i].xian + addr.data[i].jiedao + '</div>' +
                                    '</a>' +
                                    '<div class="btn-style" id="shiydizhi">确定使用该地址</div>' +
                                '</li>';
                                shouhuoMobile = addr.data[i].mobile;
                                shouhuoRenDiz = addr.data[i].sheng + addr.data[i].shi + addr.data[i].xian + addr.data[i].jiedao + "*" + addr.data[i].shouhuoren;
                            }
                        }
                    }
                }
            });
            //$.ajaxSetup({ async: true });
            return li;
        }

        //实物_1_待确认_无默认地址
        function WuMorenAddr() {
            return '<li>' +
                    '<h1>请确认收货地址</h1>' +
                    '<p>您还没有默认的收货地址</p>' +
                    '<a class="btn-style" id="selectAddr" href="/user.address.aspx?orderid=' + orderid + '&shopid=' + shopid + '&productid=' + productid + '">新增收货地址</a>' +
                '</li>'
        }

        //实物_1_已确认_已选择收货地址
        function GetShouhuoStep1(e) {
            var info = e.info.split('*');
            var dizhi = info[0];
            var name = info[1];
            return '<li>'
                + '<h1>已确认收货地址</h1>' +
                '<div class="kuang">' +
            '<div class="knr knrbigfont">' + name + '&nbsp&nbsp' + e.phone + '</div>' +
            '<div class="knr">' + dizhi + '</div>' +
        '</div>' +
        '</li>';
        }

        //实物_2_待发货
        function GetShouhuoStep2() {
            return '<li>' +
                '<h1>待发货</h1>' +
                '<p>您的奖品正在出库中，请耐心等待...</p>' +
            '</li>';
        }

        //实物_3_已发货
        function GetShouhuoStep3(e) {
            return '<li>' +
                '<h1>已发货<time>' + e.sendtime + '</time></h1>' + '<div class="kuang">' +
                '<div class="knr">快递公司：' + e.kuaidigongsi + '</div>' +
                '<div class="knr">快递单号：' + e.kuaidihao + '</div>' +
                //'<div class="knr"><h2>e.q_end_time</h2><time>' + e.q_end_time + '</time></div>' +
                '<div class="btn-style" id="QuerenShouh">确认收货</div>' +
                '<div class="btn-style btn-style2 lianxikefu" id="">联系客服</div>' +
            '</li>';
            SubmitShouhuo();
        }

        //实物_4_已收货
        function GetShouhuoStep4(e) {
            return '<li>' +
                '<h1>已发货<time>' + e.sendtime + '</time></h1>' + '<div class="kuang">' +
                '<div class="knr">快递公司：' + e.kuaidigongsi + '</div>' +
                '<div class="knr">快递单号：' + e.kuaidihao + '</div>' +
            '</li>' +
            '<li>' +
                '<h1>已确认收货</h1>' + '<div class="kuang">' +
                '<div class="btn-style btn-style2" id="">联系客服</div>' +
            '</li>' +
            '<li>' +
                '<h1>您的奖品已签收<time>' + e.q_end_time + '</time></h1>' +
                '<p>祝您再中大奖~</p>' +
            '</li>';
        }

        //游戏地址_1_待确认_读取默认游戏地址
        function GetGameMorenAddr() {
            var li = "";
            $.ajaxSetup({ async: false });
            var ShouhuoUrl = "http://" + window.location.host + "/ilerruser?action=usergameaddresslist";
            $.ajax({
                type: 'get', dataType: 'json', timeout: 10000,
                url: ShouhuoUrl,
                success: function (addr) {
                    if (addr.default == "Y") {//有默认收货地址
                        for (var i = 0; i < addr.data.length; i++) {
                            if (addr.data[i].isdefault == "Y") {
                                shouhuoMobile = addr.data[i].mobile;
                                shouhuoRenDiz = addr.data[i].gamename + addr.data[i].gamearea + addr.data[i].gameserver + '@' + addr.data[i].gameusercode + '*' + addr.data[i].shouhuoren;
                                li = '<li>' +
                                    '<h1>请确认游戏地址</h1>' +
                                    '<a id="AddrKuang" class="kuang" href="/user.gameaddress.aspx?orderid=' + orderid + '&shopid=' + shopid + '&productid=' + productid + '">' +
                                        '<div class="knr knrbigfont">' + addr.data[i].shouhuoren + '&nbsp;&nbsp;' + addr.data[i].mobile + '<span class="rtxt blue knrsmallfont">编辑<i class="z-arrow"></i></span></div>' +
                                        '<div class="knr">' + addr.data[i].gameusercode + '</div>' +
                                        '<div class="knr">' + addr.data[i].gamename + addr.data[i].gamearea + addr.data[i].gameserver + '</div>' +
                                    '</a>' +
                                    '<div class="btn-style" id="shiydizhi">确定使用该地址</div>' +
                                '</li>';
                            }
                        }
                    }
                }
            });
            $.ajaxSetup({ async: true });
            return li;
        }

        //游戏地址_1_待确认_无默认地址
        function WuMorenGameAddr() {
            return '<li>' +
                    '<h1>请确认游戏地址</h1>' +
                    '<p>您还没有默认的游戏地址</p>' +
                    '<a class="btn-style" id="selectAddr" href="/user.gameaddress.aspx?orderid=' + orderid + '&shopid=' + shopid + '&productid=' + productid + '">去新增游戏地址</a>' +
                '</li>'
        }

        //游戏地址_1_已确认_已选择游戏地址
        function GetGameStep1(e) {
            var info = e.info.split('*');
            var name = info[1];
            var dizhicode = info[0].split('@');
            var dizhi = dizhicode[0];
            var code = dizhicode[1];
            return '<li>'
                + '<h1>已确认收货地址</h1>' +
                '<div class="kuang">' +
            '<div class="knr knrbigfont">' + e.phone + '</div>' +
            '<div class="knr">游戏账号：' + code + '</div>' +
            '<div class="knr">' + dizhi + '</div>' +
        '</div>' +
        '</li>';
        }

        //游戏地址_2_待发货
        function GetGameStep2() {
            return '<li>' +
                '<h1>待发货</h1>' +
                '<p>您的奖品正在出库中，请耐心等待...</p>' +
            '</li>';
        }

        //游戏地址_3_已发货
        function GetGameStep3(e) {
            return '<li>' +
                '<h1>已发货<time>' + e.sendtime + '</time></h1>' + '<div class="kuang">' +
                '<div class="btn-style" id="QuerenShouh">确认收货</div>' +
                '<div class="btn-style btn-style2 lianxikefu" id="">联系客服</div>' +
            '</li>';
            SubmitShouhuo();
        }

        //游戏地址_4_已收货
        function GetGameStep4(e) {
            return '<li>' +
                '<h1>已发货<time>' + e.sendtime + '</time></h1>' +
            '</li>' +
            '<li>' +
                '<h1>已确认收货</h1>' + '<div class="kuang">' +
                '<div class="btn-style btn-style2" id="">联系客服</div>' +
            '</li>' +
            '<li>' +
                '<h1>您的物品已签收<time>' + e.q_end_time + '</time></h1>' +
                '<p>祝您再中大奖~</p>' +
            '</li>';
        }

        //继续参与
        function CanyuAgain() {
            var ajaxUrl = "http://" + window.location.host + "/ilerrshop?action=detail&productid=" + productid;
            $.ajax({
                type: 'get', dataType: 'json', timeout: 10000,
                url: ajaxUrl,
                success: function (e) {
                    $(".fixbox .link-canyu").attr("href", "/index.item.aspx?operation=" + e.Operation + "&productid=" + productid);
                }
            })
        }

    </script>
    <!-- 充值到话费 S  -->
    <div class="IsSelway-popup clearfix" id="HuafeiPopBg"></div>
    <div id="HuafeiPop">
        <header class="Winheader">
            <h2>手机充值</h2>
            <a class="Cancelbtn" href="javascript:;">
                <img width="30" height="30" src="images/fanhui.png">
            </a>
        </header>
        <div class="fixttbox">
            <ul class="fixtt">
                <li class="cur">充话费</li>
                <li>充流量</li>
            </ul>
        </div>
        <div class="mobilebox">
            <input type="text" class="mobile" placeholder="请输入手机号码" />
            <p class="tips">请仔细确认手机号码无误</p>
        </div>
        <ul class="MianZhiList">
            <li money="10"><i>10元</i><span>手机话费直充</span></li>
            <li money="20"><i>20元话费</i><span>手机话费直充</span></li>
            <li money="30"><i>30元话费</i><span>手机话费直充</span></li>
            <li money="50"><i>50元话费</i><span>手机话费直充</span></li>
            <li money="100"><i>100元话费</i><span>手机话费直充</span></li>
            <li money="200"><i>200元话费</i><span>手机话费直充</span></li>
            <li money="300"><i>300元话费</i><span>手机话费直充</span></li>
            <li money="500"><i>500元话费</i><span>手机话费直充</span></li>
        </ul>
        <div class="OnebtnBox">
            <a href="javascript:;" class="Surebtn">确定</a>
        </div>


        <%--<h1>操作提示</h1>
        <div class="po-nrbox">
            <p>使用方式一经确认，不可修改！</p>
        </div>
        <div class="textBox">
            <input type="text" class="mobile" /><span>充值号码：</span>
        </div>
        <div class="textBox">
            <input type="text" class="mobagain" /><span>重复号码：</span>
        </div>
        <div class="TwobtnBox">
            <a href="javascript:;" class="Cancelbtn">取消</a>
            <a href="javascript:;" class="Surebtn">确定</a>
        </div>--%>
    </div>
    <!-- 充值到话费 E  -->

    <!-- 充值到支付宝 S  -->
    <div class="IsSelway-popup clearfix" id="zhifubaoPopBg"></div>
    <div id="zhifubaoPop">
        <header class="Winheader">
            <h2>话费转让</h2>
            <a class="Cancelbtn" href="javascript:;">
                <img width="30" height="30" src="images/fanhui.png">
            </a>
        </header>
        <div class="mobilebox zhifuInpbox">
            <input type="text" class="mobile" placeholder="请输入支付宝账号" />
            <input type="text" class="usename" placeholder="请输入真实姓名" />
            <p class="tips">请仔细确认支付宝账号和姓名无误</p>
        </div>

        <%--        <div class="iputxtbox">
            <li class="mianzhi">面值<span>50元(到账47.5元|服务费2.5元)</span></li>
        </div>
        <div class="iputxtbox">
            <li>卡号<span>hkjghjkghjkjjjkg</span></li>
            <li>卡密<span>hkjghjkghjkjjjkg</span></li>
        </div>--%>
        <div class="IAgree">同意<a href="/index.zhifubaoAgreement.aspx">《话费卡转让到支付宝协议》</a></div>
        <div class="OnebtnBox">
            <a href="javascript:;" class="Surebtn">确定</a>
        </div>


        <%--<h1>操作提示</h1>
        <div class="po-nrbox">
            <p>使用方式一经确认，不可修改！</p>
        </div>--%>
        <%--<div class="textBox textBox2"><span>支付宝账号：</span>
        </div>
        <div class="textBox textBox2">
            <input type="text" class="mobagain" /><span>重复号码：</span>
        </div>
        <div class="TwobtnBox">
            <a href="javascript:;" class="Cancelbtn">取消</a>
            <a href="javascript:;" class="Surebtn">确定</a>
        </div>--%>
    </div>


    <div id="ZhiFuBao">
    </div>
    <!-- 充值到支付宝 E  -->

    <!-- 充值到爽乐币 S  -->
    <div class="IsSelway-popup clearfix" id="TaolebiPopBg"></div>
    <div class="IsSelway_layer" id="TaolebiPop">
        <h1>操作提示</h1>
        <div class="po-nrbox">
            <p>使用方式一经确认，不可修改！</p>
            <h2>您是否要充值到当前账户？</h2>
        </div>
        <div class="TwobtnBox">
            <a href="javascript:;" class="Cancelbtn">取消</a>
            <a href="javascript:;" class="Surebtn">确定</a>
        </div>
    </div>
    <!-- 充值到爽乐币 E  -->

    <!-- QQ币充值 S  -->
    <div class="IsSelway-popup clearfix" id="QqPopBg"></div>
    <div class="IsSelway_layer" id="QqPop">
        <h1>操作提示</h1>
        <div class="po-nrbox">
            <p>使用方式一经确认，不可修改！</p>
        </div>
        <div class="textBox">
            <input type="text" class="mobile" /><span>Q Q号码：</span>
        </div>
        <div class="textBox">
            <input type="text" class="mobagain" /><span>重复号码：</span>
        </div>
        <div class="TwobtnBox">
            <a href="javascript:;" class="Cancelbtn">取消</a>
            <a href="javascript:;" class="Surebtn">确定</a>
        </div>
    </div>
    <!-- QQ币充值 E  -->
    <!-- 确认收货 S  -->
    <div class="IsSelway-popup clearfix" id="ShouhuoPopBg"></div>
    <div class="IsSelway_layer" id="ShouhuoPop">
        <h1>操作提示</h1>
        <div class="po-nrbox">
            <p>一经确认收货，不可修改！</p>
            <h2>您是否真的已收到该物品？</h2>
        </div>
        <div class="TwobtnBox">
            <a href="javascript:;" class="Cancelbtn">取消</a>
            <a href="javascript:;" class="Surebtn">确认收货</a>
        </div>
    </div>
    <!-- 确认收货 E  -->
    <!-- 确认使用默认地址 S  -->
    <div class="IsSelway-popup clearfix" id="DizhiPopBg"></div>
    <div class="IsSelway_layer" id="DizhiPop">
        <h1>操作提示</h1>
        <div class="po-nrbox">
            <p>收货地址提交成功后，不可修改！</p>
            <h2>您是否真的确定使用该地址？</h2>
        </div>
        <div class="TwobtnBox">
            <a href="javascript:;" class="Cancelbtn">取消</a>
            <a href="javascript:;" class="Surebtn">确定</a>
        </div>
    </div>
    <!-- 确认使用默认地址 E  -->

</body>
</html>
