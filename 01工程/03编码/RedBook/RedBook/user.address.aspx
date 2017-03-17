<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="user.address.aspx.cs" Inherits="RedBook.user_address" %>

<!DOCTYPE html>
<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width,initial-scale=1,minimum-scale=1,maximum-scale=1,user-scalable=no" />
    <title>收货地址管理</title>
    <link href="css/comm.css?v=130715" rel="stylesheet" type="text/css" />
    <link href="css/member.css?v=130726" rel="stylesheet" type="text/css" />
    <link href="css/address.css" rel="stylesheet" type="text/css" />
    <script src="js/jquery190.js" language="javascript" type="text/javascript"></script>
    <script src="js/Validform_v5.3.2.js" language="javascript" type="text/javascript"></script>
    <script type="text/javascript" src="js/area.js"></script>
    <script src="/js/fastclick.js"></script>

</head>
<body>
    <header class="header">
        <h2>收货地址管理</h2>
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
    <section class="clearfix g-member g-goods">
        <div class="add mt10 add-dizbtnbox">
            <a id="btnAddnewAddr" href="javascript:;" type="button" class="orangebut orgBtn" style="display: block;">新增收货地址</a>
        </div>
        <div id="div_consignee" class="addAddress" style="display: none; position: absolute; top: 50px; left: 0px; width: 100%;">
            <article class="mt10">

                <%--<dl style="font-size: 14px; color: #fd524e;">添加收货地址</dl>--%>
                <form class="registerform" method="post" action="" id="AddAddressForm">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tbody>
                            <tr class="hanggao">
                                <td class="nowrtxt">
                                    <em class="red" id="Em5"></em><label>收&nbsp&nbsp货&nbsp&nbsp人</label>
                                </td>
                                <td>
                                    <input name="shouhuoren" type="text" maxlength="20" id="shouhuoren" class="input" value="">
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
                            <tr class="hanggao">
                                <td width="20%" class="nowrtxt">
                                    <label><em id="Em1" class="red"></em>所在地区</label>
                                    <script>var s2 = ["sheng", "shi", "xian"];</script>
                                </td>
                                <td width="80%">
                                    <select style="width: 33.33%; font-size: 14px; text-align: center;float:left;height:40px;" datatype="*" nullmsg="请选择有效的省市区" class="select" id="sheng" runat="server" name="sheng"></select>
                                    <select style="width: 33.33%; font-size: 14px; text-align: center;float:left;height:40px;" datatype="*" nullmsg="请选择有效的省市区" class="select" id="shi" runat="server" name="shi"></select>
                                    <select style="width: 33.33%; font-size: 14px; text-align: center;float:left;height:40px;" datatype="*" nullmsg="请选择有效的省市区" class="select" id="xian" runat="server" name="xian"></select>
                                    <script type="text/javascript">setup2()</script>
                                </td>
                            </tr>
                            <tr class="hanggao">
                                <td class="nowrtxt">
                                    <em id="Em2" class="red"></em><label>街道地址</label>
                                </td>
                                <td>
                                    <input id="jiedao" name="jiedao" type="text" class="input" maxlength="100" />
                                </td>
                            </tr>
                            <%-- <tr class="hanggao">
                                <td class="nowrtxt">
                                    <label>是否发货：</label>
                                </td>
                                <td>
                                    <input type="radio" name="shifoufahuo" value="1" />
                                    是，马上收货&nbsp;&nbsp;&nbsp;
								<input type="radio" name="shifoufahuo" value="0" checked="checked" />
                                    否，暂时别发货
                                </td>
                            </tr>--%>
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
    </section>
    <!-- 新增收货地址 End -->

    <div style="height: 70px;"></div>

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
        var pageFrom = getUrlParam("pageFrom");

        //读取收货地址
        function AddressList() {
            var ajaxUrl = "http://" + window.location.host + "/ilerruser?action=useraddresslist";
            $.ajax({
                type: 'get', dataType: 'json', timeout: 10000,
                url: ajaxUrl,
                success: function (e) {
                    AddressListEx(e);
                },
                error: function (me) {
                    //alert("获取失败！");
                }
            });
        }

        var moren = "";//是否是默认地址
        //读取收货地址_结果处理
        function AddressListEx(e) {
            var li = "";
            if (e.state == 3) {
                $("#addressListDiv #AddrListUlBox").html("<div style='text-align:center;padding-top:30px;font-size:15px;color:#666;'>暂未添加收货地址</div>");
            }
            if (e.state == 0) {//获取地址成功
                for (var i = 0; i < e.data.length; i++) {

                    if (e.data[i].isdefault == "Y") {
                        moren = '<span class="moren">默认地址</span>';
                    } else {
                        moren = '<a class="shemoren" href="javascript:;" onclick="setdf(' + e.data[i].id + ')" title="设为默认">设为默认</a>';
                    }
                    if (!orderid || orderid == "null" || orderid == "" || typeof (orderid) == "undefined") {
                        li += '<li id="' + e.data[i].id + '" >' +
                        '<dl>' +
                            '<dt>' + e.data[i].shouhuoren + '<span>' + e.data[i].mobile + '</span>' + '</dt>' +
                            '<dd id="dizh_">' + e.data[i].sheng + '&nbsp;&nbsp;' + e.data[i].shi + '&nbsp;&nbsp;' + e.data[i].xian + '&nbsp;&nbsp;' + e.data[i].jiedao + '</dd>' +
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
                            '<dl i="' + i + '">' +
                                '<dt>' + e.data[i].shouhuoren + '<span>' + e.data[i].mobile + '</span>' + '</dt>' +
                                '<dd id="dizh_">' + e.data[i].sheng + '&nbsp;&nbsp;' + e.data[i].shi + '&nbsp;&nbsp;' + e.data[i].xian + '&nbsp;&nbsp;' + e.data[i].jiedao + '</dd>' +
                            '</dl>' +
                            '<div class="caozuo">' + moren +
                                    '<span class="xgs">' +
                                    '<a class="xiugai" href="javascript:;" onclick="update(' + e.data[i].id + ')" title="修改">修改</a>' +
                                    '<a class="xiugai" href="javascript:;" onclick="dell(' + e.data[i].id + ')" title="删除">删除</a>' +
                                '</span>' +
                            '</div>' +
                            '<div class="selectAddr"></div>' +
                        '</li>';
                    }
                }
            }
            $("#addressListDiv #AddrListUlBox").append(li);
            ClickSelectAddr(e);
        }
        //是否是默认
        function IsMoren(e) {
            if (e.data[i].isdefault == "Y") {
                moren = '<span class="moren">默认地址</span>';
            } else {
                moren = '<a class="shemoren" href="javascript:;" onclick="setdf(' + e.data[i].id + ')" title="设为默认">设为默认</a>';
            }
        }

        //普通地址列表
        function MorenAddrList(e) {
            return '<li id="' + e.data[i].id + '">' +
            '<dl>' +
                '<dt>' + e.data[i].shouhuoren + '<span>' + e.data[i].mobile + '</span>' + '</dt>' +
                '<dd id="dizh_">' + e.data[i].sheng + '&nbsp;&nbsp;' + e.data[i].shi + '&nbsp;&nbsp;' + e.data[i].xian + '&nbsp;&nbsp;' + e.data[i].jiedao + '</dd>' +
            '</dl>' +
            '<div class="caozuo">' + moren +
                 '<span class="xgs">' +
                    '<a class="xiugai" href="javascript:;" onclick="update(' + e.data[i].id + ')" title="修改">修改</a>' +
                    '<a class="xiugai" href="javascript:;" onclick="dell(' + e.data[i].id + ')" title="删除">删除</a>' +
                '</span>' +
            '</div>' +
        '</li>';

        }

        //从中奖确认过来选择的地址列表
        function WinAddrList(e) {
            return '<li id="' + e.data[i].id + '" class="SelWinAddr">' +
                '<dl>' +
                    '<dt>' + e.data[i].shouhuoren + '<span>' + e.data[i].mobile + '</span>' + '</dt>' +
                    '<dd id="dizh_">' + e.data[i].sheng + '&nbsp;&nbsp;' + e.data[i].shi + '&nbsp;&nbsp;' + e.data[i].xian + '&nbsp;&nbsp;' + e.data[i].jiedao + '</dd>' +
                '</dl>' +
                '<div class="caozuo">' + moren +
                        '<span class="xgs">' +
                        '<a class="xiugai" href="javascript:;" onclick="update(' + e.data[i].id + ')" title="修改">修改</a>' +
                        '<a class="xiugai" href="javascript:;" onclick="dell(' + e.data[i].id + ')" title="删除">删除</a>' +
                    '</span>' +
                '</div>' +
                '<div class="selectAddr"></div>' +
            '</li>';
        }

        //为地址添加选择事件
        var selectedIndex = 0;
        function ClickSelectAddr(e) {
            $("#AddrListUlBox .SelWinAddr dl").on("click", function () {
                var txt = '<p>收货地址一经确认，不可修改！</p>' +
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
            var i = selectedIndex;
            SubmitData("&mobile=" + e.data[i].mobile + "&info=" + e.data[i].sheng + e.data[i].shi + e.data[i].xian + e.data[i].jiedao + "*" + e.data[i].shouhuoren);
            if (pageFrom == 'giftConfirm') {
                location.href = '/user.giftconfirm.aspx?orderid=' + orderid;
            } else if (pageFrom == 'quanConfirm') {
                location.href = '/user.quanconfirm.aspx?orderid=' + orderid;
            }
            else {
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
            var name = $("#shouhuoren").val();
            var mobile = $("#mobile").val();
            var sheng = $("#sheng").val();
            var shi = $("#shi").val();
            var xian = $("#xian").val();
            var jiedao = $("#jiedao").val();
            if (name == "") {
                //InfoPopupEvent("收货人不能为空");
                alert("收货人不能为空");
                isVali = false;
            }
            else if (mobile == "") {
                //InfoPopupEvent("联系方式不能为空");
                alert("联系方式不能为空");
                isVali = false;
            }
            else if (!isMobile.test(mobile) && !isPhone.test(mobile)) {
                //InfoPopupEvent("请正确填写电话号码，例如:13488889999或021-4818888");
                alert("请正确填写电话号码，例如:13488889999或021-4818888");
                isVali = false;
            }
            else if (sheng == "") {
                //InfoPopupEvent("省份不能为空");
                alert("省份不能为空");
                isVali = false;
            }
            else if (shi == "") {
                //InfoPopupEvent("城市不能为空");
                alert("城市不能为空");
                isVali = false;
            }
            else if (xian == "") {
                //InfoPopupEvent("县不能为空");
                alert("县不能为空");
                isVali = false;
            }
            else if (jiedao == "") {
                //InfoPopupEvent("详细地址不能为空");
                alert("街道不能为空");
                isVali = false;
            }
            return isVali;
        }

        $(function () {
            FastClick.attach(document.body);

            AddressList();
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
                XinZengAddress(vid);
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
        //添加收货地址
        function XinZengAddress(id) {
            id = vid;
            var ajaxUrl = "http://" + window.location.host + "/ilerruser?action=adduseraddress&addressid=" + id;
            $.ajax({
                cache: true,
                type: "POST",
                url: ajaxUrl,
                data: $('#AddAddressForm').serialize(),// 你的formid
                async: false,
                error: function (request) {
                    if (id == "") {
                        alert("添加失败！");
                    } else {
                        alert("修改失败！");
                    }
                },
                success: function (data) {
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
            OlderAds(id);
        }

        //读取原来的收货地址
        function OlderAds(id) {
            vid = id;
            $("#div_consignee").show();
            $(".add-dizbtnbox").hide();
            $("#btnAddnewAddr").hide();
            var ajaxUrl = "http://" + window.location.host + "/ilerruser?action=useraddresslist";
            $.ajax({
                type: 'get', dataType: 'json', timeout: 10000,
                url: ajaxUrl,
                success: function (e) {
                    if (e.state == 0) {//获取地址成功
                        //读取收货地址
                        for (var i = 0; i < e.data.length; i++) {
                            if (e.data[i].id == id) {
                                $("#shouhuoren").val(e.data[i].shouhuoren);
                                $("#mobile").val(e.data[i].mobile);
                                $("#sheng").val(e.data[i].sheng);
                                $("#shi").append('<option selected value="' + e.data[i].shi + '">' + e.data[i].shi + '</ption>');
                                $("#xian").append('<option selected value="' + e.data[i].xian + '">' + e.data[i].xian + '</option>');
                                $("#jiedao").val(e.data[i].jiedao);
                            }
                        }
                    }
                }
            });
        }

        //删除收货地址
        function dell(id) {
            $(this).parents("li").remove();
            if (confirm("您真的要删除该收货地址吗？")) {
                var ajaxUrl = "http://" + window.location.host + "/ilerruser?action=deleteuseraddress&addressid=" + id;
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
                        window.location.reload();
                    }
                });
            }
        }

        //设置为默认地址
        function setdf(id) {
            if (confirm("您确认设置为默认地址？")) {
                var ajaxUrl = "http://" + window.location.host + "/ilerruser?action=defauleuseraddress&addressid=" + id;
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

        //填写信息提示弹出层
        function InfoPopupEvent(element) {
            $("#InfoPopupBg").show();
            $("#InfoPopup").show();
            $("#InfoPopup .po-nrbox h2").text(element);
            var popupHeight = $("#InfoPopup").height();
            var popupWidth = $("#InfoPopup").width();
            $("#InfoPopup").css({
                "top": (windowHeight - popupHeight) / 2,
                "left": (windowWidth - popupWidth) / 2
            });
            //确认选择
            $("#InfoPopup .btn").click(function () {
                $("#InfoPopupBg").hide();
                $("#InfoPopup").hide();
                return false;
            });
        }

        //禁用右上角微信菜单
        document.addEventListener('WeixinJSBridgeReady', function onBridgeReady() {
            WeixinJSBridge.call('hideOptionMenu');
        });
    </script>
    <!-- 填写信息提示弹出层 s -->
    <div class="PopupBg clearfix" id="InfoPopupBg"></div>
    <div class="PopupLayer" id="InfoPopup">
        <h1>操作提示</h1>
        <div class="po-nrbox">
            <h2></h2>
        </div>
        <div class="OnebtnBox">
            <div class="btn">我知道了</div>
        </div>
    </div>
    <!-- 填写信息提示弹出层 E -->
    <!-- 页面消息提示弹出层 -->
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
</body>
</html>
