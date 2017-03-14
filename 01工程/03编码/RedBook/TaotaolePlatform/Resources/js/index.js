var jsonStr = {
    "0": [
        { "name": "SEO设置", "href": "wx/wxmenu.aspx" },
        {
            "name": "微信基本设置", "href": "pages/system/weixin/wxInterface.html", "child":
               [
                   { "name": "微信菜单", "href": "wx/wxmenu.aspx" },
                   { "name": "自定义回复", "href": "wx/wxreply.aspx" },
               ]
        },
        {
            "name": "用户管理", "href": "a.html", "child":
               [
                   { "name": "用户列表", "href": "member/memberlist.aspx" },
               ]
        },
        {
            "name": "管理员管理", "href": "admin/adminlist.aspx", "child":
               [
                   { "name": "管理员列表", "href": "admin/adminlist.aspx" },
                   { "name": "修改密码", "href": "admin/ResettingPass.aspx" },
                   { "name": "操作日志", "href": "admin/operationLogs.aspx" }
               ]
        },
        {
              "name": "百度推广管理", "href": "baidu/baiducfglist.aspx", "child":
               [
                   { "name": "推广列表", "href": "baidu/baiducfglist.aspx" },
                   { "name": "添加推广", "href": "baidu/addbaiducfglist.aspx" }
               ]
        }

    ],
    "1": [
        {
            "name": "文章管理", "href": "a.html", "child":
               [
                   { "name": "文章列表", "href": "Article/ArticleList.aspx" },
                   { "name": "添加文章", "href": "Article/addArticle.aspx" }
               ]
        },
        {
            "name": "栏目管理", "href": "a.html", "child":
               [
                   { "name": "栏目列表", "href": "productClassify/CategoryList.aspx?action=wzlist" },
                   { "name": "添加栏目", "href": "productClassify/addCategory.aspx?action=addwzfl" }
               ]
        },
    {
        "name": "幻灯片管理", "href": "a.html", "child":
           [
               { "name": "微信", "href": "Slides/SlidesWechat.aspx" },
               { "name": "APP", "href": "Slides/SlidesAPP.aspx" },
               { "name": "直购", "href": "Slides/SlidesMarket.aspx" },
           ]
    },
     {
         "name": "充值卡管理", "href": "a.html", "child":
            [
                { "name": "充值卡列表", "href": "CardRecharge/CardRechargeList.aspx" },
                { "name": "添加充值卡", "href": "CardRecharge/AddCardRecharge.aspx" }
            ]
     }

    ],
    
    "2": [
        {
            "name": "商品基本信息", "href": "a.html", "child":
               [
                   { "name": "商品列表", "href": "products/productList.aspx" },
                   { "name": "添加商品", "href": "products/addProduct.aspx" }
               ]
        },
        {
            "name": "分类管理", "href": "a.html", "child":
               [
                   { "name": "分类列表", "href": "productClassify/CategoryList.aspx" },
                   { "name": "添加分类", "href": "productClassify/addCategory.aspx?action=addspfl" }
               ]
        },
        {
            "name": "品牌管理", "href": "a.html", "child":
               [
                   { "name": "品牌列表", "href": "productClassify/brandList.aspx" },
                   { "name": "添加品牌", "href": "productClassify/addBrand.aspx" }
               ]
        },
        {
            "name": "一元购商品", "href": "a.html", "child":
            [
                { "name": "一元购商品列表", "href": "products/yuangouList.aspx" },
                { "name": "一元购商品回收站", "href": "products/yuangouRecover.aspx" }
            ]
        },

        {
            "name": "团购商品", "href": "a.html","child":
                [
                      { "name": "团购商品列表", "href": "products/tuangouList.aspx" },
                      { "name": "团购商品回收站", "href": "products/tuangouRecover.aspx" }
                ]
        },
        {
            "name": "积分商城", "href": "a.html","child":
                [
                      { "name": "积分商品列表", "href": "products/gifts.aspx" },
                ]
        },
        {
            "name": "直购商城", "href": "a.html", "child":
                [
                      { "name": "直购商品列表", "href": "products/zhiProduct.aspx" },
                       
                ]
        }

    ],
    "3": [
        { 
            "name": "商品订单", "href": "a.html","child":
                [
                    { "name": "订单列表", "href": "OrderClass/orderList.aspx" },
                    { "name": "直购订单", "href": "OrderClass/ZhiGouOrder.aspx" },
                    { "name": "中奖订单", "href": "OrderClass/orderLucky.aspx" },
                    { "name": "未发货订单", "href": "OrderClass/orderNotOut.aspx" },
                    { "name": "订单统计", "href": "a.html" }
                ]
        },
        {
             "name": "账单明细", "href": "a.html", "child":
                     [
                         { "name": "充值记录", "href": "member/rechargerecord.aspx" },
                         { "name": "消费记录", "href": "member/shoppingrecord.aspx" },
                         { "name": "系统账单", "href": "yolly/SystemBillList.aspx" },
                         //{ "name": "永乐账单", "href": "yolly/yollybilllist.aspx" },
                         { "name": "夺宝币消费", "href": "member/ZenPointDetail.aspx" },

                     ]
        }

         ],
    "4": [
        {
            "name": "活动列表", "href": "a.html", "child":
               [
                   { "name": "冲20送118元", "href": "operative/newUserRecharge.aspx" },
                   { "name": "首次支付5元不中包赔", "href": "operative/firstFiveYuan.aspx" },
                   { "name": "拼团抽奖", "href": "operative/tuanbuy.aspx" },
                   { "name": "0元购活动", "href": "operative/zeroBuy.aspx" },
                   { "name": "五折大狂欢", "href": "operative/halfoffCarnival.aspx" },
                   { "name": "买爆款不中包赔", "href": "operative/buyHotCake.aspx" },
                   { "name": "晒单送红包", "href": "operative/shaidan.aspx" },
                   { "name": "睡眠用户定向红包", "href": "operative/sleepuser.aspx" },
                   { "name": "红包基本信息", "href": "operative/redpacklist.aspx" },
                   { "name": "优惠商品红包", "href": "operative/saleProduct.aspx" },
                   { "name": "用户红包列表", "href": "operative/userRedpackList.aspx" }
               ]
        }

    ],
    "5": [
        {
            "name": "渠道管理", "href": "a.html", "child":
            [
                { "name": "服务商列表", "href": "channel/channellist.aspx" },
                { "name": "服务商佣金", "href": "channel/channelrecodes.aspx" },
                { "name": "服务商提现", "href": "channel/channelcashout.aspx" },
                { "name": "渠道商列表", "href": "channel/distributorList.aspx" },
                { "name": "渠道商佣金", "href": "channel/distributorRecodes.aspx" },
                { "name": "网吧活动记录", "href": "channel/activityList.aspx" },
                { "name": "会员兑换记录", "href": "channel/activityDetailList.aspx" },
                { "name": "佣金比例设置", "href": "channel/brokerageSet.aspx" }
            ]
        }
    ],
    "6": [
            {
                "name": "财务分析", "href": "a.html", "child":
                [
                    { "name": "充值排名", "href": "statistics/RechargeStatistics.aspx" },
                    { "name": "消费排名", "href": "statistics/ConsumeStatistics.aspx" },
                ]
            },
            { 
                "name": "用户分析", "href": "a.html", "child":
                [
                    { "name": "用户统计", "href": "statistics/UserStatistics.aspx" },
                ]
            },
            {
                "name": "消费分析", "href": "a.html", "child":
                [
                    { "name": "新增订单", "href": "statistics/OrdersStatistics.aspx" },
                    //{ "name": "日报表", "href": "OrderClass/orderLucky.aspx" },
                    //{ "name": "周报表", "href": "OrderClass/orderNotOut.aspx" },
                    { "name": "月报表", "href": "statistics/OrdersMonthStatistics.aspx" }
                ]
            },
            {
                "name": "活跃分析", "href": "a.html", "child":
                [
                    { "name": "日报表", "href": "statistics/BriskStatistics.aspx" },
                    //{ "name": "周报表", "href": "OrderClass/orderNotOut.aspx" },
                    //{ "name": "月报表", "href": "a.html" }
                ]
            },
    ]
};

function leftbar(index) {
    $(".left-bar ul").html("");
    var liList = "";
    for (var i = 0; i < jsonStr[index].length; i++) {

        var flag = false;
        if (nodes != null) {
            for (var k = 0; k < nodes.length; k++) {

                if (nodes[k].id.length == 4 && nodes[k].name == jsonStr[index][i].name) {
                    flag = true;
                    break;
                }
            }
        }

        if (!flag)
        {
            continue;
        }
        
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

            var flag = false;
            if (nodes != null) {
                for (var k = 0; k < nodes.length; k++) {

                    if (nodes[k].id.length == 6 && nodes[k].name == offspring[i].name) {
                        flag = true;
                        break;
                    }
                }
            }

            if (!flag) {
               
            } else {


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
        }
        son += "</ul>";
    }
    return son;
}

//左侧折叠
function foldUp(e) {
    if ($(e).hasClass('foldUp')) {
        return;
    }
    $('.left-bar>ul>li>span').removeClass('foldUp');
    $(".left-bar ul li ul li span").removeClass("active");
    $(e).addClass('foldUp');
    var that = $(e).next('ul').find('span').eq(0);
    $(that).click();
    //$("#rightIframe").attr("src", $(e).attr("linkUrl"));
    setIfremeSize();
}