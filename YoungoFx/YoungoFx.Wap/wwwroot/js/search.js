$(function () {
    $('.headerBar').hide();
    $('.footerNav').hide();
    $('.searchBtn').click(function () {
        var keyword = $('.searchInput').val();
        window.location.href = "/Product/ProductList?keyword=" + keyword;
    });
    $('.searchHot').click(function () {
        var keyword = $(this).text();
        window.location.href = "/Product/ProductList?keyword=" + keyword;
    });
    $('.searchInput').keydown(function (e) {
        if (e.which == 13) {
            var keyword = $('.searchInput').val();
            window.location.href = "/Product/ProductList?keyword=" + keyword;
        }
    });
});