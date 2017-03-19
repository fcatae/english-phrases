
function phrase_submit(text) {

    $.ajax({
        type: "POST",
        data: JSON.stringify(text),
        url: "api/values",
        contentType: "application/json"
    })
    .done( () => {
        alert('submitted');
    });
    
    
    return false;
}