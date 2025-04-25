using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Types",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TypeData = table.Column<int>(type: "int", nullable: true),
                    Discriminator = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsAbstract = table.Column<bool>(type: "bit", nullable: true),
                    IsSealed = table.Column<bool>(type: "bit", nullable: true),
                    BaseClassId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Access = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Types", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Types_Types_BaseClassId",
                        column: x => x.BaseClassId,
                        principalTable: "Types",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Atributes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataType = table.Column<int>(type: "int", nullable: true),
                    Access = table.Column<int>(type: "int", nullable: true),
                    ClassId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Atributes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Atributes_Types_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Types",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Methods",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReturnType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsAbstract = table.Column<bool>(type: "bit", nullable: true),
                    IsSealed = table.Column<bool>(type: "bit", nullable: true),
                    Access = table.Column<int>(type: "int", nullable: true),
                    ClassId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Methods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Methods_Types_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Types",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LocalVariables",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: true),
                    MethodId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalVariables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LocalVariables_Methods_MethodId",
                        column: x => x.MethodId,
                        principalTable: "Methods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MethodMethod",
                columns: table => new
                {
                    InvokedMethodsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MethodId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MethodMethod", x => new { x.InvokedMethodsId, x.MethodId });
                    table.ForeignKey(
                        name: "FK_MethodMethod_Methods_InvokedMethodsId",
                        column: x => x.InvokedMethodsId,
                        principalTable: "Methods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MethodMethod_Methods_MethodId",
                        column: x => x.MethodId,
                        principalTable: "Methods",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Parameters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Access = table.Column<int>(type: "int", nullable: true),
                    DataType = table.Column<int>(type: "int", nullable: true),
                    MethodId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parameters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Parameters_Methods_MethodId",
                        column: x => x.MethodId,
                        principalTable: "Methods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Atributes_ClassId",
                table: "Atributes",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_LocalVariables_MethodId",
                table: "LocalVariables",
                column: "MethodId");

            migrationBuilder.CreateIndex(
                name: "IX_MethodMethod_MethodId",
                table: "MethodMethod",
                column: "MethodId");

            migrationBuilder.CreateIndex(
                name: "IX_Methods_ClassId",
                table: "Methods",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Parameters_MethodId",
                table: "Parameters",
                column: "MethodId");

            migrationBuilder.CreateIndex(
                name: "IX_Types_BaseClassId",
                table: "Types",
                column: "BaseClassId",
                unique: true,
                filter: "[BaseClassId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Atributes");

            migrationBuilder.DropTable(
                name: "LocalVariables");

            migrationBuilder.DropTable(
                name: "MethodMethod");

            migrationBuilder.DropTable(
                name: "Parameters");

            migrationBuilder.DropTable(
                name: "Methods");

            migrationBuilder.DropTable(
                name: "Types");
        }
    }
}
