<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="addBookCategory.aspx.cs" Inherits="RedBookPlatform.RedBook.addBookCategory" validateRequest="false" %>
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
                            <td>排序</td>
                            <td><input type="text" name="inpOrder" id="inpOrder" runat="server"  value="999" style="width: 50%;" /></td>
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

        <script type="text/javascript">


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

            });
        </script>


	</body>
</html>

