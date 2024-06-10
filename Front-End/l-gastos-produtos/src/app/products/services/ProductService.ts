import { productApi } from "@/common/services/axios-config/AxiosApis";

const api = await productApi();

export const getById = async (id: string) =>
{
  const response = await api.get(`/${ id }`);

  return response.data;
}