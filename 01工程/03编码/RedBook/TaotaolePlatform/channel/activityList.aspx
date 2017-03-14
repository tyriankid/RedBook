<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="activityList.aspx.cs" Inherits="RedBookPlatform.channel.activityList" %>

<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8">
    <title>网吧活动记录</title>
    <style type="text/css">
        #msgDiv { z-index: 10001; width: 500px; height: 300px; background: white; border: #336699 1px solid; position: absolute; left: 50%; top: 20%; font-size: 12px; margin-left: -225px; display: none; }
        /*#bgDiv { position: absolute; top: 0px; left: 0px; right: 0px; background-color: #777; filter: progid:DXImageTransform.Microsoft.Alpha(style=3,opacity=25,finishOpacity=75); opacity: 0.6; }*/
    </style>
    <link rel="stylesheet" type="text/css" href="/Resources/css/comm.css" />
    <script src="/Resources/js/jquery.min.js" type="text/javascript" charset="utf-8"></script>
    <script src="/Resources/js/comm.js" type="text/javascript" charset="utf-8"></script>
    <script src="/Resources/laydate/laydate.dev.js"></script>
</head>
<body>
    <form id="Form1" runat="server">
        <div class="wrapper" id="bgDiv">
            <div class="crumbs">
                <ul id="breadcrumbs" class="breadcrumb">
                    <li><a href="#">主页</a></li>
                    <li><a href="#">渠道管理</a></li>
                    <li class="active"><a href="#" title="">会员兑换记录</a></li>
                </ul>
            </div>
            <div class="content">
                <div class="oprationbtn" style="height: 50px">
                    <%--                   时间范围:<input type="text" name="TimeStart" onclick="laydate({ istime: true, format: 'YYYY-MM-DD' })" id="txtTimeStart" runat="server">-
                    <input type="text" name="TimeEnd" onclick="laydate({ istime: true, format: 'YYYY-MM-DD' })" id="txtTimeEnd" runat="server">--%>
                    <span style="padding-right: 3px">金额:</span>
                    <asp:DropDownList ID="ddlMoney" runat="server" Style="padding: 4px 7px 4px 7px;" CssClass="btn">
                        <asp:ListItem Value="-1" Selected="True">请选择</asp:ListItem>
                        <asp:ListItem Value="0">未结算金额</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Button runat="server" ID="Export" CssClass="btn btn-info" Text="导出数据" OnClick="Export_Click" />
                    <div class="search">
                        <span>查询关键字</span>
                        <asp:TextBox runat="server" ID="tboxKeyword" MaxLength="10"></asp:TextBox>

                        <asp:Button runat="server" ID="btnSearch" CssClass="btn btn-info" Text="查询" OnClick="btnSearch_Click" />
                    </div>
                </div>
                <table class="table table-striped table-even">
                    <thead>
                        <tr>
                            <th>渠道商名称</th>
                            <th>联系人</th>
                            <th>联系方式</th>
                            <th>支付宝账号</th>
                            <th>参与总人数</th>
                            <th>总金额(元)</th>
                            <th>已结算金额(元)</th>
                            <th>未结算金额(元)</th>
      <%--                      <th>申请结算金额(元)</th>--%>
                            <%-- <th>最近结算时间</th>--%>
                     <%--       <th>操作</th>--%>
                        </tr>
                    </thead>

                    <tbody>
                        <asp:Repeater runat="server" ID="rptAdmin">
                            <ItemTemplate>
                                <tr>
                                    <td><%#Eval ("realname") %></td>
                                    <td><%#Eval ("contacts") %></td>
                                    <td><%#Eval ("usermobile") %></td>
                                    <td><%#Eval ("gatheringaccount") %></td>
                                    <td><a href='activityDetailList.aspx?uid=<%#Eval("cuid") %>' style="color: blue"><%#Eval("totalnumber")%></a></td>
                                    <td><span style="color: blue"><%# ((int)Eval("totalnumber")*(decimal)Eval("settlementprice")).ToString("0.00") %></span></td>
                                    <td><span style="color: green"><%#((int)Eval("hadexchangenumber")*(decimal)Eval("settlementprice")).ToString("0.00")%></span></td>
                                    <td><span style="color: darkviolet"><%#((int)Eval("unexchangenumber")*(decimal)Eval("settlementprice")).ToString("0.00")%></span></td>
                                <%--    <td><span style="color: red"><%#((int)Eval("applynumber")*(decimal)Eval("settlementprice")).ToString("0.00")%></span></td>--%>
                                    <%--<td><%#Eval("settlementtime","{0:yyyy-MM-dd HH:mm:ss}") %></td>--%>
<%--                                    <td class="opration">
                                        <span style="padding-left: 5px" >
                                            <a href="#" onclick="showDetail('<%#Eval("realname")%>','<%#Eval("cuid")%>','<%#((int)Eval("unexchangenumber")*(decimal)Eval("settlementprice")).ToString("0.00")%>','<%#Eval("gatheringaccount")%>',2)">结算</a>
                                        </span>
                                    <span style="padding-left: 5px">
                                            <a href="#" onclick="showDetail('<%#Eval("realname")%>','<%#Eval("cuid")%>','<%#((int)Eval("applynumber")*(decimal)Eval("settlementprice")).ToString("0.00")%>','<%#Eval("gatheringaccount")%>',1)">申请结算</a>
                                        </span>
                                    <span style="padding-left: 5px">
                                            <a href='activityDetailList.aspx?uid=<%#Eval("cuid") %>&exchange=3'>详细</a>
                                        </span>
                                    </td>--%>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
                <div class="pager oprationbtn">
                    <span style="float: left; padding-left: 0px">当前查询数据汇总:总共有<span style="color: blue"><%= allusercount %></span>人次参与
                        ,总金额<span style="color: blue"><%= allmoney %></span>元,已结算总金额<span style="color: green"><%= exchange2 %></span>元,未结算金额<span style="color: darkviolet"><%= unsettledmoney %></span>元</span>
                    <UI:Pager runat="server" ShowTotalPages="true" ID="pager1" />
                </div>
            </div>

        </div>

        <%--弹出层--%>
        <div id="msgDiv">
            <div id="msgShut">关闭</div>
            <div id="msgDetail">
                <table class="table table-striped tabel-textalign">
                    <tbody>
                        <tr>
                            <td><span style="color: red">*</span>渠道商名称</td>
                            <td>
                                <span id="txtRealname" /></span>
                            </td>
                        </tr>
                        <tr>
                            <td><span style="color: red">*</span>支付宝账号</td>
                            <td>
                                <span id="txtsettllementaccount" /></span>
                            </td>
                        </tr>
                        <tr>
                            <td><span style="color: red">*</span>结算总额</td>
                            <td id="money">
                                <span id="txtsumMoney" /></span>&nbsp;<span style="color: red">元</span>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align: center">
                                <input type='button' name='btnSave' id='btnSave' value="提交" class="btn btn-primary" onclick='ApplySettlement()' />
                                <input type='button' name='btnApply' id='btnApply' value="提交" class="btn btn-primary" onclick='ApplySettlement(2)' />
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
        <input type="hidden" id="cuid" name="cuid" runat="server" value="" />
        <script type="text/javascript">

            //初始化
            //显示弹出层
            function showDetail(realname, cuid, unsettledmoney, account, apply) {//show detail
                //msgDiv
                if (apply == 1) {
                    $("#btnSave").hide();
                    $("#btnApply").show();
                }
                if (apply == 2) {
                    $("#btnApply").hide();
                    $("#btnSave").show();
                }
                var msgDiv = document.getElementById("msgDiv");
                msgDiv.style.marginTop = -75 + document.documentElement.scrollTop + "px";
                //bgDiv
                var bgDiv = document.getElementById("bgDiv");
                bgDiv.style.width = document.body.offsetwidth + "px";
                bgDiv.style.height = screen.height + "px";
                msgShut
                var msgShut = document.getElementById("msgShut");
                msgShut.onclick = function () {
                    msgDiv.style.display = msgDiv.style.display = "none";
                }
                //content
                document.getElementById("cuid").value = cuid;
                document.getElementById("txtRealname").innerText = realname;
                document.getElementById("txtsumMoney").innerText = unsettledmoney;
                document.getElementById("txtsettllementaccount").innerText = account;
                msgDiv.style.display = bgDiv.style.display = "block";
                //var msgDetail = document.getElementById("money");
                //msgDetail.innerHTML = "";
                //window.open ('addchannel.aspx','newwindow','height=100,width=400,top=0,left=0,toolbar=no,menubar=no,scrollbars=no,resizable=no, location=no,status=no') 
            }
            function ApplySettlement(apply) {
                var alipayaccount = $("#txtsettllementaccount").text();
                var settledmoney = $("#txtsumMoney").text();
                var cuid = $("#cuid").val();
                if (alipayaccount == "") {
                    alert("支付宝账号不能为空");
                    return;
                }
                con = confirm("打款是不可恢复的,确定要给账号【" + alipayaccount + "】打款【" + settledmoney + "】吗?"); //在页面上弹出对话框  
                    if (con == true) {
                        $.ajax({
                            type: 'post', dataType: 'json', timeout: 10000,
                            async: false,
                            url: location.href,
                            data: {
                                action: "submit",
                                unumber: alipayaccount,
                                summoney: settledmoney,
                                cuid: cuid
                            },
                            success: function (resultData) {
                                if (resultData.success == true) {
                                    location.reload();
                                    alert(resultData.msg);
                                } else {
                                    alert(resultData.msg);
                                }
                            }
                        });
                    }
            }
        </script>
    </form>
</body>
</html>
