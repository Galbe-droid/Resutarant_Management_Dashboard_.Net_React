import {Box, Button, CircularProgress, Divider, Grid, Stack, TableContainer, Typography} from "@mui/material";
import {useEffect, useState} from "react";
import {DashboardCategoryTable} from "../../components/categories/DashboardCategoryTable.tsx";
import type {ReturnCategoryDashboardDto} from "../../types/categories/ReturnCategoryDashboardDto.ts";
import {getCategoriesDashboard} from "../../services/CategoryServices.tsx";
import {CreateCategoryDialog} from "../../components/categories/CreateCategoryDialog.tsx";

export function CategoryDashboard(){
    const [categories, setCategories] = useState<ReturnCategoryDashboardDto[]>();
    const [loading, setLoading] = useState(true);
    const [openCreateDialog, setOpenCreateDialog] = useState(false);

    const loadDashboard = async() => {
        try{
            const response = await getCategoriesDashboard();
            if(response){
                setCategories(response);
            }
        }finally {
            setLoading(false);
        }
    }

    useEffect(() => {
        const load = async () => {
            await Promise.all([
                loadDashboard()
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
                    <Typography variant="h5" sx={{fontWeight: 600}}>Categorias</Typography>
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
                            Nova Categoria
                        </Button>
                    </Box>
                    {loading ? <CircularProgress/> : <DashboardCategoryTable categories={categories!}/>}
                </TableContainer>
            </Box>
            <CreateCategoryDialog open={openCreateDialog} onClose={() => setOpenCreateDialog(false)} onCreated={loadDashboard}/>
        </Grid>
    )
}