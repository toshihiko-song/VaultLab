/** Mirrors VaultLab.Domain.Enums.DocumentStatus as serialised by the API. */
export type DocumentStatus = "Uploaded" | "Processing" | "Processed" | "Failed"

/** Mirrors VaultLab.Application.DTOs.Documents.DocumentResponse. */
export interface VaultDocument {
    id: string
    userId: string
    fileName: string
    contentType: string
    fileSize: number
    status: DocumentStatus
    createdAt: string
}
