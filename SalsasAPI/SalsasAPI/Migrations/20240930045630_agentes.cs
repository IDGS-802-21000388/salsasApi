using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalsasAPI.Migrations
{
    /// <inheritdoc />
    public partial class agentes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Direccion",
                columns: table => new
                {
                    idDireccion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    estado = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    municipio = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    codigoPostal = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    colonia = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    calle = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    numExt = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    numInt = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    referencia = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Direccion__645723A6D661BB22", x => x.idDireccion);
                });

            migrationBuilder.CreateTable(
                name: "InventarioReporte",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Cantidad = table.Column<double>(type: "float", nullable: false),
                    UltimaActualizacion = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventarioReporte", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LogsUser",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    procedimiento = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    lastDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    idUsuario = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__LogsUser__3213E83F1B07DC4C", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Medida",
                columns: table => new
                {
                    idMedida = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tipoMedida = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Medida__4E0391E8A3E17875", x => x.idMedida);
                });

            migrationBuilder.CreateTable(
                name: "Paqueteria",
                columns: table => new
                {
                    idPaqueteria = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombrePaqueteria = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false, defaultValue: ""),
                    telefono = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: false, defaultValue: ""),
                    direccion = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false, defaultValue: ""),
                    estatus = table.Column<int>(type: "int", nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Paqueter__90A13AAB7556F2B1", x => x.idPaqueteria);
                });

            migrationBuilder.CreateTable(
                name: "Proveedor",
                columns: table => new
                {
                    idProveedor = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombreProveedor = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false, defaultValue: ""),
                    direccion = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false, defaultValue: ""),
                    telefono = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: false, defaultValue: ""),
                    nombreAtiende = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false, defaultValue: ""),
                    estatus = table.Column<int>(type: "int", nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Proveedo__A3FA8E6B5E0DDC30", x => x.idProveedor);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    idUsuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "varchar(45)", unicode: false, maxLength: 45, nullable: false),
                    nombreUsuario = table.Column<string>(type: "varchar(45)", unicode: false, maxLength: 45, nullable: false, defaultValue: ""),
                    correo = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false, defaultValue: ""),
                    contrasenia = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false, defaultValue: ""),
                    rol = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    estatus = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    telefono = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: false, defaultValue: ""),
                    intentos = table.Column<int>(type: "int", nullable: false),
                    idDireccion = table.Column<int>(type: "int", nullable: false),
                    dateLastToken = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    IdAgente = table.Column<int>(type: "int", nullable: true),
                    AgenteIdUsuario = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Usuario__645723A6C661BB22", x => x.idUsuario);
                    table.ForeignKey(
                        name: "FK_Usuario_Direccion",
                        column: x => x.idDireccion,
                        principalTable: "Direccion",
                        principalColumn: "idDireccion");
                    table.ForeignKey(
                        name: "FK_Usuario_Usuario_AgenteIdUsuario",
                        column: x => x.AgenteIdUsuario,
                        principalTable: "Usuario",
                        principalColumn: "idUsuario");
                });

            migrationBuilder.CreateTable(
                name: "Producto",
                columns: table => new
                {
                    idProducto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombreProducto = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false, defaultValue: ""),
                    precioVenta = table.Column<double>(type: "float", nullable: false),
                    precioProduccion = table.Column<double>(type: "float", nullable: false),
                    cantidad = table.Column<double>(type: "float", nullable: false),
                    idMedida = table.Column<int>(type: "int", nullable: true),
                    fotografia = table.Column<string>(type: "text", nullable: true),
                    estatus = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Producto__07F4A1321305AC9C", x => x.idProducto);
                    table.ForeignKey(
                        name: "FK__Producto__idMedi__60A75C0F",
                        column: x => x.idMedida,
                        principalTable: "Medida",
                        principalColumn: "idMedida");
                });

            migrationBuilder.CreateTable(
                name: "MateriaPrima",
                columns: table => new
                {
                    idMateriaPrima = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombreMateria = table.Column<string>(type: "varchar(45)", unicode: false, maxLength: 45, nullable: false, defaultValue: ""),
                    precioCompra = table.Column<double>(type: "float", nullable: false),
                    idMedida = table.Column<int>(type: "int", nullable: true),
                    idProveedor = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__MateriaP__97EB058FD45C14F1", x => x.idMateriaPrima);
                    table.ForeignKey(
                        name: "FK__MateriaPr__idMed__4D94879B",
                        column: x => x.idMedida,
                        principalTable: "Medida",
                        principalColumn: "idMedida");
                    table.ForeignKey(
                        name: "FK__MateriaPr__idPro__4E88ABD4",
                        column: x => x.idProveedor,
                        principalTable: "Proveedor",
                        principalColumn: "idProveedor");
                });

            migrationBuilder.CreateTable(
                name: "RankingClientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    NombreUsuario = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ComprasTotales = table.Column<double>(type: "float", nullable: false),
                    ProductosComprados = table.Column<string>(type: "text", nullable: false),
                    UltimaActualizacion = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RankingClientes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RankingClientes_Usuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuario",
                        principalColumn: "idUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Venta",
                columns: table => new
                {
                    idVenta = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fechaVenta = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    total = table.Column<double>(type: "float", nullable: false),
                    IdUsuario = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Venta__077D56146F550EF7", x => x.idVenta);
                    table.ForeignKey(
                        name: "FK_Venta_Usuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuario",
                        principalColumn: "idUsuario");
                });

            migrationBuilder.CreateTable(
                name: "Detalle_producto",
                columns: table => new
                {
                    idDetalle_producto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fechaVencimiento = table.Column<DateTime>(type: "datetime", nullable: true),
                    cantidadExistentes = table.Column<int>(type: "int", nullable: false),
                    estatus = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    idProducto = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Detalle___505159E1638AC0C9", x => x.idDetalle_producto);
                    table.ForeignKey(
                        name: "FK__Detalle_p__idPro__656C112C",
                        column: x => x.idProducto,
                        principalTable: "Producto",
                        principalColumn: "idProducto");
                });

            migrationBuilder.CreateTable(
                name: "PasoReceta",
                columns: table => new
                {
                    idPasoReceta = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    paso = table.Column<int>(type: "int", nullable: true),
                    descripcion = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    idProducto = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PasoRece__F29634850B24BD79", x => x.idPasoReceta);
                    table.ForeignKey(
                        name: "FK__PasoRecet__idPro__14270015",
                        column: x => x.idProducto,
                        principalTable: "Producto",
                        principalColumn: "idProducto");
                });

            migrationBuilder.CreateTable(
                name: "Receta",
                columns: table => new
                {
                    idReceta = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idMedida = table.Column<int>(type: "int", nullable: true),
                    idProducto = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Receta__7D03FC81C76DEC79", x => x.idReceta);
                    table.ForeignKey(
                        name: "FK__Receta__idMedida__73BA3083",
                        column: x => x.idMedida,
                        principalTable: "Medida",
                        principalColumn: "idMedida");
                    table.ForeignKey(
                        name: "FK__Receta__idProduc__74AE54BC",
                        column: x => x.idProducto,
                        principalTable: "Producto",
                        principalColumn: "idProducto");
                });

            migrationBuilder.CreateTable(
                name: "VentasPorProductoPeriodo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductoId = table.Column<int>(type: "int", nullable: false),
                    NombreProducto = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PeriodoInicio = table.Column<DateTime>(type: "date", nullable: false),
                    PeriodoFin = table.Column<DateTime>(type: "date", nullable: false),
                    NumeroVentas = table.Column<int>(type: "int", nullable: false),
                    CantidadVendida = table.Column<double>(type: "float", nullable: false),
                    TotalRecaudado = table.Column<double>(type: "float", nullable: false),
                    IndicadorGlobal = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VentasPorProductoPeriodo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VentasPorProductoPeriodo_Producto",
                        column: x => x.ProductoId,
                        principalTable: "Producto",
                        principalColumn: "idProducto");
                });

            migrationBuilder.CreateTable(
                name: "Detalle_materia_prima",
                columns: table => new
                {
                    idDetalle_materia_prima = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fechaCompra = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    fechaVencimiento = table.Column<DateTime>(type: "datetime", nullable: true),
                    cantidadExistentes = table.Column<double>(type: "float", nullable: false),
                    estatus = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    idMateriaPrima = table.Column<int>(type: "int", nullable: true),
                    porcentaje = table.Column<int>(type: "int", nullable: false, defaultValue: 100)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Detalle___451468AC4075AEB9", x => x.idDetalle_materia_prima);
                    table.ForeignKey(
                        name: "FK__Detalle_m__idMat__5535A963",
                        column: x => x.idMateriaPrima,
                        principalTable: "MateriaPrima",
                        principalColumn: "idMateriaPrima");
                });

            migrationBuilder.CreateTable(
                name: "DetalleVenta",
                columns: table => new
                {
                    idDetalleVenta = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cantidad = table.Column<double>(type: "float", nullable: false),
                    subtotal = table.Column<double>(type: "float", nullable: false),
                    idVenta = table.Column<int>(type: "int", nullable: true),
                    idProducto = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__DetalleV__BFE2843F1CD4AC29", x => x.idDetalleVenta);
                    table.ForeignKey(
                        name: "FK__DetalleVe__idPro__02FC7413",
                        column: x => x.idProducto,
                        principalTable: "Producto",
                        principalColumn: "idProducto");
                    table.ForeignKey(
                        name: "FK__DetalleVe__idVen__02084FDA",
                        column: x => x.idVenta,
                        principalTable: "Venta",
                        principalColumn: "idVenta");
                });

            migrationBuilder.CreateTable(
                name: "Envio",
                columns: table => new
                {
                    idEnvio = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fechaEnvio = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    fechaEntregaEstimada = table.Column<DateTime>(type: "datetime", nullable: true),
                    fechaEntregaReal = table.Column<DateTime>(type: "datetime", nullable: true),
                    estatus = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false, defaultValue: "pendiente"),
                    idVenta = table.Column<int>(type: "int", nullable: true),
                    idPaqueteria = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Envio__527F831F0C350BBA", x => x.idEnvio);
                    table.ForeignKey(
                        name: "FK__Envio__idPaquete__2BFE89A6",
                        column: x => x.idPaqueteria,
                        principalTable: "Paqueteria",
                        principalColumn: "idPaqueteria");
                    table.ForeignKey(
                        name: "FK__Envio__idVenta__2B0A656D",
                        column: x => x.idVenta,
                        principalTable: "Venta",
                        principalColumn: "idVenta");
                });

            migrationBuilder.CreateTable(
                name: "Movimiento",
                columns: table => new
                {
                    idMovimiento = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fechaMovimiento = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    tipoMovimiento = table.Column<string>(type: "varchar(45)", unicode: false, maxLength: 45, nullable: false, defaultValue: ""),
                    monto = table.Column<double>(type: "float", nullable: false),
                    idVenta = table.Column<int>(type: "int", nullable: true),
                    idMateriaPrima = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Movimien__628521737A63CD88", x => x.idMovimiento);
                    table.ForeignKey(
                        name: "FK__Movimient__idMat__0A9D95DB",
                        column: x => x.idMateriaPrima,
                        principalTable: "MateriaPrima",
                        principalColumn: "idMateriaPrima");
                    table.ForeignKey(
                        name: "FK__Movimient__idVen__09A971A2",
                        column: x => x.idVenta,
                        principalTable: "Venta",
                        principalColumn: "idVenta");
                });

            migrationBuilder.CreateTable(
                name: "Pago",
                columns: table => new
                {
                    idPago = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fechaPago = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    monto = table.Column<double>(type: "float", nullable: false),
                    metodoPago = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    idVenta = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Pago__BD2295ADFF6693D1", x => x.idPago);
                    table.ForeignKey(
                        name: "FK__Pago__idVenta__18EBB532",
                        column: x => x.idVenta,
                        principalTable: "Venta",
                        principalColumn: "idVenta");
                });

            migrationBuilder.CreateTable(
                name: "SolicitudProduccion",
                columns: table => new
                {
                    idSolicitud = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fechaSolicitud = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    estatus = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    idVenta = table.Column<int>(type: "int", nullable: false),
                    idUsuario = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Solicitu__D801DDB8417435EB", x => x.idSolicitud);
                    table.ForeignKey(
                        name: "FK__Solicitud__idUsu__114A936A",
                        column: x => x.idUsuario,
                        principalTable: "Usuario",
                        principalColumn: "idUsuario",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__Solicitud__idVen__1234ABCD",
                        column: x => x.idVenta,
                        principalTable: "Venta",
                        principalColumn: "idVenta",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Merma",
                columns: table => new
                {
                    idMerma = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cantidadMerma = table.Column<double>(type: "float", nullable: false),
                    fechaMerma = table.Column<DateTime>(type: "datetime", nullable: true),
                    descripcion = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false, defaultValue: ""),
                    idProducto = table.Column<int>(type: "int", nullable: true),
                    idDetalle_producto = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Merma__248B3BCB1CE57DA7", x => x.idMerma);
                    table.ForeignKey(
                        name: "FK__Merma__idDetalle__6B24EA82",
                        column: x => x.idDetalle_producto,
                        principalTable: "Detalle_producto",
                        principalColumn: "idDetalle_producto");
                    table.ForeignKey(
                        name: "FK__Merma__idProduct__6A30C649",
                        column: x => x.idProducto,
                        principalTable: "Producto",
                        principalColumn: "idProducto");
                });

            migrationBuilder.CreateTable(
                name: "Detalle_receta",
                columns: table => new
                {
                    idDetalle_receta = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cantidadMateriaPrima = table.Column<int>(type: "int", nullable: false),
                    medidaIngrediente = table.Column<int>(type: "int", nullable: false),
                    idMateriaPrima = table.Column<int>(type: "int", nullable: true),
                    idReceta = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Detalle___1629BEDE55F6928B", x => x.idDetalle_receta);
                    table.ForeignKey(
                        name: "FK__Detalle_r__idMat__787EE5A0",
                        column: x => x.idMateriaPrima,
                        principalTable: "MateriaPrima",
                        principalColumn: "idMateriaPrima");
                    table.ForeignKey(
                        name: "FK__Detalle_r__idRec__797309D9",
                        column: x => x.idReceta,
                        principalTable: "Receta",
                        principalColumn: "idReceta");
                });

            migrationBuilder.CreateTable(
                name: "Compra",
                columns: table => new
                {
                    idCompra = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idMateriaPrima = table.Column<int>(type: "int", nullable: true),
                    idDetalle_materia_prima = table.Column<int>(type: "int", nullable: true),
                    cantidadComprada = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Compra__48B99DB7DE15CCD8", x => x.idCompra);
                    table.ForeignKey(
                        name: "FK__Compra__idDetall__59FA5E80",
                        column: x => x.idDetalle_materia_prima,
                        principalTable: "Detalle_materia_prima",
                        principalColumn: "idDetalle_materia_prima");
                    table.ForeignKey(
                        name: "FK__Compra__idMateri__59063A47",
                        column: x => x.idMateriaPrima,
                        principalTable: "MateriaPrima",
                        principalColumn: "idMateriaPrima");
                });

            migrationBuilder.CreateTable(
                name: "merma_inventario",
                columns: table => new
                {
                    idMerma = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cantidadMerma = table.Column<double>(type: "float", nullable: false),
                    fechaMerma = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    idMateriaPrima = table.Column<int>(type: "int", nullable: true),
                    idDetalle_materia_prima = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__merma_in__248B3BCB5993AD91", x => x.idMerma);
                    table.ForeignKey(
                        name: "FK__merma_inv__idDet__70DDC3D8",
                        column: x => x.idDetalle_materia_prima,
                        principalTable: "Detalle_materia_prima",
                        principalColumn: "idDetalle_materia_prima");
                    table.ForeignKey(
                        name: "FK__merma_inv__idMat__6FE99F9F",
                        column: x => x.idMateriaPrima,
                        principalTable: "MateriaPrima",
                        principalColumn: "idMateriaPrima");
                });

            migrationBuilder.CreateTable(
                name: "Efectivo",
                columns: table => new
                {
                    idEfectivo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    montoRecibido = table.Column<double>(type: "float", nullable: false),
                    cambioDevuelto = table.Column<double>(type: "float", nullable: false),
                    idPago = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Efectivo__A7ACA7323C1AF00D", x => x.idEfectivo);
                    table.ForeignKey(
                        name: "FK__Efectivo__idPago__208CD6FA",
                        column: x => x.idPago,
                        principalTable: "Pago",
                        principalColumn: "idPago");
                });

            migrationBuilder.CreateTable(
                name: "Tarjeta",
                columns: table => new
                {
                    idTarjeta = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    numeroTarjeta = table.Column<string>(type: "varchar(16)", unicode: false, maxLength: 16, nullable: false),
                    nombreTitular = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    fechaExpiracion = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: false),
                    cvv = table.Column<string>(type: "varchar(4)", unicode: false, maxLength: 4, nullable: false),
                    idPago = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Tarjeta__C456D538C4D469F2", x => x.idTarjeta);
                    table.ForeignKey(
                        name: "FK__Tarjeta__idPago__1BC821DD",
                        column: x => x.idPago,
                        principalTable: "Pago",
                        principalColumn: "idPago");
                });

            migrationBuilder.CreateTable(
                name: "detalle_solicitud",
                columns: table => new
                {
                    id_detalle_solicitud = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idSolicitud = table.Column<int>(type: "int", nullable: true),
                    fechaInicio = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    fechaFin = table.Column<DateTime>(type: "datetime", nullable: true),
                    idUsuario = table.Column<int>(type: "int", nullable: true),
                    estatus = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    numeroPaso = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__detalle___F5221E0861D951BC", x => x.id_detalle_solicitud);
                    table.ForeignKey(
                        name: "FK__detalle_s__idSol__30C33EC3",
                        column: x => x.idSolicitud,
                        principalTable: "SolicitudProduccion",
                        principalColumn: "idSolicitud");
                    table.ForeignKey(
                        name: "FK__detalle_s__idUsu__31B762FC",
                        column: x => x.idUsuario,
                        principalTable: "Usuario",
                        principalColumn: "idUsuario");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Compra_idDetalle_materia_prima",
                table: "Compra",
                column: "idDetalle_materia_prima");

            migrationBuilder.CreateIndex(
                name: "IX_Compra_idMateriaPrima",
                table: "Compra",
                column: "idMateriaPrima");

            migrationBuilder.CreateIndex(
                name: "IX_Detalle_materia_prima_idMateriaPrima",
                table: "Detalle_materia_prima",
                column: "idMateriaPrima");

            migrationBuilder.CreateIndex(
                name: "IX_Detalle_producto_idProducto",
                table: "Detalle_producto",
                column: "idProducto");

            migrationBuilder.CreateIndex(
                name: "IX_Detalle_receta_idMateriaPrima",
                table: "Detalle_receta",
                column: "idMateriaPrima");

            migrationBuilder.CreateIndex(
                name: "IX_Detalle_receta_idReceta",
                table: "Detalle_receta",
                column: "idReceta");

            migrationBuilder.CreateIndex(
                name: "IX_detalle_solicitud_idSolicitud",
                table: "detalle_solicitud",
                column: "idSolicitud");

            migrationBuilder.CreateIndex(
                name: "IX_detalle_solicitud_idUsuario",
                table: "detalle_solicitud",
                column: "idUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleVenta_idProducto",
                table: "DetalleVenta",
                column: "idProducto");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleVenta_idVenta",
                table: "DetalleVenta",
                column: "idVenta");

            migrationBuilder.CreateIndex(
                name: "IX_Efectivo_idPago",
                table: "Efectivo",
                column: "idPago");

            migrationBuilder.CreateIndex(
                name: "IX_Envio_idPaqueteria",
                table: "Envio",
                column: "idPaqueteria");

            migrationBuilder.CreateIndex(
                name: "IX_Envio_idVenta",
                table: "Envio",
                column: "idVenta");

            migrationBuilder.CreateIndex(
                name: "IX_MateriaPrima_idMedida",
                table: "MateriaPrima",
                column: "idMedida");

            migrationBuilder.CreateIndex(
                name: "IX_MateriaPrima_idProveedor",
                table: "MateriaPrima",
                column: "idProveedor");

            migrationBuilder.CreateIndex(
                name: "IX_Merma_idDetalle_producto",
                table: "Merma",
                column: "idDetalle_producto");

            migrationBuilder.CreateIndex(
                name: "IX_Merma_idProducto",
                table: "Merma",
                column: "idProducto");

            migrationBuilder.CreateIndex(
                name: "IX_merma_inventario_idDetalle_materia_prima",
                table: "merma_inventario",
                column: "idDetalle_materia_prima");

            migrationBuilder.CreateIndex(
                name: "IX_merma_inventario_idMateriaPrima",
                table: "merma_inventario",
                column: "idMateriaPrima");

            migrationBuilder.CreateIndex(
                name: "IX_Movimiento_idMateriaPrima",
                table: "Movimiento",
                column: "idMateriaPrima");

            migrationBuilder.CreateIndex(
                name: "IX_Movimiento_idVenta",
                table: "Movimiento",
                column: "idVenta");

            migrationBuilder.CreateIndex(
                name: "IX_Pago_idVenta",
                table: "Pago",
                column: "idVenta");

            migrationBuilder.CreateIndex(
                name: "IX_PasoReceta_idProducto",
                table: "PasoReceta",
                column: "idProducto");

            migrationBuilder.CreateIndex(
                name: "IX_Producto_idMedida",
                table: "Producto",
                column: "idMedida");

            migrationBuilder.CreateIndex(
                name: "IX_RankingClientes_IdUsuario",
                table: "RankingClientes",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Receta_idMedida",
                table: "Receta",
                column: "idMedida");

            migrationBuilder.CreateIndex(
                name: "IX_Receta_idProducto",
                table: "Receta",
                column: "idProducto");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudProduccion_idUsuario",
                table: "SolicitudProduccion",
                column: "idUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudProduccion_idVenta",
                table: "SolicitudProduccion",
                column: "idVenta");

            migrationBuilder.CreateIndex(
                name: "IX_Tarjeta_idPago",
                table: "Tarjeta",
                column: "idPago");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_AgenteIdUsuario",
                table: "Usuario",
                column: "AgenteIdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_idDireccion",
                table: "Usuario",
                column: "idDireccion",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Venta_IdUsuario",
                table: "Venta",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_VentasPorProductoPeriodo_ProductoId",
                table: "VentasPorProductoPeriodo",
                column: "ProductoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Compra");

            migrationBuilder.DropTable(
                name: "Detalle_receta");

            migrationBuilder.DropTable(
                name: "detalle_solicitud");

            migrationBuilder.DropTable(
                name: "DetalleVenta");

            migrationBuilder.DropTable(
                name: "Efectivo");

            migrationBuilder.DropTable(
                name: "Envio");

            migrationBuilder.DropTable(
                name: "InventarioReporte");

            migrationBuilder.DropTable(
                name: "LogsUser");

            migrationBuilder.DropTable(
                name: "Merma");

            migrationBuilder.DropTable(
                name: "merma_inventario");

            migrationBuilder.DropTable(
                name: "Movimiento");

            migrationBuilder.DropTable(
                name: "PasoReceta");

            migrationBuilder.DropTable(
                name: "RankingClientes");

            migrationBuilder.DropTable(
                name: "Tarjeta");

            migrationBuilder.DropTable(
                name: "VentasPorProductoPeriodo");

            migrationBuilder.DropTable(
                name: "Receta");

            migrationBuilder.DropTable(
                name: "SolicitudProduccion");

            migrationBuilder.DropTable(
                name: "Paqueteria");

            migrationBuilder.DropTable(
                name: "Detalle_producto");

            migrationBuilder.DropTable(
                name: "Detalle_materia_prima");

            migrationBuilder.DropTable(
                name: "Pago");

            migrationBuilder.DropTable(
                name: "Producto");

            migrationBuilder.DropTable(
                name: "MateriaPrima");

            migrationBuilder.DropTable(
                name: "Venta");

            migrationBuilder.DropTable(
                name: "Medida");

            migrationBuilder.DropTable(
                name: "Proveedor");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Direccion");
        }
    }
}
