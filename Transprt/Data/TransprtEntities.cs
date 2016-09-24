namespace Transprt.Data {
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Utils;
    public partial class TransprtEntities : DbContext {
        public TransprtEntities()
            : base(UtilAut.GetConnectionString()) {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<TransprtEntities, TransprtConfiguration>());
        }

        public virtual DbSet<AsignacionRuta> AsignacionRutas { get; set; }
        public virtual DbSet<Chofere> Choferes { get; set; }
        public virtual DbSet<Cliente> Clientes { get; set; }
        public virtual DbSet<Direccione> Direcciones { get; set; }
        public virtual DbSet<Estado> Estados { get; set; }
        public virtual DbSet<Marca> Marcas { get; set; }
        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<Modelo> Modelos { get; set; }
        public virtual DbSet<Pais> Paises { get; set; }
        public virtual DbSet<Pedido> Pedidos { get; set; }
        public virtual DbSet<Persona> Personas { get; set; }
        public virtual DbSet<Ruta> Rutas { get; set; }
        public virtual DbSet<TipoTransporte> TipoTransportes { get; set; }
        public virtual DbSet<Transporte> Transportes { get; set; }
        public virtual DbSet<MenuByArea> MenuByAreas { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            modelBuilder.Entity<AsignacionRuta>()
                .HasMany(e => e.Pedidos)
                .WithOptional(e => e.AsignacionRuta)
                .HasForeignKey(e => e.id_asignacion_ruta);

            modelBuilder.Entity<Chofere>()
                .Property(e => e.sexo)
                .IsUnicode(false);

            modelBuilder.Entity<Chofere>()
                .Property(e => e.nss)
                .IsUnicode(false);

            modelBuilder.Entity<Chofere>()
                .Property(e => e.num_lic_conducir)
                .IsUnicode(false);

            modelBuilder.Entity<Chofere>()
                .HasMany(e => e.AsignacionRutas)
                .WithRequired(e => e.Chofere)
                .HasForeignKey(e => e.id_chofer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Cliente>()
                .Property(e => e.nom_razon)
                .IsUnicode(false);

            modelBuilder.Entity<Cliente>()
                .Property(e => e.rfc)
                .IsUnicode(false);

            modelBuilder.Entity<Cliente>()
                .HasMany(e => e.Pedidos)
                .WithRequired(e => e.Cliente)
                .HasForeignKey(e => e.id_cliente)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Cliente>()
                .HasMany(e => e.Pedidos1)
                .WithRequired(e => e.Cliente1)
                .HasForeignKey(e => e.id_cliente_entrega)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Direccione>()
                .Property(e => e.calle)
                .IsUnicode(false);

            modelBuilder.Entity<Direccione>()
                .Property(e => e.num_ext)
                .IsUnicode(false);

            modelBuilder.Entity<Direccione>()
                .Property(e => e.num_int)
                .IsUnicode(false);

            modelBuilder.Entity<Direccione>()
                .Property(e => e.col)
                .IsUnicode(false);

            modelBuilder.Entity<Direccione>()
                .Property(e => e.cp)
                .IsUnicode(false);

            modelBuilder.Entity<Direccione>()
                .Property(e => e.tel_fijo)
                .IsUnicode(false);

            modelBuilder.Entity<Direccione>()
                .HasMany(e => e.Pedidos)
                .WithRequired(e => e.Direccione)
                .HasForeignKey(e => e.id_dir_entrega)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Direccione>()
                .HasMany(e => e.Pedidos1)
                .WithRequired(e => e.Direccione1)
                .HasForeignKey(e => e.id_dir_recogida)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Direccione>()
                .HasMany(e => e.Rutas)
                .WithRequired(e => e.Direccione)
                .HasForeignKey(e => e.id_direccion_inicio)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Direccione>()
                .HasMany(e => e.Rutas1)
                .WithRequired(e => e.Direccione1)
                .HasForeignKey(e => e.id_dirección_final)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Estado>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Estado>()
                .HasMany(e => e.Direcciones)
                .WithRequired(e => e.Estado)
                .HasForeignKey(e => e.id_estado)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Marca>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Marca>()
                .HasMany(e => e.Modelos)
                .WithRequired(e => e.Marca)
                .HasForeignKey(e => e.id_marca)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Menu>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Menu>()
                .Property(e => e.url)
                .IsUnicode(false);

            modelBuilder.Entity<Menu>()
                .HasMany(e => e.MenuByAreas)
                .WithRequired(e => e.Menu)
                .HasForeignKey(e => e.id_menu)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Modelo>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Modelo>()
                .HasMany(e => e.TipoTransportes)
                .WithRequired(e => e.Modelo)
                .HasForeignKey(e => e.id_modelo)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Pais>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Pais>()
                .Property(e => e.cod_iso)
                .IsUnicode(false);

            modelBuilder.Entity<Pais>()
                .HasMany(e => e.Estados)
                .WithRequired(e => e.Pais)
                .HasForeignKey(e => e.id_pais)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Persona>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Persona>()
                .Property(e => e.a_paterno)
                .IsUnicode(false);

            modelBuilder.Entity<Persona>()
                .Property(e => e.a_materno)
                .IsUnicode(false);

            modelBuilder.Entity<Persona>()
                .Property(e => e.c_electronico)
                .IsUnicode(false);

            modelBuilder.Entity<Persona>()
                .HasMany(e => e.Choferes)
                .WithRequired(e => e.Persona)
                .HasForeignKey(e => e.id_persona)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Persona>()
                .HasMany(e => e.Clientes)
                .WithRequired(e => e.Persona)
                .HasForeignKey(e => e.id_persona_contacto)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Ruta>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Ruta>()
                .HasMany(e => e.AsignacionRutas)
                .WithRequired(e => e.Ruta)
                .HasForeignKey(e => e.id_ruta)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TipoTransporte>()
                .Property(e => e.descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<TipoTransporte>()
                .HasMany(e => e.Transportes)
                .WithOptional(e => e.TipoTransporte)
                .HasForeignKey(e => e.id_tipo_transporte);

            modelBuilder.Entity<Transporte>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Transporte>()
                .Property(e => e.valor_factura)
                .IsFixedLength();

            modelBuilder.Entity<Transporte>()
                .HasMany(e => e.AsignacionRutas)
                .WithRequired(e => e.Transporte)
                .HasForeignKey(e => e.id_transporte)
                .WillCascadeOnDelete(false);
        }
    }
}
