$(document).ready(function () {
    var showMenu = false;

    $('.dropdown-menu li a').click(function () {
        $("#footer-dropdown").html($(this).text() + ' <span class="glyphicon glyphicon-chevron-down">');
    });

    $('.menu-control').click(function () {
        if (showMenu) {
            $('.menu').css("display", "none");
            //            $('.menu').css({"visibility": "hidden"}, {"opacity": 0});
        } else {
            $('.menu').css("display", "block");
            //            $('.menu').css({ "visibility": "visible" }, { "opacity": 1 });
        }

        showMenu = !showMenu;
    });
});