<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="brokerageSet.aspx.cs" Inherits="RedBookPlatform.channel.brokerageSet" %>

<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="UTF-8">
    <title>佣金比例设置</title>

    <link rel="stylesheet" type="text/css" href="/Resources/css/comm.css" />
    <script src="/Resources/js/jquery.min.js" type="text/javascript" charset="utf-8"></script>
    <script src="/Resources/js/comm.js" type="text/javascript" charset="utf-8"></script>
    <script src="/Resources/laydate/laydate.dev.js"></script>
</head>
<body>
    <div class="wrapper">
        <div class="crumbs">
            <ul id="breadcrumbs" class="breadcrumb">
                <li><a href="#">主页</a></li>
                <li><a href="#">渠道管理</a></li>
                <li class="active"><a href="#" title="">佣金比例设置</a></li>
            </ul>
        </div>
        <div class="content">
            <form runat="server" id="form2">
                <table class="table table-striped tabel-textalign">
                    <tbody>
                        <tr>
                            <td><span>消费者充值</span></td>
                            <td><span><input type="text" id="scoreSet"  runat="server" onkeyup="value=value.replace(/\D/g,'')" value="0"/>%爽乐币</span></td>
                        </tr>
                        <tr>
                            
                            <td><span>代理商</span></td>
                            <td><span><input type="text" id="agentSet"  runat="server" onkeyup="value=value.replace(/\D/g,'')" value="0"/>%现金返佣体现</span></td>
                        </tr>
                        <tr>
                            
                            <td><span>总部奖池</span></td>
                            <td><span><input type="text" id="giftSet" runat="server" onkeyup="value=value.replace(/\D/g,'')" value="0"/>%充值爽乐币总额</span></td>
                        </tr>
                        <tr>
                            
                            <td><span>总部预留</span></td>
                            <td><span><input type="text" id="obligateSet" runat="server" onkeyup="value=value.replace(/\D/g,'')" value="0"/>% 其他用处</span></td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align: center">
                                <asp:Button runat="server" ID="btnSave" Text="保 存"   CssClass="btn btn-primary" OnClick="btnSave_Click"   OnClientClick="return validation()"/>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </form>
        </div>
    </div>
     <script type="text/javascript">
         function validation() {
             var flag = true;
             var msg = "";
             if ($("#obligateSet").val()=="")
             {
                 flag = false; msg = "总部预留不能为空";
             }
             if ($("#giftSet").val() == "") {
                 flag = false; msg = "总部奖池不能为空!";
             }
             if ($("#agentSet").val() == "") {
                 flag = false; msg = "代理商不能为空!";
             }
             if ($("#scoreSet").val() == "") {
                 flag = false; msg = "充值返佣比例不能为空!";
             }
             if (!flag) { alert(msg); }
             return flag;
         }
    </script>
</body>
</html>
