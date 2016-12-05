using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace TeamVoterMobile.ListViews
{
    public class VoteItemViewHolder : RecyclerView.ViewHolder
    {
        public TextView UserEmail { get; private set; }
        public TextView Timestamp { get; private set; }
        public View UpVoteImage { get; private set; }
        public View DownVoteImage { get; private set; }

        public VoteItemViewHolder(View itemView) : base(itemView)
        {
            UserEmail = itemView.FindViewById<TextView>(Resource.Id.list_item_vote_userEmail);
            Timestamp = itemView.FindViewById<TextView>(Resource.Id.list_item_vote_voteTimestamp);
            UpVoteImage = itemView.FindViewById<View>(Resource.Id.list_item_vote_upVoteImage);
            DownVoteImage = itemView.FindViewById<View>(Resource.Id.list_item_vote_downVoteImage);
        }
    }
}