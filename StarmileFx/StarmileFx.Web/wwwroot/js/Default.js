$(function () {
    if (document.getElementById("leavechart")) {
        GetLeaveChart();
        GetSalaryChart();
    }
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