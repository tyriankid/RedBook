<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddZhiGou.aspx.cs" Inherits="RedBookPlatform.products.AddZhiGou" %>
<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>
<%@ Register TagPrefix="UI" Namespace="Taotaole.Common" Assembly="Taotaole.Common" %>
<!DOCTYPE html>
<html>
   
	<head id="Head1" runat="server">
		<meta charset="UTF-8">
		<title>添加团购商品</title>
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
	                <li class="active"><a href="#" title="">添加直购商品</a></li>
	            </ul>
			</div>
		    <div class="content">
                <form runat="server" id="form1">
		    	<table class="table table-striped tabel-textalign">
                    <tbody>
                        <tr role="ddlproducts">
                            <td width="150"><span  class="red" id="spProducts">*</span>商品选择</td>
                            <td>
                                <asp:DropDownList ID="ddlCategory" runat="server" AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" Width="151px"></asp:DropDownList>
                                <asp:DropDownList ID="ddBrand" runat="server" OnSelectedIndexChanged="ddlBrand_SelectedIndexChanged" AppendDataBoundItems="True" AutoPostBack="True" Width="213px"></asp:DropDownList>
                                <asp:DropDownList ID="ddlProducts" runat="server" OnSelectedIndexChanged="ddlProducts_SelectedIndexChanged" AppendDataBoundItems="True" AutoPostBack="True" Width="213px"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td><span class="red" id="spTitle">*</span>直购商品标题</td>
                            <td><input type="text" name="title" id="title" runat="server" value="" style="width: 50%;" /></td>
                        </tr>
                         <tr>
                            <td><span class="red" id="spstock">*</span>库存</td>
                            <td><input type="text" name="stock" id="stock" runat="server" value="" style="width: 50%;" /></td>
                        </tr>
                        <tr role="ddlproducts">
                            <td><span class="red" id="spMoney">*</span>原价</td>
                            <td><input type="text" name="money" id="money" runat="server" value="" style="width: 50%;"  disabled="disabled"/></td>
                        </tr>
                         <tr>
                            <td><span class="red" id="spZhiMoney">*</span>直购价</td>
                            <td><input type="text" name="zhiMoney" id="zhiMoney" runat="server" value="" style="width: 50%;" /></td>
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
        <input type="hidden"  runat="server"  id ="hidChange"/>
        <script type="text/javascript">
            function validation() {
                var flag = true;
                var msg = "";
                if ($("#stock").val() == "") {

                    $("#spstock").css({ color: "red" });
                    flag = false; msg = "请设置商品库存!";
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

                if (!flag) { alert(msg); }
                return flag;
            }

            $(function () {
                var QuanId = "<%=QuanId %>";
                //编辑模式
                if (QuanId > 0) {
                    $("[role='ddlproducts']").hide();
                }
            })
    </script>

	</body>
</html>