<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="distributorRecodes.aspx.cs" Inherits="RedBookPlatform.channel.distributorRecodes" %>

<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8">
    <title>渠道商佣金</title>

    <link rel="stylesheet" type="text/css" href="/Resources/css/comm.css" />
    <script src="/Resources/js/jquery.min.js" type="text/javascript" charset="utf-8"></script>
    <script src="/Resources/js/comm.js" type="text/javascript" charset="utf-8"></script>
    <script src="/Resources/laydate/laydate.dev.js"></script>
    <style type="text/css">
        .auto-style1 { height: 31px; }
    </style>
</head>
<body>
    <form id="Form1" runat="server">
        <div class="wrapper">
            <div class="crumbs">
                <ul id="breadcrumbs" class="breadcrumb">
                    <li><a href="#">主页</a></li>
                    <li><a href="#">渠道管理</a></li>
                    <li class="active"><a href="#" title="">渠道商佣金</a></li>
                </ul>
            </div>
            <div class="content">
                <div class="oprationbtn">
                    时间范围:<input type="text" name="TimeStart" onclick="laydate({ istime: true, format: 'YYYY-MM-DD' })" id="txtTimeStart" runat="server" />-
                    <input type="text" name="TimeEnd" onclick="laydate({ istime: true, format: 'YYYY-MM-DD' })" id="txtTimeEnd" runat="server" />
                    <panel id="PlExchange" runat="server">
                        <span style="padding-right: 3px">状态:</span>
                        <asp:DropDownList ID="ddlExchange" runat="server" Style="padding: 4px 7px 4px 7px;" CssClass="btn">
                            <asp:ListItem Value="-1" Selected="True">请选择</asp:ListItem>
                            <asp:ListItem Value="0">未结算</asp:ListItem>
                            <asp:ListItem Value="1">已结算</asp:ListItem>
                        </asp:DropDownList>
                    </panel>
                    <a class="btn" href="javascript:;" runat="server" id="btnSettlement" onclick="Settlement()" style="margin-left: 10px" visible="true">佣金结算</a>
                    <div class="search">
                        查询关键字
                        <asp:TextBox runat="server" ID="tboxKeyword" MaxLength="10"></asp:TextBox>
                        <asp:Button runat="server" ID="btnSearch" CssClass="btn btn-info" Text="查询" OnClientClick="return validation();" OnClick="btnSearch_Click" />
                    </div>
                </div>
                <table class="table table-striped table-even">
                    <thead>
                        <tr>
                            <th id="checkall" runat="server">
                                <input onclick="checkall()" id="cball" type="checkbox" m="0" /></th>
                            <th>渠道商</th>
                            <th>充值用户</th>
                            <th>描述</th>
                            <th>充值金额</th>
                            <th>佣金</th>
                            <th>时间</th>
                            <th>状态</th>
                        </tr>
                    </thead>

                    <tbody>
                        <asp:Repeater runat="server" ID="rptAdmin" OnItemDataBound="rptAdmin_ItemDataBound">
                            <ItemTemplate>
                                <tr>
                                    <td id="checkall2" runat="server">
                                        <input type="checkbox" name="cbbox" u="<%#Eval("realname") %>" m="<%#Eval("money") %>" id='cb<%#Eval("rid") %>' /></td>
                                    <td><%#Eval ("realname") %></td>
                                    <td><%#Eval ("username") %></td>
                                    <td><%#Eval("content") %></td>
                                    <td><%#Eval("rechargemoney","{0:F2}") %></td>
                                    <td><%#Eval("money","{0:F2}") %></td>
                                    <td><%#Eval("time").ToString().Replace("/","-") %></td>
                                    <td><%#Eval("state").ToString()=="0"?"<span style='color:red'>未结算</span>":"<span style='color:green'>已结算</span>" %></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
                <div class="pager oprationbtn">
                    <span style="float: left; padding-left: 0px">当前查询数据汇总:充值总金额<span style="color: red"><%=allrechargemoney %></span>元，佣金总金额<span style="color: red"><%= allmoney %></span>元</span>
                    <UI:Pager runat="server" ShowTotalPages="true" ID="pager1" />
                </div>
            </div>

        </div>
    </form>
    <script type="text/javascript">
        function checkall() {
            var a = $("table [name='cbbox']");
            if ($("#cball").is(':checked')) {
                for (var i = 0; i < a.length; i++) {
                    // $(a[i]).attr('checked');
                    $(a[i]).attr('checked', true);
                    $(a[i]).prop('checked', true);

                }
            }
            else {
                for (var i = 0; i < a.length; i++) {
                    $(a[i]).attr('checked', false);
                    $(a[i]).prop('checked', false);
                }
            }
        }

        //线下佣金结算
        function Settlement() {
            var names = '';
            var idlist = '';
            var money = '';
            var array = new Array();
            var sum = 0;
            var cblist = $("table [type='checkbox']")
            for (var i = 0; i < cblist.length; i++) {
                if ($(cblist[i]).is(':checked')) {
                    idlist += $(cblist[i]).attr('id').split('cb')[1] + ",";
                    names += $(cblist[i]).attr('u') + ",";
                    money += $(cblist[i]).attr('m') + ",";
                }
            }
            array = money.split(",");
            for (var m = 0; m < array.length - 1; m++) {
                sum += parseFloat(array[m]);
            }
            if (idlist.length == 0) {
                alert("请勾选需要结算的数据");
                return;
            }
            else {
                var str = new Array();
                if ($("#cball").is(':checked')) {
                    str = idlist.split(",").length - 2;
                }
                else {
                    str = idlist.split(",").length - 1
                }
                con = confirm("已选择【" + str + "】条记录，确认要结算佣金【" + sum.toFixed(2) + "】元吗?"); //在页面上弹出对话框  
                if (con == true) {
                    $.ajax({
                        type: 'post', dataType: 'json', timeout: 10000,
                        async: false,
                        url: location.href,
                        data: {
                            action: "settlement",
                            adidlist: idlist,
                            names: names
                        },
                        success: function (resultData) {
                            if (resultData.success == true) {
                                alert(resultData.msg);
                                location.reload();

                            } else {
                                alert(resultData.msg);
                            }
                        }
                    });
                }
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
