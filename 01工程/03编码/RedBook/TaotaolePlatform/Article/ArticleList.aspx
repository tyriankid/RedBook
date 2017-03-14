<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ArticleList.aspx.cs" Inherits="RedBookPlatform.Article.ArticleList" %>
<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>
<!DOCTYPE html>
<html>
	<head runat="server">
		<meta charset="UTF-8">
		<title>文章列表</title>

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
	                <li><a href="#">文章管理</a></li>
	                <li class="active"><a href="#" title="">文章列表</a></li>
	            </ul>
			</div>
		    <div class="content">
		    	<div class="oprationbtn">
		    		<a class="btn" href="javascript:;" onclick="openNewWin('/Article/addArticle.aspx')">添加文章</a>

		    		<div class="search" style="">
                          <span style="padding-right:3px; padding-left:10px">文章类型:</span>
                        <asp:DropDownList ID="ddlCategory" runat="server"></asp:DropDownList>


		    		   <span style="padding-right:3px; padding-left:10px">查询关键字:</span>
                        <asp:TextBox runat="server" ID="tboxKeyword" Width="320px" MaxLength="10"></asp:TextBox>
		    			<select name="sel"  runat="server" id="selMultiple">
                            <option value="0">按添加时间倒序</option>
		    				<option value="1">按添加时间升序</option>
		    				
		    			</select>
                        <asp:Button runat="server" ID="btnSearch" CssClass="btn btn-info" Text="查询" OnClick="btnSearch_Click" />
		    		</div>
		    	</div>
		    	<table class="table table-striped table-even">
                    <thead>
                        <tr>
                              <th style="width:80px;text-align:center">排序</th>
                            <th>文章ID</th>
                            <th>标题</th>
                            <th>文章类型</th>
                            <th style="width:120px;text-align:center">操作</th>
                        </tr>
                    </thead>
                    
                    <tbody>
                        <asp:Repeater runat="server" ID="rptAdmin">
                        <ItemTemplate>
                            <tr>
                                 <td style="width:80px;text-align:center"><input type="text" name="ordersn" id="ordersn" value='<%#Eval("ordersn") %>' onchange="uporders(<%#Eval("id") %>,this)" size="2" /></td>
                                <td><%#Eval("id") %></td>
                                <td><div class="imgBox"><img src="<%=G_UPLOAD_PATH %><%#Eval("thumb") %>"/></div><%#Eval("title") %></td>
                                 
                                 <td><%#Eval("name") %></td>
                                
                              
                                <td style="width:120px;text-align:center" class="opration"><span onclick="openNewWin('/Article/addArticle.aspx?id=<%#Eval("id") %>')">修改</span> | <span onclick="goDelete('<%#Eval("title") %>','<%#Eval("id") %>')">删除</span></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    </tbody>
                </table>
                <div class="pager oprationbtn">
                    <input type="button" class="pager oprationbtn" value="排 序" onclick="UpOrder()"  style="float:left;"/>
                    <UI:Pager runat="server" ShowTotalPages="true" ID="pager1" />
                </div>
		    </div>
            
		</div>
        </form>

        <script>

            //更改排序号
            var yid = ""; var order = "";
            function uporders(x, y) {
                yid += x + ",";
                if (y.value == "") {
                    y.value = 0;
                }
                order += y.value + ",";
            }

            function UpOrder() {

                if (yid == "" || order == "") {
                    alert("未更改任何排序")
                }
                else {
                    if (!confirm("确认更改当前排序?")) { return false; }
                    $.ajax({
                        type: "POST",
                        contentType: "application/json",
                        url: "ArticleList.aspx/UpOrder",
                        data: "{yid:'" + yid + "',order:'" + order + "'}",
                        dataType: 'json',
                        success: function (result) {
                            alert(result.d);
                        }
                    });
                }
            }

            //删除列
            function goDelete(name, id) {
                if (!confirm("确认删除" + name + "?")) { return false; }
                $.ajax({
                    type: "POST",
                    contentType: "application/json",
                    url: "ArticleList.aspx/goDelete",
                    data: "{id:'" + id + "'}",
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
