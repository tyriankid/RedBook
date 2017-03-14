<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="user.help.aspx.cs" Inherits="RedBook.user_help1" %>


<html xmlns="http://www.w3.org/1999/xhtml">




<head>
    <meta charset="utf-8">
    <meta http-equiv="x-ua-compatible" content="ie=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, user-scalable=no, maximum-scale=1.0" />
    <meta content="yes" name="apple-mobile-web-app-capable" />
    <meta content="black" name="apple-mobile-web-app-status-bar-style" />
    <meta content="telephone=no" name="format-detection" />
    <title>常见问题</title>
    <link href="css/comm.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="css/reset.css">
    <link rel="stylesheet" href="css/index.nav.css">
    <script src="js/jquery.js"></script>
    <script src="js/index.nav.js"></script>
    <script src="/js/fastclick.js"></script>
    <script>
        $(function () {
            FastClick.attach(document.body);
        })
    </script>
</head>
<body style="background-color: #f4f4f4;">
    <header class="header">
        <h2>常见问题</h2>
        <a class="cefenlei" onclick="history.go(-1)" href="javascript:;">
            <img width="30" height="30" src="/images/fanhui.png">
        </a>
        <div class="head-r"><a href="/index.aspx" class="fr_Home"></a></div>
    </header>

    <section class="hot-question">
        <ul>
            <li>
                <p>1.获得商品后多久发货？<span class="icon hot-q-arrow"></span></p>
                <div>
                    <p>有“现货”标志的商品，在您确认收货地址后，会在1-7个工作日内发出；</p>
                    <p>有“预售”标志的商品，在您确认收货地址并且网站到货后，会第一时间为您发货；</p>
                    <p>商品发出后您可以在商品确认页查看物流信息</p>
                </div>
            </li>
            <li>
                <p>2.快递费由谁支付？<span class="icon hot-q-arrow"></span></p>
                <div>
                    <p>所有大陆地区均免费配送，您不需要再支付任何费用</p>
                </div>
            </li>
            <li>
                <p>3.安排什么快递配送？<span class="icon hot-q-arrow"></span></p>
                <div>
                    <p>默认选择顺丰快递为您配送，部分无法送达的地区会根据情况为您选择其他快递公司进行配送；</p>
                    <p>其中部分包含电池的商品则按照国家相关规定选择顺丰陆地运输服务为您配送，以确保商品尽快来带您的身边。</p>
                    <p>温馨提示：部分偏远地区快递配送时间较长，请您耐心等待。</p>
                </div>
            </li>
            <li>
                <p>4.怎么查询配送进度？<span class="icon hot-q-arrow"></span></p>
                <div>
                    <p>商品发出后您可以通过商品确认页中的快递单号进行查询，了解当前配送进度</p>
                    <p>新疆、西藏等地区尽支持EMS配送，事件较长，请您耐心等待。</p>
                    <p>温馨提示：快递单属于个人隐私，请妥善保管，避免泄露。</p>
                </div>
            </li>
            <li>
                <p>5.签收要注意什么？<span class="icon hot-q-arrow"></span></p>
                <div>
                    <p>签收时请当面拆箱验货并本人签收，仔细检查商品（如：外包装是否开封、是否破损、配件是否缺失、功能是否正常）。确保无误后再签收，以免产生不必要的纠纷。</p>
                    <p>如果发现运输途中造成商品损坏，请勿签收，直接拒签退回。</p>
                    <p>温馨提示：签收过程中产生任何疑问，请及时拨打客服电话反馈，用户签收后即认可商品完好无损。若签收后产生纠纷，1元购仅承担协调处理的义务。</p>
                </div>
            </li>
            <li>
                <p>6.收到的商品可以退还吗？<span class="icon hot-q-arrow"></span></p>
                <div>
                    <p>非质量问题，不在三包范围内，不给予退换货。</p>
                </div>
            </li>
            <li>
                <p>7.长时间未收到商品？<span class="icon hot-q-arrow"></span></p>
                <div>
                    <p>请确保您所填写的收货地址、电话、邮编等信息的准确性；</p>
                    <p>配送过程中请保证联系方式畅顺无阻，如联络您的时间超过7天，默认您放弃次商品。</p>
                </div>
            </li>
            <li>
                <p>8.写错地址没收到货？<span class="icon hot-q-arrow"></span></p>
                <div>
                    <p>若因地址填写错误、联系方式填写错误等情况造成商品无法完成投递或被退回，所产生的额外费用及后果由用户负责</p>
                </div>
            </li>
            <li>
                <p>9.其他？<span class="icon hot-q-arrow"></span></p>
                <div>
                    <p></p>
                    <p>如因不可抗拒的自然原因，如地震、洪水等，所造成的商品配送延迟，爽乐购所属贝格互动不承担责任。</p>
                </div>
            </li>
        </ul>
    </section>
</body>
</html>

