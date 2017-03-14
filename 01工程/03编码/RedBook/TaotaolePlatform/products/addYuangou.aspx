<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="addYuangou.aspx.cs" Inherits="RedBookPlatform.products.addYuangou" %>
<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>
<%@ Register TagPrefix="UI" Namespace="Taotaole.Common" Assembly="Taotaole.Common" %>
<!DOCTYPE html>
<html>
   
	<head runat="server">
		<meta charset="UTF-8">
		<title>添加一元购商品</title>
		<link rel="stylesheet" type="text/css" href="/Resources/css/comm.css"/>
        <script src="/Resources/js/jquery.min.js" type="text/javascript" charset="utf-8"></script>
        <script src="/Resources/js/comm.js" type="text/javascript" charset="utf-8"></script>
		
	</head>
	<body>
		<div class="wrapper">
			<div class="crumbs">
				<ul id="breadcrumbs" class="breadcrumb"> 
	                <li><a href="#">主页</a></li>
	                <li><a href="#">商品管理</a></li>
	                <li class="active"><a href="#" title="">添加一元购商品</a></li>
	            </ul>
			</div>
		    <div class="content">
                <form runat="server" id="form1">
		    	<table class="table table-striped tabel-textalign">
                    <tbody>
                        <tr role="ddlproducts">
                            <td width="150"><span  class="red" id="spProducts" style="color:red"><strong>*&nbsp;</strong></span>商品选择</td>
                            <td>
                                <asp:DropDownList ID="ddlCategory" runat="server" AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" Width="151px"></asp:DropDownList>
                                <asp:DropDownList ID="ddBrand" runat="server" OnSelectedIndexChanged="ddlBrand_SelectedIndexChanged" AppendDataBoundItems="True" AutoPostBack="True" Width="213px"></asp:DropDownList>
                                <asp:DropDownList ID="ddlProducts" runat="server" OnSelectedIndexChanged="ddlProducts_SelectedIndexChanged" AppendDataBoundItems="True" AutoPostBack="True" Width="213px"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td><span class="red" id="spOrder" style="color:red"><strong>*&nbsp;</strong></span>排序号</td>
                            <td><input type="text" name="title" id="tdOrder" runat="server" value="99999" style="width: 50%;" />&nbsp;&nbsp;排序号越小，排位越靠前.</td>
                        </tr>

                        <tr>
                            <td><span class="red" id="spTitle" style="color:red"><strong>*&nbsp;</strong></span>一元购商品标题</td>
                            <td><input type="text" name="title" id="title" runat="server" value="" style="width: 50%;" /></td>
                        </tr>
                        <tr>
                            <td><span class="red" id="spZongjia" style="color:red"><strong>*&nbsp;</strong></span>总价格</td>
                            <td><input type="text" name="zongjiage" id="zongjiage" onkeyup="value=value.replace(/\D/g,'')" runat="server"  value="" style="width: 50%;" /><asp:Label runat="server" ID="labZongJiaGe"></asp:Label>&nbsp;&nbsp;元<a  runat="server" id="aPrompt">,展示用,实际总价格以(单人次购买价格*总人次)为准</a></td>
                        </tr>
                        <tr>
                            <td><span class="red" id="spDanjia" style="color:red"><strong>*&nbsp;</strong></span>单人次价格</td>
                            <td><input type="text" name="yunjiage" id="yunjiage" onkeyup="value=value.replace(/\D/g,'')" runat="server"  value="" style="width: 50%;" /><asp:Label runat="server" ID="labYunJiaGe"></asp:Label>&nbsp;&nbsp;元</td>
                        </tr>
                       
                        <tr>
                            <td><span class="red" id="spRenshu" style="color:red"><strong>*&nbsp;</strong></span>开奖所需人次</td>
                            <td><input type="text" name="zongrenshu" id="zongrenshu" onkeyup="value=value.replace(/\D/g,'')" runat="server" value="" style="width: 50%;" /><asp:Label runat="server" ID="labZongRenShu" ></asp:Label>&nbsp;&nbsp;人</td>
                        </tr>
                        <tr>
                            <td><span class="red" id="spQishu" style="color:red"><strong>*&nbsp;</strong></span>最大期数</td>
                            <td><input type="text" name="maxqishu" id="maxqishu" onkeyup="value=value.replace(/\D/g,'')" runat="server"  value="" style="width: 50%;"/>&nbsp;&nbsp;期,	   期数上限为65535期,每期揭晓后会根据此值自动添加新一期商品！</td>
                        </tr>
                        <tr>
                            <td><span class="red" id="spKuaisu" style="color:red"><strong>*&nbsp;</strong></span>快速购买价格区域</td>
                            <td><input type="text" name="pricerange" id="pricerange"  runat="server"  value="" style="width: 50%;" />&nbsp;&nbsp;多个价格请用英文逗号分割开（如： 5,20,50,100）</td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align:center">
                                <asp:Button runat="server" ID="btnSave" Text="保 存" OnClientClick="return validation();"   CssClass="btn btn-primary" OnClick="btnSave_Click" />
                            </td>
                        </tr>
                    </tbody>
                </table>
                    </form>
		    </div>
		</div>
        <input type="hidden" id ="hidZuida"/>
        <input type="hidden"  runat="server"  id ="hidChange"/>
        <script type="text/javascript">
            $(document).ready(function () {
                var zuida = $("#maxqishu").val();
                $("#hidZuida").val(zuida);
                });
            function validation() {
                var flag = true;
                var msg = "";
                if ($("#pricerange").val() == "") {
                    $("#spKuaisu").css({ color: "red" });
                    flag = false; msg = "请填写快速购买价格区域!";
                }
                if ($("#maxqishu").val() == "") {
                    $("#spQishu").css({ color: "red" });
                    flag = false; msg = "请填写最大期数!";
                }
                if ($("#zongrenshu").val() == "")
                {
                    $("#spRenshu").css({ color: "red" });
                    flag = false; msg = "开奖所需人数不能为空!";     
                }
                if ($("#zongjiage").val() == "") {
                    $("#spZongjia").css({ color: "red" });
                    flag = false; msg = "商品总价不能为空!";
                }
                if ($("#yunjiage").val() == "") {
                    $("#spDanjia").css({ color: "red" });
                    flag = false; msg = "单人次价格不能为空!";
                }
                if (($("#hidChange").val()) == "") {
                    if ($("#ddlCategory").val() == "0" && $("#ddlProducts").val() == "0") {
                        $("#spProducts").css({ color: "red" });
                        flag = false; msg = "请选择商品分类和商品!";
                    }
                    else if ($("#ddlCategory").val() != "0" && $("#ddlProducts").val() == "0") {
                        $("#spProducts").css({ color: "red" });
                        flag = false; msg = "请选择商品!";
                    }
                    
                }
                //分类和品牌不能为未选中
                if (parseInt($("#zongrenshu").val()) * parseInt($("#yunjiage")) < parseInt($("#zongjiage"))) {
                    $("#spDanjia").css({ color: "red" });
                    $("#spRenshu").css({ color: "red" });
                    $("#spZongjia").css({ color: "red" });
                    flag = false; msg = "单人次价格*开奖所需人数不能低于商品总价格!";
                }
                if ($("#maxqishu").val() != "") {
                    if ($("#maxqishu").val() < $("#hidZuida").val()) {
                        $("#spQishu").css({ color: "red" });
                        flag = false; msg = "最大期数不能小于往期!";
                    }
                }
                if (!flag) { alert(msg); }
                return flag;
            }

            $(function () {
                //根据编辑或添加状态进行隐藏显示
                var yid = "<%=yid %>";
                //编辑模式
                if (yid > 0) {
                    $("[role='ddlproducts']").hide();
                    $("#zongjiage").hide();
                    $("#yunjiage").hide();
                    $("#zongrenshu").hide();
                }
            })
        </script>

	</body>
</html>
