import {useState, type ReactNode} from 'react'
import {AuthContext} from '../context/AuthContext'

export function AuthProvider({children}: {children: ReactNode}) {
    const [isAuthenticated, setIsAuthenticated] = useState(false);
    const [isLoading] = useState(false);

    const login = () => {
        setIsAuthenticated(true)
    }

    const logout = () => {
        setIsAuthenticated(false);
    }

    return (
    <AuthContext.Provider
        value={{
        isAuthenticated,
        isLoading,
        login,
        logout,
        }}
    >
        {children}
    </AuthContext.Provider>
    );
}