using System;
using System.Collections.Generic;

namespace english.Models
{
    public partial class Phrases
    {
        public Phrases()
        {
            Translations = new HashSet<Translations>();
            UserQuestions = new HashSet<UserQuestions>();
        }

        public int PhraseId { get; set; }
        public string Text { get; set; }

        public virtual ICollection<Translations> Translations { get; set; }
        public virtual ICollection<UserQuestions> UserQuestions { get; set; }
    }
}
