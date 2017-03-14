(function (window) {
    var pullDownEl, pullDownOffset,
        pullUpEl, pullUpOffset,
        generatedCount = 0,
        backgroundPosX = 0,
        iconjump, pullUpState = 1;
    //默认值，当不传值的时候
    loaded.prototype.options = {
        pullDownEvent: "",
        pullUpEvent: ""
    }
    loaded.prototype._init = function (options) {
        extend(this.options, options);
        var pullDownEvent = this.options.pullDownEvent,
            pullUpEvent = this.options.pullUpEvent;
        pullDownEl = document.getElementById('pullDown');
        pullDownOffset = pullDownEl.offsetHeight;
        pullUpEl = document.getElementById('pullUp');
        pullUpOffset = pullUpEl.offsetHeight;
        myScroll = new iScroll('wrapper', {
            useTransition: true,
            vScrollbar: false,
            topOffset: pullDownOffset,
            click:false,
            bounce: true,
            onRefresh: function () {
                if (pullDownEl.className.match('loadEnding')) {
                    window.clearInterval(iconjump);
                    $(".pullDownIcon").css('background-position-x', 0);
                    pullDownEl.className = '';
                    pullDownEl.querySelector('.pullDownLabel').innerHTML = '下拉刷新...';
                }
            },
            onScrollMove: function () {
                //console.log(this.y + ',' + this.maxScrollY);
                if (this.y > -50 && !pullDownEl.className.match('flip')) {
                    window.clearInterval(iconjump);
                    
                    iconjump = setInterval(function () {
                        if ($(".pullDownIcon").css('background-position-x') == '-560px') {
                            backgroundPosX = 0;
                        }
                        backgroundPosX -= 80;
                        $(".pullDownIcon").css('background-position-x', backgroundPosX + 'px');
                    }, 100);
                    pullDownEl.className = 'flip';
                    pullDownEl.querySelector('.pullDownLabel').innerHTML = '松开刷新...';
                    this.minScrollY = -50;
                } else if (this.y < -50 && pullDownEl.className.match('flip')) {
                    window.clearInterval(iconjump);
                    pullDownEl.className = '';
                    pullDownEl.querySelector('.pullDownLabel').innerHTML = '下拉刷新...';
                    this.minScrollY = -pullDownOffset;
                }
            },
            onScrollEnd: function () {
                if (pullDownEl.className.match('flip')) {
                    $('.pullUpLabel').text('加载中');
                    $('.pullUpLabel').prev().show();
                    pullDownEl.className = 'loadEnding';
                    $('.pullDownLabel').text('努力加载中...')
                    pullDownAction(pullDownEvent);

                } else {
                    pullUpAction(pullUpEvent);
                }
            }
        });

        setTimeout(function () {
            document.getElementById('wrapper').style.left = '0';
        }, 800);

        
    }

    //下拉事件,从新加载元素,参数为true表示重新加载
    function pullDownAction(pullDownEvent) {
        eval('' + pullDownEvent + '()');
    }

    function pullUpAction(pullUpEvent) {
        if (nextPage != 0) {
            $('.pullUpLabel').text('加载中');
            $('.pullUpLabel').prev().show();
        }
        if (nextPage == 0) {
            $('.pullUpLabel').text('已加载完全部商品');
            $('.pullUpLabel').prev().hide();
            return;
        }
        eval('' + pullUpEvent + '()');
    }

    function extend(a, b) {
        for (var key in b) {
            if (b.hasOwnProperty(key)) {
                a[key] = b[key];
            }
        }
        return a;
    }
    function loaded(options) {
        this._init(options);
    }
    window.loaded = loaded;
})(window);
