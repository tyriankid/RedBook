<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="tuangouList.aspx.cs" Inherits="RedBookPlatform.products.tuangouList" %>
<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <meta charset="UTF-8">
    <title>团购商品列表</title>
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
                    <li><a href="#">团购商品管理</a></li>
                    <li class="active"><a href="#" title="">团购商品列表</a></li>
                </ul>
            </div>
            <div class="content">
                <div class="oprationbtn">
                    <a class="btn" style="<%=display%>" href="javascript:;" onclick="openNewWin('/products/addTuangou.aspx')">添加团购商品</a>
                    <div class="search">
                        <span style="padding-right:3px">状态:</span>
                        <asp:DropDownList ID="dropState" runat="server">
                            <asp:ListItem>全部</asp:ListItem>
                            <asp:ListItem Value="已开奖"></asp:ListItem>
                            <asp:ListItem Value="已过期"></asp:ListItem>
                        </asp:DropDownList>
                        <span style="padding-right:3px; padding-left:10px">类型:</span>
                        <asp:DropDownList ID="dropType" runat="server"></asp:DropDownList>
                        <span style="padding-right:3px; padding-left:10px">查询关键字:</span>
                        <asp:TextBox runat="server" ID="tboxKeyword" MaxLength="10" Width="400px"></asp:TextBox>
                        <select name="sel" runat="server" id="selMultiple">
                            <option value="1">按添加时间降序</option>
                            <option value="0">按添加时间升序</option>
                            <option value="2">排序号升序</option>
                            <option value="3">排序号降序</option>
                            <option value="4">开始时间升序</option>
                            <option value="5">开始时间降序</option>
                            <option value="6">人均价升序</option>
                            <option value="7">人均价降序</option>
                        </select>
                        <asp:Button runat="server" ID="btnSearch" CssClass="btn btn-info" Text="查询" OnClick="btnSearch_Click" />
                    </div>
                </div>
                <table class="table table-striped table-even">
                    <thead>
                        <tr>
                            <th width="70">排 序</th>
                            <th width="95">商品原始ID</th>
                            <th >商品名</th>
                            <th>人均价</th>
                            <th width="90">成团的人数</th>
                            <th width="90">最大团购数</th>
                            <th width="80">商品类型</th>
                            <th width="140">开始时间</th>
                            <th width="140">结束时间</th>
                            <th width="140">截止时间</th>
                            <th width="100">是否开奖</th>
                            <th width="100" style="<%=display%>">操 作</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater runat="server" ID="rptTuangou">
                            <ItemTemplate>
                                <tr>
                                    <td><input type="text" name="orders" id="orders" value='<%#Eval("sort") %>' onchange="uporders(<%#Eval("tId") %>,this)" size="2" /></td>
                                    <td><%#Eval("productid")%></td>
                                    <td><div class="imgBox"><img src="<%=G_UPLOAD_PATH %><%#Eval("thumb") %>"/></div><%#Eval("title") %></td>
                                    <td><%#Eval("per_price","{0:F2}") %></td>
                                    <td><%#Eval("total_num") %></td>
                                    <td><%#Eval("max_sell") %></td>
                                    <td><%#Eval("categoryName") %></td>
                                    <td><%#Eval("start_time")%></td>
                                    <td><%#Eval("end_time")%></td>
                                    <td id="stopTime"><%#(Eval("deadline").ToString()=="")?"即时开奖":Eval("deadline") %></td>
                                    <td><%#(int.Parse(Eval("status").ToString())==0)?"活动正常" : "活动已过期" %></td>
                                    <td  style="<%=display%>" class="opration"><span onclick="openNewWin('/products/addTuangou.aspx?Tid=<%#Eval("tId") %>')">修改</span> | <span onclick="goDelete('<%#Eval("title") %>',<%#Eval("tId") %>)">回收</span></td>
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
        //删除列
        function goDelete(name, id) {
            var myDate = new Date();
            if (!confirm("确认将" + name + "放入回收站?")) { return false; }
            $.ajax({
                type: 'post', dataType: 'json', timeout: 10000,
                async: false,
                url: location.href,
                data: {
                    action: "goDelete",
                    tid: id,
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
        //更改排序号
        var tid = ""; var sort = "";
        function uporders(x, y) {
            tid += x + ",";
            if (y.value == "") {
                y.value = 0;
            }
            sort += y.value + ",";
        }
        function UpOrder() {

            if (tid == "" || sort == "") {
                alert("未更改任何排序")
            }
            else {
                if (!confirm("确认更改当前排序?")) { return false; }
                $.ajax({
                    type: "POST",
                    contentType: "application/json",
                    url: "tuangouList.aspx/UpOrder",
                    data: "{tid:'" + tid + "',sort:'" + sort + "'}",
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
