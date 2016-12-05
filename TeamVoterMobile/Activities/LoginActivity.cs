using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Views;
using Android.OS;
using Android.Util;
using Android.Widget;
using TeamVoterMobile.Models;

namespace TeamVoterMobile.Activities
{
    [Activity(Label = "TeamVoterMobile", MainLauncher = true, Icon = "@drawable/icon")]
    public class LoginActivity : BaseActivity
    {
        private View _loginButton;
        private EditText _loginEmail;
        private View _loadingIndicator;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            Window.RequestFeature(WindowFeatures.ActionBar);
            ActionBar.Hide();

            SetContentView(Resource.Layout.LoginActivity);

            _loginButton = FindViewById(Resource.Id.activity_login_loginButton);
            _loginEmail = FindViewById<EditText>(Resource.Id.activity_login_userEmail);
            _loadingIndicator = FindViewById(Resource.Id.activity_login_loadingIndicator);
            _loginButton.Click += LoginButtonOnClick;
        }

        protected override void OnResume()
        {
            base.OnResume();
            SetLoadingView(false);
        }

        private async void LoginButtonOnClick(object sender, EventArgs eventArgs)
        {
            string userEmail = _loginEmail.Text;
            if (string.IsNullOrWhiteSpace(userEmail))
                return;

            try
            {
                SetLoadingView(true);
                await AuthenticationService.LoginAsync(userEmail);
                ICollection<Team> teams = await TeamService.GetTeams();

                Intent navigateToTeamListIntent = new Intent(this, typeof(TeamListActivity));
                StartActivity(navigateToTeamListIntent);
            }
            catch (Exception e)
            {
                Log.Error(GetType().Name, e.ToString());
            }
            finally
            {
                SetLoadingView(false);
            }
        }

        private void SetLoadingView(bool isLoading)
        {
            _loginEmail.Visibility = isLoading ? ViewStates.Gone : ViewStates.Visible;
            _loadingIndicator.Visibility = isLoading ? ViewStates.Visible : ViewStates.Gone;
        }

    }
}

