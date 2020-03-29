$(function () {
    $('.datetimepicker').datetimepicker({
        format:'L'
    });
});

$(function () {
    $('.datetimepicker2').datetimepicker({
        format: 'L',
        locale: 'tr',
        language: 'tr',
    });
});

$(function () {
    $('.datetimepicker3').datetimepicker({
        format: 'LT',
        locale: 'tr',
        language: 'tr'
    });
});

$(function () {
    $.datepicker.setDefaults($.datepicker.regional['tr']);
    $(".tarih").datepicker({ dateFormat: 'dd.mm.yy' });
    $
});

$(function () {
    $('#datetimepicker6').datetimepicker();
    $('#datetimepicker7').datetimepicker({
        useCurrent: false //Important! See issue #1075
    });
    $("#datetimepicker6").on("dp.change", function (e) {
        $('#datetimepicker7').data("DateTimePicker").minDate(e.date);
    });
    $("#datetimepicker7").on("dp.change", function (e) {
        $('#datetimepicker6').data("DateTimePicker").maxDate(e.date);
    });
});