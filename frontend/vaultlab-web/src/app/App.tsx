import { RouterProvider } from "react-router-dom"
import { ThemeProvider } from "next-themes"
import { AuthProvider } from "../features/Auth/providers/AuthProvider"
import { router } from "./router"

function App() {

  return (
    <ThemeProvider attribute="class" defaultTheme="light" enableSystem disableTransitionOnChange>
      <AuthProvider>
        <RouterProvider router={router} />
      </AuthProvider>
    </ThemeProvider>
  )
}

export default App
