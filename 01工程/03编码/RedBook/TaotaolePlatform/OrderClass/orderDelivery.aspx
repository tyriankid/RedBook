<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="orderDelivery.aspx.cs" Inherits="RedBookPlatform.OrderClass.orderDelivery" %>
<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <meta charset="UTF-8" />
    <title>订单发货</title>
    <link rel="stylesheet" type="text/css" href="<%=CSS_PATH %>comm.css" />
    <link href="<%=CSS_PATH %>flashupload.css" rel="stylesheet" type="text/css" />
    <script src="<%=JS_PATH %>jquery.min.js"></script>
    <script src="<%=JS_PATH %>flashupload.js"></script>
</head>
<body>
    <form id="Form1" runat="server">
        <input  type="hidden" runat="server" id="hidOrderId"/>
            <div class="content">
            <table class="table" id="Table1" runat="server"> 
                <tbody>
                    <tr>
                        <td style="background-color:#eaddeb; font-weight: 100; ">商品信息</td>
                    </tr>
                </tbody>
            </table>
           </div>
            <%--元购--%>
            <div class="content"  runat="server" id ="divYuangou">
            <table class="table table-striped tabel-textalign" id="tabProductInfo" runat="server" style='TABLE-LAYOUT: fixed' > 
                <tbody>

                    <tr>
                        <td style="width:120px">*商品名称:</td>
                        <td>
                            <asp:Literal ID="litProductName" runat="server"></asp:Literal>
                        </td>
                        <td style="width:120px">*订单ID:</td>
                        <td>
                            <asp:Literal ID="litOrderID" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td>*商品期数:</td>
                        <td>
                            <asp:Literal ID="litQishu" runat="server"></asp:Literal>
                        </td>
                        <td style="width:120px">*总需人数:</td>
                        <td>
                            <asp:Literal ID="litZongxu" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td >*商品总期数:</td>
                        <td>
                            <asp:Literal ID="litMaxQishu" runat="server"></asp:Literal>
                        </td>
                        <td style="width:120px">*商品价格：</td>
                        <td>
                            <asp:Literal ID="litJiage" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr id="trFahuo" runat="server">
                        <td>*发货时间:</td>
                        <td>
                            <asp:Literal ID="litSendTime" runat="server"></asp:Literal>
                        </td>
                        <td style="width:120px">*付款方式：</td>
                        <td>
                            <asp:Literal ID="litPayType" runat="server" ></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td>*中奖人:</td>
                        <td>
                            <asp:Literal ID="litLuckyName" runat="server"></asp:Literal>
                        </td>
                        <td style="width:120px">*购买码：</td>
                        <td style='word-WRAP: break-word'>
                            <div style="overflow:scroll;width:280px;height:90px"><asp:Literal ID="litYunGou" runat="server"></asp:Literal></div>
                        </td>

                    </tr>
                    <tr>
                        <td>*购买次数:</td>
                        <td>
                            <asp:Literal ID="litGoumai" runat="server"></asp:Literal>
                        </td>
                        <td style="width:120px">*购买价格：</td>
                        <td>
                            <asp:Literal ID="litPay" runat="server"></asp:Literal>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
            <%--团购--%>
            <div class="content" runat="server" id="divTuangou">
            <table class="table table-striped tabel-textalign" id="Table4" runat="server"> 
                <tbody>
                    <tr>
                        <td style="width:120px">*商品名称:</td>
                        <td>
                            <asp:Literal ID="litTuanProductName" runat="server"></asp:Literal>
                        </td>
                        <td style="width:120px">*订单ID:</td>
                        <td>
                            <asp:Literal ID="litTuanOrderID" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td>*成团人数:</td>
                        <td>
                            <asp:Literal ID="litNum" runat="server"></asp:Literal>
                        </td>
                        <td style="width:120px">*商品价格:</td>
                        <td>
                            <asp:Literal ID="litProductJiage" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td >*开始时间:</td>
                        <td>
                            <asp:Literal ID="litStarTime" runat="server"></asp:Literal>
                        </td>
                        <td style="width:120px">*结束时间：</td>
                        <td>
                            <asp:Literal ID="litEndTime" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr id="trFaHuoHou" runat="server">
                        <td>*发货时间:</td>
                        <td>
                            <asp:Literal ID="litOutGoods" runat="server"></asp:Literal>
                        </td>
                        <td style="width:120px">*付款方式：</td>
                        <td>
                            <asp:Literal ID="litPayTypeTuan" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td>*中奖人:</td>
                        <td>
                            <asp:Literal ID="litLuckyMan" runat="server"></asp:Literal>
                        </td>
                        <td style="width:120px">*购买码：</td>
                        <td id="Goumaima">
                            <asp:Literal ID="litGouMaiMa" runat="server"></asp:Literal>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>*购买次数:</td>
                        <td>
                            <asp:Literal ID="litCiShu" runat="server"></asp:Literal>
                        </td>
                        <td style="width:120px">*购买价格：</td>
                        <td>
                            <asp:Literal ID="litDanJia" runat="server"></asp:Literal>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
            <%--收货人信息--%>
            <div class="content">
            <table class="table" id="Table2" runat="server"> 
                <tbody>
                    <tr>
                        <td style="background-color:#eaddeb; font-weight: 100">收货人信息</td>
                    </tr>
                </tbody>
            </table>
           </div>
            <div class="content">
            <table class="table table-striped tabel-textalign">
                <tbody>
                    <tr>
                        <td style="width:120px">*购买人ID:</td>
                        <td>
                            <asp:Literal ID="litMemberID" runat="server"></asp:Literal>

                        </td>
                    </tr>
                    <tr>
                        <td>*购买人昵称:</td>

                        <td>
                            <asp:Literal ID="litMemberName" runat="server"></asp:Literal>

                        </td>
                    </tr>
                    <tr>
                        <td>*购买人邮箱:</td>
                        <td>
                            <asp:Literal ID="litMemberEmail" runat="server"> </asp:Literal>

                        </td>
                    </tr>
                    <tr>
                        <td>*购买人手机:</td>
                        <td>
                            <asp:Literal ID="litMemberPhone" runat="server"></asp:Literal>

                        </td>
                    </tr>
                    <tr>
                        <td>*购买时间:</td>
                        <td>
                            <asp:Literal ID="litTime" runat="server"></asp:Literal>

                        </td>
                    </tr>
                    <tr>
                        <td>*收货地址:</td>
                        <td>
                            <asp:Literal ID="litaddress" runat="server"></asp:Literal>

                        </td>
                    </tr>
                </tbody>
            </table>

        </div>
            <%--物流信息--%>
            <div class="content">
                <table class="table" id="Table3" runat="server"> 
                    <tbody>
                        <tr>
                            <td style="background-color:#eaddeb; font-weight: 100">物流信息</td>
                        </tr>
                    </tbody>
                </table>
               </div>
            <div class="content">
                <table class="table table-striped tabel-textalign">
                    <tbody>
                        <tr>
                            <td style="width:120px">*当前状态:</td>
                            <td>
                                <asp:Literal ID="litState" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                                <td>*物流公司:</td>
                            <td> <input  runat="server" id="inpCompany"/><asp:Literal ID="litCompany" runat="server" ></asp:Literal></td>
                        </tr>
                    </tbody>
                </table>  
            </div>
        <div>
            <input type="button"  class="btn btn-info" id="btnSave" style="float:right" value="确认发货" onclick="return delivery()" runat="server"/>
        </div>
    </form>
    <script  type="text/ecmascript">
        function delivery() {
            if (!confirm("是否确认发货！")) { return false; }
            if ($("#inpCompany").val() != "") {
                var company = $("#inpCompany").val();
                var orderId = $("#hidOrderId").val();
                $.ajax({
                    type: "POST",
                    contentType: "application/json",
                    url: "orderDelivery.aspx/delivery",
                    data: "{company:'" + company + "',orderId:'" + orderId + "'}",
                    dataType: 'json',
                    success: function (result) {
                        alert(result.d)
                        location.reload();
                    }
                });
            }
            else {
                    alert("请选择发货公司.")
            }   
        }
    </script>
</body>
</html>
