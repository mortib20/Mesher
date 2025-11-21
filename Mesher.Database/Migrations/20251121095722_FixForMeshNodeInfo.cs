using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mesher.Database.Migrations
{
    /// <inheritdoc />
    public partial class FixForMeshNodeInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
CREATE OR REPLACE VIEW ""MeshNodeInfo"" AS
SELECT *
FROM (SELECT DISTINCT ON (""From"") ('0x' || replace(""RawMessage"" -> 'payload' ->> 'id', '!', ''))::bigint AS ""Id"",
                                  (""RawMessage"" -> 'payload' ->> 'longname')::text                       AS ""LongName"",
                                  (""RawMessage"" -> 'payload' ->> 'shortname')::text                      AS ""ShortName"",
                                  (""RawMessage"" -> 'payload' ->> 'role')::bigint                         AS ""Role"",
                                  ""MeshHardwares"".""Name""                                                 AS ""Hardware"",
                                  ""CreatedAt""                                                            AS ""LastSeen"",
                                  ""HopsAway""                                                             AS ""HopsAway""
      FROM ""MeshMessages""
               LEFT JOIN ""MeshHardwares""
                         ON (""RawMessage"" -> 'payload' ->> 'hardware')::bigint = ""MeshHardwares"".""Key""
      WHERE ""Type"" = 'nodeinfo'
      -- für DISTINCT ON MUSS das ORDER BY mit der DISTINCT-Spalte beginnen
      ORDER BY ""From"", ""CreatedAt"" DESC) AS sub
ORDER BY ""LastSeen"" DESC;");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
