using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TGJ.NetworkFreight.UserServices.Migrations
{
    public partial class init1018 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Phone = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    HasAuthenticated = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    RoleName = table.Column<string>(nullable: true),
                    LoginPassword = table.Column<string>(nullable: true),
                    wx_OpenID = table.Column<string>(nullable: true),
                    wx_UnionID = table.Column<string>(nullable: true),
                    wx_HeadImgUrl = table.Column<string>(nullable: true),
                    wx_NickName = table.Column<string>(nullable: true),
                    IDCard = table.Column<string>(nullable: true),
                    CarClass = table.Column<string>(nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    LastUpdateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
