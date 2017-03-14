<%@ Page Language="C#" validateRequest="false" AutoEventWireup="true" CodeBehind="addMultiArticle.aspx.cs" Inherits="RedBookPlatform.wx.addMultiArticle" %>

<!DOCTYPE html>

<html>
	<head runat="server">
		<meta charset="UTF-8">
		<title>新增多图文回复</title>
		<link rel="stylesheet" type="text/css" href="<%=CSS_PATH %>comm.css"/>
        <script src="<%=JS_PATH %>jquery.min.js"></script>
        
	</head>
	<body>
		<div class="wrapper">
			<div class="crumbs">
				<ul id="breadcrumbs" class="breadcrumb"> 
	                <li><a href="#">主页</a></li>
	                <li><a href="#">系统设置</a></li>
	                <li class="active"><a href="#" title="">新增多图文回复</a></li>
	            </ul>
			</div>
		    <div class="content">
                <form runat="server" id="form1">


                    <!-- 单图文编辑-->
                <div class="tw_body">
                <div class="tw_box box_left">
                    <div id="sbox1" class="body">
                        <div class="img_fm" onmousemove="sBoxzzShow('1')">
                            <div id="fm1" style="display: block;" class="gy_bg fmImg">
                                封面图片</div>
                            <img id="img1" class="fmImg" style="display: none" src="" />
                            <p class="abstractVal">
                                <span id="title1" style="margin-left: 4px;">摘要</span></p>
                            <div id="zz_sbox1" class="zzc" onmouseout="sBoxzzHide('1')" style="line-height: 178px;">
                                <a href="javascript:void(0)" onclick="editTW(1);">修改</a></div>
                        </div>
                    </div>
                    <div id="sbox2" class="baseBorder twSBox" onmousemove="sBoxzzShow(2)" style="position: relative;">
                        <div class="body">
                            <div id="title2" class="info">
                                <p>
                                    标题</p>
                            </div>
                            <div class="simg">
                                <div style="width: 100px; height: 100px; background-color: rgb(236,236,236); line-height: 100px;
                                    color: #c0c0c0; font-weight: bold; text-align: center;">
                                    预览图</div>
                                <img id="img2" src="" class="fmImg" style="display: none" />
                            </div>
                            <div class="clearfix">
                            </div>
                        </div>
                        <div id="zz_sbox2" onmouseout="sBoxzzHide(2)" class="zzc" style="line-height: 121px;
                            height: 121px;">
                            <a href="javascript:void(0)" onclick="editTW(2);">修改</a> <a href="javascript:void(0)"
                                onclick="sBoxDel(2);">删除</a></div>
                    </div>
                    <span id="addSBoxInfoHere"></span>
                    <div class="baseBorder twSBox boxAdd">
                        <div style="width: 360px;">
                            <a href="javascript:void(0)" onclick="addSBox()">添加一个图文</a>
                        </div>
                    </div>
                </div>
                <div id="box_move" class="tw_box box_left box_body">
                    <div class="cont_body">
                        <div class="fgroup">
                            <span><em>*</em>标 题：</span>
                            <input id="title" type="text" onkeyup="syncTitle(this.value)" />
                        </div>
                        <div class="fgroup">
                            <div style="width: 100%; height: 28px;">
                                <span><em>*</em>封 面：</span> <span id="swfu_container"><span>
                                <span id="spanButtonPlaceholder"></span>
                                </span><span id="divFileProgressContainer"></span></span>
								<div>建议尺寸：360*200</div>
                            </div>
                            <div id="smallpic" style="display: none; margin-left: 100px;">
                            </div>
                             <!--封面上传后，返回的图片地址，填充下面的input对象。-->
                            <input id="fmSrc" type="text" value="" style="display: none;" />
                        </div>
                        <div class="fgroup" id="w_url">
                            <span>自定义链接：</span> http://
                            <input id="urlData" style="width: 192px;" type="text" value="" />(可不填，若填写则优先跳转)
                        </div>
                        <div id="msg" style="display: none; color: red; font-size: 14px;">
                        </div>
                        <div style="width:700px">
                            <textarea id ="content" runat="server" runat="server"   style="width: 100%;"  height="200px"></textarea>
                        </div>
                    </div>
                    <i class="arrow arrow_out" style="margin-top: 0px;"></i><i class="arrow arrow_in"
                        style="margin-top: 0px;"></i>
                </div>
                <div id="nextTW">
                </div>
                <div class="clearfix">
                </div>
            </div>
            <div id="modelSBox" style="display: none;">
                <div id="sboxrpcode1366" class="baseBorder twSBox" onmousemove="sBoxzzShow(rpcode1366)"
                    style="position: relative;">
                    <div class="body">
                        <div id="titlerpcode1366" class="info">
                            <p>
                                标题</p>
                        </div>
                        <div class="simg">
                            <div style="width: 100%; height: 100%; background-color: rgb(236,236,236); line-height: 100px;
                                color: #c0c0c0; font-weight: bold; text-align: center;">
                                预览图</div>
                            <img id="imgrpcode1366" src="pig1.png" style="width: 100%; height: 100%; display: none;
                                position: absolute; top: 0; left: 0;" />
                        </div>
                        <div class="clearfix">
                        </div>
                    </div>
                    <div id="zz_sboxrpcode1366" onmouseout="sBoxzzHide(rpcode1366)" class="zzc" style="line-height: 121px;
                        height: 121px;">
                        <a href="javascript:void(0)" onclick="editTW(rpcode1366)">修改</a> <a href="javascript:void(0)"
                            onclick="sBoxDel(rpcode1366);">删除</a></div>
                </div>
            </div>
            <div class="clearfix">
            </div>
                    <input id="Articlejson" name="Articlejson" type="hidden" />
		    	<table class="table table-striped tabel-textalign">
                    <tbody>
                        <tr>
                            <td width="130">回复类型</td>
                            <td>
                                <input type="radio" name="rdoReplytype" value="1" checked="checked"> 关键字回复 
                                <input type="radio" name="rdoReplytype" value="2"> 关注时回复 
                                <input type="radio" name="rdoReplytype" value="3"> 无匹配回复
                            </td>
                        </tr>
                        <tr role="trKeyword">
                            <td><span style="color:red" id="spKeyword">*</span>关键字</td>
                            <td><input type="text" name="Keyword" id="txtKeyword"   style="width: 50%;" /> 用户可通过该关键字搜到到这个内容</td>
                        </tr>
                        <tr role="trKeyword">
                            <td>匹配模式</td>
                            <td>
                                <input type="radio" name="rdoMatchtype" value="1" checked="checked"> 精确匹配 
                                <input type="radio" name="rdoMatchtype" value="2"> 模糊匹配 
                            </td>
                        </tr>
                        <tr>
                            <td>状态</td>
                            <td>
                                <input type="radio" name="rdoIsdisable" value="0" checked="checked"> 启用 
                                <input type="radio" name="rdoIsdisable" value="1"> 禁用 
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align:center">
                               
                                <input type="button" role="btnAdd" onclick="return checkJson();"   Class="btn btn-primary submit_DAqueding" value="添 加" />
                            </td>
                        </tr>
                    </tbody>
                </table>
                    </form>
		    </div>
		</div>
        <script src="<%=JS_PATH %>swfupload/swfupload.js" type="text/javascript"></script>
        <script src="<%=JS_PATH %>swfupload/handlers.js" type="text/javascript"></script>
        <link href="<%=CSS_PATH %>MutiArticle.css" rel="stylesheet" type="text/css" />
        <script type="text/javascript" charset="utf-8" src="/ueditor/ueditor.config.js"></script>
        <script type="text/javascript" charset="utf-8" src="/ueditor/ueditor.all.min.js"> </script>
        <script type="text/javascript" charset="utf-8" src="/ueditor/lang/zh-cn/zh-cn.js"></script>
        <script src="<%=JS_PATH %>swfupload/uploadMult.js" type="text/javascript"></script>
        <script src="<%=JS_PATH %>MultiBox.js" type="text/javascript"></script>
        <script src="<%=JS_PATH %>jquery-json-2.4.js" type="text/javascript"></script>
        

        <script type="text/javascript">
            //上传图片控件需要
            var auth = "<%=(Request.Cookies[FormsAuthentication.FormsCookieName]==null ? string.Empty : Request.Cookies[FormsAuthentication.FormsCookieName].Value) %>";
            //content需要
            var editor = new baidu.editor.ui.Editor();
            editor.render("content");
            //关注时回复和误匹配回复如果存在则不能再被选
            var isMismatchExist = "<%=isMismatchExist%>";
            var isSubscribeReplyExist = "<%=isSubscribeReplyExist%>";
            //全局变量存储三个radiobutton的值
            var replytype = "<%=replytype%>", matchtype = "<%=matchtype%>", isdisable = "<%=isdisable%>", keyword = "<%=keyword%>";
            $(function () {
                //只有点击了关键字回复时,显示关键字区域
                $("[name='rdoReplytype']").click(function () {
                    replytype = $(this).val();
                    if ($(this).val() == "1") $("[role='trKeyword']").show();
                    else $("[role='trKeyword']").hide();
                });
                $("[name='rdoMatchtype']").click(function () {
                    matchtype = $(this).val();
                });
                $("[name='rdoIsdisable']").click(function () {
                    isdisable = $(this).val();
                });
                //判断并无效化已经存在的关注时回复和无匹配回复
                if(isMismatchExist == "True"){
                    $("[name='rdoReplytype'][value='3']").attr("disabled", true);
                }
                if (isSubscribeReplyExist == "True") {
                    $("[name='rdoReplytype'][value='2']").attr("disabled",true);
                }

                //根据载入时的replytype隐藏关键字区域
                var needsToHideKeyword = "<%=needsToHideKeyword%>";
                if (needsToHideKeyword == "False") $("[role='trKeyword']").show();
                else $("[role='trKeyword']").hide();

                var replyid = parseInt("<%=replyid%>");
                if (replyid <= 0) {//添加模式
                    $("[name='rdoReplytype']").eq(0).click();
                    $("[name='rdoMatchtype']").eq(0).click();
                    $("[name='rdoIsdisable']").eq(0).click();
                }
                else {//编辑模式
                    $("[role='btnAdd']").val("编辑"); //按钮名字变成编辑
                    edit = true;//定义当前JS执行为编辑状态
                    $("#txtKeyword").val(keyword);
                    $("[name='rdoReplytype'][value='" + replytype + "']").click();
                    $("[name='rdoMatchtype'][value='" + matchtype + "']").click();
                    $("[name='rdoIsdisable'][value='" + isdisable + "']").click();
                    loadJsonData();
                }
            });


            function loadJsonData() {
                tws = <%=articleJson%>;
                
                boxIdN = 1;
                for (var a = 0; a < parseInt("<%=sboxCount%>") ;a++) {
                    if (a >= 2) {
                        editSBox();
                    }
                    if (!isNull(tws[a].Title)) {
                        $("#title" + (parseInt(a) + 1)).text(tws[a].Title);
                    }
                    if (!isNull(tws[a].Imgurl)) {
                        $("#img" + (parseInt(a) + 1)).attr("src", (tws[a].Imgurl)).show();
                    }
                }

                loadData();
            }

            var SessionID = '<%=Session.SessionID %>';
            function AddMultArticles() {
                if (replytype == "1" && $("#txtKeyword").val() == "") {
                    alert("你选择了关键字回复，请填写关键字！");
                    return;
                }

                $.ajax({
                    url: "addMultiArticle.aspx?cmd=add",
                    type: "POST",
                    dataType: "text",
                    data: {
                        "MultiArticle": $("#Articlejson").val() //articlejson串
                        , "Keyword": $("#txtKeyword").val() //关键字
                        , "Matchtype": matchtype
                        , "Replytype": replytype
                        , "Isdisable": isdisable
                    },
                    success: function (msg) {
                        if (msg == "true") {
                            alert("添加成功！");
                            window.location.href = "wxreply.aspx";
                        }
                        else if (msg == "key") {
                            alert("关键字重复，请重新填写！");
                            $("#txtKeys").focus();
                        }
                        else if (msg == "miskey") {
                            alert("缺少关键字，请重新填写！");
                            $("#txtKeys").focus();
                        }
                        else {
                            alert("添加失败！");
                        }
                    },
                    error: function (xmlHttpRequest, error) {
                        // alert(error);
                    }
                });
            }

            function EditMultArticles() {
                if (replytype == "1" && $("#txtKeyword").val() == "") {
                    alert("你选择了关键字回复，请填写关键字！");
                    return;
                }

                $.ajax({
                    url: "addMultiArticle.aspx?cmd=edit&replyid=<%=replyid%>",
                    type: "POST",
                    dataType: "text",
                    data: {
                        "MultiArticle": $("#Articlejson").val() //articlejson串
                        , "Keyword": $("#txtKeyword").val() //关键字
                        , "Matchtype": matchtype
                        , "Replytype": replytype
                        , "Isdisable": isdisable
                    },
                    success: function (msg) {
                        if (msg == "true") {
                            alert("编辑成功！");
                            window.location.href = "wxreply.aspx";
                        }
                        else if (msg == "key") {
                            alert("关键字重复，请重新填写！");
                            $("#txtKeys").focus();
                        }
                        else if (msg == "miskey") {
                            alert("缺少关键字，请重新填写！");
                            $("#txtKeys").focus();
                        }
                        else {
                            alert("编辑失败！");
                        }
                    },
                    error: function (xmlHttpRequest, error) {
                        // alert(error);
                    }
                });
            }



        </script>

	</body>
</html>

