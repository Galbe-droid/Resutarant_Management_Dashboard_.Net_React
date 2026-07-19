import {type ReactNode} from "react";
import {Card, CardContent, Stack, Typography} from "@mui/material";

interface DashboardCardProps {
    title: string;
    value: string | null;
    icon: ReactNode;
}

export function DashboardCard({title, value, icon}: DashboardCardProps) {
    return (
       <Card
           sx={{
               minWidth: 240,
               borderRadius: 3,
           }}
       >
           <CardContent>
               <Stack
                    sx={{
                        display: "flex",
                        flexDirection: "row",
                        justifyContent: "space-between",
                        alignItems: "center"
                    }}
               >
                   <Stack>
                       <Typography sx={{variant: "body2", color: "text.secondary"}}>{title}</Typography>
                       <Typography sx={{variant: "h4", fontWeight: 700}}>{value}</Typography>
                   </Stack>
                   {icon}
               </Stack>
           </CardContent>
       </Card>
    )
}