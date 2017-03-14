<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="shoppingrecord.aspx.cs" Inherits="RedBookPlatform.member.shoppingrecord" %>

<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>充值记录</title>

    <link rel="stylesheet" type="text/css" href="/Resources/css/comm.css" />
    <script src="/Resources/js/jquery.min.js" type="text/javascript" charset="utf-8"></script>
    <script src="/Resources/js/comm.js" type="text/javascript" charset="utf-8"></script>
    <script src="/Resources/laydate/laydate.dev.js"></script>
</head>
<body>
    <form id="Form1" runat="server">
        <div class="wrapper">
            <div class="crumbs">
                <ul id="breadcrumbs" class="breadcrumb">
                    <li><a href="#">主页</a></li>
                    <li><a href="#">会员管理</a></li>
                    <li class="active"><a href="#" title="">消费记录</a></li>
                </ul>
            </div>
            <div class="oprationbtn">
                <div class="search">
                    <span>开始时间:</span>
                    <input type="text" name="inpTime" id="inpStarTime" runat="server" value="" style="width: 18.5%" onclick="laydate({ istime: true, format: 'YYYY-MM-DD hh:mm:ss' })" />
                    <span>结束时间:</span>

                   

                    <input type="text" name="inpTime" id="inpEndTime" runat="server" value="" style="width: 18.5%;" onclick="laydate({ istime: true, format: 'YYYY-MM-DD hh:mm:ss' })" />
                  <span style="padding-right:3px">类型:</span>
                        <asp:DropDownList ID="dropType" runat="server">
                            <asp:ListItem Value="">全部</asp:ListItem>
                            <asp:ListItem Value="weixin">微信关注</asp:ListItem>
                             <asp:ListItem Value="baidu">百度推广</asp:ListItem>
                        </asp:DropDownList>
                       <span style="padding-right: 3px; padding-left: 10px">查询关键字:</span>
                    <asp:TextBox runat="server" ID="tboxKeyword" MaxLength="10" Width="150px"></asp:TextBox>
                    <asp:Button runat="server" ID="btnSearch" CssClass="btn btn-info" Text="查询" OnClick="btnSearch_Click" />
                </div>
            </div>
            <table class="table table-striped">
                <thead>
                    <tr>
                        <th>ID</th>
                        <th>用户ID</th>
                        <th>用户名</th>
                        <th>操作类型</th>
                        <th>操作方式</th>
                        <th>描述</th>
                        <th>金额</th>
                        <th>时间</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater runat="server" ID="rptRechargerecord">
                        <ItemTemplate>
                            <tr>
                                <td><%#Eval("id") %></td>
                                <td><%#Eval("uid") %></td>
                                <td><%#Eval("username") %></td>
                                <td><%#Eval("type").ToString()=="1"?"充值":"消费" %></td>
                                <td><%#Eval("pay") %></td>
                                <td><%#Eval("content") %></td>
                                <td><%#Eval("money","{0:F2}") %></td>
                                <td><%#Eval("time","{0:yyyy-MM-dd HH:mm:ss}") %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
            <div class="pager oprationbtn">
                <span style="float: left">当前消费总金额：<span style="color: red"><asp:Label ID="labSum" runat="server"></asp:Label></span>元</span>
                <UI:Pager runat="server" ShowTotalPages="true" ID="pager1" />
            </div>
        </div>
    </form>
</body>
</html>
