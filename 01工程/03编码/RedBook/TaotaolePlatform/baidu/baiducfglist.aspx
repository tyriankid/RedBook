<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="baiducfglist.aspx.cs" Inherits="RedBookPlatform.baidu.baiducfglist" %>
<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>
<!DOCTYPE html>

<html>
	<head id="Head1" runat="server">
		<meta charset="UTF-8">
		<title>推广列表</title>
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
	                <li><a href="#">百度推广</a></li>
	                <li class="active"><a href="#" title="">推广列表</a></li>
	            </ul>
			</div>
		    <div class="content">
		    	<div class="oprationbtn">
		    		<a class="btn"   href="addbaiducfglist.aspx" onclick="openNewWin('/baidu/addbaiducfglist.aspx')">添加推广</a>
		    		
                    <div class="search">
		    			<div  class="chaxun">
                       
                         </div>
                       
		    		</div>
                    
		    	</div>
		    	<table class="table table-striped">
                    <thead>
                        <tr>
                            <th style="white-space:nowrap;">推广ID</th>
                            <th style="white-space:nowrap;">推广商品</th>
                            <th style="white-space:nowrap;">商品业务ID</th>
                            <th style="white-space:nowrap;">推广编码</th>
                            <th style="white-space:nowrap;">推广时间</th>
                            <th style="white-space:nowrap;">推广标识</th>
                            <th style="white-space:nowrap;">推广状态</th>
                            <th style="white-space:nowrap;">管理操作</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater runat="server" ID="rptmember">
                        <ItemTemplate>
                            <tr>
                                <td style="white-space:nowrap;"><%#Eval("cfgid") %></td>
                                <td style="white-space:nowrap;"><%#Eval("title") %></td>
                                <td style="white-space:nowrap;"><%#Eval("originalid") %></td>
                                <td style="word-break: break-word;"><%#Eval("codequantitys") %></td>
                                <td><%#Eval("buytimes") %></td>
                                <td style="white-space:nowrap;"><%#Eval("cfgtype") %></td>
                                <td style="white-space:nowrap;"><%#Eval("isdelete").ToString()=="0"?"正常":"已停用" %></td>
                                <td style="white-space:nowrap;">
                                    <span><a class="btn" href='addbaiducfglist.aspx?cfgid=<%#Eval("cfgid") %>'>修改</a></span>
                                    <span style="padding-left:5px" class='<%#Eval("cfgid") %>'><a class="btn" href="#"onclick="javascript:Deletemember('<%#Eval("cfgid") %>','<%#Eval("title") %>')">删除</a></span>
                                    <span style="padding-left:5px" class='<%#Eval("cfgid") %>'>
                                        <a class="btn"  href="#" onclick="javascript:Ceaseusermember('<%#Eval("cfgid") %>','<%#Eval("title") %>','<%#Eval("isdelete") %>')"><%#Eval("isdelete").ToString()=="0"?"停用":"<span style='color:red'>启用</span>" %></a></span></td>
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
                function Deletemember(id, name) {
                    var con;
                    con = confirm("确定要删除商品【" + name + "】吗?"); //在页面上弹出对话框  
                    if (con == true) {
                        $.ajax({
                            type: 'post', dataType: 'json', timeout: 10000,
                            async: false,
                            url: location.href,
                            data: {
                                action: "delete",
                                id: id,
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
                function Ceaseusermember(cfgid, title, isdelete) {
                    var tq;
                    tq = (isdelete == '0' ? '停用' : '启用');
                    var con;
                    con = confirm("确定要" + tq + "商品【" + title + "】吗?"); //在页面上弹出对话框  
                    if (con == true) {
                        $.ajax({
                            type: 'post', dataType: 'json', timeout: 10000,
                            async: false,
                            url: location.href,
                            data: {
                                action: "ceaseuser",
                                cfgid: cfgid,
                                ucease: isdelete
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