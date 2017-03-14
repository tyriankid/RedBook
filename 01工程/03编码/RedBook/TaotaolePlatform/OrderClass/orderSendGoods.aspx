<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="orderSendGoods.aspx.cs" Inherits="RedBookPlatform.OrderClass.orderSendGoods" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link rel="stylesheet" type="text/css" href="/Resources/css/comm.css"/>
        <script src="/Resources/js/jquery.min.js" type="text/javascript" charset="utf-8"></script>
        <script src="/Resources/js/comm.js" type="text/javascript" charset="utf-8"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
                <table class="table table-striped tabel-textalign" >
                    <thead id="disPlay" runat="server">
                        <tr >
                            <th  style="text-align:center">商品图片</th>
                            <th  style="text-align:center">商品名称</th>
                            <th style="text-align:center">收货人姓名/账号</th>
                            <th style="text-align:center">收货人地址</th>
                            <th style="text-align:center">快递公司</th>
                            <th style="text-align:center">快递单号</th>
                        </tr>
                    </thead>
                    <tbody id="dtInpAll">
                        <asp:Repeater runat="server" ID="rptAdmin">
                            <ItemTemplate>
                                <tr >
                                    <td style="width:6%;text-align:center">
                                        <span><div class="imgBox"><img src="<%=G_UPLOAD_PATH %><%#Eval("thumb") %>"/></div></span>
                                    </td>
                                    <td style="width:10%;text-align:center" >
                                        <span><%#Eval("title") %></span>
                                    </td>
                                    <td style="width:6%;text-align:center">
                                        <span><%#Eval("order_address_info").ToString().Split('*')[0].Split('@').Length==2?Eval("order_address_info").ToString().Split('@')[1].Split('*')[0]:Eval("order_address_info").ToString().Split('*')[1] %></span>
                                    </td>
                                    <td  style="width:8%;text-align:center">
                                       <span><%#Eval("order_address_info").ToString().Split('*')[0] %></span>
                                    </td>
                                    <td  style="width:6%;text-align:center">
                                       <span><input  id="inpCompany"  type="text"/></span>
                                    </td>
                                    <td style="width:6%;text-align:center">
                                      <span><input id="inpCode" type="text" /></span>
                                     </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
                <div>
                    <input type="button"  class="btn btn-info" id="btnSave" style="float:right" value="确认发货" onclick="allSendGoods()" runat="server"/>
                </div>
            </div>
        <input type ="hidden" runat="server" id="hidOrderId" />
    </form>
    <script  type="text/ecmascript">
        function allSendGoods() {
            var company =""; var code ="";
            var inpAll = document.getElementById('dtInpAll').getElementsByTagName('input');
            if (!confirm("是否确认发货！")) { return false; }
            for (var i = 0; i < inpAll.length; i++) {
                if (i % 2 == 0) {
                    company+= inpAll[i].value + ',';
                }
                else {

                    code+= inpAll[i].value + ',';
                }
                var txt = inpAll[i].value;
                if (txt == "") {
                    alert("快递公司和快递单号不能为空！");
                    return false;
                }
            }
            if (company!= "" && code!= "") {
                var orderId = $("#hidOrderId").val().replace(/'/g, "")
                $.ajax({
                    type: "POST",
                    contentType: "application/json",
                    url: "orderSendGoods.aspx/allSendGoods",
                    data: "{company:'" + company+ "',orderId:'"+orderId+ "',code:'" + code+ "'}",
                    dataType: 'json',
                    success: function (result) {
                        alert(result.d)
                        window.opener.location.reload();
                        self.close()
                    }
                });
            }
        }
    </script>
</body>
</html>
