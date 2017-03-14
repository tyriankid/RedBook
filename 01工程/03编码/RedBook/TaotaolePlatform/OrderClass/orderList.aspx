<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="orderList.aspx.cs" Inherits="RedBookPlatform.OrderClass.orderList" %>
<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>
<!DOCTYPE html>
<html>
<head>
    <meta charset="UTF-8">
    <title>订单列表</title>
    <link rel="stylesheet" type="text/css" href="<%=CSS_PATH %>comm.css" />
    <script src="<%=JS_PATH %>jquery.min.js" type="text/javascript" charset="utf-8"></script>
    <script src="<%=JS_PATH %>/comm.js" type="text/javascript" charset="utf-8"></script>
     <script src="/Resources/laydate/laydate.dev.js"></script>
</head>
<body>
    <form id="Form1" runat="server">
        <div class="wrapper">
            <div class="crumbs">
                <ul id="breadcrumbs" class="breadcrumb">
                    <li><a href="#">主页</a></li>
                    <li><a href="#">订单管理</a></li>
                    <li class="active"><a href="#" title="">订单列表</a></li>
                </ul>
            </div>
            <div class="content">
                <div class="oprationbtn">
                    <a class="btn" href="javascript:;" onclick="openNewWin('/OrderClass/orderNotOut.aspx')">未发货订单</a>
                    <div class="search">
                        <span>开始:</span>
                       <input type="text" name="inpTime" id="inpStarTime"  runat="server"  value="" style="width:15%"   onclick="laydate({ istime: true, format: 'YYYY-MM-DD hh:mm:ss' })"/>
                        <span>结束:</span>
                       <input type="text" name="inpTime" id="inpEndTime"  runat="server"  value="" style="width:15%;"   onclick="laydate({ istime: true, format: 'YYYY-MM-DD hh:mm:ss' })"/>
                        <span style="padding-right: ">类型:</span>
                        <asp:DropDownList ID="dropType" runat="server">
                            <asp:ListItem Value="">全部</asp:ListItem>
                            <asp:ListItem Value="tuan">团购</asp:ListItem>
                            <asp:ListItem Value="yuan">一元购</asp:ListItem>
                             <asp:ListItem Value="baidu">百度推广</asp:ListItem>
                        </asp:DropDownList>
                         <span style="padding-right:3px;padding-left:10px">中奖:</span>
                        <asp:DropDownList ID="iswon" runat="server">
                            <asp:ListItem Value="">全部</asp:ListItem>
                            <asp:ListItem Value="1">中奖</asp:ListItem>
                            <asp:ListItem Value="0">未中奖</asp:ListItem>
                        </asp:DropDownList>
                        <span style="padding-right:3px; padding-left:10px">状态:</span>
                        <asp:DropDownList ID="dropState" runat="server">
                            <asp:ListItem Value="">全部</asp:ListItem>
                            <asp:ListItem Value="待确认">待确认</asp:ListItem>
                            <asp:ListItem Value="待发货">待发货</asp:ListItem>
                            <asp:ListItem Value="已发货">已发货</asp:ListItem>
                            <asp:ListItem Value="已收货">已收货</asp:ListItem>
                        </asp:DropDownList>
                        <span style="padding-right:3px; padding-left:10px">查询关键字:</span>
                        <asp:TextBox runat="server" ID="tboxKeyword" MaxLength="20" Width="280px"></asp:TextBox>
                        <asp:Button runat="server" ID="btnSearch" CssClass="btn btn-info" Text="查询"  OnClick="btnSearch_Click"/>
                        <asp:Button runat="server" ID="Export" CssClass="btn btn-info" Text="导出"  OnClick="Export_Click" />
                        
                    </div>
                </div>
                <table class="table table-striped table-even">
                    <thead>
                        <tr>
                            <th  style="text-align:center">用户名</th>
                            <th>商品信息</th>
                            <th>订单ID</th>
                            <th>订单金额</th>
                            <th>订单备注</th>
                            <th>购买日期</th>
                            <th>订单状态</th>
                            <th>订单来源</th>
                            <th style="text-align:center">是否中奖</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater runat="server" ID="rptAdmin">
                            <ItemTemplate>
                                <tr>
                                    <td><%#Eval("username") %></td>
                                    <td><%#Eval("title") %></td>
                                    <td><%#Eval("orderid") %></td>
                                    <td><%# Convert.ToDecimal( Eval("money")).ToString("0.00") %></td>
                                    <td>在第<%#Eval("qishu") %>期内，购买<%#Eval("quantity") %>次，<%#Eval("shiyong").ToString()=="ok"?"<apan style='color:red'>使用红包："+Eval("discount")+"<apan>":"未使用红包" %>元</td>
                                    <td><%#Eval("time") %></td>
                                    <td><%#Eval("status") %></td>
                                    <td><%#Eval("ordertype").ToString()=="yuan"?"一元购":"团购" %></td>
                                    <td><%#Eval("iswon").ToString()=="1"?"中奖":"未中奖" %></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
                <div class="pager oprationbtn">
                     <span style="float:left; padding-left: 0px">当前查询数据汇总:订单总金额：
                         <span style="color: red" id="allMoney" runat="server"></span>元，
                         <span  padding-left: 0px">使用红包总金额：<span style="color: red" id="hbmoney" runat="server"></span>元，
                             实际收入总金额：<span style="color: red" id="shmoney" runat="server"></span>元，
                             购买总次数：<span style="color: red" id="allQuantity" runat="server"></span>人</span></span>
                    <UI:Pager runat="server" ShowTotalPages="true" ID="pager1"/>
                </div>
            </div>

        </div>
    </form>
</body>
</html>
