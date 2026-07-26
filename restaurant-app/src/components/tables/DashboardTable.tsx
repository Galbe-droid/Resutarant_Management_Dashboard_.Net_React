import {Chip, Table, TableBody, TableCell, TableHead, TableRow} from "@mui/material";
import type {TableStatus} from "../../enum/TableStatus.ts";
import type {TableSummaryDto} from "../../types/tables/TableSummaryDto.ts";

interface DashboardTableProps {
    tables: TableSummaryDto[] | null;
}

export function DashboardTable({tables}: DashboardTableProps) {
    const getStatusChip = (status: TableStatus) => {
        switch (status) {
            case 0:
                return <Chip label="Livre" color="success"/>;

            case 1:
                return <Chip label="Ocupada" color="error"/>;

            case 2:
                return <Chip label="Rerservada" color="warning"/>;

            default:
                return <Chip label={status} />;
        }
    }


    return (
        <Table>
            <TableHead>
                <TableRow>
                    <TableCell sx={{textAlign:"center"}}>Mesa</TableCell>
                    <TableCell sx={{textAlign:"center"}}>Capacidade</TableCell>
                    <TableCell sx={{textAlign:"center"}}>Estado</TableCell>
                    <TableCell sx={{textAlign:"center"}}>Nome Reserva</TableCell>
                    <TableCell sx={{textAlign:"center"}}>Data Reserva</TableCell>
                </TableRow>
            </TableHead>
            <TableBody>
                {tables?.map((table) => (
                    <TableRow key={table.id}>
                        <TableCell sx={{textAlign:"center"}}>{table.number}</TableCell>
                        <TableCell sx={{textAlign:"center"}}>{table.capacity}</TableCell>
                        <TableCell sx={{textAlign:"center"}}>{getStatusChip(table.tableStatus)}</TableCell>
                        <TableCell sx={{textAlign:"center"}}>{table.reservationName}</TableCell>
                        <TableCell sx={{textAlign:"center"}}>{table.reservationTime}</TableCell>
                    </TableRow>
                ))}
            </TableBody>
        </Table>
    )
}