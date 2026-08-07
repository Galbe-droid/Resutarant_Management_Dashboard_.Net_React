import {Box, Button, CircularProgress, Divider, Grid, Stack, TableContainer, Typography} from "@mui/material";
import {DashboardCard} from "../../components/common/DashboardCard.tsx";
import {ProductionQuantityLimits} from "@mui/icons-material";
import {useEffect, useState} from "react";
import {DashboardProductTable} from "../../components/products/DashboardProductTable.tsx";
import {getAllProducts} from "../../services/ProductServices.tsx";
import type {ReturnProductDto} from "../../types/products/ReturnProductDto.ts";
import {CreateProductDialog} from "../../components/products/CreateProductDialog.tsx";
import {getAllCategories} from "../../services/CategoryServices.tsx";
import type {ReturnCategoryDto} from "../../types/categories/ReturnCategoryDto.ts";
import {useTranslation} from "react-i18next";

export function ProductsDashboard() {
    const { t } = useTranslation("product");
    const [products, setProducts] = useState<ReturnProductDto[]>([]);
    const [categories, setCategories] = useState<ReturnCategoryDto[]>([]);
    const [loading, setLoading] = useState(true);
    const [openCreateDialog, setOpenCreateDialog] = useState(false);

    const loadProducts = async () => {
        try{
            const response = await getAllProducts();
            if(response) {
                setProducts(response);
            }
        }
        finally {
            setLoading(false);
        }
    }

    const loadCategories = async () => {
        try {
            const response = await getAllCategories();
            setCategories(response);
        } catch (error) {
            console.error(error);
        }
    };

    const productCount = products?.length;
    const categoryCount = categories?.length;

    useEffect(() => {
        const load = async () => {
            await Promise.all([
                loadProducts(),
                loadCategories(),
            ]);
        };

        load();
    }, [])

    return (
        <Grid container spacing={3}>
            <Grid size={{xs: 12, md: 3}}>
                <DashboardCard title={t("productQuantity")} value={productCount.toString()} icon={<ProductionQuantityLimits/>}/>
            </Grid>
            <Grid size={{xs: 12, md: 3}}>
                <DashboardCard title={t("categoryQuantity")} value={categoryCount.toString()} icon={<ProductionQuantityLimits/>}/>
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
                            {t("newButtonDashboard")}
                        </Button>
                    </Box>
                    {loading ? <CircularProgress/> : <DashboardProductTable products={products} categories={categories}/>}
                </TableContainer>
            </Box>
            <CreateProductDialog open={openCreateDialog} onClose={() => setOpenCreateDialog(false)} onCreated={loadProducts} categories={categories}/>
        </Grid>
    )
}