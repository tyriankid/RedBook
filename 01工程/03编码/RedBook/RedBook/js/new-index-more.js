//页面下路加载更多
var indexBox  = new Vue({
    el: "#indexBox",
    data: {
        dataUrl: "http://localhost:8095/rdbook?action=getBookList",
        imgUrl: [],
        MainTitle: [],
        SubTitle: [],
        headimg: [],
        FabulousCount: [],
        WatchCount: [],
        p: "1",
        pagesize:"3"
    },
    created: function () {
        this.$http.post(this.dataUrl, {
            p: this.p,
            pagesize: this.pagesize
        }, { emulateJSON: true }).then(function (respnoe) {
            for (var i = 0; i < respnoe.body.data.length;i++){
                this.imgUrl.push(respnoe.body.imgroot + respnoe.body.data[i].BackImgUrl);
                this.MainTitle.push(respnoe.body.data[i].MainTitle);
                this.SubTitle.push(respnoe.body.data[i].SubTitle);
                this.headimg.push(respnoe.body.data[i].headimg);
                this.FabulousCount.push(respnoe.body.data[i].FabulousCount);
                this.WatchCount.push(respnoe.body.data[i].WatchCount);
            }
        }, function (res) {
            alert(404)
        })
    }
})
