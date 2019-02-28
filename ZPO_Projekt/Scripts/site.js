$(document).ready(function () {

    $(".hamburger-btn").on('click', function () {
        var menu = $(".nav-menu");
        var visible = menu.hasClass('nav-visible');

        if (visible) {
            menu.removeClass('nav-visible');
        } else {
            menu.addClass('nav-visible');
        }
    });
});