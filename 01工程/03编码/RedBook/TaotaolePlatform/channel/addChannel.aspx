<%@ Page Language="C#" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="addChannel.aspx.cs" Inherits="RedBookPlatform.channel.addChannel" %>

<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>
<%@ Register TagPrefix="UI" Namespace="Taotaole.Common" Assembly="Taotaole.Common" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="UTF-8">
    <title>添加服务商</title>
    <link rel="stylesheet" type="text/css" href="/Resources/css/comm.css" />
    <script src="/Resources/js/jquery.cityselect.js"></script>
    <script src="/Resources/js/city.min.js"></script>
    <script src="/Resources/js/ProvCity.js"></script>
    <script src="/Resources/js/jquery.min.js"></script>
</head>
<body>
    <div class="wrapper">
        <div class="crumbs">
            <ul id="breadcrumbs" class="breadcrumb">
                <li><a href="#">主页</a></li>
                <li><a href="#">渠道管理</a></li>
                <li class="active"><a href="#" title="">添加服务商</a></li>
            </ul>
        </div>
        <div class="content">
            <form runat="server" id="form1">
                <table class="table table-striped tabel-textalign" style="width:630px">
                    <tbody>
                        <tr id="trClassify" runat="server">
                            <td style="width:130px"><span style="color:red">*</span>用户名</td>
                            <td style="width:500px">
                                <input type="text" name="username" id="UserName" runat="server" value="" style="width: 50%;" /><span style="color:red">&nbsp;默认密码123456</span></td>
                        </tr>
                        <tr >
                            <td>
                             <span style="color:red">*</span>   <asp:Label ID="lbUserName" runat="server" Text="服务商名称"></asp:Label></td>
                            <td>
                                <input type="text" name="realname" id="RealName" runat="server" value="" style="width: 50%;" /></td>
                        </tr>
                          
                        <tr>
                            <td><span style="color:red">*</span>联系人</td>
                            <td>
                                <input type="text" name="contacts" id="Contacts" runat="server" value="" style="width: 50%;" /></td>
                        </tr>
                        <tr>
                            <td><span style="color:red">*</span>联系手机</td>
                            <td>
                                <input type="text" name="usermobile" id="UserMobile" runat="server" value="" style="width: 50%;" /></td>
                        </tr>
                        <tr>
                            <td>联系邮箱</td>
                            <td>
                                <input type="text" name="useremail" id="UserEmail" runat="server" value="" style="width: 50%;" /></td>
                        </tr>
                        <tr>
                            <td><span style="color:red">*</span>所在城市</td>
                            <td>
                                <asp:DropDownList ID="DropDownList1" runat="server" onChange="test(this);">
                                    <asp:ListItem></asp:ListItem>
                                    
                                    <asp:ListItem >北京</asp:ListItem>
                                    <asp:ListItem>上海</asp:ListItem>
                                    <asp:ListItem>天津</asp:ListItem>
                                    <asp:ListItem>重庆</asp:ListItem>
                                    <asp:ListItem>河北</asp:ListItem>
                                    <asp:ListItem>山西</asp:ListItem>
                                    <asp:ListItem>内蒙古</asp:ListItem>
                                    <asp:ListItem>辽宁</asp:ListItem>
                                    <asp:ListItem>吉林</asp:ListItem>
                                    <asp:ListItem>黑龙江</asp:ListItem>
                                    <asp:ListItem>江苏</asp:ListItem>
                                    <asp:ListItem>浙江</asp:ListItem>
                                    <asp:ListItem>安徽</asp:ListItem>
                                    <asp:ListItem>福建</asp:ListItem>
                                    <asp:ListItem>江西</asp:ListItem>
                                    <asp:ListItem>山东</asp:ListItem>
                                    <asp:ListItem>河南</asp:ListItem>
                                    <asp:ListItem>湖北</asp:ListItem>
                                    <asp:ListItem>湖南</asp:ListItem>
                                    <asp:ListItem>广东</asp:ListItem>
                                    <asp:ListItem>广西</asp:ListItem>
                                    <asp:ListItem>海南</asp:ListItem>
                                    <asp:ListItem>四川</asp:ListItem>
                                    <asp:ListItem>贵州</asp:ListItem>
                                    <asp:ListItem>云南</asp:ListItem>
                                    <asp:ListItem>西藏</asp:ListItem>
                                    <asp:ListItem>陕西</asp:ListItem>
                                    <asp:ListItem>甘肃</asp:ListItem>
                                    <asp:ListItem>宁夏</asp:ListItem>
                                    <asp:ListItem>青海</asp:ListItem>
                                    <asp:ListItem>新疆</asp:ListItem>
                                    <asp:ListItem>香港</asp:ListItem>
                                    <asp:ListItem>澳门</asp:ListItem>
                                    <asp:ListItem>台湾</asp:ListItem>
                                </asp:DropDownList>
                                <asp:DropDownList ID="DropDownList2" runat="server">
                                        <asp:ListItem>请选择</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td><span style="color:red">*</span>返佣比例</td>
                            <td>
                                <input type="text" name="rebateratio" id="Rebateratio" runat="server" value="" style="width: 50%;" />%</td>
                        </tr>

                      
                          <tr>
                            <td><span style="color:red">*</span>支付宝账号</td>
                            <td>
                                <input type="text" name="txtgatheringaccount" id="txtgatheringaccount" runat="server" value="" style="width: 50%;" /></td>
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
                flag = false; msg = "请填写服务商名称!";
            }
           
            else if ($("#Contacts").val() == "") {
                flag = false; msg = "请填写联系人!";
            }
            else if ($("#UserMobile").val() == "") {
                flag = false; msg = "请填写联系手机!";
            }
            
            else if ($("#DropDownList2").val() == "请选择") {
                flag = false; msg = "请选择所在省市!";
            }
            else if ($("#Rebateratio").val() == "") {
                flag = false; msg = "请填写返佣比例!";
            }
            else if ($("#txtgatheringaccount").val() == "") {
                flag = false; msg = "请填写支付宝账号!";
            }
            
            if (!flag) { alert(msg); }
            return flag;
        }
    </script>
</body>
</html>
