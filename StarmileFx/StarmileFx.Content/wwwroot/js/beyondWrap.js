var blockBeyondWrap = function (wrap) {
    var self = this;
    this.one = wrap.showBox;
    this.two = wrap.display;
    this.handle = wrap.handle;
    this.btn = wrap.showBtn;
    $(window).resize(function () { self.wrapHandle() });
}
blockBeyondWrap.prototype = {
    wrapHandle: function () {
        var self = this;
        var oneWidth = $(self.one).width(), allInfo = self.getAllWidth({ one: self.one, two: self.two, wrap: self.wrap, wid: oneWidth });
        allInfo.wid > oneWidth && self.moveToOther({ two: self.two, mov: allInfo.mov });
        $(self.two).children().length > 0 ? $(self.btn).show() : $(self.btn).hide();
        typeof self.handle == "function" && self.handle();
    },
    getAllWidth: function (wrap) {
        var self = this, reW = 0, mov = [];
        var childs = $(wrap.one).children();
        var myFun = {
            moveOther: function () {
                for (var i = 0; i < childs.length; i++) {
                    reW += (childs.eq(i).outerWidth() + myFun.getMarginW(childs.eq(i)));
                    wrap.wid < reW && mov.push(childs[i]);
                }
            },
            moveBack: function () {
                if (reW < wrap.wid && $(wrap.two).children().length > 0) {//如果位置不够时候从另外一个容器获取放到显示容器
                    $(wrap.one).append($(wrap.two).children().eq($(wrap.two).children().length - 1));
                    reW += ($(wrap.one).children().eq($(wrap.one).children().length - 1).outerWidth() + myFun.getMarginW($(wrap.one).children().eq($(wrap.one).children().length - 1)));
                    reW > wrap.wid ? $(wrap.two).append($(wrap.one).children().eq($(wrap.one).children().length - 1)) : myFun.moveBack();
                }
            },
            getMarginW: function (wrap) {//获取margin-left和margin-right的值
                var thisW = 0;
                if (wrap.length > 0) {
                    wrap.css('margin-left') && wrap.css('margin-left').indexOf('px') >= 0 && (thisW += parseInt(wrap.css('margin-left')));
                    wrap.css('margin-right') && wrap.css('margin-right').indexOf('px') >= 0 && (thisW += parseInt(wrap.css('margin-right')));
                }
                return thisW;
            }
        }
        $(wrap.one).length > 0 && childs.length > 0 && myFun.moveOther();
        myFun.moveBack();
        return { wid: reW, mov: mov };//返回子元素占据的所有宽度之和以及所有超出的元素
    },
    moveToOther: function (wrap) {//将超出的放到下拉位置
        var self = this;
        for (var i = 0; i < wrap.mov.length; i++) {
            $(wrap.two).append(wrap.mov[i]);
        }
    }
}
