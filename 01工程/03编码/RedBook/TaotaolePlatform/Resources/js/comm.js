
function setIframe(e) {
    $('.left-bar>ul>li>span').removeClass('foldUp');
    $(e).parent().parent().prev().addClass('foldUp');
    $(".left-bar ul li ul li span").removeClass("active");
    $(e).addClass("active")
    $("#rightIframe").attr("src", $(e).attr("linkUrl"));
    setIfremeSize();
}

function setIfremeSize() {
    $("#rightIframe").load(function () {
        if ($("#rightIframe").contents().find("body").height() >= $(".right-bar").height()) {
            $("#rightIframe").width($(".right-bar").width() + 15);
        } else {
            $("#rightIframe").width($(".right-bar").width());
        }
        $("#rightIframe").height($(".right-bar").height());
    })
}

function openNewWin(url) {
    window.parent.document.getElementById('rightIframe').src = url;
    setIfremeSize();
}

window.onresize = function () {
    if ($("#rightIframe").contents().find("body").height() > $(".right-bar").height()) {
        $("#rightIframe").width($(".right-bar").width() + 15);
    } else {
        $("#rightIframe").width($(".right-bar").width());
    }
    $("#rightIframe").height($(".right-bar").height());
}


///////////////////////////////////////////////////////////////////////////////////
// URL Helper
///////////////////////////////////////////////////////////////////////////////////
function GetQueryString(key) {
    var url = location.href;
    if (url.indexOf("?") <= 0) {
        return "";
    }

    var paraString = url.substring(url.indexOf("?") + 1, url.length).split("&");
    var paraObj = {};

    for (i1 = 0; j = paraString[i1]; i1++) {
        paraObj[j.substring(0, j.indexOf("=")).toLowerCase()] = j.substring(j.indexOf("=") + 1, j.length);
    }

    var returnValue = paraObj[key.toLowerCase()];
    if (typeof (returnValue) == "undefined") {
        return "";
    } else {
        return returnValue;
    }
}

function GetQueryStringKeys() {
    var keys = {};
    var url = location.href;

    if (url.indexOf("?") <= 0) {
        return keys;
    }

    keys = url.substring(url.indexOf("?") + 1, url.length).split("&");
    for (i2 = 0; i2 < keys.length; i2++) {
        if (keys[i2].indexOf("=") >= 0) {
            keys[i2] = keys[i2].substring(0, keys[i2].indexOf("="));
        }
    }

    return keys;
}

function GetCurrentUrl() {
    var url = location.href;

    if (url.indexOf("?") >= 0) {
        return url.substring(0, url.indexOf("?"));
    }

    return url;
}

function AppendParameter(key, pvalue) {
    var reg = /^[0-9]*[1-9][0-9]*$/;
    var url = GetCurrentUrl() + "?";
    var keys = GetQueryStringKeys();

    if (keys.length > 0) {
        for (i3 = 0; i3 < keys.length; i3++) {
            if (keys[i3] != key) {
                url += keys[i3] + "=" + GetQueryString(keys[i3]) + "&";
            }
        }
    }

    if (!reg.test(pvalue)) {
        alert_h("只能输入正整数");
        return url.substring(0, url.length - 1);
    }

    url += key + "=" + pvalue;
    return url;
}