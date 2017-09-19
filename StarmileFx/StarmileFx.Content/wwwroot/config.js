/**
 * requirejs config
 */
(function() {
    var _config = {
        urlArgs: 'v=1791104738',
        baseUrl: 'https://content.starmile.com.cn',//正式
        //baseUrl: 'http://localhost:8004',//测试
        map: {
            '*': {
                'css': 'css'
            }
        },

        paths: {
            //--starmilefx
            'ui': 'js/ui.min',
            'introjs': 'js/intro.min',
            'commonInit': 'js/commonInit', // 全局基础控件初始化脚本
            'starmilefx': 'js/starmilefx', // 框架基础
            'starmilefx-tree': 'js/starmilefx-tree', // 菜单架构
            'ChineseToPinyin': 'js/ChineseToPinyin', // 菜单架构

            //--第三方控件

            //--bootstrap
            'tether': 'lib/tether/dist/js/tether',
            'bootstrap': 'lib/bootstrap/dist/js/bootstrap',
            'bootstrap-datetimepicker': 'lib/bootstrap-datetimepicker/build/js/bootstrap-datetimepicker.min',
            'bootstrapvalidator': 'lib/bootstrapvalidator/dist/js/bootstrapValidator',
            'bootstrapvalidator-cn': 'lib/bootstrapvalidator/dist/js/language/zh_CN',

            //-- jQuery UI
            'jquery-ui': 'lib/jquery-ui/jquery-ui.min',

            //-- jqGrid
            'jqGrid': 'lib/jqGrid/js/jqGrid.min',

            //-- layer
            'layer': 'lib/layer/build/layer',     
            '_layer-addon': 'js/jquery.layer.ext',

            //-- laydate
            'laydate':'lib/laydate/src/laydate',

            'indextab': 'js/indextab',
            'Default': 'js/Default',
            'amazeui': 'js/amazeui.min',
            'Chart': 'js/Chart',
            'font': 'css!lib/font-awesome/css/font-awesome.min.css'
        },

        shim: {
            //-- starmilefx     
            'ui': ['jquery', 'css!css/ui.min.css'],
            'introjs': ['css!css/introjs.min.css'],
            'starmilefx': ['css!css/starmilefx.css'], // 框架基础           
            'commonInit': ['css!css/commonInit.css'], // 全局基础控件初始化样式
            'starmilefx-login': ['css!css/starmilefx-login.css'],
            'starmilefx-tree': ['ChineseToPinyin', 'css!css/starmilefx-tree.css', 'jquery', 'js/beyondWrap', 'js/pageScroll'],


            //--第三方控件

            //--bootstrap
            'tether': ['jquery'],
            'bootstrap': ['jquery'],
            'bootstrapvalidator-cn': ['bootstrapvalidator'],
            'bootstrapvalidator': ['bootstrap', 'css!lib/bootstrapvalidator/dist/css/bootstrapValidator.min.css'],
            'bootstrap-datetimepicker': ['bootstrap', 'css!lib/bootstrap-datetimepicker/build/css/bootstrap-datetimepicker.min.css'],

            //-- jQuery UI
            'jquery-ui': ['jquery', 'css!jquery-ui/themes/base/jquery-ui.min.css'],

            //-- jqGrid
            'jqGrid': ['jquery', 'css!jqGrid/css/ui.jqgrid.css'],

            //-- layer
            'layer': ['css!lib/layer/build/skin/default/layer.css'],

            //-- laydate
            'laydate': ['css!lib/laydate/src/theme/default/laydate.css'],
        }
    }

    if (window.jQuery) {
        // 页面中已有 jQuery 的情况下，避免依赖重复加载 jQuery
        var jqVer = window.jQuery.fn.jquery;
        if (jqVer === '1.4.4') {
            define('jquery-1.4.4', function () { return window.jQuery; });
            _config.paths['jquery'] = 'lib/jquery/dist/jquery';
            _config.paths['jquery-1.7.2'] = 'jquery/jquery-1.7.2.min';
            _config.paths['jquery-1.9.1'] = 'jquery/jquery-1.9.1.min';
        } else if (jqVer === '1.7.2') {
            define('jquery-1.7.2', function () { return window.jQuery; });
            _config.paths['jquery'] = 'lib/jquery/dist/jquery';
            _config.paths['jquery-1.4.4'] = 'jquery/jquery-1.4.4.min';
            _config.paths['jquery-1.9.1'] = 'jquery/jquery-1.9.1.min';
        } else if (jqVer === '1.9.1') {
            define('jquery-1.9.1', function () { return window.jQuery; });
            _config.paths['jquery'] = 'lib/jquery/dist/jquery';
            _config.paths['jquery-1.4.4'] = 'jquery/jquery-1.4.4.min';
            _config.paths['jquery-1.7.2'] = 'jquery/jquery-1.7.2.min';
        } else {
            define('jquery', function () { return window.jQuery; });
            _config.paths['jquery-1.4.4'] = 'lib/jquery/jquery-1.4.4.min';
            _config.paths['jquery-1.7.2'] = 'jquery/jquery-1.7.2.min';
            _config.paths['jquery-1.9.1'] = 'jquery/jquery-1.9.1.min';
        }
    } else {
        _config.paths['jquery'] = 'lib/jquery/dist/jquery.min';
        _config.paths['jquery-1.4.4'] = 'jquery/jquery-1.4.4.min';
        _config.paths['jquery-1.7.2'] = 'jquery/jquery-1.7.2.min';
        _config.paths['jquery-1.9.1'] = 'jquery/jquery-1.9.1.min';
    }

    requirejs.config(_config);

    // 加载全局基础控件初始化脚本和样式
    requirejs(['commonInit']);
})();

/**
 * 常用部分库组合通过标识调用
 * 用法：
 *  requirejs(['../../Content/config'], function () {
 *    requireBundle(['@bundle1', '@bundle2', 'lib1', 'lib2'], function() {
 *      //
 *    }
 *  }
 *
 * @param  {Array}    libs     数组值如果以‘@’开头，则查询函数内部的索引，通过 key 加载多个 lib 的集合。
 *                             如果没有以 “@” 开头则为普通加载 lib 的形式。
 * @param  {Function} callback 与requirejs 的 callback 用法一致
 */
function requireBundle(libs, callback) {
    var bundles = {
        'listview': ['jqGrid'],
        'edit': ['jquery-form', 'jquery.edit'],
        // ...
    };
    var finalLibs = [];

    libs.map(function (lib) {
        var bundle = bundles[lib.substr(1)];
        if (lib.indexOf('@') === 0) {
            finalLibs = finalLibs.concat(bundle);
        } else {
            finalLibs.push(lib);
        }
    });

    requirejs(finalLibs, function () {
        callback && callback.apply(null, arguments);
    });
}