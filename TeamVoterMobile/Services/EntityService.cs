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
using Javax.Crypto.Interfaces;
using Microsoft.WindowsAzure.MobileServices;
using TeamVoterMobile.Models;

namespace TeamVoterMobile.Services
{
    public abstract class EntityService<T> : IEntityService<T> where T : EntityData
    {

        protected readonly MobileServiceClient MobileClient;

        protected EntityService(MobileServiceClient client)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));

            MobileClient = client;
        }

        public async Task<IList<T>> GetEntitiesAsync()
        {
            return await MobileClient.GetTable<T>().ToListAsync();
        }

        public Task<T> GetEntityAsync(string entityId)
        {
            return MobileClient.GetTable<T>().LookupAsync(entityId);
        }

        public Task AddEntityAsync(T entity)
        {
            return MobileClient.GetTable<T>().InsertAsync(entity);
        }
    }
}