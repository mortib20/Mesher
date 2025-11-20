using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mesher.Database.Migrations
{
    /// <inheritdoc />
    public partial class FixForNullOnSomeFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "SNR",
                table: "MeshMessages",
                type: "double precision",
                nullable: true,
                computedColumnSql: "(CAST(\"RawMessage\" ->> 'snr' AS double precision))",
                stored: true,
                oldClrType: typeof(double),
                oldType: "double precision",
                oldComputedColumnSql: "(CAST(\"RawMessage\" ->> 'snr' AS double precision))",
                oldStored: true);

            migrationBuilder.AlterColumn<double>(
                name: "RSSI",
                table: "MeshMessages",
                type: "double precision",
                nullable: true,
                computedColumnSql: "(CAST(\"RawMessage\" ->> 'rssi' AS double precision))",
                stored: true,
                oldClrType: typeof(double),
                oldType: "double precision",
                oldComputedColumnSql: "(CAST(\"RawMessage\" ->> 'rssi' AS double precision))",
                oldStored: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "SNR",
                table: "MeshMessages",
                type: "double precision",
                nullable: false,
                computedColumnSql: "(CAST(\"RawMessage\" ->> 'snr' AS double precision))",
                stored: true,
                oldClrType: typeof(double),
                oldType: "double precision",
                oldNullable: true,
                oldComputedColumnSql: "(CAST(\"RawMessage\" ->> 'snr' AS double precision))",
                oldStored: true);

            migrationBuilder.AlterColumn<double>(
                name: "RSSI",
                table: "MeshMessages",
                type: "double precision",
                nullable: false,
                computedColumnSql: "(CAST(\"RawMessage\" ->> 'rssi' AS double precision))",
                stored: true,
                oldClrType: typeof(double),
                oldType: "double precision",
                oldNullable: true,
                oldComputedColumnSql: "(CAST(\"RawMessage\" ->> 'rssi' AS double precision))",
                oldStored: true);
        }
    }
}
