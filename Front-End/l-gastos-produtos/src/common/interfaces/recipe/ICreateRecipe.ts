import IngredientDto from './dtos/IngredientDto';
import PackingDto from './dtos/PackingDto';

export default interface ICreateRecipe {
  name: string;
  description?: string;
  ingredients: IngredientDto[];
  packings: PackingDto[];
  quantity: number;
  sellingValue: number;
  groupId?: string;
}
