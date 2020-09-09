using Microsoft.EntityFrameworkCore.Migrations;

namespace Domodedovo.PhoneBook.Data.Migrations
{
    public partial class FsImages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Path",
                table: "Picture",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Path",
                table: "Picture");
        }
    }
}
