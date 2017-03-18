
// - start session
// - get question
// - show answer key
// - rate the question
// - restart

var page_state = {
    question_text: null,
    answer_text: null,
    display_rating_buttons: false
};

let set_question_text = (text) => $('#question_text').text(text);
let show_answer_button = (show) => $('#btn_answer').show();
let hide_answer_button = (show) => $('#btn_answer').hide();
let show_answer_text = (show) => $('#answer_text').show();
let hide_answer_text = (show) => $('#answer_text').hide();
let set_answer_text = (text) => $('#answer_text').text(text);
let show_rating_buttons = (show) => $('.answerratebox').show();
let hide_rating_buttons = (show) => $('.answerratebox').hide();

function render(page) {

    if(page.question_text == null) {
        // ???
    }

    set_question_text(page.question_text);

    if(page.answer_text == null) {
        show_answer_button();
        hide_answer_text();
    } else {
        hide_answer_button();
        set_answer_text(page.answer_text);
        show_answer_text();
    }

    if(page.display_rating_buttons == true) {
        show_rating_buttons();
    } else {
        hide_rating_buttons();
    }
}

// jQuery Controller
$(function () {

    let state_initial = ({
        question_text: 'can you translate this text?',
        answer_text: null,
        display_rating_buttons: false
    });

    let state_show_answer = ({
        question_text: 'the book is on the table',
        answer_text: 'o livro está em cima da mesa',
        display_rating_buttons: true
    });

    let state_show_all = ({
        question_text: 'This screen has everything enabled',
        answer_text: 'Except the answer button',
        display_rating_buttons: true
    });

    render(state_show_answer);    

    function ignore_now() {
    $('#btn_answer').click( ()=> { alert('show answer') });
    $('.answerrating').click( (ev)=> { 
        let button = ev.target
        alert('rate: ' + button.value) });
    }

});


// start tests
$(function() {
    testAnki( ankiAPI );
})