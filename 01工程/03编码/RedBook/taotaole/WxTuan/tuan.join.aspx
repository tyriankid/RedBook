<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="tuan.join.aspx.cs" Inherits="RedBook.WxTuan.tuan_join" %>

<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>
<%@ Register TagPrefix="UI" Namespace="Taotaole.Common" Assembly="Taotaole.Common" %>

<!DOCTYPE html>
<html>

<head>
    <meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
    <title>参团详情</title>
    <meta content="app-id=518966501" name="apple-itunes-app" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, user-scalable=no, maximum-scale=1.0" />
    <meta content="yes" name="apple-mobile-web-app-capable" />
    <meta content="black" name="apple-mobile-web-app-status-bar-style" />
    <meta content="telephone=no" name="format-detection" />
    <meta name="msapplication-tap-highlight" content="no">
    <link href="/css/comm.css" rel="stylesheet" type="text/css" />
    <link href="/css/tuan.css" rel="stylesheet" type="text/css" />
    <script src="/js/jquery-1.8.2.js" language="javascript" type="text/javascript"></script>
    <%-- 微信分享 --%>
    <script src="http://res.wx.qq.com/open/js/jweixin-1.0.0.js"></script>
    <script src="/js/wxShare.js"></script>
    <%-- 引导微信分享弹出层 --%>
    <script src="/js/AlertShare.js"></script>
    <script src="/js/fastclick.js"></script>
    <%-- 弹出层美化 --%>
    <%--<script src="/js/AlertBox.js"></script>--%>
</head>

<body>
    <header class="header">
        <h2>参团详情</h2>
        <a id="A1" class="cefenlei" onclick="history.go(-1)" href="javascript:;">
            <img width="30" height="30" src="/images/fanhui.png">
        </a>
        <a href="javascript:showShare();" class="share">分享</a>
    </header>
    <!-- 内页顶部 -->
    <section class="tuanJoinbox">

        <div class="pp">
            <%--团员列表--%>
            <!-- 团员列表 S -->
            <div class="pp_users all" id="pp_users"></div>
            <!-- 团员列表 E -->

            <!-- 口号 S -->
            <div class="pp_tips" id="flag_0_a">
            </div>
            <!-- 口号 E -->
            <div class="pp_state" id="flag_0_b">
                <div class="pp_time" data-sec="1471608188">
                    剩余<i id="t_dbox" style="display: none;"><span id="t_d">00</span>天</i>
                    <span id="t_h">00</span>:
                    <span id="t_m">00</span>:
                    <span id="t_s">00</span>结束
                </div>
            </div>

        </div>


        <div class="pp_list">
            <div id="showYaoheList">
            </div>
        </div>

        <script type="application/javascript">
            $(".pp_vtips").click(function () {
                var t = $(".pp_vtips span").text();
                if (t == "收起全部参团详情") {
                    $("#pp_list").addClass("pp_list_hide");
                    $("#show_all").addClass("hideAllUserList");
                    $(".pp_vtips span").text("查看全部参团详情");
                }
                else {
                    $("#pp_list").removeClass("pp_list_hide");
                    $("#show_all").removeClass("hideAllUserList");
                    $(".pp_vtips span").text("收起全部参团详情");
                }
            });
        </script>

        <div class="step">
            <div class="step_hd">拼团玩法<%--<a class="step_more">查看详情</a>--%></div>
            <div id="footItem" class="step_list">
                <div class="step_item">
                    <div class="step_num">1</div>
                    <div class="step_detail">
                        <p class="step_tit">选择心仪商品</p>
                    </div>
                </div>
                <div class="step_item ">
                    <div class="step_num">2</div>
                    <div class="step_detail">
                        <p class="step_tit">支付开团或参团</p>
                    </div>
                </div>
                <div class="step_item step_item_on">
                    <div class="step_num">3</div>
                    <div class="step_detail">
                        <p class="step_tit">等待好友参团支付</p>
                    </div>
                </div>
                <div class="step_item">
                    <div class="step_num">4</div>
                    <div class="step_detail">
                        <p class="step_tit">达到人数团购成功</p>
                    </div>
                </div>
            </div>
        </div>
    </section>

    <div class="H5_con fixopt" id="fixopt">
        <div class="fixopt_item fixopt_item2">
            <a class="fixopt_home" href="/WxTuan/tuan.index.aspx">更多拼团</a>
           <%-- <a class="fixopt_btn" role="btn_Buy" href="javascript:validateTuanPay();">我要跟团</a>--%>
            <%--<a class="fixopt_btn" href="/tuan/tuan.join_pay.aspx">我要跟团</a>--%>
            <%--<a class="fixopt_btn" href="/tuan/tuan.item.aspx">再开一个</a>
            <a class="fixopt_btn" onclick="showShare();">还差8人成团</a>--%>
        </div>
    </div>

    <!-- 分享弹出层 Start-->
    <%--<div class="popup_share-bg clearfix" id="SharePopup">
        <div class="popup_share-box clearfix">
            <div class="Txtips" id="remain_num">
                <h1>还差<b>5</b>人就能组团成功</h1>
                <p>快邀请更多小伙伴参团吧！</p>
            </div>
            <script type="text/javascript" charset="utf-8" src="http://static.bshare.cn/b/buttonLite.js#style=-1&amp;uuid=&amp;pophcol=2&amp;lang=zh"></script>
            <script type="text/javascript" charset="utf-8" src="http://static.bshare.cn/b/bshareC0.js"></script>
            <div class="popup-nrbox">
                <ul class="share-ulbox">
                    <li class="wx">
                        <a class="bshare-weixin">
                            <em></em>
                            <p>微信好友</p>
                        </a>
                    </li>
                    <li class="qq">
                        <a class="bshare-qqim">
                            <em></em>
                            <p>QQ</p>
                        </a>
                    </li>
                    <li class="qqkj">
                        <a class="bshare-qzone" target="_blank">
                            <em></em>
                            <p>QQ空间</p>
                        </a>
                    </li>
                    <li class="weibo">
                        <a class="bshare-sinaminiblog">
                            <em></em>
                            <p>微博</p>
                        </a>
                    </li>
                </ul>
                <div class="onebtn_box">
                    <a href="javascript:hideShare();" class="closebtn">取消</a>
                </div>
            </div>
        </div>
    </div>--%>
    

    <div class="zhe" style="position: absolute; width: 100%; height: 100%; min-height: 610px; top: 0; left: 0; background: rgba(60,60,60,.5); z-index: 9999; display: none;"></div>
    <div id="msg" style="display: none; position: fixed; top: 0; left: 0; z-index: 1000000;">
        <p class="code" style="position: fixed; z-index: 9998; width: 200px; height: 100px; top: 35%; right: 0; left: 0; margin: auto; font-size: 16px; text-align: center; color: #fff; margin: auto;"></p>
    </div>

    <script type="text/javascript">
        //function hideShare() {
        //    $("#SharePopup").hide();
        //}
        //function showShare() {
        //    $(".mask").show();
        //    $("#wxShareDiv").show();
        //}
        //$("#wxShareDiv").on("click", function () {
        //    $(this).hide();
        //    $('.mask').hide();
        //})
        //$(".mask").on("click", function () {
        //    $(this).hide();
        //    $("#wxShareDiv").hide();
        //})
    </script>

    <!-- 拼团介绍 Start -->
    <!--<div class="explain_tuan explain_icon" onclick="showExplain()" style="display:block;"></div>-->
    <div id="showExplain">
        <div class="showBg" onclick="hideExplain()">
            <img src="/images/quit_button.png">
        </div>
    </div>
    <script type="application/javascript">
        function hideExplain() {
            $("#showExplain").hide();
        }
        function showExplain() {
            $("#showExplain").show();
        }
    </script>
    <!-- 拼团介绍 End -->
    <UI:WeixinSet ID="weixin" runat="server" />
    

    <script>
        $(function () {
            FastClick.attach(document.body);

            //得到团的明细
            getTuanInfoDetail();
            if (share == "0") {
                //$("#SharePopup").show();
                showShare();
            }
        });
        var share = getUrlParam("share");
        var per_price = 0.0;

        //得到团的明细
        var time = "";
        var daojishi = "";
        function getTuanInfoDetail() {
            var ajaxUrl = "http://" + window.location.host + "/ilerrtuan?action=tuanjoinlistdetail&tid=" + getUrlParam("tid") + "&tuanlistId=" + getUrlParam("tuanlistId");
            $.ajax({
                type: 'get', dataType: 'json', timeout: 10000,
                url: ajaxUrl,
                success: function (data) {

                    $(".tuanJoinbox .pp").before(getGoogsInfo(data));//获取商品基本信息

                    per_price = parseFloat(data.price);
                    //参团人员列表
                    var div = "";
                    for (var i = 0; i < data.data.length && i < 10; i++) {//已经参团的人
                        div += '<a class="pp_users_item pp_users_normal"><img alt="" src="' + data.data[i].tyimg + '" /></a>';
                    }
                    for (var j = 0; j < data.synum && j < 10; j++) {//空白的头像表示剩余人数
                        div += '<a class="pp_users_item pp_users_blank "><img alt="" src="/images/icon/touxiang.png"></a>';
                    }
                    $("#pp_users").append(div);
                    $("#showYaoheList").append(showYaoheList(data));

                    $("#SharePopup #remain_num b").text(data.synum);//分享弹出层

                    //设置底部按钮
                    BtnState(data);

                    //设置微信分享内容
                    setWxShareInfo(data);
                    //鼓励参加口号
                    var kouhao = "";
                    if (data.synum == 0) {
                        kouhao = '人数已满，开团成功~';
                        $("#t_dbox").hide();
                    } else {
                        kouhao = '还差<b>' + data.synum + '</b> 人，盼你如南方人盼暖气~';
                    }
                    $("#flag_0_a").html(kouhao);

                    //加载截团倒计时
                    if (data.synum > 0) {
                        if (!data.data[0].deadline) {
                            time = data.data[0].end_time.replace('T', ' ').replace(/\-/g, '/');
                        } else {
                            time = data.data[0].deadline.replace('T', ' ').replace(/\-/g, '/');
                        }
                        getRTimeEX(data);
                    } else {
                        $("#flag_0_b").hide();
                    }

                    RenImgList("#pp_users a");

                }
            });
        }



        //加载商品相关信息
        var em = "";
        function getGoogsInfo(data) {
            //alert("1是成功，2是失败，3是已开奖，当前tuanstate状态是：" + data.tuanstate);
            if (data.tuanstate == "1") {//成团
                em = '<em class="i-succeed"></em>';
            }
            else if (data.tuanstate == "2") {//失败
                em = '<em class="i-fail"></em>';
            }
            else if (data.tuanstate == "3") {//已开奖
                em = '<em class="i-kaijiang"></em>';
            }

            return '<div id="group_detail" class="tm">' +
            '<div class="goItemPage">' +
                '<div class="td tuanDetailWrap">' +
                    '<a class="td_img" href="/WxTuan/tuan.item.aspx?tuanid=' + data.data[0].tId + '">' +
                        '<img alt="" src="' + data.imgroot + data.timg + '">' +
                    '</a>' +
                    '<div class="td_info"><a class="goItemPage" href="/WxTuan/tuan.item.aspx?tuanid=' + data.data[0].tId + '">' +
                        '<p class="td_name ">' + data.title + '</p>' +
                        '</a><p class="td_mprice">' +
                            '<span class="tuanTotal">' + data.tnum + '</span>人团<span class="singleprice"><i>¥</i><b>' + parseFloat(data.price) + '</b><i> /件</i></span>' +
                        '</p>' +
                        '<p class="td_num"></p>' +
                    '</div>' +
                '</div>' +
            '</div>' + em +
        '</div>';
        }

        //参团人员详细列表
        function showYaoheList(data) {
            var showYaohediv = "";
            for (var j = 0; j < data.data.length; j++) {
                if (data.data[j].tyname == data.data[j].tzname) {
                    showYaohediv += '<div class="pp_list_item" id="' + data.data[j].uid + '">' +
                        '<img class="pp_list_avatar" alt="" src="' + data.data[j].tyimg + '">' +
                        '<div class="pp_list_info" id="pp_list_info">' +
                            '<span class="pp_list_name">' +
                                //'<span>团长</span>' +
                                '<b>' + data.data[j].tyname + '</b>' +
                            '</span>' +
                            '<span class="pp_list_time">' + data.data[j].start_time.replace('T', ' ') + '<span>开团</span></span>' +
                        '</div>' +
                    '</div>';
                } else {
                    showYaohediv += '<div class="pp_list_item" id="' + data.data[j].uid + '">' +
                        '<img class="pp_list_avatar" alt="" src="' + data.data[j].tyimg + '">' +
                        '<div class="pp_list_info" id="pp_list_info">' +
                            '<span class="pp_list_name">' +
                                '<b>' + data.data[j].tyname + '</b>' +
                            '</span>' +
                            '<span class="pp_list_time">' + data.data[j].time.replace('T', ' ') + '<span>参团</span></span>' +
                        '</div>' +
                    '</div>';
                }
            }
            return showYaohediv;
        }

        //参团人员详细列表格式化
        function RenImgList(picdiv) {
            var picnum = $(picdiv).length;
            var pw = $("#pp_users").width();
            var mar = $(window).width() * 0.005;
            if (picnum > 4) {
                var size = (100 / picnum - 1) / 100 * pw;
                $(picdiv).css({ "width": size, "height": size, "margin-right": mar });
                $(picdiv).find("img").css({ "width": size, "height": size });
            }
        }

        //底部按钮状态
        function BtnState(data) {
            var btn = "";
            if (data.ismy=0) {//没买过
                btn='<a class="fixopt_btn" role="btn_Buy" href="javascript:validateTuanPay();">我要跟团</a>';
            } else if (data.tuanstate == "0") {//组团中
                //btn = '<a class="fixopt_btn" role="btn_Buy" href="javascript:validateTuanPay();">还差' + data.synum + '人成团</a>';
                btn = '<a class="fixopt_btn" role="btn_Buy" href="javascript:validateTuanPay();">立即参团</a>';
            } else if (data.tuanstate == "3") {//已开奖
                btn = '<a class="fixopt_btn fixopt_gree" role="btn_Buy" href="/WxTuan/tuan.WinDetail.aspx?tid=' + data.data[0].tId + '">查看中奖详情</a>';
            } else {//已失败已过期
                btn = '<a class="fixopt_btn fixopt_gray" role="btn_Buy" href="/WxTuan/tuan.index.aspx">看看别的团</a>';
            }
            $(".fixopt_item").append(btn);
        }

        function validateTuanPay() {
            var ajaxUrl = "http://" + window.location.host + "/ilerrpay?action=userpaymenttuan&typeid=2&businessid=" + getUrlParam("tuanlistId");
            $.ajax({
                type: 'get', dataType: 'json', timeout: 10000,
                url: ajaxUrl,
                success: function (data) {
                    //showAlertDiv("4:团购已过期,5:团购数已满,6:已参过此团,当前state状态是：" + data.state);
                    switch (data.state) {
                        case 0:
                            //通过验证
                            location.href = "http://" + window.location.host + "/WxPay/wx_Submit.aspx?businessType=tuan&tuanType=followTuan&money=" + per_price + "&shopid=" + getUrlParam("tuanlistId");
                            break;
                        case 2:
                            //showAlertDiv("数据来源可合法");
                            alert("数据来源可合法");
                            break;
                        case 3:
                            //showAlertDiv("缺少参数");
                            alert("缺少参数");
                            break;
                        case 4:
                            //showAlertDiv("团购已过期");
                            alert("团购已过期");
                            break;
                        case 5:
                            //showAlertDiv("团购数已满");
                            alert("团购数已满");
                            break;
                        case 6:
                            //showAlertDiv("已参过此团");
                            alert("您已经参团，立刻当团长，抽奖不限次！");
                            break;
                    }
                }
            });
        }

        //倒计时方法调用
        function getRTimeEX(data) {
            //var EndTime = new Date('2016/02/1 10:00:00'); //截止时间 
            var EndTime = new Date(time); //截止时间 
            var NowTime = new Date();
            var t = EndTime.getTime() - NowTime.getTime();
            var d = Math.floor(t / 1000 / 60 / 60 / 24);
            var h = Math.floor(t / 1000 / 60 / 60 % 24);
            var m = Math.floor(t / 1000 / 60 % 60);
            var s = Math.floor(t / 1000 % 60);
            if (d <= 0) {
                $("#t_dbox").hide();
            } else {
                $("#t_dbox").show();
            }
            if (d <= 0 && h <= 0 && m <= 0 && s <= 0) { $("#flag_0_b").hide(); return; }
            document.getElementById("t_d").innerHTML = BuLing(d);
            document.getElementById("t_h").innerHTML = BuLing(h);
            document.getElementById("t_m").innerHTML = BuLing(m);
            document.getElementById("t_s").innerHTML = BuLing(s);
        }
        setInterval(getRTimeEX, 1000);
        function BuLing(a) {
            var shijian = "";
            if (a < 10) {
                shijian = "0" + a;
            }
            else { shijian = a; }
            return shijian;
        }



        //设置微信分享内容
        function setWxShareInfo(data) {
            //var $wxShareDiv = $("#wxShareInfo");
            //分享图标,分享标题描述,分享描述,分享url
            var shareImg = data.imgroot + data.data[0].thumb;
            var shareTitleDes = '我花1毛钱团购了“' + data.data[0].title + '”快和我一起拼团吧！';
            var shareDes = "你的好友花1毛钱抢到了" + data.data[0].title  ;
            var link = "http://" + window.location.host + "/WxTuan/tuan.join.aspx?tid=" + getUrlParam("tid") + "&tuanlistId=" + getUrlParam("tuanlistId");
            //alert(shareImg + shareTitleDes + shareDes + link);
            WinXinShareMessage(shareTitleDes, shareDes, link, shareImg);
        }
        
        //页面传参
        function getUrlParam(name) {
            var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)"); //构造一个含有目标参数的正则表达式对象
            var r = window.location.search.substr(1).match(reg);  //匹配目标参数
            if (r != null) return unescape(r[2]); return null; //返回参数值
        }

        //开启右上角微信菜单
        document.addEventListener('WeixinJSBridgeReady', function onBridgeReady() {
            WeixinJSBridge.call('showOptionMenu');
        });
    </script>
</body>

</html>
