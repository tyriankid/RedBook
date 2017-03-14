<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CategoryList.aspx.cs" Inherits="RedBookPlatform.productClassify.productCategory" %>
<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>
<!DOCTYPE html>
<html>
	<head runat="server">
		<meta charset="UTF-8">
		<title>分类列表</title>

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
	                <li><a href="#">商品分类管理</a></li>
	                <li class="active"><a href="#" title="">商品分类列表</a></li>
	            </ul>
			</div>
		    <div class="content">
		    	<div class="oprationbtn">
		    		<a class="btn"  style="<%=display%>" href="javascript:;" onclick="openNewWin('/productClassify/addCategory.aspx?action=<%= actions%>')">添加分类</a>
		    		<div class="search">
		    			查询关键字
                        <asp:TextBox runat="server" ID="tboxKeyword" MaxLength="10" Width="400px"></asp:TextBox>
		    			<select name="" id="selMultiple" runat="server" >
		    				<option value="1">按排序号升序</option>
		    				<option value="2">按排序号倒序</option>
		    			</select>
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
                                <td style="text-align:center;"><%#Eval("orders") %></td>
                                <td>
                                    <div class='imgBox <%#Eval("pic_url").ToString()==""?"0":Eval("pic_url").ToString().Split('|')[0] %>'  ><img src="/Resources/uploads/<%#Eval("pic_url").ToString()==""?"0":Eval("pic_url").ToString().Split('|')[0] %>"/></div>
                                    <div class='imgBox <%#Eval("pic_url").ToString()==""?"0":Eval("pic_url").ToString().Split('|')[0] %>'  ><img src="/Resources/uploads/<%#Eval("pic_url").ToString()==""?"0":Eval("pic_url").ToString().Split('|')[1] %>"/></div> 
                                    <%#Eval("name") %></td>
                                <td><%#Eval("info") %></td>
                                <td class="opration" style="text-align:center;<%=display%>"><span onclick="openNewWin('/productClassify/addCategory.aspx?cateid=<%#Eval("cateid") %>')">修改</span> | <span onclick="goDelete('<%#Eval("name") %>',<%#Eval("cateid") %>)">删除</span></td>
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
