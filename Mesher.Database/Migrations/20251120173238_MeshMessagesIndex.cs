using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Mesher.Database.Migrations
{
    /// <inheritdoc />
    public partial class MeshMessagesIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Channel",
                table: "MeshMessages",
                type: "bigint",
                nullable: false,
                computedColumnSql: "(CAST(\"RawMessage\" ->> 'channel' AS bigint))",
                stored: true);

            migrationBuilder.AddColumn<long>(
                name: "From",
                table: "MeshMessages",
                type: "bigint",
                nullable: false,
                computedColumnSql: "(CAST(\"RawMessage\" ->> 'from' AS bigint))",
                stored: true);

            migrationBuilder.AddColumn<double>(
                name: "RSSI",
                table: "MeshMessages",
                type: "double precision",
                nullable: false,
                computedColumnSql: "(CAST(\"RawMessage\" ->> 'rssi' AS double precision))",
                stored: true);

            migrationBuilder.AddColumn<double>(
                name: "SNR",
                table: "MeshMessages",
                type: "double precision",
                nullable: false,
                computedColumnSql: "(CAST(\"RawMessage\" ->> 'snr' AS double precision))",
                stored: true);

            migrationBuilder.AddColumn<long>(
                name: "To",
                table: "MeshMessages",
                type: "bigint",
                nullable: false,
                computedColumnSql: "(CAST(\"RawMessage\" ->> 'to' AS bigint))",
                stored: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "MeshMessages",
                type: "text",
                nullable: true,
                computedColumnSql: "(CAST(\"RawMessage\" ->> 'type' AS text))",
                stored: true);

            migrationBuilder.CreateIndex(
                name: "IX_MeshMessages_CreatedAt_DESC",
                table: "MeshMessages",
                column: "CreatedAt")
                .Annotation("Npgsql:IndexSortOrder", new[] { SortOrder.Descending });

            migrationBuilder.CreateIndex(
                name: "IX_MeshMessages_From",
                table: "MeshMessages",
                column: "From");

            migrationBuilder.CreateIndex(
                name: "IX_MeshMessages_To",
                table: "MeshMessages",
                column: "To");

            migrationBuilder.CreateIndex(
                name: "IX_MeshMessages_Type",
                table: "MeshMessages",
                column: "Type");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MeshMessages_CreatedAt_DESC",
                table: "MeshMessages");

            migrationBuilder.DropIndex(
                name: "IX_MeshMessages_From",
                table: "MeshMessages");

            migrationBuilder.DropIndex(
                name: "IX_MeshMessages_To",
                table: "MeshMessages");

            migrationBuilder.DropIndex(
                name: "IX_MeshMessages_Type",
                table: "MeshMessages");

            migrationBuilder.DropColumn(
                name: "Channel",
                table: "MeshMessages");

            migrationBuilder.DropColumn(
                name: "From",
                table: "MeshMessages");

            migrationBuilder.DropColumn(
                name: "RSSI",
                table: "MeshMessages");

            migrationBuilder.DropColumn(
                name: "SNR",
                table: "MeshMessages");

            migrationBuilder.DropColumn(
                name: "To",
                table: "MeshMessages");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "MeshMessages");
        }
    }
}
