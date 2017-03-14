<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="applyCash.aspx.cs" Inherits="RedBookPlatform.channel.applyCash" %>

<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>
<%@ Register TagPrefix="UI" Namespace="Taotaole.Common" Assembly="Taotaole.Common" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8">
    <title>添加服务商</title>
    <link rel="stylesheet" type="text/css" href="/Resources/css/comm.css" />
    <script src="/Resources/js/jquery.cityselect.js"></script>
    <script src="/Resources/js/city.min.js"></script>
    <script src="/Resources/js/ProvCity.js"></script>
    <script src="/Resources/js/jquery.min.js"></script>
    <style>
    </style>
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
                <table class="table table-striped tabel-textalign" style="width:560px"> 
                    <tbody>
                        <tr id="trClassify" runat="server">
                            <td style="width:150px"><span style="color: red">*</span>佣金金额:</td>
                            <td>
                                <asp:Label ID="lbMoney" runat="server" Text="Label"></asp:Label>
                            </td>
                        </tr>
                        <tr >
                            <td style="width:150px">
                                <span style="color: red">*</span>当月可提现金额:   
                                <a class="a1" tips="(例如:未满5万，累计下一个月周期，满5万，未满10万，计算5万的返佣，2500元。)">?</a>
                            </td>
                            <td>
                                 <%=showDrawableMoney %>   
                            </td>
                        </tr>
                        <tr>
                            <td>申请提现金额:</td>
                            <td>
                                <input type="text" name="money" id="Money" runat="server" value="" style="width: 50%;" /></td>
                        </tr>
                        <tr>
                            <td>开户人:</td>
                            <td>
                                <input type="text" name="username" id="UserName" runat="server" value="" style="width: 50%;" /></td>
                        </tr>
                        <tr>
                            <td>银行名称:</td>
                            <td>
                                <input type="text" name="bankname" id="BankName" runat="server" value="" style="width: 50%;" /></td>
                        </tr>
                        <tr>
                            <td>开户支行:</td>
                            <td>
                                <input type="text" name="branch" id="Branch" runat="server" value="" style="width: 50%;" /></td>
                        </tr>
                        <tr>
                            <td>银行账号:</td>
                            <td>
                                <input type="text" name="banknumber" id="BankNumber" runat="server" value="" style="width: 50%;" /></td>
                        </tr>
                        <tr>
                            <td>联系电话:</td>
                            <td>
                             <input type="text" id="LinkPhone"  name="linkphone" runat="server" value="" style="width: 50%;" /></td>   
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align: center">
                                <asp:Button runat="server" ID="btnSave" Text="提交申请" OnClientClick="return validation();" CssClass="btn btn-primary" OnClick="btnSave_Click" />
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
            if ($("#Money").val() == "") {
                flag = false; msg = "请填写提现金额!";
            }
            else if ($("#UserName").val() == "") {
                flag = false; msg = "请填写开户人!";
            }
            else if ($("#BankName").val() == "") {
                flag = false; msg = "请填写银行名称!";
            }
            else if ($("#BankNumber").val() == "") {
                flag = false; msg = "请填写银行账号!";
            }
            else if ($("#LinkPhone").val() == "") {
                flag = false; msg = "请填写联系电话!";
            }

            if (!flag) { alert(msg); }
            return flag;
        }
    </script>
    <script type="text/javascript">
        var Tips = function (spans) {
            if (Object.prototype.toString.call(spans) === '[object HTMLCollection]') {
                for (var i = 0; i < spans.length; i++) {
                    var span = spans[i];
                    fun(span);
                }
            } else {
                fun(spans);
            }
            function fun(span) {
                span.style.cssText = "display:inline-block;width:20px;height:20px;border-radius:50%;color:#fff;text-align:center;line-height:20px;background:#666;";
                var body = document.getElementsByTagName("body")[0];
                span.addEventListener("mouseover", function (event) {
                    var e = event.target;
                    var y = event.pageY,
                        x = event.pageX,
                        w = e.scrollWidth;
                    var i = document.createElement("i");
                    var tips = e.getAttribute("tips");
                    i.innerText = tips;
                    i.style.cssText = "position: fixed;padding: 2px 5px;font-style: normal;border-radius: 3px;border: 1px solid #000;box-shadow: 2px 2px 5px #888;background-color:#fff;z-index:999999999;"
                    i.style.top = y + "px";
                    i.style.left = x + w + "px";
                    body.insertBefore(i, body.childNodes[0]);
                });
                span.addEventListener("mouseout", function (event) {
                    body.removeChild(body.childNodes[0]);
                });
            }
        }

        var a1 = document.getElementsByClassName("a1");
        Tips(a1);
	</script>
</body>
</html>
