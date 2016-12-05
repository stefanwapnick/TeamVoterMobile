using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Microsoft.WindowsAzure.MobileServices;
using TeamVoterMobile.Models;

namespace TeamVoterMobile.Services
{
    public class TeamService : EntityService<Team>, ITeamService
    {
        private readonly IAuthenticationService _authenticationService;

        public TeamService(MobileServiceClient client, IAuthenticationService authenticationService) : base(client)
        {
            if(authenticationService == null)
                throw new ArgumentNullException(nameof(authenticationService));

            _authenticationService = authenticationService;
        }

        public Task<IList<Team>> GetTeams()
        {
            return GetEntitiesAsync();
        }

        public Task<Team> GetTeam(string teamId)
        {
            return GetEntityAsync(teamId);
        }

        public async Task SubmitVote(string teamId, bool isUpvote)
        {
            string userId = _authenticationService.UserId;

            if (string.IsNullOrEmpty(userId))
                Log.Error(nameof(TeamService), "No user currently logged in. Cannot vote");

            try
            {
                await MobileClient.GetTable<Vote>().InsertAsync(new Vote(isUpvote, teamId, userId));
            }
            catch (Exception e)
            {
                Log.Error(GetType().Name, e.ToString());
            }
            
        }
    }
}