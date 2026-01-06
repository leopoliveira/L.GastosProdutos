import ICreateRecipe from './ICreateRecipe';

export default interface IReadRecipe extends ICreateRecipe {
  id: string;
  totalCost: number;
  groupName?: string;
}
