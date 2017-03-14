<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="yuangouList.aspx.cs" Inherits="RedBookPlatform.products.yuangouList" %>
<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <meta charset="UTF-8">
    <title>一元购商品列表</title>
 <%--   <link rel="stylesheet" type="text/css" href="<%=CSS_PATH %>comm.css" />
    <script src="<%=JS_PATH %>jquery.min.js" type="text/javascript" charset="utf-8"></script>
    <script src="<%=JS_PATH %>/comm.js" type="text/javascript" charset="utf-8"></script>--%>


    
		<link rel="stylesheet" type="text/css" href="/Resources/css/comm.css"/>
        <script src="/Resources/js/jquery.min.js" type="text/javascript" charset="utf-8"></script>
        <script src="/Resources/js/comm.js" type="text/javascript" charset="utf-8"></script>
</head>
<body>
    <form id="Form1" runat="server">
                        <%--弹出层--%>
        <div id="msgDiv" style="width:420px; height:100px;top:45%;left:35%; display:none; border-radius:5px; position:absolute; background-color: hsl(0, 0%, 83%); z-index:999;">
            <div id="msgShut" style="cursor:pointer">关闭</div>
            <div id="msgDetail">
                <p style="margin-top:10px;margin-bottom:10px;"><span>&nbsp;&nbsp;不限期次商品地址：</span><span><asp:Label runat="server" ID="url1"></asp:Label></span></p>
                <p style="margin-top:10px;margin-bottom:10px;"><span>&nbsp;&nbsp;当前期次商品地址：</span><span><asp:Label runat="server" ID="url2"></asp:Label></span></p>
            </div>
        </div>
        <div class="wrapper">
            <div class="crumbs">
                <ul id="breadcrumbs" class="breadcrumb">
                    <li><a href="#">主页</a></li>
                    <li><a href="#">一元购商品管理</a></li>
                    <li class="active"><a href="#" title="">一元购商品列表</a></li>
                </ul>
            </div>
            <div class="content">
                <div class="oprationbtn">
                    <a class="btn" style="<%=display%>" href="javascript:;" onclick="openNewWin('/products/addYuangou.aspx')">添加一元购商品</a>
                    <div class="search">
                        <span style="padding-right:3px">状态:</span>
                        <asp:DropDownList ID="dropState" runat="server">
                            <asp:ListItem>全部</asp:ListItem>
                            <asp:ListItem Value="进行中"></asp:ListItem>
                            <asp:ListItem Value="已揭晓"></asp:ListItem>
                            <asp:ListItem Value="期数已满"></asp:ListItem>
                        </asp:DropDownList>
                        <span style="padding-right:3px; padding-left:10px">类型:</span>
                        <asp:DropDownList ID="dropType" runat="server"></asp:DropDownList>
                        <span style="padding-right:3px; padding-left:10px">查询关键字:</span>
                        <asp:TextBox runat="server" ID="tboxKeyword" MaxLength="10" Width="400px"></asp:TextBox>
                        <select name="sel" runat="server" id="selMultiple">
                            <option value="0">排序号升序</option>
                            <option value="1">排序号降序</option>
                            <option value="2">按添加时间降序</option>
                            <option value="3">按添加时间升序</option>
                            <option value="4">剩余人数升序</option>
                            <option value="5">剩余人数降序</option>
                            <option value="6">单人次价格升序</option>
                            <option value="7">单人次价格降序</option>
                        </select>
                        <asp:Button runat="server" ID="btnSearch" CssClass="btn btn-info" Text="查询" OnClick="btnSearch_Click" />
                    </div>
                </div>
                <table class="table table-striped table-even">
                    <thead>
                        <tr>
                            <th style="width:80px;text-align:center">排序</th>
                            <th>商品业务ID</th>
                            <th >商品名</th>
                            <th>类型</th>
                            <th>已参与/总需</th>
                            <th>单价/元</th>
                            <th>期数/最大期数</th>
                            <th>价格区域</th>
                            <th style="width:160px;text-align:center;<%=display%>">操作</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater runat="server" ID="rptYiyuan">
                            <ItemTemplate>
                                <tr>
                                    <td style="width:80px;text-align:center"><input type="text" name="orders" id="orders" value='<%#Eval("orders") %>' onchange="uporders(<%#Eval("yid") %>,this)" size="2" /></td>
                                    <td><%#Eval("originalid")%></td>
                                    <td><div class="imgBox"><img src="<%=G_UPLOAD_PATH %><%#Eval("thumb") %>"/></div><%#Eval("title") %></td>
                                    <td><%#Eval("categoryName")%></td>
                                    <td><%#Eval("canyurenshu")%> / <%#Eval("zongrenshu") %></td>
                                    <td><%#Eval("yunjiage","{0:F2}") %></td>
                                    <td><%#Eval("qishu") %> / <%#Eval("maxqishu") %></td>
                                    <td><%#Eval("pricerange") %></td>
                                    <td style="width:160px;text-align:center;<%=display%>" class="opration"><span onclick="openNewWin('/products/addYuangou.aspx?yid=<%#Eval("yid") %>')">修改</span> 
                                        | <span onclick="goDelete('<%#Eval("title") %>',<%#Eval("yid") %>)">回收</span>
                                        |<span onclick="clone('<%#Eval("originalid") %>','<%#Eval("yid") %>','<%=Taotaole.Common.Globals.WX_Domain %>')">复制链接</span></td>
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
        $(function () {
            $("#msgShut").click(function () {
                $("#msgDiv").hide();
            })
        });
        function clone(originalid,yid,url) {
            $("#msgDiv").show();
            var url1 = url + "index.item.aspx?productid=" + originalid;
            var url2 = url + "index.item.aspx?shopid=" + yid;
            $("#url1").text(url1);
            $("#url2").text(url2);
        }

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
        //更改排序号
        var yid = ""; var order = "";
        function uporders(x, y) {
            yid += x + ",";
            if (y.value == "")
            {
                y.value =0;
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
                    url: "yuangouList.aspx/UpOrder",
                    data: "{yid:'" + yid + "',order:'" + order + "'}",
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
