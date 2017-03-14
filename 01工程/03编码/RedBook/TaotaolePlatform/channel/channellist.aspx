<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="channelList.aspx.cs" Inherits="RedBookPlatform.channel.channellist" %>

<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8">
    <title>服务商列表</title>

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
                    <li class="active"><a href="#" title="">服务商列表</a></li>
                </ul>
            </div>
            <div class="content">
                <div class="oprationbtn">
                    <a class="btn"  href="javascript:;" onclick="openNewWin('/channel/addChannel.aspx')">添加服务商</a>
                    <div class="search" style="width:550px">
                        查询关键字
                        <asp:TextBox runat="server" ID="tboxKeyword" MaxLength="15"></asp:TextBox>
                        <asp:DropDownList ID="ddlorder" runat="server">
                            <asp:ListItem Selected="True" Value="0">按添加时间倒序</asp:ListItem>
                             <asp:ListItem Value="1">按添加时间升序</asp:ListItem>
                             <asp:ListItem Value="2">按推广充值倒序</asp:ListItem>
                             <asp:ListItem Value="3">按推广充值升序</asp:ListItem>

                        </asp:DropDownList>
                       
                        <asp:Button runat="server" ID="btnSearch" CssClass="btn btn-info" Text="查询" OnClick="btnSearch_Click" />
                    </div>
                </div>
                <table class="table table-striped table-even">
                    <thead>
                        <tr>
                         
                            <th>服务商名称</th>
                            <th>所属城市</th>
                            <th>联系人</th>
                            <th>联系方式</th>
                            <th>返佣比例</th>
                            <th>佣金金额</th>
                            <th>渠道商数</th>
                            <th>推广用户数</th>
                            <th>推广总充值</th>
                           
                            <th>操作</th>
                        </tr>
                    </thead>


                    <tbody>
                        <asp:Repeater runat="server" ID="rptAdmin">
                            <ItemTemplate>
                                <tr>
                                  
                                    <td><%#Eval("realname") %></td>
                                    <td><%#Eval("cityid") %></td>
                                    <td><%#Eval("contacts") %></td>
                                    <td><%#Eval("usermobile") %></td>
                                    <td><%#Eval("rebateratio") %>%</td>
                                    <td><a href='channelRecodes.aspx?uid=<%#Eval("uid") %>' style="color: blue"><%#Taotaole.Bll.go_channel_recodesBusiness.SelectSumMoney((int)Eval("uid")) %></a></td>
                                    <td><a href='distributorList.aspx?uid=<%#Eval("uid") %>' style="color: blue"><%#Taotaole.Bll.go_channel_userBusiness.SelectChannelCount((int)Eval("uid")) %></a></td>
                                    <td><%#Eval("usercount")%></td>
                                    <td><%# String.Format("{0:F}", Convert.ToDecimal(Eval("usercountmoney")))  %></td>
                                   
                                    <td class="opration">
                                        <span>
                                            <a class="btn" href='addChannel.aspx?agent=<%=agent%>&action=modifyfws&uid=<%#Eval("uid") %>'>修改</a></span>
                                        <span style="padding-left: 5px" class='<%#Eval("uid") %>'>
                                            <a class="btn" href="#" onclick="javascript:FrozenUser('<%#Eval("uid") %>','<%#Eval("realname") %>','<%#Eval("frozenstate") %>')"><%#Eval("frozenstate").ToString()=="0"?"冻结":"<span style='color:red'>解冻</span>" %></a></span>
                                        <span>
                                            <a class="btn" href='channelRecodes.aspx?uid=<%#Eval("uid") %>'>佣金明细</a></span>

                                        <span>
                                            <a class="btn" onclick="delinfo('<%#Eval("uid") %>','<%#Eval("realname") %>')" >删除</a></span>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
                <div class="pager oprationbtn">
                    <span style="float:left; padding-left: 0px">当前查询数据汇总:佣金总余额<span style="color: red"><%=allmoney %></span>元，推广用户汇总<span style="color: red"><%= allusercount %></span>人，推广充值汇总<span style="color: red">     <%= String.Format("{0:F}", Convert.ToDecimal(allusermoney))  %></span>元</span>
                    <UI:Pager runat="server" ShowTotalPages="true" ID="pager1" />
                </div>
            </div>

        </div>
    </form>

    <script>
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

        function delinfo(uid, realname)
        {
           
            if (!confirm("确认删除" + realname + "?")) { return false; }
            
            $.ajax({
                type: 'post', dataType: 'json', timeout: 10000,
                async: false,
                url: location.href,
                data: {
                    action: "goDelete",
                    id: uid,
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
       
    </script>
</body>
</html>
