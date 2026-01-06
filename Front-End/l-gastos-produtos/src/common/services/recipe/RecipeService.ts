import ICreateRecipe from '@/common/interfaces/recipe/ICreateRecipe';
import IReadRecipe from '@/common/interfaces/recipe/IReadRecipe';
import IUpdateRecipe from '@/common/interfaces/recipe/IUpdateRecipe';
import baseApi from '@/common/services/http/api';
const api = baseApi.create({
  baseURL: `${process.env.NEXT_PUBLIC_RECIPE_API_URL}`,
});

export const GetRecipeById = async (recipeId: string): Promise<IReadRecipe> => {
  try {
    const response = await api.get<IReadRecipe>(`/${recipeId}`);

    return response.data;
  } catch (error) {
    throw error;
  }
};

export const GetAllRecipes = async (groupId?: string): Promise<IReadRecipe[]> => {
  try {
    const url = groupId ? `/?groupId=${groupId}` : '/';
    const response = await api.get<IReadRecipe[]>(url);

    return response.data;
  } catch (error) {
    throw error;
  }
};

export const UpdateRecipe = async (recipeId: string, recipe: IReadRecipe) => {
  try {
    const recipeUpdateObj = { ...recipe } as IUpdateRecipe;
    await api.put(`/${recipeId}`, recipeUpdateObj);
  } catch (error) {
    throw error;
  }
};

export const CreateRecipe = async (recipe: IReadRecipe) => {
  try {
    const recipeCreateObj = { ...recipe } as ICreateRecipe;
    const response = await api.post('/', recipeCreateObj);

    return response.data;
  } catch (error) {
    throw error;
  }
};

export const DeleteRecipe = async (recipeId: string) => {
  try {
    await api.delete(`/${recipeId}`);
  } catch (error) {
    throw error;
  }
};
