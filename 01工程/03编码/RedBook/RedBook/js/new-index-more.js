//页面下路加载更多
var indexBox = new Vue({
    el: "#indexBox",
    data: {
        dataUrl: "http://" + window.location.host + "/rdbook?action=getBookList",
        showData: [],
        p: 1,
        pagesize: 3,
        dzCount: "",
        imgUrl:[],
    },
}); 
$(document).ready(function () {
    showData();
    $(document).delegate(".dibu_a", "click", function () {
        if ($(this).find("i").hasClass("activeI")) {
            $(this).find("i").removeClass("activeI");
        } else {
            $(this).find("i").addClass("activeI");
        }
    });
    setTimeout(function () {
        bgImg();
        function bgImg() {
            $(".shoppers").find("li").map(function () {
                var ind = $(this).index();
                console.log(indexBox.imgUrl.length)
                $(this).css({ "backgroundImage": "url(" + indexBox.imgUrl[ind] + ")" });
            });
        };
        $(window).scroll(function () {
            if ($(document).scrollTop() >= $(document).height() - $(window).height()) {
                showData();
                setTimeout(function () {
                    bgImg();
                },500)
            }
        })
    }, 500);
    function showData() {
        indexBox.$http.post(indexBox.dataUrl, {
            p: indexBox.p,
            pagesize: indexBox.pagesize
        }, { emulateJSON: true }).then(function (res) {
            
            for (var i = 0; i < res.body.data.length; i++) {
                indexBox.imgUrl.push(res.body.imgroot + res.body.data[i].BackImgUrl);
            }
            console.log(indexBox.imgUrl.length)
            //indexBox.imgUrl = indexBox.imgUrl.concat(res.body.imgroot + res.body.data.BackImgUrl)
            indexBox.showData = indexBox.showData.concat(res.body.data);
            indexBox.p = res.body.nextPage;
        }, function (res) {
            alert(404)
        });
    }
})

