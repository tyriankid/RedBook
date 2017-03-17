<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="tuan.item.aspx.cs" Inherits="RedBook.WxTuan.tuan_item" %>

<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>
<%@ Register TagPrefix="UI" Namespace="Taotaole.Common" Assembly="Taotaole.Common" %>
<!DOCTYPE html>
<html>
<head>
    <meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
    <title>拼团详情</title>
    <meta content="app-id=518966501" name="apple-itunes-app" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, user-scalable=no, maximum-scale=1.0" />
    <meta content="yes" name="apple-mobile-web-app-capable" />
    <meta content="black" name="apple-mobile-web-app-status-bar-style" />
    <meta content="telephone=no" name="format-detection" />
    <link href="/css/comm.css" rel="stylesheet" type="text/css" />
    <link href="/css/tuan.css" rel="stylesheet" type="text/css" />
    <link href="/css/goods.css" rel="stylesheet" type="text/css" />
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <script src="http://res.wx.qq.com/open/js/jweixin-1.0.0.js"></script>
    <script src="/js/jquery190.js" language="javascript" type="text/javascript"></script>
    <script src="/js/swiper.min.js"></script>
    <script src="/js/wxShare.js"></script>
    <script src="/js/fastclick.js"></script>
</head>

<body>
    <section id="reflash-h">
        <header class="header">
            <h2>拼团详情</h2>
            <a id="fanhui" class="cefenlei" onclick="history.go(-1)" href="javascript:;">
                <img width="30" height="30" src="/images/fanhui.png">
            </a>
            <div class="head-r"><a href="/index.aspx" class="fr_Home"></a></div>
        </header>

        <section class="tuandelbox">
            <!-- 焦点图 Start  -->
            <div class="swiper-container swiper-pPicBor swiper-container-horizontal">
                <div class="swiper-wrapper">
                </div>
                <div class="swiper-pagination swiper-pagination-clickable"><span class="swiper-pagination-bullet"></span><span class="swiper-pagination-bullet"></span><span class="swiper-pagination-bullet swiper-pagination-bullet-active"></span><span class="swiper-pagination-bullet"></span><span class="swiper-pagination-bullet"></span></div>
            </div>
            <script>
                var mySwiper = new Swiper('.swiper-container', {
                    autoplay: 5000, //可选选项，自动滑动
                    pagination: '.swiper-pagination',
                    paginationClickable: true,
                    observer: true, //修改swiper自己或子元素时，自动初始化swiper
                    observeParents: true //修改swiper的父元素时，自动初始化swiper
                })
            </script>

            <!-- 焦点图 End  -->

            <!-- 简介 Start  -->
            <div class="TuanDetails">
                <%-- <dl class="del-money clearfix">
                <dt><cite>￥</cite><b role="per_price">0</b><i role="tuan_price">￥0</i></dt>
            </dl>
            <h2 role="tuan_title"></h2>
            <div class="dec"></div>--%>
            </div>
            <!-- 简介 End  -->

            <section id="g-mall-service" class="clearfix">
                <ul>
                    <li class="mall_detail_1">
                        <span>包邮</span>
                    </li>
                    <li class="mall_detail_2">
                        <span>7天退换</span>
                    </li>
                    <li class="mall_detail_3">
                        <span>假一赔十</span>
                    </li>
                    <li class="mall_detail_4">
                        <span>海关监督</span>
                </ul>
            </section>

            <div class="smbox clearfix">
                <div class="smtxt">支付开团并邀请<b role="left_renshu">1</b>人参团，人数不足自动退款</div>
            </div>

            <!-- 团购人员列表 Start -->
            <section id="g-nearby-group">
                <span class="item-nearby-gourp-tip">以下小伙伴正在发起团购，您可以直接参与</span>
                <div id="divLoad">点击加载更多</div>
            </section>
            <!-- 团购人员列表 End -->
            <input id="pagenum" value="0" type="hidden" /><!--已加载到第几页-->
        </section>
    </section>
    <section class="tuandelbox">

        <!-- 商品详情  Start -->
        <section class="tgoods_detailsbox clearfix">
            <!-- 编辑器内容 Start -->
            <div id="productContent"></div>
            <!-- 编辑器内容 End -->
        </section>
        <!-- 商品详情  End -->

    </section>

    <div class="tuan-buyfix">
        <ul>
            <li class="f_home">
                <a title="首页" href="/index.aspx">
                    <i>&nbsp;</i> 首页
                </a>
            </li>
            <li class="tuan-love">
                <i>&nbsp;</i> 收藏</li>
            <li class="tuan-one">
                <a class="" href="javascript:;">
                    <p>￥0</p>
                    <b>单卖价</b>
                </a>
            </li>
            <li class="tuan-pin">
                <a class="link-buy" role="btn_Buy" href="javascript:validateTuanPay();">
                    <p></p>
                    <b></b>
                </a>
                <%--<a class="link-buy" style="background: #666;">
                    <p>￥54</p>
                    <b>此团已过期</b>
                </a>--%>
            </li>
        </ul>
    </div>
    <div class="pin-toast-container">
        <div class="pin-toast" style="display: none; opacity: 5;">拼团活动暂不支持单独购买</div>
    </div>

    <!-- 热门推荐  Start -->
    <div class="recommends-listbox clearfix">
        <div class="recommend_head">你可能会喜欢</div>
        <ul class="recommends-list clearfix" id="tuanlike">
            <!--<li class="item">
                    <div class="recommend_img">
                        <img src="/shopimg/20160721/44945360069407.jpg">
                    </div>
                    <div class="recommend_title">【韩国直供】九朵云奇迹马油霜 去痘印淡斑补水保湿面霜 70ml/瓶 【复购排行NO.1】</div>
                    <div class="recommend-price">
                        <span class="price-icon">￥</span>
                        <span class="price-header">0.01</span>
                    </div>
                    <div class="like_click"><div class="recommend_like"></div></div>
                </li>-->
        </ul>
    </div>

    <!-- 热门推荐  End -->
    <div class="h50"></div>

    <UI:WeixinSet ID="weixin" runat="server" />
</body>
<script>
    $(function () {
        FastClick.attach(document.body);//快速点击

        $("#pagenum").val("0");
        $(".tuan-one a").click(function () {//点击单卖价弹出提示层
            $(".pin-toast-container .pin-toast").show();
            setTimeout(function () {
                $(".pin-toast-container .pin-toast").hide();
            }, 3000);
        });

        //$("#g-nearby-group .nearby_group_detail").remove();
        bind();//绑定事件
        getGoodsInfo();
        getJoinlistInfo(true);//获取参团列表
        getlike();
        getTuanKeep();
    });
    function getTuanKeep() {
        var ajaxUrl = "http://" + window.location.host + "/ilerrtuan?action=tuankeep&type=2&tid=" + getUrlParam("tuanid");
        $.ajax({
            type: 'get', dataType: 'json', timeout: 10000,
            url: ajaxUrl,
            success: function (data) {
                switch (data.state) {
                    case 0:
                        $('.tuan-love').toggleClass('zan');
                        break;
                }
            }
        });
    }
    //刷新页面移除列表项

    //获取商品详情
    var tid, productid, per_price, tuan_price, total_num;
    function getGoodsInfo() {
        var ajaxUrl = "http://" + window.location.host + "/ilerrtuan?action=tuandetail&tid=" + getUrlParam("tuanid");
        $.ajax({
            type: 'get', dataType: 'json', timeout: 10000,
            url: ajaxUrl,
            success: function (data) {
                //设置页面元素
                tid = data.data[0].tId;
                productid = data.data[0].productid;
                per_price = parseFloat(data.data[0].per_price);
                total_num = parseInt(data.data[0].total_num);
                //tuan_price = parseFloat(data.data[0].money);

                var vdescription = data.data[0].description;
                vdescription = vdescription.replace(/\r\n/g, "<br/>");

                $(".tuan-one p").html('￥' + data.data[0].money);//单卖价
                $(".TuanDetails").append(' <dl class="del-money clearfix">' +
                    '<dt><cite>团购价￥</cite><b role="per_price">' + data.data[0].per_price + '</b><i role="tuan_price">单卖价￥' + data.data[0].money + '</i></dt>' +
                '</dl>' +
                '<h2 role="tuan_title">' + data.data[0].money + '</h2>' +
                '<div class="dec">' + vdescription + '</div>');

                GetGoodsPic(data);//商品幻灯片


                $("[role='btn_Buy']").find("p").html("￥" + per_price);
                $("[role='btn_Buy']").find("b").html("开团(" + total_num + "人团)");
                //$(".dec").html(data.data[0].description.replace("\r\n","<br />"));

                $("#productContent").append(data.data[0].contents);

                //设置微信分享内容
                setWxShareInfo(data);
            }
        });
    }
    //获取猜你喜欢
    function getlike() {
        var ajaxUrl = "http://" + window.location.host + "/ilerrtuan?action=tuanlike&tid=" + getUrlParam("tuanid");
        $.ajax({
            type: 'get', dataType: 'json', timeout: 10000,
            url: ajaxUrl,
            success: function (data) {
                //设置页面元素

                //tuan_price = parseFloat(data.data[0].money);
                li = '';
                for (i = 0; i < data.data.length; i++) {
                    li += '<li class="item" onclick="onclicklike(' + data.data[i].tId + ')"><div class="recommend_img"><img src="' + data.imgroot + data.data[i].thumb + '"></div><div class="recommend_title">' + data.data[i].title + '</div><div class="recommend-price"><span class="price-icon">￥</span><span class="price-header">' + parseFloat(data.data[i].per_price) + '</span>/div>' +
                        //'<div class="like_click"><div class="recommend_like"></div></div>'+
                        '</li>';
                }
                for (i = 0; i < data.yydata.length; i++) {
                    li += '<li class="item" onclick="onclicklikeyy(' + data.yydata[i].yId + ')"><div class="recommend_img"><img src="' + data.imgroot + data.yydata[i].thumb + '"></div><div class="recommend_title">' + data.yydata[i].title + '</div><div class="recommend-price"><span class="price-icon">￥</span><span class="price-header">' + parseFloat(data.yydata[i].yunjiage) + '</span>/div>' +
                       // '<div class="like_click"><div class="recommend_like"></div></div>'+
                        '</li>';
                }
                $("#tuanlike").append(li);
                $(".recommends-listbox .item .recommend_img img").height($(".recommends-listbox .item").width());
            }
        });
    }
    function onclicklike(tid) {
        if (tid) {
            window.location.href = "tuan.item.aspx?tuanid=" + tid;
        }
    }
    function onclicklikeyy(yid) {
        if (yid) {
            window.location.href = "/index.item.aspx?shopid=" + yid;
        }
    }
    //获取商品幻灯片
    function GetGoodsPic(data) {
        $("[role='per_price']").html(per_price);
        //$("[role='tuan_price']").html("￥" + tuan_price);
        $("[role='tuan_title']").html(data.data[0].title);
        $("[role='left_renshu']").html(total_num - 1);
        if (data.data[0].picarr) {
            var DetailSrc = data.data[0].picarr.split(","); //幻灯片路径
        }
        var swiperContent = ""
        for (var i = 0; i < DetailSrc.length ; i++) {
            if (DetailSrc[i].length > 0) {
                swiperContent += '<div class="swiper-slide" style="width:100%;">' +
                    '<li><img src="' + data.imgroot + DetailSrc[i] + '" class="animClass"></li>' +
                    '</div>';
            }
        }
        $(".swiper-wrapper").append(swiperContent);
    }


    var nextPage = 1;
    //获取参团列表
    function getJoinlistInfo() {
        $("#pagenum").val("0");
        var ajaxUrl = "http://" + window.location.host + "/ilerrtuan?action=tuanjoinlist&tid=" + getUrlParam("tuanid") + "&p=" + nextPage;
        $.ajax({
            type: 'get', dataType: 'json', timeout: 10000,
            url: ajaxUrl,
            success: function (data) {
                if (parseInt($("#pagenum").val()) >= nextPage) { return; }//防止重复加载
                if (nextPage == 0) { return; } //表示加载完毕

                //暂无数据
                if (data.count <= 0) {
                    $("#divLoad").text("暂无数据");
                    return;
                }
                $("#pagenum").val(nextPage);//当前页
                nextPage = data.nextPage;//下一页
                //构建数据集
                var div = "";
                for (var i = 0; i < data.data.length; i++) {
                    var userIp = data.data[i].user_ip;
                    userIp = !userIp ? "未知地址" : userIp.substr(0, userIp.indexOf(","));
                    if (data.data[i].remain_num == 0) {//已成团
                        div += '<div class="nearby_group_detail group_mantuan">' +
                                        '<div class="nearby_g_img"><img class="nearby_g_owner_img" src="' + data.data[i].headimg + '"></div>' +
                                        '<div class="nearby_g_button">' +
                                            '<div class="nearby_g_info">' +
                                                '<div class="divbox">' +
                                                    '<div class="nearby_g_owner">' + data.data[i].username + '</div>' +
                                                    '<div class="nearby_g_left_user_num">已成团</div>' +
                                                '</div>' +
                                                '<div class="divbox">' +
                                                    '<div id="' + data.data[i].rid + '" class="nearby_g_left_time" >' + data.data[i].end_time.replace('T', ' ').substring(5) + ' 结束</div>' +
                                                 '</div>' +
                                             '</div>' +
                                       '</div>' +
                                            '<div class="qucantuan">' +
                                                '<a href="tuan.join.aspx?tid=' + data.data[i].tuanid + '&tuanlistId=' + data.data[i].tuanlistId + '"><span>已成团</span></a>' +
                                            '</div>' +
                                        '<div class="nearby_line"></div>' +
                                    '</div>';

                    } else {
                        div += '<div class="nearby_group_detail">' +
                                  '<div class="nearby_g_img"><img class="nearby_g_owner_img" src="' + data.data[i].headimg + '"></div>' +
                                  '<div class="nearby_g_button">' +
                                      '<div class="nearby_g_info">' +
                                          '<div class="divbox">' +
                                              '<div class="nearby_g_owner">' + data.data[i].username + '</div>' +
                                             // '<div class="nearby_g_left_user_num">还差' + data.data[i].remain_num + '人成团</div>' +
                                              '<div class="nearby_g_left_user_num">还差' + data.data[i].remain_num + '人成团</div>' +
                                          '</div>' +
                                          '<div class="divbox">' +
                                                '<div id="' + data.data[i].rid + '" class="nearby_g_left_time" >' + data.data[i].end_time.replace('T', ' ').substring(5) + ' 结束</div>' +
                                            '</div>' +
                                       '</div>' +
                                 '</div>' +
                                      '<div class="qucantuan">' +
                                          '<a href="tuan.join.aspx?tid=' + data.data[i].tuanid + '&tuanlistId=' + data.data[i].tuanlistId + '"><span>去参团</span></a>' +
                                      '</div>' +
                                  '<div class="nearby_line"></div>' +
                              '</div>';
                    }
                }
                $("#divLoad").before(div);
                if (nextPage == 0) {
                    $("#divLoad").text("加载完成");
                    return;
                }
            }
        });

    }

    function getJoinlistInfoEx(data) {
    }

    //绑定事件
    function bind() {
        //收藏商品
        $('.tuan-love').click(function () {
            if ($(this).hasClass('zan')) {
                //取消收藏
                var ajaxUrl = "http://" + window.location.host + "/ilerrtuan?action=tuankeep&type=1&tid=" + getUrlParam("tuanid");
                $.ajax({
                    type: 'get', dataType: 'json', timeout: 10000,
                    url: ajaxUrl,
                    success: function (data) {
                        switch (data.state) {
                            case 0:
                                $('.tuan-love').toggleClass('zan');
                                break;
                            case 1:
                                alert("无此收藏，无需取消");
                                break;
                            case 5:
                                alert("数据来源非合法");
                                break;
                            case 4:
                                alert("缺少参数");
                                break;
                        }
                    }
                });
            } else {
                //添加收藏
                var ajaxUrl = "http://" + window.location.host + "/ilerrtuan?action=tuankeep&type=0&tid=" + getUrlParam("tuanid");
                $.ajax({
                    type: 'get', dataType: 'json', timeout: 10000,
                    url: ajaxUrl,
                    success: function (data) {
                        switch (data.state) {
                            case 2:
                                $('.tuan-love').toggleClass('zan');
                                break;
                            case 3:
                                alert("已收藏，无需再次收藏");
                                break;
                            case 5:
                                alert("数据来源非合法");
                                break;
                            case 4:
                                alert("缺少参数");
                                break;
                        }
                    }
                });
            }
        });
        ////支付事件
        //$("[role='btn_Buy']").click(function () {
        //    location.href = "http://" + window.location.host + "/WxPay/wx_Submit.aspx?businessType=tuan&tuanType=openTuan&money=" + per_price + "&shopid=" + tid;
        //});
    }

    //先验证，在参团支付
    function validateTuanPay() {
        var ajaxUrl = "http://" + window.location.host + "/ilerrpay?action=userpaymenttuan&typeid=1&businessid=" + tid;
        $.ajax({
            type: 'get', dataType: 'json', timeout: 10000,
            url: ajaxUrl,
            success: function (data) {
                switch (data.state) {
                    case 0:
                        //通过验证
                        location.href = "http://" + window.location.host + "/WxPay/wx_Submit.aspx?businessType=tuan&tuanType=openTuan&money=" + per_price + "&shopid=" + tid;
                        break;
                    case 2:
                        alert("数据来源非合法");
                        break;
                    case 3:
                        alert("缺少参数");
                        break;
                    case 4:
                        alert("团购已过期");
                        break;
                    case 5:
                        alert("团购数已满");
                        break;
                    case 6:
                        alert("已参过此团");
                        break;
                }
            }
        });
    }

    function setWxShareInfo(data) {
        //var $wxShareDiv = $("#wxShareInfo");
        //分享图标,分享标题描述,分享描述,分享url
        var shareImg = data.imgroot + data.data[0].thumb;
        var shareTitleDes = data.data[0].per_price + "元买" + data.data[0].title + "凑齐" + data.data[0].total_num + "人开团，就差你了！";
        var shareDes = data.data[0].per_price + "元"+"拼团抢购" + data.data[0].title + "每人只要" + data.data[0].per_price+"元哦！";
        var link = "http://" + window.location.host + "/WxTuan/tuan.item.aspx?tuanid=" + data.data[0].tId;
        //alert(shareImg + shareTitleDes + shareDes + link);
        WinXinShareMessage(shareTitleDes, shareDes, link, shareImg);
    }

    //页面传参
    function getUrlParam(name) {
        var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)"); //构造一个含有目标参数的正则表达式对象
        var r = window.location.search.substr(1).match(reg);  //匹配目标参数
        if (r != null) return unescape(r[2]); return null; //返回参数值
    }

    
    var currentPage=1;
    $("#divLoad").click(function () {//点击单卖价弹出提示层
        if (currentPage == nextPage) return;
        getJoinlistInfo();
        currentPage = nextPage;
    });

    //下拉加载
    //$(window).scroll(function () {
    //    if ($(window).scrollTop() >= $("#g-nearby-group").height() - $(window).height()) {
    //        getJoinlistInfo();
    //    }
    //});
    //开启右上角微信菜单
    document.addEventListener('WeixinJSBridgeReady', function onBridgeReady() {
        WeixinJSBridge.call('showOptionMenu');
    });
</script>
</html>
