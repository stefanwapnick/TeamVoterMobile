using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Android.Support.V7.Widget;
using Android.Views;
using TeamVoterMobile.ListViews;
using TeamVoterMobile.Models;

namespace TeamVoterMobile.ListViews
{
    public class VoteListAdapter : RecyclerView.Adapter
    {
        private IList<Vote> _voteList;

        public EventHandler<int> ItemClicked;

        public VoteListAdapter() : this(null)
        { }

        public VoteListAdapter(IEnumerable<Vote> votes)
        {
            SetVotes(votes);
        }

        public void SetVotes(IEnumerable<Vote> votes)
        {
            if (votes == null)
            {
                _voteList = new List<Vote>();
                return;
            }
            
            _voteList = votes.OrderByDescending(v => v.CreatedAt).ToList();
            NotifyDataSetChanged();
        }


        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            VoteItemViewHolder viewHolder = holder as VoteItemViewHolder;
            if (viewHolder == null)
                return;

            Vote vote = _voteList[position];

            viewHolder.UserEmail.Text = vote.User?.Email ?? string.Empty;
            viewHolder.Timestamp.Text = vote.CreatedAt.ToString(@"yyyy/MM/dd hh:mm:ss tt", CultureInfo.InvariantCulture);
            viewHolder.UpVoteImage.Visibility = vote.IsUpvote ? ViewStates.Visible : ViewStates.Gone;
            viewHolder.DownVoteImage.Visibility = vote.IsUpvote ? ViewStates.Gone : ViewStates.Visible;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View cellView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.VoteListItem, parent, false);
            return new VoteItemViewHolder(cellView);
        }

        public override int ItemCount => _voteList.Count;
    }
}