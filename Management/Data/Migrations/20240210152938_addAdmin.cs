using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Collections.Generic;
using System.Security;
using System.Xml.Linq;

#nullable disable

namespace Management.Data.Migrations
{
    public partial class addAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO[security].[Users] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Fname], [Lname]) VALUES(N'0c3f2932-12f3-4f7a-b139-d1ee37dbc179', N'Admin', N'Admin', N'AdminNasser@gmail.com', N'ADMINNASSER@GMAIL.COM', 0, N'AQAAAAEAACcQAAAAEEoQcH/QfSsFUqoBsGUvVb+MoQw4NWENNZj10WlyhBcMtt9CaZv6IZTQ9emsdoQn7A==', N'LHGT3JLHI6YU2T74Y7JJAWCBD4IET5FJ', N'bd70d7d7-72cc-47bd-9491-b38d55690a85', NULL, 0, 0, NULL, 1, 0, N'mohamed', N'nasser')");
            
        }
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [security].[Users] WHERE Id ='0c3f2932-12f3-4f7a-b139-d1ee37dbc179' ");
        }
    }
}
