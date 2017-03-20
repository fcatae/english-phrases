// state
var page_state = {
    question_text: null,
    question_id: null
};
var state_start_session = function () { return ({
    question_text: null,
    question_id: null
}); };
var state_question_received = function (state, question_id, question_text) {
    state.question_text = question_text;
    state.question_id = question_id;
    return state;
};
// event: init
(function page_init() {
    page_state = state_start_session();
})();
// accessors
var set_question_text = function (text) { return $('#question_text').text(text); };
var get_answer_text = function () { return $('#answer_text').val(); };
var set_answer_text = function (text) { return $('#answer_text').val(text); };
var hide_answer_button = function () { return $('#btn_answer').hide(); };
var show_noanswer_warning = function () { return alert('Não há mais frases a serem traduzidas.'); };
function startSession() {
    set_answer_text('');
    page_state = state_start_session();
    render(page_state);
    ankiAPI.pendingAnswer()
        .done(function (qr) {
        page_state = state_question_received(page_state, qr.question_id, qr.question_text);
        render(page_state);
    });
}
// event: load
$(function page_load() {
    startSession();
});
// event: btn_answer -> click
$('#btn_answer').click(function () {
    var text = get_answer_text();
    if (text == null || text == '')
        return;
    ankiAPI.translate(page_state.question_id, text)
        .done(function () {
        startSession();
    });
});
// render
function render(page) {
    if (page.question_text == null) {
        // loading...
        return;
    }
    if (page.question_id == -1) {
        hide_answer_button();
        show_noanswer_warning();
    }
    set_question_text(page.question_text);
}
// render tests
$(function () {
    // tests
    var state_initial = ({
        question_text: 'can you translate this text?',
        answer_text: null,
        display_rating_buttons: false
    });
    var state_show_answer = ({
        question_text: 'the book is on the table',
        answer_text: 'o livro está em cima da mesa',
        display_rating_buttons: true
    });
    var state_show_all = ({
        question_text: 'This screen has everything enabled',
        answer_text: 'Except the answer button',
        display_rating_buttons: true
    });
    // render(state);
});
