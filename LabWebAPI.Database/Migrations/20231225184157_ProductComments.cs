using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LabWebAPI.Database.Migrations
{
    /// <inheritdoc />
    public partial class ProductComments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comment");
        }
    }
}
