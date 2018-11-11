using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EFRandevouDAL.Migrations
{
    public partial class UsersFriends : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserDetailsDictionary",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DetailsType = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    DisplayName = table.Column<string>(nullable: true),
                    ObjectType = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDetailsDictionary", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    DisplayName = table.Column<string>(nullable: true),
                    Gender = table.Column<char>(nullable: false),
                    BirthDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Friendships",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    User1Id = table.Column<int>(nullable: false),
                    User2Id = table.Column<int>(nullable: false),
                    RelationStatus = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Friendships", x => new { x.User1Id, x.User2Id });
                    table.ForeignKey(
                        name: "FK_Friendships_Users_User1Id",
                        column: x => x.User1Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Friendships_Users_User2Id",
                        column: x => x.User2Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    FromUserId = table.Column<int>(nullable: false),
                    ToUserId = table.Column<int>(nullable: false),
                    SendDate = table.Column<DateTime>(nullable: false),
                    ReadDate = table.Column<DateTime>(nullable: true),
                    MessageContent = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_Users_FromUserId",
                        column: x => x.FromUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Messages_Users_ToUserId",
                        column: x => x.ToUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsersDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Width = table.Column<int>(nullable: false),
                    Heigth = table.Column<int>(nullable: false),
                    Region = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Tattos = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsersDetails_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsersDetailsItemsValues",
                columns: table => new
                {
                    UserDetailsId = table.Column<int>(nullable: false),
                    UserDetailsDictionaryItemId = table.Column<int>(nullable: false),
                    Value = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersDetailsItemsValues", x => new { x.UserDetailsId, x.UserDetailsDictionaryItemId });
                    table.UniqueConstraint("AK_UsersDetailsItemsValues_UserDetailsDictionaryItemId_UserDetailsId", x => new { x.UserDetailsDictionaryItemId, x.UserDetailsId });
                    table.ForeignKey(
                        name: "FK_UsersDetailsItemsValues_UserDetailsDictionary_UserDetailsDictionaryItemId",
                        column: x => x.UserDetailsDictionaryItemId,
                        principalTable: "UserDetailsDictionary",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsersDetailsItemsValues_UsersDetails_UserDetailsId",
                        column: x => x.UserDetailsId,
                        principalTable: "UsersDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "UserDetailsDictionary",
                columns: new[] { "Id", "DetailsType", "DisplayName", "IsDeleted", "Name", "ObjectType" },
                values: new object[] { 1, "EyesColor", "Brązowe", false, "Brown", "boolean" });

            migrationBuilder.InsertData(
                table: "UserDetailsDictionary",
                columns: new[] { "Id", "DetailsType", "DisplayName", "IsDeleted", "Name", "ObjectType" },
                values: new object[] { 2, "EyesColor", "Niebieskie", false, "Blue", "boolean" });

            migrationBuilder.InsertData(
                table: "UserDetailsDictionary",
                columns: new[] { "Id", "DetailsType", "DisplayName", "IsDeleted", "Name", "ObjectType" },
                values: new object[] { 3, "EyesColor", "zielone", false, "Green", "boolean" });

            migrationBuilder.InsertData(
                table: "UserDetailsDictionary",
                columns: new[] { "Id", "DetailsType", "DisplayName", "IsDeleted", "Name", "ObjectType" },
                values: new object[] { 4, "HairColor", "ciemne", false, "HairDark", "boolean" });

            migrationBuilder.InsertData(
                table: "UserDetailsDictionary",
                columns: new[] { "Id", "DetailsType", "DisplayName", "IsDeleted", "Name", "ObjectType" },
                values: new object[] { 5, "HairColor", "jasne", false, "HairLight", "boolean" });

            migrationBuilder.InsertData(
                table: "UserDetailsDictionary",
                columns: new[] { "Id", "DetailsType", "DisplayName", "IsDeleted", "Name", "ObjectType" },
                values: new object[] { 6, "Interests", "piłka nożna", false, "Football", "boolean" });

            migrationBuilder.InsertData(
                table: "UserDetailsDictionary",
                columns: new[] { "Id", "DetailsType", "DisplayName", "IsDeleted", "Name", "ObjectType" },
                values: new object[] { 7, "Interests", "koszykówka", false, "Basketball", "boolean" });

            migrationBuilder.InsertData(
                table: "UserDetailsDictionary",
                columns: new[] { "Id", "DetailsType", "DisplayName", "IsDeleted", "Name", "ObjectType" },
                values: new object[] { 8, "Interests", "szachy", false, "Chess", "boolean" });

            migrationBuilder.CreateIndex(
                name: "IX_Friendships_User2Id",
                table: "Friendships",
                column: "User2Id");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_FromUserId",
                table: "Messages",
                column: "FromUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ToUserId",
                table: "Messages",
                column: "ToUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersDetails_UserId",
                table: "UsersDetails",
                column: "UserId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Friendships");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "UsersDetailsItemsValues");

            migrationBuilder.DropTable(
                name: "UserDetailsDictionary");

            migrationBuilder.DropTable(
                name: "UsersDetails");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
