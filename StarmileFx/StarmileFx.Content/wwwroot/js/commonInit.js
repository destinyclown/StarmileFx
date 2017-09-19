/**
 * 仅存放全局最基础常用控件初始化等，尽可能保持精简
 */
define([
    'jquery'
], function ($) {
    $(function () {
        // 日期选择
        //$('[data-datetimepicker]').each(function () {
        //    $(this).datetimepicker({
        //        minDate: $(this).data('mindate') || undefined,
        //        maxDate: $(this).data('maxdate') || undefined,
        //        format: $(this).data('datetimepicker'),
        //        showTodayButton: true,
        //        showClear: true
        //    })
        //});
    });
    $.loading = function (bool, text) {
        var $loadingpage = top.$("#loadingPage");
        var $loadingtext = $loadingpage.find('.loading-content');
        if (bool) {
            $loadingpage.show();
        } else {
            if ($loadingtext.attr('istableloading') == undefined) {
                $loadingpage.hide();
            }
        }
        if (!!text) {
            $loadingtext.html(text);
        } else {
            $loadingtext.html("数据加载中，请稍后…");
        }
        $loadingtext.css("left", (top.$('body').width() - $loadingtext.width()) / 2 - 50);
        $loadingtext.css("top", (top.$('body').height() - $loadingtext.height()) / 2);
    }
    $(window).on('load', function () {
        window.setTimeout(function () {
            $('#ajax-loader').fadeOut();
        }, 300);
    });
})
