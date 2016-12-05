using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace TeamVoterMobile.Models
{
    public class Team : EntityData
    {
        public string Name { get; set; }
        
        public ICollection<Vote> Votes { get; set; }

        public int UpVotes => Votes?.Count(v => v.IsUpvote) ?? 0;
        public int DownVotes => Votes?.Count(v => !v.IsUpvote) ?? 0;
        public int TotalScore => UpVotes - DownVotes;
        public DateTime? LastVoteTimestamp => Votes != null && Votes.Any() ? Votes.Select(v => v.CreatedAt).Max() : (DateTime?)null;

        public Team()
        {
            Votes = new List<Vote>();
        }
    }
}