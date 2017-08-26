var $table = $('#table');
$(function () {
    initTable();
    $('#datetimepicker1').datetimepicker({
        collapse: false,
        format: 'yyyy-MM-dd',
        language: 'zh-CN',
        autoclose: true
    });
    $('#datetimepicker2').datetimepicker({
        collapse: false,
        format: 'yyyy-MM-dd',
        language: 'zh-CN',
        autoclose: true
    });
    $('#btn_query').click(function () {
        $.loading(true);
        $table.bootstrapTable('refresh', queryParams);  
    });

    $('#modfiy').click(function () {
        edit(1);
    });
});

function initTable() { 
    $table.bootstrapTable({
        url: '/Youngo/GetProductList',
        dataType: "json",
        method: 'get',
        contentType: "application/json",
        toolbar: '#toolbar',                //工具按钮用哪个容器
        cache: false,                       //是否使用缓存，默认为true，所以一般情况下需要设置一下这个属性（*）
        striped: true,     //使表格带有条纹  
        //iconsPrefix: 'fa',
        pagination: true, //在表格底部显示分页工具栏  
        pageSize: 8,
        paginationPreText: '上一页',
        paginationNextText: '下一页',
        pageNumber: 1,
        pageList: [8, 25, 50, 100],
        idField: "id",  //标识哪个字段为id主键  
        showToggle: false,   //名片格式  
        cardView: false,//设置为True时显示名片（card）布局  
        showColumns: false, //显示隐藏列    
        //showRefresh: true,  //显示刷新按钮  
        singleSelect: false,//复选框只能选择一条记录  
        search: false,//是否显示右上角的搜索框  
        clickToSelect: true,//点击行即可选中单选/复选框  
        sidePagination: "server",//表格分页的位置  
        queryParams: queryParams, //参数  
        queryParamsType: "limit", //参数格式,发送标准的RESTFul类型的参数请求  
        silent: true,  //刷新事件必须设置 
        classes: 'table table-hover',
        rowStyle: function (row, index) {
            //这里有5个取值代表5中颜色['active', 'success', 'info', 'warning', 'danger'];
            var strclass = "";
            if (row.state == "有效") {
                strclass = 'success';//还有一个active
            }
            else if (row.state == "无效") {
                strclass = 'danger';
            }
            else {
                return {};
            }
            return { css: { "line-height": "50px" } }
        },
        columns: [{
            field: 'id',
            checkbox: true,
            align: 'center',
            valign: 'top',
        }, {
            field: 'picture',
            title: '图片',
            align: 'center',
            valign: 'top',
            formatter: function (value, row, index) {
                return '<img class="picture" src="' + value + '"';
            }
        }, {
            field: 'productId',
            title: '商品ID（SKU）',
            align: 'center',
            valign: 'top'
        }, {
            field: 'name',
            title: '名称',
            align: 'center',
            valign: 'top'
        }, {
            field: 'type',
            title: '产品类型',
            align: 'center',
            valign: 'top'
        }, {
            field: 'purchasePrice',
            title: '价格',
            align: 'center',
            valign: 'top'
        }, {
            field: 'salesVolumem',
            title: '销量',
            align: 'center',
            valign: 'top'
        }, {
            field: 'isTop',
            title: '是否置顶',
            align: 'center',
            valign: 'top'
        }, {
            field: 'isClearStock',
            title: '是否清货',
            align: 'center',
            valign: 'top'
        }, {
            field: 'state',
            title: '状态',
            align: 'center',
            valign: 'top'
        }, {
            field: 'creatTime',
            title: '创建时间',
            align: 'center',
            valign: 'top'
        },
        {
            title: '操作',
            field: 'id',
            align: 'center',
            valign: 'top',
            formatter: function (value, row, index) {
                var e = '<a href="#" mce_href="#" onclick="edit(\'' + row.id + '\')">编辑</a> ';
                var d = '<a href="#" mce_href="#" onclick="del(\'' + row.id + '\')">删除</a> ';
                return e + d;
            }
        }],
        formatLoadingMessage: function () {
            //return "请稍等，正在加载中...";
            //$table.bootstrapTable('showLoading', true);
        },
        formatNoMatches: function () {
            $.loading(false);
            return '无符合条件的记录';
        },
        onLoadError: function (data) {
            $.loading(false);
            $('#table').bootstrapTable('removeAll');
        },
        onLoadSuccess: function (data) {
            $.loading(false);
        }
        //onClickRow: function (row) {
        //    window.location.href = "/qStock/qProInfo/" + row.ProductId;
        //}
    });
}
 
//传递的参数
function queryParams(params) {
    var search = {
        pageSize: params.limit,
        pageIndex: params.offset,
        //pageIndex: params.pageNumber,
        productId: $(".productId").val(),
        dateType: $(".dateType").val(),
        name: $(".name").val(),
        state: $(".state").val(),
        type: $(".type").val(),
        starDate: $(".starDate").val(),
        endDate: $(".endDate").val()
    };
    return search;
}

function edit(id) {
    Modal.load({ msg: "测试", url: "/Home/error", title: "远程加载页面", width: 1100, height: 650 });
}
function del(id) {

}