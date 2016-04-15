﻿using DAL.Data;
using Shared.Filters;
using System.Threading.Tasks;

namespace DAL.DataAccess
{
    public interface ICrud<T1, T2> : IGet<T1, T2> where T1 : IDao where T2 : BaseFilter
    {
        Task Create(params T1[] entities);

        Task Update(params T1[] entities);

        Task Delete(int id);

        Task DeleteAll();
    }
}
