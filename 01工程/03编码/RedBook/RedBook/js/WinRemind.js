
//中奖提醒
function WinRemind() {
    var ajaxUrl = "http://" + window.location.host + "/ilerrorder?action=winningremind";
    $.ajax({
        type: 'get', dataType: 'json', timeout: 10000,
        url: ajaxUrl,
        success: function (e) {
            //showAlertDiv(JSON.stringify(e));
            if (e.iswin == 0) {
                return;
            }
            if (e.iswin == 1) {
                if ($("#winingAlert").length > 0) {
                    $("#winingAlert").remove();
                }
               var WinRemindBox = "";
                WinRemindBox += '<link href="/css/winning.css" rel="stylesheet" type="text/css" />' +
                '<div class="Winning-popup clearfix" id="winingAlert" style="display:block;">' +
                    '<div class="popup_layer-box clearfix">' +
                        '<a href="javascript:;" class="link-close" style="display:block;z-index:10;" >+</a>' +
                        '<div class="fixedbox">' +
                            '<div class="po-winningbox">' +
                                '<div class="Winning-img"><img id="winningImg" src="/images/Winning.png" /></div>' +
                            '</div>' +
                            '<div class="wininfobox">' +
                                '<div class="wininfo clearfix">' +
                                    '<p>爽乐购恭喜您获得</p>' +
                                    '<h4>第<span class="periods">' + e.qishu + '</span>期</h4>' +
                                    '<ul class="productName">' + e.title + '</ul>' +
                                '</div>' +
                                '<div class="twobtn_box2">' +
                                    '<a href="/user.winningconfirm.aspx?shopid=' + e.yid + '&orderid=' + e.orderid + '&productid=' + e.productid + '" class="kanbtn" >查看</a>' +
                                    '<a href="javascript:;" class="sharebtn" >分享</a>' +
                                '</div>' +
                            '</div>' +
                        '</div>' +
                    '</div>' +
                    '<div class="winshare_box" style="display:none;">' +
                        '</div>' +
                '</div>';
                //showAlertDiv('<a href="/user.winningconfirm.aspx?shopid=' + e.yid + '&orderid=' + e.orderid + '&productid=' + e.productid);
                $("body").append(WinRemindBox);
                $(".sharebtn").click(function () {
                    $(".winshare_box").show();
                    $(".popup_layer-box").hide();
                });
                $(".winshare_box").click(function () {
                    $(".winshare_box").hide();
                });
                $(".Winning-popup .link-close,.winshare_box").click(function () {
                    if ($(".winshare_box").is(":visible")) {
                        $(".winshare_box").hide();
                    } else {
                        $(".Winning-popup").hide();
                    }
                });
            }

            setWxShareInfo(e);
        }
    });
}

function setWxShareInfo(data) {
    var shareImg = data.imgroot + data.thumb;
    var shareTitleDes = '我在爽乐购中了'+data.title;
    var shareDes = "来跟我拼一拼手气吧！";
    var link = "http://" + window.location.host + "/index.userindex.aspx?luckyListOpen=1&uid=" + data.uid;
    WinXinShareMessage(shareTitleDes, shareDes, link, shareImg);
    //开启右上角微信菜单
    document.addEventListener('WeixinJSBridgeReady', function onBridgeReady() {
        WeixinJSBridge.call('showOptionMenu');
    });
}