<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="addadmin.aspx.cs" Inherits="RedBookPlatform.admin.addadmin" %>
<!DOCTYPE html>
<html>
	<head runat="server">
		<meta charset="UTF-8">
		<title>添加管理员</title>
		<link rel="stylesheet" type="text/css" href="/Resources/css/comm.css"/>
        <script src="../Resources/js/jquery.min.js"></script>
	</head>
	<body>

		<div class="wrapper">
			<div class="crumbs">
				<ul id="breadcrumbs" class="breadcrumb"> 
	                <li><a href="#">主页</a></li>
	                <li><a href="#">管理员管理</a></li>
	                <li class="active"><a href="#" title=""><%=active %>></a></li>
	            </ul>
			</div>
		    <div class="content">
		    	<table class="table table-striped">

                    <tbody>
                        <tr>
                            <td>用户名</td>
                            <td><input type="text" name="" id="username" value="<%=Showusername %>" style="width: 50%;" /></td>
                        </tr>
                       <%--关于 class="show" 为是否隐藏密码栏目，勿删--%>
                        <tr class="show">
                            <td>密码</td>
                            <td><input type="password" name="" id="userpass" value="" style="width: 50%;" /></td>
                        </tr>
                        <tr class="show">
                            <td>确认密码</td>
                            <td><input type="password" name="" id="reuserpass" value="" style="width: 50%;" /></td>
                        </tr>
                        <tr>
                            <td>E-Mail</td>
                            <td><input type="text" name="" id="useremail" value="<%=Showuseremail %>" style="width: 50%;" /></td>
                        </tr>
                        <tr>
                            <td>所属角色</td>
                            <td>
                                <select name="">
		    				        <option value="">超级管理员</option>
		    				        <option value="">普通管理员</option>
		    			        </select>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <input type="button" name="name" id="bcbtn" onclick="javascript: holdclick()" value="保存" class="btn btn-primary" />
                            </td>
                        </tr>
                    </tbody>
                </table>
		    </div>
		</div>

        <script type="text/javascript">
            function holdclick() {
                if ($("#useremail").val() != "")
                {
                    var reg = /^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$/;
                    if (!reg.test($("#useremail").val())) {
                        alert("邮箱格式不对");
                        return false;
                    }
                }
                if ($("#username").val() == "") {
                    $("#username").focus();
                    alert("用户名不能为空！");
                    return;
                };
                if ($("#reuserpass").val() != $("#userpass").val()) {
                    alert("两次输入密码不一样！");
                    return;
                };
                $.ajax({
                    type: 'post', dataType: 'json', timeout: 10000,
                    async: false,
                    url: location.href,
                    data: {
                        action: "hold",
                        username: $("#username").val(),
                        userpass: $("#userpass").val(),
                        useremail: $("#useremail").val()
                    },
                    success: function (resultData) {
                        if (resultData.success == true) {
                         
                            alert(resultData.msg);
                            location.href = 'adminlist.aspx'
                        }
                        if (resultData.success == false) {
                            alert(resultData.msg);
                        }
                    }
                })
            };
            $(function () {
                if ($("#useremail").val() != "" || $("#username").val() != "") {
                    $(".show").hide();
                    $("#username").attr('disabled', 'true');
                }
                else {
                    $(".show").show();
                }

            });
        </script>
	</body>
</html>
