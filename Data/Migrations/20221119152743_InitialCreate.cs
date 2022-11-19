using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatChallenge.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Chat",
                schema: "dbo",
                columns: table => new
                {
                    ChatId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chat", x => x.ChatId);
                });

            migrationBuilder.CreateTable(
                name: "ChatUser",
                schema: "dbo",
                columns: table => new
                {
                    ChatUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Username = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatUser", x => x.ChatUserId);
                });

            migrationBuilder.CreateTable(
                name: "ChatChatUser",
                schema: "dbo",
                columns: table => new
                {
                    JoinedChatUsersChatUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JoinedChatsRoomsChatId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatChatUser", x => new { x.JoinedChatUsersChatUserId, x.JoinedChatsRoomsChatId });
                    table.ForeignKey(
                        name: "FK_ChatChatUser_Chat_JoinedChatsRoomsChatId",
                        column: x => x.JoinedChatsRoomsChatId,
                        principalSchema: "dbo",
                        principalTable: "Chat",
                        principalColumn: "ChatId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChatChatUser_ChatUser_JoinedChatUsersChatUserId",
                        column: x => x.JoinedChatUsersChatUserId,
                        principalSchema: "dbo",
                        principalTable: "ChatUser",
                        principalColumn: "ChatUserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChatMessage",
                schema: "dbo",
                columns: table => new
                {
                    ChatMessageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChatUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChatId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Message = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatMessage", x => x.ChatMessageId);
                    table.ForeignKey(
                        name: "FK_Chat_ChatMessage",
                        column: x => x.ChatId,
                        principalSchema: "dbo",
                        principalTable: "Chat",
                        principalColumn: "ChatId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChatUser_ChatMessage",
                        column: x => x.ChatUserId,
                        principalSchema: "dbo",
                        principalTable: "ChatUser",
                        principalColumn: "ChatUserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChatChatUser_JoinedChatsRoomsChatId",
                schema: "dbo",
                table: "ChatChatUser",
                column: "JoinedChatsRoomsChatId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessage_ChatId",
                schema: "dbo",
                table: "ChatMessage",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessage_ChatUserId",
                schema: "dbo",
                table: "ChatMessage",
                column: "ChatUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChatChatUser",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ChatMessage",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Chat",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ChatUser",
                schema: "dbo");
        }
    }
}
