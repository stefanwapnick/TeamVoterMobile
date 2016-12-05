using System.Collections.Generic;

namespace TeamVoterMobile.Models
{
    public class User : EntityData
    {
        public string Email { get; set; }

        public ICollection<Vote> Votes { get; set; }

        public User()
        {
            Votes = new List<Vote>();
        }
    }
}