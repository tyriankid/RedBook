<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Configs.aspx.cs" Inherits="RedBookPlatform.admin.Configs" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:TextBox ID="TextBox1" runat="server" Height="112px" TextMode="MultiLine" Width="440px"></asp:TextBox>
        <asp:TextBox ID="TextBox2" runat="server" Height="108px" TextMode="MultiLine" Width="429px"></asp:TextBox>
        <br />
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />
        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Button" />
        <br />
    
    </div>
    </form>
</body>
</html>
