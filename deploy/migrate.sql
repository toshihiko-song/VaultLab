CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260929044719_InitialCreate') THEN
    CREATE TABLE "Users" (
        "Id" uuid NOT NULL,
        "Name" character varying(200) NOT NULL,
        "Email" text NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        CONSTRAINT "PK_Users" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260929044719_InitialCreate') THEN
    CREATE TABLE "Documents" (
        "Id" uuid NOT NULL,
        "UserId" uuid NOT NULL,
        "FileName" character varying(200) NOT NULL,
        "ContentType" character varying(100) NOT NULL,
        "FileSize" bigint NOT NULL,
        "StoragePath" character varying(1000) NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "Status" integer NOT NULL,
        CONSTRAINT "PK_Documents" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_Documents_Users_UserId" FOREIGN KEY ("UserId") REFERENCES "Users" ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260929044719_InitialCreate') THEN
    CREATE TABLE "DocumentChunks" (
        "Id" uuid NOT NULL,
        "DocumentId" uuid NOT NULL,
        "Content" text NOT NULL,
        "ChunkIndex" integer NOT NULL,
        CONSTRAINT "PK_DocumentChunks" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_DocumentChunks_Documents_DocumentId" FOREIGN KEY ("DocumentId") REFERENCES "Documents" ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260929044719_InitialCreate') THEN
    CREATE UNIQUE INDEX "IX_DocumentChunks_DocumentId_ChunkIndex" ON "DocumentChunks" ("DocumentId", "ChunkIndex");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260929044719_InitialCreate') THEN
    CREATE INDEX "IX_Documents_UserId" ON "Documents" ("UserId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260929044719_InitialCreate') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20260929044719_InitialCreate', '10.0.12');
    END IF;
END $EF$;
COMMIT;

START TRANSACTION;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20261005065922_AddDocumentChunkEmbeddings') THEN
    CREATE EXTENSION IF NOT EXISTS vector;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20261005065922_AddDocumentChunkEmbeddings') THEN
    ALTER TABLE "DocumentChunks" ADD "Embedding" vector(1536);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20261005065922_AddDocumentChunkEmbeddings') THEN
    CREATE TABLE "DocumentChunk" (
        "Id" uuid NOT NULL,
        "DocumentId" uuid NOT NULL,
        "Content" text NOT NULL,
        "ChunkIndex" integer NOT NULL,
        CONSTRAINT "PK_DocumentChunk" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_DocumentChunk_Documents_DocumentId" FOREIGN KEY ("DocumentId") REFERENCES "Documents" ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20261005065922_AddDocumentChunkEmbeddings') THEN
    CREATE UNIQUE INDEX "IX_DocumentChunk_DocumentId_ChunkIndex" ON "DocumentChunk" ("DocumentId", "ChunkIndex");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20261005065922_AddDocumentChunkEmbeddings') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20261005065922_AddDocumentChunkEmbeddings', '10.0.12');
    END IF;
END $EF$;
COMMIT;

START TRANSACTION;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20261005082113_RemoveDomainDocumentChunkTable') THEN
    ALTER TABLE "DocumentChunk" DROP CONSTRAINT "FK_DocumentChunk_Documents_DocumentId";
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20261005082113_RemoveDomainDocumentChunkTable') THEN
    ALTER TABLE "DocumentChunk" DROP CONSTRAINT "PK_DocumentChunk";
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20261005082113_RemoveDomainDocumentChunkTable') THEN
    ALTER TABLE "DocumentChunk" RENAME TO "DocumentChunkModel";
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20261005082113_RemoveDomainDocumentChunkTable') THEN
    ALTER INDEX "IX_DocumentChunk_DocumentId_ChunkIndex" RENAME TO "IX_DocumentChunkModel_DocumentId_ChunkIndex";
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20261005082113_RemoveDomainDocumentChunkTable') THEN
    ALTER TABLE "DocumentChunkModel" ADD CONSTRAINT "PK_DocumentChunkModel" PRIMARY KEY ("Id");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20261005082113_RemoveDomainDocumentChunkTable') THEN
    ALTER TABLE "DocumentChunkModel" ADD CONSTRAINT "FK_DocumentChunkModel_Documents_DocumentId" FOREIGN KEY ("DocumentId") REFERENCES "Documents" ("Id") ON DELETE CASCADE;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20261005082113_RemoveDomainDocumentChunkTable') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20261005082113_RemoveDomainDocumentChunkTable', '10.0.12');
    END IF;
END $EF$;
COMMIT;

START TRANSACTION;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20261005082620_RemoveDomainDocumentChunkTable2') THEN
    ALTER TABLE "DocumentChunkModel" DROP CONSTRAINT "FK_DocumentChunkModel_Documents_DocumentId";
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20261005082620_RemoveDomainDocumentChunkTable2') THEN
    ALTER TABLE "DocumentChunkModel" DROP CONSTRAINT "PK_DocumentChunkModel";
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20261005082620_RemoveDomainDocumentChunkTable2') THEN
    ALTER TABLE "DocumentChunkModel" RENAME TO "DocumentChunk";
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20261005082620_RemoveDomainDocumentChunkTable2') THEN
    ALTER INDEX "IX_DocumentChunkModel_DocumentId_ChunkIndex" RENAME TO "IX_DocumentChunk_DocumentId_ChunkIndex";
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20261005082620_RemoveDomainDocumentChunkTable2') THEN
    ALTER TABLE "DocumentChunk" ADD CONSTRAINT "PK_DocumentChunk" PRIMARY KEY ("Id");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20261005082620_RemoveDomainDocumentChunkTable2') THEN
    ALTER TABLE "DocumentChunk" ADD CONSTRAINT "FK_DocumentChunk_Documents_DocumentId" FOREIGN KEY ("DocumentId") REFERENCES "Documents" ("Id") ON DELETE CASCADE;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20261005082620_RemoveDomainDocumentChunkTable2') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20261005082620_RemoveDomainDocumentChunkTable2', '10.0.12');
    END IF;
END $EF$;
COMMIT;

