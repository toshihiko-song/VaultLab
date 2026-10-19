import { cn } from "@/lib/utils"

/** The VaultLab mark: a gallery wall label, the title line set heavier than the caption lines. */
export function VaultMark({ className }: { className?: string }) {
    return (
        <svg
            viewBox="0 0 32 32"
            fill="none"
            aria-hidden="true"
            className={cn("size-8 shrink-0", className)}
        >
            <rect x="2.5" y="5.5" width="27" height="21" rx="3" stroke="currentColor" strokeWidth="2" />
            <path d="M8 12h12" stroke="currentColor" strokeWidth="2.75" strokeLinecap="round" />
            <path d="M8 17h16M8 21h9" stroke="currentColor" strokeWidth="1.5" strokeLinecap="round" opacity=".7" />
        </svg>
    )
}

export function VaultWordmark({ className }: { className?: string }) {
    return (
        <span className={cn("flex items-center gap-2.5 text-xl font-semibold tracking-[-0.02em]", className)}>
            <VaultMark />
            VaultLab
        </span>
    )
}
