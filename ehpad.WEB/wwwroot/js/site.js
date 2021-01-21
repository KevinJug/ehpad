﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


console.log(window.location.pathname);
console.log(window.location.pathname.split('/'));
console.log(window.location.pathname.split('/')[0]);
console.log(window.location.pathname.split('/')[1]);


$(document).ready(function () {

    console.log(window.location.pathname);
    var path = window.location.pathname.split('/')[1];
    console.log(path);
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

    }

    $('ul.navbar-nav > li')
        .click(function (e) {
            $('ul.navbar-nav > li')
                .removeClass('active');
            $(this).addClass('active');
        });
});
