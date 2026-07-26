import type {TableStatus} from "../../enum/TableStatus.ts";

export interface CreateTableDto {
    number: number;
    capacity: number;
    tableStatus: TableStatus;
    reservationName?: string;
    reservationTime?: string;
}