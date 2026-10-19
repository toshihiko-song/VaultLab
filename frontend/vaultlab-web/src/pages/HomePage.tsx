import { Upload } from "lucide-react"
import { AppShell } from "@/components/layout/AppShell"
import { Button } from "@/components/ui/button"
import { AccessionLabel } from "@/components/vault/AccessionLabel"
import { AskBar } from "@/components/vault/AskBar"
import { StatusStamp } from "@/components/vault/StatusStamp"
import type { DocumentStatus, VaultDocument } from "@/components/vault/document.types"
import { cn } from "@/lib/utils"

const exampleQuestions = [
    "What is our refund window?",
    "Who approves travel over $2,000?",
    "When does the Northwind contract renew?",
]

const lifecycle: { status: DocumentStatus; note: string }[] = [
    { status: "Uploaded", note: "Stored and queued" },
    { status: "Processing", note: "Text extracted and indexed" },
    { status: "Processed", note: "Ready to answer questions" },
]

// TODO: accession numbers must come from a stable backend sequence, not list position,
// so a label keeps its number through deletes and pagination.
const accession = (index: number) => `VL-${String(index).padStart(4, "0")}`

function formatSize(bytes: number) {
    if (bytes < 1024 * 1024) return `${Math.max(1, Math.round(bytes / 1024))} KB`
    return `${(bytes / (1024 * 1024)).toFixed(1)} MB`
}

function formatMedium(doc: VaultDocument) {
    const extension = doc.fileName.split(".").pop()?.toUpperCase() ?? "FILE"
    return `${extension} · ${formatSize(doc.fileSize)}`
}

const dateFormat = new Intl.DateTimeFormat(undefined, { month: "short", day: "numeric" })

export default function HomePage() {
    // TODO: load from GET /api/documents/users/{userId}. Newest first.
    const documents: VaultDocument[] = []

    // TODO: open a file picker and POST /api/documents.
    const handleUpload = () => {}

    // TODO: POST /api/documents/ask and show the answer with its sources.
    const handleAsk = (question: string) => {
        void question
    }

    const recent = documents.slice(0, 6)

    return (
        <AppShell>
            <div className="mx-auto grid w-full max-w-6xl gap-10 px-4 pt-8 pb-16 sm:px-8 sm:pt-12 lg:gap-12 lg:px-14 lg:pt-14">
                <section aria-labelledby="ask-heading" className="grid gap-7">
                    <div className="flex flex-wrap items-end justify-between gap-x-6 gap-y-4">
                        <div className="grid max-w-2xl gap-3">
                            <h1
                                id="ask-heading"
                                className="text-[clamp(2rem,1.4rem+2.4vw,2.75rem)] leading-[1.05] font-semibold tracking-[-0.035em]"
                            >
                                Ask the collection.
                            </h1>
                            <p className="max-w-[60ch] text-base text-pretty text-muted-foreground sm:text-[17px]">
                                Every answer is drawn from the documents catalogued in this vault, and points back to
                                the labels it came from.
                            </p>
                        </div>
                        <Button
                            variant="outline"
                            size="lg"
                            onClick={handleUpload}
                            className={cn("h-10 gap-2 bg-card px-4", recent.length === 0 && "hidden lg:inline-flex")}
                        >
                            <Upload />
                            Upload document
                        </Button>
                    </div>

                    <AskBar onAsk={handleAsk} examples={exampleQuestions} />
                </section>

                <section aria-labelledby="recent-heading" className="grid gap-5">
                    <div className="flex items-baseline justify-between gap-4 border-b pb-3">
                        <h2 id="recent-heading" className="text-base font-semibold">
                            Recently accessioned
                        </h2>
                        <span className="label-data text-xs text-muted-foreground">
                            {documents.length} {documents.length === 1 ? "document" : "documents"}
                        </span>
                    </div>

                    {recent.length > 0 ? (
                        <div className="grid gap-4 sm:grid-cols-2 xl:grid-cols-3">
                            {recent.map((doc, i) => (
                                <AccessionLabel
                                    key={doc.id}
                                    accession={accession(documents.length - i)}
                                    title={doc.fileName}
                                    medium={formatMedium(doc)}
                                    date={dateFormat.format(new Date(doc.createdAt))}
                                    status={doc.status}
                                />
                            ))}
                        </div>
                    ) : (
                        <div className="grid gap-8">
                            <div className="grid gap-4">
                                <h3 className="text-xl font-semibold tracking-[-0.02em]">Nothing is on the wall yet</h3>
                                <p className="max-w-[52ch] text-pretty text-muted-foreground">
                                    Upload a policy, contract or spec. Each one is labelled and catalogued here, then
                                    becomes searchable once processing finishes. PDF, Word, Excel, Markdown and plain
                                    text are supported.
                                </p>
                                <div>
                                    <Button size="lg" onClick={handleUpload} className="h-10 gap-2 px-4 font-semibold">
                                        <Upload />
                                        Upload your first document
                                    </Button>
                                </div>
                            </div>
                            <div className="grid gap-4 sm:grid-cols-2 xl:grid-cols-3">
                                <AccessionLabel
                                    ghost
                                    accession={accession(1)}
                                    title="Your first document"
                                    medium="PDF · DOCX · XLSX · MD · TXT"
                                />
                                {/* Empty hanging slots: the grid real labels will fill. */}
                                <div aria-hidden="true" className="hidden rounded-sm border border-dashed border-border sm:block" />
                                <div aria-hidden="true" className="hidden rounded-sm border border-dashed border-border xl:block" />
                            </div>
                        </div>
                    )}
                </section>

                <section aria-labelledby="lifecycle-heading" className="grid gap-4">
                    <h2 id="lifecycle-heading" className="text-base font-semibold">
                        How a document enters the vault
                    </h2>
                    <ol className="grid overflow-hidden rounded-lg border bg-card sm:grid-cols-3">
                        {lifecycle.map(({ status, note }, i) => (
                            <li
                                key={status}
                                className="flex items-center justify-between gap-4 border-t px-5 py-4 first:border-t-0 sm:border-t-0 sm:border-l sm:first:border-l-0"
                            >
                                <span className="text-sm text-muted-foreground">
                                    <span className="label-data mr-2 text-xs">{i + 1}</span>
                                    {note}
                                </span>
                                <StatusStamp status={status} className="min-w-0" />
                            </li>
                        ))}
                    </ol>
                    <p className="text-sm text-muted-foreground">
                        If a file can't be read it is marked{" "}
                        <StatusStamp status="Failed" className="min-w-0 align-[-1px]" /> and stays in the list, so
                        nothing goes missing quietly.
                    </p>
                </section>
            </div>
        </AppShell>
    )
}
