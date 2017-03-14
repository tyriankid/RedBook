<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CardRechargeList.aspx.cs" Inherits="RedBookPlatform.CardRecharge.CardRechargeList" %>

<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <meta charset="UTF-8">
    <title>充值卡列表</title>

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
                    <li><a href="#">充值卡管理</a></li>
                    <li class="active"><a href="#" title="">充值卡列表</a></li>
                </ul>
            </div>
            <div class="content">
                <div class="oprationbtn">
                   
                     <a class="btn" href="javascript:;" onclick="openNewWin('/CardRecharge/AddCardRecharge.aspx')">批量添加</a>
                       <a class="btn" href="javascript:;" onclick="del()">删除</a>
                    <div class="search" style="">

                        <span style="padding-right: 3px; padding-left: 10px">卡号:</span>
                        <asp:TextBox runat="server" ID="txtcode" Width="100px" MaxLength="32"></asp:TextBox>
                        <span style="padding-right: 3px; padding-left: 10px">金额:</span>
                        <asp:TextBox runat="server" ID="txtmoney" Width="100px" MaxLength="10"></asp:TextBox>
                        <span style="padding-right: 3px; padding-left: 10px">用户:</span>
                        <asp:TextBox runat="server" ID="txtusername" Width="100px" MaxLength="10"></asp:TextBox>
                          <span style="padding-right: 3px; padding-left: 10px">订单编号:</span>
                        <asp:TextBox runat="server" ID="txtorderId" Width="100px" MaxLength="10"></asp:TextBox>
                        
                        <span style="padding-right: 3px; padding-left: 10px">是否使用:</span>
                        <asp:DropDownList ID="ddlisrepeat" runat="server">
                            <asp:ListItem Selected="True" Value="-1">请选择</asp:ListItem>
                            <asp:ListItem Value="0">未使用</asp:ListItem>
                            <asp:ListItem Value="1">已使用</asp:ListItem>
                        </asp:DropDownList>

                        <span style="padding-right: 3px; padding-left: 10px">用途:</span>

                        <asp:DropDownList ID="ddlusetype" runat="server">
                            <asp:ListItem Selected="True" Value="-1">请选择</asp:ListItem>
                            <asp:ListItem Value="1">充话费</asp:ListItem>
                            <asp:ListItem Value="2">充支付宝</asp:ListItem>
                            <asp:ListItem Value="3">充爽乐币</asp:ListItem>
                        </asp:DropDownList>




                        <asp:Button runat="server" ID="btnSearch" CssClass="btn btn-info" Text="查询" OnClick="btnSearch_Click" />
                    </div>
                </div>
                <table class="table table-striped table-even">
                    <thead>
                        <tr>
                            <th><input onclick="checkall()" id="cball"  type="checkbox"/>全选</th>
                              <th>唯一标识</th>
                            <th>卡号</th>
                            <th>密码</th>
                            <th>金额</th>
                            <th>是否使用</th>
                            <th>添加时间</th>
                            <th>充值期限</th>
                            <th>订单编号</th>
                            <th>用户</th>
                            <th>充值用途</th>
                            <th>使用时间</th>
                        
                        </tr>
                    </thead>

                    <tbody>
                        <asp:Repeater runat="server" ID="rptAdmin">
                            <ItemTemplate>
                                <tr>
                               <td><input  type="checkbox"  name="cbbox"  id="<%#Eval("isrepeat") %>cb<%#Eval("id") %>" /></td>
                                      <td><%#Eval("id") %></td>
                                    <td><%#Eval("code") %></td>
                                    <td><%#Eval("codepwd") %></td>
                                    <td>
                                         <%#(Eval("isrepeat", "{0}") == "0") ? " " : Eval("money")%>

                                    </td>
                                    <td>

                                        <%#(Eval("isrepeat", "{0}") == "0") ? "未使用" : "<span style='color:red'>已使用</span>"%>
                                    </td>
                                    <td><%# Eval("time", "{0:yyyy-MM-dd}")%></td>
                                    <td><%# Eval("rechargetime", "{0:yyyy-MM-dd}")%></td>
                                    <td><%#Eval("orderId") %></td>
                                    <td><%#Eval("username") %></td>
                                    <td><%# getuseType(Eval("usetype").ToString()) %></td>
                                    <td><%# Eval("usetime", "{0:yyyy-MM-dd}")%></td>
                                   <%-- <td class="opration"><span onclick="openNewWin('/Article/addArticle.aspx?id=<%#Eval("id") %>')">修改</span> | <span onclick="goDelete('<%#Eval("code") %>','<%#Eval("id") %>')">删除</span></td>--%>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
                <div class="pager oprationbtn">
                   
                    <UI:Pager runat="server" ShowTotalPages="true" ID="pager1" />
                </div>
            </div>

        </div>
    </form>

    <script>

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
                for (var i =0; i < a.length; i++) {
                    $(a[i]).attr('checked', false);
                    $(a[i]).prop('checked', false);
                }
            }
        }


       
        function del()
        {
            var idlist = '';
            var cblist = $("table [type='checkbox']")
            for (var i = 0; i < cblist.length; i++) {
                if($(cblist[i]).is(':checked')&&$(cblist[i]).attr('id').split('cb')[0]=="0")
                {
                    idlist += $(cblist[i]).attr('id').split('cb')[1]+",";
                }
            }

            if (idlist.length > 1)
            {
                goDelete(idlist)
            }


        }
     
        
        //删除列
        function goDelete( id) {
            if (!confirm("确认删除吗?")) { return false; }
            $.ajax({
                type: "POST",
                contentType: "application/json",
                url: "CardRechargeList.aspx/goDelete",
                data: "{id:'" + id + "'}",
                dataType: 'json',
                success: function (result) {
                   
                    alert(result.d);
                    $("table [type='checkbox']").each(function () {

                        if ($(this).is(':checked')) {
                            $(this).removeAttr("checked");

                        }


                    })
                    location.reload();

                   
                }
            });
        }
    </script>
</body>
</html>
