﻿using DAL.Data;
using Shared.Filters;
using Shared.Modifiers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.DataAccess
{
    public abstract class BaseModifiableGetDataAccess<T1, T2, T3> : BaseGetDataAccess<T1, T2>, IModifiableGet<T1, T2, T3> where T1 : class, IDao, new() where T2 : BaseFilter, new() where T3 : BaseModifier
    {
        public async Task<IList<T1>> GetAll(T3 modifier)
        {
            var entities = await GetAll();
            if (modifier != null)
                await ApplyModifiers(entities, modifier);

            return entities;
        }

        public async Task<IList<T1>> Get(T2 filter, T3 modifier)
        {
            var entities = await Get(filter);
            if (modifier != null)
                await ApplyModifiers(entities, modifier);

            return entities;
        }

        public async Task<T1> Get(int id, T3 modifier)
        {
            var entity = await Get(id);
            if (modifier != null && entity != null)
                await ApplyModifiers(entity, modifier);

            return entity;
        }

        protected abstract Task ApplyModifiers(T1 entity, T3 modifier);

        protected async Task ApplyModifiers(IList<T1> entities, T3 modifier)
        {
            foreach (var entity in entities)
                await ApplyModifiers(entity, modifier);
        }
    }

    public abstract class BaseModifiableCrudDataAccess<T1, T2, T3> : BaseCrudDataAccess<T1, T2>, IModifiableCrud<T1, T2, T3> where T1 : class, IDao, new() where T2 : BaseFilter, new() where T3 : BaseModifier
    {
        public async Task<IList<T1>> GetAll(T3 modifier)
        {
            var entities = await GetAll();
            if (modifier != null)
                await ApplyModifiers(entities, modifier);

            return entities;
        }

        public async Task<IList<T1>> Get(T2 filter, T3 modifier)
        {
            var entities = await Get(filter);
            if (modifier != null)
                await ApplyModifiers(entities, modifier);

            return entities;
        }

        public async Task<T1> Get(int id, T3 modifier)
        {
            var entity = await Get(id);
            if (modifier != null && entity != null)
                await ApplyModifiers(entity, modifier);

            return entity;
        }

        protected abstract Task ApplyModifiers(T1 entity, T3 modifier);

        protected async Task ApplyModifiers(IList<T1> entities, T3 modifier)
        {
            foreach (var entity in entities)
                await ApplyModifiers(entity, modifier);
        }
    }

}