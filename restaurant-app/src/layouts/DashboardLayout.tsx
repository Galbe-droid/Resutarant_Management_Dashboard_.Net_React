import {Topbar} from "../components/Topbar.tsx";
import {Sidebar} from "../components/Sidebar.tsx";
import {Box, Toolbar} from "@mui/material";
import {Outlet} from "react-router-dom";

export function DashboardLayout() {
    return (
        <Box sx={{display: "flex"}}>
            <Topbar/>
            <Sidebar/>
            <Box
             sx={{
                 flexGrow: 1,
                 p:3
             }}
            >
                <Toolbar/>
                <Outlet/>
            </Box>
        </Box>
    )
}