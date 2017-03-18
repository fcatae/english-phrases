// test functions
function testAnki(anki) {

    // Tests: start
    anki.start('test', true).done( v => {
        var START_TEST_TRUE = 50001;        
        if(v != START_TEST_TRUE) 
            alert('/api/anki/start FAILED')
    });

    anki.start('test', false).done( v => {
        var START_TEST_FALSE = 60009;
        if(v != START_TEST_FALSE) 
            alert('/api/anki/start FAILED')
    });

    // Tests: question
    anki.question(50001, 'test').done( v => {
        var QUESTION_TEST = '(test_question)';

        var valid = (v) && 
                    (v.question_text) &&
                    (v.question_text.startsWith(QUESTION_TEST));

        if(!valid) 
            alert('/api/anki/question FAILED');  
    });

    // Tests: answer
    anki.answer(50001, 'test', 1).done( v => {
        var ANSWER_TEST = '(test_answer)';

        if(!v.startsWith(ANSWER_TEST)) 
            alert('/api/anki/answer FAILED');
    });

    // Tests: rate
    anki.rate(50001, 'test', 123, 100).done( ret => {
        if(ret == false)
            alert(ret);
    });
}
