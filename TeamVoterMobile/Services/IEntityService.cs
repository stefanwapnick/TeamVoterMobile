using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeamVoterMobile.Models;

namespace TeamVoterMobile.Services
{
    public interface IEntityService<T> where T : EntityData
    {
        Task<IList<T>> GetEntitiesAsync();

        Task<T> GetEntityAsync(string entityId);

        Task AddEntityAsync(T entity);
    }
}