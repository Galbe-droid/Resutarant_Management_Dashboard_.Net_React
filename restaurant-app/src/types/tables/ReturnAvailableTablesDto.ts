import type {TableStatus} from "../../enum/TableStatus.ts";

export interface ReturnAvailableTablesDto {
    id: string;
    number: number;
    tableStatus: TableStatus;
}