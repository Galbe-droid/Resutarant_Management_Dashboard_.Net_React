import type {ReturnProductDto} from "../types/products/ReturnProductDto.ts";
import {api, type ApiResponse} from "../api/axios.ts";
import type {CreateProductDto} from "../types/products/CreateProductDto.ts";

export const getAllProducts = async (): Promise<ReturnProductDto[]> => {
    const response = await api.get<ApiResponse<ReturnProductDto[]>>(`/Products`);
    return response.data.data;
}

export const getProduct = async (id: string): Promise<ReturnProductDto> => {
    const response = await api.get<ApiResponse<ReturnProductDto>>(`/Products/${id}`);
    return response.data.data;
}

export const createProduct = async (data: CreateProductDto): Promise<ReturnProductDto> => {
    const response = await api.post<ApiResponse<ReturnProductDto>>(`/Products/create`, data);
    return response.data.data;
}