using System;
using Android.App;
using Android.Runtime;
using Microsoft.WindowsAzure.MobileServices;
using TeamVoterMobile.Services;

namespace TeamVoterMobile
{
    [Application]
    public class TeamVoterApplication : Application
    {
        private const string ApiEndpoint = "https://mobiledevpresentation.azurewebsites.net";
        
        public ITeamService TeamService { get; private set; }

        public IAuthenticationService AuthenticationService { get; private set; }

        public TeamVoterApplication(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();
            var mobileService = new MobileServiceClient(ApiEndpoint);
            AuthenticationService = new AuthenticationService(mobileService);
            TeamService = new TeamService(mobileService, AuthenticationService);
        }
    }
}