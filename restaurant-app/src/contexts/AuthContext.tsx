import type {LoginRequest} from "../types/LoginRequest.ts";
import {createContext} from "react";
import type {User} from "../types/User.ts";

interface AuthContextType {
    userInfo: User | null;
    accessToken: string | null;
    //refreshToken: string | null;
    login: (data: LoginRequest) => Promise<boolean>;
    logout: () => Promise<void>;
    //hasRole: (role: string) => Promise<boolean>;
}

export const AuthContext = createContext<AuthContextType>(
    {} as AuthContextType
);