<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="new-xiangqing.aspx.cs" Inherits="RedBook.new_xiangqing" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta charset="utf-8">
		<meta name="viewport" content="initial-scale=1.0, maximum-scale=1.0, user-scalable=no" />
		<title></title>
		<link rel="stylesheet" href="css/swiper.min.css" />
		<link rel="stylesheet" href="css/new-xiangqing.css" />
		<style>
			@font-face {
				font-family: 'iconfont';
				src: url('tb/iconfont.eot');
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
			
			/*滑动插件*/
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
				<a href="javascript:history.back();" id="arrow"><span><</span>返回</a>
				<p>文章详情</p>
			</div>
			<!--商品详情-->
			<div class="banner">
				<div class="swiper-container">
					<div class="swiper-wrapper">
						<div class="swiper-slide banner_img"><img src="images/img/1.png" /></div>
						<div class="swiper-slide banner_img"><img src="images/img/2.png" /></div>
						<div class="swiper-slide banner_img"><img src="images/img/3.png" /></div>
						<div class="swiper-slide banner_img"><img src="images/img/1.png" /></div>
					</div>
				</div>

				<!--<div class="banner_img">
			<img src="img/1.png"/>
		</div>-->
				<div class="banner_bottom">
					<a href="#"></a>
					<p>用户昵称</p>
					<p>2017/12/01 <span> 09:00</span></p>
					<a href="#"><span>+</span> 关注</a>
				</div>
			</div>
			<!--内容区-->
			<div class="summary">
				<!--内容1-->
				<p class="summary_p1"><b>标题标题标题标题标题标题标题标题标题标题标题标题标题标题</b></p>
				<p class="summary_p2">01</p>
				<div class="sanjiao"></div>
				<p class="summary_p3">
					前期所有的导购文章由后台人员编辑上传。需要在PC端进行操作。后期将开放一般会员编辑导购文章的功能。为保障文章质量，所有一般会员编辑的导购文章后台需审核通过后，才能在前端显示。前期不用会员编辑文章，此次用实现会员编辑的功能。
				</p>
				<!--内容2-->
				<p class="summary_p2">02</p>
				<div class="sanjiao"></div>
				<p class="summary_p3">
					前期所有的导购文章由后台人员编辑上传。需要在PC端进行操作。后期将开放一般会员编辑导购文章的功能。为保障文章质量，所有一般会员编辑的导购文章后台需审核通过后，才能在前端显示。前期不用会员编辑文章，此次用实现会员编辑的功能。
				</p>
				<div class="summary_bottom">
					<img src="img/2.png" />
				</div>
			</div>
			<!--评论-->
			<div class="comments">
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
						<a href="javascript:;" class="a1 cor"><i class="iconfont">&#xe66d;</i>点赞</a>
						<a href="#" class="a2"><i class="iconfont">&#xe696;</i>回复</a>
					</li>
				</ul>
				<p class="comments_bot">查看全部30条评论</p>
			</div>
			<!--选购指南-->
			<div class="guild" id="guild">
				<p class="guild_p1">选购指南
					<a href="javascript:;">更多推荐<span>>></span></a>
				</p>
				<ul id="prolistu">
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
			<!--推荐-->
			<div class="list">
				<p class="list_p1">相似文章
					<a href="javascript:;">更多好文<span>>></span></a>
				</p>
				<ul class="list_u1">
					<li>
						<a href="javascript:;"><img src="images/img/4.png" /></a>
						<div class="list_dd">
							<h4>名称名称</h4>
							<p class="list_p2">介介绍介绍介绍介绍介绍介绍介绍介绍介绍绍</p>
							<p>
								<span></span>用户昵称
								<del class=""><i class="iconfont cor">&#xe66d;</i>12012</del>
							</p>
						</div>
					</li>
					<li>
						<a href="javascript:;"><img src="images/img/5.png" /></a>
						<div class="list_dd">
							<h4>名称名称</h4>
							<p class="list_p2">介介绍介绍介绍介绍介绍介绍介绍介绍介绍绍</p>
							<p>
								<span></span>用户昵称
								<del class=""><i class="iconfont cor">&#xe66d;</i>12012</del>
							</p>
						</div>
					</li>
				</ul>
				<ul class="list_u1">
					<li>
						<a href="javascript:;"><img src="images/img/3.png" /></a>
						<div class="list_dd">
							<h4>名称名称</h4>
							<p class="list_p2">介介绍介绍介绍介绍介绍介绍介绍介绍介绍绍</p>
							<p>
								<span></span>用户昵称
								<del class=""><i class="iconfont cor">&#xe66d;</i>12012</del>
							</p>
						</div>
					</li>
					<li>
						<a href="javascript:;"><img src="images/img/3.png" /></a>
						<div class="list_dd">
							<h4>名称名称</h4>
							<p class="list_p2">介介绍介绍介绍介绍介绍介绍介绍介绍介绍绍</p>
							<p>
								<span></span>用户昵称
								<del class=""><i class="iconfont cor">&#xe66d;</i>12012</del>
							</p>
						</div>
					</li>
				</ul>
			</div>
			<!--底部-->
			<div class="dibu">
				<ul>
					<li>
						<a href="javascript:;" class="dibu_a">
							<i class="iconfont">&#xe66d;</i>
							<p>点赞·<span>26</span></p>
						</a>
					</li>
					<li>
						<a href="javascript:;">
							<i class="iconfont">&#xe696;</i>
							<p>评论·<span>0</span></p>
						</a>
					</li>
					<li>
						<a href="javascript:;">
							<i class="iconfont">&#xe611;</i>
							<p>收藏·<span>0</span></p>
						</a>
					</li>
				</ul>
			</div>
		</div>
	</body>

	<script src="js/jquery-1.8.3.min.js"></script>
	<script src="js/swiper.min.js"></script>
	<script src="js/new-index.js"></script>
	<script>
	    var swiper = new Swiper('.swiper-container');

	    //高度使用rem
	    var ss = document.documentElement.clientHeight;
	    var ss1 = ss / 20;
	    document.documentElement.style.fontSize = ss1 + "px";

	</script>
</html>
