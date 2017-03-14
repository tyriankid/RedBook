<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="user.login.aspx.cs" Inherits="RedBook.user_login" %>

<!DOCTYPE html>
<html>

<head>
    <meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
    <title>登录</title>
    <meta content="app-id=518966501" name="apple-itunes-app" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, user-scalable=no, maximum-scale=1.0" />
    <meta content="yes" name="apple-mobile-web-app-capable" />
    <meta content="black" name="apple-mobile-web-app-status-bar-style" />
    <meta content="telephone=no" name="format-detection" />
    <link href="css/comm.css" rel="stylesheet" type="text/css" />
    <link href="css/login.css" rel="stylesheet" type="text/css" />
    <script src="js/jquery190.js" language="javascript" type="text/javascript"></script>
    <script id="pageJS" data="js/Login.js" language="javascript" type="text/javascript"></script>
    <script src="/js/fastclick.js"></script>
</head>

<body style="background: #f4f4f4;">
    <header class="header">
        <h2>帐号登陆</h2>
        <a class="cefenlei" onclick="history.go(-1)" href="javascript:;">
            <img width="30" height="30" src="images/fanhui.png">
        </a>
        <div class="head-r"><a href="/index.aspx" class="fr_Home"></a></div>
    </header>
    <div class="h5-1yyg-v1" id="content">
        <section>
            <div class="registerCon">
                <ul>
                    <li class="accAndPwd" style="float: none; height: auto;">
                        <dl>
                            <input id="txtAccount" type="text" placeholder="请输入您的手机号码/邮箱" class="lEmail">
                            <s class="rs4"></s>
                        </dl>
                        <dl>
                            <input style="border-top: 1px solid #ddd;" type="password" id="txtPassword" class="lPwd" placeholder="密码">
                            <s class="rs3"></s>
                        </dl>
                        <dl style="display: none">
                            <input type="hidden" id="txtVerify" class="lVerify" placeholder="验证码" value="123456">
                            <span class="fog"></span>
                            <s class="rs3"></s>
                        </dl>
                    </li>
                    <li style="float: none;">
                        <a href="javascript:;" id="btnLogin" class="nextBtn orgBtn" style="width: 100%; text-align: center; display: block; margin: 0 auto; height: 50px; line-height: 55px;">登&nbsp&nbsp&nbsp录</a>
                        <input name="hidLoginForward" type="hidden" id="hidLoginForward" value="{WEB_PATH}/mobile/home" />
                    </li>
                    </li>
                    <li style="margin-top: 30px; text-align: center;">
                        <a style="height: 20px; line-height: 20px; color: #999!important; background: none; display: inline; font-size: 14px; margin-right: 10%;" href="{WEB_PATH}/mobile/user/step1" class="nextBtn orgBtn" style="width: 40%; float: right;">忘记密码?</a> |
                        <a style="height: 20px; line-height: 20px; color: #999!important; background: none; display: inline; font-size: 14px; margin-left: 10%;" href="{WEB_PATH}/mobile/user/register" class="nextBtn orgBtn">免费注册</a>
                    </li>
                </ul>
                <div class="fastLogin" style="padding-top: 30px;">
                    <h2>
                        <span class="line_l"></span>
                        一键登录
				<span class="line_r"></span>
                    </h2>
                    <div class="fastInfo" style="margin-top: 20px;">
                        <a href="">
                            <img src="images/qq.png" alt="" class="user_login_q"></a>
                        <a href="">

                            <img src="images/wx.png" alt="" class="user_login_w"></a>
                    </div>
                </div>
            </div>
        </section>
        <script language="javascript" type="text/javascript">
            $(function () {
                FastClick.attach(document.body);
            })
            var Path = {};
            Path.Skin = "{G_TEMPLATES_STYLE}";
            Path.Webpath = "{WEB_PATH}";
            var Base = {
                head: document.getElementsByTagName("head")[0] || document.documentElement,
                Myload: function (B, A) {
                    this.done = false;
                    B.onload = B.onreadystatechange = function () {
                        if (!this.done && (!this.readyState || this.readyState === "loaded" || this.readyState === "complete")) {
                            this.done = true;
                            A();
                            B.onload = B.onreadystatechange = null;
                            if (this.head && B.parentNode) {
                                this.head.removeChild(B)
                            }
                        }
                    }
                },
                getScript: function (A, C) {
                    var B = function () { };
                    if (C != undefined) {
                        B = C
                    }
                    var D = document.createElement("script");
                    D.setAttribute("language", "javascript");
                    D.setAttribute("type", "text/javascript");
                    D.setAttribute("src", A);
                    this.head.appendChild(D);
                    this.Myload(D, B)
                },
                getStyle: function (A, B) {
                    var B = function () { };
                    if (callBack != undefined) {
                        B = callBack
                    }
                    var C = document.createElement("link");
                    C.setAttribute("type", "text/css");
                    C.setAttribute("rel", "stylesheet");
                    C.setAttribute("href", A);
                    this.head.appendChild(C);
                    this.Myload(C, B)
                }
            };

            function GetVerNum() {
                var D = new Date();
                return D.getFullYear().toString().substring(2, 4) + '.' + (D.getMonth() + 1) + '.' + D.getDate() + '.' + D.getHours() + '.' + (D.getMinutes() < 10 ? '0' : D.getMinutes().toString().substring(0, 1))
            }

            Base.getScript('js/Bottom.js?v=' + GetVerNum());

            var checkcode = document.getElementById('checkcode');

            checkcode.src = checkcode.src + new Date().getTime();

            var src = checkcode.src;

            checkcode.onclick = function () {

                this.src = src + '/' + new Date().getTime();

            }
        </script>
    </div>
</body>

</html>
