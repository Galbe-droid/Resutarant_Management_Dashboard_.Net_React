import i18n from "i18next";
import { initReactI18next } from "react-i18next";
import loginPt from "./locales/pt/pages/login.json";
import loginEn from "./locales/en/pages/login.json";
import tablePt from "./locales/pt/pages/table.json";
import tableEn from "./locales/en/pages/table.json";
import dashboardPt from "./locales/pt/common/dashboard.json";
import dashboardEn from "./locales/en/common/dashboard.json";
import topbarPt from "./locales/pt/common/topbar.json";
import topbarEn from "./locales/en/common/topbar.json";
import sidebarPt from "./locales/pt/common/sidebar.json";
import sidebarEn from "./locales/en/common/sidebar.json";
import productPt from "./locales/pt/pages/product.json"
import productEn from "./locales/en/pages/product.json"

i18n
    .use(initReactI18next)
    .init({
        resources: {
            pt: {
                login: loginPt,
                table: tablePt,
                dashboard: dashboardPt,
                topbar: topbarPt,
                sidebar: sidebarPt,
                product: productPt,
            },
            en: {
                login: loginEn,
                table: tableEn,
                dashboard: dashboardEn,
                topbar: topbarEn,
                sidebar: sidebarEn,
                product: productEn,
            }
        },

        lng: localStorage.getItem("language") || navigator.language.split("-")[0] || "pt",
        fallbackLng: "en",

        ns: ["login", "table", "dashboard", "topbar", "sidebar", "product"],
        defaultNS: "login",

        interpolation: {
            escapeValue: false,
        },
    });

export default i18n;