import { Navigate, Outlet, useLocation } from "react-router-dom";
import { VaultMark } from "@/components/brand/VaultMark";
import { useAuth } from "../hooks/useAuth";

export default function ProtectedRoute() {
    const {isAuthenticated, isLoading} = useAuth();
    const location = useLocation();

    if(isLoading) {
        return (
            <div role="status" className="grid min-h-dvh place-items-center bg-wall text-wall-foreground">
                <div className="grid justify-items-center gap-4">
                    <VaultMark className="size-10 animate-pulse text-wall-foreground motion-reduce:animate-none" />
                    <span className="label-data text-xs uppercase text-wall-muted">Opening the vault…</span>
                </div>
            </div>
        )
    }

    if(!isAuthenticated) {
        return (
            <Navigate
                to="/login"
                state={{from: location}}
                replace
            />
        )
    }
    

    return <Outlet />
}