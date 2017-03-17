<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="wx_Submit.aspx.cs" Inherits="RedBook.WxPay.wx_Submit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, user-scalable=no, maximum-scale=1.0" />
    <script src="/js/jquery190.js" language="javascript" type="text/javascript"></script>
    <script src="/js/guanzhu.js?v=1.2" language="javascript" type="text/javascript"></script>
    <link href="/css/comm.css" rel="stylesheet" type="text/css">
    <title>正在玩命支付中...</title>
</head>
<body>
<script type="text/javascript">

    //调用微信JS api 支付
    function jsApiCall()
    {
        WeixinJSBridge.invoke('getBrandWCPayRequest',<%=pay_json%>,//josn串
                    function (res){
                        WeixinJSBridge.log(res.err_msg);
                        //alert(res.err_code + res.err_desc + res.err_msg);
                        var businessType = "<%=businessType%>";
                        if(res.err_msg == "get_brand_wcpay_request:ok" ) {
                            //支付成功进行相关的后续业务逻辑处理
                            switch(businessType){
                                case"add"://充值
                                    alert("支付成功！");
                                    if(!GuanzhuRemind()) return;
                                    location.href = getUrlOK(businessType);//充值成功后跳转
                                    break;
                                case "yuan"://一元购
                                    alert("支付成功！");
                                    yuanGo();
                                    break;
                                case "tuan"://团购
                                    alert("支付成功！");
                                    tuanGo();
                                    break;
                                case "quan"://直购
                                    alert("支付成功！");
                                    quanGo();
                                    break;
                                default:
                                    alert("支付成功！");
                                    if(!GuanzhuRemind()) return;
                                    location.href = getUrlOK(businessType);//"/community.index.aspx";//其他状态后跳转
                                    break;
                            }
                        }
                        else if(res.err_msg == "get_brand_wcpay_request:cancel"){//取消支付后
                            //location.href = "cart.paycancel.aspx";
                            location.href = getUrlCancel(businessType);
                        }
                        else
                        {
                            location.href = getUrlCancel(businessType);//"cart.payError.aspx";
                        }
                    });

        var yuanGo = function yuanGo(){
            $.getJSON('http://' + window.location.host + '/ilerrpay?action=userpaymentsubmit&shopid=<%=shopid%>&productid=<%=productid%>&quantity=<%=quantity%>&paytype=0&redpaper=<%=redpaper%>&APIsubmitcode=0', function (data) {
                switch (data.state) {
                    case 0://验证通过
                        if("<%=otype%>"!="0")
                        {
                            location.href = "/WxZhuanti/Wangba.aspx";//后续若有其它业务需要通过商品原始ID进行支付，可扩冲一个类型变量实现无限扩展
                            return;
                        }
                        if(!GuanzhuRemind()) return;
                            location.href = getUrlOK("yuan");//"/WxPay/cart.paysuccess.aspx";
                        break;
                    default:
                        alert("一元购订单产生错误,错误码:"+data.state +",支付成功的爽乐币已经加入账户余额!");
                        location.href = getUrlCancel("yuan");
                        break;
                }
            });
        }

        var quanGo = function quanGo(){
            $.getJSON("http://" + window.location.host + '/ilerrpay?action=userpaymentQuanSubmit&quanid=<%=quanId%>&redpaper=<%=redpaper%>&paytype=' + 0 + '&APIsubmitcode=0', function (data) {
                    switch (data.state) {
                        case 0://验证通过
                        if(!GuanzhuRemind()) return;
                        location.href = "/user.quanconfirm.aspx?orderid="+data.orderid;
                    break;
                    default:
                        alert("直购订单产生错误,错误码:"+data.state +",支付成功的爽乐币已经加入账户余额!");
                        location.href = getUrlCancel("quan");
                    break;
                }
            });
                }



        var tuanGo = function tuanGo(){
            $.getJSON('http://' + window.location.host + '/ilerrpay?action=userpaymenttuansubmit&typeid=<%=tuanType%>&businessid=<%=businessid%>&tuanorderid=<%=tuanorderid%>', function (data) {
                switch (data.state) {
                    case 0://验证通过
                        //getUrlOK("tuan");
                        //location.href = "/WxPay/cart.paysuccess.aspx";
                        //alert(GuanzhuRemind());
                        if(!GuanzhuRemind()) return;
                            location.href='/WxTuan/tuan.join.aspx?tid=' + data.tid + '&tuanlistId=' + data.tuanlistId + '&share=0';
                        break;
                    default:
                        alert("团订单产生错误,错误码:"+data.state +",爽乐购会将您支付的金额退回!");
                        location.href = getUrlCancel("tuan");//"cart.payError.aspx";
                        break;
                }
            });
        }
    }

    //获取(成功)不同类型的跳转地址
    function getUrlOK(type)
    {
        var tzUrl="/index.aspx";
        switch(type){
            case "add":
                tzUrl="/community.index.aspx";
                break;
            case "tuan":
                tzUrl="/WxTuan/tuan.index.aspx";
                break;
            case "yuan":
                location.href="/index.aspx";
                break;
        }
        return tzUrl;
    }
    //获取(取消)不同类型的跳转地址
    function getUrlCancel(type)
    {
        var tzUrl="/index.aspx";
        switch(type){
            case "add":
                tzUrl="/user.userbalance.aspx?showzhifu=0";
                break;
            case "tuan":
                tzUrl="/WxTuan/tuan.index.aspx";
                break;
            case "yuan":
                location.href="/index.aspx";
                break;
        }
        return tzUrl;
    }

    function callpay()
    {
        
        if (typeof WeixinJSBridge == "undefined")
        {
            if (document.addEventListener)
            {
                document.addEventListener('WeixinJSBridgeReady', jsApiCall, false);
            }
            else if (document.attachEvent)
            {
                document.attachEvent('WeixinJSBridgeReady', jsApiCall);
                document.attachEvent('onWeixinJSBridgeReady', jsApiCall);
            }
        }
        else
        {
            jsApiCall();
        }
    }
    callpay();
     </script>
</body>
</html>
