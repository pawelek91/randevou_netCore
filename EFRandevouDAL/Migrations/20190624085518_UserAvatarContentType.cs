using Microsoft.EntityFrameworkCore.Migrations;

namespace EFRandevouDAL.Migrations
{
    public partial class UserAvatarContentType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AvatarContentType",
                table: "UsersDetails",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvatarContentType",
                table: "UsersDetails");
        }
    }
}
