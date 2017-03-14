<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="channelRecodes.aspx.cs" Inherits="RedBookPlatform.channel.channelRecodes" %>

<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8">
    <title>服务商佣金</title>

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
                    <li><a href="#">渠道管理</a></li>
                    <li class="active"><a href="#" title="">服务商佣金</a></li>
                </ul>
            </div>
            <div class="content">
                <div class="oprationbtn" style="height: 50px;">
                    <span id="ApplyCash" runat="server">
                        <a class="btn" id="btntx" runat="server" href="javascript:;" onclick="openNewWin('/channel/applyCash.aspx?uid=<%=applyUid %>')">申请提现</a>&nbsp;&nbsp;</span>
                    时间范围:<input type="text" name="TimeStart" onclick="laydate({ istime: true, format: 'YYYY-MM-DD' })" id="txtTimeStart" runat="server" />-
                    <input type="text" name="TimeEnd" onclick="laydate({ istime: true, format: 'YYYY-MM-DD' })" id="txtTimeEnd" runat="server" />
                    <div class="search">
                        查询关键字
                        <asp:TextBox runat="server" ID="tboxKeyword" MaxLength="10"></asp:TextBox>
                        <asp:Button runat="server" ID="btnSearch" CssClass="btn btn-info" Text="查询" OnClientClick="return validation();" OnClick="btnSearch_Click" />
                    </div>
                </div>
                <table class="table table-striped table-even">
                    <thead>
                        <tr>
                            <th>城市服务商</th>
                            <th>充值用户</th>
                            <th>描述</th>
                            <th>充值金额</th>
                            <th>佣金</th>
                            <th>时间</th>

                        </tr>
                    </thead>

                    <tbody>
                        <asp:Repeater runat="server" ID="rptAdmin">
                            <ItemTemplate>
                                <tr>
                                    <td><%#Eval ("realname") %></td>
                                    <td><%#Eval ("username") %></td>
                                    <td><%#Eval("content") %></td>
                                    <td><%#  String.Format("{0:F}", Convert.ToDecimal(Eval("rechargemoney")))   %></td>
                                    <td><%#String.Format("{0:F}", Convert.ToDecimal(Eval("money"))) %></td>
                                    <td><%#Eval("time").ToString().Replace("/","-") %></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
                <div class="pager oprationbtn">
                    <span style="float: left; padding-left: 0px">当前查询数据汇总:充值总金额<span style="color: red"><%=allrechargemoney %></span>元，佣金总金额<span style="color: red"><%= allmoney %></span>元<%=nowithdrawcash %></span>
                    <UI:Pager runat="server" ShowTotalPages="true" ID="pager1" />
                </div>
            </div>

        </div>
    </form>

    <script type="text/javascript">
        function validation() {
            var flag = true;
            var msg = "";
            var starTime = $("#txtTimeStart").val();
            var endTime = $("#txtTimeEnd").val();

            var starTimes = new Date(starTime.replace("-", "/").replace("-", "/"));
            var endTimes = new Date(endTime.replace("-", "/").replace("-", "/"));

            if (endTimes < starTimes) {
                flag = false; msg = "结束时间要大于开始时间!";
            }

            if (!flag) { alert(msg); }
            return flag;
        }
    </script>
</body>
</html>
