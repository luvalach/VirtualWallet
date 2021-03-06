﻿using BL.Models;
using BL.Mapping;
using Shared.Filters;
using System.Threading.Tasks;
using System;

namespace BL.Service
{
    public class BaseCrudService<T1, T2, T3, T4> : BaseGetService<T1, T2, T3, T4>, ICrudService<T1, T4> where T1 : IDto where T2 : DAL.Data.IDao where T3 : class, DAL.DataAccess.ICrud<T2, T4>, new() where T4 : BaseFilter
    {
        public async Task InsertAsync(bool storeDbId, params T1[] entities)
        {
            if (storeDbId)
                await InsertAsync(_instance.InsertAsync, entities);
            else
                await _instance.InsertAsync(MapperInstance.Mapper.Map<T2[]>(entities));
        }

        public async Task InsertOrIgnoreAsync(bool storeDbId, params T1[] entities)
        {
            if (storeDbId)
                await InsertAsync(_instance.InsertOrIgnoreAsync, entities);
            else
                await _instance.InsertOrIgnoreAsync(MapperInstance.Mapper.Map<T2[]>(entities));
        }

        public async Task InsertOrReplaceAsync(bool storeDbId, params T1[] entities)
        {
            if (storeDbId)
                await InsertAsync(_instance.InsertOrReplaceAsync, entities);
            else
                await _instance.InsertOrReplaceAsync(MapperInstance.Mapper.Map<T2[]>(entities));
        }

        public async Task UpdateAsync(params T1[] entities)
        {
            await _instance.UpdateAsync(MapperInstance.Mapper.Map<T2[]>(entities));
        }

        public async Task DeleteAsync(int id)
        {
            await _instance.DeleteAsync(id);
        }

        public async Task DeleteAllAsync()
        {
            await _instance.DeleteAllAsync();
        }

        private async Task InsertAsync(Func<T2, Task> action, params T1[] entities)
        {
            foreach (var entity in entities)
            {
                var e = MapperInstance.Mapper.Map<T2>(entity);
                await action(e);
                entity.Id = e.Id;
            }
        }
    }
}
