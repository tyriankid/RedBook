<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.goodsdesc.aspx.cs" Inherits="RedBook.index_goodsdesc" %>
<html>

	<head>
		<meta property="qc:admins" content="7545416270657117166375777">
		<!--<base href="/">-->
		<meta charset="utf-8">
		<meta http-equiv="x-ua-compatible" content="ie=edge">
		<meta name="renderer" content="webkit">
		<title>爽乐购</title>
		<meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no">
		<link href="css/comm.css" rel="stylesheet" type="text/css" />
		<link href="css/goods.css" rel="stylesheet" type="text/css" />
		<script src="js/jquery190.js" type="text/javascript"></script>
        <script src="/js/fastclick.js"></script>
	</head>

	<body class="buyContentBody">
    <header class="header">
        <h2>商品详情</h2>
        <a class="cefenlei" onclick="window.location.href=document.referrer;" href="javascript:;">
            <img width="30" height="30" src="images/fanhui.png">
        </a>
        <div class="head-r"><a href="/index.aspx" class="fr_Home"></a></div>
    </header>

		<!-- 详情内容 Start -->
		<section id="buyContentBox">
			<div id="buyContent">
			</div>
		</section>
		<!-- 详情内容 End -->
		<script type="text/javascript">
		    //打开页面加载数据
		    window.onload = function () {
		        FastClick.attach(document.body);

		        //loaded();
		        getGoodsInfo();
		        //getBuyRecord();
		    }
            //页面传参
		    function getUrlParam(name) {
		        var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)"); //构造一个含有目标参数的正则表达式对象
		        var r = window.location.search.substr(1).match(reg);  //匹配目标参数
		        if (r != null) return unescape(r[2]); return null; //返回参数值
		    }
		    //获取商品详情
		    function getGoodsInfo() {
		        var ajaxUrl = "http://" + window.location.host + "/ilerrshop?action=detail&shopid=" + getUrlParam("shopid");
		        $.ajax({
		            type: 'get', dataType: 'json', timeout: 10000,
		            url: ajaxUrl,
		            success: function (data) {
		                $("#buyContent").append(data.Contents);
		            }
		        })
		    }

		</script>
	</body>

</html>