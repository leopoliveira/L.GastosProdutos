﻿using System.Linq.Expressions;
using L.GastosProdutos.Core.Domain.Entities.Recipe;
using MongoDB.Driver;

namespace L.GastosProdutos.Core.Interfaces
{
    public interface IRecipeRepository
    {
        Task<IReadOnlyList<RecipeEntity>> GetAllAsync();

        Task<RecipeEntity> GetByIdAsync(string id);

        Task<List<RecipeEntity>> GetByFilterAsync(Expression<Func<RecipeEntity, bool>> filter);

        Task<List<RecipeEntity>> GetByFilterAsync(FilterDefinition<RecipeEntity> filter);

        Task<long> CountIngredientsAsync(string recipeId);

        Task CreateAsync(RecipeEntity entity);

        Task UpdateAsync(string id, RecipeEntity entity);

        Task DeleteAsync(string id);
    }
}
