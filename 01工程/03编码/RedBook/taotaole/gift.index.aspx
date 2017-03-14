<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="gift.index.aspx.cs" Inherits="RedBook.gift_index" %>

<!DOCTYPE html>
<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>积分商城</title>
    <META HTTP-EQUIV="pragma" CONTENT="no-cache"> 
    <META HTTP-EQUIV="Cache-Control" CONTENT="no-cache, must-revalidate"> 
    <META HTTP-EQUIV="expires" CONTENT="0">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, user-scalable=no, maximum-scale=1.0" />
    <meta content="yes" name="apple-mobile-web-app-capable" />
    <meta content="black" name="apple-mobile-web-app-status-bar-style" />
    <meta content="telephone=no" name="format-detection" />
    <link href="dist/css/export.css?v=201701050900" rel="stylesheet" type="text/css" /><%--intergral--%>
</head>
<body>
    <div id="app">
        <div class="loader" v-if="loading">
            <div class="loader-inner ball-spin-fade-loader">
              <div></div><div></div><div></div><div></div><div></div><div></div><div></div><div></div>
            </div>
        </div>
        <header-page-temp title="积分商城"></header-page-temp>
        <router-view></router-view>
    </div>
    <input id="currentScore" type="hidden" value="<%=currentScore%>" />

    <script type="text/javascript" src="dist/lib/require.js" data-main="dist/js/export.js?v=201701041738" charset="UTF-8"></script><%--config--%>
</body>

</html>
