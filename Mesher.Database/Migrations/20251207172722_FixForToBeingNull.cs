using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mesher.Database.Migrations
{
    /// <inheritdoc />
    public partial class FixForToBeingNull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "To",
                table: "MeshMessages",
                type: "bigint",
                nullable: true,
                computedColumnSql: "(CAST(\"RawMessage\" ->> 'to' AS bigint))",
                stored: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComputedColumnSql: "(CAST(\"RawMessage\" ->> 'to' AS bigint))",
                oldStored: true);

            migrationBuilder.AlterColumn<long>(
                name: "From",
                table: "MeshMessages",
                type: "bigint",
                nullable: true,
                computedColumnSql: "(CAST(\"RawMessage\" ->> 'from' AS bigint))",
                stored: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComputedColumnSql: "(CAST(\"RawMessage\" ->> 'from' AS bigint))",
                oldStored: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "To",
                table: "MeshMessages",
                type: "bigint",
                nullable: false,
                computedColumnSql: "(CAST(\"RawMessage\" ->> 'to' AS bigint))",
                stored: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComputedColumnSql: "(CAST(\"RawMessage\" ->> 'to' AS bigint))",
                oldStored: true);

            migrationBuilder.AlterColumn<long>(
                name: "From",
                table: "MeshMessages",
                type: "bigint",
                nullable: false,
                computedColumnSql: "(CAST(\"RawMessage\" ->> 'from' AS bigint))",
                stored: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComputedColumnSql: "(CAST(\"RawMessage\" ->> 'from' AS bigint))",
                oldStored: true);
        }
    }
}
