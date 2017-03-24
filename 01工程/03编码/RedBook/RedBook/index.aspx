<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="RedBook.index1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
		<title></title>
		<meta charset="UTF-8" name="viewport" content="initial-scale=1.0, maximum-scale=1.0, user-scalable=no" />
		<link rel="stylesheet" href="css/new-index.css" />
        <script src="js/vue.min.js"></script>
        <script src="js/vue-resource.min.js"></script>
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
        <div id="indexBox">
		    <div class="tt">
			    <!--顶部-->
			    <div class="top">
				    <a href="javascript:history.back()" id="refundd"><span><</span>返回</a>
				    <p>电商商城</p>
			    </div>
			    <!--导购-->
			    <div class="shoppers">
				    <ul>
					    <li v-for="val in showData">
                            <div class="art-title">
                                <img src="../images/img/6.png" alt="" class="titleImg" />
                                <p class="mTitle">{{val.MainTitle}}</p>
                                <p class="subTitle">{{val.SubTitle}}</p>
                            </div>
						    <div class="shoppers_div">
							    <del><span><img src="../images/img/6.png"/></span>{{val.usename}}</del>
                                <a href="javascript:;"><i class="iconfont">&#xe618;</i><span>{{val.WatchCount}}</span></a>
							    <a class="dibu_a" href="javascript:;"><i class="iconfont" id="{{val.rid}}" >&#xe66d;</i><span>{{val.FabulousCount}}</span></a>
						    </div>
					    </li>
				    </ul>
			    </div>
			    <!--底部-->
			    <div class="bottom">
				    <ul id="cc">
					    <li>
						    <a href="javascript:;" class="cr">
							    <i class="iconfont ">&#xe717;</i> 最热门
						    </a>
					    </li>
					    <li>
						    <a href="new-fenlei.aspx">
							    <i class="iconfont">&#xe600;</i> 大厅
						    </a>
					    </li>
					    <li>
						    <a href="new-wo.aspx">
							    <i class="iconfont ">&#xe601;</i> 我的
						    </a>
					    </li>
				    </ul>
			    </div>
		    </div>
        </div>
	</body>
	<script src="js/jquery-1.8.3.min.js"></script>
    <script src="js/new-index-more.js"></script>
	<script>
	    //高度使用rem
	    var ss = document.documentElement.clientHeight;
	    var ss1 = ss / 20;
	    document.documentElement.style.fontSize = ss1 + "px";
	</script>
</html>
