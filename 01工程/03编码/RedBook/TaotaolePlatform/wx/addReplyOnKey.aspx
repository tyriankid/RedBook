<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="addReplyOnKey.aspx.cs" Inherits="RedBookPlatform.wx.addReplyOnKey" %>
<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>
<!DOCTYPE html>

<html>
	<head>
		<meta charset="UTF-8">
		<title>新增文本回复</title>
		<link rel="stylesheet" type="text/css" href="<%=CSS_PATH %>comm.css"/>
        <script src="<%=JS_PATH %>jquery.min.js"></script>
        
	</head>
	<body>
		<div class="wrapper">
			<div class="crumbs">
				<ul id="breadcrumbs" class="breadcrumb"> 
	                <li><a href="#">主页</a></li>
	                <li><a href="#">系统设置</a></li>
	                <li class="active"><a href="#" title="">新增文本回复</a></li>
	            </ul>
			</div>
		    <div class="content">
                <form runat="server" id="form1">
		    	<table class="table table-striped tabel-textalign">
                    <tbody>
                        <tr>
                            <td>回复内容</td>
                            <td><asp:TextBox ID="txtContent" runat="server" Rows="5" Width="500" TextMode="MultiLine"></asp:TextBox></td>
                            
                        </tr>
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

        <script type="text/javascript">
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
                    if($(this).val() == "1") $("[role='trKeyword']").show();
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
                if (1==2) {
                    flag = false; msg = "请填写关键字";
                }
                if (!flag) { alert(msg); }
                return flag;
            }
        </script>

	</body>
</html>
