var docEl = document.documentElement,
        resizeEvt = 'orientationchange' in window ? 'orientationchange' : 'resize',
        recalc = function () {
            docEl.style.fontSize = 14 * (docEl.clientWidth / 350) + 'px';
        };
window.addEventListener(resizeEvt, recalc, false);
document.addEventListener('DOMContentLoaded', recalc, false);

$(function () {
    $(".hot-question li p").on("click", function () {
        $(this).find("span").toggleClass("rotation").end().next("div").slideToggle();
        var font_size = $("html").css("font-size");
        var index = font_size.indexOf("px");
        var top_h = font_size.substring(0, index) * 3;
        var t = $(this).offset().top;
        $("html,body").animate({ scrollTop: t - 46 }, "fast");
    });
});