import { ICreateProduct } from "./ICreateProduct"

export interface IUpdateProduct extends ICreateProduct
{
  unitPrice: number
}