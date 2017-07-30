$(function () {
    if (document.getElementById("leavechart")) {
        GetLeaveChart();
        GetSalaryChart();
    }

    var $table = $('#table');
    $table.bootstrapTable({
        url: '../SysMenus.json',
        dataType: "json",
        toolbar: '#toolbar',                //工具按钮用哪个容器
        striped: true,                      //是否显示行间隔色
        singleSelect: false,
        pagination: true, //分页
        pageNumber: 1,                       //初始化加载第一页，默认第一页
        pageSize: 10,                       //每页的记录行数（*）
        pageList: [10, 25, 50, 100],        //可供选择的每页的行数（*）
        search: false, //显示搜索框
        sidePagination: "server", //服务端处理分页
        columns: [{
            field: 'id',
            title: '序号'
        }, {
            field: 'liushuiid',
            title: '交易编号'
        }, {
            field: 'orderid',
            title: '订单号'
        }, {
            field: 'receivetime',
            title: '交易时间'
        }, {
            field: 'price',
            title: '金额'
        }, {
            field: 'coin_credit',
            title: '投入硬币'
        }, {
            field: 'bill_credit',
            title: '投入纸币'
        }, {
            field: 'changes',
            title: '找零'
        }, {
            field: 'tradetype',
            title: '交易类型'
        }, {
            field: 'goodmachineid',
            title: '货机号'
        }, {
            field: 'inneridname',
            title: '货道号'
        }, {
            field: 'goodsName',
            title: '商品名称'
        }, {
            field: 'changestatus',
            title: '支付'
        }, {
            field: 'sendstatus',
            title: '出货'
        },
                {
                    title: '操作',
                    field: 'id',
                    align: 'center',
                    formatter: function (value, row, index) {
                        var e = '<a href="#" mce_href="#" onclick="edit(\'' + row.id + '\')">编辑</a> ';
                        var d = '<a href="#" mce_href="#" onclick="del(\'' + row.id + '\')">删除</a> ';
                        return e + d;
                    }
                }
        ]
    });
});
function GetLeaveChart() {
    var randomScalingFactor = function () { return Math.round(Math.random() * 100) };
    var a_value = randomScalingFactor();
    var b_value = randomScalingFactor();
    var c_value = randomScalingFactor();
    var d_value = randomScalingFactor();
    var e_value = randomScalingFactor();
    var doughnutData = [
        {
            value: a_value,
            color: "#F7464A",
            highlight: "#FF5A5E",
            label: "谷歌"
        },
        {
            value: b_value,
            color: "#46BFBD",
            highlight: "#5AD3D1",
            label: "IE"
        },
        {
            value: c_value,
            color: "#FDB45C",
            highlight: "#FFC870",
            label: "其他"
        },
        {
            value: d_value,
            color: "#949FB1",
            highlight: "#A8B3C5",
            label: "火狐"
        }
        ,
        {
            value: e_value,
            color: "#726dd1",
            highlight: "#A8B3C5",
            label: "Safari"
        }
    ];

    var ctx = document.getElementById("leavechart").getContext("2d");
    window.myDoughnut = new Chart(ctx).Doughnut(doughnutData, { responsive: false });
    $("#a_value").html(a_value + "次");
    $("#b_value").html(b_value + "次");
    $("#c_value").html(c_value + "次");
    $("#d_value").html(d_value + "次");
    $("#e_value").html(e_value + "次");
}
function GetSalaryChart() {
    var randomScalingFactor = function () { return Math.round(Math.random() * 100) };
    var lineChartData = {
        labels: ["1月", "2月", "3月", "4月", "5月", "6月", "7月", "8月", "8月", "10月", "11月", "12月"],
        datasets: [
            {
                label: "My First dataset",
                fillColor: "rgba(220,220,220,0.2)",
                strokeColor: "rgba(220,220,220,1)",
                pointColor: "rgba(220,220,220,1)",
                pointStrokeColor: "#fff",
                pointHighlightFill: "#fff",
                pointHighlightStroke: "rgba(220,220,220,1)",
                data: [randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor()]
            }
        ]
    }
    var ctx = document.getElementById("salarychart").getContext("2d");
    window.myLine = new Chart(ctx).Line(lineChartData, {
        responsive: false,
        bezierCurve: false
    });
}
function initTable() {
    var queryUrl = '../SysMenus.json';
    $table = $('#table-javascript').bootstrapTable({
        method: 'post',
        contentType: "application/x-www-form-urlencoded",
        url: queryUrl,
        height: $(window).height() - 200,
        striped: true,
        pagination: true,
        singleSelect: false,
        pageSize: 50,
        pageList: [10, 50, 100, 200, 500],
        search: false, //不显示 搜索框
        showColumns: false, //不显示下拉框（选择显示的列）
        sidePagination: "server", //服务端请求，属性设为 "client",因为客户端会处理分页和全文检索，无需向服务器端发请求，所以也无需传递参数
        queryParams: queryParams,
        minimunCountColumns: 2,
        columns: [{
            field: 'state',
            checkbox: true
        }, {
            field: 'Tel',
            title: '固定电话',
            width: 100,
            align: 'left',
            valign: 'top',
            sortable: true
        }, {
            field: 'Mobile',
            title: '手机号码',
            width: 100,
            align: 'left',
            valign: 'top',
            sortable: true
        }, {
            field: 'operate',
            title: '操作',
            width: 100,
            align: 'center',
            valign: 'middle',
            formatter: operateFormatter,
            events: operateEvents
        }],
        onLoadSuccess: function () {
        },
        onLoadError: function () {
            mif.showErrorMessageBox("数据加载失败！");
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