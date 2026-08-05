import type {ReturnCategoryDto} from "../types/categories/ReturnCategoryDto.ts";
import {api, type ApiResponse} from "../api/axios.ts";

export const getAllCategories = async (): Promise<ReturnCategoryDto[]> => {
    const response = await api.get<ApiResponse<ReturnCategoryDto[]>>(`/Categories`);
    return response.data.data;
}