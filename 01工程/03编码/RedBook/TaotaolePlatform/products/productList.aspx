<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="productList.aspx.cs" Inherits="RedBookPlatform.products.productList" %>
<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>
<!DOCTYPE html>
<html>
	<head runat="server">
		<meta charset="UTF-8">
		<title>商品列表</title>

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
	                <li><a href="#">商品管理</a></li>
	                <li class="active"><a href="#" title="">商品列表</a></li>
	            </ul>
			</div>
		    <div class="content">
		    	<div class="oprationbtn">
		    		<a class="btn"  style="<%=display%>" href="javascript:;" onclick="openNewWin('/products/addProduct.aspx')">添加商品</a>
		    		<div class="search">
		    			查询关键字
                        <asp:TextBox runat="server" ID="tboxKeyword" MaxLength="10" Width="400px"></asp:TextBox>
                        <asp:DropDownList ID="dropState" runat="server">
                            <asp:ListItem Value="">全部</asp:ListItem>
                            <asp:ListItem Value="推荐">推荐</asp:ListItem>
                             <asp:ListItem Value="人气">人气</asp:ListItem>
                            <asp:ListItem Value="爆款">爆款</asp:ListItem>
                             <asp:ListItem Value="特价">特价</asp:ListItem>
                            <asp:ListItem Value="优惠">优惠</asp:ListItem>
                            <asp:ListItem Value="新手">新手</asp:ListItem>
                        </asp:DropDownList>
		    			<select name="" id="selTime" runat="server">
                            <option value="1">按添加时间倒序</option>
		    				<option value="0">按添加时间升序</option>
		    			</select>
                        <asp:Button runat="server" ID="btnSearch" CssClass="btn btn-info" Text="查询" OnClick="btnSearch_Click" />
		    		</div>
		    	</div>
		    	<table class="table table-striped table-even">
                    <thead>
                        <tr>
                            <th style="width:80px;text-align:center">商品ID</th>
                            <th>商品名</th>
                            <th>商品副标题</th>
                            <th>商品单价</th>
                            <th>商品类型</th>
                            <th>货号</th>
                            <th>库存</th>
                            <th  style="width:120px;text-align:center;<%=display%>">操作</th>
                        </tr>
                    </thead>
                    
                    <tbody>
                        <asp:Repeater runat="server" ID="rptAdmin">
                        <ItemTemplate>
                            <tr>
                                <td style="width:80px;text-align:center"><%#Eval("productId") %></td>
                                <td><div class="imgBox"><img src="<%=G_UPLOAD_PATH %><%#Eval("thumb") %>"/></div> <%#Eval("title") %></td>
                                <td><%#Eval("title2") %></td>
                                <td><%#Eval("money","{0:F2}") %></td>
                                <td><%# Taotaole.Common.Globals.getEnumDisplayName(Taotaole.Common.Globals.businessType.productType,(int)Eval("typeid")) %></td>
                                <td><%#Eval("number") %></td>
                                <td><%#Eval("stock") %></td>
                                <td  style="width:120px;text-align:center;<%=display%>" class="opration"><span onclick="openNewWin('/products/addProduct.aspx?productId=<%#Eval("productId") %>')">修改</span> | <span onclick="goDelete('<%#Eval("title") %>',<%#Eval("productId") %>)">删除</span></td>
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
            function goDelete(name,id) {
                if (!confirm("确认删除"+name+"?")) { return false; }
                $.ajax({
                    type: "POST",  
                    contentType: "application/json", 
                    url: "productList.aspx/goDelete",
                    data: "{productId:'" + id + "'}",
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
