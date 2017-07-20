/*
name : common.js
version : v1.0
author : starmile
date : 2017-07-21
*/

$(function () {
    // 检测是否微信打开
    if (!IsWeChat()) {
        //alert("请用微信打开网页！");
    }
})

// 是否微信
function IsWeChat() {
    var ua = window.navigator.userAgent.toLowerCase();
    if(ua.match(/MicroMessenger/i) == 'micromessenger'){
        return true;
    }else{
        return false;
    }
}

// 模板方法
function _LayoutNav() {
    $('#home').click(function(){
        $('.footerNav').find().removeClass('cur');
        this.parent().addClass('cur');
    });
    $('#category').click(function(){
        $('.footerNav').find().removeClass('cur');
        this.parent().addClass('cur');
    });
    $('#shoppingcart').click(function(){
        $('.footerNav').find().removeClass('cur');
        this.parent().addClass('cur');
    });
    $('#usercenter').click(function(){
        $('.footerNav').find().removeClass('cur');
        this.parent().addClass('cur');
    });
}