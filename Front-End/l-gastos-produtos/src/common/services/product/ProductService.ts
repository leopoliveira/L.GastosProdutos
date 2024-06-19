import { ICreateProduct } from "@/common/interfaces/product/ICreateProduct";
import { IReadProduct } from "@/common/interfaces/product/IReadProduct";
import { IUpdateProduct } from "@/common/interfaces/product/IUpdateProduct";
import axios from "axios";

const api = axios.create({
  baseURL: process.env.NEXT_PUBLIC_PRODUCT_API_URL
});

export const GetProductById = async (producId: string): Promise<IReadProduct> =>
{
  try
  {
    const response = await api.get<IReadProduct>(`/${ producId }`);

    return response.data;
  }
  catch (error)
  {
    throw error;
  }
}

export const GetAllProducts = async (): Promise<IReadProduct[]> =>
{
  try
  {
    const response = await api.get<IReadProduct[]>("/");

    return response.data;
  }
  catch (error)
  {
    throw error;
  }
}

export const UpdateProduct = async (producId: string, product: IReadProduct) =>
{
  try
  {
    const productUpdateObj = { ...product } as IUpdateProduct;
    await api.put(`/${ producId }`, productUpdateObj);
  }
  catch (error)
  {
    throw error;
  }
}

export const CreateProduct = async (product: IReadProduct) =>
{
  try
  {
    const productCreateObj = { ...product } as ICreateProduct;
    const response = await api.post("/", productCreateObj);

    return response.data;
  }
  catch (error)
  {
    throw error;
  }
}

export const DeleteProduct = async (producId: string) =>
{
  try
  {
    await api.delete(`/${ producId }`);
  }
  catch (error)
  {
    throw error;
  }
}