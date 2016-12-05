using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Microsoft.WindowsAzure.MobileServices;
using TeamVoterMobile.Models;

namespace TeamVoterMobile.Services
{
    public class AuthenticationService : EntityService<User>, IAuthenticationService
    {
        public AuthenticationService(MobileServiceClient client) : base(client)
        {
        }

        public User User { get; private set; }

        public string UserId => User?.Id ?? string.Empty;

        public async Task LoginAsync(string userEmail)
        {
            string trimmedEmail = userEmail.Trim().ToLower();
            User user = (await MobileClient.GetTable<User>()
                .Where(u => u.Email == trimmedEmail)
                .ToListAsync()).SingleOrDefault();

            if (user == null)
            {
                await AddEntityAsync(new User {Email = trimmedEmail});
                await LoginAsync(userEmail);
            }
            else
            {
                User = user;
            }
        }
    }
}