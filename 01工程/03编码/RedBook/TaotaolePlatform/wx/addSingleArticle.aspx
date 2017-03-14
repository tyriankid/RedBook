<%@ Page Language="C#" validateRequest="false" AutoEventWireup="true" CodeBehind="addSingleArticle.aspx.cs" Inherits="RedBookPlatform.wx.addSingleArticle" %>

<!DOCTYPE html>

<html>
	<head>
		<meta charset="UTF-8">
		<title>新增单图文回复</title>
		<link rel="stylesheet" type="text/css" href="<%=CSS_PATH %>comm.css"/>
        <script src="<%=JS_PATH %>jquery.min.js"></script>
        
	</head>
	<body>
		<div class="wrapper">
			<div class="crumbs">
				<ul id="breadcrumbs" class="breadcrumb"> 
	                <li><a href="#">主页</a></li>
	                <li><a href="#">系统设置</a></li>
	                <li class="active"><a href="#" title="">新增单图文回复</a></li>
	            </ul>
			</div>
		    <div class="content">
                <form runat="server" id="form1">


                    <!-- 单图文编辑-->
    <div class="tw_body">
	        <div class="tw_box box_left">
    	        <div class="body">        
    		        <asp:Label ID="LbimgTitle" runat="server"  Text="标题"></asp:Label>
                    <div class="img_fm">
            	        <div id="img_default" style="display:block" class="gy_bg">封面图片</div>
                        <img id="uploadpic" runat="server" class="fmImg" width="300"  />
                       
                    </div>            
    		        <asp:Label ID="Lbmsgdesc" runat="server" Text=""></asp:Label>
                </div>
            </div>
            <div id="box_move" class="tw_box box_left box_body">
                <div class="cont_body">
                    <div class="fgroup">
                        <span><em>*</em>标题：</span>
                        <asp:TextBox ID="Tbtitle" runat="server" Width="282px" onkeyup="syncSingleTitle(this.value)"></asp:TextBox>
                    </div>
                    <div class="fgroup">
                            <div style="width: 100%; height: 28px;">
                                <span><em>*</em>封 面：</span> <span id="swfu_container"><span>
                                <span id="spanButtonPlaceholder"></span>
                                </span><span id="divFileProgressContainer"></span></span>
								<div>建议尺寸：360*200</div>
                            </div>
                            <div id="smallpic" style="display: none; margin-left: 100px;">
                            </div>
                             <!--封面上传后，返回的图片地址，填充下面的input对象。-->
                            <input id="fmSrc" type="text" value="" style="display: none;" />
                        </div>

                    <div class="fgroup">
                        <span><em>*</em>摘要：</span>
                        <asp:TextBox ID="Tbdescription" runat="server" TextMode="MultiLine" onkeyup="syncAbstract(this.value)" ></asp:TextBox>
                    </div>
                    <div class="fgroup">
                        <span>自定义链接：</span>
                        http://<asp:TextBox ID="TbUrl" runat="server" Width="228px"></asp:TextBox>(可不填，若填写则优先跳转)
                    </div>            
                    <div style="width:700px">
                       <textarea id ="content" runat="server" runat="server"   style="width: 100%;"></textarea>
                    </div>
                </div>
                <i class="arrow arrow_out" style="margin-top: 0px;"></i>
                <i class="arrow arrow_in" style="margin-top: 0px;"></i>
            </div>        
             <div id="nextTW"></div>        
        </div>
<asp:HiddenField ID="hdpic" runat="server" />

		    	<table class="table table-striped tabel-textalign">
                    <tbody>
                        <tr>
                            <td>回复类型</td>
                            <td>
                                <asp:RadioButtonList ID="rdoReplytype" runat="server" RepeatLayout="UnorderedList" CssClass="choosebox">
                                    <asp:ListItem Value="1">关键字回复</asp:ListItem>
                                    <asp:ListItem Value="2">关注时回复</asp:ListItem>
                                    <asp:ListItem Value="3">无匹配回复</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr role="trKeyword">
                            <td><span style="color:red" id="spKeyword">*</span>关键字</td>
                            <td><input type="text" name="Keyword" id="txtKeyword" runat="server"  style="width: 50%;" /> 用户可通过该关键字搜到到这个内容</td>
                        </tr>
                        <tr role="trKeyword">
                            <td>匹配模式</td>
                            <td>
                                <asp:RadioButtonList ID="rdoMatchtype" RepeatLayout="UnorderedList" CssClass="choosebox" runat="server">
                                    <asp:ListItem Value="1">精确匹配</asp:ListItem>
                                    <asp:ListItem Value="2">模糊匹配</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td>状态</td>
                            <td>
                                <asp:RadioButtonList ID="rdoIsdisable" RepeatLayout="UnorderedList" CssClass="choosebox" runat="server">
                                    <asp:ListItem Value="0">启用</asp:ListItem>
                                    <asp:ListItem Value="1">禁用</asp:ListItem>
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
        <script src="<%=JS_PATH %>swfupload/swfupload.js" type="text/javascript"></script>
        <script src="<%=JS_PATH %>swfupload/handlers.js" type="text/javascript"></script>
        <link href="<%=CSS_PATH %>MutiArticle.css" rel="stylesheet" type="text/css" />
        <script type="text/javascript" charset="utf-8" src="/ueditor/ueditor.config.js"></script>
        <script type="text/javascript" charset="utf-8" src="/ueditor/ueditor.all.min.js"> </script>
        <script type="text/javascript" charset="utf-8" src="/ueditor/lang/zh-cn/zh-cn.js"></script>
        <script src="<%=JS_PATH %>swfupload/upload.js" type="text/javascript"></script>
        <script src="<%=JS_PATH %>MultiBox.js" type="text/javascript"></script>
        

        <script type="text/javascript">
            //上传图片控件需要
            var auth = "<%=(Request.Cookies[FormsAuthentication.FormsCookieName]==null ? string.Empty : Request.Cookies[FormsAuthentication.FormsCookieName].Value) %>";
            //content需要
            var editor = new baidu.editor.ui.Editor();
            editor.render("content");


            //关注时回复和误匹配回复如果存在则不能再被选
            var isMismatchExist = "<%=isMismatchExist%>";
            var isSubscribeReplyExist = "<%=isSubscribeReplyExist%>";
            $(function () {
                
                //判断并无效化已经存在的关注时回复和无匹配回复
                if (isMismatchExist == "True") {
                    $("[name='rdoReplytype'][value='3']").attr("disabled", true);
                }
                if (isSubscribeReplyExist == "True") {
                    $("[name='rdoReplytype'][value='2']").attr("disabled", true);
                }

                //只有点击了关键字回复时,显示关键字区域
                $("[name='rdoReplytype']").click(function () {
                    if ($(this).val() == "1") $("[role='trKeyword']").show();
                    else $("[role='trKeyword']").hide();
                });
                //根据载入时的replytype隐藏关键字区域
                var replytype = "<%=needsToHideKeyword%>";
                if (replytype == "False") $("[role='trKeyword']").show();
                else $("[role='trKeyword']").hide();
            });


            function validation() {//暂时在后台进行判断
                var flag = true;
                var msg = "";
                if (1 == 2) {
                    flag = false; msg = "请填写关键字";
                }
                if (!flag) { alert(msg); }
                return flag;
            }
        </script>

	</body>
</html>
