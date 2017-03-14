<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SlidesMarket.aspx.cs" Inherits="RedBookPlatform.Slides.SlidesMarket" %>
<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>
<!DOCTYPE html>
<html>
<head>
    <meta charset="UTF-8">
    <title>团购幻灯片</title>

    <link rel="stylesheet" type="text/css" href="/Resources/css/comm.css" />
    <script src="/Resources/js/jquery.min.js" type="text/javascript" charset="utf-8"></script>
    <script src="/Resources/js/comm.js" type="text/javascript" charset="utf-8"></script>
</head>
<body>
    <form id="Form1" runat="server">
        <div class="wrapper">
            <div class="crumbs">
                <ul id="breadcrumbs" class="breadcrumb">
                    <li><a href="#">主页</a></li>
                    <li><a href="#">幻灯片管理</a></li>
                    <li class="active"><a href="#" title="">直购幻灯片管理</a></li>
                </ul>
            </div>
            <div class="content">
                <div class="oprationbtn">
                    <a class="btn" href="javascript:;" onclick="openNewWin('/Slides/AddSlidesMarket.aspx')">添加直购幻灯片</a>

                    <%--<div class="search" style="">
                          <span style="padding-right:3px; padding-left:10px">文章类型:</span>
                        <asp:DropDownList ID="ddlCategory" runat="server"></asp:DropDownList>


		    		   <span style="padding-right:3px; padding-left:10px">查询关键字:</span>
                        <asp:TextBox runat="server" ID="tboxKeyword" Width="400px" MaxLength="10"></asp:TextBox>
		    			<select name="sel"  runat="server" id="selMultiple">
                            <option value="0">按添加时间倒序</option>
		    				<option value="1">按添加时间升序</option>
		    				
		    			</select>
                        <asp:Button runat="server" ID="btnSearch" CssClass="btn btn-info" Text="查询" OnClick="btnSearch_Click" />
		    		</div>--%>
                    <div class="search">
                  <span style="padding-right:3px; padding-left:10px">选择排列顺序:</span>
		    				
		    			<asp:DropDownList ID="selMultiple" runat="server" AutoPostBack="true" OnSelectedIndexChanged="selMultiple_SelectedIndexChanged">
                             <asp:ListItem Selected="True" Value="0">
                                请选择
                            </asp:ListItem>
                            <asp:ListItem  Value="1">
                                按添加时间倒序
                            </asp:ListItem>
                             <asp:ListItem  Value="2">
                                按添加时间升序
                            </asp:ListItem>
		    			         </asp:DropDownList>
                        </div>
                </div>
                <table class="table table-striped table-even">
                    <thead>
                        <tr>
                            <th style="width:80px;text-align:center">排序</th>
                            <th>幻灯片ID</th>
                            <th>幻灯片</th>

                            <th>幻灯片链接地址</th>
                              <th>上次修改时间</th>
                            <th>幻灯片状态</th>
                            <th style="width:120px;text-align:center">操作</th>
                        </tr>
                    </thead>

                    <tbody>
                        <asp:Repeater runat="server" ID="rptAdmin">
                            <ItemTemplate>
                                <tr>
                                    <td style="width:80px;text-align:center">
                                        <input type="text" name="ordersn" id="ordersn" value='<%#Eval("slideorder") %>' onchange="uporders(<%#Eval("slideid") %>,this)" size="2" /></td>
                                    <td><%#Eval("slideid") %></td>
                                    <td>
                                        <div class="imgBox">
                                            <img src="<%=G_UPLOAD_PATH %><%#Eval("slideimg") %>" /></div>
                                    </td>
                                    <td> <%#Eval("slidelink") %> </td>
                                      <td><%#Eval("slidelastupdatetime").ToString().Replace("/","-") %></td>
                                   
                                        <td> <%# (Eval("slidestate", "{0}").ToString() == "0") ? "启用" : "禁用"%></td>
                                    <td style="width:120px;text-align:center" class="opration"><span onclick="openNewWin('/Slides/AddSlidesMarket.aspx?id=<%#Eval("slideid") %>')">修改</span> | <span onclick="goDelete('<%#Eval("slideid") %>')">删除</span></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
                <div class="pager oprationbtn">
                    <input type="button" class="pager oprationbtn" value="排 序" onclick="UpOrder()" style="float: left;" />
                 
                    <UI:Pager ID="pager1" ShowTotalPages="true" runat="server" />
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
                    url: "SlidesMarket.aspx/UpOrder",
                    data: "{yid:'" + yid + "',order:'" + order + "'}",
                    dataType: 'json',
                    success: function (result) {
                        alert(result.d);
                    }
                });
            }
        }

        //删除列
        function goDelete(id) {
            if (!confirm("确认删除该幻灯片吗?")) { return false; }
            $.ajax({
                type: "POST",
                contentType: "application/json",
                url: "SlidesMarket.aspx/goDelete",
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