using Microsoft.EntityFrameworkCore.Migrations;

namespace AspNetUserRolesEntityFramework.Data.Migrations
{
    public partial class initial6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StarRating",
                table: "DiscussionForum",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StarRating",
                table: "DiscussionForum");
        }
    }
}
