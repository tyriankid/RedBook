<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="user.buyDetail.aspx.cs" Inherits="RedBook.user_buyDetail" %>
<!DOCTYPE html>
<html>

	<head>
		<meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
		<title>参与详情</title>
		<meta content="app-id=518966501" name="apple-itunes-app" />
		<meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, user-scalable=no, maximum-scale=1.0" />
		<meta content="yes" name="apple-mobile-web-app-capable" />
		<meta content="black" name="apple-mobile-web-app-status-bar-style" />
		<meta content="telephone=no" name="format-detection" />
		<link href="css/comm.css" rel="stylesheet" type="text/css" />
		<link href="css/single.css" rel="stylesheet" type="text/css" />
		<link rel="stylesheet" type="text/css" href="css/member.css" />
		<script src="js/jquery190.js" type="text/javascript"></script>
        <script src="/js/fastclick.js"></script>
	</head>

	<body>
    <header class="header">
        <h2>参与详情</h2>
        <a class="cefenlei" onclick="history.go(-1)" href="javascript:;">
            <img width="30" height="30" src="images/fanhui.png">
        </a>
        <div class="head-r"><a href="/index.aspx" class="fr_Home"></a></div>
    </header>

		<section class="clearfix">
			<section class="clearfix g-member g-Record-ctlst">
                <div id="parContent">
	                <div class="par-title">
		                <div id="sticky-wrapper" class="sticky-wrapper" style="height: 39px;">
			                <div class="par-options"><span class="par-time">参与时间</span><span class="par-count">参与人次</span><span class="par-number">参与号码</span></div>
		                </div>
	                </div>
                    
	                
                </div>
			</section>
		</section>
		<script type="text/javascript">
		    //页面传参
		    var yid;
		    var otherid;
		    function getUrlParam(name) {
		        var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)"); //构造一个含有目标参数的正则表达式对象
		        var r = window.location.search.substr(1).match(reg);  //匹配目标参数
		        if (r != null) return unescape(r[2]); return null; //返回参数值
		    }

		    //打开页面加载数据
		    window.onload = function () {
		        FastClick.attach(document.body);

		        getDrawCode();
		        yid = getUrlParam("yid");
		        otherid = getUrlParam("otherid");
		    };

            //获取所有抽奖码
		    function getDrawCode() {
		        var ajaxUrl = "http://" + window.location.host + "/ilerrorder?action=partakewin&shopid=" + getUrlParam("yid") + "&otherid=" + getUrlParam("otherid"),
		            GoodsInfo = "",
                    parList = "";
		        $.ajax({
		            type: 'get', dataType: 'json', timeout: 10000,
		            url: ajaxUrl,
		            success: function (e) {
                        //商品详情
		                GoodsInfo += '<section class="clearfix g-Record-ct">' +
				            '<a class="fl z-Limg" href="/index.item.aspx?shopid=' + yid + '"><img src="' + e.imgroot + e.thumb + '" border="0"></a>' +
				            '<div class="u-Rcd-r gray9">' +
					            '<p class="z-Rcd-tt">' +
						            '<a href="/index.item.aspx?shopid=' + yid + '" class="gray6">' + e.title + '</a>' +
					            '</p>' +
					            '<p class="qihao">期号：' + e.qishu + '</p>' +
					            '<p>获得者：<a class="blue" href="/index.userindex.aspx?uid=' + e.q_uid + '">' + e.username + '</a></p>' +
					            '<p>揭晓时间：<em class="gray6">' + e.q_end_time + '</em></p>' +
				            '</div>' +
			            '</section>';
		                //if (e.productid != "")
		                    $(".g-Record-ctlst").before(GoodsInfo);
                        //爽乐购码列表
		                for (var i = 0; i < e.data.length; i++) {
		                    var goucode = e.data[i].goucode.split(","),
		                        goucodeList = "";
		                    for (var j = 0; j < goucode.length; j++) {
		                        goucodeList += '<span class="num-item">' + goucode[j] + '</span>';
		                    }
		                    parList += '<div class="par-item" data-page="2" data-maxpage="1">'+
		                        '<div id="" class="sticky-wrapper" style="height: 39px;">'+
			                        '<div class="par-options"><span class="par-time">' + e.data[i].time + '</span><span class="par-count">' + e.data[i].quantity + '</span><span class="par-number" onclick="parnumber(this)">展开</span></div>' +
		                        '</div>'+
		                        '<div class="par-detail">'+
			                        '<div class="num-list">' + goucodeList + '</div>' +
		                        '</div>' +
		                    '</div>';
		                }
		                $(".g-Record-ctlst .par-title").after(parList);
		            }
		           , error: function (xhr, type) {
		           }
		        })
		    }

		    //收起展开爽乐购码
		    function parnumber(t) {
		        $(t).toggleClass("par-look");
		        if ($(t).hasClass("par-look")) {
		            $(t).text("收起");
		            $(t).parent().parent().next(".par-detail").show();
		        } else {
		            $(t).text("展开");
		            $(t).parent().parent().next(".par-detail").hide();
		        }
		    }
		</script>
	</body>

</html>