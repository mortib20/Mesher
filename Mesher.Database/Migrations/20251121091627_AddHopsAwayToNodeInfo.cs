using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mesher.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddHopsAwayToNodeInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "HopsAway",
                table: "MeshMessages",
                type: "bigint",
                nullable: true,
                computedColumnSql: "(CAST(\"RawMessage\" ->> 'hops_away' AS bigint))",
                stored: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HopsAway",
                table: "MeshMessages");
        }
    }
}
