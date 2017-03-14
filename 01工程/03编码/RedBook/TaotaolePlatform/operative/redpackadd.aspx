<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="redpackadd.aspx.cs" Inherits="RedBookPlatform.operative.redpackadd" %>

<!DOCTYPE html>

<html>
	<head runat="server">
		<meta charset="UTF-8">
		<title>添加商品</title>
		<link rel="stylesheet" type="text/css" href="/Resources/css/comm.css"/>
        <script src="/Resources/js/jquery.min.js"></script>
	</head>
	<body>
		<div class="wrapper">
			<div class="crumbs">
				<ul id="breadcrumbs" class="breadcrumb"> 
	                <li><a href="#">主页</a></li>
	                <li><a href="operative/redpackinfo.aspx">红包基本信息</a></li>
	                <li class="active"><a href="javascript:void(0);" title=""><asp:Literal runat="server" Text="添加红包模板" ID="litTitle"></asp:Literal></a></li>
	            </ul>
			</div>
		    <div class="content">
                <form runat="server" id="form1">
		    	<table class="table table-striped tabel-textalign">
                    <tbody>
                        <tr>
                            <td><span class="red">*</span>红包标题</td>
                            <td><asp:TextBox runat="server" ID="tboxTitle" MaxLength="100" Width="300px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>限定金额</td>
                            <td><asp:TextBox runat="server" ID="tboxAmount" MaxLength="100" Width="300px" onKeyUp="value=value.replace(/\D/g,'')"></asp:TextBox>元</td>
                        </tr>
                        <tr>
                            <td><span class="red">*</span>抵扣金额</td>
                            <td><asp:TextBox runat="server" ID="tboxDiscount" MaxLength="100" Width="300px" onKeyUp="value=value.replace(/\D/g,'')"></asp:TextBox>元</td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align:center">
                                <asp:Button runat="server" ID="btnSave" name="btnSave" OnClientClick="return validation();" class="btn btn-primary" Text="保存" OnClick="btnSave_Click"  />
                            </td>
                        </tr>
                    </tbody>
                </table>
                    </form>
		    </div>
		</div>
	</body>
    <script type="text/javascript">
        function validation(){

            var flag = true;
            var msg = "";
                if ($("#tboxTitle").val().length == 0)
                {
                    msg="请输入标题";
                    flag = false;
                }
                if ($("#tboxDiscount").val().length == 0) {
                    msg = "请输入抵扣金额";
                    flag = false;
                }
                if ($("#tboxAmount").val().length == 0) {
                    msg = "请输入限定金额，如为无限制红包请输入0";
                    flag = false;
                }
                if (!flag) { alert(msg); }
                return flag;
           
        }
        </script>
</html>