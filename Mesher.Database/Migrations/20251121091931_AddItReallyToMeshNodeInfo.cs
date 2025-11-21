using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mesher.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddItReallyToMeshNodeInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
CREATE OR REPLACE VIEW ""MeshNodeInfo"" AS
SELECT DISTINCT ON (""From"", ""HopsAway"", ""CreatedAt"") ('0x' || replace(""RawMessage"" -> 'payload' ->> 'id', '!', ''))::bigint AS ""Id"",
                                         (""RawMessage"" -> 'payload' ->> 'longname')::text                       AS ""LongName"",
                                         (""RawMessage"" -> 'payload' ->> 'shortname')::text                      AS ""ShortName"",
                                         (""RawMessage"" -> 'payload' ->> 'role')::bigint                         AS ""Role"",
                                         ""MeshHardwares"".""Name""                                                 AS ""Hardware"",
                                         ""CreatedAt""                                                            AS ""LastSeen"",
                                         ""HopsAway""
FROM ""MeshMessages""
         LEFT JOIN ""MeshHardwares"" ON (""RawMessage"" -> 'payload' ->> 'hardware')::bigint = ""MeshHardwares"".""Key""
WHERE ""Type"" = 'nodeinfo'
ORDER BY ""CreatedAt"" DESC;");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
