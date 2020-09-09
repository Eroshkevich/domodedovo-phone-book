using Microsoft.EntityFrameworkCore.Migrations;

namespace Domodedovo.PhoneBook.Data.Migrations
{
    public partial class DbImages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Picture",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Picture");
        }
    }
}
