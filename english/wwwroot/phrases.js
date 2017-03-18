$(function () {

    var phrase = "it just works";

    $.ajax({
        type: "POST",
        data :JSON.stringify(phrase),
        url: "api/phrases",
        contentType: "application/json"
    });

});