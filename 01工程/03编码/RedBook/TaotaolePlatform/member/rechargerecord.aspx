<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rechargerecord.aspx.cs" Inherits="RedBookPlatform.member.rechargerecord" %>

<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>充值记录</title>

    <link rel="stylesheet" type="text/css" href="/Resources/css/comm.css" />
    <script src="/Resources/js/jquery.min.js" type="text/javascript" charset="utf-8"></script>
    <script src="/Resources/js/comm.js" type="text/javascript" charset="utf-8"></script>
    <script src="/Resources/laydate/laydate.dev.js"></script>
</head>
<body>
    <form id="Form1" runat="server">
        <div class="wrapper">
            <div class="crumbs">
                <ul id="breadcrumbs" class="breadcrumb">
                    <li><a href="#">主页</a></li>
                    <li><a href="#">会员管理</a></li>
                    <li class="active"><a href="#" title="">充值记录</a></li>
                </ul>
            </div>

            <a id="disPlay" runat="server">
                <input class="btn " value="删 除" style="width: 87px" runat="server" onclick="return oncDelete()" id="btnClick" />
                <input type="button" id="All" value="全选" class="pager oprationbtn" onclick="checkAll()" />
                <input type="button" id="Button1" value="反选" class="pager oprationbtn" onclick="uncheckAll()" /></a>
            <div class="oprationbtn">
                <div class="search">
                    <span>开始时间:</span>
                    <input type="text" name="inpTime" id="inpStarTime" runat="server" value="" style="width: 18.5%" onclick="laydate({ istime: true, format: 'YYYY-MM-DD hh:mm:ss' })" />
                    <span>结束时间:</span>
                    <input type="text" name="inpTime" id="inpEndTime" runat="server" value="" style="width: 18.5%;" onclick="laydate({ istime: true, format: 'YYYY-MM-DD hh:mm:ss' })" />
                    <span style="padding-right: 3px; padding-left: 10px">查询关键字:</span>
                    <asp:TextBox runat="server" ID="tboxKeyword" MaxLength="20" Width="150px"></asp:TextBox>
                    <asp:DropDownList ID="dropStatus" runat="server">
                        <asp:ListItem Value="">全部</asp:ListItem>
                        <asp:ListItem Value="未付款" Text="未付款"></asp:ListItem>
                        <asp:ListItem Value="已付款" Text="已付款"></asp:ListItem>
                        <asp:ListItem Value="已退款" Text="已退款"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:Button runat="server" ID="btnSearch" CssClass="btn btn-info" Text="查询" OnClick="btnSearch_Click" />
                </div>
            </div>
            <table class="table table-striped">
                <thead>
                    <tr>
                        <th>ID</th>
                        <th>用户ID</th>
                        <th>用户名</th>
                        <th>订单ID</th>
                        <th>充值金额</th>
                        <th>系统赠送</th>
                        <th>总部奖池</th>
                        <th>总部预留</th>
                        <th>佣金</th>
                        <th>支付方式</th>
                        <th>付款状态</th>
                        <th>记录</th>
                        <th>时间</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater runat="server" ID="rptRechargerecord">
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <input name="CheckBoxGroup" type="checkbox" value='<%#Eval("id") %>' /><%#Eval("id") %></td>
                                <td><%#Eval("uid") %></td>
                                <td><%#Eval("username") %></td>
                                <td><%#Eval("code") %></td>
                                <td><%#Eval("money","{0:F2}") %></td>
                                <td><%#Eval("giftMoney","{0:F2}") %></td>
                                <td><%#Eval("pricemoney","{0:F2}") %></td>
                                <td><%#Eval("obligatemoney","{0:F2}") %></td>
                                <td><%#Eval("chanmoney","{0:F2}") %></td>
                                <td><%#Eval("pay_type") %></td>
                                <td><%#Eval("status") %></td>
                                <td><%#Eval("score").ToString() == "1"  ? "团购在线支付" : "充值" %></td>
                                <td><%#Eval("time","{0:yyyy-MM-dd HH:mm:ss}") %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
            <div class="pager oprationbtn">
                <span style="float: left">当前充值总金额：<span style="color: red"><asp:Label ID="labSum" runat="server"></asp:Label></span>元</span>
                <span style="float: left">，当前总部预留总额：<span style="color: red"><asp:Label ID="libYu" runat="server"></asp:Label></span>元</span>
                <span style="float: left">，当前总部奖池总额：<span style="color: red"><asp:Label ID="LabJiang" runat="server"></asp:Label></span>元</span>
                <span style="float: left">，当前系统赠送总额：<span style="color: red"><asp:Label ID="LabGiftMoney" runat="server"></asp:Label></span>元</span>
                <span style="float: left">，当前佣金总额：<span style="color: red"><asp:Label ID="LabYong" runat="server"></asp:Label></span>元</span>
                <UI:Pager runat="server" ShowTotalPages="true" ID="pager1" />
            </div>
        </div>
    </form>
    <script type="text/javascript">
        function checkAll() {//全选
            var code_Values = document.getElementsByTagName("input");
            for (i = 0; i < code_Values.length; i++) {
                if (code_Values[i].type == "checkbox") {
                    code_Values[i].checked = true;
                }
            }
        }
        function uncheckAll() {//反选
            var code_Values = document.getElementsByTagName("input");
            for (i = 0; i < code_Values.length; i++) {
                if (code_Values[i].type == "checkbox") {
                    code_Values[i].checked = false;
                }
            }
        }
        function oncDelete() {
            var ids = "";
            $("input:checked[name='CheckBoxGroup']").each(function () {
                ids += $(this).val() + ",";
            });
            if (ids == "") {
                alert("没有勾选任何项")
                return false;
            }
            if (!confirm("将删除勾选的未付款项？")) { return false; }
            else {
                $.ajax({
                    type: "POST",
                    contentType: "application/json",
                    url: "rechargerecord.aspx/oncDelete",
                    data: "{Id:'" + ids + "'}",
                    dataType: 'json',
                    success: function (result) {
                        alert(result.d)
                        location.reload();
                    }
                });
            }

        }
    </script>
</body>
</html>
