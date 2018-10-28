using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EFRandevouDAL.Migrations
{
    public partial class InitialValues : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    SendDate = table.Column<DateTime>(nullable: false),
                    ReadDate = table.Column<DateTime>(nullable: true),
                    MessageContent = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                });

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
                table: "Users",
                columns: new[] { "Id", "Name", "Gender", "BirthDate", "IsDeleted" },
                values: new object[] { 1, "User1", 'F', new System.DateTime(1999, 1, 1), false}
                );


            migrationBuilder.InsertData(
                table: "UserDetailsDictionary",
                columns: new[] { "Id", "DetailsType", "DisplayName", "IsDeleted", "Name", "ObjectType" },
                values: new object[] { 1, "EyesColor", "Brązowe", false, "Brązowe", "boolean" });

            migrationBuilder.InsertData(
                table: "UserDetailsDictionary",
                columns: new[] { "Id", "DetailsType", "DisplayName", "IsDeleted", "Name", "ObjectType" },
                values: new object[] { 2, "EyesColor", "Niebieskie", false, "Niebieskie", "boolean" });

            migrationBuilder.InsertData(
                table: "UserDetailsDictionary",
                columns: new[] { "Id", "DetailsType", "DisplayName", "IsDeleted", "Name", "ObjectType" },
                values: new object[] { 3, "EyesColor", "zielone", false, "zielone", "boolean" });

            migrationBuilder.InsertData(
                table: "UserDetailsDictionary",
                columns: new[] { "Id", "DetailsType", "DisplayName", "IsDeleted", "Name", "ObjectType" },
                values: new object[] { 4, "HairColor", "ciemne", false, "ciemne", "boolean" });

            migrationBuilder.InsertData(
                table: "UserDetailsDictionary",
                columns: new[] { "Id", "DetailsType", "DisplayName", "IsDeleted", "Name", "ObjectType" },
                values: new object[] { 5, "HairColor", "jasne", false, "jasne", "boolean" });

            migrationBuilder.InsertData(
                table: "UserDetailsDictionary",
                columns: new[] { "Id", "DetailsType", "DisplayName", "IsDeleted", "Name", "ObjectType" },
                values: new object[] { 6, "Interests", "piłka nożna", false, "football", "boolean" });

            migrationBuilder.InsertData(
                table: "UserDetailsDictionary",
                columns: new[] { "Id", "DetailsType", "DisplayName", "IsDeleted", "Name", "ObjectType" },
                values: new object[] { 7, "Interests", "koszykówka", false, "koszykówka", "boolean" });

            migrationBuilder.InsertData(
                table: "UserDetailsDictionary",
                columns: new[] { "Id", "DetailsType", "DisplayName", "IsDeleted", "Name", "ObjectType" },
                values: new object[] { 8, "Interests", "szachy", false, "szachy", "boolean" });

            migrationBuilder.CreateIndex(
                name: "IX_UsersDetails_UserId",
                table: "UsersDetails",
                column: "UserId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
