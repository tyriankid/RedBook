<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BookCategoryList.aspx.cs" Inherits="RedBookPlatform.RedBook.BookCategoryList" %>
<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>
<!DOCTYPE html>
<html>
	<head runat="server">
		<meta charset="UTF-8">
		<title>小红书分类列表</title>

		<link rel="stylesheet" type="text/css" href="/Resources/css/comm.css"/>
        <script src="/Resources/js/jquery.min.js" type="text/javascript" charset="utf-8"></script>
        <script src="/Resources/js/comm.js" type="text/javascript" charset="utf-8"></script>
	</head>
	<body>
        <form id="Form1" runat="server">
		<div class="wrapper">
			<div class="crumbs">
				<ul id="breadcrumbs" class="breadcrumb"> 
	                <li><a href="#">小红书</a></li>
	                <li><a href="#">分类管理</a></li>
	                <li class="active"><a href="#" title="">小红书分类列表</a></li>
	            </ul>
			</div>
		    <div class="content">
		    	<div class="oprationbtn">
		    		<a class="btn"  style="<%=display%>" href="javascript:;" onclick="openNewWin('/RedBook/addBookCategory.aspx')">添加分类</a>
		    		<div class="search">
		    			查询关键字
                        <asp:TextBox runat="server" ID="tboxKeyword" MaxLength="10" Width="400px"></asp:TextBox>
                        <asp:Button runat="server" ID="btnSearch" CssClass="btn btn-info" Text="查询" OnClick="btnSearch_Click" />
		    		</div>
		    	</div>
		    	<table class="table table-striped table-even">
                    <thead>
                        <tr>
                            <th style="width:100px; text-align:center;">排序号</th>
                            <th>分类名</th>
                            <th>分类备注</th>
                            <th style="width:100px;text-align:center;<%=display%>">操作</th>
                        </tr>
                    </thead>
                    
                    <tbody>
                        <asp:Repeater runat="server" ID="rptAdmin">
                        <ItemTemplate>
                            <tr>
                                <td style="text-align:center;"><%#Eval("SortBaseNum") %></td>
                                <td><%#Eval("CategoryName") %></td>
                                <td><%#Eval("Description") %></td>
                                <td class="opration" style="text-align:center;<%=display%>"><span onclick="openNewWin('/RedBook/addBookCategory.aspx?id=<%#Eval("Id") %>')">修改</span> | <span onclick="goDelete('<%#Eval("CategoryName") %>','<%#Eval("Id") %>')">删除</span></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    </tbody>
                </table>
                <div class="pager oprationbtn">
                    <UI:Pager runat="server" ShowTotalPages="true" ID="pager1" />
                </div>
		    </div>
            
		</div>
        </form>

        <script>
            //删除列
            function goDelete(name, id) {
                if (!confirm("确认删除" + name + "?")) { return false; }
                $.ajax({
                    type: 'post', dataType: 'json', timeout: 10000,
                    async: false,
                    url: location.href,
                    data: {
                        action: "goDelete",
                        id: id,
                        name: name
                    },
                    success: function (resultData) {
                        if (resultData.success == true) {
                            alert(resultData.msg);
                            location.reload();
                        }
                    }
                });
            }
            $(function () {
                $(".0").remove();
            })

        </script>
	</body>
</html>
