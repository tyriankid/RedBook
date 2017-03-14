<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="addBrand.aspx.cs" Inherits="RedBookPlatform.productClassify.addBrand" %>
<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>
<%@ Register TagPrefix="UI" Namespace="Taotaole.Common" Assembly="Taotaole.Common" %>

<!DOCTYPE html>
<html>
	<head runat="server">
		<meta charset="UTF-8">
		<title>添加品牌</title>
      <link rel="stylesheet" type="text/css" href="/Resources/css/comm.css"/>
        <link href="/Resources/css/flashupload.css" rel="stylesheet" type="text/css" />
        <script src="/Resources/js/jquery.min.js"></script>
        <script src="/Resources/js/flashupload.js"></script>
	</head>
	<body>
		<div class="wrapper">
			<div class="crumbs">
				<ul id="breadcrumbs" class="breadcrumb"> 
	                <li><a href="#">主页</a></li>
	                <li><a href="#">品牌管理</a></li>
	                <li class="active"><a href="#" title="">添加品牌</a></li>
	            </ul>
			</div>
		    <div class="content">
                <form runat="server" id="form1">
		    	<table class="table table-striped tabel-textalign">
                    <tbody>
                        <tr id="trBrand" runat="server" style="display:none">
                            <td><span class="red">*</span>品牌ID</td>
                            <td><input type="text" name="brandId" id="brandId" runat="server" value="" style="width: 50%;" aria-disabled="True"  /></td>
                        </tr>
                         <tr>
                            <td width="150">品牌排序</td>
                            <td><input type="text" name="brandId" id="inpOrder" runat="server" value="999" style="width:50%;" aria-disabled="True"  /></td>
                        </tr>
                        <tr>
                            <td>品牌名称</td>
                            <td><input type="text" name="brandName" id="brandName" runat="server" value="" style="width: 50%;" /></td>
                        </tr>
                         <tr>
                            <td>所属分类</td>
                            <td><asp:DropDownList ID="drpBrandClass" runat="server"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align:center">
                                <asp:Button runat="server" ID="btnSave" Text="保 存"  OnClientClick="return validation();"  CssClass="btn btn-primary"  OnClick="btnSave_Click"/>
                            </td>
                        </tr>
                    </tbody>
                </table>
                    </form>
		    </div>
		</div>
        <script type="text/javascript" charset="utf-8" src="/ueditor/ueditor.config.js"></script>
        <script type="text/javascript" charset="utf-8" src="/ueditor/ueditor.all.min.js"> </script>
        <script type="text/javascript" charset="utf-8" src="/ueditor/lang/zh-cn/zh-cn.js"></script>
        <script type="text/javascript">
            function validation() {
                var flag = true;
                var msg = "";
                if ($("#brandName").val() == "") {
                    flag = false; msg = "请填写品牌名称!";
                }
                if (!flag) { alert(msg); }
                return flag;
            }
        </script>
	</body>
</html>
