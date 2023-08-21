using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddedFollowandFriend : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ImagePlaceholders_ImageId",
                table: "ImagePlaceholders");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "FollowLists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FollowLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FollowLists_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FriendLists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FriendLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FriendLists_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FollowUser",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    FollowListId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FollowUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FollowUser_FollowLists_FollowListId",
                        column: x => x.FollowListId,
                        principalTable: "FollowLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FollowUser_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FriendUser",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    FriendListId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FriendUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FriendUser_FriendLists_FriendListId",
                        column: x => x.FriendListId,
                        principalTable: "FriendLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FriendUser_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ImagePlaceholders_ImageId",
                table: "ImagePlaceholders",
                column: "ImageId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FollowLists_UserId",
                table: "FollowLists",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FollowUser_FollowListId",
                table: "FollowUser",
                column: "FollowListId");

            migrationBuilder.CreateIndex(
                name: "IX_FollowUser_UserId",
                table: "FollowUser",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FriendLists_UserId",
                table: "FriendLists",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FriendUser_FriendListId",
                table: "FriendUser",
                column: "FriendListId");

            migrationBuilder.CreateIndex(
                name: "IX_FriendUser_UserId",
                table: "FriendUser",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FollowUser");

            migrationBuilder.DropTable(
                name: "FriendUser");

            migrationBuilder.DropTable(
                name: "FollowLists");

            migrationBuilder.DropTable(
                name: "FriendLists");

            migrationBuilder.DropIndex(
                name: "IX_ImagePlaceholders_ImageId",
                table: "ImagePlaceholders");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Users");

            migrationBuilder.CreateIndex(
                name: "IX_ImagePlaceholders_ImageId",
                table: "ImagePlaceholders",
                column: "ImageId");
        }
    }
}
