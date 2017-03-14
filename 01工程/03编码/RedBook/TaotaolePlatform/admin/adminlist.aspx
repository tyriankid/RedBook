<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="adminlist.aspx.cs" Inherits="RedBookPlatform.admin.adminlist" %>

<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <meta charset="UTF-8">
    <title>管理员列表</title>

    <link rel="stylesheet" type="text/css" href="/Resources/css/comm.css" />
    <script src="/Resources/js/jquery.min.js" type="text/javascript" charset="utf-8"></script>
    <script src="/Resources/js/comm.js" type="text/javascript" charset="utf-8"></script>
</head>
<body>
    <form runat="server">
        <div class="wrapper">
            <div class="crumbs">
                <ul id="breadcrumbs" class="breadcrumb">
                    <li><a href="#">主页</a></li>
                    <li><a href="#">管理员管理</a></li>
                    <li class="active"><a href="#" title="">管理员列表</a></li>
                </ul>
            </div>
            <div class="content">
                <div class="oprationbtn">
                    <a class="btn" href="javascript:;" onclick="openNewWin('/admin/addadmin.aspx')">添加管理员</a>
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
                <table class="table table-striped">
                    <thead>
                        <tr>
                            <th>ID</th>
                            <th>用户名</th>
                            <th>最后登录IP</th>
                            <th>最后登录时间</th>
                            <th>E-Mail</th>
                            <th>管理操作</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater runat="server" ID="rptAdmin">
                            <ItemTemplate>
                                <tr>
                                    <td><%#Eval("uid") %></td>
                                    <td><%#Eval("username") %></td>
                                    <td><%#Eval("loginip") %></td>
                                    <td><%#Eval("logintime") %></td>
                                    <td><%#Eval("useremail") %></td>
                                    <td class="opration"><span>
                                        <a class="btn" href='addadmin.aspx?uid=<%#Eval("uid") %>'>修改</a></span>
                                        <a class="btn" href='modifymenujson.aspx?uid=<%#Eval("uid") %>'>编辑权限</a></span>
                                        <span style="padding-left: 5px" class='<%#Eval("uid") %>'>
                                            <a class="btn" href="#" onclick="javascript:DeleteAdmin('<%#Eval("uid") %>','<%#Eval("username") %>')">删除</a></span>
                                        <span style="padding-left: 5px" class='<%#Eval("uid") %>'>
                                            <a class="btn" href="#" onclick="javascript:UpdatePass('<%#Eval("uid") %>','<%#Eval("username") %>')">重置密码</a></span>
                                        <span style="padding-left: 5px" class='<%#Eval("uid") %>'>
                                              <a class="btn" href="#" onclick="javascript:FrozenAdmin('<%#Eval("uid") %>','<%#Eval("username") %>','<%#Eval("state") %>')">
                                                  <%#Eval("state").ToString()=="0"?"冻结":"<span style='color:red'>解冻</span>" %></a></span>
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
            $(function () {
                $(".1").hide();

            });
            function DeleteAdmin(id, name) {
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
                else {
                    return
                }

            }
            function UpdatePass(id, name) {
                var con;
                con = confirm("确定更改用户【" + name + "】的密码改为“123456”吗？"); //在页面上弹出对话框  
                if (con == true) {
                    $.ajax({
                        type: 'post', dataType: 'json', timeout: 10000,
                        async: false,
                        url: location.href,
                        data: {
                            action: "updatepass",
                            uid: id
                        },
                        success: function (resultData) {
                            if (resultData.success == true) {
                                alert(resultData.msg);
                                location.reload();
                              

                            }
                        }
                    });
                }
            }
            //冻结或解管理员账号
            function FrozenAdmin(id, name, state) {
                var tq;
                tq = (state == '0' ? '停用' : '启用');
                var con;
                con = confirm("确定要" + tq + "【" + name + "】账号吗?"); //在页面上弹出对话框  
                if (con == true) {
                    $.ajax({
                        type: 'post', dataType: 'json', timeout: 10000,
                        async: false,
                        url: location.href,
                        data: {
                            action: "FrozenAdmin",
                            uid: id,
                            state: state
                        },
                        success: function (resultData) {
                            if (resultData.success == true) {
                                alert(resultData.msg);
                                location.reload();
                              

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
