<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="addCategory.aspx.cs" Inherits="RedBookPlatform.productClassify.addCategory" validateRequest="false" %>
<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>
<%@ Register TagPrefix="UI" Namespace="Taotaole.Common" Assembly="Taotaole.Common" %>

<!DOCTYPE html>
<html>
	<head runat="server">
		<meta charset="UTF-8">
		<title>添加分类</title>
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
	                <li><a href="#">分类管理</a></li>
	                <li class="active"><a href="#" title="">添加分类</a></li>
	            </ul>
			</div>
		    <div class="content">
                <form runat="server" id="form1">
		    	<table class="table table-striped tabel-textalign">
                    <tbody>
                        <tr id="trClassify" runat="server" style="display:none">
                            <td><span class="red">*</span>分类ID</td>
                            <td><input type="text" name="cateid" id="tdCateid" runat="server" value="" style="width: 50%;" aria-disabled="True"  /></td>
                        </tr>
                        <tr>
                            <td width="150">分类名称</td>
                            <td><input type="text" name="namae" id="tdName" runat="server" value="" style="width: 50%;" /></td>
                        </tr>
                        <tr>
                            <td>所属上级分类</td>
                            <td><asp:DropDownList ID="sjfl" runat="server"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td>排序</td>
                            <td><input type="text" name="inpOrder" id="inpOrder" runat="server"  value="999" style="width: 50%;" /></td>
                        </tr>
                        <tr>
                            <td><span class="red" style="color:red"><strong>*&nbsp;</strong></span>分类类型</td>
                            <td>
                                <asp:DropDownList ID="drmodel" runat="server">
                                    <asp:ListItem Value="2">文章</asp:ListItem>
                                    <asp:ListItem Value="1">商品</asp:ListItem>
                                </asp:DropDownList>
                                
                            </td>
                        </tr>

                        <tr class="show">
                            <td><span class="red" id="image">*</span>图标地址（选&nbsp;&nbsp;&nbsp;中）</td>
                            <td>
                                <asp:Image ID="imPictureUrl"  runat="server" width="100" height="100"/>
                                <asp:FileUpload ID="thumbUpload" runat="server"  onchange="onChangeFile(this)"/>
                            </td>
                        </tr>
                         <tr class="show">
                            <td><span class="red" id="imageTwo">*</span>图标地址（没选中）</td>
                            <td>
                                <asp:Image ID="imPictureUrltwo"  runat="server" width="100" height="100"/>
                                <asp:FileUpload ID="thumbUploadtwo" runat="server"  onchange="onChangeFiletwo(this)"/>
                            </td>
                        </tr>
                        <tr>
                            <td>分类地址链接</td>
                            <td><input type="text" name="inpUrl" id="inpUrl" runat="server"  value="" style="width: 50%;" /></td>
                        </tr>
                           <tr>
                            <td>备注</td>
                            <td>
                                <textarea name="inpInfo" id="inpInfo" runat="server" rows="7" style="width: 800px;" ></textarea>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align:center">
                                <asp:Button runat="server" ID="btnSave" Text="保 存"  OnClientClick="return validation();"  CssClass="btn btn-primary"  OnClick="btnSave_Click"/>
                            </td>
                        </tr>
                    </tbody>
                </table>
                    <input type="hidden" name="field＿name"  id="hidCateid" runat="server"> 
                    </form>
		    </div>
		</div>
        <script type="text/javascript" charset="utf-8" src="/ueditor/ueditor.config.js"></script>
        <script type="text/javascript" charset="utf-8" src="/ueditor/ueditor.all.min.js"> </script>
        <script type="text/javascript" charset="utf-8" src="/ueditor/lang/zh-cn/zh-cn.js"></script>
        <script type="text/javascript">
            var editor = new baidu.editor.ui.Editor();
            editor.render("content");

            //选择新图片后,旧图片被替换
            function onChangeFile(sender) {
                var i = 0;
                var filename = sender.value;
                if (filename == "") {
                    return "";
                }
                var ExName = filename.substr(filename.lastIndexOf(".") + 1).toUpperCase();
                if (ExName == "JPG" || ExName == "BMP" || ExName == "GIF" || ExName == "PNG") {
                    //店招图片
                    if (sender.getAttribute('id') == 'thumbUpload') {
                        $(sender).prev().attr('src', window.URL.createObjectURL(sender.files[0]))
                    }
                    if (sender.getAttribute('id') == 'picarr') {
                        $("<img src='" + window.URL.createObjectURL(sender.files[i]) + "' width='100' height='100' >").prependTo($(sender).parent());
                        i++;
                    }
                }
                else {
                    alert('请选择正确的图片格式！');
                    sender.value = null;
                    return false;
                }
            }
            function onChangeFiletwo(sender) {
                var i = 0;
                var filename = sender.value;
                if (filename == "") {
                    return "";
                }
                var ExName = filename.substr(filename.lastIndexOf(".") + 1).toUpperCase();
                if (ExName == "JPG" || ExName == "BMP" || ExName == "GIF" || ExName == "PNG") {
                    //店招图片
                    if (sender.getAttribute('id') == 'thumbUploadtwo') {
                        $(sender).prev().attr('src', window.URL.createObjectURL(sender.files[0]))
                    }
                    if (sender.getAttribute('id') == 'picarr') {
                        $("<img src='" + window.URL.createObjectURL(sender.files[i]) + "' width='100' height='100' >").prependTo($(sender).parent());
                        i++;
                    }
                }
                else {
                    alert('请选择正确的图片格式！');
                    sender.value = null;
                    return false;
                }
            }
            function setUeditor() {
                var myEditor = document.getElementById("content");
                myEditor.value = editor.getContent();
            }
           
            function validation() {
                if ($("#hidCateid").val()==0) {
                    var flag = true;
                    var msg = "";
                    //头像图片不能为空
                    if ($("#thumbUpload").val() == "") {
                        $("#image").css({ color: "red" })
                        flag = false; msg = "请上传图片!";
                        
                    }
                    if ($("#thumbUploadtwo").val() == "") {
                        $("#imageTwo").css({ color: "red" })
                        flag = false; msg = "请上传图片!";
                    }
                    if (!flag) { alert(msg); }
                    return flag;
                }
            }
            $(function () {
                var ddl = document.getElementById("drmodel")
                var index = ddl.selectedIndex;
                var Value = ddl.options[index].value;
                if (Value == "2") {
                    $(".show").hide();
                    $("#btnSave").removeAttr("onclick");
                }
                else {
                    $(".show").show();
                }
            });
        </script>


	</body>
</html>

