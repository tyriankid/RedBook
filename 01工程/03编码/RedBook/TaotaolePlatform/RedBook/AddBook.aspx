<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddBook.aspx.cs" validateRequest="false" Inherits="RedBookPlatform.RedBook.AddBook" %>
<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>
<%@ Register TagPrefix="UI" Namespace="Taotaole.Common" Assembly="Taotaole.Common" %>
<!DOCTYPE html>

<html>
	<head runat="server">
		<meta charset="UTF-8">
		<title>添加导购文章</title>
		
 <link rel="stylesheet" type="text/css" href="/Resources/css/comm.css"/>
        <link href="/Resources/css/flashupload.css" rel="stylesheet" type="text/css" />
        <script src="/Resources/js/jquery.min.js"></script>
        <script src="/Resources/js/flashupload.js"></script>
     
	</head>
	<body>
        <div class="divr"></div>
        <div  class="iframeBox" style =" width:1000px; height:800px; overflow:hidden;position: absolute;top: 0;left: 0;bottom:0;right: 0;margin: auto; z-index: 99999;display:none;border: 1px solid #c3c3c3;box-shadow: 2px 4px 6px #000;">
            <div  style="width:100%; height:100%;" >
                   <iframe src="" style="width:100%; height:100%;" frameborder="0" scrolling="no" >
                   </iframe>
             </div>
    </div>
		<div class="wrapper">
			<div class="crumbs">
				<ul id="breadcrumbs" class="breadcrumb"> 
	                <li><a href="#">主页</a></li>
	                <li><a href="#">导购文章管理</a></li>
	                <li class="active"><a href="#" title=""><%=show %></a></li>
	            </ul>
			</div>
		    <div class="content">
                <form runat="server" id="form1">
		    	<table class="table table-striped tabel-textalign">
                    <tbody>
                       
                        <tr>
                            <td><span class="red" style="color:red" id="spOrder"><strong>*</strong>&nbsp;</span>排序号</td>
                            <td><input type="text" name="Ordersn" id="Ordersn" runat="server" value="99999" style="width: 50%;" />&nbsp;&nbsp;排序号越小，排位越靠前.</td>
                        </tr>
                         <tr>
                            <td width="150"><span class="red" style="color:red"><strong>*</strong>&nbsp;</span>所属分类</td>
                            <td>
                                <asp:DropDownList ID="ddlCategory" runat="server"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td><span class="red" style="color:red"><strong>*</strong>&nbsp;</span>文章标题</td>
                            <td><input type="text" name="title" id="title" runat="server" value="" style="width: 50%;" /></td>
                        </tr>
                        <tr>
                            <td><span class="red" style="color:red"><strong>*</strong>&nbsp;</span>文章副标题</td>
                            <td><input type="text" name="subTitle" id="subTitle" runat="server" value="" style="width: 50%;" /></td>
                        </tr>
                        <tr>
                            <td>关键字</td>
                            <td><input type="text" name="keywords" id="keywords" runat="server"  value="" style="width: 50%;" /></td>
                        </tr>
                        <tr>
                            <td>文章描述</td>
                            <td><textarea rows="5" cols="5" name="description" id="description" runat="server"  class="span12" style="width: 50%;"></textarea></td>
                        </tr>
                        <tr>
                            <td>关联商品</td>
                            <td><input type="button" id="btnProductList" value="选择商品" onclick="bindProducts()"/><input type="text" runat="server" style="display:none" id="hidProductIds" /></td>
                        </tr>
                     
                        <tr>
                            <td>展示背景图</td>
                            <td>
                                <asp:Image ID="thumbImg"  runat="server" width="100" height="100"/>
                                <asp:FileUpload ID="thumbUpload" runat="server"  onchange="onChangeFile(this)"/>
                            
                            </td>
                        </tr>
                        <%--<tr>
                            <td><span class="red">*</span>轮播图</td>
                            <td role="picarr">
                                <UI:ProductFlashUpload ID="flashUpload" runat="server" />      
                            </td>
                        </tr>--%>
                        <tr>
                            <td><span class="red" style="color:red"><strong>*</strong>&nbsp;</span>文章内容详情：</td>
                            <td>
                                <textarea id ="contents" runat="server" onblur="setUeditor()" style="width:100%"></textarea>
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

            function bindProducts() {
                $(".iframeBox").show(300).find("iframe").attr("src", "BookProductList.aspx");
           };
            

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
                //分类和品牌不能为未选中
                if ($("#title").val().trim() == "" || $("#ddlCategory").val() == "0") {
                    flag = false; msg = "请设置文章标题或文章类别!";
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
        </script>


	</body>
</html>
