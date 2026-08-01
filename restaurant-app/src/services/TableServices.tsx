import type {ReturnTableDto} from "../types/tables/ReturnTableDto.ts";
import {api, type ApiResponse} from "../api/axios.ts";
import type {UpdateTableDto} from "../types/tables/UpdateTableDto.ts";
import type {CreateTableDto} from "../types/tables/CreateTableDto.ts";
import type {TableReservationUpdateDto} from "../types/tables/TableReservationUpdateDto.ts";
import type {TableStatusUpdateDto} from "../types/tables/TableStatusUpdateDto.ts";

export const getAllTables = async (): Promise<ReturnTableDto[]> => {
    const response = await api.get<ApiResponse<ReturnTableDto[]>>("/RestaurantTables");
    return response.data.data;
}

export const getTable = async (id: string): Promise<ReturnTableDto> => {
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

export const statusUpdateTable = async (id: string, status: TableStatusUpdateDto): Promise<ReturnTableDto> => {
    const response = await api.put(`/RestaurantTables/status/${id}`, status);
    return response.data.data;
}

export const reservationUpdateTable = async (id: string, reservation: TableReservationUpdateDto): Promise<ReturnTableDto> => {
    const response = await api.put<ApiResponse<ReturnTableDto>>(`/RestaurantTables/reservation/${id}`, reservation);
    return response.data.data;
}

export const cancelReservationTable = async (id: string): Promise<ReturnTableDto> => {
    const response = await api.put(`/RestaurantTables/cancel-reservation/${id}`);
    return response.data.data;
}

