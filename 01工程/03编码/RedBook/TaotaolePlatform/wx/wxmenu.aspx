<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="wxmenu.aspx.cs" Inherits="RedBookPlatform.wx.wxmenu" %>
<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>
<!DOCTYPE html>

<html>
	<head runat="server">
		<meta charset="UTF-8">
		<title>微信菜单</title>

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
	                <li class="active"><a href="#" title="">微信菜单</a></li>
	            </ul>
			</div>
		    <div class="content">
		    	<div class="oprationbtn">
		    		<a class="btn" href="javascript:;" onclick="openNewWin('/wx/addWxMenu.aspx')">添加主菜单</a>
                    <asp:Button runat="server" ID="btnSave" Text="保存到微信"  Class="btn" OnClick="btnSave_Click" style="margin-top:-3px;" />
		    	</div>
		    	<div class="oprationbtn">
		    		<span style="margin-right:13px;color:red">目前自定义菜单最多包括3个一级菜单，每个一级菜单最多包含5个二级菜单。一级菜单最多4个汉字，二级菜单最多7个汉字，多出来的部分将会以“...”代替。请注意，创建自定义菜单后，由于微信客户端缓存，需要24小时微信客户端才会展现出来。建议测试时可以尝试取消关注公众账号后再次关注，则可以看到创建后的效果。</span>
		    	</div>
		    	<table class="table table-striped table-even">
                    <thead>
                        <tr>
                            <th style="width:50px">排序</th>
                            <th style="width:30%">菜单名称</th>
                            <th style="width:40%">链接地址</th>
                            <th style="width:30%">操作</th>
                        </tr>
                    </thead>
                    
                    <tbody>
                        <asp:Repeater runat="server" ID="rptWxMenu">
                        <ItemTemplate>
                            <tr>
                                <td><input type="tel" style="width:50px" value="<%#Eval("sort") %>" onchange="uporder(<%#Eval("menuid") %>,this)" ></td>
                                <td><%#Eval("parentid").ToString()!="0"?"&nbsp;&nbsp;|-----&nbsp;&nbsp;&nbsp;&nbsp;":"" %><%#Eval("name") %></td>
                                <td><%#Eval("url") %></td>
                                <td class="opration">
                                    <span onclick="openNewWin('/wx/addWxMenu.aspx?menuid=<%#Eval("menuid") %>')">修改</span> | 
                                    <span onclick="goDelete(<%#Eval("menuid") %>)">删除</span>
                                    <%#Eval("parentid").ToString()!="0"?"":" | <span onclick=\"openNewWin('/wx/addWxMenu.aspx?parentid="+Eval("menuid")+"')\">添加子菜单</span>" %>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    </tbody>
                </table>
                <div class="oprationbtn">
                    <span style="margin-right:13px; height:60px">
                        <input type="button" class="btn" value="保存排序" onclick="UpOrder()"  />
                    </span>
                </div>
		    </div>
            
		</div>
        </form>

        <script>
            //删除列
            function goDelete(id) {
                if (!confirm("确认删除?")) { return false; }
                $.ajax({
                    type: "POST",
                    contentType: "application/json",
                    url: "wxmenu.aspx/goDelete",
                    data: "{menuid:'" + id + "'}",
                    dataType: 'json',
                    success: function (result) {
                        alert(result.d);
                        location.reload();
                    }
                });
            }
            //更改排序号
            var menuid = ""; var sort = "";
            function uporder(x, y) {
                menuid += x + ",";
                if (y.value == "") {
                    y.value = 0;
                }
                sort += y.value + ",";
            }
            function UpOrder() {
                if (menuid == "" || sort == "") {
                    alert("未更改任何排序")
                }
                else {
                    if (!confirm("确认更改当前排序?")) { return false; }
                    $.ajax({
                        type: "POST",
                        contentType: "application/json",
                        url: "wxmenu.aspx/UpOrder",
                        data: "{menuid:'" + menuid + "',sort:'" + sort + "'}",
                        dataType: 'json',
                        success: function (result) {
                            alert(result.d);   
                        }
                    });
                }
            }
        </script>
	</body>
</html>
