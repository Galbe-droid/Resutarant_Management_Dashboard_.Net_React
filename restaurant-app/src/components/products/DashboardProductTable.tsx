import type {ReturnProductDto} from "../../types/products/ReturnProductDto.ts";
import {Table, TableBody, TableCell, TableHead, TableRow} from "@mui/material";
import {mapDecimals} from "../../mappers/mapDecimals.tsx";
import type {ReturnCategoryDto} from "../../types/categories/ReturnCategoryDto.ts";
import {useNavigate} from "react-router-dom";
import {useTranslation} from "react-i18next";

interface DashboardProductTableProps {
    products: ReturnProductDto[] | null;
    categories: ReturnCategoryDto[];
}

export function DashboardProductTable({products, categories}: DashboardProductTableProps) {
    const navigate = useNavigate();
    const { t } = useTranslation("product");

    return (
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
                {products?.map((product) => (
                    <TableRow
                        key={product.id}
                        hover
                        onClick={() => navigate(`/products/${product.id}`)}
                        sx={{
                            cursor: "pointer",
                            "&:hover": {
                                backgroundColor: "action.hover"
                            }
                        }}
                    >
                        <TableCell sx={{textAlign:"center"}}>{product.name}</TableCell>
                        <TableCell sx={{textAlign:"center"}}>{mapDecimals(product.price)}</TableCell>
                        <TableCell sx={{textAlign:"center"}}>{categories.find(category => category.id === product.categoryId)?.name}</TableCell>
                        <TableCell sx={{textAlign:"center"}}>{product.description}</TableCell>
                    </TableRow>
                ))}
            </TableBody>
        </Table>
    )
}