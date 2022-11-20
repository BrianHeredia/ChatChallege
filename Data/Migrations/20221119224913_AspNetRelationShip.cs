using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatChallenge.Data.Migrations
{
    public partial class AspNetRelationShip : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatChatUser_ChatUser_JoinedChatUsersChatUserId",
                schema: "dbo",
                table: "ChatChatUser");

            migrationBuilder.DropForeignKey(
                name: "FK_ChatUser_ChatMessage",
                schema: "dbo",
                table: "ChatMessage");

            migrationBuilder.DropTable(
                name: "ChatUser",
                schema: "dbo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChatChatUser",
                schema: "dbo",
                table: "ChatChatUser");

            migrationBuilder.DropColumn(
                name: "JoinedChatUsersChatUserId",
                schema: "dbo",
                table: "ChatChatUser");

            migrationBuilder.RenameTable(
                name: "ChatChatUser",
                schema: "dbo",
                newName: "ChatChatUser");

            migrationBuilder.AlterColumn<string>(
                name: "ChatUserId",
                schema: "dbo",
                table: "ChatMessage",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "JoinedChatUsersId",
                table: "ChatChatUser",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChatChatUser",
                table: "ChatChatUser",
                columns: new[] { "JoinedChatUsersId", "JoinedChatsRoomsChatId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ChatChatUser_AspNetUsers_JoinedChatUsersId",
                table: "ChatChatUser",
                column: "JoinedChatUsersId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChatUser_ChatMessage",
                schema: "dbo",
                table: "ChatMessage",
                column: "ChatUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatChatUser_AspNetUsers_JoinedChatUsersId",
                table: "ChatChatUser");

            migrationBuilder.DropForeignKey(
                name: "FK_ChatUser_ChatMessage",
                schema: "dbo",
                table: "ChatMessage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChatChatUser",
                table: "ChatChatUser");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "JoinedChatUsersId",
                table: "ChatChatUser");

            migrationBuilder.RenameTable(
                name: "ChatChatUser",
                newName: "ChatChatUser",
                newSchema: "dbo");

            migrationBuilder.AlterColumn<Guid>(
                name: "ChatUserId",
                schema: "dbo",
                table: "ChatMessage",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<Guid>(
                name: "JoinedChatUsersChatUserId",
                schema: "dbo",
                table: "ChatChatUser",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChatChatUser",
                schema: "dbo",
                table: "ChatChatUser",
                columns: new[] { "JoinedChatUsersChatUserId", "JoinedChatsRoomsChatId" });

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

            migrationBuilder.AddForeignKey(
                name: "FK_ChatChatUser_ChatUser_JoinedChatUsersChatUserId",
                schema: "dbo",
                table: "ChatChatUser",
                column: "JoinedChatUsersChatUserId",
                principalSchema: "dbo",
                principalTable: "ChatUser",
                principalColumn: "ChatUserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChatUser_ChatMessage",
                schema: "dbo",
                table: "ChatMessage",
                column: "ChatUserId",
                principalSchema: "dbo",
                principalTable: "ChatUser",
                principalColumn: "ChatUserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
