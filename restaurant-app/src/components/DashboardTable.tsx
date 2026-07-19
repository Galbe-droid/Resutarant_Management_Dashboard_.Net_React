import {Chip, Table, TableBody, TableCell, TableHead, TableRow} from "@mui/material";

const tables = [
    {
        id: 1,
        tableNumber: 1,
        status: "Livre",
        reserved: false,
        total: 0
    },
    {
        id: 2,
        tableNumber: 2,
        status: "Ocupada",
        reserved: false,
        total: 186.90
    },
    {
        id: 3,
        tableNumber: 3,
        status: "Reservada",
        reserved: true,
        total: 0
    },
    {
        id: 4,
        tableNumber: 4,
        status: "Ocupada",
        reserved: false,
        total: 94.50
    },
    {
        id: 5,
        tableNumber: 5,
        status: "Livre",
        reserved: false,
        total: 0
    },
    {
        id: 6,
        tableNumber: 6,
        status: "Ocupada",
        reserved: false,
        total: 312.40
    },
    {
        id: 7,
        tableNumber: 7,
        status: "Reservada",
        reserved: true,
        total: 0
    },
    {
        id: 8,
        tableNumber: 8,
        status: "Ocupada",
        reserved: false,
        total: 147.80
    }
];

export function DashboardTable() {
    const getStatusChip = (status: string) => {
        switch (status) {
            case "Livre":
                return <Chip label="Livre" color="success"/>;

            case "Ocupada":
                return <Chip label="Ocupada" color="error"/>;

            case "Reservada":
                return <Chip label="Rerservada" color="warning"/>;

            default:
                return <Chip label={status} />;
        }
    }


    return (
        <Table>
            <TableHead>
                <TableRow>
                    <TableCell sx={{textAlign:"center"}}>Mesa</TableCell>
                    <TableCell sx={{textAlign:"center"}}>Status das Mesas</TableCell>
                    <TableCell sx={{textAlign:"center"}}>Esta Resevado ?</TableCell>
                    <TableCell sx={{textAlign:"center"}}>Total Atual</TableCell>
                </TableRow>
            </TableHead>
            <TableBody>
                {tables.map((table) => (
                    <TableRow key={table.id}>
                        <TableCell sx={{textAlign:"center"}}>{table.tableNumber}</TableCell>
                        <TableCell sx={{textAlign:"center"}}>{getStatusChip(table.status)}</TableCell>
                        <TableCell sx={{textAlign:"center"}}>{table.reserved ? "Sim" : "Não"}</TableCell>
                        <TableCell sx={{textAlign:"center"}}>R$ {table.total.toFixed(2)}</TableCell>
                    </TableRow>
                ))}
            </TableBody>
        </Table>
    )
}