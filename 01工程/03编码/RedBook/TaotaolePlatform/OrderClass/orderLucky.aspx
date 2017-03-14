<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="orderLucky.aspx.cs" Inherits="RedBookPlatform.OrderClass.orderLucky" %>
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
                    <li><a href="index.html">主页</a></li>
                    <li><a href="index.html">订单管理</a></li>
                    <li class="active"><a href="#" title="">中奖订单列表</a></li>
                </ul>
            </div>
            
		    	<div class="oprationbtn">
                       
                    <div class="search">
                         <span>开始:</span>
                       <input type="text" name="inpTime" id="inpStarTime"  runat="server"  value="" style="width:15%"   onclick="laydate({ istime: true, format: 'YYYY-MM-DD hh:mm:ss' })"/>
                        <span>结束:</span>
                       <input type="text" name="inpTime" id="inpEndTime"  runat="server"  value="" style="width:15%;"   onclick="laydate({ istime: true, format: 'YYYY-MM-DD hh:mm:ss' })"/>
                        <span style="padding-right:3px">类型:</span>
                        <asp:DropDownList ID="dropType" runat="server">
                            <asp:ListItem Value="">全部</asp:ListItem>
                            <asp:ListItem Value="gift">积分商城</asp:ListItem>
                            <asp:ListItem Value="yuan">一元购</asp:ListItem>
                            <%-- <asp:ListItem Value="baidu">百度推广</asp:ListItem>--%>
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
                        <asp:TextBox runat="server" ID="tboxKeyword" MaxLength="20" Width="300px"></asp:TextBox>
                        <asp:Button runat="server" ID="btnSearch" CssClass="btn btn-info" Text="查询"  OnClick="btnSearch_Click"/>
                        <asp:Button runat="server" ID="Export" CssClass="btn btn-info" Text="导出" OnClick="Export_Click"   />
                    </div>
                   </div>  
                <table class="table table-striped table-even">
                    <thead id="disPlay" runat="server">
                        <tr >
                            <th  style="text-align:center">用户名</th>
                            <th>商品信息</th>
                            <th>订单ID</th>
                            <th>商品来源</th>
                            <th>订单金额</th>
                            <th>是否使用红包</th>
                            <th>红包抵用金额</th>
                            <th>商品类型</th>
                            <th>使用类型</th>
                            <th>订单状态</th>
                            <th>查看</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater runat="server" ID="rptAdmin">
                            <ItemTemplate>
                                <tr>
                                    <td><%#Eval("username") %></td>
                                    <td><%#Eval("title") %></td>
                                    <td><%#Eval("orderId") %></td>
                                    <td><%#Taotaole.Common.Globals.getOrderType(Eval("ordertype").ToString()) %></td>
                                    <td><%# Convert.ToDecimal( Eval("money")).ToString("0.00") %></td>
                                    <td><%#Eval("shiyong").ToString()=="ok"?"已使用红包":"未使用红包" %></td>
                                    <td><%# Eval("shiyong").ToString()=="ok"? Eval("discount")+"元":"未使用红包" %></td>
                                    <td><%# Taotaole.Common.Globals.getEnumDisplayName(Taotaole.Common.Globals.businessType.productType,Convert.ToInt32( Eval("typeid"))) %></td>
                                    <td><%# Convert.ToInt32( Eval("typeid"))==0||Convert.ToInt32( Eval("typeid"))==2?"--------------":( Eval("usetype").ToString()!=""?Taotaole.Common.Globals.getEnumDisplayName(Taotaole.Common.Globals.businessType.yollytype,Convert.ToInt32( Eval("usetype"))):"未使用" )%></td>
                                    <td><%#Eval("status") %></td>
                                    <td><a href="javascript:void(0);" onclick="selectinfo('<%#Eval("orderId") %>')">查看详情</a></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
                <div class="pager oprationbtn">
                    <span style="float:left; padding-left: 0px">当前查询数据汇总:订单总金额<span style="color: red" id="allMoney" runat="server"></span>元，购买总次数<span style="color: red" id="allQuantity" runat="server"></span>人</span>
                    <UI:Pager runat="server" ShowTotalPages="true" ID="pager1" />
                </div>
            </div>

         <script type="text/javascript">
             function selectinfo(orderid) {
                 window.open("/OrderClass/orderdetail.aspx?orderid=" + orderid + "", "", "height=303px, width=700px,top=250px,left=400px ,toolbar =no, menubar=no, scrollbars=no, resizable=no, location=no, status=no")
             }

    </script>
    </form>
</body>
</html>
