import {AppBar, IconButton, Menu, MenuItem, Stack, Toolbar, Tooltip, Typography} from "@mui/material";
import {useAuth} from "../../hooks/useAuth.ts";
import {useTranslation} from "react-i18next";
import { Language } from "@mui/icons-material";
import { useState } from "react";

export function Topbar() {
    const auth = useAuth();
    const { i18n } = useTranslation();
    const { t } = useTranslation("topbar");
    const [anchorEl, setAnchorEl] = useState<null | HTMLElement>(null);

    const handleOpen = (event: React.MouseEvent<HTMLElement>) => {
        setAnchorEl(event.currentTarget);
    };

    const handleClose = () => {
        setAnchorEl(null);
    };

    const changeLanguage = (language: string) => {
        i18n.changeLanguage(language);
        localStorage.setItem("language", language);
        handleClose();
    };

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
                <Typography>{t("welcome")} {auth.userInfo?.name}</Typography>

                <Typography>{auth.userInfo?.email}</Typography>

                <Typography>{auth.userInfo?.roles}</Typography>

                <Tooltip title="Language">
                    <IconButton color="inherit" onClick={handleOpen}>
                        <Language />
                    </IconButton>
                </Tooltip>

                <Menu
                    anchorEl={anchorEl}
                    open={Boolean(anchorEl)}
                    onClose={handleClose}
                >
                    <MenuItem onClick={() => changeLanguage("pt")}>
                        🇧🇷 Português
                    </MenuItem>

                    <MenuItem onClick={() => changeLanguage("en")}>
                        🇺🇸 English
                    </MenuItem>
                </Menu>
            </Stack>
        </AppBar>
    )
}