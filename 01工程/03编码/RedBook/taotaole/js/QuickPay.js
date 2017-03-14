/**
 * 一元购快速支付方法,用于页面弹出快速支付框,并完成支付功能
 */

function minusBuyNum() {
    var n = $("#buyNum").val();
    var result = subtract(n);
    $("#buyNum").val(result);
    $("[role='happyBean']").html(result);
}

function addBuyNum() {
    var $quickPop = $("#popup1");
    var n = $("#buyNum").val();
    var result = plus(n);
    if (parseInt($quickPop.attr('renshuLeft') * yunjiage) >= result) {
        $("#buyNum").val(result);
        $("[role='happyBean']").html(result);
    }
    else {
        //$.PageDialog.fail("超过", $quickPop, 0,0);
    }

}
/*
$(fatherDiv).on("click",".pro-numlist li",function(){
    if($(this).find('a').attr('dis')!="true")
    {
        $("#buyNum").val($(this).text());
        $("[role='happyBean']").html($(this).text());
    }
})
*/


function quickBuy(t) {
    if ($(t).find('a').attr('dis') == "true") return;
    var result = $(t).text() * yunjiage;
    $("#buyNum").val(result);
    $("[role='happyBean']").html(result);

}
function plus(n) {
    n = parseInt(n);
    //n=n+1;
    //累加单价_160722
    if ((n % parseInt(yunJiage))) {   //支付金额非单价整数倍时，转化为整数倍价格
        n = (parseInt(n / parseInt(yunJiage)) + 1) * parseInt(yunJiage);
    }
    n += parseInt(yunJiage);
    return n;
}
function subtract(n) {
    n = parseInt(n);
    //if (n > 0) {//n=n-1;
    //累减单价_160722
    if ((n % parseInt(yunJiage))) {   //支付金额非单价整数倍时，转化为整数倍价格
        n = (parseInt(n / parseInt(yunJiage)) + 1) * parseInt(yunJiage);
    }
    if (n > parseInt(yunJiage)) {//最少购买一个单价_160722
        n -= parseInt(yunJiage);
    }
    return n;
}
function trim(s) {
    if (s.length > 0) {
        if (s.charAt(0) == " ")
            s = s.substring(1, s.length);
        if (s.charAt(s.length - 1) == " ")
            s = s.substring(0, s.length - 1);

        if (s.charAt(0) == " " || s.charAt(s.length - 1) == " ")
            return trim(s);
    }
    return s;
}


// 创建加载闭包
function lockCoverDivMOdel(fn) {
    var resultDiv;
    return function () {
        return resultDiv = resultDiv || (resultDiv = fn.apply(this, arguments));
    }
}
/*
 *展示快速购买弹出层,传递参数:图片,商品id,商品单价,商品区间,购买人数剩余值
 */
//快速支付操作匡
var leftmoney, paytype, redpacknum; //支付时的全局变量

var canPay = 0;
var imgUrl, codeId, yunJiage, priceRangeArray, renshuLeft;
var codeid, yunjiage, renshuleft;//支付需要属性
var sign, shopid, quantity, paytype, APIsubmitcode, redpaper;//支付接口属性
var showQuickPayDiv = lockCoverDivMOdel(function () {
    var quickPayDiv = "	<div class='popup_layer-bg clearfix' id='popup1' style='display: none;' >";
    quickPayDiv += "<div class='popup_layer-box clearfix'>";
    quickPayDiv += "<div class='popup-nrbox2'><div class='pro-ttbox'>";
    quickPayDiv += "<img class='pro-img' role='quickBuyImg' src='' />";
    quickPayDiv += "<h1>请选择参与人次</h1><a href='javascript:;' class='link-close' onclick='CloseDiv(\"all\")' >+</a>";
    quickPayDiv += "</div><div class='pro-infobox'><div class='pro-num'><span class='shopcart-minus' onclick='minusBuyNum()'>-</span>";
    quickPayDiv += "<input type='tel' id='buyNum' class='form-num' onkeyup=\"value=value.replace(/\\D/g,'')\" value='1'>";
    quickPayDiv += "<span class='shopcart-add' onclick='addBuyNum()'>+</span>";
    quickPayDiv += "</div>";
    quickPayDiv += "<p role='singlePriceAlert'></p>";//当单人次价格大于1的时候,给出相应的提示
    quickPayDiv += "<ul class='pro-numlist clearfix' id='ulpricerange'></ul>";
    //quickPayDiv += "<div class='sharebtn' id='sharebtn'>共<b role='happyBean'>1</b>";
    //quickPayDiv += "<span class='select' >爽乐币</span><span>夺宝币</span>"
    quickPayDiv += "<div class='pro-money clearfix'>共<b role='happyBean'>1</b>爽乐币";
    quickPayDiv += "</div><div class='twobtn_box'>";
    quickPayDiv += "<a href='javascript:;' class='closebtn' onclick='pay(leftmoney,paytype,redpacknum)'>确定</a>";
    quickPayDiv += "</div></div></div> </div> </div>";

    codeid = codeId; yunjiage = yunJiage; renshuleft = renshuLeft;
    if ((yunjiage + " ").indexOf(".") >= 0) yunjiage = yunjiage.replace(".00", "");//去除小数点_160722

    yunJiage = yunjiage;//当前全局变量后续还需引用_160722
    if (canPay == 1) return;
    $("body").append(quickPayDiv).append(alertboxHtml);
    //$(".sharebtn span").on("click", function () {
    //    $(this).addClass("select").siblings("span").removeClass("select")
    //})
    //更新单价_160722
    if (yunjiage != 1) {
        $("#buyNum").val(yunjiage);
        $("#popup1").find("[role='happyBean']").text(yunjiage);
    }
    //当单人次价格大于1的时候,给出相应的提示
    if (yunjiage > 1) $("[role='singlePriceAlert']").html("单人次需消耗" + yunjiage + "爽乐币");

    var $quickPop = $("#popup1");
    $quickPop.css('display', 'block');
    //传递图片
    $quickPop.find("[role='quickBuyImg']").attr('src', imgUrl);
    //传递codeid
    $quickPop.find(".closebtn").attr('codeid', codeId).attr('yunjiage', yunJiage);
    //动态设置快速购买价格区域
    $("#ulpricerange").html("");
    var re = /^\\d+$/;
    //var pricerangeArray = priceRangeArray.split(",");
    for (var i = 0; i < priceRangeArray.length; i++) {
        if (isNaN(priceRangeArray[i])) continue;
        $("#ulpricerange").append('<li onclick="quickBuy(this)"><a href="javascript:;">' + priceRangeArray[i] + '</a></li>');
    }
    //传递剩余值
    $quickPop.attr('renshuLeft', renshuLeft);
    //根据当前剩余人数屏蔽按钮
    $quickPop.find(".pro-numlist li").each(function () {
        if (parseInt(renshuLeft) < parseInt($(this).find('a').html())) {
            $(this).html(("<a style='background:#DDD' dis='true'>" + $(this).find('a').html() + "</a>"));
            //$(this).attr('disabled','disabled');
        }
        else {
            $(this).html(("<a >" + $(this).find('a').html() + "</a>"));
        }
    });
    canPay = 1;//可以支付状态
});

var reloadPage = false;
function pay(leftmoney, payType, redpackNum) {
    if (canPay == 0) return false;//如果为不可支付状态,返回.(用于防止快速点击按钮重复提交)
    canPay = 0;
    var $inputBuyNum = $("#buyNum");
    var Money = trim($inputBuyNum.val());
    if (Money == '') {
        //showAlertDiv("请输入购买数量!"); CloseDiv(); return false;
        alertBox("请输入购买数量!"); CloseDiv(); return false;
    } else if (Money > parseInt($("#popup1").attr('renshuLeft')) * yunjiage) {
        $inputBuyNum.val($("#popup1").attr('renshuLeft') * yunjiage);
    }

    var codeid = $(this).attr('codeid');
    //文本框显示的数据即为待支付金额_160722
    //perMoney = parseInt($(this).attr('yunjiage'));//这里计算可能产生了重复_160722
    //payMoney = buyNum * perMoney;//这里计算可能产生了重复_160722


    //加入合法性校验_160722【参考网易，自动转化为合理数据】
    if (Money < parseInt(yunJiage)) {
        Money = parseInt(yunJiage);    //支付为0或小于单价时，自动转为单价
    }
    else if ((Money % parseInt(yunJiage))) {   //支付金额非单价整数倍时，转化为整数倍价格
        Money = parseInt(Money / parseInt(yunJiage)) * yunjiage;
    }

    if (Money > parseInt($("#popup1").attr('renshuLeft')) * yunjiage) {
        Money = parseInt($("#popup1").attr('renshuLeft')) * yunjiage;
    }
    buyNum = parseInt(Money / yunjiage);
    //提交支付前，转化为程序变量（文本框金额->购买数量，支付金额->单价）_160722
    //如果购买数量超过5000人次,将最大值设置为5000
    if (buyNum > 5000) {
        if (confirm("单次购买人次不得超过5000,是否将购买人次设为5000？")) {
            buyNum = 5000;
            $("#buyNum").val(5000 * yunjiage);
        }
        else {
            CloseDiv(); return;
        }
    }
    $("#buyNum").val(buyNum * yunjiage);
    //判断是爽乐币支付还是夺宝币支付
    //var payZeng = "";
    //obj = document.getElementById('sharebtn').getElementsByTagName('span');
    //    class_name ="select";
    //    for (i in obj) {
    //        if (obj[i].className == class_name) {
    //            if (obj[i].innerHTML == "夺宝币")
    //            {
    //                payZeng="zeng"
    //            }
    //        }
    //    }
   
    //校验商品是否过期
    shopid = codeid;
    quantity = buyNum;
    redpaper = 0;
    APIsubmitcode = "";
    paytype = "0";

    var ajaxUrl = "http://" + window.location.host + '/ilerrpay?action=userpayment&shopid=' + shopid + '&quantity=' + quantity;
    var isQuickPay;
    var isoutpay=false;
    $.ajax({
        type: 'get', dataType: 'json', timeout: 10000, async: false, url: ajaxUrl, success: function (data) {
            if (data.isPayOut == "11") {
                if (!confirm("支付失败,您今日已消费达5000元,确定继续支付吗?"))
                {
                    isoutpay = true;
                    CloseDiv();
                }
            }
            if (data.usemoney == "1" && data.useredpaper == "0") {//余额足够且无可用红包,快速支付
                isQuickPay = true;
            }
            else {
                isQuickPay = false;
            }
        }
    });
    if (isoutpay)
    {
        return;
    }
    //如果余额足够并且没有红包,直接用余额支付
    if (isQuickPay) {
        $(".closebtn").html("正在支付中...");
        var ip = "";
        $.getJSON("http://" + window.location.host + '/ilerrpay?action=userpaymentsubmit&shopid=' + shopid + '&quantity=' + quantity + '&paytype=' + paytype + '&ip=' + ip, function (data) {
            switch (data.state) {
                case 0://验证通过
                    CloseDiv();
                    alertBox("支付成功!", callQuickPay(payType));
                    break;
                default:
                    alertBox("支付失败!错误码：" + data.state);
                    break;
            }
            CloseDiv();
        });
    }
        //否则跳转到付款页面
    else {
        location.href = "http://" + window.location.host + "/WxPay/cart.payment.aspx?yid=" + shopid + "&quantity=" + quantity;
    }
}

function callQuickPay(payType) {
    switch (payType) {
        case "ajaxPay"://如果是首页的无刷新支付,支付成功后刷新页面
            if (!GuanzhuRemind()) return;
            getrefdata();
            //getdata(true);
            getLottery(true);
            break;
        case "singleAjaxPay":
            if (!GuanzhuRemind()) return;
            reloadPage = true;
            break;
        case "allgoods":
            if (!GuanzhuRemind()) return;
            glist_json(id);
            break;
    }
}



function CloseDiv(type) {
    $("#popup1").remove();
    if (type == "all") {
        $('.mask').remove();
        $('.alertBox').remove();
    }
    canPay = 0;
    /*
    document.getElementById("popup1").style.display='none';
    $("#buyNum").val(1);
    $("[role='happyBean']").html(1);
    */
};

var alertboxHtml = "<div class='mask'></div>";
alertboxHtml += "<div class='alertBox'>";
alertboxHtml += "<p class='content'>请选择支付方式</p>";
alertboxHtml += "<p class='close' onclick='closeAlertBox()'>关闭</p></div>";

function alertBox(content, evntname) {
    $('.alertBox .content').text(content);
    $('.mask').show();
    $('.alertBox').show();
}

function closeAlertBox(eventname) {
    if (reloadPage == true) location.href = "http://" + window.location.host + "/index.item.aspx?shopid=" + codeid;
    $('.mask').remove();
    $('.alertBox').remove();
    if (eventname !== null || eventname !== undefined || eventname !== '') {
        //var str = "alert('testtesttest');";
        eval(eventname);
    }
    return false;
}
