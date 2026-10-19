# Product

<!-- impeccable:product-schema 1 -->

## Platform

web

## Users

Small teams that keep shared knowledge in documents, such as policies, contracts, specs and notes. Team members upload files and ask questions in plain language instead of hunting through folders.

## Product Purpose

VaultLab is a document intelligence platform. Users upload documents, VaultLab extracts and indexes them, and users can then ask questions that are answered only from those documents, with the source passages returned alongside the answer. Success means a teammate gets a trustworthy answer and can see where it came from.

## Positioning

Answers are grounded strictly in the team's own uploaded documents. When the documents do not contain the answer, VaultLab says so instead of guessing, and every answer carries the source chunks it was built from.

## Operating Context

- Upload, then wait for processing: documents move through Uploaded → Processing → Processed (or Failed).
- Supported formats: PDF, DOCX, Excel, Markdown and plain text.
- Question answering: semantic search over chunk embeddings, then an LLM answer limited to the retrieved context, returned with its sources.

## Capabilities and Constraints

- Frontend: React 19 + Vite + React Router; the user has asked for Tailwind CSS and shadcn/ui.
- Backend: .NET API (`/api/documents`: upload, list per user, get by id, ask), processing over RabbitMQ, pgvector embeddings, OpenAI models.
- Auth: Google sign-in only. It is currently stubbed in the frontend and hard-coded to one user in the API.
- Current frontend scope: Login and Home only. The user is implementing features (documents library, upload, ask) themselves; design work should restyle the existing pages and provide the foundation, not build those features.

## Evidence on Hand

No customers, testimonials, usage numbers or security certifications exist. Do not fabricate any.

## Product Principles

1. Trust over cleverness: show sources, admit when there is no answer.
2. Team knowledge, not personal clutter: the vault is shared ground.
3. Get to the question fast: the answer is the product, chrome stays out of the way.
