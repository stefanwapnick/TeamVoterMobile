using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Android.App;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.V7.Widget;
using TeamVoterMobile.ListViews;
using TeamVoterMobile.Models;

namespace TeamVoterMobile.Activities
{
    [Activity(Label = "Team Details")]
    public class TeamDetailsActivity : BaseActivity
    {
        private View _upVoteButton;
        private View _downVoteButton;
        private TextView _teamNameText;
        private TextView _upVoteCountText;
        private TextView _downVoteCountText;
        private View _loadingIndicator;
        private RecyclerView _voteListRecyclerView;
        private string _teamId;
        
        private VoteListAdapter _votesListAdapter;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.TeamDetailsActivity);

            _upVoteButton = FindViewById<View>(Resource.Id.activity_details_upVoteButton);
            _downVoteButton = FindViewById<View>(Resource.Id.activity_details_downVoteButton);
            _teamNameText = FindViewById<TextView>(Resource.Id.activity_details_teamNameText);
            _upVoteCountText = FindViewById<TextView>(Resource.Id.activity_details_upVotesText);
            _downVoteCountText = FindViewById<TextView>(Resource.Id.activity_details_downVotesText);
            _loadingIndicator = FindViewById(Resource.Id.activity_details_loadingIndicator);

            _upVoteButton.Click += UpVoteButtonOnClick;
            _downVoteButton.Click += DownVoteButtonOnClick;

            _teamId = Intent.GetStringExtra(TeamListActivity.IntentExtraSelectedTeamId) ?? string.Empty;

            _voteListRecyclerView = FindViewById<RecyclerView>(Resource.Id.activity_details_votingHistoryList);
            _votesListAdapter = new VoteListAdapter();
            _voteListRecyclerView.SetAdapter(_votesListAdapter);
            _voteListRecyclerView.SetLayoutManager(new LinearLayoutManager(this));
        }

        protected override async void OnResume()
        {
            base.OnResume();
            await RefreshViewAsync();
        }

        private void DownVoteButtonOnClick(object sender, EventArgs eventArgs)
        {
            OnVote(false);
        }

        private void UpVoteButtonOnClick(object sender, EventArgs eventArgs)
        {
            OnVote(true);
        }

        private async void OnVote(bool isUpVote)
        {
            SetLoadingView(true);
            await TeamService.SubmitVote(_teamId, isUpVote);
            await RefreshViewAsync();
            SetLoadingView(false);
        } 

        private void SetLoadingView(bool isLoading)
        {
            float controlsAlpha = isLoading ? 0.5f : 1.0f; 
            _loadingIndicator.Visibility = isLoading ? ViewStates.Visible : ViewStates.Gone;
            _upVoteButton.Enabled = _downVoteButton.Enabled = !isLoading;
            _downVoteButton.Alpha = _upVoteButton.Alpha = _voteListRecyclerView.Alpha = controlsAlpha;
        }

        private async Task RefreshViewAsync()
        {
            if (string.IsNullOrEmpty(_teamId))
                return;

            Team team = await TeamService.GetTeam(_teamId);

            if (team == null)
                return;

            _teamNameText.Text = team.Name;
            _upVoteCountText.Text = team.UpVotes.ToString();
            _downVoteCountText.Text = team.DownVotes.ToString();
            _votesListAdapter.SetVotes(team.Votes);
            _votesListAdapter.NotifyDataSetChanged();
        }
    }
}

