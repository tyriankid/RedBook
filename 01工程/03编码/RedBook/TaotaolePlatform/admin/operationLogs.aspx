<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="operationLogs.aspx.cs" Inherits="RedBookPlatform.admin.operationLogs" %>
<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>操作日志</title>
    <link rel="stylesheet" type="text/css" href="/Resources/css/comm.css" />
    <script src="/Resources/js/jquery.min.js" type="text/javascript" charset="utf-8"></script>
    <script src="/Resources/js/comm.js" type="text/javascript" charset="utf-8"></script>
    <script src="/Resources/laydate/laydate.dev.js"></script>
    <%-- <link href="../Resources/js/zTree/bootstrap.min.css" rel="stylesheet" />--%>
</head>
<body>
    <form id="Form1" runat="server">
        <div class="wrapper">
            <div class="crumbs">
                <ul id="breadcrumbs" class="breadcrumb">
                    <li><a href="#">主页</a></li>
                    <li><a href="#">管理员管理</a></li>
                    <li class="active"><a href="#" title="">操作日志</a></li>
                </ul>
            </div>
            <div class="content">
                <div class="oprationbtn" style="height: 50px">
               <div style="float:left">
                      <a class="btn" href="javascript:;" onclick="Export()">导出日志</a>
                    	<a class="btn" href="javascript:;" onclick="goDelete()">删除日志</a>

                  </div>
                 
                    <div class="search" style=" float:right">
                         	
                          时间范围:<input type="text" style="width:110px" name="TimeStart" onclick="laydate({ istime: true, format: 'YYYY-MM-DD' })" id="txtTimeStart" runat="server">-
                    <input type="text" name="TimeEnd" style="width:110px" onclick="laydate({ istime: true, format: 'YYYY-MM-DD' })" id="txtTimeEnd" runat="server">
                        <span style="padding-right: 3px; padding-left: 10px">操作类型:</span>
                        <asp:DropDownList ID="ddlAction" runat="server">
                            <asp:ListItem Value="0" Selected="True">全部</asp:ListItem>
                            <asp:ListItem Value="1">增加</asp:ListItem>
                            <asp:ListItem Value="2">删除</asp:ListItem>
                            <asp:ListItem Value="3">修改</asp:ListItem>
                            <asp:ListItem Value="4">审核</asp:ListItem>
                            <asp:ListItem Value="5">登陆</asp:ListItem>
                        </asp:DropDownList>
 <span style="padding-right: 3px; padding-left: 10px">操作用户:</span>
                        <asp:DropDownList ID="ddladmin" runat="server">
                          
                        </asp:DropDownList>
                        <span style="padding-right: 3px; padding-left: 10px">查询关键字:</span>
                        <asp:TextBox runat="server" ID="tboxKeyword" Width="170px" MaxLength="10"></asp:TextBox>
                        <asp:Button runat="server" ID="btnSearch" CssClass="btn btn-info" Text="查询" OnClick="btnSearch_Click" OnClientClick="return validation();" />
                    </div>
                </div>
                <table class="table table-striped table-even">
                    <thead>
                        <tr>
                            <th>操作人</th>
                            <th>IP</th>
                            <th>操作时间</th>
                            <th>操作页面</th>
                            <th>操作类型</th>
                            <th>操作前信息</th>
                            <th>操作后信息</th>

                        </tr>
                    </thead>

                    <tbody>
                        <asp:Repeater runat="server" ID="rptAdmin">
                            <ItemTemplate>
                                <tr>
                                    <td><%#Eval("username") %></td>
                                    <td><%#Eval("loginip") %></td>
                                    <td><%#Eval("actiontime","{0:yyyy-MM-dd HH:mm:ss}") %></td>
                                    <td><%#Eval("actionform") %></td>
                                    <td><%#getAction(Eval("action").ToString()) %></td>
                                    <td><%#Eval("infobefore") %></td>
                                    <td><%#Eval("infoafter") %></td>

                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
                <div class="pager oprationbtn">
                 
                    <ui:pager runat="server" showtotalpages="true" id="pager1" />
                </div>
            </div>

        </div>

        <div style="display:none">
            
<asp:Button ID="Export" runat="server"  Text="Button" OnClick="Export_Click" />
        </div>
        
    </form>

    <script>
        function Export()
        {
            document.getElementById('Export').click();

        }

        function validation() {
            var flag = true;
            var msg = "";
            var starTime = $("#txtTimeStart").val();
            var endTime = $("#txtTimeEnd").val();

            var starTimes = new Date(starTime.replace("-", "/").replace("-", "/"));
            var endTimes = new Date(endTime.replace("-", "/").replace("-", "/"));

            if (endTimes < starTimes) {
                flag = false; msg = "结束时间要大于开始时间!";
            }

            if (!flag) { alert(msg); }
            return flag;
        }

        //删除列
        function goDelete() {
            if (!confirm("确认删除三个月以前的日志吗?")) { return false; }
            $.ajax({
                type: 'post',
               
                url:  location.href,
                data: {
                    action: "goDelete",
                 
                },
                dataType: 'json',
                success: function (result) {
                    alert(result.msg);
                    location.reload();
                }
            });
        }

    </script>
</body>
</html>
