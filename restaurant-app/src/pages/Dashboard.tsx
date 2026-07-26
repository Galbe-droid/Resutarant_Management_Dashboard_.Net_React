import {Box, Button, CircularProgress, Divider, Grid, Stack, TableContainer, Typography} from "@mui/material";
import {DashboardCard} from "../components/common/DashboardCard.tsx";
import {Payments, ReceiptLong, TableBarOutlined, TableRestaurant} from "@mui/icons-material";
import {DashboardTable} from "../components/tables/DashboardTable.tsx";
import {useEffect, useState} from "react";
import {getAllTables} from "../services/TableServices.tsx";
import type {TableSummaryDto} from "../types/tables/TableSummaryDto.ts";
import {CreateTableDialog} from "../components/tables/CreateTableDialog.tsx";

export function Dashboard() {
    const [tables, setTables] = useState<TableSummaryDto[]>([]);
    const [loading, setLoading] = useState(true);
    const [openCreateDialog, setOpenCreateDialog] = useState(false);

    const loadTables = async () => {
        try{
            const response = await getAllTables();
            if(response) {
                const summaries: TableSummaryDto[] = response.map(table => ({
                    id: table.id,
                    number: table.number,
                    capacity: table.capacity,
                    tableStatus: table.tableStatus,
                    reservationTime: table.reservationTime,
                    reservationName: table.reservationName,
                }))
                setTables(summaries);
            }
        }
        finally {
            setLoading(false);
        }
    }

    useEffect(() => {
        loadTables();
    }, [])

    return (
        <Grid container spacing={3}>
            <Grid size={{xs: 12, md: 3}}>
                <DashboardCard title={"Mesas Livres"} value={"8"} icon={<TableRestaurant/>}/>
            </Grid>
            <Grid size={{xs: 12, md: 3}}>
                <DashboardCard title={"Mesas Reservadas"} value={"2"} icon={<TableBarOutlined/>}/>
            </Grid>
            <Grid size={{xs: 12, md: 3}}>
                <DashboardCard title={"Pedidos"} value={"15"} icon={<ReceiptLong/>}/>
            </Grid>
            <Grid size={{xs: 12, md: 3}}>
                <DashboardCard title={"Receita Hoje"} value={"R$ 2.430"} icon={<Payments/>}/>
            </Grid>

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
                    <Typography variant="h5" sx={{fontWeight: 600}}>Status das Mesas</Typography>
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
                            Nova Mesa
                        </Button>
                    </Box>
                    {loading ? <CircularProgress/> : <DashboardTable tables={tables}/>}
                </TableContainer>
            </Box>
            <CreateTableDialog open={openCreateDialog} onClose={() => setOpenCreateDialog(false)} onCreated={loadTables} />
        </Grid>
    )
}