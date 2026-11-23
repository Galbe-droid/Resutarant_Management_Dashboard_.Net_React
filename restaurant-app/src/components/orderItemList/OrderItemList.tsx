import type ReturnOrderItemDto from "../../types/ordersItens/ReturnOrderItemDto.ts";
import {
    Box,
    IconButton,
    Paper,
    Table,
    TableBody,
    TableCell,
    TableContainer,
    TableHead,
    TableRow, Tooltip,
    Typography
} from "@mui/material";
import EditIcon from "@mui/icons-material/Edit";
import DeleteIcon from "@mui/icons-material/Delete";

interface OrderItemsListProps {
    items: ReturnOrderItemDto[];
    onEdit: (item: ReturnOrderItemDto) => void;
    onDelete: (item: ReturnOrderItemDto) => void;
    canEdit: boolean;
    totalAmount: number;
}

export function OrderItemList(props: OrderItemsListProps) {
    const currency = (value: number) =>
        value.toLocaleString("pt-BR", {
            style: "currency",
            currency: "BRL"
        });

    return(
        <Box
            sx={{
                display: "flex",
                flexDirection: "column",
                gap: 2
            }}
        >
            <Typography variant="h6">Ordered Items</Typography>
            <TableContainer component={Paper}>
                <Table>
                    <TableHead>
                        <TableRow>
                            <TableCell>Product</TableCell>
                            <TableCell align="center">Quantity</TableCell>
                            <TableCell align="right">Total</TableCell>
                            <TableCell align="center">Actions</TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {props.items?.length > 0 ? (
                            props.items.map((item) => (
                                <TableRow key={item.id}>
                                    <TableCell>{item.productName}</TableCell>
                                    <TableCell align="center">{item.quantity}</TableCell>
                                    <TableCell align="right">{currency(item.totalPrice)}</TableCell>
                                    {props.canEdit &&(
                                        <TableCell align="center">
                                            <Tooltip title="Edit item">
                                                <IconButton
                                                    color="primary"
                                                    onClick={() => props.onEdit(item)}
                                                >
                                                    <EditIcon />
                                                </IconButton>
                                            </Tooltip>

                                            <Tooltip title="Remove item">
                                                <IconButton
                                                    color="error"
                                                    onClick={() => props.onDelete(item)}
                                                >
                                                    <DeleteIcon />
                                                </IconButton>
                                            </Tooltip>
                                        </TableCell>
                                    )}
                                </TableRow>
                            ))
                        ) : (
                            <TableRow>
                                <TableCell colSpan={3} align="center">
                                    No items in this order.
                                </TableCell>
                            </TableRow>
                        )}
                    </TableBody>
                </Table>
            </TableContainer>
            <Box sx={{
                display: "flex",
                justifyContent: "flex-end",
                padding: 2
            }}>
                <Typography variant="h6">
                    Total: {currency(props.totalAmount)}
                </Typography>
            </Box>
        </Box>
    )
}