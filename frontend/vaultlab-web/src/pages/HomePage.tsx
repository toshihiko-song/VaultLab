import { useAuth } from "../features/Auth/hooks/useAuth";

export default function HomePage() {
    const {logout} = useAuth();
    
    return (
        <main>
            <h1>VaultLab</h1>
            <p>Welcome to your document Intelligence platform</p>

            <button
                onClick={logout}
            >
                Logout
            </button>
        </main>
        
    )
}