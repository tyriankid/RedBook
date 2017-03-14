

//引导分享弹出层
var txtBox = "";
function wxShareDiv() {
    txtBox = '<div class="mask"  id="wxShareDivbg"></div>' +
            '<div id="wxShareDiv" style="display: none; position: fixed; top: 0; left: 0; z-index: 1000000;">' +
                '<img src="/images/shareWx.png" width="100%" />' +
                //'<img src="/images/wxShare.png" width="100%" />' +
            '</div>';
    $("body").append(txtBox);
    $("#wxShareDivbg").click(function () {
        removeShare();
    });
    $("#wxShareDiv").click(function () {
        removeShare();
    });
}

function showShare() {
    wxShareDiv();
    $("#wxShareDivbg").show();
    $("#wxShareDiv").show();
}

function removeShare() {
    $("#wxShareDivbg").remove();
    $("#wxShareDiv").remove();
}


