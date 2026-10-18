import { useEffect } from "react";
import { useLocation, useNavigate } from "react-router-dom";
import { useAuth } from "../hooks/useAuth";

export default function LoginPage() {
    const {isAuthenticated, login} = useAuth();
    const navigate = useNavigate();
    const location = useLocation();

    const from = location.state?.from?.pathname || "/";

    if(isAuthenticated) {
        return <NavigateToHome />
    } 

    return (
        <main>
            <h1>Welcome to VaultLab</h1>
            <p>Sign in to access your documents and AI-powered search</p>

            <button
                onClick={() => {
                    login();
                    navigate(from, {replace: true})
                }}
            >
                Continue with Google
            </button>

        </main>

    )
}

function NavigateToHome() {
    const navigate = useNavigate();

    useEffect(() => {
        navigate("/", {replace: true})
    }, [navigate])

    return null;
}