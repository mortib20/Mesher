using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mesher.Database.Migrations
{
    /// <inheritdoc />
    public partial class added_entry_id_to_MeshPosition_View : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
CREATE OR REPLACE VIEW ""MeshPosition"" AS
SELECT ""From"",
       (""RawMessage"" -> 'payload' -> 'latitude_i')::double precision / 10000000  AS ""Latitude"",
       (""RawMessage"" -> 'payload' -> 'longitude_i')::double precision / 10000000 AS ""Longitude"",
       (""RawMessage"" -> 'payload' -> 'altitude')::bigint                         AS ""Altitude"",
       (""RawMessage"" -> 'payload' -> 'precision_bits')::bigint                   AS ""PrecisionBits"",
       to_timestamp((""RawMessage"" -> 'payload' -> 'time')::bigint)               AS ""Time"",
       ""CreatedAt"",
       ""HopsAway"",
       ""EntryId""
FROM ""MeshMessages""
WHERE ""Type"" = 'position';");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
