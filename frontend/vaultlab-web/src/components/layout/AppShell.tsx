import { useState, type ReactNode } from "react"
import { NavLink } from "react-router-dom"
import { LogOut, Menu } from "lucide-react"
import { VaultWordmark } from "@/components/brand/VaultMark"
import { Button } from "@/components/ui/button"
import { Sheet, SheetContent, SheetDescription, SheetTitle, SheetTrigger } from "@/components/ui/sheet"
import { useAuth } from "@/features/Auth/hooks/useAuth"
import { cn } from "@/lib/utils"
import { navItems } from "./nav"
import { ThemeToggle } from "./ThemeToggle"

function WallNav({ onNavigate }: { onNavigate?: () => void }) {
    return (
        <nav aria-label="Main" className="grid gap-1">
            {navItems.map(({ label, to, icon: Icon, soon }) =>
                soon ? (
                    <span
                        key={to}
                        aria-disabled="true"
                        className="flex h-10 items-center gap-3 rounded-lg px-3 text-sm text-wall-muted"
                    >
                        <Icon className="size-4" />
                        {label}
                        <span className="label-data ml-auto text-[10px] uppercase">Soon</span>
                    </span>
                ) : (
                    <NavLink
                        key={to}
                        to={to}
                        end
                        onClick={onNavigate}
                        className={({ isActive }) =>
                            cn(
                                "relative flex h-10 items-center gap-3 rounded-lg px-3 text-sm text-wall-muted transition-colors outline-none hover:bg-wall-raised/60 hover:text-wall-foreground focus-visible:ring-2 focus-visible:ring-signal",
                                isActive && "bg-wall-raised font-medium text-wall-foreground",
                            )
                        }
                    >
                        {({ isActive }) => (
                            <>
                                <Icon className={cn("size-4", isActive && "text-signal")} />
                                {label}
                            </>
                        )}
                    </NavLink>
                ),
            )}
        </nav>
    )
}

function WallFooter() {
    const { logout } = useAuth()

    return (
        <div className="grid gap-3 border-t border-wall-border pt-4">
            <ThemeToggle />
            <Button
                variant="ghost"
                onClick={logout}
                className="h-10 justify-start gap-3 px-3 text-wall-muted hover:bg-wall-raised/60 hover:text-wall-foreground focus-visible:ring-signal"
            >
                <LogOut />
                Sign out
            </Button>
        </div>
    )
}

/** The painted gallery wall holds navigation; the work happens on label stock beside it. */
export function AppShell({ children }: { children: ReactNode }) {
    const [menuOpen, setMenuOpen] = useState(false)

    return (
        <div className="min-h-dvh lg:grid lg:grid-cols-[264px_1fr] lg:bg-[linear-gradient(to_right,var(--wall)_264px,transparent_264px)]">
            <aside className="sticky top-0 hidden h-dvh flex-col gap-8 bg-wall px-5 py-7 text-wall-foreground lg:flex">
                <VaultWordmark className="px-1" />
                <WallNav />
                <div className="mt-auto">
                    <WallFooter />
                </div>
            </aside>

            <header className="sticky top-0 z-30 flex h-14 items-center justify-between bg-wall px-4 text-wall-foreground lg:hidden">
                <VaultWordmark className="text-lg [&_svg]:size-7" />
                <Sheet open={menuOpen} onOpenChange={setMenuOpen}>
                    <SheetTrigger asChild>
                        <Button
                            variant="ghost"
                            size="icon-lg"
                            aria-label="Open menu"
                            className="text-wall-foreground hover:bg-wall-raised hover:text-wall-foreground focus-visible:ring-signal"
                        >
                            <Menu />
                        </Button>
                    </SheetTrigger>
                    <SheetContent
                        side="left"
                        className="w-[min(18rem,85vw)] gap-8 border-wall-border bg-wall px-5 py-7 text-wall-foreground [&_[data-slot=sheet-close]]:text-wall-muted [&_[data-slot=sheet-close]]:hover:bg-wall-raised [&_[data-slot=sheet-close]]:hover:text-wall-foreground"
                    >
                        <SheetTitle className="sr-only">Menu</SheetTitle>
                        <SheetDescription className="sr-only">VaultLab navigation</SheetDescription>
                        <VaultWordmark className="px-1" />
                        <WallNav onNavigate={() => setMenuOpen(false)} />
                        <div className="mt-auto">
                            <WallFooter />
                        </div>
                    </SheetContent>
                </Sheet>
            </header>

            <main className="min-w-0">{children}</main>
        </div>
    )
}
