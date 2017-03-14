<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddSlidesWechat.aspx.cs" Inherits="RedBookPlatform.Slides.AddSlidesWechat" %>

<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>
<%@ Register TagPrefix="UI" Namespace="Taotaole.Common" Assembly="Taotaole.Common" %>
<!DOCTYPE html>

<html>
	<head>
		<meta charset="UTF-8">
		<title>添加文章</title>
		<link rel="stylesheet" type="text/css" href="<%=CSS_PATH %>comm.css"/>
        <link href="<%=CSS_PATH %>flashupload.css" rel="stylesheet" type="text/css" />
        <script src="<%=JS_PATH %>jquery.min.js"></script>
        <script src="<%=JS_PATH %>flashupload.js"></script>
 
     
	</head>
	<body
        <div class="divr"></div>
		<div class="wrapper">
			<div class="crumbs">
				<ul id="breadcrumbs" class="breadcrumb"> 
	                <li><a href="#">主页</a></li>
	                <li><a href="#">幻灯片管理</a></li>
	                <li class="active"><a href="#" title="">添加微信幻灯片</a></li>
	            </ul>
			</div>
		    <div class="content">
                <form runat="server" id="form1">
		    	<table class="table table-striped tabel-textalign">
                    <tbody>
                       
                        <tr>
                            <td><span class="red" id="spOrder" style="color:red"><strong>*</strong>&nbsp;</span>排序号</td>
                            <td><input type="text" name="slideorder" id="txtslideorder" runat="server" value="99999" style="width: 50%;" />&nbsp;&nbsp;排序号越小，排位越靠前.</td>
                        </tr>
                         <tr>
                            <td width="150"><span class="red" style="color:red"><strong>*</strong>&nbsp;</span>链接地址</td>
                            <td>
                            <input type="text" name="slidelink" id="txtslidelink" runat="server" style="width: 50%;" />
                            </td>
                        </tr>
                        <tr>
                            <td><span class="red" style="color:red"><strong>*</strong>&nbsp;</span>幻灯片</td>
                            <td> <asp:Image ID="thumbImg"  runat="server" width="350"/>
                                <asp:FileUpload ID="thumbUpload" runat="server"  onchange="onChangeFile(this)"/></td>
                        </tr>
                    
                         <tr>
                          
                            <td style="width:140px"><span class="red"style="color:red"><strong>*</strong>&nbsp;</span>幻灯片状态：</td>
                             <td >
                                <ul style="white-spacing:nowrap;">

　　<li style="display:inline-block;float:left;width:50px;">  <asp:RadioButton ID="rbqiyong" runat="server" Checked="true" Text="启用" GroupName="state" /></li>

　　<li style="display:inline-block;float:left;width:50px;"><asp:RadioButton   GroupName="state" ID="rbjinyong" Text="禁用" runat="server" /></li>
                                    </ul>
                            
                            </td>
                        </tr>
                     
                     
                       
                        <tr>
                            <td colspan="2" style="text-align:center">
                                <asp:Button runat="server" ID="btnSave" Text="保 存" OnClientClick="return validation();"   CssClass="btn btn-primary" OnClick="btnSave_Click" />
<%--                                <input type="button" name="name" value="保存" class="btn btn-primary" />--%>
                            </td>
                        </tr>
                    </tbody>
                </table>
                    </form>
		    </div>
		</div>
        <script type="text/javascript" charset="utf-8" src="/ueditor/ueditor.config.js"></script>
        <script type="text/javascript" charset="utf-8" src="/ueditor/ueditor.all.min.js"> </script>
        <!--建议手动加在语言，避免在ie下有时因为加载语言失败导致编辑器加载失败-->
        <!--这里加载的语言文件会覆盖你在配置项目里添加的语言类型，比如你在配置项目里配置的是英文，这里加载的中文，那最后就是中文-->
        <script type="text/javascript" charset="utf-8" src="/ueditor/lang/zh-cn/zh-cn.js"></script>
        <script type="text/javascript">
            var editor = new baidu.editor.ui.Editor();
            editor.render("contents");

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

            function validation() {
                var flag = true;
                var msg = "";
                //验证
                if ($("#txtslideorder").val().trim() == "" || $("#thumbImg").attr("src") == "" || $("#txtslidelink").val() == "") {
                    flag = false; msg = "请设置幻灯片,链接地址和排序号!";
                }



                if (!flag) { alert(msg); }
                return flag;
            }

            function setUeditor() {
                var myEditor = document.getElementById("contents");
                myEditor.value = editor.getContent();
            }

            $(function () {
                $("[role='picarr']").find("img").each(function () {
                    var imgUrl = " <%=UPLOAD_PATH %>" + $(this).attr("src");
                    $(this).attr("src", imgUrl);
                });
            })
                alert(imgUrl)
        </script>


	</body>
</html>
