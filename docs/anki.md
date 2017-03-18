Anki
=====

## Workflow

- start session
- get question
- show answer key
- rate the question
- restart

## API definition

Endpoint: api/anki
- /start(user,isFirstLogin) -> (session_id)
- /{session_id}/question(user) -> question_id, question_text
- /{session_id}/answer(question_id) -> answer_text
- /{session_id}/rate(user, question_id, rating) -> ()

