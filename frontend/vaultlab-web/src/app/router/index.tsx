import { createBrowserRouter } from "react-router-dom";
import LoginPage from "../../features/Auth/pages/LoginPage";
import ProtectedRoute from "../../features/Auth/components/ProtectedRoute";
import HomePage from "../../pages/HomePage";

export const router = createBrowserRouter([
    {
        path: "/login",
        element: <LoginPage />,
    },
    {
        element: <ProtectedRoute />,
        children: [
            {
                path: "/",
                element: <HomePage />
            }
        ]        
    }
])