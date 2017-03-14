<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="firstFiveYuan.aspx.cs" Inherits="RedBookPlatform.operative.firstFiveYuan" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>拉新充值送红包活动</title>
    <link rel="stylesheet" type="text/css" href="/Resources/css/comm.css"/>
        <script src="/Resources/js/jquery.min.js"></script>
    <script src="/Resources/laydate/laydate.dev.js"></script>
    <style type="text/css">
        .auto-style1 {
            width: 187px;
        }
        </style>
</head>
    <script>
        $(function () {
            $.getJSON(location.href, { action: "catelist" }, function (myJSON) {

                if (myJSON.length > 0) {
                    var options = "<option value=0>请选择分类</option>";
                    for (var i = 0; i < myJSON.length; i++) {
                        options += "<option value=" + myJSON[i].cateid + ">" + myJSON[i].name + "</option>";
                    }
                }
                else {
                    options = "<option value=0>≡ 暂无分类 ≡</option>";
                }
                $("#cate").html(options);
            });

            $("#cate").change(function () {
                var parentId = $("#cate").val();
                if (null != parentId && "" != parentId) {
                    $.getJSON(location.href, { cid: parentId, action: "shoplist" }, function (myJSON) {

                        if (myJSON.length > 0) {
                            options = '<option value="0">≡ 请选择商品 ≡</option>';
                            for (var i = 0; i < myJSON.length; i++) {
                                options += "<option value=" + myJSON[i].originalid + ">" + myJSON[i].title + "</option>";
                            }
                        }
                        else {
                            options = "<option value=0>≡ 暂无商品 ≡</option>";
                        }
                        $("#shop").html(options);
                    });
                }
            });

        });

</script>
<body>
    <div class="wrapper">
			<div class="crumbs">
				<ul id="breadcrumbs" class="breadcrumb"> 
	                <li><a href="index.html">主页</a></li>
	                <li><a href="javascript:void(0);">运营活动</a></li>
	                <li class="active"><a href="javascript:void(0);" title=""><asp:Literal runat="server" Text="首付5元不中包赔活动" ID="litTitle"></asp:Literal></a></li>
	            </ul>
			</div></div>
        <div class="content">
    <form runat="server" id="form1">
		    	<table class="table table-striped tabel-textalign">
                    <tbody>
                        <tr>
                            <td class="auto-style1">标题</td>
                            <td>
                                <asp:TextBox ID="tboxTitle" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style1">购买份数</td>
                            <td>
                                <asp:TextBox ID="tboxShopnum" runat="server"  onKeyUp="value=value.replace(/\D/g,'')"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style1">时段</td>
                            <td>
                               <asp:TextBox ID="tboxStarttime" runat="server" onclick="laydate({ istime: true, format: 'YYYY-MM-DD hh:mm:ss' })"></asp:TextBox>—<asp:TextBox ID="tboxEndtime" runat="server" onclick="laydate({ istime: true, format: 'YYYY-MM-DD hh:mm:ss' })"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style1">红包有效天数</td>
                            <td>
                                <asp:TextBox ID="tboxRedpackday" runat="server" onKeyUp="value=value.replace(/\D/g,'')" style="margin-right:10px;"></asp:TextBox>天
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style1">参与次数</td>
                            <td>
                                <asp:TextBox ID="tboxCount" runat="server" onKeyUp="value=value.replace(/\D/g,'')" style="margin-right:10px;"></asp:TextBox>次
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style1">发放红包</td>
                            <td>
                                          <asp:DropDownList ID="DropDownList1" runat="server">
                                          </asp:DropDownList>&nbsp;&nbsp;/&nbsp;&nbsp;
                                          <asp:DropDownList ID="DropDownList2" runat="server">
                                          </asp:DropDownList>
                                      </td></tr>
                        <tr>
                            <td><span class="red">*</span>商品选择</td>
                            <td>
                                分类：<select id="cate" name="cate"> <option value="0">≡ 请选择分类 ≡</option>          
                                      </select>            
                                  商品：<select id="shop" name="shop" style="margin-right:10px;" >
                	                    <option value="0" >≡ 请选择商品 ≡</option>
				                    </select><input id="addshoprange" value="添加选中商品" class="btn" onclick="" type="button"/>
				                <input id="shopclear" value="清空" class="btn" onclick="" type="button" /></td>
                        </tr>
                        <tr>
			                <td style="width:120px">商品列表：</td>
			                <td>
			                <span id = "shoprangelist"  class="lr10"></span>
			                </td>
		                </tr>
                        <tr>
                            <td class="auto-style1">用户显示信息</td>
                            <td>
                                <asp:TextBox ID="tboxUsercodeinfo" runat="server"></asp:TextBox>例：“（首付红包）”
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align:center">
                                 <asp:Button runat="server" ID="btnSave"  OnClientClick="return validation();" class="btn btn-primary" Text="保存" OnClick="btnSave_Click" />&nbsp;&nbsp;
                                </td>
                        </tr>
                    </tbody>
                </table>
                    </form></div>
</body>
        <script type="text/javascript">
            function validation() {
                var flag = true;
                var msg = "";
                if ($("#tboxTitle").val().length == 0) {
                    msg = "请输入标题";
                    flag = false;
                }
                if ($("#tboxRedpackday").val().length == 0) {
                    msg = "请输入有效天数";
                    flag = false;
                }
                if ($("#tboxShopnum").val().length == 0) {
                    msg = "请输入购买份数";
                    flag = false;
                }

                if ($("#tboxStarttime").val().length == 0) {
                    msg = "请输入开始时间";
                    flag = false;
                }
                if ($("#tboxEndtime").val().length == 0) {
                    msg = "请输入结束时间";
                    flag = false;
                }
                var sfalg = 0;
                $("[name='shoprange[]']:checked").each(function () {
                    if ($(this).attr("checked")) {
                        sfalg += 1;
                    }
                })
                if (sfalg == 0) {
                    msg = "请增加商品";
                    flag = false;
                }
                if (!flag) { alert(msg); }
                return flag;

            }
            $.getJSON(location.href, { action: "getinfo" }, function (myJSON) {
                //alert(myJSON[0].use_range);
                userange = jQuery.parseJSON(myJSON[0].use_range);
                if (userange.length > 0) {
                    userangehtml = "";
                    for (var i = 0; i < userange.length; i++) {
                        userangehtml += '<input name="shoprange[]" value="' + userange[i].id + '{}' + userange[i].name + '" type="checkbox" checked />&nbsp;' + userange[i].name + '&nbsp;&nbsp;红包上限<input type="text" name="redpackmax' + userange[i].id + '" value="' + userange[i].amountmax + '"/>元<br>';
                    }
                    $("#shoprangelist").html(userangehtml);

                }
            });
            $(document).ready(function () {
                $(function () {

                    $('#shopclear').click(function () {
                        $('#shoprangelist').html("");
                    });

                    $('#addshoprange').click(function () {
                        // alert($("#shop").select().val());
                        var addshop = $("#shoprangelist").html();
                        var chk_value = [];
                        $('input[name="shoprange[]"]').each(function () {
                            chk_value.push($(this).val());
                        });
                        if ($("#shop option:selected").select().val() != 0 && $.inArray($("#shop option:selected").select().val(), chk_value) == -1) {
                            addshop += '<input name="shoprange[]" value="' + $("#shop option:selected").select().val() + '{}' + $("#shop option:selected").select().text() + '" type="checkbox" checked />&nbsp;' + $("#shop option:selected").select().text() + '&nbsp;&nbsp;红包上限<input type="text" name="redpackmax' + $("#shop option:selected").select().val() + '"/>元<br>';
                            $("#shoprangelist").html(addshop);
                        }
                        else {
                            alert("未选择商品或选择商品已添加")
                        }
                    });
                    
                });
            });
        </script>
</html>
