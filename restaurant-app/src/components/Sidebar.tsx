import {Drawer, ListItem, ListItemButton, ListItemText, Toolbar} from "@mui/material";


export function Sidebar()
{
    const menuItems = [
        { text: "Dashboard", path: "/dashboard"},
        { text: "Produtos", path: "/products" },
        { text: "Categorias", path: "/categories" },
        { text: "Mesas", path: "/tables" },
        { text: "Pedidos", path: "/orders" },
        { text: "Pagamentos", path: "/payments" },
        { text: "Usuários", path: "/users" },
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
                    <ListItemButton>
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