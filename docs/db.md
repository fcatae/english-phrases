# Database

Phrases
- PhraseId
- Text

Translations
- TranslationId
- PhraseId
- Text

UserQuestions
- UserId
- Difficulty
- PhraseId

Users
- UserId
- Name



Data v2
========

Verbs
- verb (key)
- she
- ing
- past
- perfect

PhrasalVerbs
- verb
- article
- expression

-- Token --

Verbs
-> VerbTense
-> Phrasal verbs (article)

WordWithPreposition
- eg: depend on , talk to

Expressions
- pair of pants
- at the corner

Associations

-- Meanings --

Meanings (Dictionary)
-Media (1:n)

Media
- images
- video (opt)

-- Sample texts --

Phrases
- Complete example

Dialogs
- Interchanged phrases

Texts
- Long text