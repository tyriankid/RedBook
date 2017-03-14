<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="user.zengbi.aspx.cs" Inherits="RedBook.user_zengbi" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, user-scalable=no, maximum-scale=1.0" />
    <meta content="yes" name="apple-mobile-web-app-capable" />
    <meta content="black" name="apple-mobile-web-app-status-bar-style" />
    <meta content="telephone=no" name="format-detection" />
    <link href="css/comm.css" rel="stylesheet" type="text/css" />
    <script src="/js/jquery-1.8.2.js" language="javascript" type="text/javascript"></script>
    <script src="/js/fastclick.js"></script>
    <script src="/js/vue.min.js"></script>
    <script src="/js/vue-resource.min.js"></script>
    <style type="text/css">
        .record .myIntegral{
            background-color: #FD524E;
            color: #FFD100;
            line-height: 10rem;
            text-align: center;
            font-size: 3rem;
            border-radius: 0 0 50% 50%;
        }
        .record .tips{
            margin-top: 2rem;
            overflow:hidden;
        }
        .record .tips span{
            float:left;
            width:50%;
            text-align:center;
        }
        .record .tips span i{
            display: inline-block;
            width: 1.5rem;
            height: 1.5rem;
            vertical-align: middle;
            margin-right: 1rem;
        }
        .record .tips span:first-child i{
            background-color:#FD524E;
        }
        .record .tips span:last-child i{
            background-color:#02B76D;
        }
        .record ul{
            margin:20px 0;
        }
        .record ul li{
            overflow:hidden;
            text-align: center;
        }
        .record ul li span{
            float: left;
            width: 25%;
            font-size:0.8rem;
            line-height: 2.2rem;
            text-overflow: ellipsis;
            white-space: nowrap;
            overflow: hidden;
        }
        .record ul li span:first-child{
            width:50%;
        }
        .record ul li.liHeader{
            font-size:1rem;
        }
    </style>
</head>
<body>
    <header class="header">
        <h2>我的夺宝币</h2>
        <a id="fanhui" class="cefenlei" onclick="history.go(-1)" href="javascript:;">
            <img width="30" height="30" src="/images/fanhui.png">
        </a>
        <div class="head-r"><a href="/index.aspx" class="fr_Home"></a></div>
    </header>
    <section id="app" class="record">
        <div class="myIntegral">{{ myIntegral }}</div>
        <div class="tips"><span><i></i>消耗</span><span><i></i>获得</span></div>
        <ul>
	    <template v-if="records">
            <li class="liHeader">
                <span>商品</span><span>夺宝币</span><span>时间</span>
            </li>
            <li v-for="record in records" :style="green(record.type)">
                <span>{{ record.detail }}</span><span>{{ record.money }}</span><span>{{ record.usetime.substring(0,10) }}</span>
            </li>
	    </template>
        <template v-else>
            <li class="noReocrd">暂无兑换记录</li>
	    </template>
        </ul>
    </section>
    <form id="form1" runat="server"></form>
    <script type="text/javascript">
        var vm = new Vue({
            el: "#app",
            data: {
                myIntegral:0,
                records: null,
                green: function (type) {
                    var color = {};
                    if (type == 1) {
                        color = {
                            color: '#02B76D'
                        }
                    } else {
                        color = {
                            color: '#FD524E'
                        }
                    }
                    return color;
                }
            },
            created: function(){
                this.fetchData()
            },
            methods: {
                fetchData: function () {
                    var ajaxUrl = "http://" + window.location.host + "/ilerrbuy?action=zengPointDetail";
                    this.$http.get(ajaxUrl).then(function (response) {
                        this.myIntegral = response.data[0].zenPoint;
                        this.records = response.data;
                    });
                }
            }
        })
        window.onload = function () {
            FastClick.attach(document.body);
        }
    </script>
</body>
</html>
