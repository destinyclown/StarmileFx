/*------------------------------------------------------
     *基于bootstrap模态窗口的简单扩展
     *作者：strmile
-------------------------------------------------------*/
/*----------------------------------------------------------------------------------------------------
1、
------------------------------------------------------------------------------------------------------*/
(function ($) {

    $.fn.wDialog = function (title, remote, type, saveEvent, openEvent, closeEvent) {
        var defaults = {
            id: "modal",//弹窗id
            width: "600",//弹窗宽度，暂时不支持%
            height: "500",//弹窗高度,不支持%
            backdrop: true,//是否显示遮障，和原生bootstrap 模态框一样
            keyboard: true,//是否开启esc键退出，和原生bootstrap 模态框一样
        };

        //动态创建窗口
        var creatDialog = {
            init: function (opts) {
                var _self = this;

                //动态插入窗口
                var d = _self.dHtml(opts);
                $("body").append(d);

                var modal = $("#" + opts.id);

                //初始化窗口
                //modal.modal(opts);                
                modal.modal({
                    backdrop: opts.backdrop,
                    keyboard: opts.keyboard
                });
                $(".modal-body").on('load', remote);

                //窗口大小位置
                //var h = modal.height() - modal.find(".modal-header").outerHeight() - modal.find(".modal-footer").outerHeight() - 5;
                //modal.css({ 'margin-left': opts.width / 2 * -1, 'margin-top': opts.height / 2 * -1, 'top': '50%' }).find(".modal-body").innerHeight(h);
                modal.css({
                    position: "absolute",
                    left: ($(window).width() - opts.width) / 2,
                    top: ($(document).height() - opts.height) / 2 - 150
                });
                $(".modal-body").css({
                    height: opts.height - 115
                });
                modal
                //显示窗口
                .modal('show')
                //隐藏窗口后删除窗口html
                .on('hidden', function () {
                    modal.remove();
                    $(".modal-backdrop").remove();
                    if (typeof (closeEvent) == "function") {
                        closeEvent();
                    }
                })
                //窗口显示后
                .on('shown', function () {

                    if (typeof(openEvent) == "function") {
                        openEvent();
                    }


                });
                //绑定按钮事件
                $(".ok").click(function () {
                    if (typeof (saveEvent) == "function") {
                        var ret = saveEvent();
                        if (ret) {
                            modal.modal('hide');
                        }
                    }
                });
            },
            dHtml: function (o) {
                if (!type || type == "Dialog") {
                    return '<div id="' + o.id + '" class="modal fade" role="dialog" tabindex="-1" aria-labelledby="myModalLabel" aria-hidden="true" >'
                        + '<div class="modal-dialog" style="display:table-cell"><div class="modal-content" style="width:' + o.width + 'px;height:' + o.height + 'px;">'
                        + '<div class="modal-header"><button type="button" class="close" data-dismiss="modal" ><span aria-hidden="true">&times;</span>'
                        + '<span class="sr-only">Close</span></button><h4 id="myModalLabel" class="modal-title">' + title + '</h4></div><div class="modal-body" ><p>正在加载...</p></div>'
                        + '<div class="modal-footer"><button class="btn btn-primary ok">保存</button>'
                        + '<button class="btn" data-dismiss="modal" aria-hidden="true">取消</button>'
                        + '</div></div></div></div>';
                } else if (type == "alert") {
                    return '<div id="' + o.id + '" class="modal fade" role="dialog" tabindex="-1" aria-labelledby="myModalLabel" aria-hidden="true" >'
                    + '<div class="modal-dialog" style="display:table-cell"><div class="modal-content" style="width: 600px;height: 200px;">'
                    + '<div class="modal-header" style="border-bottom:0;"><button type="button" class="close" data-dismiss="modal" ><span aria-hidden="true">&times;</span>'
                    + '<span class="sr-only">Close</span></button><h4 id="myModalLabel" class="modal-title">' + title + '</h4></div><div class="modal-body" >' + remote + '</div>'
                    + '<div class="modal-footer" style="border-top:0;"><button class="btn btn-primary ok" data-dismiss="modal" aria-hidden="true">确定</button>'
                    + '</div></div></div></div>';
                } else if (type == "confirm") {
                    return '<div id="' + o.id + '" class="modal fade" role="dialog" tabindex="-1" aria-labelledby="myModalLabel" aria-hidden="true" >'
                    + '<div class="modal-dialog" style="display:table-cell"><div class="modal-content" style="width: 600px;height: 200px;">'
                    + '<div class="modal-header" style="border-bottom:0;"><button type="button" class="close" data-dismiss="modal" ><span aria-hidden="true">&times;</span>'
                    + '<span class="sr-only">Close</span></button><h4 id="myModalLabel" class="modal-title">' + title + '</h4></div><div class="modal-body" >' + remote + '</div>'
                    + '<div class="modal-footer" style="border-top:0;"><button class="btn btn-primary ok" data-dismiss="modal" aria-hidden="true">确定</button>'
                    + '<button class="btn" data-dismiss="modal" aria-hidden="true">取消</button>'
                    + '</div></div></div></div>';
                }
            }
        };

        return this.each(function () {
            $(this).click(function () {
                var opts = $.extend({}, defaults, {
                    id: $(this).attr("data-id"),
                    width: $(this).attr("data-width"),
                    height: $(this).attr("data-height"),
                    backdrop: $(this).attr("data-backdrop"),
                    keyboard: $(this).attr("data-keyboard")
                });
                creatDialog.init(opts);
            });
        });

    };

})(jQuery);