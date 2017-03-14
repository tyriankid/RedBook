<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="orderNotOut.aspx.cs" Inherits="RedBookPlatform.OrderClass.orderNotOut" %>
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
                    <li class="active"><a href="#" title="">未发货订单列表</a></li>
                </ul>
            </div>
            <div class="content">
                <div > 
                    <div>
                        <a class="btn" href="javascript:;" onclick="allSend()">批量发货</a>
                        <div class="search">
                        <span style="padding-right:3px">类型:</span>
                        <asp:DropDownList ID="dropType" runat="server">
                           <%-- <asp:ListItem Value="tuan">团购</asp:ListItem>--%>
                            <asp:ListItem Value="yuan">一元购</asp:ListItem>
                            <asp:ListItem Value="quan">直购</asp:ListItem>
                            <asp:ListItem Value="gift">积分商城</asp:ListItem>
                      <%--      <asp:ListItem Value="baidu">百度推广</asp:ListItem>--%>
                        </asp:DropDownList>
                            <span style="padding-right:3px">商品类型:</span>
                            <asp:DropDownList ID="dropState" runat="server">
                            <asp:ListItem Value="0,2">实体及游戏</asp:ListItem>
                            <asp:ListItem Value="1,3">话费及Q币</asp:ListItem>
                        </asp:DropDownList>
                        <span style="padding-right:3px; padding-left:10px">查询关键字:</span>
                        <asp:TextBox runat="server" ID="tboxKeyword" MaxLength="20" Width="300px"></asp:TextBox>
                        <asp:Button runat="server" ID="btnSearch" CssClass="btn btn-info" Text="查询"  OnClick="btnSearch_Click"/>
                    </div>
                    </div>

                </div>
                <table class="table table-striped table-even">
                    <thead id="disPlay" runat="server">
                        <tr >
                            <th  style="text-align:center">订单编号</th>
                            <th  style="text-align:center">商品信息</th>
                            <th style="text-align:center">收货人姓名</th>
                            <th style="text-align:center">电话</th>
                            <th style="text-align:center">购买次数</th>
                            <th style="text-align:center">金额（元）</th>
                            <th style="text-align:center">购买日期</th>
                            <th style="text-align:center">订单状态</th>
                            <th style="text-align:center">订单来源</th>
                           <th style="text-align:center">管理</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater runat="server" ID="rptAdmin">
                            <ItemTemplate>
                                <tr>
                                    <td style="width:15%">
                                        <input name="CheckBoxGroup" type="checkbox" value='<%#Eval("OrderId") %>' /><%#Eval("OrderId") %>
                                    </td>
                                    <td>
                                        <div class="imgBox"><img src="<%=G_UPLOAD_PATH %><%#Eval("thumb") %>"/></div><%#Eval("title") %>
                                    </td>
                                    <td style="width:8%">
                                        <%#Eval("username") %>
                                    </td>
                                    <td style="width:7%">
                                        <%#Eval("phone") %>
                                    </td>
                                    <td  style="width:8%">
                                       <%#Eval("quantity") %>
                                    </td>
                                    <td  style="width:8%">
                                        <%#Eval("money","{0:F2}")%>
                                    </td>
                                    <td style="width:10%;white-space:nowrap; overflow:hidden; text-overflow:ellipsis;" >
                                      <%#Eval("time","{0:yyyy-MM-dd HH:mm:ss}") %>
                                     </td>
                                      <td style="width:7%">
                                        <%#Eval("status") %>
                                    </td>
                                    <td style="width:7%">
                                        <%#Eval("ordertype") %>
                                    </td>
                                     <td style="width:8%" id="tdFahuo">
                                        <a id="outGoods" class="btn btn-info" onclick="openOrderInfo('<%#Eval("orderId")%>')" style="display:<%#(Eval("status").ToString()=="待发货")?"":"none" %>">发  货</a>
                                        <a id="isOutGoods" class="btn" onclick="window.open('/OrderClass/orderDelivery.aspx?orderId=<%#Eval("orderId")%>,<%#Eval("ordertype")%>','', 'width=200,height=200')" style="display:<%#(Eval("express_company").ToString()!="")?"":"none" %>">已发货</a>
                                        <a id="isGetGoods" class="btn" onclick="outGoods()" style="display:<%#(Eval("express_company").ToString()=="已收货")?"":"none" %>">已收货</a>
                                    </td>
                           
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
                <div class="pager oprationbtn">
                    <input type="button" id="All" value="全选"  style="float:left;" class="pager oprationbtn" onclick="checkAll()"/>
                        <input type="button" id="Button1" value="反选"  style="float:left;" class="pager oprationbtn" onclick="uncheckAll()"/>
                    <UI:Pager runat="server" ShowTotalPages="true" ID="pager1" />
                </div>
            </div>

        </div>
    </form>
     <script  type="text/javascript">
         function checkAll() {//全选
             var code_Values = document.getElementsByTagName("input");
             for (i = 0; i < code_Values.length; i++) {
                 if (code_Values[i].type == "checkbox") {
                     code_Values[i].checked = true;
                 }
             }
         }
         function uncheckAll() {//反选
             var code_Values = document.getElementsByTagName("input");
             for (i = 0; i < code_Values.length; i++) {
                 if (code_Values[i].type == "checkbox") {
                     code_Values[i].checked = false;
                 }
             }
         }
         function allSend() {
             var OrderIds = "";
             $("input:checked[name='CheckBoxGroup']").each(function () {
                 OrderIds += "'"+$(this).val()+"'" + ",";
             });
             if (OrderIds == "") {
                 alert("请选择要发货的订单！")
             }
             else {
                 window.open("/OrderClass/orderSendGoods.aspx?orderIds=" + OrderIds + "", "", "height=606px, width=1215px,top=150px,left=150px ,toolbar =no, menubar=no, scrollbars=no, resizable=no, location=no, status=no")
             }
         }
         function openOrderInfo(orderId)
             {
             window.open("/OrderClass/orderSendGoods.aspx?orderId='" + orderId + "'", "", "height=606px, width=1215px,top=150px,left=150px, toolbar =no, menubar=no, scrollbars=no, resizable=no, location=no, status=no")
             }

    </script>
</body>
</html>
