<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BriskStatistics.aspx.cs" Inherits="RedBookPlatform.statistics.BriskStatistics" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>活跃统计</title>
    <link rel="stylesheet" type="text/css" href="/Resources/css/comm.css"/>
    <script src="../Resources/js/jquery-3.0.0.js"></script>
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
	                <li><a href="#">活跃分析</a></li>
	                <li class="active"><a href="#" title="">日报表</a></li>
	            </ul>
			</div>
		    <div class="content">
		    	<div class="oprationbtn">
		    		<div class="search">
                       
                        <p class="btn btn-info" style="cursor: pointer;" id="LastWeek">&nbsp;&nbsp;&nbsp;上&nbsp;&nbsp;&nbsp;周&nbsp;&nbsp;&nbsp;</p>
                        <p class="btn btn-info" style="cursor: pointer;" id="ThisWeek">&nbsp;&nbsp;&nbsp;本&nbsp;&nbsp;&nbsp;周&nbsp;&nbsp;&nbsp;</p>
                        <p class="btn btn-info" style="cursor: pointer;" id="PrecedingMonth">&nbsp;&nbsp;&nbsp;上&nbsp;&nbsp;&nbsp;月&nbsp;&nbsp;&nbsp;</p>
                        <p class="btn btn-info" style="cursor: pointer;" id="ThisMonth">&nbsp;&nbsp;&nbsp;本&nbsp;&nbsp;&nbsp;月&nbsp;&nbsp;&nbsp;</p>
                       <span>开始:</span>
                       <input type="text" name="inpTime" id="inpStarTime"  runat="server"  value="" style="width:130px"   onclick="laydate({ istime: true, format: 'YYYY-MM-DD' })"/>
                        <span>结束:</span>
                       <input type="text" name="inpTime" id="inpEndTime"  runat="server"  value="" style="width:130px;"   onclick="laydate({ istime: true, format: 'YYYY-MM-DD' })"/>
		    		   <asp:Button runat="server" ID="btnSearch" CssClass="btn btn-info" Text="&nbsp;&nbsp;&nbsp;查&nbsp;&nbsp;&nbsp;询&nbsp;&nbsp;&nbsp;" OnClick="btnSearch_Click"  />
                    </div>
		    	</div>
                <div id="quxian" ><span style="float:left">曲线模式&nbsp;&nbsp;&nbsp;</span><span id="Span1" name="show" style="float:left;cursor:pointer">(全屏显示)</span></div>
                  <div id="container" style="width:1383px;height:300px; margin-left: auto; margin-right: auto; margin-bottom: 0; padding-right: 10px;"></div>
                <div id="liebiao" style=" margin-top:15px;"><span style="float:left">列表模式&nbsp;&nbsp;&nbsp;</span><span id="show" name="show" style="float:left;cursor:pointer">(全屏显示)</span></div>
                	<table class="table table-striped" id="table">
                    <thead>
                        <tr >
                            <th style="text-align:center">用户ID</th>
                            <th style="text-align:center">用户昵称</th>
                            <th style="text-align:center">订单总数</th>
                            <th style="text-align:center">今日总消费金额</th>
                            <th style="text-align:center">昨日总消费金额</th>
                            <th style="text-align:center">增长金额</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater runat="server" ID="rptbind">
                        <ItemTemplate>
                            <tr>
                                <td style="text-align:center"><%#Eval("uid") %></td>
                                <td style="text-align:center"><%#Eval("username") %></td>
                                <td style="text-align:center"><%#Eval("counts") %>个</td>
                                <td style="text-align:center"><%#Convert.ToDecimal( Eval("money")).ToString("0.00") %>元</td>
                                <td style="text-align:center"><%#Convert.ToDecimal( Eval("ztmoney")).ToString("0.00") %>元</td>                    
                                <td style="text-align:center"><%# Convert.ToInt32(Eval("scale"))%>.00元</td> 
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    </tbody>
                        </table>
		    </div>
            
		</div>
    </form>
        <script type="text/javascript">
            var now = new Date(); //当前日期   
            var nowDayOfWeek = now.getDay(); //今天本周的第几天   
            var nowDay = now.getDate(); //当前日   
            var nowMonth = now.getMonth(); //当前月   
            var nowYear = now.getYear(); //当前年   
            nowYear += (nowYear < 2000) ? 1900 : 0; //   
            var lastMonthDate = new Date(); //上月日期   
            lastMonthDate.setDate(1);   
            lastMonthDate.setMonth(lastMonthDate.getMonth() - 1);   
            var lastYear = lastMonthDate.getYear();   
            var lastMonth = lastMonthDate.getMonth(); 
            $(function(){
                loadCharts();
                $("#ThisWeek").click(function(){
                    //获得本周的开始日期
                    var weekStartDate = new Date(nowYear, nowMonth, nowDay - nowDayOfWeek+1); 
                    //获得本周的结束日期
                    var weekEndDate = new Date(nowYear, nowMonth, nowDay + (7 - nowDayOfWeek));   
                    $("#inpStarTime").val(formatDate(weekStartDate));
                    $("#inpEndTime").val(formatDate(weekEndDate));
                });

                $("#LastWeek").click(function(){
                    //获得上周的开始日期
                    var weekStartDate = new Date(nowYear, nowMonth, nowDay - nowDayOfWeek+1 - 7);   
                    //获得上周的结束日期
                    var weekEndDate = new Date(nowYear, nowMonth, nowDay - nowDayOfWeek );  
                    $("#inpStarTime").val(formatDate(weekStartDate));
                    $("#inpEndTime").val(formatDate(weekEndDate));
                });

                $("#PrecedingMonth").click(function(){
                    var lastMonthStartDate = new Date(nowYear, lastMonth, 1); 
                    var lastMonthEndDate = new Date(nowYear, lastMonth, getMonthDays(lastMonth)); 
                    $("#inpStarTime").val(formatDate(lastMonthStartDate));
                    $("#inpEndTime").val(formatDate(lastMonthEndDate));
                });
                $("#ThisMonth").click(function(){
                    var monthStartDate = new Date(nowYear, nowMonth, 1);
                    var monthEndDate = new Date(nowYear, nowMonth, getMonthDays(nowMonth));  
                    $("#inpStarTime").val(formatDate(monthStartDate));
                    $("#inpEndTime").val(formatDate(monthEndDate));
                });

                function getQuarterEndDate() {   
                    var quarterEndMonth = getQuarterStartMonth() + 2;   
                    var quarterStartDate = new Date(nowYear, quarterEndMonth,   
                            getMonthDays(quarterEndMonth));   
                    return formatDate(quarterStartDate);   
                }

                //获得某月的天数   
                function getMonthDays(myMonth) {   
                    var monthStartDate = new Date(nowYear, myMonth, 1);   
                    var monthEndDate = new Date(nowYear, myMonth + 1, 1);   
                    var days = (monthEndDate - monthStartDate) / (1000 * 60 * 60 * 24);   
                    return days;   
                }  
                function formatDate(date) {   
                    var myyear = date.getFullYear();   
                    var mymonth = date.getMonth() + 1;   
                    var myweekday = date.getDate();   
                    if (mymonth < 10) {   
                        mymonth = "0" + mymonth;   
                    }   
                    if (myweekday < 10) {   
                        myweekday = "0" + myweekday;   
                    }   
                    return (myyear + "-" + mymonth + "-" + myweekday);   
                } 
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
                        text: '<%=titles%>',
                        x: 20,
                        //style:{display:"none"}//可隐藏
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
                    //valueSuffix: ''//数据的后辍
                    pointFormat: '{point.y}<br />用户ID:{point.x.name}',
                    headerFormat: '<b>{series.name}</b>'
                       
                 
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
                    { name: '消费总金额', data: <%=datas.ToString()%> },
                   { name: '消费总订单', data: <%=data.ToString()%> },
                   
                ]
            });
        }

</script>
</body>
</html>
