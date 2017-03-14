<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="memberlist.aspx.cs" Inherits="RedBookPlatform.member.memberlist" %>
<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>
<!DOCTYPE html>

<html>
	<head runat="server">
		<meta charset="UTF-8">
		<title>用户列表</title>
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
	                <li><a href="#">会员管理</a></li>
	                <li class="active"><a href="#" title="">会员列表</a></li>
	            </ul>
			</div>
		    <div class="content">
		    	<div class="oprationbtn">
		    		<a class="btn"  style="<%=display%>" href="javascript:;" onclick="openNewWin('/member/addmember.aspx')">添加会员</a>
		    		
                    <div class="search">
                        <span style="padding-right:3px">来源:</span>
                        <asp:DropDownList ID="dropType" runat="server">
                        <asp:ListItem Value="">全部</asp:ListItem>
                        <asp:ListItem Value="weixin">微信关注</asp:ListItem>
                        <asp:ListItem Value="baidu">百度推广</asp:ListItem>
                        </asp:DropDownList>
		    			<div  class="chaxun">
                        查询关键字
                        <asp:TextBox runat="server" ID="tboxKeyword" MaxLength="10"></asp:TextBox>
		    			<select name="sel"  runat="server" id="selMultiple">
                            <option value="0">按添加时间倒序</option>
		    				<option value="1">按添加时间升序</option>
		    				
		    			</select>
                         </div>
                        <asp:Button runat="server" ID="btnSearch" CssClass="btn btn-info " Text="查询"  OnClick="btnSearch_Click" />
		    		</div>
                    
		    	</div>
		    	<table class="table table-striped">
                    <thead>
                        <tr>
                            <th>ID</th>
                            <th>用户名</th>
                            <th>E-Mail</th>
                            <th>手机号</th>
                            <th>账户金额</th>
                            <th>手机号认证</th>
                            <th>账号来源</th>
                            <th>App唤醒</th>
                            <th style="<%=display%>">管理操作</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater runat="server" ID="rptmember">
                        <ItemTemplate>
                            <tr>
                                <td><%#Eval("uid") %></td>
                                <td><%#Eval("username") %></td>
                                <td><%#Eval("email") %></td>
                                <td><%#Eval("mobile") %></td>
                                <td><%#  Eval("typeid").ToString()=="9"?"0.00": Convert.ToDouble( Eval("money")).ToString("0.00") %></td>
                                <td><%#Eval("mobilecode").ToString()=="1"?"<span style='color:red'>已认证</span>":"未认证"%></td>
                                <td><%# Taotaole.Common.Globals.getEnumDisplayName(Taotaole.Common.Globals.businessType.user_type,Convert.ToInt32( Eval("typeid"))) %></td>
                                <td><%#Eval("approuse").ToString()=="0"?"未唤醒":"<span style='color:red'>已唤醒</span>" %></td>
                                <td  style="<%=display%>" class="opration"><span>
                                    <a class="btn" style="display:none" href='addmember.aspx?uid=<%#Eval("uid") %>'>修改</a></span>
                                    <span style="padding-left:5px" class='<%#Eval("uid") %>'>
                                    <a class="btn" href="#"  style="display:none"  onclick="javascript:Deletemember('<%#Eval("uid") %>','<%#Eval("username") %>')">删除</a></span>
                                    <span style="padding-left:5px" class='<%#Eval("uid") %>'>
                                    <a class="btn" href="#" onclick="javascript:sentredpack('<%#Eval("uid") %>')">发包</a></span>
                                    <span style="padding-left:5px" class='<%#Eval("uid") %>'>
                                    <a class="btn"  href="#" onclick="javascript:Ceaseusermember('<%#Eval("uid") %>','<%#Eval("username") %>','<%#Eval("ceaseuser") %>')"><%#Eval("ceaseuser").ToString()=="0"?"停用":"<span style='color:red'>启用</span>" %></a></span>
                                    <span style="padding-left:5px" class='<%#Eval("uid") %>'>
                                    <a class="btn" href="/member/memberDetail.aspx?uid=<%#Eval("uid") %>" >详情</a></span>
                                </td>
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
            <script type="text/javascript">
                function Deletemember(id,name)
                {
                    var con;
                    con = confirm("确定要删除用户【" + name + "】吗?"); //在页面上弹出对话框  
                    if (con == true) {
                        $.ajax({
                            type: 'post', dataType: 'json', timeout: 10000,
                            async: false,
                            url: location.href,
                            data: {
                                action: "delete",
                                uid: id,
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
                //发红包
                function sentredpack(id) {
                    var con;
                    con = confirm("确定要给用户发红包吗?"); //在页面上弹出对话框  
                    if (con == true) {
                        $.ajax({
                            type: 'post', dataType: 'json', timeout: 10000,
                            async: false,
                            url: location.href,
                            data: {
                                action: "sentredpack",
                                uid: id,
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
                //停用或启用账户
                function Ceaseusermember(id, name, cease)
                {
                    var tq;
                    tq = (cease == '0' ? '停用' : '启用');
                    var con;
                    con = confirm("确定要" + tq + "用户【" + name + "】吗?"); //在页面上弹出对话框  
                    if (con == true) {
                        $.ajax({
                            type: 'post', dataType: 'json', timeout: 10000,
                            async: false,
                            url: location.href,
                            data: {
                                action: "ceaseuser",
                                uid: id,
                                ucease: cease
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
        </form>
	</body>
</html>