import {AppBar, Stack, Toolbar, Typography} from "@mui/material";
import {useAuth} from "../hooks/useAuth.ts";

export function Topbar() {
    const auth = useAuth();

    return(
        <AppBar
            sx = {{
                flexShrink: 0,
                display: "flex",
                flexDirection: "row",
                paddingLeft: "2rem",
                paddingRight: "2rem",
                zIndex: (theme) => theme.zIndex.drawer + 1,
            }}
        >
            <Toolbar/>
            <Stack
                direction="row"
                spacing={2}
                sx = {{
                    width: "100%",
                    alignItems: "center",
                    justifyContent: "space-between",
                }}
            >
                <Typography>Bem Vindo! {auth.userInfo?.name}</Typography>

                <Typography>{auth.userInfo?.email}</Typography>

                <Typography>{auth.userInfo?.roles}</Typography>
            </Stack>
        </AppBar>
    )
}