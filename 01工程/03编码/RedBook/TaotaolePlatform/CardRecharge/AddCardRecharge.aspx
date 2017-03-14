<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddCardRecharge.aspx.cs" Inherits="RedBookPlatform.CardRecharge.AddCardRecharge" %>

<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <meta charset="UTF-8">
    <title>添加充值卡</title>

    <link rel="stylesheet" type="text/css" href="/Resources/css/comm.css" />
    <script src="/Resources/js/jquery.min.js" type="text/javascript" charset="utf-8"></script>
    <script src="/Resources/js/comm.js" type="text/javascript" charset="utf-8"></script>
</head>
<body>
    <form id="Form1" runat="server">
        <div class="wrapper">
            <div class="crumbs">
                <ul id="breadcrumbs" class="breadcrumb">
                    <li><a href="#">主页</a></li>
                    <li><a href="#">充值卡管理</a></li>
                    <li class="active"><a href="#" title="">添加充值卡</a></li>
                </ul>
            </div>
            <div class="content">

                 <asp:Label ID="Label2" runat="server" Text="卡号位数："></asp:Label>
                <asp:TextBox ID="txtcode" runat="server" Text="17"></asp:TextBox>
             
                 <asp:Label ID="Label3" runat="server" Text="密码位数："></asp:Label>
                <asp:TextBox ID="txtpwd" runat="server"  Text="18"></asp:TextBox>


                <asp:Label ID="Label1" runat="server" Text="请输入要生成的数量："></asp:Label>
                <asp:TextBox ID="TextBox1" Text="1" runat="server"></asp:TextBox>
                <asp:Button ID="Button1" runat="server" Text="生&nbsp;&nbsp;成" OnClick="Button1_Click" />

            </div>

        </div>
    </form>


</body>
</html>

