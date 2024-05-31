using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
#nullable disable

namespace jwt__dev_Creed.Migrations
{
    public partial class seedd_data : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // rule 1
            migrationBuilder.InsertData(

                table: "AspNetRoles",
                columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
                values : new object[] {Guid.NewGuid().ToString(),"User","User".ToUpper(),Guid.NewGuid().ToString()}
            );

            migrationBuilder.InsertData(

               table: "AspNetRoles",
               columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
               values: new object[] { Guid.NewGuid().ToString(), "Admin", "Admin".ToUpper(), Guid.NewGuid().ToString() }
           );



        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Delete all from [AspNetRoles]");
        }
    }
}
