/**
 * requirejs config
 */
(function() {
    var _config = {
        urlArgs: 'v=1791104738',
        baseUrl: 'https://content.starmile.com.cn/Content',//正式
        //baseUrl: 'http://localhost:8004/Content',//测试
        map: {
            '*': {
                'css': 'js/css'
            }
        },

        paths: {
            //-- commont
            'commonInit': 'js/commonInit', // 全局基础控件初始化脚本
            'tether': 'lib/tether/dist/js/tether',
            'bootstrap': 'lib/bootstrap/dist/js/bootstrap',
            'bootstrap-table': 'lib/bootstrap-table/dist/bootstrap-table.min',
            'bootstrap-datetimepicker': 'lib/bootstrap-datetimepicker/build/js/bootstrap-datetimepicker.min',
            'bootstrapvalidator': 'lib/bootstrapvalidator/dist/js/bootstrapValidator',
            'bootstrapvalidator-cn': 'lib/bootstrapvalidator/dist/js/language/zh_CN',
            'bootstrap-table-cn': 'lib/bootstrap-table/dist/locale/bootstrap-table-zh-CN.min',
            'indextab': 'js/indextab',
            'Default': 'js/Default',
            'app': 'js/app',
            'amazeui': 'js/amazeui.min',
            'Chart': 'js/Chart',
            'admin': 'css!css/admin.css'
        },

        shim: {
            //-- commont
            'tether': ['jquery'],
            'bootstrap': ['tether', 'css!lib/bootstrap/dist/css/bootstrap.min.css'],
            'font': ['jquery', 'css!lib/font-awesome/css/font-awesome.min.css'],
            'bootstrapvalidator-cn': ['bootstrapvalidator'],
            'bootstrapvalidator': ['bootstrap', 'css!lib/bootstrapvalidator/dist/css/bootstrapValidator.min.css'],
            'bootstrap-datetimepicker': ['bootstrap', 'css!lib/bootstrap-datetimepicker/build/css/bootstrap-datetimepicker.min.css'],
            'amazeui': ['jquery', 'css!../Content/css/amazeui.min.css'], 
            'app': ['amazeui', 'css!../Content/css/app.css']
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

    // 加载全局基础控件初始化脚本
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