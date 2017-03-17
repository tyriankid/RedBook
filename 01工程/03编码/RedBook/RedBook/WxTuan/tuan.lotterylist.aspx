<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="tuan.lotterylist.aspx.cs" Inherits="RedBook.WxTuan.tuan_lotterylist" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
    <title>我的抽奖</title>
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
        <h2>我的抽奖</h2>
        <a id="A1" class="cefenlei" onclick="history.go(-1)" href="javascript:;">
            <img width="30" height="30" src="/images/fanhui.png">
        </a>
        <div class="head-r"><a href="/index.aspx" class="fr_Home"></a></div>
    </header>

    <div class="mt_order" id="groups" style="display: block;">

        <%--<div class="myLottery">
            <h1>3人团<b>二等级，已退款并送券</b></h1>
            <a class="mt_g" href="">
                <div class="mt_g_wrap">
                    <div class="mt_g_img">
                        <img src="ret.listItems[i].thumb+'">
                    </div>
                    <div class="mt_g_info">
                        <p class="mt_g_name">题目题目题目题目题目题目题目题目题目题目题目题目</p>
                        <span class="money_txt">实付:<b>￥0.1</b>(免运费)</span>
                    </div>
					<em class="icon-zhongj"></em>
					<em class="icon-yidj"></em>
                </div>
            </a>
            <div class="mt_status">
                <a class="mt_status_lk1" href="">查看中奖详情</a>
            </div>
        </div>--%>
    </div>
    <div id="divLoad"><b></b>正在加载...</div>
    <input id="pagenum" value="1" type="hidden" />

    <script type="text/javascript">
        $(function () {
            FastClick.attach(document.body);

            $("#myinfo").html('');
            //列表
            $("#pagenum").val("0");
            getUserTuanList();
        });
        var nextPage = 1;
        var pagecount = 1;//总页数
        //获取列表
        function getUserTuanList() {
            $("#pagenum").val("0");
            if (parseInt($("#pagenum").val()) >= nextPage) {
                return;
            }//防止重复加载
            var ajaxUrl = "http://" + window.location.host + "/ilerrtuan?action=tuanmylist&status=0&p=" + nextPage;
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
            var em = "";//一等奖/二等级
            var div = '';
            for (var i = 0; i < data.data.length; i++) {
                //根据状态值得到显示状态显示名称
                if (data.data[i].tuanstate == 0) {
                    statusname = "开团中";
                }
                else if (data.data[i].tuanstate == 1) {
                    statusname = "拼团成功，待开奖";
                }
                else if (data.data[i].tuanstate == 2 && data.data[i].cstatus == "已支付") {
                    statusname = "拼团失败，待退款";
                }
                else if (data.data[i].tuanstate == 2 && data.data[i].cstatus == "已退款") {
                    statusname = "拼团失败，已退款";
                }
                else if (data.data[i].iswon == "1") {
                    statusname = "一等奖";
                }
                else if (data.data[i].tuanstate == 3 && data.data[i].iswon == "0") {
                    statusname = "二等奖，红包已发放";
                }
                //alert('/user.winningconfirm.aspx?orderid=' + data.data[i].orderId + '&shopid=' + data.data[i].tuanlistId);
                //查看按钮
                if (data.data[i].iswon == "1") {//一等奖
                    WinBtn = '<div class="mt_status">' +
                        '<a class="mt_status_lk1" href="tuan.join.aspx?tid=' + data.data[i].tId + '&tuanlistId=' + data.data[i].tuanlistId + '">查看团详情</a>' +
                        '<a class="mt_status_lk1 mt_btngreen" href="/WxTuan/tuan.WinDetail.aspx?tid=' + data.data[i].tuanlistId + '">查看中奖详情</a>' +
                        '<a class="mt_status_lk1 mt_btnyellow" href="/user.winningconfirm.aspx?orderid=' + data.data[i].orderId + '&shopid=' + data.data[i].tuanlistId + '">奖品发放</a>' +
                    '</div>';
                }
                else if (data.data[i].tuanstate == 3 && data.data[i].iswon == "0") {//二等奖
                    WinBtn = '<div class="mt_status">' +
                        '<a class="mt_status_lk1" href="tuan.join.aspx?tid=' + data.data[i].tId + '&tuanlistId=' + data.data[i].tuanlistId + '">查看团详情</a>' +
                        '<a class="mt_status_lk1 mt_btngreen" href="/WxTuan/tuan.WinDetail.aspx?tid=' + data.data[i].tuanlistId + '">查看中奖详情</a>' +
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
                //一等奖/二等级
                if (data.data[i].iswon == "1") {//一等奖
                    em = '<em class="icon-yidj"></em>';
                }
                else if (data.data[i].tuanstate == 3 && data.data[i].iswon == "0") {//二等奖
                    em = '<em class="icon-zhongj"></em>';
                }

                div += '<div class="myLottery">' +
                            '<h1>' + data.data[i].total_num + '人团<b>' + statusname + '</b></h1>' +
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

        //下拉加载
        $(window).scroll(function () {
            if ($(window).scrollTop() == $(document).height() - $(window).height()) {
                getUserTuanList();
            }
        });
    </script>

</body>
</html>
