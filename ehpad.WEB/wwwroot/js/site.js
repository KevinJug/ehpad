// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


$(document).ready(function () {

    var path = window.location.pathname.split('/')[1];
    var path2 = window.location.pathname.split('/')[2];
    switch (path) {
        case "Drugs":
            $('.drugs')
                .addClass('active');
            break;
        case "Brands":
            $('.brands')
                .addClass('active');
            break;
        case "Vaccines":
            $('.vaccines')
                .addClass('active');
            break;
        case "Injections":
            $('.injections')
                .addClass('active');
            break;
        case "Drugs":
            $('.drugs')
                .addClass('active');
            break;
        case "People":
            $('.peoples')
                .addClass('active');
            break;
        default :
            $('.filters')
                .addClass('active');
            if (path2 === "IndexVaccineByPeople" || path2 === "Details") {
                $('.filter-index')
                    .addClass('active');
            } else if (path2 === "IndexNoVaccine" || path2 == "NoVaccine") {
                $('.filter-no-vaccine')
                    .addClass('active');
                                    ;
            } else {
                 $('.filter-index-reminder')
                   .addClass('active');
            }
    }

    $('ul.navbar-nav > li')
        .click(function (e) {
            $('ul.navbar-nav > li')
                .removeClass('active');
            $(this).addClass('active');
        });
});
