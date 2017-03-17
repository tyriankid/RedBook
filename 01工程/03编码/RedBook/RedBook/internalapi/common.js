//参照网易倒计时，应该是0.2秒执行时钟循环，而每次减少毫秒数随机，但一秒中执行5次
/*
*$this:装载着倒计时时分秒的<time>标签
* currMS:是剩余的毫秒数量
* type:倒计时类型,singleLottery:首页单个倒计时;lotteryList:揭晓页面批量倒计时;
*/
var timer = new Array;
startTimeOut = function ($this, currMS, type) {
	var m = 0;//分钟
	var s = 0;//秒
	var o = 0;//毫秒
	var i = 0;//计数器
	var v;		//时钟对象

	//var currTime = new Date();
	//var endTime = new Date();
	//endTime.setSeconds(endTime.getSeconds() + currMS/1000 - 1);
    //var w = parseInt((endTime.getTime() - currTime.getTime()) / 1000);
	var w = currMS/1000;
	if (w < 0)
		return;
	m = parseInt(w / 60);
	s = parseInt(w % 60);
	o = (w == 0) ? currMS : 99;
	this.countdown = function () {
		v = setInterval(function() {
		    i++;
		    if (type == "singleLottery" && $("#singleLottery").attr("style").indexOf("top:0")<0) {
		        $("#singleLottery").attr("style", "top:0;position:relative;padding:0;margin:0;");//将滚动条还原到第一行,此处避免滚动中的回零失效问题
		    }
			if (m <= 0 && s <= 0 && o <= 0) {
			    //开始揭晓 spanTime1
			    clearInterval(v);
			    refreshLottery($this,type);
				return;
			}
			o = o - Math.round(Math.random() * 19 + 1);

			if (i >= 5) {
				i = 0;
				//Math.round(Math.random()*9+1);
				s = s - 1;
				/*if (s == 0 && m > 0)
					m = m - 1;
				else if (s < 0 && m > 0)
					s = 59;*/
				if (s <= 0 && m > 0) {
				    s = 59;
				    m = m - 1;
				}
				if (m <= 0 && s <= 0)
					o = 0;
				if (m > 0 || s > 0)
					o = 99;
			}
			if (m <= 0) m = 0;
			if (s <= 0) s = 0;

			var mStr = m < 10 ? ('0' + m) : m;
			var sStr = s < 10 ? ('0' + s) : s;
			var oStr = o < 10 ? ('0' + o) : o;
			$this.html(mStr + ":" + sStr + ":" + oStr);
		}, 200);
		timer.push(v);
	}
}

//更新揭晓页面最新揭晓内容
function refreshLottery($this, type) {
    switch (type) {
        case "lotteryList"://揭晓页面多个处理
            //$this.html("倒计时结束，计算中奖信息...");
            $this.html("<i>中奖信息计算中...</i>");
            var ajaxUrl = "http://" + window.location.host + "/ilerrbuy?action=refreshLottery&yid=" + $this.attr("id");
            var ul = "";
            $.ajax({
                type: 'get', dataType: 'json', timeout: 10000, url: ajaxUrl, success: function (data) {
                    //参数异常
                    if (data.state == 2) {
                        return;
                    }
                    var $product = $this.parents("dl").eq(0);
                    var dd = '<dd>期号：' + data.data[0].qishu + '</dd>' +
                                    '<dd>幸运码：<em class="orange arial">' + data.data[0].q_code + '</em></dd>' +
                                    '<dd>获得者：<a name="uName" uweb="' + data.data[0].q_uid + '" class="rUserName blue">' + data.data[0].username + '</a></dd>' +
                                    '<dd>本期参与：<em class="orange arial">' + data.data[0].quantity + '</em>人次</dd>' +
                                    '<dd>揭晓时间：' + data.data[0].q_end_time + '</dd>';
                    $product.parents("ul").removeClass("WeiJieXiao");
                    $product.find("dd").remove();
                    $product.append(dd);
                    if (data.ismy == 1) {
                        WinRemind();
                    }
                }
            });
            break;

        case "singleLottery"://首页单个处理
            var $countDownLi = $this.parents("li");
            $countDownLi.removeClass("JJJX");
            //$countDownLi.html("倒计时结束，计算中奖信息...");
            $this.html("<i>中奖信息计算中...</i>");
            var ajaxUrl = "http://" + window.location.host + "/ilerrbuy?action=refreshLottery&yid=" + $this.attr("id");
            var ul = "";
            $.ajax({
                type: 'get', dataType: 'json', timeout: 10000, url: ajaxUrl, success: function (data) {
                    //参数异常
                    if (data.state == 2) {
                        return;
                    }
                    $countDownLi.html('<a href="index.item.aspx?shopid=' + $this.attr("id") + '">恭喜<i>' + data.data[0].username + '</i>获得<b>' + data.data[0].title + '</b><i class="z-arrow"></i></a>');
                    getLotteryList("won");//倒计时结束后继续往后插入最新揭晓信息
                    if (data.ismy == 1) {
                        WinRemind();
                    }
                }
            });
            break;

        case "itemLottery"://商品详情页面
            var ajaxUrl = "http://" + window.location.host + "/ilerrbuy?action=refreshLottery&yid=" + shopid;
            var ul = "";
            $.ajax({
                type: 'get', dataType: 'json', timeout: 10000, url: ajaxUrl, success: function (data) {
                    //参数异常
                    if (data.state == 2) {
                        return;
                    }
                    location.href = "http://" + window.location.host + "/index.item.aspx?shopid=" + shopid;
                    //if (data.ismy == 1) {
                    //    WinRemind();
                    //}
                }
            });
            
            break;

        case "mylist"://个人中心参与详情页面
            //$this.html("倒计时结束，计算中奖信息...");
            //$this.html("<i>中奖信息计算中...</i>");
            var ajaxUrl = "http://" + window.location.host + "/ilerrbuy?action=refreshLottery&yid=" + $this.attr("id");
            $.ajax({
                type: 'get', dataType: 'json', timeout: 10000, url: ajaxUrl, success: function (data) {
                    //参数异常
                    if (data.state == 2) {
                        return;
                    }
                    var $product = $this.parents(".canyubox").eq(0);
                    //var dd = '<p>获得者：<em class="blue">' + data.data[0].username + '</em></p>' +
                    //                '<p>参与次数：<em class="gray6" style="color:#FD524E">' + data.data[0].quantity + '</em></p>' +
                    //                '<p>幸运号码：<em class="gray6" style="color:#FD524E">' + data.data[0].q_code + '</em></p>' +
                    //                '<p>揭晓时间：<em class="gray6">' + data.data[0].q_end_time + '</em></p>';
                    var dd = '<div class="pzhongj">获得者：' + data.data[0].username + '<span class="renci"><b>' + data.data[0].quantity + '</b>人次</span></div>' +
                    '<a class="link-canyu" href="/index.item.aspx?jump=true&shopid=' + data.data[0].yid + '">再次参与</a>';
                    $this.parents(".canyubox").html(dd);
                    if (data.ismy == 1) {
                        WinRemind();
                    }
                }
            });

            break;
    }



}




     