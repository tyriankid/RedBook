<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="new-wo.aspx.cs" Inherits="RedBook.new_wo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
		<title></title>
		<meta http-equiv="Content-Type" charset="UTF-8" name="viewport" content="initial-scale=1.0, maximum-scale=1.0, user-scalable=no" />
		<link rel="stylesheet" href="css/new-wo.css" />
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
				<a href="javascript:history.back()" id="wo"><span><</span>返回</a>
				<p>我</p>
			</div>
			<!--我的信息-->
			<div class="xinxi">
				<div class="xinxi_left">
					<h6>用户昵称</h6>
					<p>简介</p>
					<table class="tab">
						<tr>
							<td>0</td>
							<td>0</td>
							<td>0</td>
							<td>45641</td>
						</tr>
						<tr>
							<td>粉丝</td>
							<td>被赞</td>
							<td>被收藏</td>
							<td>收获积分</td>
						</tr>
					</table>
				</div>
				<div class="xinxi_right">
					<span></span>
					<a href="#">编辑个人资料</a>
				</div>
				<div class="xinxi_bottom">
					<ul>
						<li>
							<a href="#">
								<i class="iconfont ">&#xe669;</i>我的关注
							</a>
						</li>
						<li>
							<a href="#">
								<i class="iconfont ">&#xe611;</i>我的收藏
							</a>
						</li>
						<li>
							<a href="#">
								<i class="iconfont ">&#xe600;</i>我的团购
							</a>
						</li>
						<li>
							<a href="#">
								<i class="iconfont ">&#xe62f;</i>浏览记录
							</a>
						</li>
					</ul>
				</div>
			</div>
			<!--文章-->
			<div class="content">
				<ul id="content">
					<li>
						<a href="#" class="bor">我的文章·0</a>
					</li>
					<li>
						<a href="#">我分享的文章·0</a>
					</li>
				</ul>
			</div>
			<!--底部-->
			<div class="bottom">
				<ul id="bb">
					<li>
						<a href="index.aspx">
							<i class="iconfont ">&#xe717;</i> 最热门
						</a>
					</li>
					<li>
						<a href="new-fenlei.aspx">
							<i class="iconfont">&#xe600;</i> 大厅
						</a>
					</li>
					<li>
						<a href="javascript:;" class="cr">
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
	</script>
</html>
