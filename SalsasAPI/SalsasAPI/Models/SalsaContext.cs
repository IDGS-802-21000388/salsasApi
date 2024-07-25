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

    public virtual DbSet<Recetum> Receta { get; set; }

    public virtual DbSet<SolicitudProduccion> SolicitudProduccions { get; set; }

    public virtual DbSet<Tarjetum> Tarjeta { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Ventum> Venta { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LENOVO\\MSSQLSERVER02; Initial Catalog=Salsas; user id=sa; password=root;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Compra>(entity =>
        {
            entity.HasKey(e => e.IdCompra).HasName("PK__Compra__48B99DB7DE15CCD8");

            entity.ToTable("Compra");

            entity.Property(e => e.IdCompra).HasColumnName("idCompra");
            entity.Property(e => e.CantidadExistentes).HasColumnName("cantidadExistentes");
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
            entity.HasKey(e => e.IdDetalleMateriaPrima).HasName("PK__Detalle___451468AC4075AEB9");

            entity.ToTable("Detalle_materia_prima");

            entity.Property(e => e.IdDetalleMateriaPrima).HasColumnName("idDetalle_materia_prima");
            entity.Property(e => e.CantidadExistentes).HasColumnName("cantidadExistentes");
            entity.Property(e => e.Estatus)
                .HasDefaultValue(1)
                .HasColumnName("estatus");
            entity.Property(e => e.FechaCompra)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaCompra");
            entity.Property(e => e.FechaVencimiento)
                .HasColumnType("datetime")
                .HasColumnName("fechaVencimiento");
            entity.Property(e => e.IdMateriaPrima).HasColumnName("idMateriaPrima");
            entity.Property(e => e.Porcentaje)
                .HasDefaultValue(100)
                .HasColumnName("porcentaje");

            entity.HasOne(d => d.IdMateriaPrimaNavigation).WithMany(p => p.DetalleMateriaPrimas)
                .HasForeignKey(d => d.IdMateriaPrima)
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

        modelBuilder.Entity<DetalleRecetum>(entity =>
        {
            entity.HasKey(e => e.IdDetalleReceta).HasName("PK__Detalle___1629BEDE55F6928B");

            entity.ToTable("Detalle_receta");

            entity.Property(e => e.IdDetalleReceta).HasColumnName("idDetalle_receta");
            entity.Property(e => e.IdMateriaPrima).HasColumnName("idMateriaPrima");
            entity.Property(e => e.IdReceta).HasColumnName("idReceta");
            entity.Property(e => e.Porcion).HasColumnName("porcion");

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
            entity.Property(e => e.IdMedida).HasColumnName("idMedida");
            entity.Property(e => e.IdProducto).HasColumnName("idProducto");
            entity.Property(e => e.IdVenta).HasColumnName("idVenta");
            entity.Property(e => e.Subtotal).HasColumnName("subtotal");

            entity.HasOne(d => d.IdMedidaNavigation).WithMany(p => p.DetalleVenta)
                .HasForeignKey(d => d.IdMedida)
                .HasConstraintName("FK__DetalleVe__idMed__03F0984C");

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
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
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
            entity.Property(e => e.CantidadProduccion)
                .HasDefaultValue(1)
                .HasColumnName("cantidadProduccion");
            entity.Property(e => e.Estatus)
                .HasDefaultValue(1)
                .HasColumnName("estatus");
            entity.Property(e => e.FechaSolicitud)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaSolicitud");
            entity.Property(e => e.IdProducto).HasColumnName("idProducto");
            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.SolicitudProduccions)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("FK__Solicitud__idPro__10566F31");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.SolicitudProduccions)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK__Solicitud__idUsu__114A936A");
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

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__645723A6C661BB22");

            entity.ToTable("Usuario");

            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
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
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
