<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SystemBillList.aspx.cs" Inherits="RedBookPlatform.yolly.SystemBillList" %>
<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>系统账单</title>
		<link rel="stylesheet" type="text/css" href="/Resources/css/comm.css"/>
        <script src="/Resources/js/jquery.min.js" type="text/javascript" charset="utf-8"></script>
        <script src="/Resources/js/comm.js" type="text/javascript" charset="utf-8"></script>
        <script src="/Resources/laydate/laydate.dev.js"></script>
</head>
<body>
    <form id="form1" runat="server">
		<div class="wrapper">
			<div class="crumbs">
				<ul id="breadcrumbs" class="breadcrumb"> 
	                <li><a href="#">主页</a></li>
	                <li><a href="#">账单管理</a></li>
	                <li class="active"><a href="#" title="">系统账单</a></li>
	            </ul>
			</div>   
             <div class="search">
                       <span style="padding-right:3px">充值类型:</span>
                        <asp:DropDownList ID="paytype" runat="server">
                            <asp:ListItem Value="">全部</asp:ListItem>
                            <asp:ListItem Value="永乐充值">永乐充值</asp:ListItem>
                            <asp:ListItem Value="线下充值">线下充值</asp:ListItem>
                        </asp:DropDownList>
                        <span style="padding-right:3px">使用类型:</span>
                        <asp:DropDownList ID="dropType" runat="server">
                            <asp:ListItem Value="">全部</asp:ListItem>
                            <asp:ListItem Value="1">话费充值</asp:ListItem>
                            <asp:ListItem Value="2">支付宝充值</asp:ListItem>
                            <asp:ListItem Value="4">QQ币充值</asp:ListItem>
                        </asp:DropDownList>
                         <span style="padding-right:3px; padding-left:10px">状态:</span>
                            <asp:DropDownList ID="dropState" runat="server">
                            <asp:ListItem Value="">全部</asp:ListItem>
                            <asp:ListItem Value="1">已成功</asp:ListItem>
                            <asp:ListItem Value="0">待充值</asp:ListItem>
                            <asp:ListItem Value="2">充值失败</asp:ListItem>
                        </asp:DropDownList>
                        <span>开始时间:</span>
                       <input type="text" name="inpTime" id="inpStarTime"  runat="server"  value="" style="width:15.5%"   onclick="laydate({ istime: true, format: 'YYYY-MM-DD hh:mm:ss' })"/>
                        <span>结束时间:</span>
                       <input type="text" name="inpTime" id="inpEndTime"  runat="server"  value="" style="width:15.5%;"   onclick="laydate({ istime: true, format: 'YYYY-MM-DD hh:mm:ss' })"/>
                        <span style="padding-right:3px; padding-left:10px">查询关键字:</span>
                        <asp:TextBox runat="server" ID="tboxKeyword" MaxLength="50" Width="200px"></asp:TextBox>
                        <asp:Button runat="server" ID="btnSearch" CssClass="btn btn-info" Text="查询" OnClick="btnSearch_Click" />
                        <asp:Button ID="tongbuyolly" runat="server" Text="同步状态"  CssClass="btn btn-info" OnClick="tongbuyolly_Click" />
                        <span>最新同步时间:</span><asp:Label runat="server" ID="time"></asp:Label>
                        </div>
		    	<table class="table table-striped">
                    <thead>
                        <tr>
                            <th style="text-align:center">流水号</th>
                            <th>订单号</th>
                            <th>会员账户</th>
                            <th>充值金额</th>
                            <th>充值账号</th>
                            <th>使用类型</th>
                            <th>充值类型</th>
                            <th>充值状态</th>
                            <th>充值备注</th>
                            <th>使用时间</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater runat="server" ID="rptRechargerecord">
                        <ItemTemplate>
                            <tr>
                                <td style="text-align:center"><%#Eval("serialid") %></td>
                                <td><%#Eval("orderId") %></td>
                                <td><%#Eval("username") %></td>
                                <td><%# Convert.ToDecimal( Eval("money")).ToString("0.00") %></td>
                                <td><%#Eval("usenum") %></td>
                                <td><%# Taotaole.Common.Globals.getEnumDisplayName(Taotaole.Common.Globals.businessType.yollytype,Convert.ToInt32( Eval("usetype"))) %></td>
                                <td><%#Eval("paytype").ToString()=="线下充值"?"<span style='color:red'>线下充值</span>":"永乐充值" %></td>
                                <td><%# Eval("issynchro").ToString()=="1"
                                    ? "<a href='javascript:void(0);' onclick=\"update('"+Eval("serialid")+"','"+Eval("usenum")+"','"+ Convert.ToDecimal( Eval("money")).ToString("0.00")+"')\">"+Taotaole.Common.Globals.getEnumDisplayName(Taotaole.Common.Globals.businessType.yollystatus,Convert.ToInt32( Eval("status")))+"</a>"
                                    : Eval("status").ToString()=="2"?"<a href='javascript:void(0);' onclick=\"update('"+Eval("serialid")+"','"+Eval("usenum")+"','"+ Convert.ToDecimal( Eval("money")).ToString("0.00")+"')\">"+ Taotaole.Common.Globals.getEnumDisplayName(Taotaole.Common.Globals.businessType.yollystatus,Convert.ToInt32( Eval("status")))+"</a>"
                                    : Taotaole.Common.Globals.getEnumDisplayName(Taotaole.Common.Globals.businessType.yollystatus,Convert.ToInt32( Eval("status"))) %></td>
                                <td><%#Eval("context") %></td>
                                <td><%#Eval("usetime") %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    </tbody>
                </table>
                <div class="pager oprationbtn">
                     <span style="float:left"> 当前消费总金额：<span style="color:red"><asp:Label ID="labSum" runat="server"></asp:Label></span>元</span>
                    <UI:Pager runat="server" ShowTotalPages="true" ID="pager1" />
                </div>
            </div>
            <script type="text/javascript">
                function update(serialid, usenum, money) {

                   


                    var con;

                    con = confirm("确定要将流水号【" + serialid + "】、充值账号【" + usenum + "】、金额【" + money + "】订单在线下充值吗?"); //在页面上弹出对话框  
                    if (con == true) {

                        var code = "<%=code%>";
                        if (code != '3') {
                            alert('您没有权限')
                            return;
                        }
                        $.ajax({
                            type: 'post', dataType: 'json', timeout: 10000,
                            async: false,
                            url: location.href,
                            data: {
                                action: "update",
                                serialid: serialid,
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

                    }
                }

    </script>
    </form>

</body>
</html>
