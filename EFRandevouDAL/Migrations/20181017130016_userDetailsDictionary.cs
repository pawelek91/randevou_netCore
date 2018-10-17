using Microsoft.EntityFrameworkCore.Migrations;

namespace EFRandevouDAL.Migrations
{
    public partial class userDetailsDictionary : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsersDetailsItemsValues");
        }
    }
}
