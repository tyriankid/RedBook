$(function() {

    var d = 10;

    var timestamp = Date.parse(new Date())/1000;

    var g = {

        FIdx: 0,

        EIdx: d,

        isCount: 1,

        state: -1

    };

	 

    var a = 0;

    var f = null;

    var e = $("#divGoodsLoading");

    var h = $("#btnLoadMore");

    var i = false;

    var b = false;

    var c = function() {

	

        var j = function() {

            return "/" + g.FIdx + "/" + g.EIdx + "/" + g.isCount + "/" + g.state

        };

		 

        var k = function() {		    

            e.show();	            

			GetJPData(Gobal.Webpath, "shopajax", "getUserBuyList"+j(),

            function(q) {			

                if (q.code == 0) {				 

                    var n = q.listItems;					 

                    if (g.isCount == 1) {					

                        a = q.count;						 

                        g.isCount = 0

                    }

                    var r = n.length;

                    var o = "";

                    var s = 0;

                    var t = 0;

                    var l = 0;

                    var u = 0;

                    for (var p = 0; p < r; p++) {

                        var m = parseInt(n[p].codeState);

                        o += "<li><div class='touxianga'>";

                        if (m == 1) {

                            o += '<span class="z-Imgbg"></span><em class="z-Imgtxt jxz"></em>' //进行中...

                        } else {

                            if (m == 2) {

                                o += '<span class="z-Imgbg"></span><em class="z-Imgtxt wjx"></em>'//已满员

                            } else {

                                if (m == 3) {
                                    if(n[p].q_end_times<=timestamp){
                                            o += '<span class="z-Imgbg"></span><em class="z-Imgtxt yjx"></em>'//已揭晓
                                    }else{
                                            o += '<span class="z-Imgbg"></span><em class="z-Imgtxt wjx"></em>'//揭晓中
                                    }
                                    

                                }

                            }

                        }

                        o += "<img src='" + Gobal.LoadPic + "' src2='" + Gobal.imgpath + "/uploads/" + n[p].thumb + "' border=0 onclick=\"location.href='"+Gobal.Webpath+"/mobile/mobile/item/" + n[p].shopid +"'\"></div>"+
                        	"<div class='info'><p class='z-sgl-tt'><a class='tt' onclick=\"location.href='"+Gobal.Webpath+"/mobile/mobile/item/" + n[p].shopid +"'\">" + n[p].shopname + "</a></p>"+
                        	'<p class="qihao">期号：<span style="color:#FD524E">' + n[p].shopqishu + '</span></p>'+
                        	'<p class="qihao">本期参与：<span style="color:#FD524E">' + n[p].gonumber + 
                        	"</span>人次<span style='float: right;color: #1a93dc;' onclick=\"location.href='"+Gobal.Webpath+"/mobile/user/buyDetail/" + n[p].shopid +"'\">参与详情</span></p>"+
                        	'</div><div class="huoder" style="min-height: 100%;" onclick=\'location.href="'+Gobal.Webpath+'/mobile/mobile/item/' + n[p].shopid +'"\'>';

                        //o += '<img src="' + Gobal.LoadPic + '" src2="'+Gobal.imgpath+'/uploads/' + n[p].thumb + '" border=0 alt=""></a><div class="u-sgl-r "><p class="z-sgl-tt"><a class="gray6">(第' + n[p].shopqishu + "期)" + n[p].shopname + "</a></p>";

                        if (m == 1 || m == 2) {

                            s = parseInt(n[p].canyurenshu);    //已参与

                            t = parseInt(n[p].zongrenshu); //总需要

                            l = parseInt(t - s);             //剩余

                            u = parseInt(s * 100) / t;       //百分比

                            u = s > 0 && u < 1 ? 1 : u;

                            //o += '<p>已参与<em class="orange">' + n[p].gonumber + '</em>人次</p><div class="Progress-bar"><p class="u-progress">' + (s > 0 ? '<span style="width:' + u + '%;" class="pgbar"><span class="pging"></span></span>' : "") + '</p><ul class="Pro-bar-li"><li class="P-bar01"><em>' + s + '</em>已参与</li><li class="P-bar02"><em>' + t + '</em>总需人次</li><li class="P-bar03"><em>' + l + "</em>剩余</li></ul></div>"

                            o += '<div class="Progress-bar"><p class="u-progress">' + (s > 0 ? '<span style="width:' + u + '%;" class="pgbar"><span class="pging"></span></span>' : "") + '</p><ul class="Pro-bar-li"><li class="P-bar02" style="background:transparent"><em style="display:block">' + t + '</em>总需人次</li><li class="P-bar03" style="background:transparent"><em style="display:block">' + l + "</em>剩余人次</li></ul></div>"

                        } else {

                            if (m == 3) {
                               if(n[p].q_end_times<=timestamp){
                                    o += '<p>获得者：<em class="blue">' + n[p].q_user + '</em></p><p>参与次数：<em class="gray6" style="color:#FD524E">'+n[p].huodeGonumber+'</em></p><p>幸运号码：<em class="gray6" style="color:#FD524E">'+n[p].q_user_code+'</em></p><p>揭晓时间：<em class="gray6">' + n[p].q_end_time + '</em></p>';
                               }else{
                                        o += '<p>获得者：<em class="blue">正在倒计时揭晓中...</em></p><p>揭晓倒计时正在进行中...</p>';
                               }
                                

                            }

                        }

                        //o += '</div><b class="z-arrow"></b></li>'
                        o += '</div></li>'

                    }

                    if (g.FIdx > -1) {

                        e.prev().removeClass("bornone")

                    }

                    e.before(o).prev().addClass("bornone");

                    if (g.EIdx < a) {

                        i = false;

                        h.show()

                    }

                    loadImgFun(0)

                } else {

                    if (g.FIdx == 0) {

                        if (g.state == -1) {

                            b = true

                        }

                        e.before(Gobal.NoneHtml)

                    }

                }

                e.hide()

            })

        };

        this.getInitPage = function() {

            g.FIdx = 0;

            g.EIdx = d;

            g.isCount = 1;

            k()

        };

        this.getNextPage = function() {

            g.FIdx += d;

            g.EIdx += d;

            k()

        }

    };

    $("#navBox").children("div").each(function() {

        var j = $(this);

        j.click(function() {

            g.state = j.attr("state");			

            j.addClass("z-sgl-crt").siblings().removeClass("z-sgl-crt");

            if (!b) {

                h.hide();

                e.prevAll().remove();

                f.getInitPage();

            }

        })

    });

    h.click(function() {

        if (!i) {

            i = true;

            h.hide();

            f.getNextPage()

        }

    });

	 

    f = new c();

    f.getInitPage()

});