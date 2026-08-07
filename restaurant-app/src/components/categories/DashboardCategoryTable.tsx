import {Table, TableBody, TableCell, TableHead, TableRow} from "@mui/material";
import type {ReturnCategoryDashboardDto} from "../../types/categories/ReturnCategoryDashboardDto.ts";
import {useNavigate} from "react-router-dom";

interface DashboardCategoryTableProps{
    categories: ReturnCategoryDashboardDto[];
}

export function DashboardCategoryTable({categories}: DashboardCategoryTableProps){
    const navigate = useNavigate();
    return (
        <Table>
            <TableHead>
                <TableRow>
                    <TableCell sx={{textAlign:"center"}}>Nome</TableCell>
                    <TableCell sx={{textAlign:"center"}}>Qty de Produtos</TableCell>
                </TableRow>
            </TableHead>
            <TableBody>
                {categories?.map((category) => (
                    <TableRow
                        key={category.id}
                        hover
                        onClick={() => navigate(`/categories/${category.id}`)}
                        sx={{
                            cursor: "pointer",
                            "&:hover": {
                                backgroundColor: "action.hover"
                            }
                        }}
                    >
                        <TableCell sx={{textAlign:"center"}}>{category.name}</TableCell>
                        <TableCell sx={{textAlign:"center"}}>{category.quantity}</TableCell>
                    </TableRow>
                ))}
            </TableBody>
        </Table>
    )
}