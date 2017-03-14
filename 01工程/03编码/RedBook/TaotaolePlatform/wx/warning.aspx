<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="warning.aspx.cs" Inherits="RedBookPlatform.wx.warning" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, user-scalable=no, maximum-scale=1.0" />
    <title>今日预警信息</title>
    <style>
        tr 
        { 
            height:30px;
        }
        .warningTitle {
        font-weight:bold; padding-left:10px; padding-top:5px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table style="width:100%; border:solid 1px red">
        <tr>
            <td style="font-weight:bold; color:green; padding-left:3px"><a href="warning.aspx" style="color:green">刷新(今日预警)</a></td>
        </tr>
        <tr>
            <td class="warningTitle">可用码过小预警<asp:Button runat="server" ID="btnUpdate" Text="更新" OnClick="btnUpdate_Click" /></td>
        </tr>
        <tr>
            <td>
                <asp:Repeater runat="server" ID="RepeaterInfo1">
                    <HeaderTemplate>
                        <table style="width:100%; border:solid 1px blue">
                            <tr>
                                <td>商品原始ID</td>
                                <td>商品名称</td>
                                <td>码表名</td>
                                <td>当前剩余</td>
                                <td>当前购买</td>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td><%#Eval("yid") %></td>
                            <td><%#Eval("shopname") %></td>
                            <td><%#Eval("codetable") %></td>
                            <td><%#Eval("shengyurenshu") %></td>
                            <td><%#Eval("buyquantity") %></td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate></table></FooterTemplate>
                </asp:Repeater>
            </td>
        </tr>
        <tr>
            <td class="warningTitle">攻击充值小于1元预警</td>
        </tr>
        <tr>
            <td>
                <asp:Repeater runat="server" ID="RepeaterInfo2">
                    <HeaderTemplate>
                        <table style="width:100%; border:solid 1px blue">
                            <tr>
                                <td>用户ID</td>
                                <td>操作时间</td>
                                <td>金额</td>
                                <td>备注</td>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td><%#Eval("uid") %></td>
                            <td><%#Eval("time") %></td>
                            <td><%#Eval("money") %></td>
                            <td><%#Eval("memo") %></td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate></table></FooterTemplate>
                </asp:Repeater>
            </td>
        </tr>
        <tr>
            <td class="warningTitle">单次购买大于5的订单</td>
        </tr>
        <tr>
            <td>
                <asp:Repeater runat="server" ID="RepeaterInfo3">
                    <HeaderTemplate>
                        <table style="width:100%; border:solid 1px blue">
                            <tr>
                                <td>订单ID</td>
                                <td>操作时间</td>
                                <td>用户ID</td>
                                <td>购买次数</td>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td><%#Eval("orderid") %></td>
                            <td><%#Eval("time") %></td>
                            <td><%#Eval("uid") %></td>
                            <td><%#Eval("quantity") %></td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate></table></FooterTemplate>
                </asp:Repeater>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
