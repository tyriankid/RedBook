<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="RedBookPlatform.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
	<title>爽乐购后台管理系统</title>
	<meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1"/>
	
	<link rel="stylesheet" type="text/css" href="/Resources/iconfont/iconfont.css"/>
    <link rel="stylesheet" type="text/css" href="/Resources/css/comm.css"/>
	<link rel="stylesheet" type="text/css" href="/Resources/css/index.css"/>
	
	<script src="/Resources/js/jquery.min.js" type="text/javascript" charset="utf-8"></script>
	<script src="/Resources/js/index.js?v=2" type="text/javascript" charset="utf-8"></script>
    <script src="/Resources/js/comm.js" type="text/javascript" charset="utf-8"></script>

</head>
    <style>
    body{
      margin:0;
    }

    /*字体图标*/
    @font-face {
  font-family: 'iconfont';
  src: url('/Resources/tubiao/iconfont.eot');
  src: url('/Resources/tubiao/iconfont.eot?#iefix') format('embedded-opentype'),
  url('/Resources/tubiao/iconfont.woff') format('woff'),
  url('/Resources/tubiao/iconfont.ttf') format('truetype'),
  url('/Resources/tubiao/iconfont.svg#iconfont') format('svg');
}
    .iconfont{
  font-family:"iconfont" !important;
  font-size:16px;font-style:normal;
  -webkit-font-smoothing: antialiased;
  -webkit-text-stroke-width: 0.2px;
  -moz-osx-font-smoothing: grayscale;
}
</style>
<body>
    <form id="form1" runat="server">

	<div id="top">
		<div class="top-fixed minwidth">
			<a href="#" class="logo"><img src="/Resources/images/logo.png" alt="logo"/></a>
			<ul id="topmenu" class="top-menu">
				<li class="active"><a href="javascript:;"><i class="iconfont icon-bangongxitong"></i>系统设置</a></li>
				<li><a href="javascript:;"><i class="iconfont icon-neirongguanli"></i>内容管理</a></li>
				<li><a href="javascript:;"><i class="iconfont icon-shangpinguanli"></i>商品管理</a></li>
				<li><a href="javascript:;"><i class="iconfont icon-tubiao30"></i>订单管理</a></li>
				<li><a href="javascript:;"><i class="iconfont icon-yingxiaohuodong"></i>运营活动</a></li>
				<li><a href="javascript:;"><i class="iconfont icon-qudao"></i>渠道管理</a></li>
                <li><a href="javascript:;"><i class="iconfont icon-qudao"></i>统计分析</a></li>
                <li><a href="javascript:;"><i class="iconfont">&#xe60c;</i>小红书管理</a></li>
			</ul>
			<ul class="top-menu admin">
				<li><a href="javascript:;"><i class="iconfont icon-guanliyuan"></i><%=this.CurrLoginAdmin.Username %>[超级管理员]</a></li>
				<li><a href="index.aspx?out=true"><i class="iconfont icon-tuichu-copy"></i>退出</a></li>
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
			<iframe id="rightIframe" src="seo.html" frameborder="0"></iframe>
		</div>
	</div>
    </form>
</body>
	<script type="text/javascript">
	    var nodes;
	    $(function () {
	        //默认加载第一页
	        setIfremeSize();

	        $(".top-menu>li").click(function () {
	            $(this).addClass("active");
	            $(this).siblings().removeClass("active");
	            leftbar($(this).index());
	        })
            
	        $.ajax({
	            type: "POST",
	            contentType: "application/json", //WebService 会返回Json类型
	            url: "index.aspx/GetSelect", //调用WebService的地址和方法名称组合 ---- WsURL/方法名
	            async: false, //同步调用(如果是异步,则会慢一拍)
	            success: function (result) {     //回调函数，result，返回值
	                if (result.d != "") {
	                    nodes = JSON.parse(result.d)
	                    var li = $("#topmenu a")
	                    for (var k = 0; k < li.length; k++) {

	                        var flag = false;
	                        if (nodes!=null)
	                        {
	                            for (var i = 0; i < nodes.length; i++) {
	                                if (nodes[i].id.length == 2) {
	                                    var str = $(li[k]).html();
	                                    var str1 = nodes[i].name;
	                                    if (str.indexOf(str1) != -1) {
	                                        flag = true;
	                                    }
	                                }
	                            }
	                        }
	                        if (!flag) {
	                            $(li[k]).parent().css("display", "none")
	                        }
	                    }
	                }
	            }

	        });
	  
	        for (var b = 0; b < $(".top-menu>li").length; b++) {
	            leftbar(b) //首次进来加载
	            $('.left-bar span').eq(0).click();
	            if ($(".left-bar ul").html().trim() != "")//如果有权限就会有栏目 有栏目就会有内容
	            {
	                $(".top-menu>li").eq(b).addClass("active");
	                break;
	            }
	        }

	       


	    })

	</script>


</html>
