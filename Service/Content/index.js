$(document).ready(function () {
    $('.dropdown-menu li a').click(function () {
        $("#footer-dropdown").html($(this).text() + ' <span class="glyphicon glyphicon-chevron-down">');
        //$("#footer-dropdown").val($(this).text());

    });
});