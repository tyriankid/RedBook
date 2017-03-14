<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrdersMonthStatistics.aspx.cs" Inherits="RedBookPlatform.statistics.OrdersMonthStatistics" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>用户统计</title>
    		<link rel="stylesheet" type="text/css" href="/Resources/css/comm.css"/>
    <script src="../Resources/js/jquery.min.js"></script>
    <script src="../Resources/js/highcharts.js"></script>
    <script src="<%=JS_PATH %>/comm.js" type="text/javascript" charset="utf-8"></script>
     <script src="/Resources/laydate/laydate.dev.js"></script>
</head>
<body>
    <form id="form1" runat="server">
<div class="wrapper">
			<div class="crumbs">
				<ul id="breadcrumbs" class="breadcrumb"> 
	                <li><a href="#">主页</a></li>
	                <li><a href="#">消费分析</a></li>
	                <li class="active"><a href="#" title="">新增订单（月报表）</a></li>
	            </ul>
			</div>
		    <div class="content">
		    	<div class="oprationbtn">
		    		<div class="search">
                       <span>开始:</span>
                       <input type="text" name="inpTime" id="inpStarTime"  runat="server"  value="" style="width:130px"   onclick="laydate({ istime: true, format: 'YYYY-MM' })"/>
                        <span>结束:</span>
                       <input type="text" name="inpTime" id="inpEndTime"  runat="server"  value="" style="width:130px;"   onclick="laydate({ istime: true, format: 'YYYY-MM' })"/>
		    		   <asp:Button runat="server" ID="btnSearch" CssClass="btn btn-info" Text="&nbsp;&nbsp;&nbsp;查&nbsp;&nbsp;&nbsp;询&nbsp;&nbsp;&nbsp;" OnClick="btnSearch_Click"  />
                    </div>
		    	</div>
                <div id="quxian" ><span style="float:left">曲线模式&nbsp;&nbsp;&nbsp;</span><span id="Span1" name="show" style="float:left;cursor:pointer">(全屏显示)</span></div>
                  <div id="container" style="width:1383px;height:300px; margin-left: auto; margin-right: auto; margin-bottom: 0; padding-right: 10px;"></div>
                <div id="liebiao" style=" margin-top:15px;"><span style="float:left">列表模式&nbsp;&nbsp;&nbsp;</span><span id="show" name="show" style="float:left;cursor:pointer">(全屏显示)</span></div>
                	<table class="table table-striped" id="table">
                    <thead>
                        <tr>
                            <th style="text-align:center">时间</th>
                            <th style="text-align:center">新增订单</th>
                            <th style="text-align:center">新增订单金额</th>
                            <th style="text-align:center">总订单</th>
                            <th style="text-align:center">订单金额</th>
                            <th style="text-align:center">订单日增长比例</th>
                            <th style="text-align:center">金额日增长比例</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater runat="server" ID="rptbind">
                        <ItemTemplate>
                            <tr>
                                <td style="text-align:center"><%#Eval("timeday") %></td>
                                <td style="text-align:center"><%#Eval("counts") %>个</td>
                                <td style="text-align:center"><%#Convert.ToDecimal( Eval("moneys")).ToString("0.00") %>元</td>
                                <td style="text-align:center"><%#Eval("allcount") %>个</td>
                                <td style="text-align:center"><%#Convert.ToDecimal(Eval("allmoney")).ToString("0.00") %>元</td>                               
                                <td style="text-align:center"><%# Convert.ToDecimal( Eval("scale"))>100?"<div style='background:url(../Resources/images/arrow-spr265437.png) no-repeat ;width:10px;height:9px;display:inline-block;'></div>"+Eval("scale")+"%":Convert.ToDecimal( Eval("scale"))==100?""+ Eval("scale")+"%":"<div style='background:url(../Resources/images/arrow-spr265437.png) no-repeat -10px ;width:10px;height:9px;display:inline-block;'></div>"+Eval("scale")+"%"%></td>
                                <td style="text-align:center"><%# Convert.ToDecimal( Eval("moneyscale"))>100?"<div style='background:url(../Resources/images/arrow-spr265437.png) no-repeat ;width:10px;height:9px;display:inline-block;'></div>"+Eval("moneyscale")+"%":Convert.ToDecimal( Eval("moneyscale"))==100?""+ Eval("moneyscale")+"%":"<div style='background:url(../Resources/images/arrow-spr265437.png) no-repeat -10px ;width:10px;height:9px;display:inline-block;'></div>"+Eval("moneyscale")+"%"%></td>
                         </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    </tbody>
                        </table>
		    </div>
            
		</div>
    </form>
    <script type="text/javascript">
        
        $(function(){
            loadCharts();

            $("#show").click(function(){
                if($("#show").attr("name")=="show")
                {
                    $("#show").attr("name","hide")
                    $("#show").text("(退出全屏)");
                    $("#quxian").hide();
                    $("#container").hide();
                    return;
                }
                if($("#show").attr("name")=="hide")
                {
                    $("#show").attr("name","show")
                    $("#show").text("(全屏显示)");
                    $("#quxian").show();
                    $("#container").show();
                    return;
                }
                
            });
            $("#Span1").click(function(){
                if($("#Span1").attr("name")=="show")
                {
                    $("#Span1").attr("name","hide")
                    $("#Span1").text("(退出全屏)");
                    $("#container").height(600);
                    $("#highcharts-0").height(600);
                    loadCharts();
                    $("#liebiao").hide();
                    $("#table").hide();

                    return;
                }
                if($("#Span1").attr("name")=="hide")
                {
                    $("#Span1").attr("name","show")
                    $("#Span1").text("(全屏显示)");
                    $("#container").height(300);
                    $("#highcharts-0").height(300);
                    loadCharts();
                    $("#liebiao").show();
                    $("#table").show();
                    return;
                }
                
            });
        })

        function loadCharts(){
            $('#container').highcharts({
                title: {//走势图标题
                    text: '订单新增统计',
                    x: 20,
                    style:{display:"none"}//可隐藏
                },
                subtitle: {//走势图来源
                    text: 'Source: www.xwcms.net',
                    x: 20,
                    style: { display: "none" }//可隐藏
                },
                xAxis: {//X轴分类
                    categories: <%=times.ToString()%>
                    },
                yAxis: {//Y轴会根据series的data数值自动分格并划分上下限
                    title: {
                        text: '(<span style="color:red;">    数值    </span>)',//Y轴表示的文本
                        //style:{display:"none"}//可隐藏
                    }
                },
                tooltip: {
                    valueSuffix: ''//数据的后辍
                },
                legend: {//线条所表示的品种分类
                    enabled: 0,//0为隐藏1为显示
                    layout: 'vertical',
                    align: 'right',
                    verticalAlign: 'middle',
                    borderWidth: 0
                },
                credits: {//制作人；可作为本站水印
                    text: "www.ewaywin.com",
                    href: "#",
                    position: { x: -250, y: -180 },
                    style: { "z-index": "999" }
                },
                series: [//可以为多个品种
                    { name: '新增订单', data: <%=datas.ToString()%> },
                    { name: '订单总金额', data: <%=money.ToString()%> },
                   
                ]
            });
        }

</script>
</body>
</html>
