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

    // send the text    
    alert(text);
    // receive the id
    
    return false;
}