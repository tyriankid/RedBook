<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.winning.aspx.cs" Inherits="RedBook.index_winning" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8">
    <meta http-equiv="x-ua-compatible" content="ie=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, user-scalable=no, maximum-scale=1.0"/>
    <meta content="yes" name="apple-mobile-web-app-capable" />
    <meta content="black" name="apple-mobile-web-app-status-bar-style" />
    <meta content="telephone=no" name="format-detection" />
    <title>100%中奖</title>
    <link rel="stylesheet" href="css/comm.css">
    <link rel="stylesheet" href="css/reset.css">
    <link rel="stylesheet" href="css/index.nav.css">
    <script src="/js/JQuery.js"></script>
    <script src="/js/index.nav.js"></script>
</head>
<body style="background-color: #FFEFDF;">
    <header class="header">
        <h2>赚钱攻略</h2>
        <a id="fanhui" class="cefenlei" onclick="history.go(-1)" href="javascript:;">
            <img width="30" height="30" src="/images/fanhui.png">
        </a>
        <div class="head-r"><a href="/index.aspx" class="fr_Home"></a></div>
    </header>
   <%-- <header>
        <div><span onclick="history.go(-1)">关闭</span><h3>赚钱攻略</h3></div>
    </header>--%>
    <img src="images/winning-top.gif" />
    <section class="wrap">
        <div class="routine">
            <h3>具体步骤如下：</h3>
            <p>立即充值<b>5元送</b>超值<b>红包</b></p>
            <p><b>抽话费</b>不中包赔<i>(不中返满5减2和满10减3红包)</i></p>
            <a href="/WxZhuanti/shouchong5.aspx">立即购买</a>
        </div>
    </section>

    <section class="wrap title">
        <span>从五元爽乐币开始</span>
    </section>
    <section class="wrap commodity">
        <a href="/index.item.aspx?productid=3">
            <img src="images/card-yd20.png" />
            <%--<img src="images/card-jd5.jpg" />--%>
            <div>
                <p class="oneline">使用<span>“满5减2红包活动“</span></p>
                <%--<p>使用<span>“满5减2红包活动“</span>，没中奖用户</p>
                <p>立即参加<span>“满10减3红包活动”</span></p>--%>
            </div>
            <span>购买</span>
        </a>
    </section>

    <section class="wrap title">
        <span>若未获得：参与满10减3活动</span>
    </section>
    <section class="wrap arrows">
        <span><img src="images/arrows.png" /></span>
    </section>
    <section class="wrap commodity">
        <a href="/index.item.aspx?productid=3">
            <img src="images/card-yd20.png" />
            <div>
                <p>使用<span>"满10减3红包活动"</span>，没中奖用户</p>
                <p>立即参加<span>"充20送118元活动"</span></p>
            </div>
            <span>购买</span>
        </a>
    </section>

    <section class="wrap">
        <div class="routine">
            <h3>具体步骤如下：</h3>
            <p>立即充值<b>20元送</b>超值<b>红包</b></p>
            <p><b>参与</b>"充20送118元活动"</p>
            <a href="/WxZhuanti/20bian118.aspx">立即参与</a>
        </div>
    </section>

    
    <section class="wrap title">
        <span>获得红包继续参与</span>
    </section>
    <section class="wrap commodity">
        <a href="/index.item.aspx?productid=3">
            <img src="images/card-yd20.png" />
            <%--<img src="images/card-jd5.jpg" />--%>
            <div>
                <p>使用<span>“满5减2红包活动“</span>，没中奖用户</p>
                <p>立即参加<span>“满10减3红包活动”</span></p>
            </div>
            <span>购买</span>
        </a>
    </section>

    <section class="wrap title">
        <span>若未获得：参与满10减3活动</span>
    </section>
    <section class="wrap arrows">
        <span><img src="images/arrows.png" /></span>
    </section>
    <section class="wrap commodity">
        <a href="/index.item.aspx?productid=3">
            <img src="images/card-yd20.png" />
            <%--<img src="images/card-yd1000.jpg" />--%>
            <div>
                <p class="oneline">使用<span>"满10减3红包活动"</span></p>
            </div>
            <span>购买</span>
        </a>
    </section>

    <section class="wrap arrows">
        <span><img src="images/arrows.png" /></span>
    </section>

    <section class="wrap title gmstart">
        <span>如上步骤连续购买6次</span>
        <br />
        <span>赚钱的概率高达<b>99%</b></span>
        <br />
        <a href="/index.aspx">我要去赚钱</a>
    </section>
</body>
</html>
