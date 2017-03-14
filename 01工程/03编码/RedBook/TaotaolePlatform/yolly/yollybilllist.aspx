<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="yollybilllist.aspx.cs" Inherits="RedBookPlatform.yolly.yollybilllist" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>充值记录</title>
    
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
	                <li class="active"><a href="#" title="">永乐账单</a></li>
	            </ul>
			</div>   
                <div style="padding-top:10px;">
                    <ul>
                        <li>
                            <span><asp:Button runat="server" Text="同步账单" OnClick="Unnamed1_Click" CssClass=" btn btn-info" /></span>
                        </li>
                    </ul>
                </div>
            </div>
    </form>
</body>
</html>
