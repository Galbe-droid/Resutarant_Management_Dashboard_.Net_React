import i18n from "i18next";
import { initReactI18next } from "react-i18next";

import loginPt from "./locales/pt/login.json";
import loginEn from "./locales/en/login.json";
import tablePt from "./locales/pt/table.json";
import tableEn from "./locales/en/table.json";
import dashboardPt from "./locales/pt/dashboard.json";
import dashboardEn from "./locales/en/dashboard.json";
import topbarPt from "./locales/pt/topbar.json";
import topbarEn from "./locales/en/topbar.json";
import sidebarPt from "./locales/pt/sidebar.json";
import sidebarEn from "./locales/en/sidebar.json";

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
            },
            en: {
                login: loginEn,
                table: tableEn,
                dashboard: dashboardEn,
                topbar: topbarEn,
                sidebar: sidebarEn,
            }
        },

        lng: "pt",
        fallbackLng: "en",

        ns: ["login", "table", "dashboard", "topbar", "sidebar"],
        defaultNS: "login",

        interpolation: {
            escapeValue: false,
        },
    });

export default i18n;