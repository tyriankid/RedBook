<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="gift.pao.aspx.cs" Inherits="RedBook.gift_index" %>

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
        

    <script type="text/javascript">
        $(function () {

            var ajaxUrl = "http://" + window.location.host + "/ilerrgift?action=getScoreNums";
            alert(ajaxUrl);
            $.ajax({
                type: 'get', dataType: 'json', timeout: 10000,
                url: ajaxUrl,
                success: function (e) {
                    alert(e);
                }
                
            });
        })
	</script>
</body>

</html>
