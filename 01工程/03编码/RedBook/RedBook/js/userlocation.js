//var getLocation = {
//    //浏览器原生获取经纬度方法
//    latAndLon: function (callback, error) {
//        var that = this;
//        if (navigator.geolocation) {
//            navigator.geolocation.getCurrentPosition(function (position) {
//                var latitude = position.coords.latitude;
//                var longitude = position.coords.longitude;
//                localStorage.setItem("latitude", latitude);
//                localStorage.setItem("longitude", longitude);
//                var data = {
//                    latitude: latitude,
//                    longitude: longitude
//                };
//                if (typeof callback == "function") {
//                    callback(data);
//                }
//            },
//                function () {
//                    if (typeof error == "function") {
//                        error();
//                    }
//                });
//        } else {
//            if (typeof error == "function") {
//                error();
//            }
//        }
//    },

//将经纬度转换成城市名和街道地址，参见百度地图接口文档：http://developer.baidu.com/map/index.php?title=webapi/guide/webservice-geocoding
//cityname: function (latitude, longitude, callback) {
//    $.ajax({
//        url: 'http://api.map.baidu.com/geocoder/v2/?ak=btsVVWf0TM1zUBEbzFz6QqWF&callback=renderReverse&location=' + latitude + ',' + longitude + '&output=json&pois=1',
//        type: "get",
//        dataType: "jsonp",
//        jsonp: "callback",
//        success: function (data) {
//            console.log(data);
//            var province = data.result.addressComponent.province;
//            var cityname = (data.result.addressComponent.city);
//            var district = data.result.addressComponent.district;
//            var street = data.result.addressComponent.street;
//            var street_number = data.result.addressComponent.street_number;
//            var formatted_address = data.result.formatted_address;
//            localStorage.setItem("province", province);
//            localStorage.setItem("cityname", cityname);
//            localStorage.setItem("district", district);
//            localStorage.setItem("street", street);
//            localStorage.setItem("street_number", street_number);
//            localStorage.setItem("formatted_address", formatted_address);
//            //domTempe(cityname,latitude,longitude);
//            var data = {
//                latitude: latitude,
//                longitude: longitude,
//                cityname: cityname,
//                province: province,
//                district: district,
//                street: street,
//                number: street_number,
//                formataddress: formatted_address
//            };
//            if (typeof callback == "function") {
//                callback(data);
//            }

//        }
//    });
//},
////设置默认城市
//setDefaultCity: function (callback) {
//    //alert("获取地理位置失败！");
//    //默认经纬度
//    var latitude = "30.59306";
//    var longitude = "114.214415";
//    var cityname = "武汉市";
//    var province = "湖北省";
//    var district = "硚口区";
//    var street = "沿河大道";
//    var street_number = "88号";
//    var formatted_address = "湖北省武汉市硚口区沿河大道88号";
//    localStorage.setItem("latitude", latitude);
//    localStorage.setItem("longitude", longitude);
//    localStorage.setItem("cityname", cityname);
//    localStorage.setItem("province", province);
//    localStorage.setItem("district", district);
//    localStorage.setItem("street", street);
//    localStorage.setItem("street_number", street_number);
//    localStorage.setItem("formatted_address", formatted_address);
//    var data = {
//        latitude: latitude,
//        longitude: longitude,
//        cityname: cityname,
//        province: province,
//        district: district,
//        street: street,
//        number: street_number,
//        formataddress: formatted_address
//    };
//    if (typeof callback == "function") {
//        callback(data);
//    }
//},
////更新地理位置
//refresh: function (callback) {
//    var that = this;
//    //重新获取经纬度和城市街道并设置到localStorage
//    that.latAndLon(
//        function (data) {
//            that.cityname(data.latitude, data.longitude, function (datas) {
//                if (typeof callback == "function") {
//                    callback();
//                }
//            });
//        },
//        function () {
//            that.setDefaultCity(function () {
//                if (typeof callback == "function") {
//                    callback();
//                }
//            });
//        });
//}

//微信JS-SDK获取经纬度方法：http://mp.weixin.qq.com/wiki/7/aaa137b55fb2e0456bf8dd9148dd613f.html
wx.ready(function () {
    wx.getLocation({
        success: function (res) {
            var latitude = res.latitude; // 纬度，浮点数，范围为90 ~ -90
            var longitude = res.longitude; // 经度，浮点数，范围为180 ~ -180。
            var speed = res.speed; // 速度，以米/每秒计
            var accuracy = res.accuracy; // 位置精度
            localStorage.setItem("latitude", latitude);
            localStorage.setItem("longitude", longitude);
            /*var point = {
                latitude: latitude,
                longitude: longitude
            };*/
            // 百度地图API功能
            var point = new BMap.Point(longitude, latitude);
            var geoc = new BMap.Geocoder();
            geoc.getLocation(point, function (rs) {
                var addComp = rs.addressComponents;
                //alert("您的位置：" + addComp.province + ", " + addComp.city + ", " + addComp.district + ", " + addComp.street + ", " + addComp.streetNumber);
                //提交到内部接口
                if (addComp.city != null) {
                    var cityname = addComp.city.substring(0, 2);
                    var ajaxUrl = "http://" + window.location.host + "/ilerruser?action=updateuseripcity&cityname=" + cityname;//更新COOKIE，并更新
                    $.ajax({
                        type: 'post', dataType: 'json', timeout: 10000,
                        url: ajaxUrl,
                        success: function (data) {
                            //执行完成暂无操作
                        }
                    })
                }
            });

        },
        cancel: function () {
            //这个地方是用户拒绝获取地理位置
            if (typeof error == "function") {
                error();
            }
        }
    });

});
wx.error(function (res) {
    if (typeof error == "function") {
        error();
    }
});


//function readCookie (name)
//{
//    var cookieValue = "";
//    var search = name + "=";
//    if (document.cookie.length > 0)
//    {
//        offset = document.cookie.indexOf (search);
//        if (offset != -1)
//        {
//            offset += search.length;
//            end = document.cookie.indexOf (";", offset);
//            if (end == -1)
//                end = document.cookie.length;
//            cookieValue = unescape (document.cookie.substring (offset, end))
//        }
//    }
//    return cookieValue;
//}



//function getUserip() {
//    var myprovince = "";
//    var mycity = "";
//    var ip = "";
//    var url = 'http://chaxun.1616.net/s.php?type=ip&output=json&callback=?&_=' + Math.random();
//    $.ajax({
//        type: "POST",
//        url: url,
//        async: false,
//        error: function (request) {
//        },
//        success: function (data) {
//            ip = data.Ip;
//        }
//    });
//    url = "http://ip.taobao.com/service/getIpInfo.php?ip=" + ip;
//    $.ajax({
//        type: "POST",
//        url: url,
//        async: false,
//        error: function (request) {
//        },
//        success: function (json) {
//            myprovince = json.data.area;
//            mycity = json.data.region;
//        }
//    });
//    //alert(myprovince + " " + mycity + " IP" + data.Ip);
//    if (myprovince == mycity) myprovince = "";
//    return myprovince + " " + mycity+ " IP"+ data.Ip;
//}

///**************调用****************/

////原生浏览器获取经纬度方法
//getLocation.latAndLon(
//    function (data) {
//        //data包含经纬度信息
//        getLocation.cityname(data.latitude, data.longitude, function (datas) {
//            //datas包含经纬度信息和城市
//            //var result = datas.latitude + "|" + datas.longitude + "|" + datas.cityname + "|" + datas.province
//            //    + "|" + datas.district + "|" + datas.street + "|" + datas.number + "|" + datas.formataddress
//            $.cookie('user-location', datas.cityname); //设置cookie的值
//            //alert("您的位置：" + datas.cityname);
//            //提交到内部接口
//            if (datas.cityname != null) {
//                var cityname = datas.cityname + "";
//                var ajaxUrl = "http://" + window.location.host + "/ilerruser?action=updateuseripcity&cityname=" + cityname + "&userip=" + getUserip();//更新COOKIE，并更新
//                $.ajax({
//                    type: 'post', dataType: 'json', timeout: 10000,
//                    url: ajaxUrl,
//                    success: function (data) {
//                        //执行完成暂无操作
//                    }
//                })
//            }
//        });
//    },
//    function () {
//        getLocation.setDefaultCity(
//            function (defaultData) {
//                url = "http://" + window.location.host + "/ilerruser?action=updateuseripcity&cityname=&userip=";//更新COOKIE
//                $.ajax({
//                    type: "POST",
//                    url: url,
//                    async: false,
//                    success: function (json) {
//                    }
//                });
//            }
//        );
//    }
//);

