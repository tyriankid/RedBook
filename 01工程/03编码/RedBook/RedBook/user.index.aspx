<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="user.index.aspx.cs" Inherits="RedBook.user_index" %>
<%@ Register src="Controls/ucFoot.ascx" tagname="ucFoot" tagprefix="uc1" %>

<!DOCTYPE html>
<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, user-scalable=no, maximum-scale=1.0" />
    <title>个人中心</title>
    <META HTTP-EQUIV="pragma" CONTENT="no-cache"> 
    <META HTTP-EQUIV="Cache-Control" CONTENT="no-cache, must-revalidate"> 
    <META HTTP-EQUIV="expires" CONTENT="0">
    <meta content="yes" name="apple-mobile-web-app-capable" />
    <meta content="black" name="apple-mobile-web-app-status-bar-style" />
    <meta content="telephone=no" name="format-detection" />
    <script src="http://res.wx.qq.com/open/js/jweixin-1.0.0.js"></script>
    <link href="css/comm.css?v=130715" rel="stylesheet" type="text/css" />
    <link href="css/member.css?v=130726" rel="stylesheet" type="text/css" />
    <script src="js/jquery190.js" language="javascript" type="text/javascript"></script>
    <script src="/js/fastclick.js"></script>
</head>
<body>
    <%--<header class="header">
        <h2>个人中心</h2>
        <a id="fanhui" class="cefenlei" onclick="history.go(-1)" href="javascript:;">
            <img width="30" height="30" src="images/fanhui.png">
        </a>
        <div class="head-r"><a href="/user.service.aspx" class="fr_baozhu"></a></div>
    </header>--%>
    <!-- 栏目页面顶部 -->
    <!-- 内页顶部 -->
    <section class="clearfix g-member">
        <div class="m-member-nav">
            <ul class="ulFun">
                <li class="icon-iuserjf">
                    <a href="gift.index.aspx">积分商城</a>
                </li>
                <li class="icon-iusergr">
                    <a href="community.index.aspx">我的帐户</a>
                </li>
                <%--
                <li class="icon-iuserpt">
                    <a href="/WxTuan/tuan.mylist.aspx">我的拼团</a>
                </li>
                <li class="icon-love">
                    <a href="/WxTuan/tuan.Collection.aspx">我的收藏</a>
                </li>
                    --%>
               <li class="icon-iuserjfjp">
                    <a href="/user.giftlist.aspx">积分奖品</a>
                </li>
                <li class="icon-iuserchouj">
                    <a href="/user.zhigou.aspx">我的直购</a>
                </li>
                <%--
                <li class="icon-iuserchouj">
                    <a href="/user.zengbi.aspx">我的夺宝币</a>
                </li>
                <li class="icon-iuserlg">
                        <a href="">
                            爽乐购零购
                        </a>
                    </li>--%>
            </ul>
            <ul class="ulFun">
                <%--<li class="icon-iuserqd">
                    <a href="javascript:;" onclick="alertBox('此功能尚未开通')">每日签到
                    </a>
                </li>--%>
                <li class="icon-iuserzh">
                    <a href="/user.userbalance.aspx">账户明细
                    </a>
                </li>
                <li class="icon-iuserdz">
                    <a href="/user.address.aspx">收货地址
                    </a>
                </li>
                <li class="icon-iusergame">
                    <a href="/user.gameaddress.aspx">游戏地址
                    </a>
                </li>
            </ul>
            <ul class="ulFun">
                <%--<li class="icon-iuserxy">
                    <a class="icon-iuserxy" href="/user.service.aspx">爽乐购宝珠
                    </a>
                </li>--%>
                <li class="icon-iuserxs">
                    <a href="/user.newcomer.aspx">新手入门
                    </a>
                </li>
                <li class="icon-iuserkf">
                    <a href="/user.help.aspx">常见问题
                    </a>
                </li>
                <li class="icon-iusertc">
                    <a href="user.updateApp.aspx">APP下载
                    </a>
                </li>
            </ul>
        </div>
    </section>
    <uc1:ucFoot ID="ucFoot1" runat="server" />
    <script>
        $(function () {
            FastClick.attach(document.body);
        })
        function alertBox(content) {
            $('.alertBox .content').text(content);
            $('.mask').show();
            $('.alertBox').show();
        }

        $('.alertBox .close').click(function () {
            $('.mask').hide();
            $('.alertBox').hide();
        })
        //禁用右上角微信菜单
        document.addEventListener('WeixinJSBridgeReady', function onBridgeReady() {
            WeixinJSBridge.call('hideOptionMenu');
        });
    </script>
</body>
</html>
