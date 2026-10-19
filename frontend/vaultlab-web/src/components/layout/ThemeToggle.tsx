import { Monitor, Moon, Sun } from "lucide-react"
import { useTheme } from "next-themes"
import { cn } from "@/lib/utils"

const options = [
    { value: "light", label: "Light", icon: Sun },
    { value: "dark", label: "Dark", icon: Moon },
    { value: "system", label: "System", icon: Monitor },
] as const

/** Three-way segmented switch, styled for the spruce wall. */
export function ThemeToggle({ className }: { className?: string }) {
    const { theme, setTheme } = useTheme()

    return (
        <div
            role="radiogroup"
            aria-label="Colour theme"
            className={cn("grid grid-cols-3 rounded-lg bg-black/15 p-1", className)}
        >
            {options.map(({ value, label, icon: Icon }) => {
                const active = theme === value
                return (
                    <button
                        key={value}
                        type="button"
                        role="radio"
                        aria-checked={active}
                        aria-label={label}
                        title={label}
                        onClick={() => setTheme(value)}
                        className={cn(
                            "flex h-7 items-center justify-center rounded-md text-wall-muted transition-colors outline-none hover:text-wall-foreground focus-visible:ring-2 focus-visible:ring-signal",
                            active && "bg-wall-raised text-wall-foreground shadow-[0_1px_2px_rgb(0_0_0/0.25)]",
                        )}
                    >
                        <Icon className="size-3.5" />
                    </button>
                )
            })}
        </div>
    )
}
