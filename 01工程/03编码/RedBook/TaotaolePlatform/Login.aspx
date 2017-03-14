<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="RedBookPlatform.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
	<meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
	<title>爽乐购后台登录</title>
	<meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">

	<link rel="stylesheet" type="text/css" href="/Resources/css/comm.css"/>

    <script src="/Resources/js/jquery.min.js" type="text/javascript" charset="utf-8"></script>
    <script src="/Resources/js/js.KinerCode.js"></script>
</head>
    <style type="text/css">
         body { background-color:#ebebeb
    }
   .top_div{
	background: #008ead;
	width: 100%;
	height: 400px;
}
         .checkbox.inline{
          margin-left:25px;
         }

    </style>
<body>
    <div class="top_div"></div>
    <form id="form1" runat="server" >

    <div class="login" >
        <div class="navbar">
            <div class="navbar-inner">
                <h6><i class="icon-user"></i><span style="color:#ff5722">爽乐购</span>管理员登录</h6>
            </div>
        </div>
        <div class="well">
            <form action="index.html" class="row-fluid">
                <div class="control-group">
                    <label class="control-label">用户名:</label>
                    <div class="controls"><input class="span12" type="text" name="regular" id="username" placeholder="Username" /></div>
                </div>
                
                <div class="control-group">
                    <label class="control-label">密码:</label>
                    <div class="controls"><input class="span12" type="password" name="pass" id="userpass" placeholder="Password" /></div>
                </div>

                <div class="control-group" style="margin-bottom:10px;">
                     <label class="control-label">验证码:</label>
                    <div class="controls">
                        <label class="checkbox inline">
                        <div class="checker" id="uniform-undefined">
                              <span class="checked"><input type="checkbox" id="checked" name="checkbox1" class="styled" value="" checked="checked" style="opacity: 0;" /></span>
                               </div>记住密码
                        </label> 
                        <input id="inputCode" type="text" style="width:130px;height:35px; float:left; margin-right:30px"/> 
                        <span id="code" class="mycode" style="float:left; display:block;width: 80px;height: 30px;vertical-align: middle;"></span>

                    </div>                      
                </div>
                <div class="login-btn"><input type="button" onclick="javascript:login()" value="登    陆" class="btn btn-danger btn-block" /></div>
            </form>
        </div>
        <span style="color:red;">为了您体验方便快速，请使用IE6以上版本的浏览器</span>
    </div>
        
    <script>

        document.onkeydown = function (event) {
            var e = event || window.event || arguments.callee.caller.arguments[0];
           
            if (e && e.keyCode == 13) { // enter 键
                login();
            }
        };


        $(function () {
            $("label.checkbox").click(function () {
                $(this).find("span").toggleClass('checked');
            });
        });
        function login() {


            if (yz == 1) {
                var okjzmm = 0;
                if ($("#checked").prop('checked')) {
                    //如果是1 则记住密码
                    okjzmm = 1;
                }
                else {
                    //如果是二 则不记住密码
                    okjzmm = 2;
                }

                $.ajax({
                    type: 'post', dataType: 'json', timeout: 10000,
                    async: false,
                    url: location.href,
                    data: {
                        action: "login",
                        username: $("#username").val(),
                        userpass: $("#userpass").val(),
                        jzmm: okjzmm
                    },
                    success: function (resultData) {

                        if (resultData.success == true) {
                            //window.location.href = 'index.aspx';
                            parent.window.location.href = 'index.aspx';

                        }
                        if (resultData.success == false) {
                            alert(resultData.msg);
                            c.refresh();
                            $("#inputCode").val("");
                        }
                    }
                })
            }
            else {
                alert("验证码错误");
                $("#inputCode").val("");
            }
        }
        var yz = 0;
        var inp = document.getElementById('inputCode');
        var code = document.getElementById('code');
        var inp2 = document.getElementById('inputCode2');
        var code2 = document.getElementById('code2');
        var c = new KinerCode({
            len: 4,//需要产生的验证码长度
            //        chars: ["1+2","3+15","6*8","8/4","22-15"],//问题模式:指定产生验证码的词典，若不给或数组长度为0则试用默认字典
            chars: [
                1, 2, 3, 4, 5, 6, 7, 8, 9, 0,
                'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
                'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'
            ],//经典模式:指定产生验证码的词典，若不给或数组长度为0则试用默认字典
            question: false,//若给定词典为算数题，则此项必须选择true,程序将自动计算出结果进行校验【若选择此项，则可不配置len属性】,若选择经典模式，必须选择false
            copy: false,//是否允许复制产生的验证码
            bgColor: "",//背景颜色[与背景图任选其一设置]
            bgImg: "bg.jpg",//若选择背景图片，则背景颜色失效
            randomBg: false,//若选true则采用随机背景颜色，此时设置的bgImg和bgColor将失效
            inputArea: inp,//输入验证码的input对象绑定【 HTMLInputElement 】
            codeArea: code,//验证码放置的区域【HTMLDivElement 】
            click2refresh: true,//是否点击验证码刷新验证码
            false2refresh: true,//在填错验证码后是否刷新验证码
            validateEven: "blur",//触发验证的方法名，如click，blur等
            validateFn: function (result, code) {//验证回调函数
                if (result) {
                    yz = 1;
                } else {
                    
                    if (this.opt.question) {
                        yz = 2;
                    } else {
                        yz = 2;
                    }
                }
            }
        });
    </script>
        
    </form>
</body>
</html>
