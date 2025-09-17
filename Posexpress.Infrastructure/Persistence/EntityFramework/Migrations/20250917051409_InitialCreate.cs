﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Posexpress.Infrastructure.Persistence.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    IdCategoriaPadre = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categorias_Categorias_IdCategoriaPadre",
                        column: x => x.IdCategoriaPadre,
                        principalTable: "Categorias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TiposProducto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposProducto", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExpProductos",
                columns: table => new
                {
                    IdProducto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    FechaVencimiento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Observaciones = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IdTipoProducto = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpProductos", x => x.IdProducto);
                    table.ForeignKey(
                        name: "FK_ExpProductos_TiposProducto_IdTipoProducto",
                        column: x => x.IdTipoProducto,
                        principalTable: "TiposProducto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ErpProductos",
                columns: table => new
                {
                    IdProducto = table.Column<int>(type: "int", nullable: false),
                    UniqueCodigo = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Costo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ErpProductos", x => x.IdProducto);
                    table.ForeignKey(
                        name: "FK_ErpProductos_ExpProductos_IdProducto",
                        column: x => x.IdProducto,
                        principalTable: "ExpProductos",
                        principalColumn: "IdProducto",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductosCategorias",
                columns: table => new
                {
                    IdDetalle = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdProducto = table.Column<int>(type: "int", nullable: false),
                    IdCategoria = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductosCategorias", x => x.IdDetalle);
                    table.ForeignKey(
                        name: "FK_ProductosCategorias_Categorias_IdCategoria",
                        column: x => x.IdCategoria,
                        principalTable: "Categorias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductosCategorias_ExpProductos_IdProducto",
                        column: x => x.IdProducto,
                        principalTable: "ExpProductos",
                        principalColumn: "IdProducto",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VentaExpress",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Cliente = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IdProducto = table.Column<int>(type: "int", nullable: false),
                    UniqueProducto = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Descuento = table.Column<decimal>(type: "decimal(5,4)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VentaExpress", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VentaExpress_ExpProductos_IdProducto",
                        column: x => x.IdProducto,
                        principalTable: "ExpProductos",
                        principalColumn: "IdProducto",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CodigosBarras",
                columns: table => new
                {
                    IdCodigoBarra = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UniqueCodigo = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    IdProducto = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CodigosBarras", x => x.IdCodigoBarra);
                    table.ForeignKey(
                        name: "FK_CodigosBarras_ErpProductos_IdProducto",
                        column: x => x.IdProducto,
                        principalTable: "ErpProductos",
                        principalColumn: "IdProducto",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categorias",
                columns: new[] { "Id", "Activo", "Descripcion", "IdCategoriaPadre" },
                values: new object[] { 1, true, "General", null });

            migrationBuilder.InsertData(
                table: "TiposProducto",
                columns: new[] { "Id", "Descripcion" },
                values: new object[,]
                {
                    { 1, "General" },
                    { 2, "Service" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categorias_IdCategoriaPadre",
                table: "Categorias",
                column: "IdCategoriaPadre");

            migrationBuilder.CreateIndex(
                name: "IX_CodigosBarras_IdProducto",
                table: "CodigosBarras",
                column: "IdProducto");

            migrationBuilder.CreateIndex(
                name: "IX_CodigosBarras_UniqueCodigo",
                table: "CodigosBarras",
                column: "UniqueCodigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ErpProductos_UniqueCodigo",
                table: "ErpProductos",
                column: "UniqueCodigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExpProductos_IdTipoProducto",
                table: "ExpProductos",
                column: "IdTipoProducto");

            migrationBuilder.CreateIndex(
                name: "IX_ProductosCategorias_IdCategoria",
                table: "ProductosCategorias",
                column: "IdCategoria");

            migrationBuilder.CreateIndex(
                name: "IX_ProductosCategorias_IdProducto_IdCategoria",
                table: "ProductosCategorias",
                columns: new[] { "IdProducto", "IdCategoria" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VentaExpress_IdProducto",
                table: "VentaExpress",
                column: "IdProducto");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CodigosBarras");

            migrationBuilder.DropTable(
                name: "ProductosCategorias");

            migrationBuilder.DropTable(
                name: "VentaExpress");

            migrationBuilder.DropTable(
                name: "ErpProductos");

            migrationBuilder.DropTable(
                name: "Categorias");

            migrationBuilder.DropTable(
                name: "ExpProductos");

            migrationBuilder.DropTable(
                name: "TiposProducto");
        }
    }
}
