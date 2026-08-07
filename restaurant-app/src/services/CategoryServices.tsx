import type {ReturnCategoryDto} from "../types/categories/ReturnCategoryDto.ts";
import {api, type ApiResponse} from "../api/axios.ts";
import type {CreateCategoryDto} from "../types/categories/CreateCategoryDto.ts";
import type {UpdateCategoryDto} from "../types/categories/UpdateCategoryDto.ts";
import type {ReturnCategoryDashboardDto} from "../types/categories/ReturnCategoryDashboardDto.ts";

export const getAllCategories = async (): Promise<ReturnCategoryDto[]> => {
    const response = await api.get<ApiResponse<ReturnCategoryDto[]>>(`/Categories`);
    return response.data.data;
}

export const getCategories = async (id: string): Promise<ReturnCategoryDto> => {
    const response = await api.get<ApiResponse<ReturnCategoryDto>>(`/Categories/${id}`);
    return response.data.data;
}

export const getCategoriesDashboard = async (): Promise<ReturnCategoryDashboardDto[]> => {
    const response = await api.get<ApiResponse<ReturnCategoryDashboardDto[]>>(`/Categories/dashboard`);
    return response.data.data;
}

export const createCategories = async (create: CreateCategoryDto): Promise<ReturnCategoryDto> => {
    const response = await api.post<ApiResponse<ReturnCategoryDto>>(`/Categories/create`, create);
    return response.data.data;
}

export const updateCategories = async (id: string, update: UpdateCategoryDto): Promise<ReturnCategoryDto> => {
    const response = await api.put<ApiResponse<ReturnCategoryDto>>(`/Categories/update/${id}`, update);
    return response.data.data;
}

export const deleteCategories = async (id: string): Promise<boolean> => {
    const response = await api.delete<ApiResponse<boolean>>(`/Categories/delete/${id}`);
    return response.data.data;
}
