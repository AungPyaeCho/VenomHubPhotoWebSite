using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VenomHubPhotoWebSiteAdminPanel.Migrations
{
    /// <inheritdoc />
    public partial class photoDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblAdmin",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AdminName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AdminEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdminPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdminCreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblAdmin", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblAlbum",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AlbumName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AlbumDescription = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AlbumPhoto = table.Column<string>(type: "nvarchar(2500)", maxLength: 2500, nullable: false),
                    AlbumCreateAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblAlbum", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CategoryDescription = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CategoryPhoto = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    AlbumCreateAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblLoginDetail",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AdminId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdminEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdminName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SessionId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SessionExpired = table.Column<DateTime>(type: "datetime2", nullable: true),
                    loginAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LogOutAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblLoginDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblLoginDetail_tblAdmin_Id",
                        column: x => x.Id,
                        principalTable: "tblAdmin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblPhoto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhotoName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhotoDescription = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Photo = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AlbumId = table.Column<int>(type: "int", nullable: true),
                    AlbumName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AlbumCreateAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblPhoto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblPhoto_tblAlbum_AlbumId",
                        column: x => x.AlbumId,
                        principalTable: "tblAlbum",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblPhoto_tblCategory_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "tblCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "tblAdmin",
                columns: new[] { "Id", "AdminCreatedAt", "AdminEmail", "AdminName", "AdminPassword" },
                values: new object[] { "a11e5c1c-b664-4a25-b493-634b5fbfb1fe", new DateTime(2024, 8, 6, 6, 55, 10, 627, DateTimeKind.Utc).AddTicks(1967), "admin@photo.com", "Default Admin", "QWRtaW5AMTIz" });

            migrationBuilder.CreateIndex(
                name: "IX_tblPhoto_AlbumId",
                table: "tblPhoto",
                column: "AlbumId");

            migrationBuilder.CreateIndex(
                name: "IX_tblPhoto_CategoryId",
                table: "tblPhoto",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblLoginDetail");

            migrationBuilder.DropTable(
                name: "tblPhoto");

            migrationBuilder.DropTable(
                name: "tblAdmin");

            migrationBuilder.DropTable(
                name: "tblAlbum");

            migrationBuilder.DropTable(
                name: "tblCategory");
        }
    }
}
