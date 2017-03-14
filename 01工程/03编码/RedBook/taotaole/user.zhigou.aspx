<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="user.giftlist.aspx.cs" Inherits="RedBook.user_giftlist" %>
<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>
<%@ Register TagPrefix="UI" Namespace="Taotaole.Common" Assembly="Taotaole.Common" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%--<meta content="app-id=518966501" name="apple-itunes-app" />--%>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, user-scalable=no, maximum-scale=1.0" />
    <meta content="yes" name="apple-mobile-web-app-capable" />
    <meta content="black" name="apple-mobile-web-app-status-bar-style" />
    <meta content="telephone=no" name="format-detection" />
    <link href="css/comm.css" rel="stylesheet" type="text/css" />
    <script src="js/jquery190.js" type="text/javascript"></script>
    <script src="js/fastclick.js"></script>
    <style type="text/css">
        .mBuyRecord {
            clear: both;
            width: 100%;
            background: #F4F4F4;
            overflow: hidden;
        }
        .mBuyRecord ul {
            padding: 10px 15px 5px;
            position: relative;
            margin-top: -1px;
            margin-bottom: 15px;
            background: #fff;
        }
        .mBuyRecord li.mBuyRecordL {
            float: left;
            width: 80px;
        }
        .mBuyRecord li a{
            display:block;
        }
        .mBuyRecord li a img {
            width: 80px;
            height: 80px;
            display: inline-block;
            overflow: hidden;
        }
        .mBuyRecord li.mBuyRecordR {
            position: relative;
            margin-left: 90px;
            color: #666;
            line-height: 18px;
            text-align: left;
        }
        .mBuyRecord li.mBuyRecordR .title {
            color: #3C3E45;
            font-size: 16px;
            white-space: nowrap;
            text-overflow: ellipsis;
            overflow: hidden;
            padding-bottom: 10px;
        }
        .mBuyRecord li.mBuyRecordR a {
            color: #666;
        }
        .mBuyRecord li.mBuyRecordR p.txt {
            color: #A4A4A4;
            font-size: 13px;
            white-space: nowrap;
            text-overflow: ellipsis;
            overflow: hidden;
            padding-bottom: 10px;
        }
        .mBuyRecord li.mBuyRecordR p.txt b {
            float:right;
            color: #FD524E;
        }
        .mBuyRecord .mBuyRecordR .gongxi {
            height: 62px;
            width: 60px;
            margin-top: 15px;
            position: absolute;
            top: 0;
            right: 0;
            background: url(../images/discover/discoverIcon0.png) center center no-repeat;
            background-size: 60px;
            margin-bottom: 0;
        }
    </style>
</head>
<body>
    <header class="header">
        <h2>我的直购</h2>
        <a id="fanhui" class="cefenlei" onclick="history.go(-1)" href="javascript:;">
            <img width="30" height="30" src="/images/fanhui.png">
        </a>
        <div class="head-r"><a href="/index.aspx" class="fr_Home"></a></div>
    </header>
    <section class="mBuyRecord">
        <div class="clearfix" id="giftList">
            <%--<ul class="BuyRecordList" id="8">
                <li class="mBuyRecordL">
                    <a class="touxianga" href="/index.item.aspx?shopid=8">
                        <img src="">
                    </a>
                </li>
                <li class="mBuyRecordR">
                    <div onclick="window.location=&quot;/user.winningconfirm.aspx?shopid=8&amp;orderid=Y161130144600430190&amp;productid=1&quot;">
                        <div class="title"><a>ttttt</a></div>
                        <p class="txt">期号：2</p>
                        <p class="txt">本期参与：<b>6000</b>人次</p>
                        <span class="gongxi"></span>
                    </div>
                </li>
            </ul>--%>
        </div>
    </section>
    <form id="form1" runat="server">
    </form>
    <script type="text/javascript">

        $(function () {
            getQuanList();
        })

        //获取奖品列表
        function getQuanList() {
            var ajaxUrl = "http://" + window.location.host + "/ilerrorder?action=ZhiOrderList";
            $.ajax({
                type: 'get', dataType: 'json', timeout: 10000,
                url: ajaxUrl,
                success: function (data) {
                    console.log(data);
                    if (data.length > 0) {
                        for (var i = 0; i < data.length; i++) {
                            var html = '';
                            html += '<ul class="BuyRecordList" onclick="goConfirm(\'' + data[i].orderId + '\')">'
                            html += '<li class="mBuyRecordL">'
                            html += '<a class="touxianga" href="/index.item.aspx?shopid=8">'
                            html += '<img src="' + data[i].imgroot + data[i].thumb + '">'
                            html += '</a>'
                            html += '</li>'
                            html += '<li class="mBuyRecordR">'
                            html += '<div onclick="">'
                            html += '<div class="title">'
                            html += '<a>' + data[i].title + '</a>'
                            html += '</div>'
                            html += '<p class="txt">在线支付：' + parseInt(data[i].money) + '<b>' + data[i].status + '</b></p>'
                            html += '<p class="txt">' + data[i].time + '</p>'
                            html += '</div>'
                            html += '</li>'
                            html += '</ul>'
                            $(html).appendTo("#giftList");
                        }
                    }
                },
                error: function (e) {
                    //console.log(e);
                }
            });
        }

        function goConfirm(orderId) {
            window.location = "/user.quanconfirm.aspx?&orderid=" + orderId;
        }
    </script>
</body>
</html>
