
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

let state_start_session = () => ({
        question_text: null,
        answer_text: null,
        display_rating_buttons: false,
        user: 'test',
        session_id: null,
        question_id: null        
    });

let state_session_started = (state, session_id) => {
        state.session_id = state;
        return state;
    };

let state_question_received = (state, question_id, question_text) => {
        state.question_id = question_id;
        state.question_text = question_text;        
        return state;
    };

let state_answer_received = (state, answer_text) => {
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
        .done( (session_id) => {
            page_state = state_session_started(page_state, session_id);

            ankiAPI.question(session_id, page_state.user)
            .done( (qr) => {
                page_state = state_question_received(page_state,
                                                        qr.question_id,
                                                        qr.question_text);
                render(page_state);                                                    
            });

        });        
}
// event: load
$(function page_load() {

    ankiAPI.start(page_state.user, /*isFirstLogin*/ true)
        .done( (session_id) => {
            page_state = state_session_started(page_state, session_id);

            ankiAPI.question(session_id, page_state.user)
            .done( (qr) => {
                page_state = state_question_received(page_state,
                                                        qr.question_id,
                                                        qr.question_text);
                render(page_state);                                                    
            });

        });    

})

// event: btn_answer -> click
$('#btn_answer').click( ()=> {
     
     ankiAPI.answer(page_state.session_id, page_state.user, page_state.question_id)
        .done( (answer) => {
            page_state = state_answer_received(page_state, answer);
            render(page_state);                                                    
        });

});

// event: answerrating -> click
$('.answerrating').click( (ev)=> {
     let button = ev.target

     let rating = parseInt(button.value);

     ankiAPI.rate(page_state.session_id, page_state.user, page_state.question_id, rating)
        .done( ()=>{
            startSession(/*isFirstLogin*/ false);
        });   
});

// accessors
let set_question_text = (text) => $('#question_text').text(text);
let show_answer_button = (show) => $('#btn_answer').show();
let hide_answer_button = (show) => $('#btn_answer').hide();
let show_answer_text = (show) => $('#answer_text').show();
let hide_answer_text = (show) => $('#answer_text').hide();
let set_answer_text = (text) => $('#answer_text').text(text);
let show_rating_buttons = (show) => $('.answerratebox').show();
let hide_rating_buttons = (show) => $('.answerratebox').hide();

// render
function render(page) {

    if(page.question_text == null) { 
        // loading...
        return;
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

function page_loop(state) {
    render(state);
}

$(function() {

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

// render tests
$(function () {

    // tests
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

    // render(state);
});