using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VaultLab.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveDomainDocumentChunkTable2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DocumentChunkModel_Documents_DocumentId",
                table: "DocumentChunkModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DocumentChunkModel",
                table: "DocumentChunkModel");

            migrationBuilder.RenameTable(
                name: "DocumentChunkModel",
                newName: "DocumentChunk");

            migrationBuilder.RenameIndex(
                name: "IX_DocumentChunkModel_DocumentId_ChunkIndex",
                table: "DocumentChunk",
                newName: "IX_DocumentChunk_DocumentId_ChunkIndex");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DocumentChunk",
                table: "DocumentChunk",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentChunk_Documents_DocumentId",
                table: "DocumentChunk",
                column: "DocumentId",
                principalTable: "Documents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DocumentChunk_Documents_DocumentId",
                table: "DocumentChunk");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DocumentChunk",
                table: "DocumentChunk");

            migrationBuilder.RenameTable(
                name: "DocumentChunk",
                newName: "DocumentChunkModel");

            migrationBuilder.RenameIndex(
                name: "IX_DocumentChunk_DocumentId_ChunkIndex",
                table: "DocumentChunkModel",
                newName: "IX_DocumentChunkModel_DocumentId_ChunkIndex");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DocumentChunkModel",
                table: "DocumentChunkModel",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentChunkModel_Documents_DocumentId",
                table: "DocumentChunkModel",
                column: "DocumentId",
                principalTable: "Documents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
