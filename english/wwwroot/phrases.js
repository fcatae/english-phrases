
function phrase_submit(text, callback) {

    $.ajax({
        type: "POST",
        data: JSON.stringify(text),
        url: "api/values",
        contentType: "application/json"
    })
    .done( () => {
        if(callback) callback();
    });
    
    
    return false;
}