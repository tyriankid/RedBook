var BuyRecordFun = null;
var GetGoodsFun = null;
var SingleFun = null;
$(document).ready(function() {
    var a = function() { 
        var d = $("#hdUserID");
        var l = $("#divMidNav");
        var m = $("#divBuyRecord");
        var e = $("#divGetGoods");
        var j = $("#divSingle");
        var c = 0;
        var k = $("#btnLoadMore");
        var b = $("#divLoading");
        l.find("span").each(function(p) {
            var q = $(this);
            q.click(function() {			 
                h(q, p)
            })
        });
        var h = function(q, p) {
            q.addClass("mCurr").siblings().removeClass("mCurr");
            switch (p) {
            case 0:
                c = 0;
                m.show();
                e.hide();
                j.hide();
                if (BuyRecordFun.CountFalg) {
                    k.show()
                } else {
                    k.hide()
                }
                _IsLoading = false;
                break;
            case 1:
                c = 1;
                m.hide();
                e.show();
                j.hide();
                if (!GetGoodsFun.initFlag) {
                    b.show();
                    GetGoodsFun.initData();
                    GetGoodsFun.initFlag = true
                }
                if (GetGoodsFun.CountFalg) {
                    k.show()
                } else {
                    k.hide()
                }
                _IsLoading = false;
                break;
            case 2:
                c = 2;
                m.hide();
                e.hide();
                j.show();
                if (!SingleFun.initFlag) {
                    b.show();
                    SingleFun.initData();
                    SingleFun.initFlag = true
                }
                if (SingleFun.CountFalg) {
                    k.show()
                } else {
                    k.hide()
                }
                _IsLoading = false;
                break
            }
            loadImgFun(0)
        };
        var g = function() {
            var s = Gobal.NoneHtml;
            var t = false;
            var p = false;
            var q = 0;
            var r = 10;
            var u = 0;
            this.CountFalg = false;
            var x = {
                Type: 0,
                UserID: d.val(),
                FIdx: q,
                EIdx: r,
                IsCount: 1
            };
            var w = function() {
                var y = "";
				y += "/" + x.Type;
                y += "/" + x.UserID;				
                y += "/" + x.FIdx;
                y += "/" + x.EIdx;
                y += "/" + x.IsCount;
                return y
            };
            var v = function() {
                var y = function(I) {
                    if (I.code == 0) {
                        if (x.IsCount == 1) {
                            u = I.count							 
                        }
                        var B = I.listItems;
                        var C = B.length;
                        var E = "";
                        if (B <= 0) {
                            E = s
                        }
                        for (var D = 0; D < C; D++) {
                            var F = B[D];
                            var H = F.canyurenshu;
                            var A = F.zongrenshu;
                            var G = parseFloat(H * 100 / A);
                            var z = Math.ceil(G);
                            //E += "<ul onclick=\"location.href='"+Gobal.Webpath+"/mobile/mobile/item/" + F.shopid + '\'"><li class="mBuyRecordL">';
                            //E += '<img src2="'+Gobal.imgpath+'/uploads/' + F.thumb + '" src="' + Gobal.LoadPic + '"></li>';
                            //E += '<li class="mBuyRecordR">(第' + F.qishu + "期)" + F.title + "";
                            //if (F.codeState == 3) {
                            //    E += '<p class="mValue">价值：￥' + F.money + '</p><span>获得者：<a style="color: #22AAff" href="'+Gobal.Webpath+'/mobile/mobile/userindex/' + F.q_uid + '">' + F.q_user + '</a><br>幸运码：<em class="orange">' + F.q_user_code + "</em></span>"
                            //} else {
                            //    E += '<div class="pRate"><div class="Progress-bar">';
                            //    if (z == 0) {
                            //        E += '<p class="u-progress"></p>'
                            //    } else {
                            //        E += '<p class="u-progress" title="已完成' + G + '%"><span class="pgbar" style="width: ' + z + '%;"><span class="pging"></span></span></p>'
                            //    }
                            //    E += '<ul class="Pro-bar-li">';
                            //    E += '<li class="P-bar01"><em>' + F.canyurenshu + '</em>已参与</li><li class="P-bar02"><em>' + F.zongrenshu + '</em>总需人次</li><li class="P-bar03"><em>' + (F.zongrenshu - F.canyurenshu) + "</em>剩余</li>";
                            //    E += "</ul></div></div>"
                            //}
                            //E += "</li></ul>"



                            E += "<ul onclick=\"location.href='" + Gobal.Webpath + "/mobile/mobile/item/" + F.shopid + '\'"><li class="mBuyRecordL">';
                            E += '<img src2="' + Gobal.imgpath + '/uploads/' + F.thumb + '" src="' + Gobal.LoadPic + '"></li>';
                            E += '<li class="mBuyRecordR"><div class="title">' + F.title + '</div><p class="txt">期号：' + F.qishu + '</p>';
                            E += '<p class="txt">本期参与：<b>'+F.gonumber+'</b>人次</p>';
                            if (F.codeState == 3) {
                                E += '<div class="gongxi"></div></li>' + '<li class="canyubox"><div class="pzhongj">获得者：<a style="color: #22AAff" href="' + Gobal.Webpath + '/mobile/mobile/userindex/' + F.q_uid + '">' + F.q_user + '</a>' + "</div>"
                            } else {
                                E += '</li><li class="canyubox"><div class="pRate"><div class="Progress-bar">';
                                if (z == 0) {
                                    E += '<p class="u-progress"></p>'
                                } else {
                                    E += '<p class="u-progress" title="已完成' + G + '%"><span class="pgbar" style="width: ' + z + '%;"><span class="pging"></span></span></p>'
                                }
                                E += '<ul class="Pro-bar-li">';
                                E += '<li class="P-zongxu"><b>' + F.zongrenshu + '</b>总需</li><li class="P-shengyu"><b>' + (F.zongrenshu - F.canyurenshu) + "</b>剩余</li>";
                                E += "</ul></div></div>";
                                E += "<a class='link-canyu' href='" + Gobal.Webpath + "/mobile/mobile/item/"+ F.shopid + "' >立即参与</a>";
                            }
                                
                            E += "</li></ul>"
                        }
                        m.append(E);
                        s = "";
                        loadImgFun();
                        if (u > x.EIdx) {
                            _IsLoading = false;
                            k.show();
                            b.hide();
                            BuyRecordFun.CountFalg = true
                        } else {
                            k.hide();
                            b.hide();
                            BuyRecordFun.CountFalg = false
                        }
                    } else {
                        if (I.code == 1) {
                            m.append('<div class="haveNot z-minheight"><s></s><p>TA的好友才可以看哦</p></div>')
                        } else {
                            if (I.code == 2) {
                                m.append('<div class="haveNot z-minheight"><s></s><p>TA未有公开的购买记录哦</p></div>')
                            } else {
                                m.append(s);
                                BuyRecordFun.CountFalg = false
                            }
                        }
                        k.hide();
                        b.hide()
                    }
                };				 			 
	GetJPData(Gobal.Webpath, "ajax", "getUserBuyList"+w(),y)
            };
            this.initData = function() {
                v()
            };
            this.getNextPage = function() {
                x.FIdx += r;
                x.EIdx += r;
                v()
            }
        };
        var i = function() {
            var s = Gobal.NoneHtml;
            this.initFlag = false;
            var t = false;
            var p = false;
            var q = 0;
            var r = 10;
            var u = 0;
            this.CountFalg = false;
            var x = {
                Type: 1,
                UserID: d.val(),
                FIdx: q,
                EIdx: r,
                IsCount: 1
            };
            var w = function() {
                var y = "";
                y += "/" + x.Type;
                y += "/" + x.UserID;
                y += "/" + x.FIdx;
                y += "/" + x.EIdx;
                y += "/" + x.IsCount;
                return y
            };
            var v = function() {
                var y = function(z) {
				 
                    if (z.code == 0) {
                        if (x.IsCount == 1) {
                            u = z.count
                        }
                        var D = z.listItems;
                        var E = D.length;
                        var B = "";
                        if (D <= 0) {
                            B = s
                        }
                        for (var A = 0; A < E; A++) {
                            var C = D[A];
                            //B += "<ul onclick=\"location.href='"+Gobal.Webpath+"/mobile/mobile/item/" + C.id + '\'" class="BuyRecordList"><li class="mBuyRecordL">';
                            //B += '<img src2="'+Gobal.imgpath+'/uploads/' + C.thumb + '" src="' + Gobal.LoadPic + '"></li>';
                            //B += '<li class="mBuyRecordR">(第' + C.qishu + "期)" + C.title + '<p class="mValue">价值：￥' + C.money + "</p>";
                            //B += '<span>幸运码：<em class="orange">' + C.q_user_code + "</em><br>揭晓时间：" + C.q_end_time + " </span></li></ul>"


                    //        <ul onclick="location.href='http://127.0.0.1/1yuangou/?/mobile/mobile/item/461'">
                    //   <li class="mBuyRecordL">
                    //       <img src="http://127.0.0.1/1yuangou/statics/uploads/shopimg/20151205/56494972304707.jpg"></li>
                    //   <li class="mBuyRecordR">
                    //       <div class="title">MacBook Pro MF840CHA 13.3英寸宽屏笔记本电脑</div>
                    //       <p class="txt">期号：8989</p>
                    //       <p class="txt">本期参与：<b>89</b>人次</p>
                    //        <div class="gongxi"></div>
                    //    </li>
                    //    <li class="canyubox">
                    //        <div class="pzhongj">
                    //            获得者：中了一次就OK
                    //        </div>
                    //        <a class="link-canyu" href="">立即参与</a>
                    //    </li>
                    //</ul>
  

                            B += "<ul onclick=\"location.href='" + Gobal.Webpath + "/mobile/mobile/item/" + C.id + '\'" class="BuyRecordList"><li class="mBuyRecordL">';
                            B += '<img src2="' + Gobal.imgpath + '/uploads/' + C.thumb + '" src="' + Gobal.LoadPic + '"></li>';
                            B += '<li class="mBuyRecordR"><div class="title">(第' + C.qishu + "期)" + C.title + '</div><p class="txt">期号：' + C.qishu + "</p>";
                            B += '<p class="txt">本期参与：<b>' + C.gonumber + '</b>人次</p><div class="gongxi"></div></li>' + '<li class="canyubox"><div class="pzhongj">获得者：' + C.q_user + '</div></li></ul>'
                        }
                        e.append(B);
                        s = "";
                        loadImgFun();
                        if (u > x.EIdx) {
                            _IsLoading = false;
                            k.show();
                            b.hide();
                            GetGoodsFun.CountFalg = true
                        } else {
                            k.hide();
                            b.hide();
                            GetGoodsFun.CountFalg = false
                        }
                    } else {
                        if (z.code == 1) {
                            e.append('<div class="haveNot z-minheight"><s></s><p>TA的好友才可以看哦</p></div>')
                        } else {
                            if (z.code == 2) {
                                e.append('<div class="haveNot z-minheight"><s></s><p>TA未有公开的获得商品记录哦</p></div>')
                            } else {
                                e.append(s);
                                GetGoodsFun.CountFalg = false
                            }
                        }
                        k.hide();
                        b.hide()
                    }
                };
	GetJPData(Gobal.Webpath, "ajax", "getUserBuyList"+w(),y)
            };
            this.initData = function() {
                v()
            };
            this.getNextPage = function() {
                x.FIdx += r;
                x.EIdx += r;
                v()
            }
        };
        var n = function() {
            var s = Gobal.NoneHtml;
            this.initFlag = false;
            var t = false;
            var p = false;
            var q = 0;
            var r = 10;
            var u = 0;
            this.CountFalg = false;
            var x = {
                Type: 2,
                UserID: d.val(),
                FIdx: q,
                EIdx: r,
                IsCount: 1
            };
            var w = function() {
                var y = "";
                y += "/" + x.Type;
                y += "/" + x.UserID;
                y += "/" + x.FIdx;
                y += "/" + x.EIdx;
                y += "/" + x.IsCount;
                return y
            };
            var v = function() {
                var y = function(H) {				 
                    if (H.code == 0) {
                        if (x.isCount == 1) {
                            u = H.count
                        }
                        var B = H.listItems;
                        var D = B.length;						
                        var F = "";
                        var z = "";
                        if (B <= 0) {
                            F = s
                        }
                        for (var E = 0; E < D; E++) {
                            var G = B[E];
                            z = G.sd_content;
                            if (z.length > 75) {
                                z = z.substring(0, 75) + "..."
                            }
                            //F += '<li><a href="'+Gobal.Webpath+'/mobile/shaidan/detail/' + G.sd_id + '"><h3><b>' + G.sd_title + "</b><em>" + G.sd_time + "</em></h3>";
                            //F += "<p>" + z + "</p><dl>";
                            //var A = G.sd_thumbs.split(",");
                            //for (var C = 0; C < A.length; C++) {
                            //    F += '<img src2="'+Gobal.imgpath+'/uploads/' + A[C] + '" src="' + Gobal.LoadPic + '">';
                            //    if (C >= 2) {
                            //        break
                            //    }
                            //}
                            //F += "</dl></a></li>"

                    //        <div class="cSingleInfobg">
                    //<div class="cSingleInfo">
                    //    <div class="fl hdtime">
                    //        <time class="date">01</time>
                    //        <time class="month">09月</time>
                    //    </div>

                    //    <div class="cSingleR m-round" id="23">
                    //        <ul>
                    //            <li>获得商品：MacBook Pro MF840CHA 13.3英寸宽屏笔记本电脑</li>
                    //            <li class="maxImg">
                    //                <span>哈哈哈哈哈哈</span></li>
                    //            <li class="maxImg">
                    //                <img src="http://127.0.0.1/1yuangou/statics/uploads//1yuangoushaidan/00005090000007089087201605091142440000.jpeg">
                    //            </li>

                    //            <li class="comment">
                    //                <dd><s></s><strong>1</strong></dd>
                    //                <dd><i></i><strong>1</strong></dd>
                    //            </li>
                    //        </ul>
                    //    </div>
                    //</div>
                    //</div>


                            F += "<div class='cSingleInfobg' onclick=\"location.href='" + Gobal.Webpath + '/mobile/shaidan/detail/' + G.sd_id + '"><div class="cSingleInfo">';
                            F += "<div class='fl hdtime'><time class='date'>"+G.DD+"</time><time class='month'>"+G.MM+"月</time></div>";
                            F += "<div class='cSingleR m-round'><ul><li>获得商品："+G.title+"</li>";
                            F += "<li class='maxImg'><span>" + z + "</span></li><li class='maxImg'>";
                            var A = G.sd_thumbs.split(",");
                            for (var C = 0; C < A.length; C++) {
                                F += '<img src2="' + Gobal.imgpath + '/uploads/' + A[C] + '" src="' + Gobal.LoadPic + '">';
                                if (C >= 2) {
                                    break
                                }
                            }
                            F += "</li><li class='comment'><dd><s></s><strong>1</strong></dd><dd><i></i><strong>1</strong></dd></li></ul></div></div></div>"

                        }
                        j.children().append(F);
                        s = "";
                        loadImgFun();
                        if (u > x.EIdx) {
                            _IsLoading = false;
                            k.show();
                            b.hide();
                            SingleFun.CountFalg = true
                        } else {
                            k.hide();
                            b.hide();
                            SingleFun.CountFalg = false
                        }
                    } else {
                        if (H.code == 1) {
                            j.append('<div class="haveNot z-minheight"><s></s><p>TA的好友才可以看哦</p></div>')
                        } else {
                            if (H.code == 2) {
                                j.append('<div class="haveNot z-minheight"><s></s><p>TA未有公开的晒单记录哦</p></div>')
                            } else {
                                j.children().append(s);
                                SingleFun.CountFalg = false
                            }
                        }
                        k.hide();
                        b.hide()
                    }
                };
	GetJPData(Gobal.Webpath, "ajax", "getUserBuyList"+w(),y)
            };
            this.initData = function() {
                v()
            };
            this.getNextPage = function() {
                x.FIdx += r;
                x.EIdx += r;
                v()
            }
        };
        var f = function() {
            b.show();
            switch (c) {
            case 0:
                BuyRecordFun.getNextPage();
                break;
            case 1:
                GetGoodsFun.getNextPage();
                break;
            case 2:
                SingleFun.getNextPage();
                break
            }
        };
        BuyRecordFun = new g();
        BuyRecordFun.initData();
        GetGoodsFun = new i();
        SingleFun = new n();
        k.bind("click",
        function() {
            k.hide();
            b.show();
            f()
        });
        isLoaded = true
    };
    a()
});