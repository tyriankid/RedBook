<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="wxreply.aspx.cs" Inherits="RedBookPlatform.wx.wxreply" %>
<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>
<%@ Register TagPrefix="UI" Namespace="Taotaole.Common" Assembly="Taotaole.Common" %>
<!DOCTYPE html>

<html>
	<head>
		<meta charset="UTF-8">
		<title>微信自定义回复</title>

		<link rel="stylesheet" type="text/css" href="/Resources/css/comm.css"/>
        <script src="/Resources/js/jquery.min.js" type="text/javascript" charset="utf-8"></script>
        <script src="/Resources/js/comm.js" type="text/javascript" charset="utf-8"></script>
	</head>
	<body>
        <form id="Form1" runat="server">
		<div class="wrapper">
			<div class="crumbs">
				<ul id="breadcrumbs" class="breadcrumb"> 
	                <li><a href="#">主页</a></li>
	                <li><a href="#">系统设置</a></li>
	                <li class="active"><a href="#" title="">微信自定义回复</a></li>
	            </ul>
			</div>
		    <div class="content">
		    	<div class="oprationbtn">
		    		<a class="btn" href="javascript:;" onclick="openNewWin('/wx/addReplyOnKey.aspx')">添加文本回复</a>
                    <a class="btn" href="javascript:;" onclick="openNewWin('/wx/addSingleArticle.aspx')">添加单图文回复</a>
                    <a class="btn" href="javascript:;" onclick="openNewWin('/wx/AddMultiArticle.aspx')">添加多图文回复</a>
		    	</div>
		    	<table class="table table-striped table-even">
                    <thead>
                        <tr>
                            <th>关键字</th>
                            <th>匹配类型</th>
                            <th>回复类型</th>
                            <th>操作</th>
                        </tr>
                    </thead>
                    
                    <tbody>
                        <asp:Repeater runat="server" ID="rptWxReply">
                        <ItemTemplate>
                            <tr>
                                <td><%#Eval("keyword") %></td>
                                <td><%#Globals.getEnumDisplayName(Globals.businessType.replyType,Convert.ToInt32(Eval("replytype")))%></td>
                                <td><%#Globals.getEnumDisplayName(Globals.businessType.messageType,Convert.ToInt32(Eval("messagetype")))%></td>
                                <td class="opration">
                                    <span role="btnEdit" messagetype="<%#Eval("messagetype") %>" replyid="<%#Eval("replyid") %>">修改</span> | 
                                    <span onclick="goDelete(<%#Eval("replyid") %>)">删除</span>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    </tbody>
                </table>
		    </div>
            
		</div>
        </form>

        <script>
            $(function () {
                //遍历所有编辑按钮,根据回复类型 1:文本 2:单图文 3:多图文 设置onclick事件,完成对应不同编辑页面的跳转
                $("[role='btnEdit']").each(function () {
                    switch ($(this).attr("messagetype")) {
                        case "1":
                            $(this).click(function () {
                                location.href = "addReplyOnKey.aspx?replyid=" + $(this).attr("replyid");
                            })
                            break;
                        case "2":
                            $(this).click(function () {
                                location.href = "addSingleArticle.aspx?replyid=" + $(this).attr("replyid");
                            })
                            break;
                        case "3":
                            $(this).click(function () {
                                location.href = "addMultiArticle.aspx?replyid=" + $(this).attr("replyid");
                            })
                            break;
                    }
                });

            });

            //删除列
            function goDelete(id) {
                if (!confirm("确认删除?")) { return false; }
                $.ajax({
                    type: "POST",
                    contentType: "application/json",
                    url: "wxreply.aspx/goDelete",
                    data: "{replyid:'" + id + "'}",
                    dataType: 'json',
                    success: function (result) {
                        alert(result.d);
                        location.reload();
                    }
                });
            }
        </script>
	</body>
</html>
