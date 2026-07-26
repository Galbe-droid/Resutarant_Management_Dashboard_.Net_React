import type {TableStatus} from "../../enum/TableStatus.ts";

export interface UpdateTableDto {
    id: string;
    number: number;
    capacity: number;
    tableStatus: TableStatus;
    reservationName?: string;
    reservationTime?: string;
}