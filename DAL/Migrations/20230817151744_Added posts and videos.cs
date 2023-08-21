using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class Addedpostsandvideos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_CloudinaryImages_CloudinaryImageId",
                table: "Images");

            migrationBuilder.RenameColumn(
                name: "CloudinaryImageId",
                table: "Images",
                newName: "PostId");

            migrationBuilder.RenameIndex(
                name: "IX_Images_CloudinaryImageId",
                table: "Images",
                newName: "IX_Images_PostId");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CloudinaryEntityId",
                table: "Images",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Posts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Videos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Reference = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    PostId = table.Column<int>(type: "int", nullable: true),
                    CloudinaryEntityId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Videos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Videos_CloudinaryImages_CloudinaryEntityId",
                        column: x => x.CloudinaryEntityId,
                        principalTable: "CloudinaryImages",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Videos_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Videos_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Images_CloudinaryEntityId",
                table: "Images",
                column: "CloudinaryEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_UserId",
                table: "Posts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Videos_CloudinaryEntityId",
                table: "Videos",
                column: "CloudinaryEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Videos_PostId",
                table: "Videos",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Videos_UserId",
                table: "Videos",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_CloudinaryImages_CloudinaryEntityId",
                table: "Images",
                column: "CloudinaryEntityId",
                principalTable: "CloudinaryImages",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Posts_PostId",
                table: "Images",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_CloudinaryImages_CloudinaryEntityId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_Posts_PostId",
                table: "Images");

            migrationBuilder.DropTable(
                name: "Videos");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Images_CloudinaryEntityId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CloudinaryEntityId",
                table: "Images");

            migrationBuilder.RenameColumn(
                name: "PostId",
                table: "Images",
                newName: "CloudinaryImageId");

            migrationBuilder.RenameIndex(
                name: "IX_Images_PostId",
                table: "Images",
                newName: "IX_Images_CloudinaryImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_CloudinaryImages_CloudinaryImageId",
                table: "Images",
                column: "CloudinaryImageId",
                principalTable: "CloudinaryImages",
                principalColumn: "Id");
        }
    }
}
