import type {ReturnProductDto} from "../../types/products/ReturnProductDto.ts";
import {Table, TableBody, TableCell, TableHead, TableRow} from "@mui/material";
import {mapDecimals} from "../../mappers/mapDecimals.tsx";
import type {ReturnCategoryDto} from "../../types/categories/ReturnCategoryDto.ts";

interface DashboardProductTableProps {
    products: ReturnProductDto[] | null;
    categories: ReturnCategoryDto[];
}

export function DashboardProductTable({products, categories}: DashboardProductTableProps) {
    return (
        <Table>
            <TableHead>
                <TableRow>
                    <TableCell sx={{textAlign:"center"}}>Name</TableCell>
                    <TableCell sx={{textAlign:"center"}}>Price</TableCell>
                    <TableCell sx={{textAlign:"center"}}>Category</TableCell>
                    <TableCell sx={{textAlign:"center"}}>Description</TableCell>
                </TableRow>
            </TableHead>
            <TableBody>
                {products?.map((product) => (
                    <TableRow
                        key={product.id}
                        hover
                        sx={{
                            cursor: "pointer",
                            "&:hover": {
                                backgroundColor: "action.hover"
                            }
                        }}
                    >
                        <TableCell sx={{textAlign:"center"}}>{product.name}</TableCell>
                        <TableCell sx={{textAlign:"center"}}>{mapDecimals(product.price)}</TableCell>
                        <TableCell sx={{textAlign:"center"}}>{categories.find(category => category.id === product.categoryId)!.name}</TableCell>
                        <TableCell sx={{textAlign:"center"}}>{product.description}</TableCell>
                    </TableRow>
                ))}
            </TableBody>
        </Table>
    )
}