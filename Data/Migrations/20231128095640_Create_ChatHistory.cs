using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SignalR_Chat.Data.Migrations
{
    /// <inheritdoc />
    public partial class Create_ChatHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ChatHistoryId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ChatHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatHistory_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ChatHistoryId",
                table: "AspNetUsers",
                column: "ChatHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatHistory_UserId",
                table: "ChatHistory",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_ChatHistory_ChatHistoryId",
                table: "AspNetUsers",
                column: "ChatHistoryId",
                principalTable: "ChatHistory",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_ChatHistory_ChatHistoryId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "ChatHistory");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ChatHistoryId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ChatHistoryId",
                table: "AspNetUsers");
        }
    }
}
