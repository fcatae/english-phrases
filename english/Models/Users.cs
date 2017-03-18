using System;
using System.Collections.Generic;

namespace english.Models
{
    public partial class Users
    {
        public Users()
        {
            UserQuestions = new HashSet<UserQuestions>();
        }

        public int UserId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<UserQuestions> UserQuestions { get; set; }
    }
}
