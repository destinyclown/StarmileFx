$(function () {
    var totalNum = 0;
    $('#shoppingcart').parent().addClass('cur');
    $('.cartCheckAll').click(function () {
        $('.cartCheck').click();
    });
    $('.add').click(function () {
        var number = Number($(this).prev('.number').val());
        number = number + 1;
        $(this).prev('.number').val(number);
        if ($(this).parent().parent().parent().children('.cartCheck').is(':checked')) {
            var price = Number($(this).parent().prev().find('.price').text());
            totalNum = totalNum + price;
            $('.totalPrice').text(totalNum.toFixed(2));
        }
    });
    $('.minus').click(function () {
        var number = Number($(this).next('.number').val());
        number = number - 1;
        $(this).next('.number').val(number);
        if ($(this).parent().parent().parent().children('.cartCheck').is(':checked')) {
            var price = Number($(this).parent().prev().find('.price').text());
            totalNum = totalNum - price;
            $('.totalPrice').text(totalNum.toFixed(2));
        }
    });
    $('.cartCheck').click(function () {
        if ($(this).is(':checked')) {
            $(this).val(true);
            var number = Number($(this).parent().find('.number').val());
            var price = Number($(this).parent().find('.price').text());
            totalNum = totalNum + price * number;
            $('.totalPrice').text(totalNum.toFixed(2));
        }
        else {
            $(this).val(false);
            var number = Number($(this).parent().find('.number').val());
            var price = Number($(this).parent().find('.price').text());
            totalNum = totalNum - price * number;
            $('.totalPrice').text(totalNum.toFixed(2));
        }
    });
    $('#goahead').click(function () {
        $('#shoppingcart').submit();
    });
});