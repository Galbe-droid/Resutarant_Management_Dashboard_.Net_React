import {type ReactNode, useState} from "react";
import {AuthContext} from "../contexts/AuthContext.tsx";
import {loginAuth} from "../api/authApi.ts";
import type {LoginRequest} from "../types/LoginRequest.ts";
import type { User } from "../types/User.ts";

export function AuthProvider({ children }: { children: ReactNode }) {
    const [accessToken, setAccessToken] = useState<string | null>(null);
    const [userInfo, setUserInfo] = useState<User | null>(null);

    async function login(data: LoginRequest)
    {
        try {
            const response = await loginAuth(data);

            const accessToken = response.data.accessToken;

            const refreshToken = response.data.refreshToken;

            setAccessToken(accessToken);

            setUserInfo(userInfo);

            localStorage.setItem(
                "accessToken",
                accessToken
            );

            localStorage.setItem(
                "refreshToken",
                refreshToken
            );

            localStorage.setItem("userInfo", JSON.stringify(response.userInfo.data));

            return true;

        } catch (error) {

            console.error(error);

            return false;
        }
    }

    async function logout()
    {
        setAccessToken(null);

        localStorage.removeItem("accessToken");
        localStorage.removeItem("refreshToken");
    }

    return (
        <AuthContext.Provider value={{login, logout, accessToken, userInfo}}>
            {children}
        </AuthContext.Provider>
    )
}