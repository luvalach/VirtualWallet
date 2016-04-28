﻿using BL.Models;
using Shared.Filters;
using Shared.Modifiers;

namespace BL.Service
{
    public interface ICategoryRuleService : IModifiableGetService<CategoryRule, CategoryRuleFilter, CategoryRuleModifier>
    {
    }
}