import type {TableStatus} from "../../enum/TableStatus.ts";

export interface TableSummaryDto {
    id: string;
    number: number;
    capacity: number;
    tableStatus: TableStatus;
    reservationName?: string;
    reservationTime?: string | null;
}