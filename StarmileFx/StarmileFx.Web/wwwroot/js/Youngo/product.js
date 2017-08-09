$(function () {
    //initTable();
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
});

function initTable() {
    var $table = $('#table');
    $table.bootstrapTable({
        url: '../SysMenus.json',
        dataType: "json",
        method: 'post',
        contentType: "application/x-www-form-urlencoded",
        toolbar: '#toolbar',                //工具按钮用哪个容器
        striped: true,                      //是否显示行间隔色
        singleSelect: false,
        pagination: true, //分页
        pageNumber: 1,                       //初始化加载第一页，默认第一页
        pageSize: 20,                       //每页的记录行数（*）
        pageList: [20, 50, 100, 200, 500],        //可供选择的每页的行数（*）
        search: false, //显示搜索框
        sidePagination: "server", //服务端处理分页
        showExport: true,                     //是否显示导出
        exportDataType: "basic", //basic', 'all', 'selected'.
        queryParams: queryParams,
        columns: [{
            field: 'id',
            checkbox: true
        }, {
            field: 'ProductID',
            title: 'SKU',
            align: 'center',
            valign: 'top'
        }, {
            field: 'Name',
            title: '名称',
            align: 'center',
            valign: 'top'
        }, {
            field: 'Type',
            title: '产品类型',
            align: 'center',
            valign: 'top'
        }, {
            field: 'PurchasePrice',
            title: '价格',
            align: 'center',
            valign: 'top'
        }, {
            field: 'SalesVolume',
            title: '销量',
            align: 'center',
            valign: 'top'
        }, {
            field: 'State',
            title: '状态',
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
        onLoadSuccess: function () {
        },
        onLoadError: function () {
            //mif.showErrorMessageBox("数据加载失败！");
        }
    });
}
 
//传递的参数
function queryParams(params) {
    return {
        pageSize: params.pageSize,
        pageIndex: params.pageNumber,
        UserName: $("#txtName").val(),
        Birthday: $("#txtBirthday").val(),
        Gender: $("#Gender").val(),
        Address: $("#txtAddress").val(),
        name: params.sortName,
        order: params.sortOrder
    };
}