<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="user.addressSelect.aspx.cs" Inherits="taotaole.user_addressSelect" %>

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
</head>
<body>
    <header class="header">
        <h2>收货地址管理</h2>
        <a class="cefenlei" onclick="/user.winningconfirm.aspx" href="javascript:;">
            <img width="30" height="30" src="/images/fanhui.png">
        </a>
        <div class="head-r"><a href="/index.aspx" class="fr_Home"></a></div>
    </header>
    <!-- 收货地址列表 -->
    <section class="clearfix g-member g-goods">
        <article class="mt10 " style="margin: 0 10px; border: none; box-shadow: 0px;">
            <div class="R-content">
                <div id="addressListDiv" class="list-tab detailAddress gray01" style="">
                    <ul class="m-useraddresslst ">
                    </ul>
                </div>
            </div>
        </article>
    </section>
    <!-- 收货地址列表 End -->
    <!-- 新增收货地址 -->
    <section class="clearfix g-member g-goods">
        <div class="add mt10 add-dizbtnbox">
            <a id="btnAddnewAddr" href="javascript:;" type="button" class="orangebut orgBtn" style="display: block;">新增收货地址</a>
        </div>
        <div id="div_consignee" class="addAddress" style="display: none; position: absolute; top: 50px; left: 0px; width: 100%;">
            <article class="mt10" style="margin: 0 10px; padding: 10px;">

                <dl style="font-size: 14px; color: #fd524e;">添加收货地址</dl>
                <form class="registerform" method="post" action="" id="AddAddressForm">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tbody>
                            <tr class="hanggao">
                                <td class="nowrtxt">
                                    <em class="red" id="Em5">*</em><label>收&nbsp&nbsp货&nbsp&nbsp人：</label>
                                </td>
                                <td>
                                    <input name="shouhuoren" type="text" maxlength="20" id="shouhuoren" class="input" value="">
                                </td>
                            </tr>
                            <tr class="hanggao">
                                <td class="nowrtxt">
                                    <label><em id="Em6" class="red">*</em>手机号码：</label>
                                </td>
                                <td>
                                    <input name="mobile" id="mobile" type="text" class="input" maxlength="11">
                                </td>
                            </tr>
                            <tr class="hanggao">
                                <td width="20%" class="nowrtxt">
                                    <label><em id="Em1" class="red">*</em>所在地区：</label>
                                    <script>var s2 = ["sheng", "shi", "xian"];</script>
                                </td>
                                <td width="80%">
                                    <select style="width: 26%; font-size: 14px; text-align: center;" datatype="*" nullmsg="请选择有效的省市区" class="select" id="sheng" runat="server" name="sheng"></select>
                                    <select style="width: 26%; font-size: 14px; text-align: center;" datatype="*" nullmsg="请选择有效的省市区" class="select" id="shi" runat="server" name="shi"></select>
                                    <select style="width: 26%; font-size: 14px; text-align: center;" datatype="*" nullmsg="请选择有效的省市区" class="select" id="xian" runat="server" name="xian"></select>
                                    <script type="text/javascript">setup2()</script>
                                </td>
                            </tr>
                            <tr class="hanggao">
                                <td class="nowrtxt">
                                    <em id="Em2" class="red">*</em><label>街道地址：</label>
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
        //读取收货地址
        function AddressList() {
            var ajaxUrl = "http://" + window.location.host + "/ilerruser?action=useraddresslist";
            var li = "";
            var moren = "";//是否是默认地址
            $.ajax({
                type: 'get', dataType: 'json', timeout: 10000,
                url: ajaxUrl,
                success: function (e) {
                    if (e.state == 3) {
                        $("#addressListDiv .m-useraddresslst").html("<div style='text-align:center;padding-top:20px;'>您还没有收货地址哦~ </div>");
                    }
                    if (e.state == 0) {//获取地址成功
                        //参与记录列表
                        for (var i = 0; i < e.data.length; i++) {
                            if (e.data[i].isdefault == "Y") {
                                moren = '<span class="moren">默认地址</span>';
                            } else {
                                moren = '<a class="shemoren" href="javascript:;" onclick="setdf(' + e.data[i].id + ')" title="设为默认">设为默认</a>';
                            }
                            li += '<li class="m-useradr-li pad" id="' + e.data[i].id + '" qqnb="" shr="" yb="" mob="">' +
                                '<div class="q1">' + e.data[i].shouhuoren + moren +
                                     '<span class="xgs">' +
                                        '<a class="xiugai" href="javascript:;" onclick="update(' + e.data[i].id + ')" title="修改">修改</a>' +
                                        '<a class="xiugai" href="javascript:;" onclick="dell(' + e.data[i].id + ')" title="删除">删除</a>' +
                                    '</span>' +
                                '</div>' +
                                '<table class="adr-tab">' +
                                    '<tbody><tr>' +
                                        '<th>收&nbsp;&nbsp;货&nbsp;&nbsp;人：</th>' +
                                        '<td>' + e.data[i].shouhuoren + '</td>' +
                                    '</tr>' +
                                    '<tr>' +
                                        '<th>联系方式：</th>' +
                                        '<td>' + e.data[i].mobile + '</td>' +
                                    '</tr>' +
                                    '<tr>' +
                                        '<th>收货地址：</th>' +
                                        '<td id="dizh_">' + e.data[i].sheng + '&nbsp;&nbsp;' + e.data[i].shi + '&nbsp;&nbsp;' + e.data[i].xian + '&nbsp;&nbsp;' + e.data[i].jiedao + '</td>' +
                                    '</tr>' +
                                '</tbody></table>' +
                            '</li>';
                        }
                    }
                    $("#addressListDiv .m-useraddresslst").append(li);
                }, error: function (me) { }
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
                alert("收货人不能为空");
                isVali = false;
            }
            else if (mobile == "") {
                alert("联系方式不能为空");
                isVali = false;
            }
            else if (!isMobile.test(mobile) && !isPhone.test(mobile)) {
                alert("请正确填写电话号码，例如:13488889999或021-4818888");
                isVali = false;
            }
                //else if (!mobile.match(/^(((13[0-9]{1})|159|153)+\d{8})$/)) {
                //    alert("联系方式格式错误！");
                //    isVali = false;
                //}
            else if (sheng == "") {
                alert("省份不能为空");
                isVali = false;
            }
            else if (shi == "") {
                alert("城市不能为空"); isVali = false;
            }
            else if (xian == "") {
                alert("县不能为空"); isVali = false;
            }
            else if (jiedao == "") {
                alert("街道不能为空"); isVali = false;
            }
            return isVali;
        }

        $(function () {
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
            $("#btn_consignee_cancle2").click(function () {
                $("#div_consignee2").hide();
                $(".add-dizbtnbox").show();
                $("#btnAddnewAddr").show();
            });
        });
        //添加收货地址
        function XinZengAddress(id) {
            id = vid;
            var ajaxUrl = "http://" + window.location.host + "/ilerruser?action=adduseraddress&addressid=" + id;
            alert(ajaxUrl);
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
                        //参与记录列表
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

        //修改收货地址
        function update(id) {
            //读取收货地址
            OlderAds(id);
        }

        //删除收货地址
        function dell(id) {
            $(this).parents("li.m-useradr-li").remove();
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
                        //$(this).parents("li.m-useradr-li").remove();
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

    </script>
</body>
</html>