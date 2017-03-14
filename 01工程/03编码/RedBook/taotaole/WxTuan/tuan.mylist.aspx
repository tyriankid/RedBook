<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="tuan.mylist.aspx.cs" Inherits="RedBook.WxTuan.tuan_mylist" %>

<!DOCTYPE html>
<html>
<head>
    <meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
    <title>我的团购</title>
    <meta content="app-id=518966501" name="apple-itunes-app" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, user-scalable=no, maximum-scale=1.0" />
    <meta content="yes" name="apple-mobile-web-app-capable" />
    <meta content="black" name="apple-mobile-web-app-status-bar-style" />
    <meta content="telephone=no" name="format-detection" />
    <link href="/css/comm.css" rel="stylesheet" type="text/css" />
    <link href="/css/member.css" rel="stylesheet" type="text/css" />
    <link href="/css/tuan.css" rel="stylesheet" type="text/css" />
    <script src="/js/fastclick.js"></script>
    <script src="/js/jquery-1.8.2.js" language="javascript" type="text/javascript"></script>

</head>
<body>
    <header class="header">
        <h2>我的团购</h2>
        <a id="A1" class="cefenlei" onclick="history.go(-1)" href="javascript:;">
            <img width="30" height="30" src="/images/fanhui.png">
        </a>
        <div class="head-r"><a href="/index.aspx" class="fr_Home"></a></div>
    </header>
    <section id="myinfo" class="home-head memberCenter">
    </section>

    <section class="MyTuanbox">
        <div class="con" avalonctrl="groups">
            <div class="nav_fixed">
                <a class="fixed_nav_item_groups">
                    <span class="nav_txt nav_cur">全部</span>
                </a>
                <a class="fixed_nav_item_groups">
                    <span class="nav_txt">拼团中</span>
                </a>
                <a class="fixed_nav_item_groups">
                    <span>已成团</span>
                </a>
                <a class="fixed_nav_item_groups">
                    <span>拼团失败</span>
                </a>
            </div>
            <div class="mt_order" id="groups" style="display: block;">
                <%--这里将加载我的团购数据--%>
            </div>
        </div>
        <div id="divLoad"><b></b>正在加载...</div>
        <input id="pagenum" value="0" type="hidden" />

        <script type="text/javascript">
            var index = -1;
            $("#pagenum").val("0");
            $(function () {
                FastClick.attach(document.body);

                $("#myinfo").html('');
                //用户信息
                UserInfo();
                //列表
                //$("#pagenum").val("0");
                //$("#groups").html("");
                getUserTuanList(index);
            });
            //个人信息
            function UserInfo() {
                var ajaxUrl = "http://" + window.location.host + "/ilerruser?action=visituser";
                $.ajax({
                    type: 'get', dataType: 'json', timeout: 10000,
                    url: ajaxUrl,
                    success: function (e) {
                        var div = '';
                        div += '<div class="user-photo-box">' +
                            '<div class="user-photo">' +
                                '<a href="/community.index.aspx" class="z-Himg">' +
                                    '<img style="border-radius: 110px;" src="' + e.imgroot + e.touxiang + '" border="0">' +
                                '</a>' +
                                '<em style="font-size: 14px; color: #fff; display: block; margin-top: 6px;">' + e.username + '</em>' +
                            '</div>' +
                            '<p style="text-align: center; color: #fff; font-size: 14px; margin-top: 5px;">' +
                    		    '签名：<span>我的签名还没想好呢</span>' +
                            '</p>' +
                            '</div>';
                        $("#myinfo").html(div);
                    }
                });
            }

            var nextPage = 1;
            var pagecount = 1;//总页数
            //获取数据列表
            function getUserTuanList(index) {
                $("#pagenum").val("0");
                //$("#divLoad").html("<b></b>正在加载...");
                if (parseInt($("#pagenum").val()) >= nextPage) {
                    return;
                }//防止重复加载
                var ajaxUrl = "http://" + window.location.host + "/ilerrtuan?action=tuanmylist&status=" + index + "&p=" + nextPage;
                $.ajax({
                    type: 'get', dataType: 'json', timeout: 10000,
                    url: ajaxUrl,
                    success: function (data) {
                        getUserTuanListEx(data);
                    }
                });
            }


            //获取列表方法
            function getUserTuanListEx(data) {
                //暂无数据
                if (data.count <= 0) {
                    $("#divLoad").text("暂无数据");
                    return;
                }
                $("#pagenum").val(nextPage);//当前页
                nextPage = data.nextPage;//下一页
                pagecount = data.pagecount;//总页数
                var statusname = "";//根据状态值得到显示状态显示名称
                var WinBtn = "";//查看中奖详情按钮
                var div = '';
                for (var i = 0; i < data.data.length; i++) {
                    var em = "";//一等奖/二等级/失败图标
                    //根据状态值得到显示状态显示名称
                    if (data.data[i].tuanstate == 0) {
                        statusname = "开团中";
                    }
                    else if (data.data[i].tuanstate == 1) {
                        statusname = "拼团成功，待开奖";
                        em = '<em class="icon-success"></em>';
                    }
                    else if (data.data[i].tuanstate == 2 && data.data[i].cstatus == "已支付") {
                        statusname = "拼团失败，待退款";
                        em = '<em class="icon-fail"></em>';
                    }
                    else if (data.data[i].tuanstate == 2 && data.data[i].cstatus == "已退款") {
                        statusname = "拼团失败，已退款";
                        em = '<em class="icon-fail"></em>';
                    }
                    else if (data.data[i].iswon == "1") {
                        statusname = "一等奖";
                        em = '<em class="icon-yidj"></em>';
                    }
                    else if (data.data[i].tuanstate == 3 && data.data[i].iswon == "0") {
                        statusname = "二等奖，已退款，红包已发放";
                        em = '<em class="icon-erdj"></em>';
                    }
                    //查看按钮
                    if (data.data[i].iswon == "1") {//一等奖
                        WinBtn = '<div class="mt_status">' +
                            '<a class="mt_status_lk1" href="tuan.join.aspx?tid=' + data.data[i].tId + '&tuanlistId=' + data.data[i].tuanlistId + '">查看团详情</a>' +
                            '<a class="mt_status_lk1 mt_btngreen" href="/WxTuan/tuan.WinDetail.aspx?tid=' + data.data[i].tId + '">查看中奖详情</a>' +
                            '<a class="mt_status_lk1 mt_btnyellow" href="/user.winningconfirm.aspx?orderid=' + data.data[i].orderId + '&shopid=' + data.data[i].tuanlistId + '">查看奖品</a>' +
                        '</div>';
                    }
                    else if (data.data[i].tuanstate == 3 && data.data[i].iswon == "0") {//二等奖
                        WinBtn = '<div class="mt_status">' +
                            '<a class="mt_status_lk1" href="tuan.join.aspx?tid=' + data.data[i].tId + '&tuanlistId=' + data.data[i].tuanlistId + '">查看团详情</a>' +
                            '<a class="mt_status_lk1 mt_btngreen" href="/WxTuan/tuan.WinDetail.aspx?tid=' + data.data[i].tId + '">查看中奖详情</a>' +
                        '</div>';
                    }
                    else if (data.data[i].tuanstate == 0) {//未成团，邀请好友拼团
                        WinBtn = '<div class="mt_status">' +
                            '<a class="mt_status_lk1" href="tuan.join.aspx?tid=' + data.data[i].tId + '&tuanlistId=' + data.data[i].tuanlistId + '">查看团详情</a>' +
                            '<a class="mt_status_lk1 mt_btnred" href="tuan.join.aspx?tid=' + data.data[i].tId + '&tuanlistId=' + data.data[i].tuanlistId + '&share=0">邀请好友拼团</a>' +
                        '</div>';
                    }
                    else {//查看团详情
                        WinBtn = '<div class="mt_status">' +
                            '<a class="mt_status_lk1" href="tuan.join.aspx?tid=' + data.data[i].tId + '&tuanlistId=' + data.data[i].tuanlistId + '">查看团详情</a>' +
                        '</div>';
                    }
                    ////一等奖/二等级
                    //if (data.data[i].iswon == "1") {//一等奖
                    //    em = '<em class="icon-yidj"></em>';
                    //}
                    //else if (data.data[i].tuanstate == 3 && data.data[i].iswon == "0") {//二等奖
                    //    em = '<em class="icon-zhongj"></em>';
                    //}

                    div += '<div class="myLottery">' +
                                '<h1>' + data.data[i].total_num + '人团<b>' + statusname + '</b><span style="padding-left:10px;">团ID:' + data.data[i].tuanlistId + '</span></h1>' +
                                //'<a class="mt_g" href="tuan.orderInfo.aspx?orderid=' + data.data[i].orderId + '">' +
                                '<a class="mt_g" href="tuan.item.aspx?tuanid=' + data.data[i].tId + '">' +
                                    '<div class="mt_g_wrap">' +
                                        '<div class="mt_g_img">' +
                                            '<img src="' + data.imgroot + data.data[i].picimg + '">' +
                                        '</div>' +
                                        '<div class="mt_g_info">' +
                                            '<p class="mt_g_name">' + data.data[i].title + '</p>' +
                                            '<span class="money_txt">实付:<b>￥' + parseFloat(data.data[i].per_price) + '</b> (免运费)</span>' +
                                        '</div>' + em +
                                    '</div>' +
                                '</a>' + WinBtn +
                            '</div>';
                }
                $("#groups").append(div);
                if (nextPage == 0) {
                    $("#divLoad").text("加载完成");
                }
            }


            //根据状态值得到显示状态显示名称
            function getStatusName(status) {
                var statusname = "";
                switch (status) {
                    case 0:
                        statusname = "拼团中";
                        break;
                    case 1:
                        statusname = "成团";
                        break;
                    case 2:
                        statusname = "拼团失败";
                        break;
                    case 3:
                        statusname = "已开奖";
                        break;
                }
                return statusname;
            }

            //点击切换Tab状态样式
            $(".nav_fixed a").click(function () {
                index = $(this).index();
                $('#groups').html('');
                $("#pagenum").val("0");
                nextPage = 1;
                //$('.loadMore').css('display','block');
                $(this).find("span").addClass("nav_cur");
                $(this).siblings().find("span").removeClass();
                $("#divLoad").html("<b></b>正在加载...");
                getUserTuanList(index);
            });

            //下拉加载
            $(window).scroll(function () {
                if ($(window).scrollTop() == $(document).height() - $(window).height()) {
                    getUserTuanList(index);
                }
            });

            //禁用右上角微信菜单
            document.addEventListener('WeixinJSBridgeReady', function onBridgeReady() {
                WeixinJSBridge.call('hideOptionMenu');
            });
        </script>
    </section>
    <%--<section class="h50"></section>--%>
</body>

</html>
