//引导分享弹出层
function wxAlertDiv(eventname) {
    var txtBox = '<div class="PopupBg clearfix" id="InfoPopupBg"></div>'+
                '<div class="PopupLayer" id="InfoPopup">'+
                    '<h1>操作提示</h1>'+
                    '<div class="po-nrbox">'+
                        '<h2></h2>'+
                    '</div>'+
                    '<div class="OnebtnBox">'+
                        '<div class="btn">我知道了</div>'+
                    '</div>'+
                '</div>';
    $("body").append(txtBox);
    $("#InfoPopup .btn").click(function () {
        removeAlertDiv(eventname);
    });
}

var windowWidth = document.documentElement.clientWidth;
var windowHeight = document.documentElement.clientHeight;
function showAlertDiv(element,eventname) {
    wxAlertDiv(eventname);
    $("#InfoPopupBg").show();
    $("#InfoPopup").show();
    $("#InfoPopup .po-nrbox h2").text(element);
    var popupHeight = $("#InfoPopup").height();
    var popupWidth = $("#InfoPopup").width();
    $("#InfoPopup").css({
        "top": (windowHeight - popupHeight) / 2,
        "left": (windowWidth - popupWidth) / 2
    });
    return false;
}

function removeAlertDiv(eventname) {
    //alert(reloadPage);
    //if (reloadPage == true) location.href = "http://" + window.location.host + "/index.item.aspx?shopid=" + codeid;
    $("#InfoPopupBg").remove();
    $("#InfoPopup").remove();
    if (eventname !== null || eventname !== undefined || eventname !== '') {
        //var str = "alert('testtesttest');";
        eval(eventname);
    }
    return false;
}