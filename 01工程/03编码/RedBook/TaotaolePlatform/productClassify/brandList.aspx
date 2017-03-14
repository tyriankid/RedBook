<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="brandList.aspx.cs" Inherits="RedBookPlatform.productClassify.brandList" %>
<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>
<!DOCTYPE html>
<html>
	<head runat="server">
		<meta charset="UTF-8">
		<title>品牌列表</title>
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
	                <li><a href="#">商品品牌管理</a></li>
	                <li class="active"><a href="#" title="">商品品牌列表</a></li>
	            </ul>
			</div>
		    <div class="content">
		    	<div class="oprationbtn">
		    		<a class="btn" style="<%=display%>" href="javascript:;" onclick="openNewWin('/productClassify/addBrand.aspx')">添加品牌</a>
		    		<div class="search">
		    			查询关键字
                        <asp:TextBox runat="server" ID="tboxKeyword" MaxLength="10" Width="400px"></asp:TextBox>
		    			<select name="" runat="server" id="selOrder">
                            <option value="1">按排序号升序</option>
                            <option value="2">按排序号倒序</option>
		    			</select>
                        <asp:Button runat="server" ID="btnSearch" CssClass="btn btn-info" Text="查询" OnClick="btnSearch_Click" />
		    		</div>
		    	</div>
		    	<table class="table table-striped table-even">
                    <thead>
                        <tr>
                            <th style="width:80px;text-align:center">排序</th>
                            <th>品牌名称</th>
                            <th>所属分类</th>
                            <th style="width:120px;text-align:center;<%=display%>">操作</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater runat="server" ID="rptAdmin" OnItemDataBound="rptAdmin_ItemDataBound">
                        <ItemTemplate>
                            <tr>
                                <td style="width:80px;text-align:center"><%#Eval("[order]") %></td>
                                <td><%#Eval("name") %></td>
                                <td style="display:none"><asp:Literal  runat="server" ID="litCateId" Text='<%#Eval("cateid") %>'></asp:Literal></td>
                                <td><asp:Literal  runat="server" ID="litClass"></asp:Literal></td>
                                <td style="width:120px;text-align:center;<%=display%>" class="opration"><span onclick="openNewWin('/productClassify/addBrand.aspx?brandId=<%#Eval("id") %>')">修改</span> | <span onclick="goDelete('<%#Eval("name") %>',<%#Eval("id") %>)">删除</span></td>
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
        </script>
	</body>
</html>
