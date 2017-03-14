<%@ Page  validateRequest="false" Language="C#" AutoEventWireup="true" CodeBehind="addProduct.aspx.cs" Inherits="RedBookPlatform.products.addProduct" %>
<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>
<%@ Register TagPrefix="UI" Namespace="Taotaole.Common" Assembly="Taotaole.Common" %>

<!DOCTYPE html>
<html>
	<head>
		<meta charset="UTF-8">
		<title>添加商品</title>
		<link rel="stylesheet" type="text/css" href="<%=CSS_PATH %>comm.css"/>
        <link href="<%=CSS_PATH %>flashupload.css" rel="stylesheet" type="text/css" />
        <script src="<%=JS_PATH %>jquery.min.js"></script>
        <script src="<%=JS_PATH %>flashupload.js"></script>
        
	</head>
	<body>
		<div class="wrapper">
			<div class="crumbs">
				<ul id="breadcrumbs" class="breadcrumb"> 
	                <li><a href="#">主页</a></li>
	                <li><a href="#">商品管理</a></li>
	                <li class="active"><a href="#" title="">添加商品</a></li>
	            </ul>
			</div>
		    <div class="content">
                <form runat="server" id="form1">
		    	<table class="table table-striped tabel-textalign">
                    <tbody>
                        <tr>
                            <td width="150"><span class="red" style="color:red"><strong>*</strong>&nbsp;</span>所属分类</td>
                            <td>
                                <asp:DropDownList ID="ddlCategory" runat="server"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td><span class="red" style="color:red"><strong>*</strong>&nbsp;</span>所属品牌</td>
                            <td>
                                <asp:DropDownList ID="ddlBrand" runat="server"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td><span class="red" style="color:red"><strong>*</strong>&nbsp;</span>商品标题</td>
                            <td><a><input type="text" name="title" id="title" runat="server" value="" style="width: 50%;" /><span style="color:#C2C2C2" id ="spTitle"> *商品标题不能为空.</span></a></td>
                        </tr>
                        <tr>
                            <td>商品副标题</td>
                            <td><input type="text" name="title2" id="title2" runat="server" value="" style="width: 50%;" /></td>
                        </tr>
                        <tr>
                            <td>关键字</td>
                            <td><input type="text" name="keyword" id="keyword" runat="server"  value="" style="width: 50%;" /></td>
                        </tr>
                        <tr>
                            <td>商品描述</td>
                            <td><textarea rows="5" cols="5" name="description" id="description" runat="server"  class="span12" style="width: 50%;"></textarea></td>
                        </tr>
                        <tr>
                            <td><span class="red">*</span>商品实际单价</td>
                            <td><a><input type="text" name="money" id="money" onkeyup="value=value.replace(/\D/g,'')" runat="server"  value="" style="width: 50%;" /> &nbsp;&nbsp;元<span style="color:#C2C2C2" id ="spMoney">*商品单价不能为空，商品价格为实际价格（20元话费价格为20，此处价格不是一元购售价）</span></a></td>
                        </tr>
                        <tr>
                            <td><span class="red"></span>货号</td>
                            <td><input type="text" name="number" id="number"  maxlength="50" runat="server"  value="" style="width: 50%;" /></td>
                        </tr>
                        <tr>
                            <td><span class="red"></span>库存</td>
                            <td><input type="text" name="stock" id="stock"  maxlength="50" runat="server" style="width: 50%;" /></td>
                        </tr>
                        <tr>
                            <td><span class="red" style="color:red"><strong>*</strong>&nbsp;</span>缩略图</td>
                            <td>
                                <asp:Image ID="thumbImg"  runat="server" width="100" height="100"/>
                                <asp:FileUpload ID="thumbUpload" runat="server"  onchange="onChangeFile(this)"/>
                            
                            </td>
                        </tr>
                        <tr>
                            <td><span class="red" style="color:red"><strong>*</strong>&nbsp;</span>轮播图</td>
                            <td role="picarr">
                                <UI:ProductFlashUpload ID="flashUpload" runat="server" />      
                            </td>
                        </tr>
                        <tr>
                            <td><span class="red" style="color:red"><strong>*</strong>&nbsp;</span>商品内容详情：</td>
                            <td>
                                <textarea id ="content" runat="server" onblur="setUeditor()" style="width:100%"></textarea>
                            </td>
                        </tr>
                        <tr>
                            <td><span class="red"></span>商品属性：</td>
                            <td>
                                <asp:CheckBoxList ID="chkProductAttr" runat="server" RepeatLayout="UnorderedList" CssClass="choosebox">
                                    <asp:ListItem Value="推荐">推荐</asp:ListItem>
                                    <asp:ListItem Value="人气">人气</asp:ListItem>
                                    <asp:ListItem Value="爆款">爆款</asp:ListItem>
                                    <asp:ListItem Value="特价">特价</asp:ListItem>
                                    <asp:ListItem Value="优惠">优惠</asp:ListItem>
                                    <asp:ListItem Value="新手">新手</asp:ListItem>
                                </asp:CheckBoxList>
                            </td>
                        </tr>
                        <tr>
                            <td><span class="red"></span>商品类型：</td>
                            <td>
                                <asp:RadioButtonList ID="rdoProductType" runat="server" RepeatLayout="UnorderedList" CssClass="choosebox">
                                    <asp:ListItem Value="0">实物</asp:ListItem>
                                    <asp:ListItem Value="1">话费</asp:ListItem>
                                    <asp:ListItem Value="2">游戏</asp:ListItem>
                                    <asp:ListItem Value="3">Q币</asp:ListItem>
                                    <asp:ListItem Value="4">账号</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>

                        </tr>
                        <tr>
                                                        <td><span class="red"></span>业务类型：</td>
                                <td>
                                <asp:RadioButtonList ID="operationtype" runat="server" RepeatLayout="UnorderedList" CssClass="choosebox">
                                <asp:ListItem Value="0">一元购/直购商品</asp:ListItem>
                                <asp:ListItem Value="1">活动商品</asp:ListItem>
                                </asp:RadioButtonList>
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
            function validation() {
                var flag = true;
                var msg = "";
                //if ($("#chkProductAttr :checked").val() == ""||$("#chkProductAttr :checked").val() ==null)
                //{
                //    flag = false; msg = "请选择商品属性!";
                //}
                if ($("#title").val() == "") {
                    $("#spTitle").css({ color: "red" });
                    return false;
                }
                else {
                    $("#spTitle").css({ color: "#8E8E8E" });
                }
                if ($("#money").val() == "") {
                    $("#spMoney").css({ color: "red" });
                    return false;
                }
                else {
                    $("#spMoney").css({ color: "#8E8E8E" });
                }
                if ($("#stock").val()=="")
                {
                    flag = false; msg = "请填写库存!";
                }
                //分类和品牌不能为未选中
                if ($("#ddlCategory").val() == "0" || $("#ddlBrand").val() == "0") {
                    flag = false; msg = "请选择商品分类和品牌!";
                }
                var vRbtid = document.getElementById("rdoProductType");
                //得到所有radio
                var vRbtidList = vRbtid.getElementsByTagName("INPUT");
                if (vRbtidList[0].checked == false && vRbtidList[1].checked == false && vRbtidList[2].checked == false && vRbtidList[3].checked == false && vRbtidList[4].checked == false) {
                    alert("请选择商品类型!");
                    return false;
                }
                if (!flag) { alert(msg);}
                return flag;
            }
            function setUeditor() {
                var myEditor = document.getElementById("content");
                myEditor.value = editor.getContent();
            }

            $(function () {
                $("[role='picarr']").find("img").each(function () {
                    var imgUrl = " <%=UPLOAD_PATH %>" + $(this).attr("src");
                    $(this).attr("src", imgUrl);
                });
            })
            //alert(imgUrl)
        </script>


	</body>
</html>
