<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="tuan.WinDetail.aspx.cs" Inherits="RedBook.WxTuan.tuan_WinDetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head>
    <meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
    <title>中奖详情</title>
    <meta content="app-id=518966501" name="apple-itunes-app" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, user-scalable=no, maximum-scale=1.0" />
    <meta content="yes" name="apple-mobile-web-app-capable" />
    <meta content="black" name="apple-mobile-web-app-status-bar-style" />
    <meta content="telephone=no" name="format-detection" />
    <link href="/css/comm.css" rel="stylesheet" type="text/css" />
    <link href="/css/tuan.css" rel="stylesheet" type="text/css" />
    <script src="/js/jquery-1.8.2.js" language="javascript" type="text/javascript"></script>
    <script src="/js/fastclick.js"></script>
</head>

<body>
    <header class="header">
        <h2>中奖详情</h2>
        <a id="A1" class="cefenlei" onclick="history.go(-1)" href="javascript:;">
            <img width="30" height="30" src="/images/fanhui.png">
        </a>
        <div class="head-r"><a href="/index.aspx" class="fr_Home"></a></div>
    </header>

    <section class="WinListInfoBox">
        <section class="WinningList-box">
            <%--<div class="win-goods">
                <div class="win-img">
                    <img src="">
                </div>
                <div class="win-ginfo">
                    <h1>标题</h1>
                    <h2><b>¥</b><span>50.00</span><i>￥70</i></h2>

                    <a class="link-kaited" href="javascript:;">已开奖</a>

                </div>
            </div>--%>
        </section>
        <div class="WinPer-lmtt">一等奖中奖名单</div>
        <section class="WinPerList">
            <ul class="WinPer-tt">
                <li class="one">幸运儿</li>
                <li class="two">订单编号</li>
                <li class="three">中奖码</li>
            </ul>
        </section>
    </section>

    <div id="divLoad"><b></b>正在加载...</div>
    <input id="pagenum" value="1" type="hidden" />

    <script>
        $(function () {
            FastClick.attach(document.body);

            GetWinGoodsInfo();
            WinnerInfo();
        });
        //页面传参
        function getUrlParam(name) {
            var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)"); //构造一个含有目标参数的正则表达式对象
            var r = window.location.search.substr(1).match(reg);  //匹配目标参数
            if (r != null) return unescape(r[2]); return null; //返回参数值
        }

        var nextPage = 1;
        var tid = getUrlParam("tid");
        //中奖人员名单
        function WinnerInfo() {
            $("#pagenum").val("0");
            if (parseInt($("#pagenum").val()) >= nextPage) {
                return;
            }//防止重复加载
            var ajaxUrl = "http://" + window.location.host + "/ilerrtuan?action=tuanwinnerinfo&tid=" + tid + "&type=1&p=" + nextPage;
            $.ajax({
                type: 'get', dataType: 'json', timeout: 10000,
                url: ajaxUrl,
                success: function (e) {
                    //暂无数据
                    if (e.count <= 0) {
                        $("#divLoad").text("暂无数据");
                        return;
                    }
                    for (var i = 0; i < e.data.length; i++) {
                        var ul = "";
                        ul += '<ul>' +
                                '<li class="one">' +
                                    '<img src="' + e.data[i].headimg + '">' +
                                    '<b>' + e.data[i].username + '</b>' +
                                '</li>' +
                                '<li class="two">' + e.data[i].orderId + '</li>' +
                                '<li class="three">' + e.data[i].goucode + '</li>' +
                            '</ul>';
                        $(".WinPerList").append(ul);
                    }
                    nextPage = e.nextPage;
                    if (nextPage == 0) {
                        $("#divLoad").text("加载完成");
                    }
                },
                error: function (me) {
                    alert("获取失败！");
                }
            });
        }

        //获取开奖商品信息
        function GetWinGoodsInfo() {
            var ajaxUrl = "http://" + window.location.host + "/ilerrtuan?action=tuandetail&tid=" + tid;
            $.ajax({
                type: 'get', dataType: 'json', timeout: 10000,
                url: ajaxUrl,
                success: function (e) {
                    if (e.state == 0) {//获取成功
                        var txtbox = '<a class="win-goods" href="/WxTuan/tuan.item.aspx?tuanid=' + tid + '">' +
                            '<div class="win-img">' +
                                '<img src="' + e.imgroot + e.data[0].thumb + '">' +
                            '</div>' +
                            '<div class="win-ginfo">' +
                                '<h1>' + e.data[0].title + '</h1>' +
                                '<h2><b>¥</b><span>' + parseFloat(e.data[0].per_price) + '</span><i>￥' + parseFloat(e.data[0].per_price) + '</i></h2>' +
                                '<div class="link-kaited">已开奖</div>' +
                            '</div>' +
                        '</a>';
                        $(".WinningList-box").append(txtbox);
                    }
                }
            });
        }

        //下拉加载
        $(window).scroll(function () {
            if ($(window).scrollTop() == $(document).height() - $(window).height()) {
                WinnerInfo();
            }
        });

    </script>

</body>

</html>
