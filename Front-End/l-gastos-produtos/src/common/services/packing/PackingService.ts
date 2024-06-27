import ICreatePacking from "@/common/interfaces/packing/ICreatePacking";
import IReadPacking from "@/common/interfaces/packing/IReadPacking";
import IUpdatePacking from "@/common/interfaces/packing/IUpdatePacking";
import PackingDto from "@/common/interfaces/recipe/dtos/PackingDto";
import axios from "axios";

const api = axios.create({
  baseURL: process.env.NEXT_PUBLIC_PACKING_API_URL
});

export const GetPackingById = async (packingId: string): Promise<IReadPacking> =>
{
  try
  {
    const response = await api.get<IReadPacking>(`/${ packingId }`);

    return response.data;
  }
  catch (error)
  {
    throw error;
  }
}

export const GetAllPackings = async (): Promise<IReadPacking[]> =>
{
  try
  {
    const response = await api.get<IReadPacking[]>("/");

    return response.data;
  }
  catch (error)
  {
    throw error;
  }
}

export const GetAllPackingsDto = async (): Promise<PackingDto[]> =>
{
  try
  {
    const response = await api.get<IReadPacking[]>("/");

    const packings: PackingDto[] = [];

    response.data.map((packing) =>
    {
      const packingUnitPrice = (packing.price / packing.quantity);

      packings.push({
        packingId: packing.id,
        packingName: packing.name,
        quantity: packing.quantity,
        packingUnitPrice: packingUnitPrice
      });
    });

    return packings;
  }
  catch (error)
  {
    throw error;
  }
}

export const UpdatePacking = async (packingId: string, packing: IReadPacking) =>
{
  try
  {
    const PackingUpdateObj = { ...packing } as IUpdatePacking;
    await api.put(`/${ packingId }`, PackingUpdateObj);
  }
  catch (error)
  {
    throw error;
  }
}

export const CreatePacking = async (packing: IReadPacking) =>
{
  try
  {
    const PackingCreateObj = { ...packing } as ICreatePacking;
    const response = await api.post("/", PackingCreateObj);

    return response.data;
  }
  catch (error)
  {
    throw error;
  }
}

export const DeletePacking = async (packingId: string) =>
{
  try
  {
    await api.delete(`/${ packingId }`);
  }
  catch (error)
  {
    throw error;
  }
}