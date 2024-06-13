import { IProduct } from "@/common/interfaces/IProduct";
import axios from "axios";

const api = axios.create({
  baseURL: process.env.NEXT_PUBLIC_PRODUCT_API_URL
});

export const GetProductById = async (producId: string): Promise<IProduct> =>
{
  try
  {
    const response = await api.get<IProduct>(`/${ producId }`);
    return response.data;
  }
  catch (error)
  {
    throw error;
  }
}

export const GetAllProducts = async (): Promise<IProduct[]> =>
{
  try
  {
    const response = await api.get<IProduct[]>("/");
    return response.data;
  }
  catch (error)
  {
    throw error;
  }
}