<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="user.newcomer.aspx.cs" Inherits="RedBook.user_help" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8">
    <meta http-equiv="x-ua-compatible" content="ie=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, user-scalable=no, maximum-scale=1.0" />
    <meta content="yes" name="apple-mobile-web-app-capable" />
    <meta content="black" name="apple-mobile-web-app-status-bar-style" />
    <meta content="telephone=no" name="format-detection" />
    <title>新手入门</title>
    <link href="css/comm.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="css/reset.css">
    <link rel="stylesheet" href="css/index.nav.css">
    <script src="js/jquery.js"></script>
    <script src="js/index.nav.js"></script>
    <script src="/js/fastclick.js"></script>
    <script>
        window.onload = function () {
            FastClick.attach(document.body);
        }
    </script>
</head>
<body style="background-color: #f4f4f4;">
    <header class="header">
        <h2>新手入门</h2>
        <a class="cefenlei" onclick="history.go(-1)" href="javascript:;">
            <img width="30" height="30" src="/images/fanhui.png">
        </a>
        <div class="head-r"><a href="/index.aspx" class="fr_Home"></a></div>
    </header>
    <section class="wrap help-top">
        <ul>
            <li><span class="icon icon-gzgk"></span>公正公开</li>
            <li><span class="icon icon-zfbz"></span>支付保障</li>
            <li><span class="icon icon-zpbz"></span>正品保障</li>
        </ul>
    </section>
    <section class="hot-question">
        <h3>热门问题</h3>
        <ul>
            <li>
                <p>1.什么是爽乐购？<span class="icon hot-q-arrow"></span></p>
                <div>
                    <p>爽乐购是旨在为您和商家提供一元众筹服务模式的网络平台。</p>
                    <p>您花费1元获得1个宝珠同时获赠1个爽乐币，您可凭爽乐币参与相应数量人次的商品并获得相应人次数量的幸运码。商品的总需人次全部售出后，系统根据规则计算出的一个幸运号码。持有改幸运号码的用户可直接获得该商品。</p>
                </div>
            </li>
            <li>
                <p>2.怎样参加爽乐购？<span class="icon hot-q-arrow"></span></p>
                <div>
                    <p>（1）首先选择您想要的商品，然后选择您要参与的人次数（参与的人次越多，获得该商品的概率越大）进行参与；</p>
                    <p>（2）参与成功后，您会获得相应数量的参与号码。当该商品的参与人次数达到总需人次后，系统开始计算唯一的幸运号码；</p>
                    <p>（3）结果揭晓后，若您参与号码中有与该商品的幸运号码一致的号码，则您获得该商品</p>
                </div>
            </li>
            <li>
                <p>3.幸运号码如何产生？<span class="icon hot-q-arrow"></span></p>
                <div>
                    <p>（1）商品的最后一个号码认购完毕后，将公示截止该认购时间点前本站全部商品的最后100个记录的参与时间（包括该商品最后一个参与时间）；</p>
                    <p>（2）将这100条记录的时间按照时、分、秒、毫秒组合（如 19:23:54.124 则是192354124），然后相加，得到数组A；</p>
                    <p>（3）数值A除以商品总需人次数，得到一个余数，用这个余数加上原始数10000001，得到幸运号码。拥有该幸运号码者，获得该商品；</p>
                </div>
            </li>
            <li>
                <p>4.什么是爽乐币？<span class="icon hot-q-arrow"></span></p>
                <div>
                    <p>爽乐币是您支付人民币后获得、用户参与爽乐购的凭据。每充值1元可获得1个宝珠同时获赠1个爽乐币，您可凭爽乐币参与爽乐购</p>
                    <p>温馨提示：使用爽乐币进行支付更加快捷方便哦~</p>
                </div>
            </li>
            <li>
                <p>5.如何获得爽乐币？<span class="icon hot-q-arrow"></span></p>
                <div>
                    <p>您可在“爽乐购”页面或“我的”页面中找到充值按钮进行充值，充值后获得相应数量的爽乐币。</p>
                    <p>温馨提示：本平台不支持红包直接兑换成爽乐币。本平台不支持爽乐币与真实货币直接兑换</p>
                </div>
            </li>
            <li>
                <p>6.爽乐币和爽乐购红包能否提现？<span class="icon hot-q-arrow"></span></p>
                <div>
                    <p>很抱歉，爽乐币和爽乐购红包不支持提现。</p>
                </div>
            </li>
            <li>
                <p>7.怎么看我有没有获得商品？<span class="icon hot-q-arrow"></span></p>
                <div>
                    <p>在您所参与的商品幸运号码公布后，您可以点击底部切换按钮进入“爽乐购”页面查询结果；此外如您获得商品，将会收到系统的推送消息。</p>
                </div>
            </li>
            <li>
                <p>8.获得商品后我需要做什么？<span class="icon hot-q-arrow"></span></p>
                <div>
                    <p>获得商品后，您会收到客户端的推送通知。</p>
                    <p>若您获得实物类商品，您需要进入商品确认页填写或确认收货地址。确认完毕后，我们会尽快给您安排快递配送；</p>
                    <p>若您获得虚拟类商品（如话费充值卡），请进入商品确认页查看相应的卡号密码，然后进入相应网站进行操作即可。</p>
                    <p>温馨提示：虚拟类商品存在有效期限制，获得商品后请尽快使用</p>
                </div>
            </li>
            <li>
                <p>9.商品破损，错发怎么办？<span class="icon hot-q-arrow"></span></p>
                <div>
                    <p>如您还未签收商品，请您直接拒收退回，并告知客服MM您的账号、商品运单号，我们会及时联系商家和快递公司核实商品情况，并给您补发商品</p>
                    <p>如您已签收快递，请您提供商品详情描述及照片给客服MM，我们会在1-2个工作日内告知您商品换货方式，请您留意电话或短信通知</p>
                </div>
            </li>
            <li>
                <p>10.获得的商品如果保修？<span class="icon hot-q-arrow"></span></p>
                <div>
                    <p>爽乐购所有商品均是从正规商家提供，100%正品保证，可享受厂家所提供的全国联保服务。如商品遇到质量问题，建议您第一时间联系该商品的官方指定售后维修点</p>
                    <p>若您遇到无法自行联系处理的问题，可联系客服MM协助处理。如需联系客服MM协助处理，请您提供您的账号、商品名称、详情描述、售后检测报告及相关图片给客服MM，我们会及时跟进处理，1-2个工作日内会电话或短信通知您核实处理结果。</p>
                </div>
            </li>
            <li>
                <p>11.用户昵称能否修改？<span class="icon hot-q-arrow"></span></p>
                <div>
                    <p>很抱歉，平台暂时不支持昵称修改。</p>
                </div>
            </li>
            <li>
                <p>12.所有活动及商品均与苹果公司无关<span class="icon hot-q-arrow"></span></p>
                <div>
                    <p>通过本软件所从事的任何活动及其获得的任何商品均与苹果公司(Apple Inc.)无关</p>
                    <p>苹果公司既不作为赞助商也不以任何形式参与</p>
                </div>
            </li>
        </ul>
    </section>
</body>
</html>
