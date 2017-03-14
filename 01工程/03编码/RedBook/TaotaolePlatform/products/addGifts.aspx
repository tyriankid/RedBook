<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="addGifts.aspx.cs" Inherits="RedBookPlatform.products.addGifts" %>

<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>
<%@ Register TagPrefix="UI" Namespace="Taotaole.Common" Assembly="Taotaole.Common" %>
<!DOCTYPE html>
<html>

<head runat="server">
    <meta charset="UTF-8">
    <title>添加积分商品</title>
    <link rel="stylesheet" type="text/css" href="/Resources/css/comm.css" />
    <script src="/Resources/js/jquery.min.js" type="text/javascript" charset="utf-8"></script>
    <script src="/Resources/js/comm.js" type="text/javascript" charset="utf-8"></script>

    <style type="text/css">
        #scope {
            width: 57px;
        }

        .auto-style1 {
            height: 39px;
        }
    </style>

</head>
<body>
    <div class="wrapper">
        <div class="crumbs">
            <ul id="breadcrumbs" class="breadcrumb">
                <li><a href="#">主页</a></li>
                <li><a href="#">商品管理</a></li>
                <li class="active"><a href="#" title="">添加积分商品</a></li>
            </ul>
        </div>
        <div class="content">
            <form runat="server" id="form1">
                <table class="table table-striped tabel-textalign">
                    <tbody>
                        <tr role="ddlproducts">
                            <td style="width: 150px"><span class="red">*</span>商品选择</td>
                            <td>
                                <asp:DropDownList ID="ddlCategory" runat="server" AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" Width="151px"></asp:DropDownList>
                                <asp:DropDownList ID="ddBrand" runat="server" OnSelectedIndexChanged="ddlBrand_SelectedIndexChanged" AppendDataBoundItems="True" AutoPostBack="True" Width="213px"></asp:DropDownList>
                                <asp:DropDownList ID="ddlProducts" runat="server" OnSelectedIndexChanged="ddlProducts_SelectedIndexChanged" AppendDataBoundItems="True" AutoPostBack="True" Width="213px"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td><span class="red">*</span>积分商品名称</td>
                            <td>
                                <input type="text" id="title" runat="server" value="" style="width: 50%;" /></td>
                        </tr>

                        <tr>
                            <td><span class="red">*</span>积分场次</td>
                            <td id="tdScope" role="ddlproducts">
                                <asp:RadioButtonList ID="rdoScope" runat="server" RepeatLayout="UnorderedList" CssClass="choosebox" /></td>
                            <td>
                                <asp:Literal runat="server" ID="litScope"></asp:Literal></td>
                        </tr>

                        <tr>
                            <td><span class="red">*</span>游戏方式</td>
                            <td role="ddlproducts">
                                <asp:RadioButtonList ID="rdoGame" runat="server" RepeatLayout="UnorderedList" CssClass="choosebox" onclick="changeGame()">
                                    <asp:ListItem Value="gua" Text="刮刮乐"></asp:ListItem>
                                    <asp:ListItem Value="pao" Text="跑马灯"></asp:ListItem>
                                    <asp:ListItem Value="zhuan" Text="大转盘"></asp:ListItem>
                                </asp:RadioButtonList></td>
                            <td>
                                <asp:Literal ID="litGameType" runat="server"></asp:Literal></td>
                        </tr>
                        <tr style="display: none" id="trGuaProduct">
                            <td><span class="red">*</span>中奖机率</td>
                            <td role="ddlproducts">奖品总数<input type="text" id="inpGuaProduct" onkeyup="value=value.replace(/\D/g,'')" runat="server" value="0" style="width: 60px;" />-中奖数<input type="text" id="inpGuaPrize" onkeyup="value=value.replace(/\D/g,'')" runat="server" value="0" style="width: 60px;"  onchange="changePrice(this)"/><span style="color: gray">例如:奖品总数1000,中奖数50,抽奖1000次则会有50个人中奖. 奖品为一次性奖品,抽完1000次即作废</span></td>
                            <td  style="display:none" id="litgua">奖品总数:<asp:Literal ID="litGiftSum" runat="server"></asp:Literal>-中奖数:<asp:Literal ID="litWinSum" runat="server"></asp:Literal></td>
                        </tr>
                        <tr style="display: none" id="trPrizeNumber">
                            <td><span class="red">*</span>中奖数量</td>
                            <td role="ddlproducts">
                                <input type="text" id="PrizeNumber" onkeyup="value=value.replace(/\D/g,'')" runat="server" value="0" style="width: 60px;" /></td>
                            <td>
                                <asp:Literal ID="litPrizeNumber" runat="server"></asp:Literal></td>
                        </tr>
                        <tr style="display: none" id="trProbability">
                            <td><span class="red">*</span>中奖概率</td>
                            <td role="ddlproducts">
                                <input type="text" id="probability" onkeyup="value=value.replace(/\D/g,'')" runat="server" value="0" style="width: 60px;" /></td>
                            <td><asp:Literal ID="litProbability" runat="server"></asp:Literal></td>
                        </tr>
                        <tr>
                            <td><span class="red">*</span>奖品总库存</td>
                            <td>
                                <input type="text" id="stock" onkeyup="value=value.replace(/\D/g,'')" runat="server" value="0" style="width: 60px"  onchange="stockChange(this)"  /></td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align: center">
                                <asp:Button runat="server" ID="btnSave" Text="保 存" OnClientClick="return validation();" CssClass="btn btn-primary" OnClick="btnSave_Click"  />
                            </td>
                        </tr>
                    </tbody>
                </table>
            </form>
        </div>
    </div>
    <input type="hidden" runat="server" id="hidChange" />
    <script type="text/javascript">
        var GemeType = "";
        function changeGame() {
            var RadioTable = document.getElementById("rdoGame");
            var RadioInput = RadioTable.getElementsByTagName("INPUT");
            for (var i = 0; i < RadioInput.length; i++) {
                if (RadioInput[i].checked == true) {
                    if (RadioInput[i].value == "gua") {
                        GemeType = "gua";
                        $("#trGuaProduct").show();
                        $("#trProbability").hide();
                        $("#trPrizeNumber").hide();
                    }
                    if (RadioInput[i].value == "zhuan" || RadioInput[i].value == "pao")
                    {
                        $("#stock").removeAttr("disabled")
                        $("#trGuaProduct").hide();
                        $("#trProbability").show();
                        $("#trPrizeNumber").show();
                    }
                }
            }
        }
        function changePrice(priceNumber) {
            if (GemeType == "gua") {
                $("#stock").val(priceNumber.value)
            }
        }
        function stockChange(stock) {
            if (GemeType == "gua") {
                $("#inpGuaPrize").val(stock.value)
            }
        }
        function validation() {
            var flag = true;
            var msg = "";
            if ($("#inpGuaPrize").val() > $("#inpGuaProduct").val())
            {
                flag = false; msg = "中奖数量不能大于商品总数";
                }
            if ($("#PrizeNumber").val() == "") {
                flag = false; msg = "请设置中奖数量!";
            }
            if ($("#stock").val() == "") {
                flag = false; msg = "请设置商品库存!";
            }
            if ($("#probability").val() == "") {
                flag = false; msg = "请设置中奖概率!";
            }
            if ($("#title").val() == "") {
                flag = false; msg = "商品名称不能为空!";
            }
            if (($("#hidChange").val()) == "") {
                if ($("#ddlCategory").val() == "0" && $("#ddlProducts").val() == "0") {
                    flag = false; msg = "请选择商品分类和商品!";
                }
                else if ($("#ddlCategory").val() != "0" && $("#ddlProducts").val() == "0") {
                    flag = false; msg = "请选择商品!";
                }
            }

            if (!flag) { alert(msg); }
            return flag;
        }
        $(function () {
            var giftId = "<%=giftId %>";
            //编辑模式
            if (giftId > 0) {
                $("[role='ddlproducts']").hide();
                $("#stock").attr("disabled", "disabled")
                $("#litgua").show();
                changeGame();
            }
        })
    </script>
</body>
</html>
