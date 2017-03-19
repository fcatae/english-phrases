// state
var page_state = {
    question_text: null,
    question_id: null
};

let state_start_session = () => ({
        question_text: null,
        question_id: null        
    });

let state_question_received = (state, question_id, question_text) => {
        state.question_text = question_text;        
        state.question_id = question_id;
        return state;
    };


// event: init
(function page_init() {

    page_state = state_start_session();

})();

// accessors
let set_question_text = (text) => $('#question_text').text(text);
let get_answer_text = () => $('#answer_text').val();
let set_answer_text = (text) => $('#answer_text').val(text);

function startSession() {

    set_answer_text('');

    page_state = state_start_session();
    render(page_state);

    ankiAPI.pendingAnswer()
    .done( (qr) => {
        page_state = state_question_received(page_state,
                                                qr.question_id,
                                                qr.question_text);
        render(page_state);
    });

}

// event: load
$(function page_load() {

    startSession();

});

// event: btn_answer -> click
$('#btn_answer').click( ()=> {
     
     var text = get_answer_text();

     ankiAPI.translate(page_state.question_id, text)
        .done( () => {
            startSession();                
        });

});

// render
function render(page) {

    if(page.question_text == null) { 
        // loading...
        return;
    }

    set_question_text(page.question_text);
}

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