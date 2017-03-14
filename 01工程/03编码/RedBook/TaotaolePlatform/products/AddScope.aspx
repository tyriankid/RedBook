<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddScope.aspx.cs" Inherits="RedBookPlatform.products.AddScope" %>

<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>
<%@ Register TagPrefix="UI" Namespace="Taotaole.Common" Assembly="Taotaole.Common" %>
<!DOCTYPE html>
<html>
   
	<head  runat="server">
		<meta charset="UTF-8">
		<title>添加积分场次</title>
		<link rel="stylesheet" type="text/css" href="/Resources/css/comm.css"/>
        <script src="/Resources/js/jquery.min.js" type="text/javascript" charset="utf-8"></script>
        <script src="/Resources/js/comm.js" type="text/javascript" charset="utf-8"></script>
        
	    <style type="text/css">
            #scope {
                width: 57px;
            }
        </style>
        
	</head>
	<body>
        <form runat="server"  method="post" action="AddScope.aspx"  enctype="multipart/form-data">
		<div class="wrapper">
			<div class="crumbs">
				<ul id="breadcrumbs" class="breadcrumb"> 
	                <li><a href="#">主页</a></li>
	                <li><a href="#">积分商品管理</a></li>
	                <li class="active"><a href="#" title="">添加积分场次</a></li>
	            </ul>
			</div>
		    <div class="content">
		    	<table  class="table table-striped tabel-textalign">
                        <tbody>
                         <tr id="PointsTable">
                             <td  style="width:150px"><span class="red" id="Span2">*</span>区间设置</td>
                             <td><input  type="button" id="addPointsject" value="增加一个区间" onclick="addPoints()" class="btn"/></td>
                            
                        </tr>
                        </tbody>
                    </table>
                    <div style="text-align:center">
                        
                        <asp:Button runat="server" ID="btnSave" Text="保 存"   CssClass="btn btn-primary"  OnClientClick="return savePointss()" OnClick="btnSave_Click" />
                    </div>

		    </div>
		</div>
            
        <input type="hidden" runat="server" id="hidInfo" clientidmode="Static"/>
        <input type="hidden" runat="server" id="hidFile" clientidmode="Static"/>
       <input type="hidden" runat="server" id="hidImageId" clientidmode="Static"/>



</form>
        <script   type="text/javascript">
            //添加积分场次
            var sbjCount = 0;
            function addPoints() {
                var sbjCount = $("[role='Points']").length + 1;

                var inputHtml = "<tr role='Points'>" +
                                                     "<td>积分场次：" + "<input onkeyup=\"value=value.replace(/\\D/g,'')\" type='text'  role='start_" + sbjCount + "'  style='width:100px;height:25px'/></td>" +
                                                     "<td><input name='fileFtp'  type='file'  id='file" + sbjCount + "'  onchange='onChangeFile(this)'/><input  type='image' width='100' height='100' id='image" + sbjCount + "'  src=''/></td>" +
                                                     "<td nowrap=\"nowrap\"><a href='javascript:void(0);' onclick='DisposeTr(this)' class='del'>移除</a></td>" +
                                            "</tr>";
                $("#PointsTable").append(inputHtml);
            }
            //保存添加
            function savePointss() {
                var PointsInfo = "";
                var ImageInfo = "";
                var flag = true;
                if ($("tr[role='Points']").length <= 0) {
                    alert("您还未增加至少一个区间!"); flag = false; return false;
                }
                $("tr[role='Points']").each(function () {
                    //保存区间
                    $(this).find("input[type=text]").each(function () {
                        if ($(this).val() == "") {
                            alert("请将内容填写完整!"); flag = false; return false;
                        }
                        PointsInfo += $(this).val() + ",";
                    });
                    PointsInfo = PointsInfo.substring(0, PointsInfo.length - 1);
                    PointsInfo += ";";
                    //图片展示ID
                    $(this).find("input[type=image]").each(function (i,e) {
                        ImageInfo += $(this).attr("id")+ ",";
                    });
                    ImageInfo = ImageInfo.substring(0, ImageInfo.length - 1);
                    ImageInfo += ";";
                });

                PointsInfo = PointsInfo.substring(0, PointsInfo.length - 1);
                $("#hidInfo").attr("value", PointsInfo);
                ImageInfo = ImageInfo.substring(0, ImageInfo.length - 1);
                $("#hidImageId").attr("value", ImageInfo);
                return flag;
            }
            //删除
            function DisposeTr(arg_obj_item) {
                var objTr = $(arg_obj_item).parent().parent();
                objTr.remove();
            }
            //重载加载
            function initSbjInfo() {
                //根据当前题目和答案的隐藏域凑行html
                var sbjList = $("#hidInfo").val().split(';');//题目列
                var inputHtml = "";
                for (var i = 1; i <= sbjList.length; i++) {
                    var valueList = sbjList[i - 1].split(',');
                    inputHtml += "<tr role='Points'>" +
                            "<td>积分场次：" + "<input onkeyup=\"value=value.replace(/\\D/g,'')\" type='text'  role='start_" + i + "' value='" + valueList[0] + "' style='width:100px;height:25px'/></td>" +
                            "<td><input  name='fileFtp'  type='file'  id='file" + i + "'  onchange='onChangeFile(this)'/><input  type='image' width='100' height='100' id='image" + i + "' src='' /></td>" +
                            "<td nowrap=\"nowrap\"><a href='javascript:void(0);' onclick='DisposeTr(this)'>移除</a></td>" +
                   "</tr>";
                }
                $("#PointsTable").append(inputHtml);
            }
            $(document).ready(function () {
                initSbjInfo();
                ImageInfo() 

            });
            function ImageInfo() {
              
                var file = $("#hidFile").val().split(';');
                var inputImageId = $("#hidImageId").val().split(';');
                for (var i = 0; i < file.length; i++)
                {
                    $("#" + inputImageId[i] + "").attr("src", "/Resources/uploads/" + file[i] + "")
                }     
            }
            var i = 1;
            //选择新图片后,旧图片被替换
            function onChangeFile(sender) {
                var filename = sender.value;
                if (filename == "") {
                    return "";
                }
                var ExName = filename.substr(filename.lastIndexOf(".") + 1).toUpperCase();
                if (ExName == "JPG" || ExName == "BMP" || ExName == "GIF" || ExName == "PNG") {
                    if (sender.getAttribute('id') == 'file' + "" + i + "") {
                       
                        if ($("#image" + i + "").attr("src") == "") {
                            $("#image" + i + "").attr("src", window.URL.createObjectURL(sender.files[0]));
                            i++;
                        }
                        else {
                            $("#image" + i + "").attr("src", window.URL.createObjectURL(sender.files[0]));
                        }
                    }
                    else {
                        //分割”fil e“获取要修改的image的ID
                        var value = sender.getAttribute('id').replace(/[^0-9]/ig, "");
                        $("#image" + value + "").attr("src", window.URL.createObjectURL(sender.files[0]));
                    }
                }
                else {
                    alert('请选择正确的图片格式！');
                    sender.value = null;
                    return false;
                }
            }

        </script>
</body>
</html>
