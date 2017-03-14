<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="userRedpackList.aspx.cs" Inherits="RedBookPlatform.operative.userRedpackList" %>
<%@ Register assembly="ASPNET.WebControls" namespace="ASPNET.WebControls" tagprefix="UI" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>红包基本信息</title>
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
	                <li><a href="javascript:void(0);">红包基本信息</a></li>
	                <li class="active"><a href="javascript:void(0);" title="">红包列表</a></li>
	            </ul>
			</div>
		    <div class="content">
		    		<div class="search">
                        时段<asp:TextBox ID="tboxStarttime" runat="server" onclick="laydate({ istime: true, format: 'YYYY-MM-DD hh:mm:ss' })" Width="170px"></asp:TextBox>—<asp:TextBox ID="tboxEndtime" runat="server" onclick="laydate({ istime: true, format: 'YYYY-MM-DD hh:mm:ss' })" Width="170px"></asp:TextBox>
                            
                        <asp:DropDownList ID="dropState" runat="server">
                            <asp:ListItem Value="">全部</asp:ListItem>
                            <asp:ListItem Value="0" Text="待发放"></asp:ListItem>
                            <asp:ListItem Value="1" Text="已发放"></asp:ListItem>
                            <asp:ListItem Value="2" Text="已使用"></asp:ListItem>
                            <asp:ListItem Value="3" Text="过期"></asp:ListItem>
                        </asp:DropDownList>
		    			查询关键字
                        <asp:TextBox runat="server" ID="tboxKeyword" MaxLength="10" Width="200px"></asp:TextBox>
		    			<select name="">
		    				<option value="">按添加时间升序</option>
		    				<option value="">按添加时间倒序</option>
		    			</select>
                        <asp:Button runat="server" ID="btnSearch" CssClass="btn btn-info" Text="查询" OnClick="btnSearch_Click" />
		    		</div>
		    	<table class="table table-striped table-even">
                    <thead>
                        <tr>
                            <th>红包ID</th>
                            <th>红包标题</th>
                            <th>用户ID</th>
                            <th>活动ID</th>
                            <th>抵扣金额</th>
                            <th>领取时间</th>
                            <th>使用时间</th>
                            <th>过期时间</th>
                            <th>订单号</th>
                        </tr>
                    </thead>
                    
                    <tbody>
                        <asp:Repeater runat="server" ID="rptAdmin">
                        <ItemTemplate>
                            <tr>
                                <td><%#Eval("id") %></td>
                                <td><%#Eval("codetitle") %></td>
                                <td><%#Eval("uid") %></td>
                                <td><%#Eval("activity_id") %></td>
                                <td><%#Eval("amount").ToString()!="0"?"满"+Eval("amount")+"减"+Eval("discount"):Eval("discount")+"爽乐币" %></td>
                                <%--<td><%#Eval("discount").ToString()=="3"?"满三减二":Eval("discount").ToString() %></td>--%>
                                <td><%#Eval("addtime") %></td>
                                <td><%#Eval("usetime").ToString()==""?(Eval("state").ToString()=="0"?"未发放":(Eval("state").ToString()=="1"?"已发放":"已过期")):Eval("usetime").ToString() %></td>
                                <td><%#Eval("overtime").ToString()==""?"永不过期":Eval("overtime").ToString() %></td>
                                <td><%#Eval("order_id") %></td>
                                 </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    </tbody>
                </table>
                <div class="pager oprationbtn">
                    <UI:Pager runat="server" ShowTotalPages="true" ID="pager1" />
                </div>
                
                <div class="pager oprationbtn">
                   当前红包总金额：<asp:Label ID="labSumdiscount" runat="server"></asp:Label>&nbsp;&nbsp;已发放：<asp:Label ID="labdo" runat="server"></asp:Label>&nbsp;&nbsp;待发放：<asp:Label ID="labsave" runat="server"></asp:Label>&nbsp;&nbsp;已使用：<asp:Label ID="labisuse" runat="server"></asp:Label>&nbsp;&nbsp;已过期：<asp:Label ID="labover" runat="server"></asp:Label>
                </div>
		    </div>
            
		</div>
    </form>
</body>
</html>
