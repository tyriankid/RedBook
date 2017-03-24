<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="new-fenlei.aspx.cs" Inherits="RedBook.new_fenlei" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
		<title></title>
		<meta  charset="UTF-8" name="viewport" content="initial-scale=1.0, maximum-scale=1.0, user-scalable=no" />
		<link rel="stylesheet" href="css/new-fenlei.css" />
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
			}
		</style>
	</head>

	<body>
		<div class="tt">
			<!--顶部-->
			<div class="top">
				<a href="javascript:history.back()" id="refundd"><span><</span>返回</a>
				<p>电商商城</p>
			</div>
			<!--分类-->
			<div class="fenlei">
				<ul class="fenlei_u1">
					<li>
						<a href="xiangqing.html"><img src="images/img/11.png" /></a>
						<div class="fenlei_dd">
							<h4>名称名称</h4>
							<p class="fenlei_p2">介介绍介绍介绍介绍介绍介绍介绍介绍介绍绍</p>
							<p>
								<span></span>用户昵称
								<del class=""><i class="iconfont cor">&#xe66d;</i>12012</del>
							</p>
						</div>
					</li>
					<li>
						<a href="xiangqing1.html"><img src="images/img/4.png" /></a>
						<div class="fenlei_dd">
							<h4>名称名称</h4>
							<p class="fenlei_p2">介介绍介绍介绍介绍介绍介绍介绍介绍介绍绍</p>
							<p>
								<span></span>用户昵称
								<del class=""><i class="iconfont cor">&#xe66d;</i>12012</del>
							</p>
						</div>
					</li>
					<li>
						<a href="javascript:;"><img src="images/img/4.png" /></a>
						<div class="fenlei_dd">
							<h4>名称名称</h4>
							<p class="fenlei_p2">介介绍介绍介绍介绍介绍介绍介绍介绍介绍绍</p>
							<p>
								<span></span>用户昵称
								<del class=""><i class="iconfont cor">&#xe66d;</i>12012</del>
							</p>
						</div>
					</li>
					<li>
						<a href="javascript:;"><img src="images/img/4.png" /></a>
						<div class="fenlei_dd">
							<h4>名称名称</h4>
							<p class="fenlei_p2">介介绍介绍介绍介绍介绍介绍介绍介绍介绍绍</p>
							<p>
								<span></span>用户昵称
								<del class=""><i class="iconfont cor">&#xe66d;</i>12012</del>
							</p>
						</div>
					</li>
				</ul>
				<ul class="fenlei_u2">
					<li>
						<a href="xiangqing1.html"><img src="images/img/4.png" /></a>
						<div class="fenlei_dd">
							<h4>名称名称</h4>
							<p class="fenlei_p2">介介绍介绍介绍介绍介绍介绍介绍介绍介绍绍</p>
							<p>
								<span></span>用户昵称
								<del class=""><i class="iconfont cor">&#xe66d;</i>12012</del>
							</p>
						</div>
					</li>
					<li>
						<a href="javascript:;"><img src="images/img/4.png" /></a>
						<div class="fenlei_dd">
							<h4>名称名称</h4>
							<p class="fenlei_p2">介介绍介绍介绍介绍介绍介绍介绍介绍介绍绍</p>
							<p>
								<span></span>用户昵称
								<del class=""><i class="iconfont cor">&#xe66d;</i>12012</del>
							</p>
						</div>
					</li>
					<li>
						<a href="javascript:;"><img src="images/img/4.png" /></a>
						<div class="fenlei_dd">
							<h4>名称名称</h4>
							<p class="fenlei_p2">介介绍介绍介绍介绍介绍介绍介绍介绍介绍绍</p>
							<p>
								<span></span>用户昵称
								<del class=""><i class="iconfont cor">&#xe66d;</i>12012</del>
							</p>
						</div>
					</li>
					<li>
						<a href="javascript:;"><img src="images/img/4.png" /></a>
						<div class="fenlei_dd">
							<h4>名称名称</h4>
							<p class="fenlei_p2">介介绍介绍介绍介绍介绍介绍介绍介绍介绍绍</p>
							<p>
								<span></span>用户昵称
								<del class=""><i class="iconfont cor">&#xe66d;</i>12012</del>
							</p>
						</div>
					</li>
				</ul>
			</div>

			<!--底部-->
			<div class="bottom">
				<ul id="bb">
					<li>
						<a href="index.aspx">
							<i class="iconfont">&#xe717;</i> 最热门
						</a>
					</li>
					<li>
						<a href="javascript:;" class="cr">
							<i class="iconfont">&#xe600;</i> 大厅
						</a>
					</li>
					<li>
						<a href="new-wo.aspx">
							<i class="iconfont">&#xe601;</i> 我的
						</a>
					</li>
				</ul>
			</div>
		</div>
	</body>
	<script src="js/jquery-1.8.3.min.js"></script>
	<script src="js/new-index.js"></script>
	<script>
	    //高度使用rem
	    var ss = document.documentElement.clientHeight;
	    var ss1 = ss / 20;
	    document.documentElement.style.fontSize = ss1 + "px";
	    //返回上一步
	    //		var back = document.getElementById("arrow");
	    //			back.onclick=function(){
	    //					window.history.go(-1);
	    //			}
	</script>
</html>
