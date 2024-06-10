import { UnitOfMeasure } from "../enums/unit-of-measure.enum"

export interface IProduct
{
  id: string,
  name: string,
  quantity: number,
  price: number,
  unitOfMeasure: UnitOfMeasure
  unitPrice: number
}