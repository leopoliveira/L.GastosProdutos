﻿using System.Linq.Expressions;

using L.GastosProdutos.Core.Domain.Entities.Receipt;

using MongoDB.Driver;

namespace L.GastosProdutos.Core.Application.Interfaces
{
    public interface IRecipeRepository
    {
        Task<IReadOnlyList<RecipeEntity>> GetAllAsync();

        Task<RecipeEntity> GetByIdAsync(string id);

        Task<IReadOnlyList<RecipeEntity>> GetByFilterAsync(Expression<Func<RecipeEntity, bool>> filter);

        Task<IReadOnlyList<RecipeEntity>> GetByFilterAsync(FilterDefinition<RecipeEntity> filter);

        Task<int> CountIngredientsAsync(string recipeId);

        Task CreateAsync(RecipeEntity entity);

        Task UpdateAsync(string id, RecipeEntity entity);

        Task DeleteAsync(string id);
    }
}
