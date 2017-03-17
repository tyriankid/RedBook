<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="cart.payment.aspx.cs" Inherits="RedBook.cart_payment" %>

<!DOCTYPE html>
<html>

<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width,initial-scale=1,minimum-scale=1,maximum-scale=1,user-scalable=no" />
    <title>结算支付</title>
    <meta content="app-id=518966501" name="apple-itunes-app" />
    <meta content="yes" name="apple-mobile-web-app-capable" />
    <meta content="black" name="apple-mobile-web-app-status-bar-style" />
    <meta content="telephone=no" name="format-detection" />
    <link href="/css/comm.css" rel="stylesheet" type="text/css" />
    <link href="/css/cartList.css" rel="stylesheet" type="text/css" />
    <link href="/css/swiper.min.css" rel="stylesheet" type="text/css" />
    <script src="/js/jquery190.js" language="javascript" type="text/javascript"></script>
    <script type="text/javascript" src="/js/swiper.min.js"></script>

    <style>
        .swiper-container {
            width: 100%;
            height: 100%;
        }

        .swiper-slide {
            text-align: center;
            font-size: 18px;
            /* Center slide text vertically */
            /*display: -webkit-box;
		        display: -ms-flexbox;
		        display: -webkit-flex;
		        display: flex;*/
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
    <script type="text/javascript">
        $(document).ready(function () {
            var swiper = new Swiper('.swiper-container', {
                pagination: '.swiper-pagination',
                slidesPerView: 3,
                paginationClickable: true,
                spaceBetween: 10
            });
        });
    </script>

</head>

<body>
    <header class="header">
        <h2>支付</h2>
        <a class="cefenlei" onclick="history.go(-1)" href="javascript:;">
            <img width="30" height="30" src="/images/fanhui.png">
        </a>
        <div class="head-r"><a href="/index.aspx" class="fr_Home"></a></div>
    </header>
    <div class="h5-1yyg-v1">
        <section class="ipaylist">
            <ul>
                <li>总需支付
						<div><em class="totalpay"><%=paymoney %></em><k role="huobi">爽乐币</k></div>
                </li>
                <li role="redpackPay" class="redpack redpack-show">红包抵扣<span class="payyue">(可用：<em class="wallet"><%=redpackNum %></em>)</span>
                    <div id="redChedTopbox" class="payless padright">
                        <span>-<em id="num">0</em><k role="huobi">爽乐币</k></span><i id="btnZhe" class="Zhedie"></i>
                    </div>

                    <div class="swiper-container">
                        <div class="swiper-wrapper">
                            <asp:Repeater ID="rptRedpack" runat="server">
                                <ItemTemplate>
                                    <div class="swiper-slide" role="redpackCheck" redpackid="<%#Eval("id") %>">
                                        <i id="redpack_<%#Eval("id") %>" class="changeCheck"></i>
                                        <section class="redpa-quan">
                                            <h1><%#Eval("discount") %></h1>
                                            <h2 role="huobi">爽乐币</h2>
                                        </section>
                                        <section class="redpa-txt">
                                            <!--<p><b>剩余：<cite><em>10</em>爽乐币</cite></b></p>-->
                                            <p><%#Eval("codetitle") %>…</p>
                                            <p><cite class="overtime"><%#Eval("overtime") %></cite></p>
                                        </section>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                        <!-- Add Pagination -->
                        <div class="swiper-pagination"></div>
                    </div>
                </li>
                <li role="moneyPay">爽乐币抵扣<span class="payyue">(剩余：<em class="wallet"><%=leftMoney %></em>)</span>
                    <div class="payless padright"><span>-<em role="moneyPayNum" class="walleti"><%=leftMoney %></em>爽乐币</span><i id="btnYue" class="changeCheck icheck"></i></div>
                </li>
            </ul>
            <ul role="onlinePay">
                <li>选择支付方式
						<div class="payless"><em role="onlinePayNum" class="payment"><%=paymoney %></em>元</div>
                </li>
                <!--{wc:loop $paylist $pay}
					<li class="payway" role="wxpay" payId="{wc:$pay['pay_id']}"><i class="icon-i {wc:$pay['icon']}"></i>{wc:$pay['pay_name']}
						<div><i class="changeCheck"></i></div>
					</li>
					{wc:loop:end}-->

                <li class="payway" role="wxpay" payid="weixin"><i class="icon-i icon-iweixin"></i>微信支付
						<div><i class="changeCheck"></i></div>
                </li>
                <!--
					<li class="payway" role="wxpay" payId="zhifubao"><i class="icon-i icon-izhifubao"></i>支付宝支付
						<div><i class="changeCheck"></i></div>
					</li>
                    -->
            </ul>
            <button type="button" id="btnPay">立即支付</button>
        </section>
        <div class="mask"></div>
        <div class="alertBox">
            <p class="content">请选择支付方式</p>
            <p class="close">关闭</p>
        </div>
    </div>
    <!-- 支付跳转加载框 Start -->
    <div class="sk-wave-bg"></div>
    <div class="sk-wave">
        <div class="sk-rect sk-rect1"></div>
        <div class="sk-rect sk-rect2"></div>
        <div class="sk-rect sk-rect3"></div>
        <div class="sk-rect sk-rect4"></div>
        <div class="sk-rect sk-rect5"></div>
        <div class="sk-txt">正在支付中...</div>
    </div>
    <!-- 支付跳转加载框 End -->
    <div class='mask'></div>
    <div class='alertBox'>
    <p class='content'></p>
    <p class='close' onclick='closeAlertBox()'>关闭</p></div>
</body>
<script type="text/javascript">

    //支付方式选择
    $(".payway").click(function () {
        payTypeId = $(this).attr("payId");//设置支付方式
        $(this).find('.changeCheck').addClass('icheck');
        $(this).siblings().find('.changeCheck').removeClass('icheck');
    })
    //支付方式切换样式
    function changePay(e) {
        var prev = $(e).prev();
        $(e).toggleClass('icheck');
        if ($(e).hasClass('icheck') || $(".payway").find('.icheck').length == 0) {
            prev.show();
        } else {
            prev.hide();
        }
    }
    //折叠展开框
    $("#redChedTopbox").click(function () {
        //默认是Zhedie,展开状态
        $("#btnZhe").toggleClass("Zhedie2");
        if ($("#btnZhe").hasClass("Zhedie2")) {
            $(".redpack").removeClass("redpack-show");
        } else {
            $(".redpack").addClass("redpack-show");
        }
    })
    //红包选择框
    $("[role='redpackCheck']").click(function () {
        var $checkBox = $(this).find('i');
        var CheckdNum = $("#redChedTopbox").find('em');
        var $listNum = parseInt($(this).find('h1').html()) > parseInt(totalMoney) ? totalMoney : $(this).find('h1').html();

        var $redChedvalue = $(this).find('.redpa-txt').find('em');
        if ($checkBox.hasClass('icheck')) {
            $checkBox.removeClass('icheck');
            $("#redChedTopbox").removeClass("redtxt");
            CheckdNum.html("0");
            redpackid = '';
        } else {
            $("[role='redpackCheck']").siblings().find("i").removeClass("icheck");
            $checkBox.addClass('icheck');
            $("#redChedTopbox").addClass("redtxt");
            CheckdNum.html($listNum);
            redpackid = $(this).attr('redpackId'); //获取红包ID
        }
        redPackMoney = parseInt(CheckdNum.html()); //设置红包抵扣金额
        setPaymentParms();
    })

    /*
        *根据当前页面选中的情况设定支付的各项参数与隐藏显示
        1:使用或不使用红包时,余额不够的情况下,余额和微信拼接使用,
        2:使用或不使用红包时,余额足够的情况下微信板块隐藏
        3:使用或者不使用红包,但余额为零的情况下,余额板块隐藏
        4:使用红包的情况下,余额支付和在线支付的金额要做相应的减法
        */
    var payTypeId = '';//支付方式id
    var redPackMoney = 0;
    var totalMoney = parseInt("<%=paymoney%>");
    var leftMoney = parseInt("<%=leftMoney%>");
    

    var payMoney = 0;
    var redpackid = '';//红包ID默认为空
    function setPaymentParms() {
        totalMoney = parseInt("<%=paymoney%>");
	    leftMoney = parseInt("<%=leftMoney%>");
	    //如果须支付总额大于用户余额,实际支付金额就为 支付总额-用户余额
        if (totalMoney > leftMoney) {
            payMoney = totalMoney - leftMoney;
        }



	    var $moneyPay = $("[role='moneyPay']"), $onlinePay = $("[role='onlinePay']");//余额支付区域和在线支付区域
	    var $moneyPayNum = $("[role='moneyPayNum']"), $onlinePayNum = $("[role='onlinePayNum']");//余额支付数量和在线支付数量
	    //余额不够的情况下,余额和微信拼接使用(不需做任何判断)

        //直购商品不允许余额支付,如果当前是全购商品,则余额为0处理,并隐藏余额支付模块
	    if ("<%=businessType%>" == "quan") {
	        leftMoney = 0;
	        $("[role='huobi']").html("元");
	    }


	    //计算余额支付需要的金额
	    payMoney = totalMoney - redPackMoney;//需要支付金额=总价-红包
	    //使用红包的金额满足了支付所需总额时,余额需要支付0,在线需要支付0,并隐藏
	    if (payMoney <= 0) {
	        //设置剩余钱数为0,并隐藏
	        $moneyPayNum.html(payMoney); $moneyPay.hide();
	        $onlinePayNum.html(payMoney); $onlinePay.hide();
	        return;
	    }
	    //如果红包抵扣的钱不足以满足支付所需总额,
	    if (payMoney - leftMoney > 0) {//余额不够
	        payMoney = payMoney - leftMoney;//需要支付金额=总价-红包之后再减余额
	        leftMoney = leftMoney;//余额支付不变
	        $moneyPayNum.html(leftMoney); $moneyPay.show();
	        $onlinePayNum.html(payMoney); $onlinePay.show();

            //余额为0的隐藏处理
	        if (leftMoney == 0) {
	            $moneyPay.hide();
	        }

	    } else {//余额够
	        leftMoney = payMoney; $moneyPayNum.html(leftMoney); $moneyPay.show();//余额扣去当前所需支付金钱
	        payMoney = 0; $onlinePayNum.html(payMoney); $onlinePay.hide();
	    }
	}

	    $('.ipaylist button').click(function () {
	        if ($(".payway .icheck").length == 0) {
	            alertBox('请选择支付方式');
	            return false;
	        }
	    })

	    $('.alertBox .close').click(function () {
	        $('.mask').hide();
	        $('.alertBox').hide();
	    })

	    $("#btnPay").click(function () {
	        $(".sk-wave-bg").show();
	        $(".sk-wave").show();
	        pay();
	    })

	    function alertBox(content) {
	        $('.alertBox .content').text(content);
	        $('.mask').show();
	        $('.alertBox').show();
	    }


	    //页面初始化
	    function init() {
	        //默认为微信支付
	        $(".icon-iweixin").click();
	        //如果余额为零,隐藏余额框
	        if (leftMoney <= 0) {
	            $("[role='moneyPay']").hide();
	        }
	        if (parseInt("<%=redpackNum%>") == 0) {
	            $(".redpack").hide();
	        }

	        $(function () {
	            var $yueCheck = $("#spBalance");
	            //如果余额足够,默认勾选余额选项
	            if (totalMoney <= leftMoney) {
	                $yueCheck.parent().removeClass("z-pay-grayC");
	                $yueCheck.attr("sel", "1").attr("class", "z-pay-mentsel").next("span").html("余额支付<em class='orange'>" + totalMoney + "</em>元（账户余额：" + (leftMoney - totalMoney) + " 元）");
	            }
	            else {
	                //$("#bankList").find("li").eq(1).click();
	            }
	            if ($("#redpackList").find("li").length <= 1) {
	                $("#redpackList").hide();
	            }
	        })
	        setPaymentParms();
        }

    function pay() {
            switch ("<%=businessType%>"){
                case "yuan":
                    //需支付零的情况下,直接用余额和红包支付
                        if (payMoney == 0) {
                            $.getJSON("http://" + window.location.host + '/ilerrpay?action=userpaymentsubmit&shopid=<%=yid%>&quantity=<%=quantity%>&paytype=' + 0 + '&redpaper=' + redpackid + "&APIsubmitcode=0", function (data) {
                            switch (data.state) {
                                case 0://验证通过
                                    //location.href = "/WxPay/cart.paysuccess.aspx";
                                    alert("支付成功！");
                                    location.href = getUrlOK("yuan");
                                    break;
                                default:
                                    alert("支付失败！");
                                    location.href = getUrlCancel("yuan");
                                    //location.href = "/WxPay/cart.payError.aspx";
                                    break;
                            }
                        });
                    }
                    else {//否则调用微信支付接口
                        location.href = "http://" + window.location.host + "/WxPay/wx_Submit.aspx?businessType=yuan&money=" +payMoney+ "&shopid=<%=yid%>&quantity=<%=quantity%>" + "&paytype=2&redpaper=" + redpackid;
                    }
                    break;

                case "quan":
                    //需支付零的情况下,直接用余额支付
                    if (payMoney == 0) {
                        $.getJSON("http://" + window.location.host + '/ilerrpay?action=userpaymentQuanSubmit&quanid=<%=quanId%>&redpaper=' + redpackid +'&paytype=' + 0 + '&APIsubmitcode=0', function (data) {
                                switch (data.state) {
                                    case 0://验证通过
                                        //location.href = "/WxPay/cart.paysuccess.aspx";
                                        alert("支付成功！");
                                        location.href = getUrlOK("quan");
                                        break;
                                    default:
                                        alert("支付失败！");
                                        location.href = getUrlCancel("quan");
                                        //location.href = "/WxPay/cart.payError.aspx";
                                        break;
                                }
                            });
                        }
                        else {//否则调用微信支付接口
                            location.href = "http://" + window.location.host + "/WxPay/wx_Submit.aspx?businessType=quan&money=" + payMoney + "&quanid=<%=quanId%>" + "&paytype=2";
                        }
                        break;
                    break;
            }
 
        }

        //获取(成功)不同类型的跳转地址
        function getUrlOK(type) {
            var tzUrl = "/index.aspx";
            switch (type) {
                case "add":
                    tzUrl = "/community.index.aspx";
                    break;
                case "tuan":
                    tzUrl = "/WxTuan/tuan.index.aspx";
                    break;
                case "yuan":
                    location.href = "/index.aspx";
                    break;
                case "quan":
                    location.href = "/index.aspx";
                    break;
            }
            return tzUrl;
        }
        //获取(取消)不同类型的跳转地址
        function getUrlCancel(type) {
            var tzUrl = "/index.aspx";
            switch (type) {
                case "add":
                    tzUrl = "/user.userbalance.aspx?showzhifu=0";
                    break;
                case "tuan":
                    tzUrl = "/WxTuan/tuan.index.aspx";
                    break;
                case "yuan":
                    location.href = "/index.aspx";
                    break;
                case "quan":
                    location.href = "/index.aspx";
                    break;
            }
            return tzUrl;
        }
        
        //关闭弹出层
        function closeAlertBox() {
            $('.mask').remove();
            $('.alertBox').remove();
        }

        //页面载入执行
        $(function () {
            init();
            ChangeTimeFormat();
        })
        //转换时间格式
        function ChangeTimeFormat() {
            $(".overtime").each(function () {
                var EndTime = new Date($(this).html()); //截止时间 
                var NowTime = new Date();
                var t = EndTime.getTime() - NowTime.getTime();
                var d = Math.floor(t / 1000 / 60 / 60 / 24);
                var h = Math.floor(t / 1000 / 60 / 60 % 24);
                if (d > 0) {
                    $(this).html(d + '天' + h + '小时后过期');
                } else {
                    $(this).html(d + '天' + h + '小时后过期');
                }
            });
        }
</script>
</html>
