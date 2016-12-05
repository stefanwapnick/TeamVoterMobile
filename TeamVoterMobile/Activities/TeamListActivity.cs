using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.Widget;
using TeamVoterMobile.ListViews;
using TeamVoterMobile.Models;

namespace TeamVoterMobile.Activities
{
    [Activity(Label = "Team List")]
    public class TeamListActivity : BaseActivity
    {
        public const string IntentExtraSelectedTeamId = "IntentExtraSelectedTeamId";

        private TeamListAdapter _teamListAdapter;
        private IList<Team> _teamList;
        
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.TeamListActivity);

            var teamListRecyclerView = FindViewById<RecyclerView>(Resource.Id.activity_team_list_recyclerView);
            _teamListAdapter = new TeamListAdapter();
            teamListRecyclerView.SetAdapter(_teamListAdapter);
            teamListRecyclerView.SetLayoutManager(new LinearLayoutManager(this));
            _teamListAdapter.ItemClicked += ItemClicked;
        }

        protected override async void OnResume()
        {
            base.OnResume();
            await RefreshTeamListAsync();
        }

        private void ItemClicked(object sender, int index)
        {
            Intent toDetailsIntent = new Intent(this, typeof(TeamDetailsActivity));
            toDetailsIntent.PutExtra(IntentExtraSelectedTeamId, _teamList[index].Id);
            StartActivity(toDetailsIntent);
        }

        private async Task RefreshTeamListAsync()
        {
            _teamList = (await TeamService.GetTeams()).OrderByDescending(t => t.TotalScore).ToList();
            _teamListAdapter.RefreshTeams(_teamList);
        }

    }
}

