<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="shouchong5.aspx.cs" Inherits="RedBook.WxZhuanti.shouchong5" %>
<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>
<%@ Register TagPrefix="UI" Namespace="Taotaole.Common" Assembly="Taotaole.Common" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
    <title>首充5元，不中全赔</title>
    <meta content="app-id=518966501" name="apple-itunes-app" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, user-scalable=no, maximum-scale=1.0" />
    <meta content="yes" name="apple-mobile-web-app-capable" />
    <meta content="black" name="apple-mobile-web-app-status-bar-style" />
    <meta content="telephone=no" name="format-detection" />
    <meta name="msapplication-tap-highlight" content="no">
    <%--<link href="/css/comm.css" rel="stylesheet" type="text/css" />--%>
    <link href="css/zhuanti.css" rel="stylesheet" type="text/css" />
    <script src="/js/jquery190.js" type="text/javascript"></script>
	<script src="http://res.wx.qq.com/open/js/jweixin-1.0.0.js"></script>
	<script src="/js/wxShare.js" type="text/javascript"></script>
    <script src="/js/AlertBox.js?v=2.4"></script>
    <script src="/js/fastclick.js"></script>
    <script>
        $(function () {
            FastClick.attach(document.body);
            setWxShareInfo();
        });
        function setWxShareInfo() {
            var shareImg = "/images/icon/";
            var shareTitleDes = '哇！在爽乐购看到好东西，必须拿出来分享...';
            var shareDes = "首付5元，不中包赔！";
            var link = "http://" + window.location.host + "/WxZhuanti/shouchong5.aspx";
            WinXinShareMessage(shareTitleDes, shareDes, link, shareImg);
        }
    </script>
</head>
<body>
    <header class="head_zhuanti">
        <h2>活动详情</h2>
        <a class="close" href="/WxZhuanti/zhongjiangmiji.aspx">关闭</a>
    </header>
    <section class="zhuantibody">
        <div class="Picbox">
            <img src="images/shouchong5/shouchong5_01.jpg" />
        </div>
        <div class="Picbox">
            <img id="btn-chongzhi" src="images/shouchong5/shouchong5_02.jpg" />
        </div>
        <div class="Picbox">
            <img src="images/shouchong5/shouchong5_03.gif" />
        </div>
        <div class="Picbox">
            <a href="javascript:;" id="PayLink"><img src="images/shouchong5/shouchong5_04.gif" /></a>
        </div>
        <div class="Picbox">
            <img src="images/shouchong5/shouchong5_05.gif" />
        </div>
    </section>
    

    <script>
        $("#PayLink").click(function () {
            IsPass();
        });
        //判断用户参与活动的资格
        function IsPass() {
            var ajaxUrl = "http://" + window.location.host + "/ilerrredpack?action=valactivityqualification&aid=23";
            $.ajax({
                type: 'get', dataType: 'json', timeout: 10000,
                url: ajaxUrl,
                success: function (e) {
                    //alert("0有资格,3已经参与过,4已兑换,目前state是：" + e.state);
                    if (e.state == 0) {//0没有资格
                        
                        $.getJSON("http://" + window.location.host + "/ilerruser?action=visituser", function (data) {
                            if (data.money >= 5) {
                                pay();
                            }
                            else {
                                $("#PayLink").attr("href", "http://" + window.location.host + "/WxPay/wx_Submit.aspx?businessType=yuan&money=5&productid=2&quantity=1&paytype=2&redpaper=0&otype=1");
                            }
                        })
                    }
                    else if (e.state == 1) {//有参与资格
                        showAlertDiv("您已经参与过了！");
                        $("#PayLink").attr("href", "#");
                    }
                    else {//没有参赛资格
                        showAlertDiv("您没有参赛资格！");
                        $("#PayLink").attr("href", "#");
                    }
                },
                error: function (me) {
                    showAlertDiv("获取失败！", "calBack()");
                    $("#PayLink").attr("href", "#");
                }
            });
        }

        function calBack() {
            window.location = "/index.aspx";
        }

        //余额支付
        function pay() {
            $("#PayLink").attr("href", "#");
            $.getJSON('http://' + window.location.host + '/ilerrpay?action=userpaymentsubmit&productid=2&quantity=1&paytype=0&redpaper=0&APIsubmitcode=0', function (data) {
                switch (data.state) {
                    case 0://验证通过
                        showAlertDiv("您已获得不中包赔资格！", "sucesBack()");
                        //location.href = "/WxZhuanti/zhongjiangmiji.aspx";//后续若有其它业务需要通过商品原始ID进行支付，可扩冲一个类型变量实现无限扩展
                        break;
                    default:
                        alert("一元购订单产生错误,错误码:" + data.state + ",支付成功的爽乐币已经加入账户余额!");
                        break;
                }
            });
        }
        function sucesBack() {
            window.location = "/WxZhuanti/zhongjiangmiji.aspx";//后续若有其它业务需要通过商品原始ID进行支付，可扩冲一个类型变量实现无限扩展
        }
    </script>
</body>
</html>
