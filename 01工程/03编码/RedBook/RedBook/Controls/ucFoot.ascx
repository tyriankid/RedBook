<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucFoot.ascx.cs" Inherits="RedBook.Controls.ucFoot" %>
<div style="height:48px;"></div>
<div class="footerdi" style="bottom: 0px;">
    <ul>
        <li class="f_home">
            <a title="首页" href="/index.aspx"> <i>&nbsp;</i> 爽购
            </a>
        </li>
        <li class="f_quan">
            <a title="直购" href="quan.index.aspx" class="">
                <i>&nbsp;</i> 直购
            </a>
        </li>
        <li class="f_community">
            <a title="爽乐购" class="" href="/community.index.aspx"><i>&nbsp;</i>发现
            </a>
        </li>
        <li class="f_jiexiao">
            <a title="揭晓" class="" href="/index.lottery.aspx">
                <i>&nbsp;</i> 最新揭晓
            </a>
        </li>
        <li class="f_personal">
            <a title="我的" class="" href="/user.index.aspx">
                <i>&nbsp;</i> 个人中心
            </a>
        </li>
    </ul>

    <script type="text/javascript">
        //高亮状态判断
        $(function (){
            var url = window.location.pathname;
            if (url.indexOf("index.aspx") > 0) {
                $(".f_home a").addClass("hover");
                $(".f_home").siblings().find("a").removeClass("hover");
            }
            if (url.indexOf("lottery") > 0) {
                $(".f_jiexiao a").addClass("hover");
                $(".f_jiexiao").siblings().find("a").removeClass("hover");
            }
            if (url.indexOf("quan") > 0) {
                $(".f_quan a").addClass("hover");
                $(".f_quan").siblings().find("a").removeClass("hover");
            }
            if (url.indexOf("community") > 0) {
                $(".f_community a").addClass("hover");
                $(".f_community").siblings().find("a").removeClass("hover");
            }
            if (url.indexOf("user.") > 0) {
                $(".f_personal a").addClass("hover");
                $(".f_personal").siblings().find("a").removeClass("hover");
            }
        });
    </script>
</div>