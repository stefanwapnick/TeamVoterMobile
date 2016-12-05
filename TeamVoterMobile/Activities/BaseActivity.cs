using Android.App;
using Android.OS;
using TeamVoterMobile.Services;

namespace TeamVoterMobile.Activities
{
    public class BaseActivity : Activity
    {
        protected ITeamService TeamService;

        protected IAuthenticationService AuthenticationService;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            TeamVoterApplication application = Application as TeamVoterApplication;
            TeamService = application?.TeamService;
            AuthenticationService = application?.AuthenticationService;
        }

    }
}