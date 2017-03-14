<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.calResult.aspx.cs" Inherits="RedBook.index_calResult" %>

<!DOCTYPE html>
<html>
<head>
    <meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, user-scalable=no, maximum-scale=1.0" />
    <title>计算结果
    </title>
    <meta content="app-id=518966501" name="apple-itunes-app" />
    <meta content="yes" name="apple-mobile-web-app-capable" />
    <meta content="black" name="apple-mobile-web-app-status-bar-style" />
    <meta content="telephone=no" name="format-detection" />
    <link href="css/comm.css" rel="stylesheet" type="text/css" />
    <link href="css/lottery.css" rel="stylesheet" type="text/css" />
    <script src="js/jquery190.js" language="javascript" type="text/javascript"></script>
    <script src="/js/fastclick.js"></script>
    <script id="pageJS" data="js/CalResult.js" language="javascript" type="text/javascript"></script>
</head>

<body>
    <header class="header">
        <h2>计算结果</h2>
        <a id="fanhui" class="cefenlei" onclick="history.go(-1)" href="javascript:;">
            <img width="30" height="30" src="images/fanhui.png">
        </a>
        <div class="head-r"><a href="/index.aspx" class="fr_Home"></a></div>
    </header>
    <div class="infoCount">
        <div class="infoCount2">
            <ul>
            </ul>
        </div>
        <div class="infoCount3">
            <p class="title">数值A</p>
    	    <p>= 商品的最后一个号码分配完毕时间点前本站全部商品的最后100个参与时间之和（包含该商品的最后一人次的参与时间）</p>
    	    <p>截至该商品最后参与时间【<span id="jxtime"></span>】网站所有商品的最后100条参与时间记录：</p>
        </div>
    </div>
    <section id="calResult" class="z-minheight">
        <div class="infoResult">
            <ul class="result2">
                <li class="iTitle"><span>购买时间</span><span>转换数据</span><span>会员账号</span></li>
            </ul>
        </div>
        
    </section>
</body>
<script>
    //首次加载
    window.onload = function () {
        FastClick.attach(document.body);

        getcalResult();
    }
    //页面传参
    function getUrlParam(name) {
        var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)"); //构造一个含有目标参数的正则表达式对象
        var r = window.location.search.substr(1).match(reg);  //匹配目标参数
        if (r != null) return unescape(r[2]); return null; //返回参数值
    }

    //获取计算结果详情
    function getcalResult() {
        var ajaxUrl = "http://" + window.location.host + "/ilerrshop?action=algorithmwon&shopid=" + getUrlParam("shopid");
        //ajaxUrl = "http://localhost:8095/ilerrshop?action=algorithmwon&shopid=10";
        var calResultList = "",
            JieXiaoResult = "";
        $.ajax({
            type: 'get', dataType: 'json', timeout: 10000,
            url: ajaxUrl,
            success: function (e) {
                $("#jxtime").html(e.jiexiaotime);
                $('.time').text()
                JieXiaoResult += '<li><span>幸运号码</span></li>' +
                        '<li>= <span>数值A</span>%<span>商品所需人次数</span>（取余） + 1000001</li>' +
                        '<li>= <span>' + e.qiuhe + '</span>%<span>' + e.zongrenshu + '</span>（取余） + 1000001</li>' +
                        '<li>= <span>' + e.y + '</span></li>';
                $(".infoCount2 ul").append(JieXiaoResult);
                //购买人数列表
                for (var i = 0; i < e.data.length; i++) {
                    calResultList += '<li><span>' + e.data[i].time + '</span><span>' + e.data[i].changedata + '</span><span>' + e.data[i].username + '</span>' +
                            '<p></p>' +
                        '</li>';
                }
                $(".iTitle").after(calResultList);
            }
        })
    }
</script>
</html>
