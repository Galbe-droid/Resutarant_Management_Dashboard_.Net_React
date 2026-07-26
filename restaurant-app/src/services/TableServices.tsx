import type {ReturnTableDto} from "../types/tables/ReturnTableDto.ts";
import {api, type ApiResponse} from "../api/axios.ts";
import type {Params} from "react-router-dom";
import type {UpdateTableDto} from "../types/tables/UpdateTableDto.ts";
import type {CreateTableDto} from "../types/tables/CreateTableDto.ts";

export const getAllTables = async (): Promise<ReturnTableDto[]> => {
    const response = await api.get<ApiResponse<ReturnTableDto[]>>("/RestaurantTables");
    return response.data.data;
}

export const getTable = async (id: Readonly<Params<string>>): Promise<ReturnTableDto> => {
    const response = await api.get<ApiResponse<ReturnTableDto>>(`/RestaurantTables/${id}`);
    return response.data.data;
}

export const createTable = async (createTable: CreateTableDto): Promise<ReturnTableDto> => {
    const response = await api.post<ApiResponse<ReturnTableDto>>("/RestaurantTables/create", createTable);
    return response.data.data;
}

export const updateTable = async (updateTable: UpdateTableDto): Promise<ReturnTableDto> => {
    const response = await api.put<ApiResponse<ReturnTableDto>>(`/RestaurantTables/update/${updateTable.id}`, updateTable);
    return response.data.data;
}