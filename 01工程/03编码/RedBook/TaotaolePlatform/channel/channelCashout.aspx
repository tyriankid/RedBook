<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="channelCashout.aspx.cs" Inherits="RedBookPlatform.channel.channelCashout" %>

<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8">
    <title>服务商提现</title>

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
                    <li class="active"><a href="#" title="">服务商提现</a></li>
                </ul>
            </div>
            <div class="content">
                <div class="oprationbtn" style="height:50px">
                    时间范围:<input type="text" name="TimeStart" onclick="laydate({ istime: true, format: 'YYYY-MM-DD' })" id="txtTimeStart" runat="server">-
                    <input type="text" name="TimeEnd" onclick="laydate({ istime: true, format: 'YYYY-MM-DD' })" id="txtTimeEnd" runat="server">
                    <div class="search" style="width:600px">

                           <span style="padding-right:3px; padding-left:10px">审核状态:</span>
                        <asp:DropDownList ID="ddlauditstatus" runat="server">
                            <asp:ListItem  Value="" Selected="True">请选择</asp:ListItem>
                            <asp:ListItem Value="0">未发放</asp:ListItem>
                            <asp:ListItem Value="1">已发放</asp:ListItem>
                            <asp:ListItem Value="2">未通过</asp:ListItem>

                        </asp:DropDownList>

                          <span style="padding-right:3px; padding-left:10px">查询关键字:</span>
                        <asp:TextBox runat="server" ID="tboxKeyword" MaxLength="10"></asp:TextBox>
                        <asp:Button runat="server" ID="btnSearch" CssClass="btn btn-info" Text="查询" OnClick="btnSearch_Click" OnClientClick="return validation();" />
                    </div>
                </div>
                <table class="table table-striped table-even">
                    <thead>
                        <tr>
                            <th>城市服务商</th>
                            <th>开户人</th>
                            <th>银行名</th>
                            <th>支行</th>
                            <th>金额(元)</th>
                            <th>时间</th>
                            <th>银行卡号</th>
                            <th>联系电话</th>
                            <th>审核状态</th>
                            <th>操作</th>
                        </tr>
                    </thead>

                    <tbody>
                        <asp:Repeater runat="server" ID="rptAdmin">
                            <ItemTemplate>
                                <tr>
                                    <td><%#Eval ("realname") %></td>
                                    <td><%#Eval("username") %></td>
                                    <td><%#Eval("bankname") %></td>
                                    <td><%#Eval("branch") %></td>
                                    <td><%# String.Format("{0:F}", Convert.ToDecimal(Eval("money")))   %></td>
                                    <td><%#Eval("reviewtime","{0:yyyy-MM-dd HH:mm:ss}") %></td>
                                    <td><%#Eval("banknumber") %></td>
                                    <td><%#Eval("linkphone") %></td>
                                    <td><%# getauditstatus(Eval("auditstatus").ToString())%></td>
                                    <td class="opration">
        <%# modifystatus(Eval("auditstatus").ToString(),Eval("rid").ToString(),Eval("username").ToString(),Eval("uid").ToString(),Eval("money").ToString()) %>

                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
                <div class="pager oprationbtn">
                       <span style="float:left; padding-left: 0px">当前查询数据汇总:已成功提现总金额<span style="color: red"><%=Allcashoutmoney %></span>元</span>
                    <UI:Pager runat="server" ShowTotalPages="true" ID="pager1" />
                </div>
            </div>

        </div>
    </form>

        <script>
            //通过服务商提现
            function AdoptApply(id, name, state,uid,money) {
                var tq;
                tq = (state == '1' ? '通过' : '不通过');
                var con;
                con = confirm("确定" + tq + "【" + name + "】提现申请吗?"); //在页面上弹出对话框  
                if (con == true) {
                    $.ajax({
                        type: 'post', dataType: 'json', timeout: 10000,
                        async: false,
                        url: location.href,
                        data: {
                            action: "adopt",
                            rid: id,
                            ustate: state,
                            uid:uid,
                            money:money
                        },
                        success: function (resultData) {
                            if (resultData.success == true) {
                                location.reload();
                                alert(resultData.msg);

                            } else if (resultData.success == false)
                            {
                                alert(resultData.msg);
                            }
                        }
                    });
                }
                else {
                    return
                }
            }

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
