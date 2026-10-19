import { cn } from "@/lib/utils"
import { StatusStamp } from "./StatusStamp"
import type { DocumentStatus } from "./document.types"

export interface AccessionLabelProps {
    /** Catalogue number, e.g. "VL-0142". */
    accession: string
    title: string
    /** Format and size, e.g. "PDF · 2.4 MB". */
    medium: string
    /** Display date, e.g. "Oct 14". */
    date?: string
    status?: DocumentStatus
    /** A dashed placeholder label, used to show the format before anything exists. */
    ghost?: boolean
    className?: string
}

/** Every document in the vault is hung with one of these: accession, title, medium, state. */
export function AccessionLabel({ accession, title, medium, date, status, ghost, className }: AccessionLabelProps) {
    return (
        <article
            className={cn(
                "grid gap-3 rounded-sm border bg-label px-5 py-4 transition-[border-color,box-shadow] duration-200",
                ghost
                    ? "border-dashed border-input bg-transparent"
                    : "shadow-[0_1px_0_var(--border)] hover:border-input hover:shadow-[0_6px_16px_-10px_rgb(20_23_20/0.35)]",
                className,
            )}
        >
            <div className="flex items-center justify-between gap-3">
                <span className="label-data text-xs text-muted-foreground">{accession}</span>
                {status && <StatusStamp status={status} />}
            </div>
            <h3
                className={cn(
                    "line-clamp-2 text-[17px] leading-snug font-semibold tracking-[-0.01em]",
                    ghost && "text-muted-foreground",
                )}
            >
                {title}
            </h3>
            <div className="label-data flex items-center justify-between gap-3 border-t pt-3 text-xs text-muted-foreground">
                <span className="truncate">{medium}</span>
                {date && <span className="shrink-0">{date}</span>}
            </div>
        </article>
    )
}
