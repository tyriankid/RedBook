<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HomePage.aspx.cs" Inherits="RedBookPlatform.HomePage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
	<title>爽乐购后台管理系统</title>
	<meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
	
	<link rel="stylesheet" type="text/css" href="/Resources/iconfont/iconfont.css"/>
    <link rel="stylesheet" type="text/css" href="/Resources/css/comm.css"/>
	<link rel="stylesheet" type="text/css" href="/Resources/css/index.css"/>
	
	<script src="/Resources/js/jquery.min.js" type="text/javascript" charset="utf-8"></script>
	<script src="/Resources/js/home.js" type="text/javascript" charset="utf-8"></script>
    <script src="/Resources/js/comm.js" type="text/javascript" charset="utf-8"></script>
</head>
<body>
    <form id="form1" runat="server">
<body>
	<div id="top">
		<div class="top-fixed minwidth">
			<a href="#" class="logo"><img src="/Resources/images/logo.png" alt="logo"></a>
			<ul class="top-menu">
				<li class="active"><a href="javascript:;"><i class="iconfont icon-bangongxitong"></i>代理平台</a></li>
			</ul>
			<ul class="top-menu admin">
				<li><a href="javascript:;"><i class="iconfont icon-guanliyuan"></i><%=this.CurrLoginChannelUser.Username %>[城市服务商]</a></li>
				<li><a href="HomePage.aspx?out=true"><i class="iconfont icon-tuichu-copy"></i>退出</a></li>
			</ul>
			
		</div>
	</div>
	<div class="content">
		<div class="left-bar">
			<div class="left-top">
				<a href="javascript:;" class="fold"><i class="iconfont icon-fenlei"></i></a>
			</div>
			<ul>
				
			</ul>
		</div>
		<div class="right-bar">
			<iframe id="rightIframe" src="channel/baseInformation.aspx" frameborder="0"></iframe>
		</div>
	</div>
	<script type="text/javascript">
	    $(function () {
	        //默认加载第一页
	        leftbar(0);
	        setIfremeSize();

	        $(".top-menu>li").click(function () {
	            $(this).addClass("active");
	            $(this).siblings().removeClass("active");
	            leftbar($(this).index());
	        })
	    })

	</script>
</body>
    </form>
</body>
</html>
