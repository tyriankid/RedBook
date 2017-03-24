<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="new-proDetail.aspx.cs" Inherits="RedBook.new_proDetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta charset="UTF-8">
		<meta name="viewport" content="initial-scale=1.0, maximum-scale=1.0, user-scalable=no" />
		<title></title>
		<link rel="stylesheet" href="css/swiper.min.css" />
		<link rel="stylesheet" href="css/new-proDetail.css" />
		<style>
			/*字体图标*/
			
			@font-face {
				font-family: 'iconfont';
				src: url('iconfont/iconfont.eot');
				/* IE9*/
				src: url('iconfont/iconfont.eot?#iefix') format('embedded-opentype'), /* IE6-IE8 */
				url('iconfont/iconfont.woff') format('woff'), /* chrome、firefox */
				url('iconfont/iconfont.ttf') format('truetype'), /* chrome、firefox、opera、Safari, Android, iOS 4.2+*/
				url('iconfont/iconfont.svg#iconfont') format('svg');
				/* iOS 4.1- */
			}
			
			.iconfont {
				font-family: "iconfont" !important;
				font-size: 16px;
				font-style: normal;
				-webkit-font-smoothing: antialiased;
				-webkit-text-stroke-width: 0.2px;
				-moz-osx-font-smoothing: grayscale;
				margin-right: 0.1rem;
			}
			/*图片滑动插件*/
			body {
				font-family: Helvetica Neue, Helvetica, Arial, sans-serif;
				font-size: 14px;
				color: #000;
				margin: 0;
				padding: 0;
			}
			
			.swiper-container {
				width: 500px;
				height: 300px;
			}
			
			.swiper-slide {
				text-align: center;
				font-size: 18px;
				background: #fff;
				/* Center slide text vertically */
				display: -webkit-box;
				display: -ms-flexbox;
				display: -webkit-flex;
				display: flex;
				-webkit-box-pack: center;
				-ms-flex-pack: center;
				-webkit-justify-content: center;
				justify-content: center;
				-webkit-box-align: center;
				-ms-flex-align: center;
				-webkit-align-items: center;
				align-items: center;
			}
		</style>
	</head>

	<body>
		<div class="tt">
			<!--顶部-->
			<div class="top">
				<a href="javascript:history.back()" id="arrow"><span><</span>返回</a>
				<p>商品详情</p>
			</div>
			<!--商品详情-->
			<div class="details">
				<div class="swiper-container">
					<div class="swiper-wrapper">
						<div class="swiper-slide details_img"><img src="images/img/1.png" /></div>
						<div class="swiper-slide details_img"><img src="images/img/2.png" /></div>
						<div class="swiper-slide details_img"><img src="images/img/3.png" /></div>
						<div class="swiper-slide details_img"><img src="images/img/1.png" /></div>
					</div>
				</div>
				<div class="details_bottom">
					<p>化妆品套装<span>累计销量： 23231件</span></p>
					<p>这里是商品介绍介绍介绍</p>
				</div>
			</div>
			<!--组团-->
			<div class="zutuan">
				<p class="zutuan_p"><span>|</span>已有开团</p>
				<div class="zutuan_d">
					<img src="img/6.png" />
					<span class="zutuan_sp"><b>￥38.6</b>仅差一人成团</span>
					<del class="zutuan_de"><span>剩余</span>02天12是12分1秒<span>结束</span></del>
					<a class="zutuan_a">
						<del>立即参团</del>
						<span>50人团</span>
					</a>
				</div>
				<div class="zutuan_d">
					<img src="img/6.png" />
					<span class="zutuan_sp"><b>￥58.6</b>仅差一人成团</span>
					<del class="zutuan_de"><span>剩余</span>02天12是12分1秒<span>结束</span></del>
					<a class="zutuan_a">
						<del>立即参团</del>
						<span>5人团</span>
					</a>
				</div>
			</div>
			<!--评论-->
			<div class="comments">
				<p class="comments_p"><span>|</span>商品评论</p>
				<ul>
					<li>
						<a href="#" class=""></a>
						<p class="comments_p1">评论用户名称<span>10.02</span></p>
						<p class="comments_p2">评论评论评论评论</p>
						<a href="javascript:;" class="a1 cor"><i class="iconfont">&#xe66d;</i>点赞</a>
						<a href="#" class="a2"><i class="iconfont">&#xe696;</i>回复</a>
					</li>
					<li>
						<a href="#"></a>
						<p class="comments_p1">评论用户名称<span>10.02</span></p>
						<p class="comments_p2">评论评论评论评论</p>
						<a href="#" class="a1 cor"><i class="iconfont">&#xe66d;</i>点赞</a>
						<a href="#" class="a2"><i class="iconfont">&#xe696;</i>回复</a>
					</li>
				</ul>
				<p class="comments_bot">查看全部30条评论</p>
			</div>
			<!--类似商品-->
			<div class="guild">
				<p class="guild_p1">类似商品
					<a href="#">更多推荐<span>>></span></a>
				</p>
				<ul>
					<li>
						<img src="images/img/6.png" />
						<p>商品名称</p>
						<p>￥58<span class="guild_sp"> ￥200</span></p>
					</li>
					<li>
						<img src="images/img/6.png" />
						<p>商品名称</p>
						<p>￥58<span class="guild_sp"> ￥200</span></p>
					</li>
					<li>
						<img src="images/img/6.png" />
						<p>商品名称</p>
						<p>￥58<span class="guild_sp"> ￥200</span></p>
					</li>
					<li>
						<img src="images/img/6.png" />
						<p>商品名称</p>
						<p>￥58<span class="guild_sp"> ￥200</span></p>
					</li>
				</ul>
			</div>
			<!--底部-->
			<div class="bottom">
				<ul>
					<li class="">
						<i class="iconfont">&#xe679;</i>
						<span>分享</span>
					</li>
					<li class="bottom_i">
						<i class="iconfont">&#xe611;</i>
						<span class="bottom_sp">收藏</span>
					</li>
				</ul>
				<div>
					<a href="javascript:;"><span>￥88.6</span>单独购买</a>
					<a href="javascript:;">去开团</a>
				</div>
			</div>
		</div>
	</body>
	<script src="js/jquery-1.8.3.min.js"></script>
	<script src="js/swiper.min.js"></script>
	<script src="js/new-index.js"></script>
	<script>
	    //		滑动插件
	    var swiper = new Swiper('.swiper-container');

	    //高度使用rem
	    var ss = document.documentElement.clientHeight;
	    var ss1 = ss / 20;
	    document.documentElement.style.fontSize = ss1 + "px";
	</script>
</html>
