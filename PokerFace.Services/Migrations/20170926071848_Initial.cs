using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace PokerFace.Services.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                    DateModified = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                    Name = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: true),
                    ShortIdentifier = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: true),
                    State = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Player",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                    DateModified = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                    GameId = table.Column<Guid>(type: "char(36)", nullable: false),
                    Name = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Player", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Player_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Round",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                    DateModified = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                    GameId = table.Column<Guid>(type: "char(36)", nullable: false),
                    Number = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Round", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Round_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlayerHand",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                    DateModified = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                    PlayerId = table.Column<Guid>(type: "char(36)", nullable: false),
                    RoundId = table.Column<Guid>(type: "char(36)", nullable: false),
                    StoryPoints = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerHand", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayerHand_Round_RoundId",
                        column: x => x.RoundId,
                        principalTable: "Round",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Player_GameId",
                table: "Player",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerHand_RoundId",
                table: "PlayerHand",
                column: "RoundId");

            migrationBuilder.CreateIndex(
                name: "IX_Round_GameId",
                table: "Round",
                column: "GameId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Player");

            migrationBuilder.DropTable(
                name: "PlayerHand");

            migrationBuilder.DropTable(
                name: "Round");

            migrationBuilder.DropTable(
                name: "Games");
        }
    }
}
