//提醒关注
function GuanzhuRemind() {
    var IsTrue = true;
    var ajaxUrl = "http://" + window.location.host + "/ilerruser?action=getsubscribe";
    $.ajax({
        type: 'get', dataType: 'json', timeout: 10000, async: false,
        url: ajaxUrl,
        success: function (e) {
            if (e.state == 0) {
                var txtBox = "";
                txtBox += '<div class="GuanzhuPopup clearfix" id="GuanzhuPopup" style="display:block;">' +
                     '<div class="Poplayer clearfix">' +
                         '<div class="fixedbox">' +
                            '<h1>支付成功！</h1>' +
                                 '<div class="ewmbox"><img src="/images/icon/qrcode_taotaole.jpg" /></div>' +
                                '<p>您目前尚未关注“爽乐购”官方微信号！<br />为了方便您领取奖品请先关注， <br />长按识别图中二维码，点击关注即可！</p>' +
                             '</div>' +
                     '</div>' +
                 '</div>';
                $("body").append(txtBox);
                IsTrue = false
                //return false;
            }
        }
    });
    return IsTrue;
}