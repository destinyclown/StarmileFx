(function(root, factory) {
    if (typeof exports === 'object' && typeof module === 'object') {
        module.exports = factory(require('jquery'));
    } else if(typeof define === 'function' && define.amd){
        define(['jquery'], factory);
    } else if(typeof exports === 'object'){
        factory(require('jquery'));
    } else{
        factory(root['jQuery']);
    }
})(this, function($) {
    var iframe_settings = {
        type: 2,
        title: false,
        shadeClose: true,
        maxmin: true,
        fix: false,
        area: ['800px', 460],
        scrollbar: false,
        cancel: function (index) {
        }
    };

    var msg_settings = {
        type: 1,
        title: false,
        closeBtn: false,
        shadeClose: true,
        offset: ['250px', ''],
        area: [240, 50],
        class: 'message-navbar',
        time: 40,
        btns: 0,
        page: { html: '<i class="icon-comment-alt"></i>  请至少选择一行数据。' }
    };

    $.extend($, {
        iframe_box: function (args) {
            args = $.extend({}, iframe_settings, args);
            var other = {
            	min:args.min,
            	restore:args.restore,
            	full:args.full,
            	cancel:args.cancel,
            	end:args.end,
            }
            args['min'] = function(){
            	try {
	            	typeof other.min=="function"?other.min():null;
	            	top!==window?window.parent.$('body').find('.layui-layer-setwin').css({'display':''}):null;
	            } catch (e) {}
            };
            args['restore'] = function(){
            	try {
	            	typeof other.restore=="function"?other.restore():null;
	            	top!==window?window.parent.$('body').find('.layui-layer-setwin').css({'display':''}):null;
	            } catch (e) {}
            };
            args['full'] = function(){
            	try {
	            	typeof other.full=="function"?other.full():null;
	            	top!==window?window.parent.$('body').find('.layui-layer-setwin').css({'display':''}):null;
	            } catch (e) {}
            };
            args['cancel'] = function(){
            	try {
	            	typeof other.cancel=="function"?other.cancel():null;
	            	top!==window?window.parent.$('body').find('.layui-layer-setwin').css({'display':''}):null;
	            } catch (e) {}
            };
            args['end'] = function(){
            	try {
	            	typeof other.end=="function"?other.end():null;
	            	top!==window?window.parent.$('body').find('.layui-layer-setwin').css({'display':''}):null;
	            } catch (e) {}
            };
            return layer.open(args);
        },
        //询问框
        confirm: function (msg,fn) {
            layer.confirm(msg, fn);
        },
        msg_box: function (msg, args) {
            args = $.extend({}, msg_settings, args);
            if (msg) {
                args.page.html = '<span class="xubox_msg xulayer_png32 xubox_msgico xubox_msgtype0" style="top: 8px;"></span><span class="xubox_msg xubox_text" style="margin-top: 11px;color:rgb(210, 40, 40)">' + msg + '</span>';
            }
            return layer.alert(msg);
        },
        msg_success: function (msg, args, fnCallback) {
            var success_args = {
                offset: 0,
                icon: 1,
                shadeClose: true,
                shade: 0,
                time: 2000
            };
            args = $.extend({}, success_args, args);
            return layer.msg(msg, args, fnCallback);
        },
        msg_warn: function (msg, args, fnCallback) {
            var warn_args = {
                offset: 0,
                shadeClose: true,
                shade: 0.4,
                icon: 0,
                time: 0
            };
            args = $.extend({}, warn_args, args);
            return layer.msg(msg, args, fnCallback);
        },
        msg_error: function (msg, args, fnCallback) {
            var error_args = {
                icon: 2,
                shadeClose: true
            };
            args = $.extend({}, error_args, args);
            return layer.alert(msg, args, fnCallback);
        }
    });

});
