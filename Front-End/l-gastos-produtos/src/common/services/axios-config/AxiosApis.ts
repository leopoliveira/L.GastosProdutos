import axios, { AxiosInstance } from "axios";
import { env } from "process";

export const productApi = async (): Promise<AxiosInstance> =>
{
  const api = axios.create({
    baseURL: env.PRODUCT_API_URL,
    timeout: 10000,
    headers: {
      "Content-Type": "application/json",
    },
  });

  return api;
}