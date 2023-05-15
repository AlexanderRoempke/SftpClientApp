using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SftpClientApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Certificates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CertificateData = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Certificates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FileFilters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pattern = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileFilters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LogLevels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogLevels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SftpConfigurations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Workstationname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Host = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Port = table.Column<int>(type: "int", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CertificateId = table.Column<int>(type: "int", nullable: true),
                    IntervalInMinutes = table.Column<int>(type: "int", nullable: false),
                    DeleteAfterTransfer = table.Column<bool>(type: "bit", nullable: false),
                    LogLevelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SftpConfigurations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SftpConfigurations_Certificates_CertificateId",
                        column: x => x.CertificateId,
                        principalTable: "Certificates",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SftpConfigurations_LogLevels_LogLevelId",
                        column: x => x.LogLevelId,
                        principalTable: "LogLevels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LogEntries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LogLevelId = table.Column<int>(type: "int", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SftpConfigurationId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LogEntries_LogLevels_LogLevelId",
                        column: x => x.LogLevelId,
                        principalTable: "LogLevels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LogEntries_SftpConfigurations_SftpConfigurationId",
                        column: x => x.SftpConfigurationId,
                        principalTable: "SftpConfigurations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SftpTasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SftpConfigurationId = table.Column<int>(type: "int", nullable: false),
                    SourcePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DestinationPath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileFilterId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SftpTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SftpTasks_FileFilters_FileFilterId",
                        column: x => x.FileFilterId,
                        principalTable: "FileFilters",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SftpTasks_SftpConfigurations_SftpConfigurationId",
                        column: x => x.SftpConfigurationId,
                        principalTable: "SftpConfigurations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "LogLevels",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "All" },
                    { 2, "Info" },
                    { 3, "Warn" },
                    { 4, "Error" },
                    { 5, "Fatal" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_LogEntries_LogLevelId",
                table: "LogEntries",
                column: "LogLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_LogEntries_SftpConfigurationId",
                table: "LogEntries",
                column: "SftpConfigurationId");

            migrationBuilder.CreateIndex(
                name: "IX_SftpConfigurations_CertificateId",
                table: "SftpConfigurations",
                column: "CertificateId");

            migrationBuilder.CreateIndex(
                name: "IX_SftpConfigurations_LogLevelId",
                table: "SftpConfigurations",
                column: "LogLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_SftpTasks_FileFilterId",
                table: "SftpTasks",
                column: "FileFilterId");

            migrationBuilder.CreateIndex(
                name: "IX_SftpTasks_SftpConfigurationId",
                table: "SftpTasks",
                column: "SftpConfigurationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LogEntries");

            migrationBuilder.DropTable(
                name: "SftpTasks");

            migrationBuilder.DropTable(
                name: "FileFilters");

            migrationBuilder.DropTable(
                name: "SftpConfigurations");

            migrationBuilder.DropTable(
                name: "Certificates");

            migrationBuilder.DropTable(
                name: "LogLevels");
        }
    }
}
