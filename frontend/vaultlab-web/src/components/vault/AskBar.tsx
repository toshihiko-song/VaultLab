import { useId, useRef, useState, type FormEvent } from "react"
import { ArrowRight } from "lucide-react"
import { Button } from "@/components/ui/button"
import { cn } from "@/lib/utils"

interface AskBarProps {
    /** Called with the trimmed question. Wire this to POST /api/documents/ask. */
    onAsk?: (question: string) => void
    examples?: string[]
    className?: string
}

/** The front door of the vault. Lime marks the path to an answer and nothing else. */
export function AskBar({ onAsk, examples = [], className }: AskBarProps) {
    const [question, setQuestion] = useState("")
    const inputId = useId()
    const inputRef = useRef<HTMLInputElement>(null)
    const trimmed = question.trim()

    function handleSubmit(event: FormEvent) {
        event.preventDefault()
        if (!trimmed) {
            inputRef.current?.focus()
            return
        }
        onAsk?.(trimmed)
    }

    return (
        <div className={cn("grid gap-4", className)}>
            <form
                onSubmit={handleSubmit}
                className="group flex items-center gap-2 rounded-xl border bg-card p-2 pl-5 shadow-[0_1px_0_var(--border),0_12px_32px_-24px_rgb(20_23_20/0.4)] transition-[border-color,box-shadow] duration-200 focus-within:border-ring focus-within:shadow-[0_0_0_4px_color-mix(in_oklab,var(--signal)_45%,transparent)] sm:gap-4 sm:p-3 sm:pl-6"
            >
                <label htmlFor={inputId} className="sr-only">
                    Ask a question about your team's documents
                </label>
                <input
                    ref={inputRef}
                    id={inputId}
                    value={question}
                    onChange={(e) => setQuestion(e.target.value)}
                    placeholder="Ask your vault anything…"
                    autoComplete="off"
                    className="h-11 min-w-0 flex-1 bg-transparent text-base outline-none placeholder:text-muted-foreground sm:h-12 sm:text-xl"
                />
                <Button
                    type="submit"
                    className="h-11 gap-2 rounded-lg px-4 text-[15px] font-semibold shadow-[inset_0_-1px_0_rgb(0_0_0/0.12)] hover:bg-primary hover:brightness-95 sm:h-12 sm:px-5"
                >
                    Ask
                    <ArrowRight className="size-4" />
                </Button>
            </form>

            {examples.length > 0 && (
                <div className="flex flex-wrap items-center gap-2 text-sm text-muted-foreground">
                    <span>Try</span>
                    {examples.map((example) => (
                        <button
                            key={example}
                            type="button"
                            onClick={() => setQuestion(example)}
                            className="rounded-md border border-transparent bg-muted px-2.5 py-1 text-foreground/80 transition-colors outline-none hover:border-input hover:text-foreground focus-visible:ring-2 focus-visible:ring-ring"
                        >
                            {example}
                        </button>
                    ))}
                </div>
            )}
        </div>
    )
}
