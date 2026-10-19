import { cn } from "@/lib/utils"
import type { DocumentStatus } from "./document.types"

const tone: Record<DocumentStatus, string> = {
    Uploaded: "text-status-uploaded",
    Processing: "text-status-processing",
    Processed: "text-status-processed",
    Failed: "text-status-failed",
}

/**
 * A status flips in place: fixed width so a label never reflows when its document
 * moves Uploaded → Processing → Processed. Failed is stamped, never hidden.
 */
export function StatusStamp({ status, className }: { status: DocumentStatus; className?: string }) {
    return (
        <span
            className={cn(
                "label-data inline-flex min-w-[10.5ch] items-center justify-end gap-1.5 text-[11px] font-medium uppercase",
                tone[status],
                className,
            )}
        >
            <span
                aria-hidden="true"
                className={cn(
                    "size-1.5 rounded-full bg-current",
                    status === "Processing" && "animate-pulse motion-reduce:animate-none",
                )}
            />
            {status}
        </span>
    )
}
