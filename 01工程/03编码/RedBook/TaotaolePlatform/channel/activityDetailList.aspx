<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="activityDetailList.aspx.cs" Inherits="RedBookPlatform.channel.activityDetailList" %>

<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8">
    <title>会员兑换记录</title>
    <style type="text/css">
        #msgDiv { z-index: 10001; width: 500px; height: 300px; background: white; border: #336699 1px solid; position: absolute; left: 50%; top: 20%; font-size: 12px; margin-left: -225px; display: none; }
        /*#bgDiv { position: absolute; top: 0px; left: 0px; right: 0px; background-color: #777; filter: progid: .Microsoft.Alpha(style=3,opacity=25,finishOpacity=75); opacity: 0.6; }*/
        .auto-style1 { height: 31px; }
    </style>
    <link rel="stylesheet" type="text/css" href="/Resources/css/comm.css" />
    <script src="/Resources/js/jquery.min.js" type="text/javascript" charset="utf-8"></script>
    <script src="/Resources/js/comm.js" type="text/javascript" charset="utf-8"></script>
    <script src="/Resources/laydate/laydate.dev.js"></script>
</head>
<body onload="showNo()">
    <form id="Form1" runat="server">
        <div class="wrapper" id="bgDiv">
            <div class="crumbs">
                <ul id="breadcrumbs" class="breadcrumb">
                    <li><a href="#">主页</a></li>
                    <li><a href="#">渠道管理</a></li>
                    <li class="active"><a href="#" title="">网吧活动记录</a></li>
                </ul>
            </div>
            <div class="content">
                <div class="oprationbtn">
                    时间范围:<input type="text" name="TimeStart" onclick="laydate({ istime: true, format: 'YYYY-MM-DD' })" id="txtTimeStart" runat="server">-
                    <input type="text" name="TimeEnd" onclick="laydate({ istime: true, format: 'YYYY-MM-DD' })" id="txtTimeEnd" runat="server">
                    <panel id="PlExchange" runat="server">
                        <span style="padding-right: 3px">状态:</span>
                        <asp:DropDownList ID="ddlExchange" runat="server" Style="padding: 4px 7px 4px 7px;" CssClass="btn">
                            <asp:ListItem Value="-1" Selected="True">请选择</asp:ListItem>
                            <asp:ListItem Value="0">未结算</asp:ListItem>
                            <asp:ListItem Value="1">已结算</asp:ListItem>
                        </asp:DropDownList>
                    </panel>
                    <a class="btn" href="javascript:;" runat="server" id="btnapplycash" onclick="ApplyCash()" style="margin-left: 30px" visible="true">申请提现</a>
                    <%--                    <a class="btn" href="javascript:;" runat="server" id="btnBatchSettlement" onclick="BatchSettlement()" style="margin-left: 10px" visible="true">线下批量结算</a>--%>
                    <asp:Button runat="server" ID="Export" CssClass="btn btn-info" Text="导出数据" OnClick="Export_Click" Style="margin-left: 30px" />
                    <div class="search">
                        <span>查询关键字</span>
                        <asp:TextBox runat="server" ID="tboxKeyword" MaxLength="10"></asp:TextBox>
                        <asp:Button runat="server" ID="btnSearch" CssClass="btn btn-info" Text="查询" OnClick="btnSearch_Click" />
                    </div>
                </div>
                <table class="table table-striped table-even">
                    <thead>
                        <tr>

                            <th style="width: 20px">
                                <input onclick="checkall()" id="cball" type="checkbox" /></th>
                            <th>序号</th>
                            <th>所属渠道商</th>
                            <th>参与人</th>
                            <th>活动名称</th>
                            <th>参与时间</th>
                            <th>结算时间</th>
                            <th>状态</th>
                        </tr>
                    </thead>

                    <tbody>
                        <asp:Repeater runat="server" ID="rptAdmin">
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <input type="checkbox" name="cbbox" u="<%#Eval("username") %>" id='cb<%#Eval("adid") %>' /></td>
                                    <td>
                                        <%#Container.ItemIndex + 1 %>  
                                    </td>
                                    <td><%#Eval ("realname") %></td>
                                    <td><%#Eval ("username") %></td>
                                    <td><%#Eval ("atitle") %></td>
                                    <td><%#Eval("createtime","{0:yyyy-MM-dd HH:mm:ss}") %></td>
                                    <td><%#Eval("settlementtime","{0:yyyy-MM-dd HH:mm:ss}") %></td>
                                    <td><%#Eval("exchange").ToString()=="0"?"<span style='color:red'>未结算</span>":"<span style='color:green'>已结算</span>" %></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
                <div class="pager oprationbtn">
                    <span style="float: left; padding-left: 0px">当前查询数据汇总:总共有<span style="color: red"><%=allusercount %></span>人次参与,
                        <span id="spSettlement" runat="server">已结算<span style="color: green"><%= hadexchange %></span>元,</span> 
                        <span id="spUnSettlement" runat="server">未结算<span style="color: red"><%= unexchange %></span>元</span></span> 
                    <UI:Pager runat="server" ShowTotalPages="true" ID="pager1" />
                </div>
            </div>
        </div>
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
            //申请提现
            function ApplyCash() {
                var idlist = '';
                var cblist = $("table [type='checkbox']")

                for (var i = 0; i < cblist.length; i++) {
                    if ($(cblist[i]).is(':checked')) {
                        idlist += $(cblist[i]).attr('id').split('cb')[1] + ",";

                    }
                }
                if (idlist.length == 0) {
                    confirm("请勾选需要申请提现的数据");
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
                    con = confirm("已选择【" + str + "】条记录，确认申请提现【" + str * 3 + "】元?"); //在页面上弹出对话框  
                    if (con == true) {
                        $.ajax({
                            type: 'post', dataType: 'json', timeout: 10000,
                            async: false,
                            url: location.href,
                            data: {
                                action: "submit",
                                adidlist: idlist

                            },
                            success: function (resultData) {
                                if (resultData.success == true) {
                                    location.reload();
                                    alert(resultData.msg);
                                } else {
                                    alert(resultData.msg);
                                }
                            }
                        });
                    }
                }
            }
            //线下批量结算
            function BatchSettlement() {
                var names = '';
                var idlist = '';

                var cblist = $("table [type='checkbox']")
                for (var i = 0; i < cblist.length; i++) {
                    if ($(cblist[i]).is(':checked')) {
                        idlist += $(cblist[i]).attr('id').split('cb')[1] + ",";
                        names += $(cblist[i]).attr('u') + ",";
                    }
                }

                if (idlist.length == 0) {
                    alert("请勾选需要批量结算的数据");
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
                    con = confirm("已选择【" + str + "】条记录，确认要结算【" + str * 3 + "】元吗?"); //在页面上弹出对话框  
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
        </script>
    </form>
</body>
</html>
