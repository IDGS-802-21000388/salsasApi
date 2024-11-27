using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SalsasAPI.Models;

public partial class SalsaContext : DbContext
{
    public SalsaContext()
    {
    }

    public SalsaContext(DbContextOptions<SalsaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Compra> Compras { get; set; }
    public virtual DbSet<EnvioDetalleWeb> EnvioDetallesWeb { get; set; }

    public virtual DbSet<DetalleMateriaPrima> DetalleMateriaPrimas { get; set; }

    public virtual DbSet<DetalleProducto> DetalleProductos { get; set; }

    public virtual DbSet<DetalleRecetum> DetalleReceta { get; set; }

    public virtual DbSet<DetalleSolicitud> DetalleSolicituds { get; set; }

    public virtual DbSet<DetalleVentum> DetalleVenta { get; set; }

    public virtual DbSet<Efectivo> Efectivos { get; set; }

    public virtual DbSet<Envio> Envios { get; set; }

    public virtual DbSet<LogsUser> LogsUsers { get; set; }

    public virtual DbSet<MateriaPrima> MateriaPrimas { get; set; }

    public virtual DbSet<Medidum> Medida { get; set; }

    public virtual DbSet<Merma> Mermas { get; set; }

    public virtual DbSet<MermaInventario> MermaInventarios { get; set; }

    public virtual DbSet<Movimiento> Movimientos { get; set; }

    public virtual DbSet<Pago> Pagos { get; set; }

    public virtual DbSet<Paqueterium> Paqueteria { get; set; }

    public virtual DbSet<PasoReceta> PasoReceta { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Proveedor> Proveedors { get; set; }

    public virtual DbSet<Testimonio> Testimonios { get; set; }


    public virtual DbSet<Recetum> Receta { get; set; }

    public virtual DbSet<SolicitudProduccion> SolicitudProduccions { get; set; }

    public virtual DbSet<Tarjetum> Tarjeta { get; set; }
    public virtual DbSet<Direccion> Direccion { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Ventum> Venta { get; set; }

    public virtual DbSet<VentasPorProductoPeriodo> VentasPorProductoPeriodos { get; set; }

    public virtual DbSet<EnvioDetalle> EnvioDetalles { get; set; }
    public virtual DbSet<vw_Detalle_Receta> vw_Detalle_Receta { get; set; }
    public virtual DbSet<vw_Producto_Detalle> vw_Producto_Detalle { get; set; }
    public virtual DbSet<vw_MateriaPrima_Detalle> vw_MateriaPrima_Detalle { get; set; }

    public virtual DbSet<InventarioReporte> InventarioReporte { get; set; }
    public virtual DbSet<RankingClientes> RankingClientes { get; set; }

    public virtual DbSet<AgentesVenta> AgentesVenta { get; set; }
     
    public virtual DbSet<EncuestaSatisfaccion> EncuestaSatisfaccion { get; set; }

    public virtual DbSet<EmailMessage> EmailMessages { get; set; }


    public virtual DbSet<Queja> Quejas { get; set; }

    public virtual DbSet<Cotizaciones> Cotizaciones { get; set; }
    public virtual DbSet<DetalleCotizacion> DetalleCotizaciones { get; set; }
    public virtual DbSet<vw_Cotizacion> VistaCotizaciones { get; set; }
    public virtual DbSet<CodigoDescuento> CodigosDescuento { get; set; }
    public virtual DbSet<UsuarioCodigoDescuento> UsuarioCodigoDescuento { get; set; }



    public DbSet<Empresa> Empresa { get; set; }
    public DbSet<EmpresaUsuario> EmpresaUsuario { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Relación entre Empresa y EmpresaUsuario
        modelBuilder.Entity<EmpresaUsuario>()
            .HasOne<Empresa>()                        // Relación con la entidad Empresa
            .WithMany()                               // No es necesario definir una colección en Empresa
            .HasForeignKey(eu => eu.IdEmpresa)        // Clave foránea en EmpresaUsuario
            .OnDelete(DeleteBehavior.Restrict);      // Evitar eliminación en cascada

        // Relación entre Usuario y EmpresaUsuario
        modelBuilder.Entity<EmpresaUsuario>()
            .HasOne<Usuario>()                       // Relación con la entidad Usuario
            .WithMany()                               // No es necesario definir una colección en Usuario
            .HasForeignKey(eu => eu.IdUsuario)       // Clave foránea en EmpresaUsuario
            .OnDelete(DeleteBehavior.Restrict);      // Evitar eliminación en cascada

        // Configurar la clave primaria para la tabla Empresa
        modelBuilder.Entity<Empresa>()
            .HasKey(e => e.idEmpresa);

        // Configurar la clave primaria para la tabla EmpresaUsuario
        modelBuilder.Entity<EmpresaUsuario>()
            .HasKey(eu => eu.IdEmpresaUsuario);

        modelBuilder.Entity<Empresa>()
        .HasOne(e => e.Direccion)
        .WithMany() // O Many-to-Many si hay una relación inversa
        .HasForeignKey(e => e.idDireccion);  // La clave foránea en la tabla Empresa


        modelBuilder.Entity<EnvioDetalleWeb>(entity =>
        {
            entity.HasNoKey(); // Las vistas no tienen clave primaria
            entity.ToView("vw_Envio_DetallesWeb"); // Nombre de la vista
        });
        modelBuilder.Entity<Compra>(entity =>
        {
            entity.HasKey(e => e.IdCompra).HasName("PK__Compra__48B99DB7DE15CCD8");

            entity.ToTable("Compra");

            entity.Property(e => e.IdCompra).HasColumnName("idCompra");
            entity.Property(e => e.cantidadComprada).HasColumnName("cantidadComprada");
            entity.Property(e => e.IdDetalleMateriaPrima).HasColumnName("idDetalle_materia_prima");
            entity.Property(e => e.IdMateriaPrima).HasColumnName("idMateriaPrima");

            entity.HasOne(d => d.IdDetalleMateriaPrimaNavigation).WithMany(p => p.Compras)
                .HasForeignKey(d => d.IdDetalleMateriaPrima)
                .HasConstraintName("FK__Compra__idDetall__59FA5E80");

            entity.HasOne(d => d.IdMateriaPrimaNavigation).WithMany(p => p.Compras)
                .HasForeignKey(d => d.IdMateriaPrima)
                .HasConstraintName("FK__Compra__idMateri__59063A47");
        });

        modelBuilder.Entity<DetalleMateriaPrima>(entity =>
        {
            entity.HasKey(e => e.idDetalleMateriaPrima).HasName("PK__Detalle___451468AC4075AEB9");

            entity.ToTable("Detalle_materia_prima");

            entity.Property(e => e.idDetalleMateriaPrima).HasColumnName("idDetalle_materia_prima");
            entity.Property(e => e.cantidadExistentes).HasColumnName("cantidadExistentes");
            entity.Property(e => e.estatus)
                .HasDefaultValue(1)
                .HasColumnName("estatus");
            entity.Property(e => e.fechaCompra)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaCompra");
            entity.Property(e => e.fechaVencimiento)
                .HasColumnType("datetime")
                .HasColumnName("fechaVencimiento");
            entity.Property(e => e.idMateriaPrima).HasColumnName("idMateriaPrima");
            entity.Property(e => e.porcentaje)
                .HasDefaultValue(100)
                .HasColumnName("porcentaje");

            entity.HasOne(d => d.IdMateriaPrimaNavigation).WithMany(p => p.DetalleMateriaPrimas)
                .HasForeignKey(d => d.idMateriaPrima)
                .HasConstraintName("FK__Detalle_m__idMat__5535A963");
        });

        modelBuilder.Entity<DetalleProducto>(entity =>
        {
            entity.HasKey(e => e.IdDetalleProducto).HasName("PK__Detalle___505159E1638AC0C9");

            entity.ToTable("Detalle_producto");

            entity.Property(e => e.IdDetalleProducto).HasColumnName("idDetalle_producto");
            entity.Property(e => e.CantidadExistentes).HasColumnName("cantidadExistentes");
            entity.Property(e => e.Estatus)
                .HasDefaultValue(true)
                .HasColumnName("estatus");
            entity.Property(e => e.FechaVencimiento)
                .HasColumnType("datetime")
                .HasColumnName("fechaVencimiento");
            entity.Property(e => e.IdProducto).HasColumnName("idProducto");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.DetalleProductos)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("FK__Detalle_p__idPro__656C112C");
        });

        modelBuilder.Entity<EncuestaSatisfaccion>(entity =>
        {
            entity.HasKey(e => e.IdEncuesta).HasName("PK_EncuestaSatisfaccion");  // Nombre de la clave primaria

            entity.ToTable("EncuestaSatisfaccion");  // Nombre de la tabla en la base de datos

            entity.Property(e => e.IdEncuesta).HasColumnName("idEncuesta");
            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.IdVenta).HasColumnName("idVenta");

            entity.Property(e => e.ProcesoCompra).HasColumnName("procesoCompra");
            entity.Property(e => e.SaborProducto).HasColumnName("saborProducto");
            entity.Property(e => e.EntregaProducto).HasColumnName("entregaProducto");
            entity.Property(e => e.PresentacionProducto).HasColumnName("presentacionProducto");
            entity.Property(e => e.FacilidadUsoPagina).HasColumnName("facilidadUsoPagina");
            entity.Property(e => e.FechaEncuesta).HasColumnName("fechaEncuesta").HasColumnType("date");

            entity.HasOne(e => e.Usuario)  // Relación con la entidad Usuario
                  .WithMany()
                  .HasForeignKey(e => e.IdUsuario)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK_EncuestaSatisfaccion_Usuario"); ;

            entity.HasOne(e => e.Venta)  // Relación con la entidad Venta
                  .WithMany()
                  .HasForeignKey(e => e.IdVenta)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK_EncuestaSatisfaccion_Venta"); ;
        });


        // Configuración de la tabla VentasPorProductoPeriodo
        modelBuilder.Entity<VentasPorProductoPeriodo>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_VentasPorProductoPeriodo");

                entity.ToTable("VentasPorProductoPeriodo");

                entity.Property(e => e.Id).HasColumnName("Id");
                entity.Property(e => e.ProductoId).HasColumnName("ProductoId");
                entity.Property(e => e.NombreProducto).HasColumnName("NombreProducto").HasMaxLength(50);
                entity.Property(e => e.PeriodoInicio).HasColumnName("PeriodoInicio").HasColumnType("date");
                entity.Property(e => e.PeriodoFin).HasColumnName("PeriodoFin").HasColumnType("date");
                entity.Property(e => e.NumeroVentas).HasColumnName("NumeroVentas");
                entity.Property(e => e.CantidadVendida).HasColumnName("CantidadVendida");
                entity.Property(e => e.TotalRecaudado).HasColumnName("TotalRecaudado");
                entity.Property(e => e.IndicadorGlobal).HasColumnName("IndicadorGlobal");

                entity.HasOne(d => d.Producto)
                    .WithMany(p => p.VentasPorProductoPeriodos)
                    .HasForeignKey(d => d.ProductoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VentasPorProductoPeriodo_Producto");
            });

        modelBuilder.Entity<DetalleRecetum>(entity =>
        {
            entity.HasKey(e => e.IdDetalleReceta).HasName("PK__Detalle___1629BEDE55F6928B");

            entity.ToTable("Detalle_receta");

            entity.Property(e => e.IdDetalleReceta).HasColumnName("idDetalle_receta");
            entity.Property(e => e.CantidadMateriaPrima).HasColumnName("cantidadMateriaPrima");
            entity.Property(e => e.MedidaIngrediente).HasColumnName("medidaIngrediente");
            entity.Property(e => e.IdMateriaPrima).HasColumnName("idMateriaPrima");
            entity.Property(e => e.IdReceta).HasColumnName("idReceta");

            entity.HasOne(d => d.IdMateriaPrimaNavigation).WithMany(p => p.DetalleReceta)
                .HasForeignKey(d => d.IdMateriaPrima)
                .HasConstraintName("FK__Detalle_r__idMat__787EE5A0");

            entity.HasOne(d => d.IdRecetaNavigation).WithMany(p => p.DetalleReceta)
                .HasForeignKey(d => d.IdReceta)
                .HasConstraintName("FK__Detalle_r__idRec__797309D9");
        });

        modelBuilder.Entity<DetalleSolicitud>(entity =>
        {
            entity.HasKey(e => e.IdDetalleSolicitud).HasName("PK__detalle___F5221E0861D951BC");

            entity.ToTable("detalle_solicitud");

            entity.Property(e => e.IdDetalleSolicitud).HasColumnName("id_detalle_solicitud");
            entity.Property(e => e.Estatus)
                .HasDefaultValue(true)
                .HasColumnName("estatus");
            entity.Property(e => e.FechaFin)
                .HasColumnType("datetime")
                .HasColumnName("fechaFin");
            entity.Property(e => e.FechaInicio)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaInicio");
            entity.Property(e => e.IdSolicitud).HasColumnName("idSolicitud");
            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.NumeroPaso).HasColumnName("numeroPaso");

            entity.HasOne(d => d.IdSolicitudNavigation).WithMany(p => p.DetalleSolicituds)
                .HasForeignKey(d => d.IdSolicitud)
                .HasConstraintName("FK__detalle_s__idSol__30C33EC3");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.DetalleSolicituds)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK__detalle_s__idUsu__31B762FC");
        });

        modelBuilder.Entity<DetalleVentum>(entity =>
        {
            entity.HasKey(e => e.IdDetalleVenta).HasName("PK__DetalleV__BFE2843F1CD4AC29");

            entity.Property(e => e.IdDetalleVenta).HasColumnName("idDetalleVenta");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.IdProducto).HasColumnName("idProducto");
            entity.Property(e => e.IdVenta).HasColumnName("idVenta");
            entity.Property(e => e.Subtotal).HasColumnName("subtotal");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.DetalleVenta)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("FK__DetalleVe__idPro__02FC7413");

            entity.HasOne(d => d.IdVentaNavigation).WithMany(p => p.DetalleVenta)
                .HasForeignKey(d => d.IdVenta)
                .HasConstraintName("FK__DetalleVe__idVen__02084FDA");
        });

        modelBuilder.Entity<Efectivo>(entity =>
        {
            entity.HasKey(e => e.IdEfectivo).HasName("PK__Efectivo__A7ACA7323C1AF00D");

            entity.ToTable("Efectivo");

            entity.Property(e => e.IdEfectivo).HasColumnName("idEfectivo");
            entity.Property(e => e.CambioDevuelto).HasColumnName("cambioDevuelto");
            entity.Property(e => e.IdPago).HasColumnName("idPago");
            entity.Property(e => e.MontoRecibido).HasColumnName("montoRecibido");

            entity.HasOne(d => d.IdPagoNavigation).WithMany(p => p.Efectivos)
                .HasForeignKey(d => d.IdPago)
                .HasConstraintName("FK__Efectivo__idPago__208CD6FA");
        });

        modelBuilder.Entity<Envio>(entity =>
        {
            entity.HasKey(e => e.IdEnvio).HasName("PK__Envio__527F831F0C350BBA");

            entity.ToTable("Envio");

            entity.Property(e => e.IdEnvio).HasColumnName("idEnvio");
            entity.Property(e => e.Estatus)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("pendiente")
                .HasColumnName("estatus");
            entity.Property(e => e.FechaEntregaEstimada)
                .HasColumnType("datetime")
                .HasColumnName("fechaEntregaEstimada");
            entity.Property(e => e.FechaEntregaReal)
                .HasColumnType("datetime")
                .HasColumnName("fechaEntregaReal");
            entity.Property(e => e.FechaEnvio)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaEnvio");
            entity.Property(e => e.IdPaqueteria).HasColumnName("idPaqueteria");
            entity.Property(e => e.IdVenta).HasColumnName("idVenta");

            entity.HasOne(d => d.IdPaqueteriaNavigation).WithMany(p => p.Envios)
                .HasForeignKey(d => d.IdPaqueteria)
                .HasConstraintName("FK__Envio__idPaquete__2BFE89A6");

            entity.HasOne(d => d.IdVentaNavigation).WithMany(p => p.Envios)
                .HasForeignKey(d => d.IdVenta)
                .HasConstraintName("FK__Envio__idVenta__2B0A656D");
        });

        modelBuilder.Entity<LogsUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__LogsUser__3213E83F1B07DC4C");

            entity.ToTable("LogsUser");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.LastDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("lastDate");
            entity.Property(e => e.Procedimiento)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("procedimiento");
        });

        modelBuilder.Entity<MateriaPrima>(entity =>
        {
            entity.HasKey(e => e.IdMateriaPrima).HasName("PK__MateriaP__97EB058FD45C14F1");

            entity.ToTable("MateriaPrima");

            entity.Property(e => e.IdMateriaPrima).HasColumnName("idMateriaPrima");
            entity.Property(e => e.IdMedida).HasColumnName("idMedida");
            entity.Property(e => e.IdProveedor).HasColumnName("idProveedor");
            entity.Property(e => e.NombreMateria)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasDefaultValue("")
                .HasColumnName("nombreMateria");
            entity.Property(e => e.PrecioCompra).HasColumnName("precioCompra");

            entity.HasOne(d => d.IdMedidaNavigation).WithMany(p => p.MateriaPrimas)
                .HasForeignKey(d => d.IdMedida)
                .HasConstraintName("FK__MateriaPr__idMed__4D94879B");

            entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.MateriaPrimas)
                .HasForeignKey(d => d.IdProveedor)
                .HasConstraintName("FK__MateriaPr__idPro__4E88ABD4");
        });

        modelBuilder.Entity<Medidum>(entity =>
        {
            entity.HasKey(e => e.IdMedida).HasName("PK__Medida__4E0391E8A3E17875");

            entity.Property(e => e.IdMedida).HasColumnName("idMedida");
            entity.Property(e => e.TipoMedida)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("tipoMedida");
        });

        modelBuilder.Entity<Merma>(entity =>
        {
            entity.HasKey(e => e.IdMerma).HasName("PK__Merma__248B3BCB1CE57DA7");

            entity.ToTable("Merma");

            entity.Property(e => e.IdMerma).HasColumnName("idMerma");
            entity.Property(e => e.CantidadMerma).HasColumnName("cantidadMerma");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasDefaultValue("")
                .HasColumnName("descripcion");
            entity.Property(e => e.FechaMerma)
                .HasColumnType("datetime")
                .HasColumnName("fechaMerma");
            entity.Property(e => e.IdDetalleProducto).HasColumnName("idDetalle_producto");
            entity.Property(e => e.IdProducto).HasColumnName("idProducto");

            entity.HasOne(d => d.IdDetalleProductoNavigation).WithMany(p => p.Mermas)
                .HasForeignKey(d => d.IdDetalleProducto)
                .HasConstraintName("FK__Merma__idDetalle__6B24EA82");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.Mermas)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("FK__Merma__idProduct__6A30C649");
        });

        modelBuilder.Entity<MermaInventario>(entity =>
        {
            entity.HasKey(e => e.IdMerma).HasName("PK__merma_in__248B3BCB5993AD91");

            entity.ToTable("merma_inventario");

            entity.Property(e => e.IdMerma).HasColumnName("idMerma");
            entity.Property(e => e.CantidadMerma).HasColumnName("cantidadMerma");
            entity.Property(e => e.FechaMerma)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaMerma");
            entity.Property(e => e.IdDetalleMateriaPrima).HasColumnName("idDetalle_materia_prima");
            entity.Property(e => e.IdMateriaPrima).HasColumnName("idMateriaPrima");

            entity.HasOne(d => d.IdDetalleMateriaPrimaNavigation).WithMany(p => p.MermaInventarios)
                .HasForeignKey(d => d.IdDetalleMateriaPrima)
                .HasConstraintName("FK__merma_inv__idDet__70DDC3D8");

            entity.HasOne(d => d.IdMateriaPrimaNavigation).WithMany(p => p.MermaInventarios)
                .HasForeignKey(d => d.IdMateriaPrima)
                .HasConstraintName("FK__merma_inv__idMat__6FE99F9F");
        });

        modelBuilder.Entity<Movimiento>(entity =>
        {
            entity.HasKey(e => e.IdMovimiento).HasName("PK__Movimien__628521737A63CD88");

            entity.ToTable("Movimiento");

            entity.Property(e => e.IdMovimiento).HasColumnName("idMovimiento");
            entity.Property(e => e.FechaMovimiento)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaMovimiento");
            entity.Property(e => e.IdMateriaPrima).HasColumnName("idMateriaPrima");
            entity.Property(e => e.IdVenta).HasColumnName("idVenta");
            entity.Property(e => e.Monto).HasColumnName("monto");
            entity.Property(e => e.TipoMovimiento)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasDefaultValue("")
                .HasColumnName("tipoMovimiento");

            entity.HasOne(d => d.IdMateriaPrimaNavigation).WithMany(p => p.Movimientos)
                .HasForeignKey(d => d.IdMateriaPrima)
                .HasConstraintName("FK__Movimient__idMat__0A9D95DB");

            entity.HasOne(d => d.IdVentaNavigation).WithMany(p => p.Movimientos)
                .HasForeignKey(d => d.IdVenta)
                .HasConstraintName("FK__Movimient__idVen__09A971A2");
        });

        modelBuilder.Entity<Pago>(entity =>
        {
            entity.HasKey(e => e.IdPago).HasName("PK__Pago__BD2295ADFF6693D1");

            entity.ToTable("Pago");

            entity.Property(e => e.IdPago).HasColumnName("idPago");
            entity.Property(e => e.FechaPago)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaPago");
            entity.Property(e => e.IdVenta).HasColumnName("idVenta");
            entity.Property(e => e.MetodoPago)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("metodoPago");
            entity.Property(e => e.Monto).HasColumnName("monto");

            entity.HasOne(d => d.IdVentaNavigation).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.IdVenta)
                .HasConstraintName("FK__Pago__idVenta__18EBB532");
        });

        modelBuilder.Entity<Paqueterium>(entity =>
        {
            entity.HasKey(e => e.IdPaqueteria).HasName("PK__Paqueter__90A13AAB7556F2B1");

            entity.Property(e => e.IdPaqueteria).HasColumnName("idPaqueteria");
            entity.Property(e => e.Direccion)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasDefaultValue("")
                .HasColumnName("direccion");
            entity.Property(e => e.Estatus)
                .HasDefaultValue(1)
                .HasColumnName("estatus");
            entity.Property(e => e.NombrePaqueteria)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasDefaultValue("")
                .HasColumnName("nombrePaqueteria");
            entity.Property(e => e.Telefono)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasDefaultValue("")
                .HasColumnName("telefono");
        });

        modelBuilder.Entity<PasoReceta>(entity =>
        {
            entity.HasKey(e => e.IdPasoReceta).HasName("PK__PasoRece__F29634850B24BD79");

            entity.Property(e => e.IdPasoReceta).HasColumnName("idPasoReceta");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.IdProducto).HasColumnName("idProducto");
            entity.Property(e => e.Paso).HasColumnName("paso");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.PasoReceta)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("FK__PasoRecet__idPro__14270015");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IdProducto).HasName("PK__Producto__07F4A1321305AC9C");

            entity.ToTable("Producto");

            entity.Property(e => e.IdProducto).HasColumnName("idProducto");
            entity.Property(e => e.Estatus)
                .HasDefaultValue(true)
                .HasColumnName("estatus");
            entity.Property(e => e.Fotografia)
                .HasColumnType("text")
                .HasColumnName("fotografia");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.IdMedida).HasColumnName("idMedida");
            entity.Property(e => e.NombreProducto)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValue("")
                .HasColumnName("nombreProducto");
            entity.Property(e => e.PrecioProduccion).HasColumnName("precioProduccion");
            entity.Property(e => e.PrecioVenta).HasColumnName("precioVenta");

            entity.HasOne(d => d.IdMedidaNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdMedida)
                .HasConstraintName("FK__Producto__idMedi__60A75C0F");
        });

        modelBuilder.Entity<Proveedor>(entity =>
        {
            entity.HasKey(e => e.IdProveedor).HasName("PK__Proveedo__A3FA8E6B5E0DDC30");

            entity.ToTable("Proveedor");

            entity.Property(e => e.IdProveedor).HasColumnName("idProveedor");
            entity.Property(e => e.Direccion)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasDefaultValue("")
                .HasColumnName("direccion");
            entity.Property(e => e.Estatus)
                .HasDefaultValue(1)
                .HasColumnName("estatus");
            entity.Property(e => e.NombreAtiende)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasDefaultValue("")
                .HasColumnName("nombreAtiende");
            entity.Property(e => e.NombreProveedor)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasDefaultValue("")
                .HasColumnName("nombreProveedor");
            entity.Property(e => e.Telefono)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasDefaultValue("")
                .HasColumnName("telefono");
        });

        modelBuilder.Entity<Recetum>(entity =>
        {
            entity.HasKey(e => e.IdReceta).HasName("PK__Receta__7D03FC81C76DEC79");

            entity.Property(e => e.IdReceta).HasColumnName("idReceta");
            entity.Property(e => e.IdMedida).HasColumnName("idMedida");
            entity.Property(e => e.IdProducto).HasColumnName("idProducto");

            entity.HasOne(d => d.IdMedidaNavigation).WithMany(p => p.Receta)
                .HasForeignKey(d => d.IdMedida)
                .HasConstraintName("FK__Receta__idMedida__73BA3083");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.Receta)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("FK__Receta__idProduc__74AE54BC");
        });


        modelBuilder.Entity<SolicitudProduccion>(entity =>
        {
            entity.HasKey(e => e.IdSolicitud).HasName("PK__Solicitu__D801DDB8417435EB");
            entity.ToTable("SolicitudProduccion");

            entity.Property(e => e.IdSolicitud).HasColumnName("idSolicitud");

            entity.Property(e => e.Estatus)
                .HasDefaultValue(1)
                .HasColumnName("estatus");

            entity.Property(e => e.FechaSolicitud)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaSolicitud");

            entity.Property(e => e.IdVenta).HasColumnName("idVenta");

            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.IdVenta).HasColumnName("idVenta");

            entity.HasOne(d => d.IdVentaNavigation).WithMany(p => p.SolicitudProduccions)
                .HasForeignKey(d => d.IdVenta)
                .HasConstraintName("FK__Solicitud__idVen__114A936A");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.SolicitudProduccions)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK__Solicitud__idUsu__114A936A");

            entity.HasOne(d => d.IdVentaNavigation)
                .WithMany(p => p.SolicitudProduccions)
                .HasForeignKey(d => d.IdVenta)
                .HasConstraintName("FK__Solicitud__idVen__1234ABCD");
        });




        modelBuilder.Entity<Tarjetum>(entity =>
        {
            entity.HasKey(e => e.IdTarjeta).HasName("PK__Tarjeta__C456D538C4D469F2");

            entity.Property(e => e.IdTarjeta).HasColumnName("idTarjeta");
            entity.Property(e => e.Cvv)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("cvv");
            entity.Property(e => e.FechaExpiracion)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("fechaExpiracion");
            entity.Property(e => e.IdPago).HasColumnName("idPago");
            entity.Property(e => e.NombreTitular)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombreTitular");
            entity.Property(e => e.NumeroTarjeta)
                .HasMaxLength(16)
                .IsUnicode(false)
                .HasColumnName("numeroTarjeta");

            entity.HasOne(d => d.IdPagoNavigation).WithMany(p => p.Tarjeta)
                .HasForeignKey(d => d.IdPago)
                .HasConstraintName("FK__Tarjeta__idPago__1BC821DD");
        });

        modelBuilder.Entity<Direccion>(entity =>
        {
            entity.HasKey(e => e.IdDireccion).HasName("PK__Direccion__645723A6D661BB22");

            entity.ToTable("Direccion");

            entity.Property(e => e.IdDireccion).HasColumnName("idDireccion");
            entity.Property(e => e.Estado)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("estado");
            entity.Property(e => e.Municipio)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("municipio");
            entity.Property(e => e.CodigoPostal)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("codigoPostal");
            entity.Property(e => e.Colonia)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("colonia");
            entity.Property(e => e.Calle)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("calle");
            entity.Property(e => e.NumExt)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("numExt");
            entity.Property(e => e.NumInt)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("numInt");
            entity.Property(e => e.Referencia)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("referencia");
        });


        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__645723A6C661BB22");

            entity.ToTable("Usuario");

            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.Correo)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasDefaultValue("")
                .HasColumnName("correo");
            entity.Property(e => e.Contrasenia)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasDefaultValue("")
                .HasColumnName("contrasenia");
            entity.Property(e => e.DateLastToken)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateLastToken");
            entity.Property(e => e.Estatus)
                .HasDefaultValue(1)
                .HasColumnName("estatus");
            entity.Property(e => e.Intentos).HasColumnName("intentos");
            entity.Property(e => e.Nombre)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.NombreUsuario)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasDefaultValue("")
                .HasColumnName("nombreUsuario");
            entity.Property(e => e.Rol)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("rol");
            entity.Property(e => e.Telefono)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasDefaultValue("")
                .HasColumnName("telefono");
            entity.Property(e => e.IdDireccion)
                .HasColumnName("idDireccion");

            entity.HasOne(d => d.Direccion)
                .WithOne()
                .HasForeignKey<Usuario>(d => d.IdDireccion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Usuario_Direccion");
        });

        modelBuilder.Entity<AgentesVenta>(entity =>
        {
            entity.ToTable("AgentesVenta");

            entity.HasKey(e => e.IdAgentesVenta);

            entity.HasOne(e => e.Agente)
                .WithMany()
                .HasForeignKey(e => e.IdAgente)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.Cliente)
                .WithMany()
                .HasForeignKey(e => e.IdCliente)
                .OnDelete(DeleteBehavior.Restrict);
        });



        modelBuilder.Entity<EnvioDetalle>(eb =>
        {
            eb.HasNoKey();
            eb.ToView("vw_Envio_Detalles");
            eb.Property(v => v.IdEnvio).HasColumnName("idEnvio");
            eb.Property(v => v.EstatusPedido).HasColumnName("EstatusPedido");
            eb.Property(v => v.FechaEnvio).HasColumnName("fechaEnvio");
            eb.Property(v => v.FechaEntregaEstimada).HasColumnName("fechaEntregaEstimada");
            eb.Property(v => v.EstatusEnvio).HasColumnName("estatusEnvio");
            eb.Property(v => v.NombrePaqueteria).HasColumnName("nombrePaqueteria");
            eb.Property(v => v.NombreCliente).HasColumnName("nombreCliente");
            eb.Property(v => v.NombreProducto).HasColumnName("nombreProducto");
            eb.Property(v => v.Domicilio).HasColumnName("domicilio");
            eb.Property(v => v.Total).HasColumnName("total");
        });

        modelBuilder.Entity<vw_Detalle_Receta>(eb =>
        {
            eb.HasNoKey();
            eb.ToView("vw_Detalle_Receta_Producto");
            eb.Property(v => v.IdProducto).HasColumnName("idProducto");
            eb.Property(v => v.IdMateriaPrima).HasColumnName("idMateriaPrima");
            eb.Property(v => v.NombreMateria).HasColumnName("nombreMateria");
            eb.Property(v => v.Cantidad).HasColumnName("cantidadMateriaPrima");
            eb.Property(v => v.IdMedida).HasColumnName("idMedida");
            eb.Property(v => v.TipoMedida).HasColumnName("MedidaProducto");
            eb.Property(v => v.IdReceta).HasColumnName("idReceta");
        });

        modelBuilder.Entity<vw_Producto_Detalle>(eb =>
        {
            eb.HasNoKey();
            eb.ToView("vw_Producto_Detalle");
            eb.Property(v => v.IdProducto).HasColumnName("idProducto");
            eb.Property(v => v.PrecioVenta).HasColumnName("precioVenta");
            eb.Property(v => v.PrecioProduccion).HasColumnName("precioProduccion");
            eb.Property(v => v.NombreProducto).HasColumnName("nombreProducto");
            eb.Property(v => v.Cantidad).HasColumnName("cantidad");
            eb.Property(v => v.Stock).HasColumnName("Stock");
            eb.Property(v => v.TipoMedida).HasColumnName("tipoMedida");
            eb.Property(v => v.Fotografia).HasColumnName("fotografia");
            eb.Property(v => v.Estatus).HasColumnName("estatus");
        });

        modelBuilder.Entity<vw_MateriaPrima_Detalle>(eb =>
        {
            eb.HasNoKey();
            eb.ToView("vw_MateriaPrima_Detalle");
            eb.Property(v => v.IdMateriaPrima).HasColumnName("idMateriaPrima");
            eb.Property(v => v.NombreMateria).HasColumnName("nombreMateria");
            eb.Property(v => v.TipoMedida).HasColumnName("tipoMedida");
        });

        // Configuración de la tabla InventarioReporte
        modelBuilder.Entity<InventarioReporte>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_InventarioReporte");

            entity.ToTable("InventarioReporte");

            entity.Property(e => e.Id).HasColumnName("Id");
            entity.Property(e => e.Tipo).HasColumnName("Tipo").HasMaxLength(50);
            entity.Property(e => e.Nombre).HasColumnName("Nombre").HasMaxLength(100);
            entity.Property(e => e.Cantidad).HasColumnName("Cantidad");
            entity.Property(e => e.UltimaActualizacion).HasColumnName("UltimaActualizacion").HasColumnType("datetime");
        });

        modelBuilder.Entity<RankingClientes>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_RankingClientes");

            entity.ToTable("RankingClientes");

            entity.Property(e => e.Id).HasColumnName("Id");
            entity.Property(e => e.IdUsuario).HasColumnName("IdUsuario");
            entity.Property(e => e.NombreUsuario).HasColumnName("NombreUsuario").HasMaxLength(100);
            entity.Property(e => e.ComprasTotales).HasColumnName("ComprasTotales");
            entity.Property(e => e.ProductosComprados).HasColumnName("ProductosComprados").HasColumnType("text");
            entity.Property(e => e.UltimaActualizacion).HasColumnName("UltimaActualizacion").HasColumnType("datetime");

            entity.HasOne(d => d.Usuario)
                .WithMany(p => p.RankingClientes)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK_RankingClientes_Usuario");
        });

        modelBuilder.Entity<EmailMessage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_EmailMessage");

            entity.ToTable("EmailMessage");

            entity.Property(e => e.Email)
                  .IsRequired()
                  .HasMaxLength(255);

            entity.Property(e => e.Mensaje)
                  .IsRequired()
                  .HasColumnType("nvarchar(max)");

            entity.Property(e => e.FechaCreacion)
                  .HasDefaultValueSql("GETDATE()")
                  .HasColumnType("datetime");
        });



        modelBuilder.Entity<Testimonio>(entity =>
        {
            entity.HasKey(e => e.IdTestimonio).HasName("PK_Testimonio");

            entity.ToTable("Testimonio");

            entity.Property(e => e.IdTestimonio)
                  .HasColumnName("idTestimonio");

            entity.Property(e => e.IdUsuario)
                  .HasColumnName("idUsuario");

            entity.Property(e => e.IdProducto)
                  .HasColumnName("idProducto");

            entity.Property(e => e.Calificacion)
                  .HasColumnName("calificacion")
                  .IsRequired(); // Asegúrate de que la calificación sea requerida

            entity.Property(e => e.Comentario)
                  .HasColumnName("comentario")
                  .IsRequired(); // Asegúrate de que el comentario sea requerido

            entity.Property(e => e.FechaTestimonio)
                  .HasColumnType("datetime")
                  .HasDefaultValueSql("(getdate())")
                  .HasColumnName("fechaTestimonio");

            entity.HasOne(d => d.Usuario)
                  .WithMany(p => p.Testimonios)
                  .HasForeignKey(d => d.IdUsuario)
                  .OnDelete(DeleteBehavior.Cascade) // Comportamiento al eliminar un usuario
                  .HasConstraintName("FK_Testimonio_Usuario");

            entity.HasOne(d => d.Producto)
                  .WithMany(p => p.Testimonios)
                  .HasForeignKey(d => d.IdProducto)
                  .OnDelete(DeleteBehavior.Cascade) // Comportamiento al eliminar un producto
                  .HasConstraintName("FK_Testimonio_Producto");
        });

        modelBuilder.Entity<Queja>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Contenido).IsRequired();
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime").IsRequired();
            entity.Property(e => e.Estado).HasMaxLength(50).HasDefaultValue("Nueva");

            entity.HasOne(q => q.Usuario)
                .WithMany(u => u.Quejas)
                .HasForeignKey(q => q.IdUsuario)
                .OnDelete(DeleteBehavior.Cascade);
        });
    
        modelBuilder.Entity<Ventum>(entity =>
        {
            entity.HasKey(e => e.IdVenta).HasName("PK__Venta__077D56146F550EF7");

            entity.Property(e => e.IdVenta).HasColumnName("idVenta");
            entity.Property(e => e.FechaVenta)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaVenta");
            entity.Property(e => e.Total).HasColumnName("total");

            // Configuración de la relación con Usuario
            entity.HasOne(d => d.IdUsuarioNavigation)
                .WithMany(p => p.Ventas)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK_Venta_Usuario");
        });

        // Configuración de la tabla Cotizaciones
        modelBuilder.Entity<Cotizaciones>(entity =>
        {
            entity.HasKey(e => e.IdCotizacion).HasName("PK_Cotizaciones");

            entity.ToTable("Cotizaciones");

            entity.Property(e => e.IdCotizacion).HasColumnName("idCotizacion");
            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.emailCliente).HasColumnName("emailCliente");
            entity.Property(e => e.FechaCreacion)
                  .HasDefaultValueSql("(getdate())")
                  .HasColumnType("datetime")
                  .HasColumnName("fechaCreacion");
            entity.Property(e => e.Subtotal).HasColumnName("subtotal");
            entity.Property(e => e.Iva).HasColumnName("iva");
            entity.Property(e => e.Total).HasColumnName("total");
            entity.Property(e => e.Atendida).HasColumnName("atendida");
        });

        // Configuración de la tabla DetalleCotizaciones
        modelBuilder.Entity<DetalleCotizacion>(entity =>
        {
            entity.HasKey(e => e.IdDetalle).HasName("PK_DetalleCotizaciones");

            entity.ToTable("DetalleCotizaciones");

            entity.Property(e => e.IdDetalle).HasColumnName("idDetalle");
            entity.Property(e => e.IdCotizacion).HasColumnName("idCotizacion");
            entity.Property(e => e.Descripcion)
                  .IsRequired()
                  .HasMaxLength(255)
                  .HasColumnName("descripcion");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.PrecioUnitario).HasColumnName("precioUnitario");
            entity.Property(e => e.Total).HasColumnName("total");
        });

        modelBuilder.Entity<CodigoDescuento>(entity =>
        {
            entity.HasKey(e => e.IdCodigo).HasName("PK_CodigoDescuento");

            entity.ToTable("CodigoDescuento");

            entity.Property(e => e.IdCodigo).HasColumnName("idCodigo");
            entity.Property(e => e.Codigo)
                  .IsRequired()
                  .HasMaxLength(50)
                  .HasColumnName("codigo");
            entity.Property(e => e.Descripcion)
                  .IsRequired()
                  .HasMaxLength(255)
                  .HasColumnName("descripcion");
            entity.Property(e => e.DescuentoPorcentaje).HasColumnName("descuentoPorcentaje");
            entity.Property(e => e.DescuentoMonto).HasColumnName("descuentoMonto");
            entity.Property(e => e.FechaInicio)
                  .HasColumnType("datetime")
                  .HasColumnName("fechaInicio");
            entity.Property(e => e.FechaFin)
                  .HasColumnType("datetime")
                  .HasColumnName("fechaFin");
            entity.Property(e => e.CantidadMaxima).HasColumnName("cantidadMaxima");
            entity.Property(e => e.CantidadUsada).HasColumnName("cantidadUsada");
            entity.Property(e => e.Estatus).HasColumnName("estatus");
        });

        modelBuilder.Entity<UsuarioCodigoDescuento>(entity =>
        {
            entity.HasKey(e => e.IdUsuarioCodigo).HasName("PK_UsuarioCodigoDescuento");

            entity.ToTable("UsuarioCodigoDescuento");

            entity.Property(e => e.IdUsuarioCodigo).HasColumnName("idUsuarioCodigo");
            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.IdCodigo).HasColumnName("idCodigo");
            entity.Property(e => e.FechaAsignacion)
                  .HasDefaultValueSql("(getdate())")
                  .HasColumnType("datetime")
                  .HasColumnName("fechaAsignacion");
            entity.Property(e => e.Usado)
                  .IsRequired()
                  .HasDefaultValue(false)
                  .HasColumnName("usado");

            entity.HasOne(d => d.Usuario)
                  .WithMany(p => p.UsuarioCodigoDescuentos)
                  .HasForeignKey(d => d.IdUsuario)
                  .HasConstraintName("FK_UsuarioCodigoDescuento_Usuario");

            entity.HasOne(d => d.CodigoDescuento)
                  .WithMany(p => p.UsuarioCodigoDescuentos)
                  .HasForeignKey(d => d.IdCodigo)
                  .HasConstraintName("FK_UsuarioCodigoDescuento_CodigoDescuento");
        });


        // Configuración de la vista VistaCotizaciones
        modelBuilder.Entity<vw_Cotizacion>(entity =>
        {
            entity.HasNoKey(); 
            entity.ToView("VistaCotizaciones");

            entity.Property(e => e.IdCotizacion).HasColumnName("idCotizacion");
            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.Atendida).HasColumnName("atendida");
            entity.Property(e => e.EmailCliente).HasColumnName("emailCliente");
            entity.Property(e => e.FechaCreacion).HasColumnName("fechaCreacion");
            entity.Property(e => e.Subtotal).HasColumnName("subtotal");
            entity.Property(e => e.Iva).HasColumnName("iva");
            entity.Property(e => e.TotalCotizacion).HasColumnName("totalCotizacion");
            entity.Property(e => e.IdDetalle).HasColumnName("idDetalle");
            entity.Property(e => e.Descripcion).HasColumnName("descripcion");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.PrecioUnitario).HasColumnName("precioUnitario");
            entity.Property(e => e.TotalDetalle).HasColumnName("totalDetalle");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
