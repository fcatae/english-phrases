using System;
using System.Collections.Generic;

namespace english.Models
{
    public partial class UserQuestions
    {
        public int UserId { get; set; }
        public int PhraseId { get; set; }
        public int Difficulty { get; set; }

        public virtual Phrases Phrase { get; set; }
        public virtual Users User { get; set; }
    }
}
