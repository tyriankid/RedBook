<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.wangqi.aspx.cs" Inherits="RedBook.index_wangqi" %>

<!DOCTYPE html>
<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>往期回顾</title>
    <meta content="app-id=518966501" name="apple-itunes-app" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, user-scalable=no, maximum-scale=1.0" />
    <meta content="yes" name="apple-mobile-web-app-capable" />
    <meta content="black" name="apple-mobile-web-app-status-bar-style" />
    <meta content="telephone=no" name="format-detection" />
    <link href="css/comm.css?v=20150129" rel="stylesheet" type="text/css" />
    <link href="css/goods.css" rel="stylesheet" type="text/css" />
    <link href="css/style.css" rel="stylesheet" type="text/css" />
    <script src="js/jquery190.js" type="text/javascript"></script>
    <script src="/js/fastclick.js"></script>
    <script src="/js/AlertBox.js"></script>
</head>

<body>
    <header class="header">
        <h2>往期揭晓</h2>
        <a class="cefenlei" onclick="history.go(-1)" href="javascript:;">
            <img width="30" height="30" src="images/fanhui.png">
        </a>
        <div class="head-r"><a href="/index.aspx" class="fr_Home"></a></div>
    </header>
    <!-- 内页头部 End -->
    <!--往期列表-->
    <div class="pProcess_log">
        <ul>
        </ul>
        <div id="btnLoadMore" class="loading" style="background: none;">玩命加载中...</div>
    </div>

    <!-- 达到最大条数-->
    <div class="nomore" id="jumpQishu" style="display: none">
        <p>更多往期被隐藏起来了</p>
        <div class="inpbox"><span>输入您要查询的期数</span><span class="inp-txtbox"><input type="text" id="txtJumpQishu" class="inp-txt" /><input type="text" id="btnJumpQishu" class="btn-sure" value="确定" /></span></div>
    </div>

    <input id="pagenum" value="0" type="hidden" /><!--已加载到第几页-->
    <script type="text/javascript">
        //打开页面加载数据
        window.onload = function () {
            FastClick.attach(document.body);

            $("#pagenum").val("0");
            getHistoryList();
        };
        $(function () {
            $("#btnJumpQishu").click(function () {
                var itemId = getUrlParam("productid");
                var jumpQishu = $("#txtJumpQishu").val();
                var tiaoUrl = "http://" + window.location.host + "/ilerrshop?action=shopidbyqishu&productid=" + itemId + "&qishu=" + jumpQishu;
                $.ajax({
                    type: 'get', dataType: 'json', timeout: 10000,
                    url: tiaoUrl,
                    success: function (data) {
                        if (data.state == 0) {
                            window.location.href = "/index.item.aspx?shopid=" + data.yid;
                        } else {
                            showAlertDiv("当前期数不存在!");
                        }
                    }, error: function (me) {
                    }
                });
            });
        });
        //页面传参
        function getUrlParam(name) {
            var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)"); //构造一个含有目标参数的正则表达式对象
            var r = window.location.search.substr(1).match(reg);  //匹配目标参数
            if (r != null) return unescape(r[2]); return null; //返回参数值
        }
        //自动加载
        $(window).scroll(function () {
            if ($(window).scrollTop() == $(document).height() - $(window).height()) {
                getHistoryList();
            }
        });
        //获取往期揭晓列表
        var nextPage = 1;
        function getHistoryList() {
            //首先判断是否超过200条,如果超过,弹出搜索框,只能选择单期跳转
            if ($(".pProcess_log").find("li").length > 100) {
                //弹出搜索框
                $("#jumpQishu").show();
                $("#btnLoadMore").hide();
                if ($("#txtJumpQishu").val() == "") {
                    return;
                }
            }
            if (parseInt($("#pagenum").val()) >= nextPage) { return; }//防止重复加载
            var ajaxUrl = "http://" + window.location.host + "/ilerrshop?action=history&productid=" + getUrlParam("productid") + "&p=" + nextPage;
            var li = "";
            $.ajax({
                type: 'get', dataType: 'json', timeout: 10000,
                url: ajaxUrl,
                success: function (me) {
                    //暂无数据
                    if (me.count == 0) {
                        $("#btnLoadMore").text("暂无数据");
                        return;
                    }
                    $("#pagenum").val(nextPage);//当前页
                    nextPage = me.nextPage;//下一页
                    for (var i = 0; i < me.data.length; i++) {
                        li += '<li><a href="/index.item.aspx?operation=' + me.data[i].operation + '&shopid=' + me.data[i].yId + '">' +
                            '<h1><b>第' + me.data[i].qishu + '期</b>揭晓时间：' + me.data[i].q_end_time + '</h1>' +
                            '<img src="' + me.imgroot + me.data[i].img + '" />' +
                            '<div class="info"><div class="name bcolor">获得者：<b>' + me.data[i].username + '</b></div> ' +
                            '<p class="num">近期参与：<em>' + me.data[i].quantity + '</em>人次</p>' +
                            '<p class="num"> 幸运号码：<em>' + me.data[i].q_code + '</em></p>' +
                            '</div></a></li>';
                    }
                    $(".pProcess_log ul").append(li);
                    if (nextPage == 0) {
                        $("#btnLoadMore").text("加载完成");
                    }
                }
            })
        }

        //禁用右上角微信菜单
        document.addEventListener('WeixinJSBridgeReady', function onBridgeReady() {
            WeixinJSBridge.call('hideOptionMenu');
        });
		</script>
</body>

</html>
