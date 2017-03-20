// - start session
// - get question
// - show answer key
// - rate the question
// - restart
// state
var page_state = {
    question_text: null,
    answer_text: null,
    display_rating_buttons: false,
    user: null,
    session_id: null,
    question_id: null
};
var state_start_session = function () { return ({
    question_text: null,
    answer_text: null,
    display_rating_buttons: false,
    user: 'test',
    session_id: null,
    question_id: null
}); };
var state_session_started = function (state, session_id) {
    state.session_id = state;
    return state;
};
var state_question_received = function (state, question_id, question_text) {
    state.question_id = question_id;
    state.question_text = question_text;
    return state;
};
var state_answer_received = function (state, answer_text) {
    state.answer_text = answer_text;
    state.display_rating_buttons = true;
    return state;
};
// event: init
(function page_init() {
    page_state = state_start_session();
})();
function startSession(isFirstLogin) {
    page_state = state_start_session();
    ankiAPI.start(page_state.user, isFirstLogin)
        .done(function (session_id) {
        page_state = state_session_started(page_state, session_id);
        ankiAPI.question(session_id, page_state.user)
            .done(function (qr) {
            page_state = state_question_received(page_state, qr.question_id, qr.question_text);
            render(page_state);
        });
    });
}
// event: load
$(function page_load() {
    ankiAPI.start(page_state.user, /*isFirstLogin*/ true)
        .done(function (session_id) {
        page_state = state_session_started(page_state, session_id);
        ankiAPI.question(session_id, page_state.user)
            .done(function (qr) {
            page_state = state_question_received(page_state, qr.question_id, qr.question_text);
            render(page_state);
        });
    });
});
// event: btn_answer -> click
$('#btn_answer').click(function () {
    ankiAPI.answer(page_state.session_id, page_state.user, page_state.question_id)
        .done(function (answer) {
        page_state = state_answer_received(page_state, answer);
        render(page_state);
    });
});
// event: answerrating -> click
$('.answerrating').click(function (ev) {
    var button = ev.target;
    var rating = parseInt(button.value);
    ankiAPI.rate(page_state.session_id, page_state.user, page_state.question_id, rating)
        .done(function () {
        startSession(/*isFirstLogin*/ false);
    });
});
// accessors
var set_question_text = function (text) { return $('#question_text').text(text); };
var show_answer_button = function (show) { return $('#btn_answer').show(); };
var hide_answer_button = function (show) { return $('#btn_answer').hide(); };
var show_answer_text = function (show) { return $('#answer_text').show(); };
var hide_answer_text = function (show) { return $('#answer_text').hide(); };
var set_answer_text = function (text) { return $('#answer_text').text(text); };
var show_rating_buttons = function (show) { return $('.answerratebox').show(); };
var hide_rating_buttons = function (show) { return $('.answerratebox').hide(); };
// render
function render(page) {
    if (page.question_text == null) {
        // loading...
        return;
    }
    set_question_text(page.question_text);
    if (page.answer_text == null) {
        show_answer_button();
        hide_answer_text();
    }
    else {
        hide_answer_button();
        set_answer_text(page.answer_text);
        show_answer_text();
    }
    if (page.display_rating_buttons == true) {
        show_rating_buttons();
    }
    else {
        hide_rating_buttons();
    }
}
function page_loop(state) {
    render(state);
}
$(function () {
    function ignore_now() {
        $('#btn_answer').click(function () { alert('show answer'); });
        $('.answerrating').click(function (ev) {
            var button = ev.target;
            alert('rate: ' + button.value);
        });
    }
});
// start tests
$(function () {
    testAnki(ankiAPI);
});
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
