import {Drawer, ListItem, ListItemButton, ListItemText, Toolbar} from "@mui/material";
import {useTranslation} from "react-i18next";


export function Sidebar()
{
    const { t } = useTranslation("sidebar");
    const menuItems = [
        { text: "Dashboard", path: "/dashboard"},
        { text: t("products"), path: "/products" },
        { text: t("categories"), path: "/categories" },
        { text: t("orders"), path: "/orders" },
        { text: t("payments"), path: "/payments" },
        { text: t("users"), path: "/users" },
    ];

    return(
        <Drawer
            variant="permanent"
            sx={{
                flexShrink: 0,
                display: "flex",
                flexDirection: "column",
                "& .MuiDrawer-paper": {
                    width: 240,
                    bgcolor: "background.paper",
                    color: "text.primary",
                    boxSizing: "border-box",
                    borderRight: (theme) => `1px solid ${theme.palette.divider}`,
                },
            }}
        >
            <Toolbar/>
            {menuItems.map((item) => (

                <ListItem
                    key={item.path}
                    disablePadding
                >
                    <ListItemButton href={item.path}>
                        <ListItemText
                            sx={{
                                display: "flex",
                                alignItems: "center",
                                justifyContent: "center",
                            }}
                            primary={item.text}
                        />
                    </ListItemButton>
                </ListItem>

            ))}
        </Drawer>
    )
}