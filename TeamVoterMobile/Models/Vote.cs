using System;

namespace TeamVoterMobile.Models
{
    public class Vote : EntityData
    {
        public bool IsUpvote { get; set; }
        public string TeamId { get; set; }
        public string UserId { get; set; }
        public Team Team { get; set; }
        public User User { get; set; }

        public Vote() { }

        public Vote(bool isUpvote, string teamId, string userId)
        {
            IsUpvote = isUpvote;
            TeamId = teamId;
            UserId = userId;
        }
    }
}