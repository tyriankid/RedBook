<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="redpacklist.aspx.cs" Inherits="RedBookPlatform.operative.redpacklist" %>

<%@ Register assembly="ASPNET.WebControls" namespace="ASPNET.WebControls" tagprefix="UI" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>红包基本信息</title>
    <link rel="stylesheet" type="text/css" href="/Resources/css/comm.css"/>
        <script src="/Resources/js/jquery.min.js" type="text/javascript" charset="utf-8"></script>
        <script src="/Resources/js/comm.js" type="text/javascript" charset="utf-8"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="wrapper">
			<div class="crumbs">
				<ul id="breadcrumbs" class="breadcrumb"> 
	                <li><a href="index.html">主页</a></li>
	                <li><a href="javascript:void(0);">红包基本信息</a></li>
	                <li class="active"><a href="javascript:void(0);" title="">红包列表</a></li>
	            </ul>
			</div>
		    <div class="content">
		    	<div class="oprationbtn">
		    		<a class="btn" href="javascript:;" onclick="openNewWin('/operative/redpackadd.aspx')">添加红包类别</a>
		    	
                    <div class="search">
                        <div class="chaxun">	
		    			查询关键字
                        <asp:TextBox runat="server" ID="tboxKeyword" MaxLength="10"></asp:TextBox>
		    			<select name="">
		    				<option value="">按添加时间升序</option>
		    				<option value="">按添加时间倒序</option>
		    			</select>
                            </div>
                        <asp:Button runat="server" ID="btnSearch" CssClass="btn btn-info" Text="查询" OnClick="btnSearch_Click" />
		    		</div>
		    	</div>
		    	<table class="table table-striped table-even">
                    <thead>
                        <tr>
                            <th>红包类别ID</th>
                            <th>红包标题</th>
                            <th>金额条件</th>
                            <th>抵扣金额</th>
                            <th>操作</th>
                        </tr>
                    </thead>
                    
                    <tbody>
                        <asp:Repeater runat="server" ID="rptAdmin">
                        <ItemTemplate>
                            <tr>
                                <td><%#Eval("id") %></td>
                                <td><%#Eval("title") %></td>
                                <td><%#Eval("amount") %></td>
                                <td><%#Eval("discount") %></td>
                                 <td class="opration"><span onclick="openNewWin('/operative/redpackadd.aspx?id=<%#Eval("id") %>')">修改</span> | <span onclick="DeleteRedpack(<%#Eval("id") %>)">删除</span></td>
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
</body>
     <script type="text/javascript">
         function DeleteRedpack(id) {
             var con;
             con = confirm("确定要删除该红包?"); //在页面上弹出对话框  
             if (con == true) {
                 $.ajax({
                     type: 'post', dataType: 'json', timeout: 10000,
                     async: false,
                     url: "/operative/redpackadd.aspx?id="+id,
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
            </script>
</html>
