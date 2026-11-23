import {Box, Button, CircularProgress, Divider, Grid, Stack, TableContainer, Typography} from "@mui/material";
import {useEffect, useState} from "react";
import {DashboardOrderTable} from "../../components/orders/DashboardOrderTable.tsx";
import {CreateOrderDialog} from "../../components/orders/CreateOrderDialog.tsx";
import {getAllOrders} from "../../services/OrderServices.tsx";
import type {ReturnOrderDto} from "../../types/orders/ReturnOrderDto.ts";
import type {ReturnAvailableTablesDto} from "../../types/tables/ReturnAvailableTablesDto.ts";
import {getAllAvailableTables} from "../../services/TableServices.tsx";

export function OrderDashboard() {
    const [orders, setOrders] = useState<ReturnOrderDto[]>([]);
    const [loading, setLoading] = useState(true);
    const [openCreateDialog, setOpenCreateDialog] = useState(false);
    const [availableTables, setAvailableTables] = useState<ReturnAvailableTablesDto[] | null>([]);

    const loadAvailableTables = async () => {
        try{
            const response = await getAllAvailableTables();
            setAvailableTables(response);
        }
        catch(error){
            console.log(error);
        }
    }

    const loadDashboard = async() => {
        try{
            const response = await getAllOrders();
            setOrders(response);
        }
        finally {
            setLoading(false);
        }
    }

    useEffect(() => {
        const load = async () => {
            await Promise.all([
                loadDashboard(),
                loadAvailableTables()
            ]);
        };

        load();
    }, [])

    return(
        <Grid container spacing={3}>
            <Box
                sx={{
                    width: "100%",
                    display: "flex",
                    flexGrow: 1,
                    flexDirection: "column",
                    justifyContent: "space-between",
                }}
            >
                <Stack sx={{display: "flex", flexDirection: "row", alignItems: "center", spacing: 1, mt:5, mb:3}}>
                    <Typography variant="h5" sx={{fontWeight: 600}}>Pedidos</Typography>
                    <Divider sx={{ flex: 1 }}/>
                </Stack>
                <TableContainer
                    sx={{
                        width: "100%",
                        display: "flex",
                        flexDirection: "column",
                        alignItems: "center",
                        justifyContent: "space-between",
                    }}
                >
                    <Box>
                        <Button
                            variant="contained"
                            onClick={() => setOpenCreateDialog(true)}
                        >
                            Novo Pedido
                        </Button>
                    </Box>
                    {loading ? <CircularProgress/> : <DashboardOrderTable orders={orders}/>}
                </TableContainer>
            </Box>
            <CreateOrderDialog open={openCreateDialog} onClose={() => setOpenCreateDialog(false)} availableTables={availableTables!} onCreated={loadDashboard} />
        </Grid>
    )
}