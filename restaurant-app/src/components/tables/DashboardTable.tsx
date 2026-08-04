import {Chip, Table, TableBody, TableCell, TableHead, TableRow} from "@mui/material";
import type {TableStatus} from "../../enum/TableStatus.ts";
import type {TableSummaryDto} from "../../types/tables/TableSummaryDto.ts";
import {useNavigate} from "react-router-dom";
import {mapFormatDate} from "../../mappers/mapFormatDate.tsx";
import {useTranslation} from "react-i18next";

interface DashboardTableProps {
    tables: TableSummaryDto[] | null;
}

export function DashboardTable({tables}: DashboardTableProps) {
    const { t } = useTranslation("table");
    const navigate = useNavigate();
    const getStatusChip = (status: TableStatus) => {
        switch (status) {
            case 0:
                return <Chip label={t("statusAvaliable")} color="success"/>;

            case 1:
                return <Chip label={t("statusOccupied")} color="error"/>;

            case 2:
                return <Chip label={t("statusReserved")} color="warning"/>;

            default:
                return <Chip label={status} />;
        }
    }


    return (
        <Table>
            <TableHead>
                <TableRow>
                    <TableCell sx={{textAlign:"center"}}>{t("cell1")}</TableCell>
                    <TableCell sx={{textAlign:"center"}}>{t("cell2")}</TableCell>
                    <TableCell sx={{textAlign:"center"}}>{t("cell3")}</TableCell>
                    <TableCell sx={{textAlign:"center"}}>{t("cell4")}</TableCell>
                    <TableCell sx={{textAlign:"center"}}>{t("cell5")}</TableCell>
                </TableRow>
            </TableHead>
            <TableBody>
                {tables?.map((table) => (
                    <TableRow
                        key={table.id}
                        hover
                        onClick={() => navigate(`/tables/${table.id}`)}
                        sx={{
                            cursor: "pointer",
                            "&:hover": {
                                backgroundColor: "action.hover"
                            }
                        }}
                    >
                        <TableCell sx={{textAlign:"center"}}>{table.number}</TableCell>
                        <TableCell sx={{textAlign:"center"}}>{table.capacity}</TableCell>
                        <TableCell sx={{textAlign:"center"}}>{getStatusChip(table.tableStatus)}</TableCell>
                        <TableCell sx={{textAlign:"center"}}>{table.reservationName}</TableCell>
                        <TableCell sx={{textAlign:"center"}}>{mapFormatDate(table.reservationTime)}</TableCell>
                    </TableRow>
                ))}
            </TableBody>
        </Table>
    )
}