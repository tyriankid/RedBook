<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="addWxMenu.aspx.cs" Inherits="RedBookPlatform.wx.addWxMenu" %>
<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>
<%@ Register TagPrefix="UI" Namespace="Taotaole.Common" Assembly="Taotaole.Common" %>
<!DOCTYPE html>

<html>
	<head runat="server">
		<meta charset="UTF-8">
		<title>添加主菜单</title>
		<link rel="stylesheet" type="text/css" href="<%=CSS_PATH %>comm.css"/>
        <script src="<%=JS_PATH %>jquery.min.js"></script>
        
	</head>
	<body>
		<div class="wrapper">
			<div class="crumbs">
				<ul id="breadcrumbs" class="breadcrumb"> 
	                <li><a href="#">主页</a></li>
	                <li><a href="#">系统设置</a></li>
	                <li class="active"><a href="#" title="">添加微信主菜单</a></li>
	            </ul>
			</div>
		    <div class="content">
                <form runat="server" id="form1">
		    	<table class="table table-striped tabel-textalign">
                    <tbody>
                        <tr id="parentMenu" style="display:none">
                            <td>上级菜单</td>
                            <td><asp:Literal runat="server" ID="parentMenuName"></asp:Literal></td>
                        </tr>
                        <tr>
                            <td><span class="red" id="spMenuname">*</span>菜单名称</td>
                            <td><input type="text" name="menuname" id="tdMenuname" runat="server" style="width: 50%;" />  菜单名称不能为空，一级菜单在五个汉字内，二级菜单在七个汉字内。</td>
                        </tr>
                        <tr>
                            <td>菜单链接地址</td>
                            <td><input type="text" name="Url" id="tdUrl" runat="server"  style="width: 50%;" /> 菜单链接地址,可为空</td>
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

        <script type="text/javascript">
            $(function () {
                if ("<%=parentid%>" > 0) {
                    $("#parentMenu").show();
                }
            });


            function validation() {
                var flag = true;
                var msg = "";
                if ($("#tdMenuname").val() == "") {
                    $("#spMenuname").css({ color: "red" });
                    flag = false; msg = "请填写菜单名";
                }
                if (!flag) { alert(msg); }
                return flag;
            }
        </script>

	</body>
</html>
