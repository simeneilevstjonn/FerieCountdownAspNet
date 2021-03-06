﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function getCookie(cname) {
    var name = cname + "=";
    var decodedCookie = decodeURIComponent(document.cookie);
    var ca = decodedCookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
        }
    }
    return null;
}

document.getElementById("localespan").innerHTML = getCookie("locale") ?? "Ingen";
if (document.getElementById("localespan").innerHTML == "custom") document.getElementById("localespan").innerHTML = "Egendefinert";
else if (document.getElementById("localespan").innerHTML == "work") {
    document.getElementById("localespan").innerHTML = "Jobb";
    document.getElementById("sdayCountdown").innerHTML = "Arbeidsdag";
    document.getElementById("dendlink").innerHTML = "Arbeidsdagens slutt";
}

// Set cohort span
document.getElementById("cohortspan").innerHTML = getCookie("cohort") ?? "Ingen";
if (document.getElementById("cohortspan").innerHTML != "Ingen") {
    document.getElementById("cohortspan").innerHTML = document.getElementById("cohortspan").innerHTML * 1 + 1;
}