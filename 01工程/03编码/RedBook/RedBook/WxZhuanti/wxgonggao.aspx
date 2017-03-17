<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="wxgonggao.aspx.cs" Inherits="RedBook.WxZhuanti.wxgonggao" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="initial-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <title>公告页面</title>
        <meta content="app-id=518966501" name="apple-itunes-app" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, user-scalable=no, maximum-scale=1.0" />
    <meta content="yes" name="apple-mobile-web-app-capable" />
    <meta content="black" name="apple-mobile-web-app-status-bar-style" />
    <meta content="telephone=no" name="format-detection" />
    <meta name="msapplication-tap-highlight" content="no">
    <link href="css/wxgonggao.css" rel="stylesheet" />
    <script src="/js/jquery190.js" type="text/javascript"></script>
    <script src="/js/AlertBox.js?v=2.4"></script>
<%--    <script type="text/javascript">

        document.addEventListener('plusready', function () {
            //console.log("所有plus api都应该在此事件发生后调用，否则会出现plus is undefined。"

        });

    </script>--%>
</head>
<body>
    <form id="form1" runat="server">
    <!--top start-->
	<div id="top" >
	    <div class="toptxet" >
		乐公告
	    </div>
       <div class="sxdiv" style="margin-top: -8px;margin-right: 3%;">
	     <img id="sx" src="../images/win/sxbutton.png"  style="width: 6%;height: 30%; float: right;"/>
	    </div>
	</div>
         
	<!--top end-->
	<!--main start-->
	<div id="main">
		<div class="content">
		    <div id="today">
			    <span class="title">今日统计</span>
			    <div class="topcon">
			    	<div class="todayin">
			    	    <div class="today1">
			    		    <div class="todaytext ">
			    			    <span >
			    				新增会员
			    			    </span>
			    			    <em><asp:Label ID="member" CssClass="num" runat="server"></asp:Label>人</em>
			    		    </div>
			    	    </div>
			    	</div>
			    	<div class="todayin">
			    	    <div class="today1">
			    		    <div class="todaytext ">
			    			    <span >
			    				新增订单
			    			    </span>
			    			    <em><asp:Label ID="orders" CssClass="num" runat="server"></asp:Label></em>
			    		    </div>
			    	    </div>
			    	</div>
			    	<div class="todayin">
			    	    <div class="today1">
			    		    <div class="todaytext ">
			    			    <span >
			    				充值金额
			    			    </span>
			    			    <em><asp:Label ID="addmoney" CssClass="num" runat="server"></asp:Label>元</em>
			    		    </div>
			    	    </div>
			    	</div>
			    	<div class="todayin">
			    	    <div class="today1">
			    		    <div class="todaytext ">
			    			    <span >
			    				消费金额
			    			    </span>
			    			    <em><asp:Label ID="consumemoney" CssClass="num" runat="server"></asp:Label>元</em>
			    		    </div>
			    	    </div>
			    	</div>
			    	<div class="todayin">
			    	    <div class="today1">
			    		    <div class="todaytext ">
			    			    <span >
			    				开奖次数
			    			    </span>
			    			    <em><asp:Label ID="prizenum" CssClass="num" runat="server"></asp:Label>次</em>
			    		    </div>
			    	    </div>      
			    	</div>
			    	<div class="todayin">
			    	    <div class="today1">
			    		    <div class="todaytext ">
			    			    <span >
			    				总用户
			    			    </span>
			    			    <em><asp:Label ID="allmember"  CssClass="num" runat ="server"></asp:Label>人</em>
			    		    </div>
			    	    </div>
			    	</div>
			    </div>
		    </div>
		    <div id="update">
		    	<span class="title">更新公告</span>
		    	<div class="countcon">
		    		<div class="count1">
		    			<ul class="countlist">
                            <h4>2016-09-14</h4>
                            <li class="wbg clearfix">1、修改拼团的专题页面</li>
                            <li class="wbg">2、商品详情页增加局部刷新进度条，优化了刷新接口</li>
                            <li class="wbg clearfix">3、团购描述更改，两个按钮 开团(...)  立即参团</li>
                            <li class="wbg">4、后台商品列表中，增加复制分享链接的功能</li>
                            <li class="wbg clearfix">5、首页商品链接优化，自动跳传到最新一期详情</li>
                            <h4>2016-09-12</h4>
                            <li class="wbg clearfix"> 1、运营活动规则调整</li>
                            <li class="wbg">2、网吧活动记录和会员兑换记录添加导出数据功能</li>
                            <li class="wbg clearfix"> 3、新增网吧线下批量结算，用户话费兑换线下结算</li>
                            <li class="wbg">4、修复后退倒计时不准的BUG</li>
                            <li class="wbg clearfix"> 5、后台新增网吧终止合作操作</li>
                            <li class="wbg">6、新增微信网吧活动有效期判断</li>
                            <li class="wbg clearfix"> 7、新增后台管理员操作日志记录</li>
                            <li class="wbg">8、调整话费充支付宝的算法</li>
                            <li class="wbg clearfix"> 9、话费充值卡屏蔽显示部分调整</li>
                            <h4>2016-09-07</h4>
                            <li class="wbg clearfix"> 1、新增网吧申请提现功能</li>
                            <li class="wbg"> 2、修改平台网吧结算流程</li>
                            <li class="wbg clearfix"> 3、修复商品详情参与记录重复显示的BUG</li>
                            <li class="wbg"> 4、修复个人中心中奖记录重复显示的BUG</li>
                            <li class="wbg clearfix"> 5、修复参团人员出现重复加载并把自动加载改成点击加载，图片详情出现断裂</li>
                            <li class="wbg"> 6、新增爽乐购公告页</li>
                            <h4>2016-09-06</h4>
                            <li class="wbg clearfix">1、修复个人购买记录进度条显示BUG</li>
                            <li class="wbg">2、修复了部分手机未关注时不弹出引导关注层的BUG</li>
                            <li class="wbg clearfix">3、修复了分享链接跳转问题</li>
                            <li class="wbg"> 3、修复了部分商品加号无法点击的BUG</li>
                            <li class="wbg clearfix">4、修复了后台用户无法编辑的BUG</li>
                            <li class="wbg"> 5、后台订单功能调整</li>
                            <li class="wbg clearfix">6、修复上架一元购新上商品无法购买的BUG，对问题用户已退爽乐购余额</li>
                            <h4>2016-09-05</h4>
                            <li class="wbg clearfix">1、修复商品分类链接无法点击</li>
                            <li class="wbg"> 2、修复网吧活动少数手机无法参与的情况（微信支付凭证档住了微信完成按钮，现余额足够时支持余额参与此活动）</li>
                            <li class="wbg clearfix"> 3、修复了个人所有购买记录排序</li>
                            <li class="wbg"> 4、修复了后台用户无法编辑的BUG</li>
                            <li class="wbg clearfix"> 5、新增了后台权限管理</li>
                            <li class="wbg">6、后台订单功能按财务所需调整</li>
					</ul>
		    		</div>
		    	</div>
		    </div>
		</div>
	</div>
	<!--main start-->
	<div id="footer">
		<div class="date">
			更新时间：<em><asp:Label ID="ttime" runat="server"></asp:Label></em>
		</div>
	</div>
    </form>

            <script>

                $(function(){
                    var ajaxUrl = "http://" + window.location.host + "/ilerruser?action=countingo";
                  
                    $.ajax({
                        type: 'get', dataType: 'json', timeout: 10000,
                        url: ajaxUrl,
                        success: function (e) {
                            $("#member").text(e.newmember);
                            $("#orders").text(e.neworders);
                            $("#addmoney").text(e.newaddmoney);
                            $("#consumemoney").text(e.newconsumemoney);
                            $("#prizenum").text(e.newprizenum);
                            $("#allmember").text(e.newallmember);
                            $("#ttime").text(e.time);

                        }
                    })


                    $("#sx").click(function () {

                        window.location.reload();
                    })
                })

    </script>
</body>
</html>
