<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="gifts.aspx.cs" Inherits="RedBookPlatform.products.gifts" %>
<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>
<%@ Register TagPrefix="UI" Namespace="Taotaole.Common" Assembly="Taotaole.Common" %>
<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <meta charset="UTF-8">
    <title>积分商品列表</title>

		<link rel="stylesheet" type="text/css" href="/Resources/css/comm.css"/>
        <script src="/Resources/js/jquery.min.js" type="text/javascript" charset="utf-8"></script>
        <script src="/Resources/js/comm.js" type="text/javascript" charset="utf-8"></script>
</head>
<body>
    <form id="Form1" runat="server">
        <div class="wrapper">
            <div class="crumbs">
                <ul id="breadcrumbs" class="breadcrumb">
                    <li><a href="#">主页</a></li>
                    <li><a href="#">积分商品管理</a></li>
                    <li class="active"><a href="#" title="">积分商品列表</a></li>
                </ul>
            </div>
            <div class="content">
                <div class="oprationbtn">
                    <a class="btn" style="<%=display%>" href="javascript:;" onclick="openNewWin('/products/addGifts.aspx')">添加积分商品</a>
                    <a class="btn" style="<%=display%>" href="javascript:;" onclick="openNewWin('/products/AddScope.aspx')">添加积分场次</a>
                    <div class="search">
                        <span style="padding-right:3px; padding-left:10px">查询关键字:</span>
                        <asp:TextBox runat="server" ID="tboxKeyword" MaxLength="10" Width="400px"></asp:TextBox>
                        <asp:Button runat="server" ID="btnSearch" CssClass="btn btn-info" Text="查询" OnClick="btnSearch_Click" />
                    </div>
                </div>
                <table class="table table-striped table-even">
                    <thead>
                        <tr>
                            <th>商品名称</th>
                            <th >商品场次</th>
                            <th>中奖概率基数/奖品个数</th>
                            <th>奖品总数/中奖数</th>
                             <th>商品库存</th>
                            <th>使用范围</th>
                            <th style="<%=display%>">操 作</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater runat="server" ID="rptTuangou"  >
                            <ItemTemplate>
                                <tr>
                                    <td><div class="imgBox"><img src="<%=G_UPLOAD_PATH %><%#Eval("thumb") %>"/></div><%#Eval("title") %></td>
                                    <td><%#Eval("scope") %></td>
                                    <td  style="background-color:<%#Eval("giftsuse").ToString()=="gua"?"#A8A8A8":"" %>"><%#Eval("probability") %>/<%#Eval("prizeNumber") %></td>
                                    <td  style="background-color:<%#Eval("giftsuse").ToString()=="gua"?"":"#A8A8A8" %>"><%#Globals.getGiftCodeSum(Eval("sumCode").ToString(),"")%>/<%#Globals.getGiftCodeSum("",Eval("winCode").ToString())%></td>
                                    <td><%#Eval("stock") %></td>
                                    <td><%#Globals.getGiftGameType(Eval("giftsuse").ToString()) %></td>
                                    <td  style="<%=display%>" class="opration"><span onclick="openNewWin('/products/addGifts.aspx?giftId=<%#Eval("giftId") %>')">修改</span> | <span onclick="goDelete('<%#Eval("title") %>',<%#Eval("giftId") %>)">删除</span></td>
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
        //删除列
        function goDelete(name, id) {
            var myDate = new Date();
            if (!confirm("确认将" + name + "删除?")) { return false; }
            $.ajax({
                type: 'post', dataType: 'json', timeout: 10000,
                async: false,
                url: location.href,
                data: {
                    action: "goDelete",
                    giftsId: id,
                    name: name
                },
                success: function (resultData) {
                    if (resultData.success == true) {
                        alert(resultData.msg);
                        location.reload();


                    }
                }
            });
        }
    </script>
</body>
</html>
