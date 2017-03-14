<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="gift.list.aspx.cs" Inherits="RedBook.gift_list" %>

<!DOCTYPE html>
<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>积分商城</title>
    <meta content="app-id=518966501" name="apple-itunes-app" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, user-scalable=no, maximum-scale=1.0" />
    <meta content="yes" name="apple-mobile-web-app-capable" />
    <meta content="black" name="apple-mobile-web-app-status-bar-style" />
    <meta content="telephone=no" name="format-detection" />
    <link href="css/comm.css?v=20150129" rel="stylesheet" type="text/css" />
    <link href="css/goods.css" rel="stylesheet" type="text/css" />
    <link href="css/style.css" rel="stylesheet" type="text/css" />
    <script src="js/jquery190.js" type="text/javascript"></script>
    <script src="/js/fastclick.js"></script>
</head>

<body>
    <header class="header">
        <h2>积分商城</h2>
        <a class="cefenlei" onclick="history.go(-1)" href="javascript:;">
            <img width="30" height="30" src="images/fanhui.png">
        </a>
        <div class="head-r"><a href="/index.aspx" class="fr_Home"></a></div>
    </header>

	<!--商品列表 Start-->
	<div id="" class="giftsList">
		<div id="divGiftsLoading" class="loading" style="display:none;height: 80px;">
		</div>
	</div>

	<input id="pagenum" value="1" type="hidden" />
	<input id="pagesum" value="" type="hidden" />
	<!--商品列表 End-->

    <script type="text/javascript">
        $(function () {
            getGiftList();


        });


        var nextPage = 1;
        function getGiftList() {
            if (nextPage == 0) {
                return;
            }
            var ajaxUrl = "http://" + window.location.host + "/ilerrgift?action=getGiftListByNum&p=" + nextPage;
            $.ajax({
                type: 'get', dataType: 'json', timeout: 10000,
                url: ajaxUrl,
                success: function (e) {
                    nextPage = e.nextPage;

                    for (var i = 0; i < e.data.length; i++) {
                        var Datalist = '';
                        Datalist += '<ul>' +
                            '<li><span id="' + e.data[i].productid + '" class="z-Limg" style="position:relative;display:block;"><img src="' + e.imgroot + e.data[i].thumb + '"></span>' +
                                '<div style="margin-top:15px;" class="goodsListR">' +
                                    '<h2 id="' + e.data[i].productid + '">' + e.data[i].title + '</h2>' +
                                '</div>' +
                                '<div class="pRate item_bottom_container">' +
                                '</div>' +
                            '</li>' +
                        '</ul>';
                        $("#divGiftsLoading").before(Datalist);
                    }
                }
            });
        }
	</script>
</body>

</html>
