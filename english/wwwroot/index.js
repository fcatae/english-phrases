
// - start session
// - get question
// - show answer key
// - rate the question
// - restart

// jQuery Controller
$(function () {

    $('#btn_answer').click( ()=> { alert('show answer') });
    $('#question_text').text('The boy and the girl are studying today');
    $('#answer_text').text('O menino e a menina estão estudando hoje');
    $('.answerrating').click( (ev)=> { 
        let button = ev.target
        alert('rate: ' + button.value) });

});

// API
$(function () {

    var phrase = "it just works";

    $.ajax({
        type: "POST",
        data :JSON.stringify(phrase),
        url: "api/phrases",
        contentType: "application/json"
    });

});
