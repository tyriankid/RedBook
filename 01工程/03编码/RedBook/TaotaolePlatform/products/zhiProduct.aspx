<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="zhiProduct.aspx.cs" Inherits="RedBookPlatform.products.zhiProduct" %>
<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>
<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <meta charset="UTF-8">
    <title>直购商品列表</title>
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
                    <li><a href="#">直购商品管理</a></li>
                    <li class="active"><a href="#" title="">直购商品列表</a></li>
                </ul>
            </div>
            <div class="content">
                <div class="oprationbtn">
                    <a class="btn" style="<%=display%>" href="javascript:;" onclick="openNewWin('/products/AddZhiGou.aspx')">添加直购商品</a>
                    <div class="search">
                        <span style="padding-right:3px; padding-left:10px">查询关键字:</span>
                        <asp:TextBox runat="server" ID="tboxKeyword" MaxLength="10" Width="400px"></asp:TextBox>
                        <asp:Button runat="server" ID="btnSearch" CssClass="btn btn-info" Text="查询" OnClick="btnSearch_Click" />
                    </div>
                </div>
                <table class="table table-striped table-even">
                    <thead>
                        <tr>
                            <th>商品业务ID</th>
                            <th >商品名</th>
                            <th>类型</th>
                            <th>商品原价（元）</th>
                            <th>商品直购价（元）</th>
                            <th>上架时间</th>
                            <th>库存</th>
                            <th style="width:160px;text-align:center;<%=display%>">操作</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater runat="server" ID="rptZhiGou">
                            <ItemTemplate>
                                <tr>
                                    <td><%#Eval("QuanId")%></td>
                                    <td><div class="imgBox"><img src="<%=G_UPLOAD_PATH %><%#Eval("thumb") %>"/></div><%#Eval("title") %></td>
                                    <td><%# Taotaole.Common.Globals.getEnumDisplayName(Taotaole.Common.Globals.businessType.productType,(int)Eval("typeid")) %></td>
                                    <td><%#Eval("money","{0:F2}") %></td>
                                    <td><%#Eval("QuanMoney","{0:F2}") %> </td>
                                    <td><%#Eval("AddTime") %></td>
                                    <td><%#Eval("stock") %></td>
                                    <td style="width:160px;text-align:center;<%=display%>" class="opration"><span onclick="openNewWin('/products/AddZhiGou.aspx?QuanId=<%#Eval("QuanId") %>')">修改</span> 
                                        |<span onclick="deleteQuan('<%#Eval("title") %>',<%#Eval("QuanId") %>)">删除</span>
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
        function deleteQuan(name,id) {
            if (!confirm("确认将" + name + "彻底删除?")) { return false; }
                $.ajax({
                    type: "POST",
                    contentType: "application/json",
                    url: "zhiProduct.aspx/deleteQuan",
                    data: "{id:'" + id + "',name:'" + name + "'}",
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
