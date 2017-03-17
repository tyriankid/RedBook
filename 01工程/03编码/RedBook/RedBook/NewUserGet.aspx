<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewUserGet.aspx.cs" Inherits="RedBook.NewUserGet" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <META HTTP-EQUIV="pragma" CONTENT="no-cache"> 
    <META HTTP-EQUIV="Cache-Control" CONTENT="no-cache, must-revalidate"> 
    <META HTTP-EQUIV="expires" CONTENT="0">
    <meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no">
    <title></title>
    <style>
        html,body,#app{
            height:100%;
            overflow:hidden;
        }
        body{
            background-color:#f83e3f
        }
        *{
            padding:0;
            margin:0;
            box-sizing:border-box;
            -webkit-box-sizing:border-box;
        }
        img{
            width:100%;
            vertical-align:middle;
        }
        .content{
            position:relative;
            width:91.6%;
            height:100%;
            color:#880001;
            background-color:#fcf0c6;
            margin:0 auto;
            border-left: 1px solid #F83E3F;
        }
        .content div{
            position:absolute;
            left:0;
            top:18%;
            transform:translateY(-50%);
            -webkit-transform:translateY(-50%);
            width:100%;
            padding:1rem 2rem;
            z-index:2;
        }
        .content>div>span{
            display:block;
            padding:0.75rem;
            margin-top:0.5rem;
            text-align:center;
            font-weight:bold;
            font-size:1.2rem;
            border-radius:0.35rem;
            background-color:#fdd11c;
        }
        .content>div>p{
            font-size:0.8rem;
            padding:0.1rem 0;
            line-height:1rem;
        }
        @media screen and (max-width: 414px) {
            .content>div>p{
                font-size:1rem;
            }
        }
        @media screen and (max-width: 375px) {
            .content>div>p{
                font-size:0.9rem;
            }
        }
        @media screen and (max-width: 320px) {
            .content>div>p{
                font-size:0.8rem;
            }
        }
        .bottom{
            position:fixed;
            bottom:0;
            left:0;
            width:100%;
        }
        .mask{
            position:fixed;
            top:0;
            left:0;
            right:0;
            bottom:0;
            width:100%;
            height:100%;
            background: rgba(0, 0, 0, 0.6);
            z-index:2;
        }
        .dialog{
            position:fixed;
            top: 50%;
            left:0;
            -webkit-transform: translateY(-50%);
            transform: translateY(-50%);
            width:100%;
            z-index:3;
        }
        .dialog div{
            text-align:center;
            width:80%;
            border-radius:0.35rem;
            margin:0 auto;
            background: -webkit-radial-gradient(#fff 60%, #DEDEDE);
            /* Safari 5.1 - 6.0 */
            background: -o-radial-gradient(#fff 60%, #DEDEDE);
            /* Opera 11.6 - 12.0 */
            background: -moz-radial-gradient(#fff 60%, #DEDEDE);
            /* Firefox 3.6 - 15 */
            background: radial-gradient(#fff 60%, #DEDEDE);
            /* 标准的语法 */
        }
        .dialog div p:first-child{
            font-size:1rem;
            line-height:3rem;
            border-bottom:1px solid #e0e0e0;
        }
        .dialog div p:last-child{
            padding:2rem 0;
            color:#666;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server"></form>
    <section id="app">
        <img src="/images/newUserGet_01.gif" />
        <img src="/images/newUserGet_02.gif" />
        <img src="/images/newUserGet_03.gif" />
        <img src="/images/newUserGet_04.gif" />
        <img src="/images/newUserGet_05.gif" />
        <div class="content">
            <div>
                <p>使用说明：</p>
                <p>1、本红包仅限年会期间，由参会嘉宾用于体验一元购</p>
                <p>2、200金币为虚拟货币，不可提现或转给他人使用</p>
                <p>3、1个金币会产生1个积分，积分可用于抽奖</p>
                <span v-on:click="get">点击领取</span>
            </div>
        </div>
        <div class="bottom">
            <img src="/images/newUserGet_06.gif" />
            <img src="/images/newUserGet_07.gif" />
        </div>
        <div class="mask" v-if="mask" v-on:click="hide"></div>
        <div class="dialog" v-if="mask">
            <img src="/images/newUserGet_08.png" v-if="dialogImg" />
            <div v-if="!dialogImg">
                <p>提示</p>
                <p>{{ msg }}</p>
            </div>
        </div>
    </section>
    <script src="/js/vue.min.js"></script>
    <script src="/js/vue-resource.min.js"></script>
    <script>
        var vm = new Vue({
            el: "#app",
            data: {
                mask: false,
                dialogImg: false,
                msg:'加载中'
            },
            methods: {
                get: function () {
                    var ajaxUrl = "http://" + window.location.host + "/ilerruser?action=activityMember";
                    this.$http.get(ajaxUrl).then(function (response) {
                        console.log(response.data);
                        //alert(response.data.state);
                        var _this = this;
                        if (response.data.state == "4") {
                            _this.mask = _this.dialogImg = true;
                        } else if (response.data.state == "3") {
                            _this.msg = "您已参与此活动了"
                            _this.mask = true;
                            _this.dialogImg = false;
                        } else {
                            _this.msg = "活动异常"
                            _this.mask = true;
                            _this.dialogImg = false;
                        }
                    })
                },
                hide: function () {
                    this.mask = false;
                }
            }
        })
    </script>
</body>
</html>
