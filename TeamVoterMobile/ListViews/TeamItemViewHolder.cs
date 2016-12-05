using System;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace TeamVoterMobile.ListViews
{
    public class TeamItemViewHolder : RecyclerView.ViewHolder
    {
        public TextView UpVoteText { get; private set; }
        public TextView DownVoteText { get; private set; }
        public TextView TeamNameText { get; private set; }
        public TextView LastUpdateTimestamp { get; private set; }
        public View UpVotesImage { get; private set; }
        public View DownVotesImage { get; private set; }

        public TeamItemViewHolder(View itemView, Action<int> listener) : base(itemView)
        {
            UpVoteText = itemView.FindViewById<TextView>(Resource.Id.list_item_team_upVotesText);
            DownVoteText = itemView.FindViewById<TextView>(Resource.Id.list_item_team_downVotesText);
            TeamNameText = itemView.FindViewById<TextView>(Resource.Id.list_item_team_nameText);
            LastUpdateTimestamp = itemView.FindViewById<TextView>(Resource.Id.list_item_team_updatedTimestampText);
            UpVotesImage = itemView.FindViewById<View>(Resource.Id.list_item_team_upVotesImage);
            DownVotesImage = itemView.FindViewById<View>(Resource.Id.list_item_team_downVotesImage);

            itemView.Click += (sender, e) => listener(base.Position);
        }

        public void SetVotesVisibility(int upVotes, int downVotes)
        {
            UpVoteText.Visibility = upVotes > 0 ? ViewStates.Visible : ViewStates.Invisible;
            UpVotesImage.Visibility = UpVoteText.Visibility;
            DownVoteText.Visibility = downVotes > 0 ? ViewStates.Visible : ViewStates.Invisible;
            DownVotesImage.Visibility = DownVoteText.Visibility;
        }

    }
}