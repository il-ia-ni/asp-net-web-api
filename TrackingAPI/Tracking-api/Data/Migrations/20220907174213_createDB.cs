using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tracking_api.Data.Migrations
// Migration is used to generate a local SQL DB on the server accesable over the connection str in the appsettings
// Migrations allow updating a DB with the latest changes in a model, reflecting them in the DB
// Migrations cls are created in the Nuget package manager console using command "Add-Migration mname -o /path_in_proj"

/* Now command Update-Database can be used to create a EntityFramework DB based on the IssueDb model on the server with the params
in the UP-method of this module (automatically supplied by IssueDbContext when creating the createDB Miration): */
{
    public partial class createDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Issues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    IssueType = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Completed = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Issues", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Issues");
        }
    }
}
