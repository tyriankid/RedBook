<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="addbaiducfglist.aspx.cs" Inherits="RedBookPlatform.baidu.addbaiducfglist" %>

<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>
<%@ Register TagPrefix="UI" Namespace="Taotaole.Common" Assembly="Taotaole.Common" %>
<!DOCTYPE html>
<html>
   
	<head>
		<meta charset="UTF-8">
		<title>添加推广</title>
		<link rel="stylesheet" type="text/css" href="<%=CSS_PATH %>comm.css"/>
        <script src="<%=JS_PATH %>jquery.min.js"></script>
        
	</head>
	<body>
		<div class="wrapper">
			<div class="crumbs">
				<ul id="breadcrumbs" class="breadcrumb"> 
	                <li><a href="#">百度推广</a></li>
	                <li><a href="#">推广管理</a></li>
	                <li class="active"><a href="#" title="">添加推广</a></li>
	            </ul>
			</div>
		    <div class="content">
                <form runat="server" id="form1">
		    	<table class="table table-striped tabel-textalign">
                    <tbody>
                        <tr role="ddlproducts">
                            <td width="150"><span  class="red" id="spProducts" style="color:red"><strong>*&nbsp;</strong></span>商品选择</td>
                            <td>
                                <asp:DropDownList ID="ddproducttype" runat="server"  Width="151px" AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="ddproducttype_SelectedIndexChanged"></asp:DropDownList>
                                <asp:DropDownList ID="ddshoptitle" runat="server"  AppendDataBoundItems="True" AutoPostBack="True"  Width="213px"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td><span class="red" id="spOrder" style="color:red"><strong>*&nbsp;</strong></span>推广编码</td>
                            <td><input type="text" name="title" id="tdOrder" runat="server" style="width: 50%;" />&nbsp;&nbsp;<span style="color:#C2C2C2" id ="Span1"> *必填，【分割符：,】</span></td>
                        </tr>
                        <tr>
                            <td><span class="red" id="Span2" style="color:red"><strong>*&nbsp;</strong></span>推广时间</td>
                            <td><input type="text" name="title" id="txtbuytimes" runat="server" style="width: 50%;" />&nbsp;&nbsp;<span style="color:#C2C2C2" id ="Span3"> *必填，【分割符：,。】</span></td>
                        </tr>
                        <tr>
                            <td><span class="red" id="Span4" style="color:red"><strong>*&nbsp;</strong></span>推广标识</td>
                            <td><input type="text" name="title" id="txtcfgtype" runat="server" style="width: 50%;" />&nbsp;&nbsp;<span style="color:#C2C2C2" id ="Span5"> *必填</span></td>
                        </tr>
                        <tr>
                            <td><span class="red" id="spTitle" style="color:red"><strong>*&nbsp;</strong></span>推广状态</td>
                                                            <td>
                                <asp:RadioButtonList ID="operationtype" runat="server" RepeatLayout="UnorderedList" CssClass="choosebox">
                                <asp:ListItem Value="0" Selected="True">启用</asp:ListItem>
                                <asp:ListItem Value="1">停用</asp:ListItem>
                                </asp:RadioButtonList>
                                </td>
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

                if ($("#ddproducttype").val() == "0" && $("#ddshoptitle").val() == "0") {
                    $("#ddproducttype").css({ color: "red" });
                    flag = false; msg = "请选择商品分类和商品!";
                }
                else if ($("#ddproducttype").val() != "0" && $("#ddshoptitle").val() == "0") {
                    $("#ddshoptitle").css({ color: "red" });
                    flag = false; msg = "请选择商品!";
                }

                if ($("#tdOrder").val() == "") {
                    $("#Span1").css({ color: "red" });
                    return false;
                }
                if (!flag) { alert(msg); }
                return flag;
            }

        </script>

	</body>
</html>

