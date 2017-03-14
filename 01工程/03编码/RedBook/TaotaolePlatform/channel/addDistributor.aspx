<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="addDistributor.aspx.cs" Inherits="RedBookPlatform.channel.addDistributor" %>

<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>
<%@ Register TagPrefix="UI" Namespace="Taotaole.Common" Assembly="Taotaole.Common" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="UTF-8">
    <title>添加服务商</title>
    <link rel="stylesheet" type="text/css" href="/Resources/css/comm.css" />
    <script src="/Resources/js/jquery.cityselect.js"></script>

    <script src="/Resources/js/jquery.min.js"></script>
</head>
<body>
    <div class="wrapper">
        <div class="crumbs">
            <ul id="breadcrumbs" class="breadcrumb">
                <li><a href="#">主页</a></li>
                <li><a href="#">渠道管理</a></li>
                <li class="active"><a href="#" title="">添加渠道商</a></li>
            </ul>
        </div>
        <div class="content">
            <form runat="server" id="form1">
                <table class="table table-striped tabel-textalign" style="width: 630px">
                    <tbody>
                        <tr id="trClassify" runat="server">
                            <td style="width: 130px"><span style="color: red">*</span>用户名</td>
                            <td style="width: 500px">
                                <input type="text" name="username" id="UserName" runat="server" value="" style="width: 50%;" /><span style="color: red">&nbsp;默认密码为当前联系人手机号</span></td>
                        </tr>
                        <tr id="trfws" runat="server">
                            <td><span style="color: red">*</span>所属服务商</td>
                            <td>
                                <asp:DropDownList ID="ddlparent" runat="server"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span style="color: red">*</span>
                                <asp:Label ID="lbUserName" runat="server" Text="渠道商名称"></asp:Label></td>
                            <td>
                                <input type="text" name="realname" id="RealName" runat="server" value="" style="width: 50%;" /></td>
                        </tr>

                        <tr>
                            <td><span style="color: red">*</span>联系人</td>
                            <td>
                                <input type="text" name="contacts" id="Contacts" runat="server" value="" style="width: 50%;" /></td>
                        </tr>
                        <tr>
                            <td><span style="color: red">*</span>联系手机</td>
                            <td>
                                <input type="text" name="usermobile" id="UserMobile" runat="server" value="" style="width: 50%;" /></td>
                        </tr>
                        <tr>
                            <td>联系邮箱</td>
                            <td>
                                <input type="text" name="useremail" id="UserEmail" runat="server" value="" style="width: 50%;" /></td>
                        </tr>

                        <tr>
                            <td><span style="color: red">*</span>返佣比例</td>
                            <td>
                                <input type="text" name="rebateratio" id="Rebateratio" runat="server" value="" style="width: 50%;" />%</td>
                        </tr>


                        <tr>
                            <td><span style="color: red">*</span>支付宝账号</td>
                            <td>
                                <input type="text" name="txtgatheringaccount" id="txtgatheringaccount" runat="server" value="" style="width: 50%;" /></td>
                        </tr>
                        <tr id="trtype" runat="server">
                            <td><span style="color: red">*</span>类型</td>
                            <td>
                                <asp:DropDownList ID="ddltype" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddltype_SelectedIndexChanged"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr runat="server" visible="false" id="trjsjg">
                            <td><span style="color: red">*</span>结算价格</td>
                            <td>
                                <input type="text" name="txtsettlementprice" id="txtsettlementprice" runat="server" value="" style="width: 50%;" /></td>
                        </tr>

                        <tr runat="server" visible="false" id="trState">
                            <td><span style="color: red">*</span>状态</td>
                            <td>
                                <asp:RadioButtonList ID="rbState" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="0" Selected="True">正常</asp:ListItem>
                                    <asp:ListItem Value="1">冻结</asp:ListItem>
                                    <asp:ListItem Value="2">终止网吧活动</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td>描述</td>
                            <td>
                                <textarea rows="5" cols="5" name="remark" id="Remark" runat="server" class="span12" style="width: 50%;"></textarea></td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align: center">
                                <asp:Button runat="server" ID="btnSave" Text="保存" OnClientClick="return validation();" CssClass="btn btn-primary" OnClick="btnSave_Click" />
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

            if ($("#UserName").val() == "") {
                flag = false; msg = "请填写用户名!";
            }
            else if ($("#RealName").val() == "") {
                flag = false; msg = "请填写渠道商名称!";
            }

            else if ($("#Contacts").val() == "") {
                flag = false; msg = "请填写联系人!";
            }
            else if ($("#UserMobile").val() == "") {
                flag = false; msg = "请填写联系手机!";
            }

            else if ($("#Rebateratio").val() == "") {
                flag = false; msg = "请填写返佣比例!";
            }
            else if ($("#txtgatheringaccount").val() == "") {
                flag = false; msg = "请填写支付宝账号!";
            }
            else if ($("#ddltype option:selected").text() == "请选择") {
                flag = false; msg = "请选择类型!";
            }
            if (!flag) { alert(msg); }
            return flag;
        }
    </script>
</body>
</html>
