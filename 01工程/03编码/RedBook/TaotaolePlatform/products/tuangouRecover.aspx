<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="tuangouRecover.aspx.cs" Inherits="RedBookPlatform.products.tuangouRecover" %>
<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <meta charset="UTF-8">
    <title>团购商品回收站</title>
 <%--   <link rel="stylesheet" type="text/css" href="<%=CSS_PATH %>comm.css" />
    <script src="<%=JS_PATH %>jquery.min.js" type="text/javascript" charset="utf-8"></script>
    <script src="<%=JS_PATH %>/comm.js" type="text/javascript" charset="utf-8"></script>--%>

    
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
                    <li><a href="#">团购回收站管理</a></li>
                    <li class="active"><a href="#" title="">团购商品回收站</a></li>
                </ul>
            </div>
            <div class="content">
                <div class="oprationbtn">
                    <a class="btn"  style="<%=display%>" href="javascript:;" onclick="openNewWin('/products/tuangouList.aspx')">团购商品列表</a>
                    <div class="search">
                        <span style="padding-right:3px; padding-left:10px">类型:</span>
                        <asp:DropDownList ID="dropType" runat="server"></asp:DropDownList>
                        <span style="padding-right:3px; padding-left:10px">查询关键字:</span>
                        <asp:TextBox runat="server" ID="tboxKeyword" MaxLength="10" Width="400px"></asp:TextBox>
                        <select name="sel" runat="server" id="selMultiple">
                            <option value="0">排序号升序</option>
                            <option value="1">排序号降序</option>
                        </select>
                        <asp:Button runat="server" ID="btnSearch" CssClass="btn btn-info" Text="查询" OnClick="btnSearch_Click" />
                    </div>
                </div>
                <table class="table table-striped table-even">
                    <thead>
                        <tr>
                            <th>排序</th>
                            <th>商品原始ID</th>
                            <th >商品名</th>
                            <th>人均价</th>
                            <th>成团的人数</th>
                            <th>最大团购数</th>
                            <th>商品类型</th>
                            <th>开始时间</th>
                            <th>结束时间</th>
                            <th>是否开奖</th>
                            <th style="<%=display%>">操作</th>
                        </tr>
                    </thead>

                    <tbody>
                        <asp:Repeater runat="server" ID="rptTuangou">
                            <ItemTemplate>
                                <tr>
                                    <td><%#Eval("sort") %></td>
                                    <td><%#Eval("productid")%></td>
                                    <td ><div class="imgBox"><img src="<%=G_UPLOAD_PATH %><%#Eval("thumb") %>"/></div><%#Eval("title") %></td>
                                    <td><%#Eval("per_price","{0:F2}") %></td>
                                    <td><%#Eval("total_num") %></td>
                                    <td><%#Eval("max_sell") %></td>
                                    <td><%#Eval("categoryName") %></td>
                                    <td><%#Eval("start_time")%></td>
                                    <td><%#Eval("end_time")%></td>
                                    <td><%#(int.Parse(Eval("status").ToString())==0)?"未开奖" : "已开奖" %></td>
                                    <td style="<%=display%>" class="opration"><span onclick="goReturn('<%#Eval("title") %>',<%#Eval("tId") %>)">还原</span> | <span onclick="goRemove('<%#Eval("title") %>',<%#Eval("tId") %>)">删除</span></td>
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
        function goRemove(name, id) {
            if (!confirm("确认将" + name + "彻底删除?")) { return false; }
            $.ajax({
                type: 'post', dataType: 'json', timeout: 10000,
                async: false,
                url: location.href,
                data: {
                    action: "goRemove",
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
        //还原商品
        function goReturn(name, id) {
            if (!confirm("确认将" + name + "还原?")) { return false; }
            $.ajax({
                type: 'post', dataType: 'json', timeout: 10000,
                async: false,
                url: location.href,
                data: {
                    action: "goReturn",
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
