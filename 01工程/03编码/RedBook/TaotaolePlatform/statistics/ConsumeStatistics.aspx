<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConsumeStatistics.aspx.cs" Inherits="RedBookPlatform.statistics.ConsumeStatistics" %>
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
                    <div class="search">
                        <span>开始:</span>
                       <input type="text" name="inpTime" id="inpStarTime"  runat="server"  value="" style="width:200px"   onclick="laydate({ istime: true, format: 'YYYY-MM-DD hh:mm:ss' })"/>
                        <span>结束:</span>
                       <input type="text" name="inpTime" id="inpEndTime"  runat="server"  value="" style="width:200px"   onclick="laydate({ istime: true, format: 'YYYY-MM-DD hh:mm:ss' })"/>
                        <asp:Button runat="server" ID="btnSearch" CssClass="btn btn-info" Text="查询" OnClick="btnSearch_Click1"/>
                        
                    </div>
                </div>
                <table class="table table-striped table-even">
                    <thead>
                        <tr>
                            <th style="text-align:center">消费排名</th>
                            <th style="text-align:center">用户名</th>
                            <th style="text-align:center">消费金额</th>
                            <th style="text-align:center">注册时间</th>
                            <th style="text-align:center">联系邮箱</th>
                            <th style="text-align:center">联系电话</th>
                            <th style="text-align:center">账户余额</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater runat="server" ID="rptAdmin">
                            <ItemTemplate>
                                <tr>
                                    <td style="text-align:center">第<span style="color:red"><%#Eval("RowNumber") %></span>名</td>
                                    <td style="text-align:center"><%#Eval("username") %></td>
                                    <td style="text-align:center"><%#Convert.ToDecimal( Eval("money")).ToString("0.00") %></td>
                                    <td style="text-align:center"><%#Eval("time") %></td>
                                    <td style="text-align:center"><%#Eval("email") %></td>
                                    <td style="text-align:center"><%#Eval("mobile") %></td>
                                    <td style="text-align:center"><%# Convert.ToDecimal( Eval("moneynow")).ToString("0.00") %></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
                <div class="pager oprationbtn">
                     <span style="float:left; padding-left: 0px">当前消费总金额：
                    <span style="color: red" id="allMoney" runat="server"></span>元</span>
                    <UI:Pager runat="server" ShowTotalPages="true" ID="pager1"/>
                </div>
            </div>

        </div>
    </form>
</body>
</html>