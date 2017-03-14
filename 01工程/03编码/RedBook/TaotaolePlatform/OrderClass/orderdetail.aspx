<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="orderdetail.aspx.cs" Inherits="RedBookPlatform.OrderClass.orderdetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>订单详情</title>
    <link rel="stylesheet" type="text/css" href="<%=CSS_PATH %>comm.css" />
    <script src="<%=JS_PATH %>jquery.min.js" type="text/javascript" charset="utf-8"></script>
    <script src="<%=JS_PATH %>/comm.js" type="text/javascript" charset="utf-8"></script>
    <script src="/Resources/laydate/laydate.dev.js"></script>
    <style>


    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <span style="line-height:30px;">用户名：<asp:Label ID="Name"  runat="server" ></asp:Label></span><br />
        <span style="line-height:30px;">商品信息：<asp:Label ID="ShopName"  runat="server" ></asp:Label></span><br />
        <span style="line-height:30px;">商品类型：<asp:Label ID="Shoptype"  runat="server" ></asp:Label></span><br />
        <span style="line-height:30px;">订单号：<asp:Label ID="Num"  runat="server" ></asp:Label></span><br />
        <span style="line-height:30px;">订单状态：<asp:Label ForeColor="Red" ID="status"  runat="server" ></asp:Label></span><br />
        <span style="line-height:30px;">订单金额：<asp:Label ID="Money"  runat="server" ></asp:Label>元</span><br />
        <span style="line-height:30px;"><%=showname %><asp:Label ID="Sname"  runat="server" ></asp:Label></span><br />
        <span style="line-height:30px;"><%=showphone %><asp:Label ID="Phone"  runat="server" ></asp:Label></span><br />
        <span style="line-height:30px;"><%=showaddress %><asp:Label ID="Address"  runat="server" ></asp:Label></span><br />
        <span style="line-height:30px;"><%=shuwexpress %><asp:Label ID="Express"  runat="server" ></asp:Label></span><br />
         <asp:Button runat="server" ID="close" CssClass="btn btn-info" Text="关闭"  />

    </div>
                         <script type="text/javascript">
                             $(function () {
                                 $("#close").click(function () {

                                     window.top.opener = null; window.close();
                                 });
                             })

                    </script>
    </form>
</body>
</html>
