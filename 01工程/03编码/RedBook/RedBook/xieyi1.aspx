﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="xieyi1.aspx.cs" Inherits="RedBook.xieyi1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, user-scalable=no, maximum-scale=1.0" />
    <title>爽乐购众筹平台用户协议</title>
    <meta content="app-id=518966501" name="apple-itunes-app" />
    <meta content="yes" name="apple-mobile-web-app-capable" />
    <meta content="black" name="apple-mobile-web-app-status-bar-style" />
    <meta content="telephone=no" name="format-detection" />
    <link href="css/comm.css" rel="stylesheet" type="text/css" />
    <link href="css/lottery.css" rel="stylesheet" type="text/css" />
    <link href="css/winning.css" rel="stylesheet" type="text/css" />
    <script src="js/jquery190.js" language="javascript" type="text/javascript"></script>
    <script src="/js/fastclick.js"></script>
    <style>
        h3{
            text-align:center;
            margin:10px 0;
            color: #797979;
        }
        p{
            line-height:28px;
            text-indent:2em;
            margin-top:10px;
            padding:0 10px;
            color:#929292;
        }
    </style>
</head>
<body style="background:#FFF;">
    <form id="form1" runat="server">
    <header class="header">
        <h2>用户协议</h2>
        <a id="fanhui" class="cefenlei" onclick="history.go(-1)" href="javascript:;">
            <img width="30" height="30" src="images/fanhui.png">
        </a>
        <div class="head-r"><a href="javascript:;" class="fr_Xieyi" onclick="showDiv()"></a></div>
    </header>
        <h3>《爽乐购众筹平台用户协议》</h3>
        <p>欢迎访问贝格互动（武汉）有限公司（以下简称“贝格互动”）提供的爽乐购众筹平台，为明确您（以下简称为“用户”）的权利义务，保护用户的合法权益，特制定本协议。申请使用贝格互动提供的爽乐购众筹平台（以下简称为“平台“）服务（包括一元爽乐购和拼团服务），请您仔细阅读以下全部内容<strong>（特别是粗体下划线标注的内容）</strong>。如您不同意本服务条款任意内容，请勿注册或使用平台服务。如您通过进入注册程序并勾选“我同意平台服务协议”，即表示您与贝格互动公司已达成协议，自愿接受本服务条款的所有内容。此后，您不得以未阅读本服务条款内容作任何形式的抗辩。</p>
        <p>鉴于：平台以“众筹”模式为各类商品的销售提供网络空间。在本平台，商品被平分成若干等分，用户可以使用爽乐币支持一份或多份，当等份全部售完后，由系统根据平台规则计算出最终获得商品或服务的用户，其他用户则可获得相应的“宝珠”。</p>
        <p><strong>一、用户使用爽乐购平台服务的前提条件</strong></p>
        <p>1、用户拥有贝格互动公司认可的帐号，包括但不限于：</p>
        <p>（1）贝格互动帐号（手机号码/邮箱注册），用户通过贝格互动帐号使用平台服务。</p>
        <p>（2）第三方帐号，用户可使用QQ帐号、微信帐号、微博帐号等其他贝格互动公司认可的帐号在同意本服务条款后使用爽乐购平台服务。</p>
        <p>2、用户在使用平台服务时须具备相应的权利能力和行为能力，能够独立承担法律责任，如果用户在18周岁以下，必须在父母或监护人的监护参与下才能使用本站。</p>
        <p><strong>二、用户管理</strong></p>
        <p>1、用户ID</p>
        <p>用户首次登录平台时，平台会为每位用户生成一个帐户ID，作为其使用平台服务的唯一身份标识，用户需要对其帐户项下发生的所有行为负责。</p>
        <p>2、用户资料完善</p>
        <p>用户应当在使用平台服务时完善个人资料，用户资料包括但不限于个人手机号码、收货地址、帐号昵称、头像、密码、注册或更新贝格互动帐号时输入的所有信息。</p>
        <p>用户在完善个人资料时承诺遵守法律法规、社会主义制度、国家利益、公民合法权益、公共秩序、社会道德风尚和信息真实性等七条底线，不得在资料中出现违法和不良信息，且用户保证其在完善个人资料和使用帐号时，不得有以下情形：</p>
        <p>（1）违反宪法或法律法规规定的；</p>
        <p>（2）危害国家安全，泄露国家秘密，颠覆国家政权，破坏国家统一的；</p>
        <p>（3）损害国家荣誉和利益的，损害公共利益的；</p>
        <p>（4）煽动民族仇恨、民族歧视，破坏民族团结的；</p>
        <p>（5）破坏国家宗教政策，宣扬邪教和封建迷信的；</p>
        <p>（6）散布谣言，扰乱社会秩序，破坏社会稳定的</p>
        <p>（7）散布淫秽、色情、赌博、暴力、凶杀、恐怖或者教唆犯罪的；</p>
        <p>（8）侮辱或者诽谤他人，侵害他人合法权益的；</p>
        <p>（9）含有法律、行政法规禁止的其他内容的。若用户提供给贝格互动公司的资料不准确，不真实，含有违法或不良信息的，贝格互动公司有权不予完善，并保留终止用户使用平台服务的权利。若用户以虚假信息骗取帐号ID或帐号头像、个人简介等注册资料存在违法和不良信息的，贝格互动公司有权采取通知限期改正、暂停使用、注销登记等措施。对于冒用关联机构或社会名人注册帐号名称的，贝格互动公司有权注销该帐号，并向政府主管部门进行报告。根据相关法律、法规规定以及考虑到爽乐购平台服务的重要性，用户同意：</p>
        <p>（1）在完善资料时提交个人有效身份信息进行实名认证；</p>
        <p>（2）提供及时、详尽及准确的用户资料；</p>
        <p>（3）不断更新用户资料，符合及时、详尽准确的要求，对完善个人资料时填写的身份证件信息不能更新。</p>
        <p>（4）用户有证明该帐号为本人所有的义务，需能提供贝格互动账号注册资料或第三方平台注册资料以证明该帐号为本人所有，否则贝格互动公司有权暂缓向用户交付其所获得的商品。</p>
        <p>3、爽乐币及宝珠</p>
        <p>（1）用户兑换爽乐币并使用后可根据宝珠获得规则获取相应的宝珠。爽乐币的有效期自兑换之日起算360天，有效期不可中断或延期，有效期届满后，用户帐户中有效期届满的爽乐币将被清空，且不可恢复。宝珠自获取之日起生效，使用期限将在宝珠规则中规定，详见平台页面提示。</p>
        <p>（2）爽乐币必须通过贝格互动公司提供或认可的平台获得，从非贝格互动公司提供或认可的平台所获得的爽乐币将被认定为来源不符合本服务协议，贝格互动公司有权拒绝从非贝格互动公司提供或认可的平台所获得的爽乐币在平台中使用。</p>
        <p>（3）爽乐币及宝珠不可兑回现金，不能在平台之外使用或者转移给其他用户。</p>
        <p>4、用户应当保证在使用平台服务的过程中遵守诚实信用原则，不扰乱平台的正常秩序，不得通过使用他人帐户、一人注册多个帐户、使用程序自动处理等非法方式损害他人或贝格互动公司的利益。</p>
        <p>5、若用户存在任何违法或违反本服务协议约定的行为，贝格互动公司有权视用户的违法或违规情况适用以下一项或多项处罚措施：</p>
        <p>（1）责令用户改正违法或违规行为；</p>
        <p>（2）中止、终止部分或全部服务；</p>
        <p>（3）取消用户爽乐购订单并取消商品发放（若用户已获得商品）， 且用户已获得的爽乐币不予退回；</p>
        <p>（4）冻结或注销用户帐号及其帐号中的爽乐币（如有）；</p>
        <p>（5）其他贝格互动公司认为合适在符合法律法规规定的情况下的处罚措施。</p>
        <p>若用户的行为造成贝格互动公司及其关联公司损失的，用户还应承担赔偿责任。</p>
        <p>6、若用户发表侵犯他人权利或违反法律规定的言论，贝格互动公司有权停止传输并删除其言论、禁止该用户发言、注销用户帐号及其帐号中的爽乐币（如有），同时，贝格互动公司保留根据国家法律法规、相关政策向有关机关报告的权利。</p>
        <p>三、爽乐购众筹平台服务的规则</p>
        <p>1、释义</p>
        <p>（1）爽乐币：指用户花费1元购买宝珠，并获赠1爽乐币，爽乐币可用于参与爽乐购平台活动。</p>
        <p>（2）爽乐购号码：指用户使用爽乐币参与平台服务时所获取的随机分配号码。</p>
        <p>（3）幸运号码：指与某件商品的全部爽乐购号码分配完毕后，平台根据爽乐购规则（详见爽乐购官方页面）计算出的一个号码。持有该幸运号码的用户可直接获得该商品。</p>
        <p>（4）宝珠：指用户充值后获得的相应回报。充值额度与获取宝珠比例为1：1，即充值1元可获得1宝珠。</p>
        <p>（5）爽乐购：指用户通过平台充值获得宝珠，并获赠爽乐币，然后凭爽乐币参与平台活动，根据活动规则获取相应商品的形式。</p>
        <p>2、贝格互动公司承诺遵循公平、公正、公开的原则运营平台，确保所有用户在平台中享受同等的权利与义务，爽乐购结果向所有用户公示。</p>
        <p>3、用户知悉，除本协议另有约定外，无论是否获得商品，用户用于参与平台活动的爽乐币不能退回；其完全了解参与平台活动存在的风险，贝格互动公司不保证用户参与爽乐购一定会获得商品，但参与后可根据宝珠规则获得相应的宝珠。</p>
        <p>4、用户通过参与平台活动获得商品后，应在7天内登录平台提交或确认收货地址，否则视为放弃该商品，用户因此行为造成的损失，贝格互动公司不承担任何责任。商品由贝格互动公司或经贝格互动公司确认的第三方商家提供及发货。</p>
        <p>5、用户通过参与平台活动获得的商品，享受该商品生产厂家提供的三包服务，具体三包规定以该商品生产厂家公布的为准。</p>
        <p>6、如果下列情形发生，贝格互动公司有权取消用户爽乐购订单：</p>
        <p>（1）因不可抗力、平台系统发生故障或遭受第三方攻击，或发生其他贝格互动公司无法控制的情形；</p>
        <p>（2）根据贝格互动公司已经发布的或将来可能发布或更新的各类规则、公告的规定，贝格互动公司有权取消用户订单的情形。</p>
        <p>贝格互动公司有权取消用户的订单时，用户可申请退还爽乐币，所退爽乐币将在3个工作日内退还至用户帐户中。</p>
        <p>7、若某件商品的爽乐购号码从开始分配之日起90天未分配完毕，则贝格互动公司有权取消该件商品的爽乐购活动，并向用户退还爽乐币，所退还爽乐币将在3个工作日内退还至用户帐户中。</p>
        <p>四、本服务协议的修改</p>
        <p>用户知晓贝格互动公司不时公布或修改的与本服务协议有关的其他规则、条款及公告等是本服务协议的组成部分。贝格互动公司有权在必要时通过在爽乐购平台内发出公告等合理方式修改本服务协议，用户在享受各项服务时，应当及时查阅了解修改的内容，并自觉遵守本服务协议。用户如继续使用本服务协议涉及的服务，则视为对修改内容的同意，当发生有关争议时，以最新的服务协议为准；用户在不同意修改内容的情况下，有权停止使用本服务协议涉及的服务。</p>
        <p>如用户对本规则内容有任何疑问，可拨打客服电话（4008-567-510） 。    </p>
        <p>本平台最终解释权归贝格互动（武汉）有限公司所有。通过本软件从事的任何活动及所获得的任何商品均与苹果公司（Apple Inc.）无关。苹果公司既不作为赞助商也不以任何形式参与。</p>
    </form>
</body>
</html>
