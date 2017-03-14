var jsonStr = {
    "0": [
        {
            "name": "个人中心", "href": "", "child":
               [
                   { "name": "个人基本信息", "href": "channel/baseInformation.aspx" },
                   { "name": "修改密码", "href": "channel/changepassword.aspx" },
                   { "name": "佣金明细", "href": "channel/channelrecodes.aspx?agent=true" },
                   { "name": "提现明细", "href": "channel/agentcashout.aspx?agent=true" }
               ]
        },
        {
            "name": "渠道推广管理", "href": "a.html", "child":
               [
                   { "name": "渠道商列表", "href": "channel/distributorList.aspx?agent=true" },
                   { "name": "渠道商佣金明细", "href": "channel/distributorRecodes.aspx?agent=true" }
               ]
        }
    ]
};

function leftbar(index) {
    $(".left-bar ul").html("");
    var liList = "";
    for (var i = 0; i < jsonStr[index].length; i++) {
        var childnum = "", classactive = "";
        if (jsonStr[index][i].child) {
            childnum = "<i>" + jsonStr[index][i].child.length + "</i>";
        }
        if (i == 0) {
            classactive = "class='foldUp'";
        }
        liList += "<li><span onclick='foldUp(this)' " + classactive + " linkUrl='" + jsonStr[index][i].href + "'>" + jsonStr[index][i].name + childnum + "</span>";
        var son = "";
        son += childmenu(jsonStr[index][i].child, son);
        liList += son;
        liList += "</li>";
    }
    $(".left-bar ul").append(liList);
}

//递归
function childmenu(offspring, son) {
    //offspring后代    sonstr儿子    grandson孙子
    if (offspring) {
        son += "<ul>";
        for (var i = 0; i < offspring.length; i++) {
            son += "<li><span onclick='setIframe(this)' linkUrl='" + offspring[i].href + "'>" + offspring[i].name + "</span>";
            //孙子辈
            if (offspring[i].child) {
                var grandson = ""
                grandson += childmenu(offspring[i].child, grandson);
                son += grandson;
            }
            //孙子辈END
            son += "</li>";
        }
        son += "</ul>";
    }
    return son;
}

//左侧折叠
function foldUp(e) {
    $('.left-bar>ul>li>span').removeClass('foldUp');
    $(e).addClass('foldUp');
    if ($(e).next().length > 0) {
        //		$(e).next().slideToggle('fast');
        //		if($(e).hasClass('foldUp')){
        //			$("#rightIframe").attr("src",$(e).attr("linkUrl"));
        //		}
    } else {
        $("#rightIframe").attr("src", $(e).attr("linkUrl"));
        setIfremeSize();
    }
    $(".left-bar ul li ul li span").removeClass("active");
    setIfremeSize();
}