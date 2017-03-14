<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GiftUse.aspx.cs" Inherits="TaotaolePlatform.products.GiftUse" %>
<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>
<%@ Register TagPrefix="UI" Namespace="Taotaole.Common" Assembly="Taotaole.Common" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>积分抽奖活动管理</title>
    <link rel="stylesheet" type="text/css" href="/Resources/css/comm.css"/>
        <script src="/Resources/js/jquery.min.js" type="text/javascript" charset="utf-8"></script>
        <script src="/Resources/js/comm.js" type="text/javascript" charset="utf-8"></script>
</head>
<body>
    <form id="Form1" runat="server">
        <div class="wrapper">
            <div class="crumbs">
                <ul id="breadcrumbs" class="breadcrumb">
                    <li><a href="#">主页</a></li>
                    <li><a href="#">积分抽奖活动管理</a></li>
                    <li class="active"><a href="#" title="">积分抽奖活动列表</a></li>
                </ul>
            </div>
     <div class="datalist">   
                <table>
                    <tbody>
                        <tr>
                            <asp:CheckBoxList ID="chk_use" runat="server" RepeatColumns="3" RepeatDirection="Horizontal" Width="300px">
                            <asp:ListItem Value="gua" Text ="刮刮乐"></asp:ListItem>
                            <asp:ListItem Value="pao" Text="跑马灯"></asp:ListItem>
                            <asp:ListItem Value="zhuan" Text="大转盘"></asp:ListItem>

                    </asp:CheckBoxList>
                        </tr>
                    </tbody>
                </table>
                   
    </div>
        <asp:Button ID="btn_Save" runat="server" Text="保存"  CssClass="btn" OnClick="btn_Save_Click" />
    </div>
    </form>
</body>
</html>
