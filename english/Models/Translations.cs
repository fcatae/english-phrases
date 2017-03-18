using System;
using System.Collections.Generic;

namespace english.Models
{
    public partial class Translations
    {
        public int TranslationId { get; set; }
        public int PhraseId { get; set; }
        public string Text { get; set; }

        public virtual Phrases Phrase { get; set; }
    }
}
