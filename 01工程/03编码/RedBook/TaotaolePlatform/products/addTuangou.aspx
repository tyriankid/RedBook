<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="addTuangou.aspx.cs" Inherits="RedBookPlatform.products.addTuangou" %>
<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>
<%@ Register TagPrefix="UI" Namespace="Taotaole.Common" Assembly="Taotaole.Common" %>
<!DOCTYPE html>
<html>
   
	<head runat="server">
		<meta charset="UTF-8">
		<title>添加团购商品</title>
		<link rel="stylesheet" type="text/css" href="<%=CSS_PATH %>comm.css"/>
        <link href="<%=CSS_PATH %>flashupload.css" rel="stylesheet" type="text/css" />
        <script src="<%=JS_PATH %>jquery.min.js"></script>
        <script src="<%=JS_PATH %>flashupload.js"></script>
        <script src="/Resources/laydate/laydate.dev.js"></script>
	</head>
	<body>
		<div class="wrapper">
			<div class="crumbs">
				<ul id="breadcrumbs" class="breadcrumb"> 
	                <li><a href="#">主页</a></li>
	                <li><a href="#">商品管理</a></li>
	                <li class="active"><a href="#" title="">添加团购商品</a></li>
	            </ul>
			</div>
		    <div class="content">
                <form runat="server" id="form1">
		    	<table class="table table-striped tabel-textalign">
                    <tbody>
                        <tr role="ddlproducts">
                            <td width="150"><span  class="red" id="spProducts">*</span>商品选择</td>
                            <td>
                                <asp:DropDownList ID="ddlCategory" runat="server" AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" Width="151px"></asp:DropDownList>
                                <asp:DropDownList ID="ddBrand" runat="server" OnSelectedIndexChanged="ddlBrand_SelectedIndexChanged" AppendDataBoundItems="True" AutoPostBack="True" Width="213px"></asp:DropDownList>
                                <asp:DropDownList ID="ddlProducts" runat="server" OnSelectedIndexChanged="ddlProducts_SelectedIndexChanged" AppendDataBoundItems="True" AutoPostBack="True" Width="213px"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td><span class="red" id="spSort">*</span>排序号</td>
                            <td><input type="text" name="title" id="tdSort" runat="server" value="99999" style="width: 50%;"  onkeyup="value=value.replace(/\D/g,'')"/>排序号越小，排位越靠前.</td>
                        </tr>
                        <tr>
                            <td><span class="red" id="spTitle">*</span>团购商品标题</td>
                            <td><input type="text" name="title" id="title" runat="server" value="" style="width: 50%;" /></td>
                        </tr>
                        <tr>
                            <td><span class="red" id="spRenshu">*</span>成团人数</td>
                            <td><input type="text" name="Renshu" id="Renshu" onkeyup="value=value.replace(/\D/g,'')" runat="server"  value="" style="width: 50%;" />人</td>
                        </tr>
                        <tr>
                            <td><span class="red" id="spDanjia">*</span>人均价</td>
                            <td><input type="text" name="Danjia" id="Danjia" onkeyup="value=value.replace(/\D/g,'')" runat="server"  value="" style="width: 50%;" />元</td>
                        </tr>
                       
                        <tr>
                            <td><span class="red" id="spZuidashu">*</span>最大团购数</td>
                            <td><input type="text" name="zongrenshu" id="Zuidashu" onkeyup="value=value.replace(/\D/g,'')" runat="server" value="" style="width: 50%;" />该商品的最大团购数</td>
                        </tr>
                        <tr>
                            <td><span class="red" id="spKaiJianggz">*</span>开奖规则</td>
                            <td>每达到<input type="text" name="Danjia" id="reachtuan_num" onkeyup="value=value.replace(/\D/g,'')" runat="server"  value="" style="width: 60px;" />团，开奖 <input type="text" name="Danjia" id="prize_num" onkeyup="value=value.replace(/\D/g,'')" runat="server"  value="" style="width: 60px;" /> 次</td>
                        </tr>
                        <tr>
                            <td><span class="red" id="spKaishi">*</span>开始时间</td>
                            <td><input type="text" name="Kaishi" onclick="laydate({ istime: true, format: 'YYYY-MM-DD hh:mm:ss' })" id="Kaishi"  runat="server" style="width:22.6%;" >
                            <span class="red" id="spJieshu">*</span>结束时间
                            <input type="text" name="Jieshu" id="Jieshu"  runat="server"  value="" style="width:22.6%;"   onclick="laydate({ istime: true, format: 'YYYY-MM-DD hh:mm:ss' })"/></td>
                        </tr>
                        <tr>
                            <td><span class="red" id="Span1">*</span>截止时间</td>
                            <td><input type="text" name="inpTime" id="inpTime"  runat="server"  value="" style="width:22.6%;"   onclick="laydate({ istime: true, format: 'YYYY-MM-DD hh:mm:ss' })"/>开团、参团截止时间。未设即为即时开奖</td>
                        </tr>

                        <tr>
                            <td colspan="2" style="text-align:center">
                                <asp:Button runat="server" ID="btnSave" Text="保 存" OnClientClick="return validation();"   CssClass="btn btn-primary" OnClick="btnSave_Click" />
                            </td>
                        </tr>
                    </tbody>
                </table>
                    </form>
		    </div>
		</div>
        <input type="hidden"  runat="server"  id ="hidChange"/>
        <script type="text/javascript">
            function validation() {
                var flag = true;
                var msg = "";
                var starTime = $("#Kaishi").val();
                var endTime=$("#Jieshu").val();
                var stopTime = $("#inpTime").val();

                var starTimes = new Date(starTime.replace("-", "/").replace("-", "/"));
                var endTimes = new Date(endTime.replace("-", "/").replace("-", "/"));
                var stopTimes = new Date(stopTime.replace("-", "/").replace("-", "/"));

                if (stopTime!="")
                {
                    if (stopTimes < starTimes)
                    {
                        flag = false; msg = "截止时间要大于开始时间!";
                    
                    }
                    if (stopTimes > endTimes)
                    {
                        flag = false; msg = "截止时间应在结束时间之前!";     
                    }    
                }
                if (endTimes < starTimes)
                {
                    flag = false; msg = "结束时间要大于开始时间!";
                }
                if ($("#Jieshu").val() == "") {
                    $("#spJieshu").css({ color: "red" });
                    flag = false; msg = "请选择结束时间!";
                }
                if ($("#Kaishi").val() == "") {
                    $("#spKaishi").css({ color: "red" });
                    flag = false; msg = "请选择开始时间!";
                }

                if ($("#reachtuan_num").val() == "" || $("#prize_num").val() == "") {
                    $("#spKaiJianggz").css({ color: "red" });
                    flag = false; msg = "请填写开奖规则数值对!";
                }

                if ($("#Zuidashu").val() == "" || $("#Zuidashu").val()=="0") {
                    $("#spZuidashu").css({ color: "red" });
                    flag = false; msg = "请填写最大团购数!";
                }
                if ($("#Danjia").val() == "") {
                    $("#spDanjia").css({ color: "red" });
                    flag = false; msg = "请填写人均价!";
                }
                if ($("#Renshu").val() == "" || $("#Renshu").val()<2) {
                    $("#spRenshu").css({ color: "red" });
                    flag = false; msg = "填写成团人数。并且不能小于两人!";
                }
                if (($("#hidChange").val()) == "") {
                    if ($("#ddlCategory").val() == "0" && $("#ddlProducts").val() == "0") {
                        $("#spProducts").css({ color: "red" });
                        flag = false; msg = "请选择商品分类和商品!";
                    }
                    else if ($("#ddlCategory").val() != "0" && $("#ddlProducts").val() == "0")
                    {
                        $("#spProducts").css({ color: "red" });
                        flag = false; msg = "请选择商品!";
                    }
                }

                if (!flag) { alert(msg); }
                return flag;
            }

            $(function () {
                var tid = "<%=tid %>";
                //编辑模式
                if (tid > 0) {
                    $("[role='ddlproducts']").hide();
                }
            })
    </script>

	</body>
</html>
