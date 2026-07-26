import type {TableStatus} from "../../enum/TableStatus.ts";

export interface TableFormDto {
    number: number;
    capacity: number;
    tableStatus: TableStatus;
    reservationName?: string;
    reservationTime?: string | null;
}