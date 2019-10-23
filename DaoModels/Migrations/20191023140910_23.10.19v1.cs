using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DaoModels.Migrations
{
    public partial class _231019v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LogTasks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogTasks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaskLoads",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NameMethod = table.Column<string>(nullable: true),
                    Array = table.Column<byte[]>(nullable: true),
                    LogTaskId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskLoads", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskLoads_LogTasks_LogTaskId",
                        column: x => x.LogTaskId,
                        principalTable: "LogTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaskLoads_LogTaskId",
                table: "TaskLoads",
                column: "LogTaskId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaskLoads");

            migrationBuilder.DropTable(
                name: "LogTasks");
        }
    }
}
