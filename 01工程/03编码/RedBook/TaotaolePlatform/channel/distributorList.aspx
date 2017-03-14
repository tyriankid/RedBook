<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="distributorList.aspx.cs" Inherits="RedBookPlatform.channel.distributorList" %>

<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="UTF-8">
    <title>渠道商列表</title>

    <link rel="stylesheet" type="text/css" href="/Resources/css/comm.css" />
    <script src="/Resources/js/jquery.min.js" type="text/javascript" charset="utf-8"></script>
    <script src="/Resources/js/comm.js" type="text/javascript" charset="utf-8"></script>
</head>
<body>
    <form id="Form1" runat="server">
        <div class="wrapper">
            <div class="crumbs">
                <ul id="breadcrumbs" class="breadcrumb">
                    <li><a href="#">主页</a></li>
                    <li><a href="#">渠道管理</a></li>
                    <li class="active"><a href="#" title="">渠道商列表</a></li>
                </ul>
            </div>
            <div class="content">
                <div class="oprationbtn">
                    <a class="btn" href="javascript:;" onclick="openNewWin('/channel/addDistributor.aspx<%=agent %>')">添加渠道商</a>
                    <asp:Button ID="BtnBatchDownImage" CssClass="btn" runat="server" Text="批量下载推广码" OnClick="BtnBatchDownImage_Click" UseSubmitBehavior="false" style="margin-top:-3px;" />
                    <%--                    <asp:Button ID="BtnBatchInitPassword" CssClass="btn" runat="server" Text="批量密码初始化"  UseSubmitBehavior="false" OnClick="BtnBatchInitPassword_Click" />--%>
                    <div class="search">
                        <span>渠道商类型:

                        </span>
                        <asp:DropDownList ID="ddltype" runat="server"></asp:DropDownList>

                        查询关键字:
                        <asp:TextBox runat="server" Width="200" ID="tboxKeyword" MaxLength="10"></asp:TextBox>
                        <asp:DropDownList ID="ddlmoney" runat="server">
                            <asp:ListItem Selected="True" Value="0">按佣金金额升序</asp:ListItem>
                            <asp:ListItem Value="1">按佣金金额倒序</asp:ListItem>
                        </asp:DropDownList>

                        <asp:Button runat="server" ID="btnSearch" CssClass="btn btn-info" Text="查询" OnClick="btnSearch_Click" />
                    </div>
                </div>
                <table id="tableadmin" class="table table-striped table-even">
                    <thead>
                        <tr>
                            <th>渠道商名称</th>
                            <th style="<%=style %>">服务商</th>
                            <th>类型</th>
                            <th>联系人</th>
                            <th>联系方式</th>
                            <th>返佣比</th>
                            <th>佣金</th>
                            <th>推广数</th>
                            <th>推广充值</th>
                            <th>状态</th>
                            <th style="text-align:center">操作</th>
                        </tr>
                    </thead>

                    <tbody>
                        <asp:Repeater runat="server" ID="rptAdmin" OnItemCommand="rptAdmin_ItemCommand">
                            <ItemTemplate>
                                <tr>
                                    <td><%#Eval("realname") %></td>
                                    <td style="<%=style %>"><%#Eval("fwsname") %></td>
                                    <td><%#Eval("typename") %></td>
                                    <td><%#Eval("contacts") %></td>
                                    <td><%#Eval("usermobile") %></td>
                                    <td><%#Eval("rebateratio") %>%</td>
                                    <td><%#Eval("m") %></td>
                                    <td><%#Eval("usercount")%></td>
                                    <td><%# String.Format("{0:F}", Convert.ToDecimal(Eval("usercountmoney")))  %></td>

                                      <td><%#Eval("frozenstate").ToString()=="0"?"<span style='color:green'>正常</span>":Eval("frozenstate").ToString()=="1"?"<span style='color:red'>冻结</span>":"<span style='color:red'>终止网吧活动</span>" %></td>
                                    <td class="opration">
                                        <span>
                                            <a class="btn" href='addDistributor.aspx?k=k<%=agent%><%=action%>&uid=<%#Eval("uid") %>'>修改</a></span>
                                        <%--                                        <span>
                                            <a class="btn" href="#" onclick="javascript:FrozenUser('<%#Eval("uid") %>','<%#Eval("realname") %>','<%#Eval("frozenstate") %>')">
 <%#Eval("frozenstate").ToString()=="0"?"冻结":Eval("frozenstate").ToString()=="1"?"<span style='color:red'>解冻</span>":""%></a></span>--%>
                                        <span>
                                            <a class="btn" href='distributorRecodes.aspx?k=k<%=agent%>&uid=<%#Eval("uid") %>'>佣金</a></span>
                                        <span>
                                            <a class="btn" onclick="delinfo('<%#Eval("uid") %>','<%#Eval("realname") %>','<%#Eval("usercount")%>')">删除</a></span>
                                        <span>
                                            <asp:Button runat="server" Text="推广码" CssClass="btn" CommandName="Down" CommandArgument='<%#Eval("uid") %>' style="margin-top: -3px;" />

                                        </span>
                                        <span>
                                            <a class="btn" href="#" onclick="javascript:InitPassword('<%#Eval("uid") %>','<%#Eval("realname") %>','<%#Eval("usermobile") %>')">密码初始化</a></span>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
                <div class="pager oprationbtn">
                    <span style="float: left; padding-left: 0px">当前查询数据汇总:佣金总余额<span style="color: red"><%=allmoney %></span>元，推广用户汇总<span style="color: red"><%= allusercount %></span>人，推广充值汇总<span style="color: red"><%= String.Format("{0:F}", Convert.ToDecimal(allusermoney))  %></span>元</span>
                    <UI:Pager runat="server" ShowTotalPages="true" ID="pager1" />
                </div>
            </div>

        </div>
    </form>
    <script type="text/javascript">

        function delinfo(uid, realname, count) {
            if (!confirm("确认删除" + realname + "?")) { return false; }
            $.ajax({
                type: 'post', dataType: 'json', timeout: 10000,
                async: false,
                url: location.href,
                data: {
                    action: "goDelete",
                    id: uid,
                    count: count,
                    name: realname

                },
                success: function (resultData) {
                    if (resultData.success == true) {
                        location.reload();
                        alert(resultData.msg);

                    }
                }
            });
        }

        //下载推广码
        function DownImage(channelid) {
            $.ajax({
                type: 'post', dataType: 'json', timeout: 10000,
                async: false,
                url: location.href,
                data: {
                    action: "down",
                    channelid: channelid,
                },
                success: function (resultData) {
                    if (resultData.success == true) {
                        location.reload();
                        alert(resultData.msg);

                    }
                }
            });
        }

        //冻结或解冻服务商
        function FrozenUser(id, name, frozenstate) {
            var tq;
            tq = (frozenstate == '0' ? '停用' : '启用');
            var con;
            con = confirm("确定要" + tq + "用户【" + name + "】吗?"); //在页面上弹出对话框  
            if (con == true) {
                $.ajax({
                    type: 'post', dataType: 'json', timeout: 10000,
                    async: false,
                    url: location.href,
                    data: {
                        action: "FrozenUid",
                        uid: id,
                        ufrozen: frozenstate
                    },
                    success: function (resultData) {
                        if (resultData.success == true) {
                            location.reload();
                            alert(resultData.msg);

                        }
                    }
                });
            }
            else {
                return
            }
        }
        //密码初始化
        function InitPassword(id, name, mobile) {
            var con;
            con = confirm("确定要将【" + name + "】密码初始化为【" + mobile + "】吗?"); //在页面上弹出对话框  
            if (con == true) {
                $.ajax({
                    type: 'post', dataType: 'json', timeout: 10000,
                    async: false,
                    url: location.href,
                    data: {
                        action: "Initpass",
                        id: id,
                        mobile: mobile
                    },
                    success: function (resultData) {
                        if (resultData.success == true) {
                            location.reload();
                            alert(resultData.msg);

                        }
                    }
                });
            }
            else {
                return
            }
        }

    </script>
</body>
</html>
