<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.lottery.aspx.cs" Inherits="RedBook.index_lottery" %>
<%@ Register src="Controls/ucFoot.ascx" tagname="ucFoot" tagprefix="uc1" %>
<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>
<%@ Register TagPrefix="UI" Namespace="Taotaole.Common" Assembly="Taotaole.Common" %>
<!DOCTYPE html>
<html>

<head>
    <title>最新揭晓</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta content="app-id=518966501" name="apple-itunes-app" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, user-scalable=no, maximum-scale=1.0" />
    <meta content="yes" name="apple-mobile-web-app-capable" />
    <meta content="black" name="apple-mobile-web-app-status-bar-style" />
    <meta content="telephone=no" name="format-detection" />
    <link href="css/comm.css" rel="stylesheet" type="text/css" />
    <link href="css/lottery.css" rel="stylesheet" type="text/css" />
    <script src="js/jquery190.js" language="javascript" type="text/javascript"></script>
    <script src="internalapi/common.js" type="text/javascript"></script>
    <!-- 中奖确认js -->
     <script src="http://res.wx.qq.com/open/js/jweixin-1.0.0.js"></script>
    <script src="js/wxShare.js" type="text/javascript"></script>
    <script src="js/fastclick.js"></script>
    <script src="js/WinRemind.js?v=3" language="javascript" type="text/javascript"></script>
    <script src="js/jquery.rotate.js"></script>
</head>

<body>
    <%--<header class="header">
        <h2>最新揭晓</h2>
        <a class="cefenlei" onclick="history.go(-1)" href="javascript:;">
            <img width="30" height="30" src="images/fanhui.png">
        </a>
        <div class="head-r"><a href="/index.aspx" class="fr_Home"></a></div>
    </header>--%>
    <section class="revealed">
        <div id="divLottery" class="revCon">

            <div role="middle"></div>
            <!--分界点,上面是正在揭晓倒计时,下面是已经揭晓-->

            <div id="divLotteryLoading" class="loading"><b></b>加载中...</div>
        </div>
    </section>
    <ul class="alityBtn">
        <li onclick="goBackTop()">
            <img src="/images/goBackTop.png" alt="返回顶部" /></li>
        <li onclick="refresh()">
            <img src="/images/refresh.png" alt="页面刷新" id="imgref" /></li>
    </ul>
    <input id="pagenum" value="1" type="hidden" /><!--已加载到第几页-->
    
    <uc1:ucFoot ID="ucFoot1" runat="server" />
    <UI:WeixinSet ID="weixin" runat="server" />
    <script type="text/javascript">
        $(function () {
            FastClick.attach(document.body);

            $("#pagenum").val("0");
            getShowLoterryList();
            getLoterryList();

            var gt = '#divLottery ul';
            $(document).on('click', gt, function () {
                var id = $(this).attr('id');
                if (id) {
                    window.location.href = "/index.item.aspx?shopid=" + id;
                }
            });

        });


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
            currentPage = 1;
            $("#pagenum").val("0");
            nextPage = 1;
            Isloading = true;
            refvalue += 360;
            $("#imgref").rotate({ animateTo: refvalue });
            $("#divLottery ul").remove();
            getLoterryList();
            getShowLoterryList();
            $("#divLoad").html("加载中...");
        }

        var nextPage = 1;
        ///获取最新的已经揭晓数据
        function getLoterryList() {
            if (nextPage > 10) {
                return;
            }
            if (parseInt($("#pagenum").val()) >= nextPage) {
                return;
            }//防止重复加载
            var ajaxUrl = "http://" + window.location.host + "/ilerrbuy?action=listinfo&p=" + nextPage;
            var ul = "";
            $.ajax({
                type: 'get', dataType: 'json', timeout: 10000,
                url: ajaxUrl,
                success: function (data) {
                    Isloading = false;
                    //暂无数据
                    if (data.count == 0) {
                        $("#divLotteryLoading").text("暂无数据");
                        return;
                    }
                   
                    $("#pagenum").val(nextPage);//当前页
                    nextPage = data.nextPage;//下一页
                    for (var i = 0; i < data.data.length; i++) {
                        if ($("#" + data.data[i].yId).length > 0) { continue; }
                        //if (data.data[i].username==)
                        //var huodezurl = '/index.calResult.aspx?shopid=' + data.data[i].yId;
                        var huodezurl = '/index.userindex.aspx?uid=' + data.data[i].q_uid;
                        ul += '<ul id="' + data.data[i].yId + '">' +
                            '<a class="revConL" href="/index.item.aspx?shopid=' + data.data[i].yId + '&operation=' + data.data[i].operation + '"><img src="' + data.imgroot + data.data[i].thumb + '"></a>' +
                            '<li class="revConR">' +
                                '<dl><dt><a  href="/index.item.aspx?shopid=' + data.data[i].yId + '&operation=' + data.data[i].operation + '">' + data.data[i].title + '</a></dt>' +
                                '<dd>期号：' + data.data[i].qishu + '</dd>' +
                                '<dd>幸运码：<em class="orange arial">' + data.data[i].q_code + '</em></dd>' +
                                '<dd>获得者：<a name="uName" uweb="' + data.data[i].q_uid + '" class="rUserName blue" href="' + huodezurl + '">' + data.data[i].username + '</a></dd>' +
                                '<dd>本期参与：<em class="orange arial">' + data.data[i].quantity + '</em>人次</dd>' +
                                '<dd>揭晓时间：' + data.data[i].q_end_time + '</dd>' +
                            '</dl></li></ul>';
                    }
                    $("#divLotteryLoading").before(ul);
                    if (nextPage == 0) {
                        $("#divLotteryLoading").text("加载完成");
                    }
                    else if (nextPage >= 21) {
                        $("#divLotteryLoading").show();
                        $("#divLotteryLoading").text("最多只能看200条最新揭晓信息哟~");
                    }
                }
            });
        }

        //获取正在揭晓中的商品
        function getShowLoterryList() {
            var ajaxUrl = "http://" + window.location.host + "/ilerrbuy?action=listwill";
            var ul = "";
            $.ajax({
                type: 'get', dataType: 'json', timeout: 10000, url: ajaxUrl, success: function (data) {
                    Isloading = false;
                    //暂无数据
                    if (data.state == 1) {
                        return;
                    }
                    for (var i = 0; i < data.data.length; i++) {
                        if ($("#" + data.data[i].yId).length > 0) { continue; }
                        ul += '<ul class="WeiJieXiao" id="' + data.data[i].yId + '">' +
                                    '<li class="revConL"><a class="revConLimg" href="/index.item.aspx?shopid=' + data.data[i].yId + '"><img src="' + data.imgroot + data.data[i].thumb + '"></a></li>' +
                                    '<li class="revConR"><dl><dt><a  href="/index.item.aspx?shopid=' + data.data[i].yId + '">' + data.data[i].title + '</a></dt>' +
                                    '<dd>期号：' + data.data[i].qishu + '</dd><dd>揭晓倒计时：</dd><dd><time class="daojishi" id="' + data.data[i].yId + '" leftTime="' + data.data[i].timeSpan + '"></time></dd>' +
                        '</dl></li></ul>';
                    }
                    $("[role='middle']").before(ul);
                    countdownLoterry();//开始倒计时,并在倒计时结束后关闭动画获取中奖人信息
                }
            });


        }

        ///调用倒计时效果,common.js内倒计时结束后,更新动画状态为停止
        function countdownLoterry() {
            $(".daojishi").each(function () {
                var countDownMS = parseInt($(this).attr("leftTime"));
                var ag = new startTimeOut($(this), countDownMS, "lotteryList");//测试倒计时
                ag.countdown();
            });
        }

        //下拉加载
        var currentPage = 1;
        $(window).scroll(function () {
            if ($(window).scrollTop() >= $(document).height() - $(window).height()) {
                if (currentPage == nextPage) return;
                getLoterryList();
                currentPage = nextPage;
            }
        });

        //禁用右上角微信菜单
        document.addEventListener('WeixinJSBridgeReady', function onBridgeReady() {
            WeixinJSBridge.call('hideOptionMenu');
        });
    </script>

</body>

</html>
