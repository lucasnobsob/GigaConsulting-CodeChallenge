using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GigaConsulting.Infra.Data.Migrations.EventStoreSql
{
    /// <inheritdoc />
    public partial class InitialEventStore : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "StoredEvent",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Data = table.Column<string>(type: "longtext", nullable: false),
                    User = table.Column<string>(type: "longtext", nullable: false),
                    Action = table.Column<string>(type: "varchar(100)", nullable: false),
                    AggregateId = table.Column<Guid>(type: "char(36)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoredEvent", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StoredEvent");
        }
    }
}
