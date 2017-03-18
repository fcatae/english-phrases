$(function () {

    var phrase = "it just works";

    $.ajax({
        type: "POST",
        data :JSON.stringify(phrase),
        url: "api/phrases",
        contentType: "application/json"
    });

});

function phrase_submit(text) {
    alert(text);
    return false;
}