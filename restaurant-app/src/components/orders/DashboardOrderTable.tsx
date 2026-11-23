import {Chip, Table, TableBody, TableCell, TableHead, TableRow} from "@mui/material";
import {useNavigate} from "react-router-dom";
import type {ReturnOrderDto} from "../../types/orders/ReturnOrderDto.ts";
import type {OrderStatus} from "../../enum/OrderStatus.ts";
import {mapFormatDate} from "../../mappers/mapFormatDate.tsx";
import {useTranslation} from "react-i18next";

interface DashboardOrderTableProps {
    orders: ReturnOrderDto[];
}

export function DashboardOrderTable({orders}: DashboardOrderTableProps) {
    const { t } = useTranslation("order");
    const navigate = useNavigate();

    const getStatusChip = (orderStatus: OrderStatus) => {
        switch (orderStatus) {
            case 0:
                return <Chip label={t("statusOpen")} color="success"/>;

            case 1:
                return <Chip label={t("statusAttention")} color="warning"/>;

            case 2:
                return <Chip label={t("statusFinished")} color="success"/>;

            case 3:
                return <Chip label={t("statusPaid")} color="success"/>;

            case 4:
                return <Chip label={t("statusCancelled")} color="error"/>;

            default:
                return <Chip label={status} />;
        }
    }

    return(
        <Table>
            <TableHead>
                <TableRow>
                    <TableCell sx={{textAlign:"center"}}>{t("cell1")}</TableCell>
                    <TableCell sx={{textAlign:"center"}}>{t("cell2")}</TableCell>
                    <TableCell sx={{textAlign:"center"}}>{t("cell3")}</TableCell>
                    <TableCell sx={{textAlign:"center"}}>{t("cell4")}</TableCell>
                </TableRow>
            </TableHead>
            <TableBody>
                {orders?.map((order) => (
                    <TableRow
                        key={order.id}
                        hover
                        onClick={() => navigate(`/orders/${order.id}`)}
                        sx={{
                            cursor: "pointer",
                            "&:hover": {
                                backgroundColor: "action.hover"
                            }
                        }}
                    >
                        <TableCell sx={{textAlign:"center"}}>{order.tableNumber}</TableCell>
                        <TableCell sx={{textAlign:"center"}}>{mapFormatDate(order.orderDate)}</TableCell>
                        <TableCell sx={{textAlign:"center"}}>{order.totalAmount}</TableCell>
                        <TableCell sx={{textAlign:"center"}}>{getStatusChip(order.orderStatus)}</TableCell>
                    </TableRow>
                ))}
            </TableBody>
        </Table>
    )
}