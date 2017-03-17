<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="tuan.join.pay.aspx.cs" Inherits="RedBook.WxTuan.tuan_join_pay" %>

<!DOCTYPE html>
<html>

<head>
    <meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
    <title>参团支付</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, user-scalable=no, maximum-scale=1.0" />
    <meta content="yes" name="apple-mobile-web-app-capable" />
    <meta content="black" name="apple-mobile-web-app-status-bar-style" />
    <meta content="telephone=no" name="format-detection" />
    <link href="/css/comm.css" rel="stylesheet" type="text/css" />
    <link href="/css/tuan.css" rel="stylesheet" type="text/css" />
    <link href="/css/member.css?v=130726" rel="stylesheet" type="text/css" />
    <link href="/css/city.css" rel="stylesheet" type="text/css" />
    <script src="/js/jquery-1.8.2.js" language="javascript" type="text/javascript"></script>
    <script src="/js/fastclick.js"></script>
    <script src="/js/city.min.js" language="javascript" type="text/javascript"></script>
    <script id="pageJS" data="/js/RechargeTuan.js" language="javascript" type="text/javascript"></script>

</head>
<script>
    $(function () {
        var flag = '{wc:$flag}';
        if (flag == 1) {
            showShare();
        }

        $(".bshare-qzone").click(function () {
            var _url = 'http://www.baidu.com';
            var _showcount = 1;
            var _desc = 'share';
            var _summary = 'share';
            var _title = 'share';
            var _site = 'share';
            var _pic = '';
            var _width = document.body.clientWidth;
            var _height = document.body.clientHeight;
            var _shareUrl = 'http://sns.qzone.qq.com/cgi-bin/qzshare/cgi_qzshare_onekey?';
            _shareUrl += 'url=' + encodeURIComponent(_url || document.location);   //参数url设置分享的内容链接|默认当前页location
            _shareUrl += '&showcount=' + _showcount || 0;      //参数showcount是否显示分享总数,显示：'1'，不显示：'0'，默认不显示
            _shareUrl += '&desc=' + encodeURIComponent(_desc || '分享的描述');    //参数desc设置分享的描述，可选参数
            _shareUrl += '&summary=' + encodeURIComponent(_summary || '分享摘要');    //参数summary设置分享摘要，可选参数
            _shareUrl += '&title=' + encodeURIComponent(_title || document.title);    //参数title设置分享标题，可选参数
            _shareUrl += '&site=' + encodeURIComponent(_site || '');   //参数site设置分享来源，可选参数
            _shareUrl += '&pics=' + encodeURIComponent(_pic || '');   //参数pics设置分享图片的路径，多张图片以＂|＂隔开，可选参数
            window.open(_shareUrl, '_blank', 'width=' + _width + ',height=' + _height + ',top=' + (screen.height - _height) / 2 + ',left=' + (screen.width - _width) / 2 + ',toolbar=no,menubar=no,scrollbars=no,resizable=1,location=no,status=0');
        })

        $(".bshare-sinaminiblog").click(function () {
            var _url = window.location;
            var _title = 'title';
            var _source = 'source';
            var _sourceUrl = '';
            var _pic = '';
            var _width = document.body.clientWidth;
            var _height = document.body.clientHeight;
            var _shareUrl = 'http://v.t.sina.com.cn/share/share.php?&appkey=895033136';     //真实的appkey ，必选参数
            _shareUrl += '&url=' + encodeURIComponent(_url || document.location);     //参数url设置分享的内容链接|默认当前页location，可选参数
            _shareUrl += '&title=' + encodeURIComponent(_title || document.title);    //参数title设置分享的标题|默认当前页标题，可选参数
            _shareUrl += '&source=' + encodeURIComponent(_source || '');
            _shareUrl += '&sourceUrl=' + encodeURIComponent(_sourceUrl || '');
            _shareUrl += '&content=' + 'utf-8';   //参数content设置页面编码gb2312|utf-8，可选参数
            _shareUrl += '&pic=' + encodeURIComponent(_pic || '');  //参数pic设置图片链接|默认为空，可选参数
            window.open(_shareUrl, '_blank', 'toolbar=no,menubar=no,scrollbars=no,resizable=1,location=no,status=0,' + 'width=' + _width + ',height=' + _height + ',top=' + (screen.height - _height) / 2 + ',left=' + (screen.width - _width) / 2);
        })
    })
</script>
<body>
    <header class="header">
        <h2>订单详情</h2>
        <a id="A1" class="cefenlei" onclick="history.go(-1)" href="javascript:;">
            <img width="30" height="30" src="/images/fanhui.png">
        </a>
        <div class="head-r"><a href="javascript:showShare();" class="z-share"></a></div>
    </header>
    <!--QQ分享-->

    <script type="text/javascript">
        (function () {
            var p = {
                url: location.href, /*获取URL，可加上来自分享到QQ标识，方便统计*/
                desc: '非常便宜', /*分享理由(风格应模拟用户对话),支持多分享语随机展现（使用|分隔）*/
                title: '一起来参加吧', /*分享标题(可选)*/
                summary: '', /*分享摘要(可选)*/
                pics: '', /*分享图片(可选)*/
                flash: '', /*视频地址(可选)*/
                site: '', /*分享来源(可选) 如：QQ分享*/
                style: '201',
                width: 32,
                height: 32
            };
            var s = [];
            for (var i in p) {
                s.push(i + '=' + encodeURIComponent(p[i] || ''));
            }
            console.log(s);
            console.log("http://connect.qq.com/widget/shareqq/index.html?" + s.join('&'));
            var url = "http://connect.qq.com/widget/shareqq/index.html?" + s.join('&');

        })();
    </script>
    <script src="http://connect.qq.com/widget/loader/loader.js" widget="shareqq" charset="utf-8"></script>
    <!--QQ分享-->

    <section class="tuanlistbox">
        <div class="order_modified">
            <div class="order_goods">
                <div class="order_goods_img">
                    <img alt="" src="">
                </div>
                <div class="order_goods_info">
                    <div class="order_goods_name">{wc:$tuanInfo['title']}</div>
                    <div class="order_goods_attr">
                        <div class="order_goods_attr_item">

                            <div class="order_goods_price">￥{wc:$tuanInfo['per_price']}元<i>/件</i></div>
                        </div>
                        <p class="order_goods_attr_item"><span id="skuLast"></span><i></i></p>
                    </div>
                </div>
            </div>
            <div class="totalprice">价格：<span id="totalPrice" class="total_price">￥{wc:$tuanInfo['per_price']}元</span></div>
        </div>

        <div class="tuanpayway">
            <input name="hidBalance" type="hidden" id="type" value="join" />
            <input name="hidBalance" type="hidden" id="hidmoney" value="" />
            <input name="hidBalance" type="hidden" id="session" value="" />
            <input name="hidBalance" type="hidden" id="tid" value="" />

            <div class="pay2">
                <article class="clearfix mt10 g-pay-ment g-bank-ct">
                    <ul id="ulBankList">

						 		<li id="{wc:$pay['pay_class']}" class="gray9 tuan-pay" urm="{wc:$pay['pay_id']}">
                                     <div class="tishi"><span class="p1">{wc:$pay['pay_name']}</span><br />
                                         <span class="p2">{wc:$pay['pay_des']}</span></div>
                                     <i class="z-bank-Round" role="check" style="line-height: 17px;"><s></s></i>
                                     <!--{wc:$pay['pay_name']} CMBCHINA-WAP -->
                                 </li>

                    </ul>
                </article>
            </div>
        </div>
        <div class="step">
            <div class="step_hd">拼团玩法</div>
            <div id="footItem" class="step_list">
                <div class="step_item">
                    <div class="step_num">1</div>
                    <div class="step_detail">
                        <p class="step_tit">选择心仪商品</p>
                    </div>
                </div>
                <div class="step_item step_item_on">
                    <div class="step_num">2</div>
                    <div class="step_detail">
                        <p class="step_tit">支付开团或参团</p>
                    </div>
                </div>
                <div class="step_item">
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
    <section style="height: 45px; line-height: 45px;" class="buynow" id="btnSubmit">
        <span>实付款：<i>￥{wc:$tuanInfo['per_price']}</i></span><a id="a-btn" mid="1">确认支付</a>
    </section>

    <script type="text/javascript">
        $(function () {
            FastClick.attach(document.body);
        })
        $(".m-addr-close").click(function () {
            $("#m-addr-mask").hide();
        });
        function showPopu() {
            $("#m-addr-mask").show();
        }
    </script>

    <!-- 分享弹出层 Start-->
    <div class="popup_share-bg clearfix" id="SharePopup">
        <div class="popup_share-box clearfix">
            <div class="Txtips">
                <h1>还差<b>{wc:$Info['remain_num']}</b>人就能组团成功</h1>
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
    </div>
    <script type="text/javascript">
        function hideShare() {
            $("#SharePopup").hide();
        }

        function showShare() {
            $("#SharePopup").show();
            $("m-addr-save").submit()
        }
    </script>
    <!-- 分享弹出层 End-->

    <script type="text/javascript">
        // 支付 单击样式样式
        $("#btn_ALI").click(function () {
            changeSelected($(this));
        });
        $("#btn_WX").click(function () {
            changeSelected($(this));
        });
        function changeSelected(e) {
            e.addClass("pay2_wx pay2_selected");
            e.find("span").eq(0).addClass("pay2_item_state");
            e.siblings().removeClass("pay2_wx pay2_selected");
            e.siblings().find("span").eq(0).removeClass("pay2_item_state");
        }
        /*
        $(".pay2_list-oc div").click(function() {
            $(this).toggleClass("pay2_wx pay2_selected");
            $('.pay2_list-oc div .pay2_item_state_default').toggleClass("pay2_wx pay2_item_state");
        });
        */
    </script>

</body>

</html>
