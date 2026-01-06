import ICreateGroup from '@/common/interfaces/group/ICreateGroup';
import IReadGroup from '@/common/interfaces/group/IReadGroup';
import IUpdateGroup from '@/common/interfaces/group/IUpdateGroup';
import baseApi from '@/common/services/http/api';

const api = baseApi.create({
  baseURL: `${process.env.NEXT_PUBLIC_RECIPE_API_URL?.replace('/Recipe', '')}/Group`,
});

export const GetGroupById = async (groupId: string): Promise<IReadGroup> => {
  try {
    const response = await api.get<IReadGroup>(`/${groupId}`);
    return response.data;
  } catch (error) {
    throw error;
  }
};

export const GetAllGroups = async (): Promise<IReadGroup[]> => {
  try {
    const response = await api.get<IReadGroup[]>('/');
    return response.data;
  } catch (error) {
    throw error;
  }
};

export const UpdateGroup = async (groupId: string, group: IReadGroup) => {
  try {
    const groupUpdateObj = { ...group } as IUpdateGroup;
    await api.put(`/${groupId}`, groupUpdateObj);
  } catch (error) {
    throw error;
  }
};

export const CreateGroup = async (group: ICreateGroup) => {
  try {
    const response = await api.post('/', group);
    return response.data;
  } catch (error) {
    throw error;
  }
};

export const DeleteGroup = async (groupId: string) => {
  try {
    await api.delete(`/${groupId}`);
  } catch (error) {
    throw error;
  }
};
