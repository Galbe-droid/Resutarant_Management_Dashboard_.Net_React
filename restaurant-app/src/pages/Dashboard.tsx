import {Box, Button, CircularProgress, Divider, Grid, Stack, TableContainer, Typography} from "@mui/material";
import {DashboardCard} from "../components/common/DashboardCard.tsx";
import {ReceiptLong, TableBarOutlined, TableRestaurant} from "@mui/icons-material";
import {DashboardTable} from "../components/tables/DashboardTable.tsx";
import {useEffect, useState} from "react";
import {getAllTables} from "../services/TableServices.tsx";
import type {TableSummaryDto} from "../types/tables/TableSummaryDto.ts";
import {CreateTableDialog} from "../components/tables/CreateTableDialog.tsx";
import {useTranslation} from "react-i18next";
import {TableStatus} from "../enum/TableStatus.ts";

export function Dashboard() {
    const { t } = useTranslation("dashboard");
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

    const totalTablesCount = tables.length;
    const freeTablesCount = tables.filter(table => table.tableStatus === TableStatus.Avaliable).length;
    const reservedTablesCount = tables.filter(table => table.tableStatus === TableStatus.Reserved).length;


    useEffect(() => {
        loadTables();
    }, [])

    return (
        <Grid container spacing={3}>
            <Grid size={{xs: 12, md: 3}}>
                <DashboardCard title={t("freeTables")} value={freeTablesCount.toString() + "/" + totalTablesCount.toString()} icon={<TableRestaurant/>}/>
            </Grid>
            <Grid size={{xs: 12, md: 3}}>
                <DashboardCard title={t("reservedTables")} value={reservedTablesCount.toString() + "/" + totalTablesCount.toString()} icon={<TableBarOutlined/>}/>
            </Grid>
            <Grid size={{xs: 12, md: 3}}>
                <DashboardCard title={t("orders")} value={"15"} icon={<ReceiptLong/>}/>
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
                    <Typography variant="h5" sx={{fontWeight: 600}}>{t("title")}</Typography>
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
                            {t("newTable")}
                        </Button>
                    </Box>
                    {loading ? <CircularProgress/> : <DashboardTable tables={tables}/>}
                </TableContainer>
            </Box>
            <CreateTableDialog open={openCreateDialog} onClose={() => setOpenCreateDialog(false)} onCreated={loadTables} />
        </Grid>
    )
}