<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="user.gameaddress.aspx.cs" Inherits="RedBook.user_gameaddress" %>

<!DOCTYPE html>
<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width,initial-scale=1,minimum-scale=1,maximum-scale=1,user-scalable=no" />
    <title>游戏地址管理</title>
    <link href="css/comm.css?v=130715" rel="stylesheet" type="text/css" />
    <link href="css/member.css?v=130726" rel="stylesheet" type="text/css" />
    <link href="css/address.css" rel="stylesheet" type="text/css" />
    <script src="js/jquery190.js" language="javascript" type="text/javascript"></script>
    <script type="text/javascript" src="js/gameArea.js"></script>
    <script src="/js/fastclick.js"></script>

</head>
<body>
    <header class="header">
        <h2>游戏地址管理</h2>
        <a class="cefenlei" href="/user.index.aspx">
            <img width="30" height="30" src="/images/fanhui.png">
        </a>
        <div class="head-r"><a href="/index.aspx" class="fr_Home"></a></div>
    </header>


    <!-- 收货地址列表 -->
    <div id="addressListDiv" class="detailAddress" style="">
        <ul class="AddrListUlBox" id="AddrListUlBox">
        </ul>
    </div>
    <!-- 收货地址列表 End -->

    <!-- 新增收货地址 -->
    <div class="add-dizbtnbox">
        <a id="btnAddnewAddr" href="javascript:;" type="button" class="orangebut orgBtn" style="display: block;">新增游戏地址</a>
    </div>
    <div id="div_consignee" class="addAddress" style="display: none; position: absolute; top: 50px; left: 0px; width: 100%;">
        <article class="mt10">
            <form class="registerform" method="post" action="" id="AddAddressForm">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tbody>
                        <tr class="hanggao">
                            <td width="20%" class="nowrtxt">
                                <label><em id="Em1" class="red"></em>游戏类型</label>
                                <script>var s2 = ["gamename", "fuwuqi", "daqu"];</script>
                            </td>
                            <td>
                                <select class="select" id="gamename" runat="server" name="gamename"></select>
                            </td>
                                
                        </tr>
                        <tr>
                            <td>
                                <label><em id="Em2" class="red"></em>服务器</label>
                            </td>
                            <td>
                                <select class="select" id="fuwuqi" runat="server" name="fuwuqi"></select>
                            </td>  
                        </tr>
                        <tr>
                            <td>
                                <label><em id="Em4" class="red"></em>游戏大区</label>
                            </td>
                            <td>
                                <select class="select" id="daqu" runat="server" name="daqu"></select>
                                <script type="text/javascript">setup2()</script>
                            </td>  
                        </tr>
                        <tr class="hanggao">
                            <td class="nowrtxt">
                                <em class="red" id="Em5"></em><label>游戏账号</label>
                            </td>
                            <td>
                                <input name="gamenum" type="text" maxlength="20" id="gamenum" class="input" value="">
                            </td>
                        </tr>
                        <tr class="hanggao" style="display:none;">
                            <td class="nowrtxt">
                                <em class="red" id="Em3"></em><label>游戏昵称</label>
                            </td>
                            <td>
                                <input name="shouhuoren" type="text" maxlength="20" id="shouhuoren" class="input" value="爽乐购">
                            </td>
                        </tr>
                        <tr class="hanggao">
                            <td class="nowrtxt">
                                <label><em id="Em6" class="red"></em>手机号码</label>
                            </td>
                            <td>
                                <input name="mobile" id="mobile" type="text" class="input" maxlength="11">
                            </td>
                        </tr>
                        <tr style="height: 100px;">
                            <td colspan="2">
                                <input style="margin-right: 1%; background: #fd524e; border-radius: 5px; cursor: pointer;" name="submit" type="submit" class="orangebut" onclick="return validate()" id="btn_consignee_save" value="保存" title="保存">
                                <input style="border-radius: 5px; cursor: pointer;" type="button" class="cancelBtn" value="取消" id="btn_consignee_cancle" title="取消"></td>
                        </tr>
                    </tbody>
                </table>
            </form>
        </article>
    </div>
    <!-- 新增收货地址 End -->

    <div style="height: 70px;"></div>

    <!-- 页面消息提示弹出层 S -->
    <div class="PopupBg clearfix" id="AddressPopBg"></div>
    <div class="PopupLayer" id="AddressPop">
        <h1>操作提示</h1>
        <div class="po-nrbox">
            <%--<p>收货地址一经确认，不可修改！</p>
            <h2>您是否要充值到当前账户？</h2>--%>
        </div>
        <div class="TwobtnBox">
            <a href="javascript:;" class="Cancelbtn">取消</a>
            <a href="javascript:;" class="Surebtn">确定</a>
        </div>
    </div>
    <!-- 页面消息提示弹出层 E -->

</body>

<script>
    var windowWidth = document.documentElement.clientWidth;
    var windowHeight = document.documentElement.clientHeight;

    //页面传参
    function getUrlParam(name) {
        var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)"); //构造一个含有目标参数的正则表达式对象
        var r = window.location.search.substr(1).match(reg);  //匹配目标参数
        if (r != null) return unescape(r[2]); return null; //返回参数值
    }
    var orderid = getUrlParam("orderid");
    var shopid = getUrlParam("shopid");
    var productid = getUrlParam("productid");
    var gameaddressid = getUrlParam("gameaddressid");
    var pageFrom = getUrlParam("pageFrom");

    //读取游戏地址
    function GameAddrList() {
        var ajaxUrl = "http://" + window.location.host + "/ilerruser?action=usergameaddresslist";
        $.ajax({
            type: 'get', dataType: 'json', timeout: 10000,
            url: ajaxUrl,
            success: function (e) {
                GameAddrListEx(e);
            },
            error: function (me) {
                alert("获取失败！");
            }
        });
    }

    var moren = "";//是否是默认地址
    var shouhuoMobile = "",//提交地址信息
        shouhuoRenDiz = "";
    //读取收货地址_结果处理
    function GameAddrListEx(e) {
        var li = "";
        if (e.state == 3) {
            $("#addressListDiv #AddrListUlBox").html("<div style='text-align:center;padding-top:30px;font-size:15px;color:#666;'>暂未添加游戏地址</div>");
        }
        if (e.state == 0) {//获取地址成功
            for (var i = 0; i < e.data.length; i++) {

                if (e.data[i].isdefault == "Y") {
                    moren = '<span class="moren">默认地址</span>';
                } else {
                    moren = '<a class="shemoren" href="javascript:;" onclick="setdf(' + e.data[i].id + ')" title="设为默认">设为默认</a>';
                }
                if (!orderid || orderid == "null" || orderid == "" || typeof (orderid) == "undefined") {
                    li += '<li id="' + e.data[i].id + '">' +
                    '<dl>' +
                        '<dt>' + e.data[i].gameusercode + '</dt>' +
                        '<dd>' + e.data[i].mobile + '</dd>' +
                        '<dd id="dizh_">' + e.data[i].gamename + '&nbsp;&nbsp;' + e.data[i].gamearea + '&nbsp;&nbsp;' + e.data[i].gameserver + '</dd>' +
                    '</dl>' +
                    '<div class="caozuo">' + moren +
                         '<span class="xgs">' +
                            '<a class="xiugai" href="javascript:;" onclick="update(' + e.data[i].id + ')" title="修改">修改</a>' +
                            '<a class="xiugai" href="javascript:;" onclick="dell(' + e.data[i].id + ')" title="删除">删除</a>' +
                        '</span>' +
                    '</div>' +
                '</li>';
                }
                else {//订单ID不为空，则是从中奖确认过来的
                    li += '<li id="' + e.data[i].id + '" class="SelWinAddr">' +
                        '<dl>' +
                            '<dt>' + e.data[i].mobile + '</dt>' +
                            '<dd>' + e.data[i].gameusercode + '</dd>' +
                            '<dd id="dizh_">' + e.data[i].gamename + '&nbsp;&nbsp;' + e.data[i].gamearea + '&nbsp;&nbsp;' + e.data[i].gameserver + '</dd>' +
                        '</dl>' +
                        '<div class="caozuo">' + moren +
                                '<span class="xgs">' +
                                '<a class="xiugai" href="javascript:;" onclick="update(' + e.data[i].id + ')" title="修改">修改</a>' +
                                '<a class="xiugai" href="javascript:;" onclick="dell(' + e.data[i].id + ')" title="删除">删除</a>' +
                            '</span>' +
                        '</div>' +
                        '<div class="selectAddr"></div>' +
                    '</li>';
                    shouhuoMobile = e.data[i].mobile;
                    shouhuoRenDiz = e.data[i].gamename + e.data[i].gamearea + e.data[i].gameserver + '@' + e.data[i].gameusercode + '*' + e.data[i].shouhuoren;
                }
            }
        }
        $("#addressListDiv #AddrListUlBox").append(li);
        ClickSelectAddr(e);
    }

    //为地址添加选择事件
    var selectedIndex = 0;
    function ClickSelectAddr(e) {
        $("#AddrListUlBox .SelWinAddr dl").on("click", function () {
            var txt = '<p>游戏地址一经确认，不可修改！</p>' +
                '<h2>您是否要选择改地址？</h2>';
            selectedIndex = $(this).attr("i");
            PopupEvent(txt, e);
        });
    }

    //选择地址触发响应的弹出层
    function PopupEvent(element, e) {
        $("#AddressPopBg").show();
        $("#AddressPop").show();
        $("#AddressPop .po-nrbox").append(element);
        var popupHeight = $("#AddressPop").height();
        var popupWidth = $("#AddressPop").width();
        $("#AddressPop").css({
            "top": (windowHeight - popupHeight) / 2,
            "left": (windowWidth - popupWidth) / 2
        });
        //取消选择
        $("#AddressPop .Cancelbtn").click(function () {
            $("#AddressPop .po-nrbox").html("");
            $("#AddressPopBg").hide();
            $("#AddressPop").hide();
        });
        //确认选择
        $("#AddressPop .Surebtn").click(function () {
            ISure(e);
        });
    }

    //确认选择该地址
    function ISure(e) {
        //alert(111);
        //alert(shouhuoMobile);
        //alert(shouhuoRenDiz);
        //SubmitData("&mobile=" + e.data[i].mobile + "&info=" + e.data[i].gamename + e.data[i].gamearea + e.data[i].gameserver + '@' + e.data[i].gameusercode + '*' + e.data[i].shouhuoren);
        var i = selectedIndex;
        SubmitData("&mobile=" + shouhuoMobile + "&info=" + shouhuoRenDiz);
        if (pageFrom == 'giftConfirm') {
            location.href = '/user.giftconfirm.aspx?&orderid=' + orderid ;
        } else {
            location.href = '/user.winningconfirm.aspx?shopid=' + shopid + '&orderid=' + orderid + '&productid=' + productid;
        }
    }

    //提交数据
    function SubmitData(params) {
        var ajaxUrl = "http://" + window.location.host + "/ilerrshareorder?action=affirminfo&orderid=" + orderid;
        $.ajax({
            cache: true,
            type: "POST",
            url: ajaxUrl,
            data: params,
            async: false,
            error: function (e) {
            },
            success: function (data) {
                if (data.state == 10) {
                    alert("提交成功！");
                } else {
                    alert("提交失败,错误码是" + data.state + "！");
                }
            }
        });

    }

    var vid = "";
    var isMobile = /^(((13[0-9]{1})|(15[0-9]{1})|(18[0-9]{1})|(17[0-9]{1})|(14[0-9]{1}))+\d{8})$/;
    var isPhone = /^(?:(?:0\d{2,3})-)?(?:\d{7,8})(-(?:\d{3,}))?$/;;
    var isVali = true;//是否通过验证
    function validate() {
        isVali = true;
        var gamename = $("#gamename").val();
        var gamearea = $("#fuwuqi").val();
        var gameserver = $("#daqu").val();
        var gameusercode = $("#gamenum").val();
        //var name = $("#shouhuoren").val();
        var mobile = $("#mobile").val();
        if (gamename == "") {
            alert("请选择有效的游戏名称");
            isVali = false;
        }
        else if (gamearea == "") {
            alert("请选择有效的游戏区");
            isVali = false;
        }
        else if (gameserver == "") {
            alert("请选择有效的游戏服务器");
            isVali = false;
        }
        else if (gameusercode == "") {
            alert("游戏账号不能为空");
            isVali = false;
        }
            //else if (name == "") {
            //    alert("游戏昵称不能为空");
            //    isVali = false;
            //}
        else if (mobile == "") {
            alert("联系方式不能为空");
            isVali = false;
        }
        else if (!isMobile.test(mobile) && !isPhone.test(mobile)) {
            alert("请正确填写电话号码，例如:13488889999或021-4818888");
            isVali = false;
        }
        return isVali;
    }

    $(function () {
        FastClick.attach(document.body);

        GameAddrList();
        //点击新增按钮
        $("#btnAddnewAddr").click(function () {
            $("#div_consignee").show();
            $(".add-dizbtnbox").hide();
            $("#btnAddnewAddr").hide();
        });
        //点击修改按钮
        $("#btnAddnewAddr").click(function () {
            $("#div_consignee").show();
            $(".add-dizbtnbox").hide();
            $("#btnAddnewAddr").hide();
        });
        //提交收货地址
        $("#btn_consignee_save").click(function () {
            if (!isVali) return false;
            ZengJiaGameAddr(vid);
            $("#div_consignee").hide();
            $("#btnAddnewAddr").show();
            $(".add-dizbtnbox").show();
        });
        //取消提交收货地址
        $("#btn_consignee_cancle").click(function () {
            $("#div_consignee").hide();
            $("#btnAddnewAddr").show();
            $(".add-dizbtnbox").show();
        });
    });

    //添加游戏地址
    function ZengJiaGameAddr(id) {
        id = vid;
        //var params = "&gameaddressid=" + id + "&gamename=" + $("#gamename").val() + "&fuwuqi=" + $("#fuwuqi").val() + "&daqu=" + $("#daqu").val() + "&gamenum=" + $("#gamenum").val() + "&shouhuoren=" + $("#shouhuoren").val() + "&mobile=" + $("#mobile").val();
        var ajaxUrl = "http://" + window.location.host + "/ilerruser?action=addusergameaddress&gameaddressid=" + id;
        $.ajax({
            cache: true,
            type: "POST",
            url: ajaxUrl,
            data: $('#AddAddressForm').serialize(),// 你的formid
            //data: params,// 你的formid
            async: false,
            error: function (request) {
                if (id == "") {
                    alert("添加失败！");
                } else {
                    alert("修改失败！");
                }
            },
            success: function (data) {
                //alert($('#AddAddressForm').serialize());
                //alert(params);
                if (data.state == 0) {
                    if (id == "") {
                        alert("添加成功！");
                    } else {
                        alert("修改成功！");
                    }
                } else {
                    if (id == "") {
                        alert("添加失败！");
                    } else {
                        alert("修改失败！");
                    }
                }
            }
        });
    }

    //修改收货地址
    function update(id) {
        OlderGameAds(id);
    }

    //读取原来的收货地址
    function OlderGameAds(id) {
        vid = id;
        $("#div_consignee").show();
        $(".add-dizbtnbox").hide();
        $("#btnAddnewAddr").hide();
        var ajaxUrl = "http://" + window.location.host + "/ilerruser?action=gameaddressidlist&gameaddressid=" + id;
        $.ajax({
            type: 'get', dataType: 'json', timeout: 10000,
            url: ajaxUrl,
            success: function (e) {
                //alert(JSON.stringify(e)); 
                if (e.state == 0) {//获取地址成功
                    //给控件赋值
                    if (e.data[0].Id == id) {
                        $("#gamenum").val(e.data[0].Gameusercode);
                        $("#shouhuoren").val(e.data[0].Shouhuoren);
                        $("#mobile").val(e.data[0].Mobile);
                        $("#gamename").val(e.data[0].Gamename);
                        $("#fuwuqi").append('<option selected value="' + e.data[0].Gamearea + '">' + e.data[0].Gamearea + '</ption>');
                        $("#daqu").append('<option selected value="' + e.data[0].Gameserver + '">' + e.data[0].Gameserver + '</option>');
                    }
                }
            }
        });
    }

    //删除收货地址
    function dell(id) {
        $(this).parents("li").remove();
        if (confirm("您真的要删除该收货地址吗？")) {
            var ajaxUrl = "http://" + window.location.host + "/ilerruser?action=deleteusergameaddress&gameaddressid=" + id;
            $.ajax({
                cache: true,
                type: "POST",
                url: ajaxUrl,
                async: false,
                error: function (request) {
                    alert("删除失败！");
                },
                success: function (data) {
                    alert("删除成功！");
                    $("li#" + id).remove();
                    //window.location.reload();
                }
            });
        }
    }

    //设置为默认地址
    function setdf(id) {
        if (confirm("您确认设置为默认地址？")) {
            var ajaxUrl = "http://" + window.location.host + "/ilerruser?action=defauleusergameaddress&gameaddressid=" + id;
            $.ajax({
                cache: true,
                type: "POST",
                url: ajaxUrl,
                async: false,
                error: function (request) {
                    alert("设置失败！");
                },
                success: function (data) {
                    alert("设置成功！");
                    window.location.reload();
                }
            });
        }
    }

    //禁用右上角微信菜单
    document.addEventListener('WeixinJSBridgeReady', function onBridgeReady() {
        WeixinJSBridge.call('hideOptionMenu');
    });


</script>

</html>
