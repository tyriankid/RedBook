<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BookProductList.aspx.cs" Inherits="RedBookPlatform.RedBook.BookProductList" %>
<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>
<!DOCTYPE html>

<html>
	<head runat="server">
		<meta charset="UTF-8">
		<title>绑定文章相关商品</title>

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
	                <li><a href="#">小红书管理</a></li>
                    <li><a href="#">导购文章列表</a></li>
	                <li class="active"><a href="#" title="">绑定文章相关商品</a></li>
	            </ul>
			</div>
		    <div class="content">
		    	<div class="oprationbtn">
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
                        <input type="button" name="" value="提交"  class="btn btn-danger" style="height: 34px;line-height: 4px;" id="new-tijiao">
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
                            <th  style="width:120px;text-align:center;">操作</th>
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
                                <td  style="width:120px;text-align:center;" class="opration"><input type="checkbox" name="productId" productId="<%#Eval("productId") %>" /></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    </tbody>
                </table>
                <div class="pager oprationbtn" >
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
            };



            $(function () {
                var str = "";
                //监听checkbox的change事件,将选中的id存放在数组内
                var changeProductIds = function () {
                    var ids = [];
                    //读取 
                    str = sessionStorage.getItem("redbook_productids");
                    if (str) {
                        //重新转换为对象 
                        obj = JSON.parse(str);
                        ids = obj;
                    }
                    return function (e) {
                        if (e.currentTarget.checked) {
                            ids.push(e.currentTarget.getAttribute("productId"));
                        } else {
                            ids.splice(ids.indexOf(e.currentTarget.getAttribute("productId")), 1);
                        }
                        var str = JSON.stringify(ids);
                        //存入 
                        sessionStorage.setItem("redbook_productids",str);
                    }
                }
                
                //页面载入时将选中的checkbox选中.
                str = sessionStorage.getItem("redbook_productids");
                if (str) {
                    //重新转换为对象 
                    obj = JSON.parse(str);
                    for (var i = 0; i < obj.length; i++) {
                        $("[name='productId'][productid='" + obj[i] + "']").attr("checked", "checked");
                    }
                }
                
                $("body").on("change", "[name='productId']", changeProductIds());
                //document.querySelectorAll("[name='productId']").addEventListener('change', changeProductIds());

                $("body").on("click", "#new-tijiao", function () {
                    //处理数组提交过去
                    str = sessionStorage.getItem("redbook_productids");
                    if (str) {
                        //重新转换为对象 
                        obj = JSON.parse(str);
                        var productIds = "";
                        for (var o = 0; o < obj.length; o++) {
                            productIds = productIds + obj[o] + ",";
                        }
                        productIds = productIds.substr(0, productIds.length - 1);
                        $("body", parent.document).find("#hidProductIds").val(productIds);
                        //清除这个sessionStorage
                        sessionStorage.removeItem("redbook_productids");
                    } else {
                        alert("请选中商品后再提交!"); return false;
                    }
                    //隐藏iframe
                    $("body", parent.document).find(".iframeBox").hide(300);
                })
            })
        </script>
	</body>
</html>
