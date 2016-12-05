using System;
using System.Collections.Generic;
using System.Globalization;
using Android.Support.V7.Widget;
using Android.Views;
using TeamVoterMobile.Models;

namespace TeamVoterMobile.ListViews
{
    public class TeamListAdapter : RecyclerView.Adapter
    {
        private IList<Team> _teamList;

        public EventHandler<int> ItemClicked;

        public TeamListAdapter() : this(null)
        { }

        public TeamListAdapter(IList<Team> teamList)
        {
            RefreshTeams(teamList);
        }

        public void RefreshTeams(IList<Team> teamList)
        {
            if (teamList == null)
            {
                _teamList = new List<Team>();
                return;
            }

            _teamList = teamList;
            NotifyDataSetChanged();
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            TeamItemViewHolder viewHolder = holder as TeamItemViewHolder;
            if (viewHolder == null)
                return;

            Team team = _teamList[position];

            viewHolder.TeamNameText.Text = team.Name;
            viewHolder.UpVoteText.Text = team.UpVotes.ToString();
            viewHolder.DownVoteText.Text = team.DownVotes.ToString();
            viewHolder.LastUpdateTimestamp.Text = $"Last vote: {(team.LastVoteTimestamp?.ToString(@"yyyy/MM/dd hh:mm:ss tt", CultureInfo.InvariantCulture) ?? "None")}";
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View cellView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.TeamListItem, parent, false);
            return new TeamItemViewHolder(cellView, OnItemClicked);
        }

        private void OnItemClicked(int position)
        {
            ItemClicked?.Invoke(this, position);
        }

        public override int ItemCount => _teamList.Count;
    }
}