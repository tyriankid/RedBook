<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="user.service.aspx.cs" Inherits="RedBook.user_service" %>
<!DOCTYPE html>
<html>
<head>
    <meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, user-scalable=no, maximum-scale=1.0" />
    <title>爽乐购宝珠</title>
    <meta content="app-id=518966501" name="apple-itunes-app" />
    <meta content="yes" name="apple-mobile-web-app-capable" />
    <meta content="black" name="apple-mobile-web-app-status-bar-style" />
    <meta content="telephone=no" name="format-detection" />
    <link href="css/comm.css" rel="stylesheet" type="text/css" />
    <link href="css/lottery.css" rel="stylesheet" type="text/css" />
    <link href="css/winning.css" rel="stylesheet" type="text/css" />
    <script src="js/jquery190.js" language="javascript" type="text/javascript"></script>
    <script src="/js/fastclick.js"></script>
    <style>
        body {
            background:#f5f5f5;
        }
        .topCon {
            padding:30px 0;
            text-align:center;
            color:#fff;
            font-size:16px;
            background: url(images/member_bg.jpg) no-repeat bottom center;
            background-size: cover;
        }
        .topCon img{
            display:inline-block;
            width:26px;
            vertical-align:top;
        }
        .topCon i{
            display:inline-block;
            width:30px;
            height:30px;
            background:url(/images/baozhu.png) no-repeat center center;
            background-size:30px;
            vertical-align: bottom;
        }
        .topCon span{
            margin: 0 15px;
            font-size:39px;
            line-height:30px;
            vertical-align: bottom;
        }
        .tips {
            background:#fff;
            padding:10px 15px;
            font-size: 14px;
        }
        .tips span{
            float:right;
        }
        .tips span i{
            font-style:normal;
            color:#FD524E;
        }
        .product{
            border-bottom:1px solid #efefef;
        }
        .product li {
            position:relative;
            padding:20px;
            border-top:1px solid #efefef;
            overflow:hidden;
        }
        .product li .imgBox{
            width:30%;
        }
        .product li .imgBox img{
            width:100%;
        }
        .product li .info{
            position:absolute;
            left:35%;
            right:20px;
            top:50%;
            font-size:14px;
            transform:translateY(-50%);
            -webkit-transform:translateY(-50%);
        }
        .product li .info p{
            line-height:20px;
        }
        .product li .info p span{
            font-size:18px;
        }
        .product li .info p:nth-child(2){
            color:#888;
        }
        .product li .info strong{
            margin-top:10px;
            display:inline-block;
            font-weight:normal;
            color:#fff;
            padding:5px 15px 6px;
            border-radius:4px;
            background:#FD524E;
        }
        .dialog{
            display:none;
            position: fixed;
            top: 50%;
            left: 50%;
            transform: translate(-50%,-50%);
            -webkit-transform: translate(-50%,-50%);
            background: #fff;
            background: -webkit-radial-gradient(#fff 5%, #efefef 60%); /* Safari 5.1 - 6.0 */
            background: -o-radial-gradient(#fff 5%, #efefef 60%); /* Opera 11.6 - 12.0 */
            background: -moz-radial-gradient(#fff 5%, #efefef 60%); /* Firefox 3.6 - 15 */
            background: radial-gradient(#fff 5%, #efefef 60%); /* 标准的语法 */
            border-radius:10px;
            padding: 10px 0;
            z-index: 1000000;
        }
        .dialog ul li{
            font-size:15px;
            padding:0 15px;
            line-height:42px;
            white-space:nowrap;
        }
        .dialog ul li:first-child{
            border-bottom:1px solid #dcdcdc;
        }
    </style>
</head>

<body style="background:#FFF;">
    <header class="header">
        <h2>我的宝珠</h2>
        <a id="fanhui" class="cefenlei" onclick="history.go(-1)" href="javascript:;">
            <img width="30" height="30" src="images/fanhui.png">
        </a>
        <div class="head-r"><a href="javascript:;" class="fr_Xieyi" onclick="showDiv()"></a></div>
    </header>
    <div class="topCon">
            <i></i><span id="num">0</span>颗
    </div>
    <p class="tips">宝珠可用来获得以下商品<span>已兑：<i>0</i></span></p>
    <ul class="product">
        <li>
            <div class="imgBox">
                <img src="/images/baozhu1.jpg" />
            </div>
            <div class="info">
                <p>所需宝珠：<span>160</span></p>
                <p>每日10点更新 限量1个</p>
                <strong>立即获得</strong>
            </div>
        </li>
        <li>
            <div class="imgBox">
                <img src="/images/baozhu2.jpg" />
            </div>
            <div class="info">
                <p>所需宝珠：<span>120</span></p>
                <p>每日10点更新 限量1个</p>
                <strong>立即获得</strong>
            </div>
        </li>
        <li>
            <div class="imgBox">
                <img src="/images/baozhu3.jpg" />
            </div>
            <div class="info">
                <p>所需宝珠：<span>100</span></p>
                <p>每日10点更新 限量1个</p>
                <strong>立即获得</strong>
            </div>
        </li>
    </ul>
    <div class="mask"></div>
    <div class="dialog">
        <ul>
            <li><a href="xieyi1.aspx">《爽乐购众筹平台支持者协议》</a></li>
            <li><a href="xieyi2.aspx">《爽乐购隐私政策》</a></li>
        </ul>
    </div>
    <script>
        $(function () {
            getNum();
            FastClick.attach(document.body);

            $('strong').each(function () {
                $(this).bind('click', function () {
                    alert('获得功能暂未开启');
                })
            })

            $('.dialog,.mask').bind('click', function () {
                $('.dialog,.mask').hide();
            })
        })

        function showDiv() {
            $('.dialog,.mask').show();
        }

        function getNum() {
            var ajaxUrl = "http://" + window.location.host + "/ilerrbuy?action=usergathermoney";
            $.ajax({
                type: 'get', dataType: 'json', timeout: 10000,
                url: ajaxUrl,
                success: function (e) {
                    if (e.state == 0) {
                        $(".topCon #num").text(e.money);
                    }
                    else { $(".topCon #num").text("0"); }
                }
            });
        }

        //禁用右上角微信菜单
        document.addEventListener('WeixinJSBridgeReady', function onBridgeReady() {
            WeixinJSBridge.call('hideOptionMenu');
        });
    </script>
</body>
</html>