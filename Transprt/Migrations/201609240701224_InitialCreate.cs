namespace Transprt.Data
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AsignacionRuta",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        id_ruta = c.Int(nullable: false),
                        id_chofer = c.Int(nullable: false),
                        id_transporte = c.Int(nullable: false),
                        fec_inicio = c.DateTime(nullable: false, storeType: "date"),
                        fec_final = c.DateTime(storeType: "date"),
                        usr_crea = c.String(maxLength: 128),
                        fec_crea = c.DateTime(nullable: false, storeType: "date"),
                        usr_modif = c.String(maxLength: 128),
                        fec_modif = c.DateTime(storeType: "date"),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Choferes", t => t.id_chofer)
                .ForeignKey("dbo.Rutas", t => t.id_ruta)
                .ForeignKey("dbo.Transportes", t => t.id_transporte)
                .Index(t => t.id_ruta)
                .Index(t => t.id_chofer)
                .Index(t => t.id_transporte);
            
            CreateTable(
                "dbo.Choferes",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        id_persona = c.Int(nullable: false),
                        edad = c.Int(nullable: false),
                        sexo = c.String(nullable: false, maxLength: 1, unicode: false),
                        nss = c.String(nullable: false, maxLength: 15, unicode: false),
                        num_lic_conducir = c.String(nullable: false, maxLength: 15, unicode: false),
                        activo = c.Boolean(nullable: false),
                        usr_crea = c.String(maxLength: 128),
                        fec_crea = c.DateTime(nullable: false, storeType: "date"),
                        usr_modif = c.String(maxLength: 128),
                        fec_modif = c.DateTime(storeType: "date"),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Personas", t => t.id_persona)
                .Index(t => t.id_persona);
            
            CreateTable(
                "dbo.Personas",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        nombre = c.String(nullable: false, maxLength: 50, unicode: false),
                        a_paterno = c.String(nullable: false, maxLength: 50, unicode: false),
                        a_materno = c.String(maxLength: 50, unicode: false),
                        c_electronico = c.String(maxLength: 50, unicode: false),
                        activo = c.Boolean(nullable: false),
                        usr_crea = c.String(maxLength: 128),
                        fec_crea = c.DateTime(nullable: false, storeType: "date"),
                        usr_modif = c.String(maxLength: 128),
                        fec_modif = c.DateTime(storeType: "date"),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Clientes",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        nom_razon = c.String(nullable: false, maxLength: 150, unicode: false),
                        id_persona_contacto = c.Int(nullable: false),
                        rfc = c.String(nullable: false, maxLength: 15, unicode: false),
                        activo = c.Boolean(nullable: false),
                        usr_crea = c.String(maxLength: 128),
                        fec_crea = c.DateTime(nullable: false, storeType: "date"),
                        usr_modif = c.String(maxLength: 128),
                        fec_modif = c.DateTime(storeType: "date"),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Personas", t => t.id_persona_contacto)
                .Index(t => t.id_persona_contacto);
            
            CreateTable(
                "dbo.Pedidos",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        id_cliente = c.Int(nullable: false),
                        id_dir_recogida = c.Int(nullable: false),
                        id_dir_entrega = c.Int(nullable: false),
                        id_cliente_entrega = c.Int(nullable: false),
                        id_asignacion_ruta = c.Int(),
                        usr_crea = c.String(maxLength: 128),
                        fec_crea = c.DateTime(nullable: false, storeType: "date"),
                        usr_modif = c.String(maxLength: 128),
                        fec_modif = c.DateTime(storeType: "date"),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Direcciones", t => t.id_dir_entrega)
                .ForeignKey("dbo.Direcciones", t => t.id_dir_recogida)
                .ForeignKey("dbo.Clientes", t => t.id_cliente)
                .ForeignKey("dbo.Clientes", t => t.id_cliente_entrega)
                .ForeignKey("dbo.AsignacionRuta", t => t.id_asignacion_ruta)
                .Index(t => t.id_cliente)
                .Index(t => t.id_dir_recogida)
                .Index(t => t.id_dir_entrega)
                .Index(t => t.id_cliente_entrega)
                .Index(t => t.id_asignacion_ruta);
            
            CreateTable(
                "dbo.Direcciones",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        calle = c.String(nullable: false, maxLength: 50, unicode: false),
                        num_ext = c.String(nullable: false, maxLength: 50, unicode: false),
                        num_int = c.String(maxLength: 50, unicode: false),
                        col = c.String(nullable: false, maxLength: 50, unicode: false),
                        id_estado = c.Int(nullable: false),
                        cp = c.String(nullable: false, maxLength: 10, unicode: false),
                        tel_fijo = c.String(nullable: false, maxLength: 50, unicode: false),
                        activo = c.Boolean(nullable: false),
                        usr_crea = c.String(maxLength: 128),
                        fec_crea = c.DateTime(nullable: false, storeType: "date"),
                        usr_modif = c.String(maxLength: 128),
                        fec_modif = c.DateTime(storeType: "date"),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Estados", t => t.id_estado)
                .Index(t => t.id_estado);
            
            CreateTable(
                "dbo.Estados",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        nombre = c.String(nullable: false, maxLength: 50, unicode: false),
                        id_pais = c.Int(nullable: false),
                        activo = c.Boolean(nullable: false),
                        usr_crea = c.String(maxLength: 128),
                        fec_crea = c.DateTime(nullable: false, storeType: "date"),
                        usr_modif = c.String(maxLength: 128),
                        fec_modif = c.DateTime(storeType: "date"),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Paises", t => t.id_pais)
                .Index(t => t.id_pais);
            
            CreateTable(
                "dbo.Paises",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        nombre = c.String(nullable: false, maxLength: 50, unicode: false),
                        cod_iso = c.String(nullable: false, maxLength: 3, unicode: false),
                        activo = c.Boolean(nullable: false),
                        usr_crea = c.String(maxLength: 128),
                        fec_crea = c.DateTime(nullable: false, storeType: "date"),
                        usr_modif = c.String(maxLength: 128),
                        fec_modif = c.DateTime(storeType: "date"),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Rutas",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        nombre = c.String(nullable: false, maxLength: 50, unicode: false),
                        id_direccion_inicio = c.Int(nullable: false),
                        id_dirección_final = c.Int(nullable: false),
                        km_total = c.Int(nullable: false),
                        gasto_gasolina = c.Decimal(nullable: false, precision: 18, scale: 2),
                        gasto_peajes = c.Decimal(nullable: false, precision: 18, scale: 2),
                        otros_gastos = c.Decimal(nullable: false, precision: 18, scale: 2),
                        activo = c.Boolean(nullable: false),
                        usr_crea = c.String(maxLength: 128),
                        fec_crea = c.DateTime(nullable: false, storeType: "date"),
                        usr_modif = c.String(maxLength: 128),
                        fec_modif = c.DateTime(storeType: "date"),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Direcciones", t => t.id_direccion_inicio)
                .ForeignKey("dbo.Direcciones", t => t.id_dirección_final)
                .Index(t => t.id_direccion_inicio)
                .Index(t => t.id_dirección_final);
            
            CreateTable(
                "dbo.Transportes",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        nombre = c.String(maxLength: 50, unicode: false),
                        id_tipo_transporte = c.Int(),
                        fec_adquisicion = c.DateTime(nullable: false, storeType: "date"),
                        valor_factura = c.String(maxLength: 10, fixedLength: true),
                        fec_ultima_inspeccion = c.DateTime(storeType: "date"),
                        fec_ultimo_servicio = c.DateTime(storeType: "date"),
                        activo = c.Boolean(nullable: false),
                        usr_crea = c.String(maxLength: 128),
                        fec_crea = c.DateTime(nullable: false, storeType: "date"),
                        usr_modif = c.String(maxLength: 128),
                        fec_modif = c.DateTime(storeType: "date"),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.TipoTransporte", t => t.id_tipo_transporte)
                .Index(t => t.id_tipo_transporte);
            
            CreateTable(
                "dbo.TipoTransporte",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        descripcion = c.String(nullable: false, maxLength: 50, unicode: false),
                        id_modelo = c.Int(nullable: false),
                        activo = c.Boolean(nullable: false),
                        usr_crea = c.String(maxLength: 128),
                        fec_crea = c.DateTime(nullable: false, storeType: "date"),
                        usr_modif = c.String(maxLength: 128),
                        fec_modif = c.DateTime(storeType: "date"),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Modelos", t => t.id_modelo)
                .Index(t => t.id_modelo);
            
            CreateTable(
                "dbo.Modelos",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        nombre = c.String(nullable: false, maxLength: 50, unicode: false),
                        anho_fabricacion = c.Int(nullable: false),
                        id_marca = c.Int(nullable: false),
                        activo = c.Boolean(nullable: false),
                        usr_crea = c.String(maxLength: 128),
                        fec_crea = c.DateTime(nullable: false, storeType: "date"),
                        usr_modif = c.String(maxLength: 128),
                        fec_modif = c.DateTime(storeType: "date"),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Marcas", t => t.id_marca)
                .Index(t => t.id_marca);
            
            CreateTable(
                "dbo.Marcas",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        nombre = c.String(nullable: false, maxLength: 50, unicode: false),
                        activo = c.Boolean(nullable: false),
                        usr_crea = c.String(maxLength: 128),
                        fec_crea = c.DateTime(nullable: false, storeType: "date"),
                        usr_modif = c.String(maxLength: 128),
                        fec_modif = c.DateTime(storeType: "date"),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.MenuByArea",
                c => new
                    {
                        id_menu = c.Int(nullable: false),
                        id_area = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.id_menu, t.id_area })
                .ForeignKey("dbo.Menu", t => t.id_menu)
                .Index(t => t.id_menu);
            
            CreateTable(
                "dbo.Menu",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 20, unicode: false),
                        url = c.String(nullable: false, maxLength: 500, unicode: false),
                        activo = c.Boolean(nullable: false),
                        usr_crea = c.String(maxLength: 128),
                        fec_crea = c.DateTime(nullable: false, storeType: "date"),
                        usr_modif = c.String(maxLength: 128),
                        fec_modif = c.DateTime(storeType: "date"),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MenuByArea", "id_menu", "dbo.Menu");
            DropForeignKey("dbo.Transportes", "id_tipo_transporte", "dbo.TipoTransporte");
            DropForeignKey("dbo.TipoTransporte", "id_modelo", "dbo.Modelos");
            DropForeignKey("dbo.Modelos", "id_marca", "dbo.Marcas");
            DropForeignKey("dbo.AsignacionRuta", "id_transporte", "dbo.Transportes");
            DropForeignKey("dbo.Pedidos", "id_asignacion_ruta", "dbo.AsignacionRuta");
            DropForeignKey("dbo.Clientes", "id_persona_contacto", "dbo.Personas");
            DropForeignKey("dbo.Pedidos", "id_cliente_entrega", "dbo.Clientes");
            DropForeignKey("dbo.Pedidos", "id_cliente", "dbo.Clientes");
            DropForeignKey("dbo.Rutas", "id_dirección_final", "dbo.Direcciones");
            DropForeignKey("dbo.Rutas", "id_direccion_inicio", "dbo.Direcciones");
            DropForeignKey("dbo.AsignacionRuta", "id_ruta", "dbo.Rutas");
            DropForeignKey("dbo.Pedidos", "id_dir_recogida", "dbo.Direcciones");
            DropForeignKey("dbo.Pedidos", "id_dir_entrega", "dbo.Direcciones");
            DropForeignKey("dbo.Estados", "id_pais", "dbo.Paises");
            DropForeignKey("dbo.Direcciones", "id_estado", "dbo.Estados");
            DropForeignKey("dbo.Choferes", "id_persona", "dbo.Personas");
            DropForeignKey("dbo.AsignacionRuta", "id_chofer", "dbo.Choferes");
            DropIndex("dbo.MenuByArea", new[] { "id_menu" });
            DropIndex("dbo.Modelos", new[] { "id_marca" });
            DropIndex("dbo.TipoTransporte", new[] { "id_modelo" });
            DropIndex("dbo.Transportes", new[] { "id_tipo_transporte" });
            DropIndex("dbo.Rutas", new[] { "id_dirección_final" });
            DropIndex("dbo.Rutas", new[] { "id_direccion_inicio" });
            DropIndex("dbo.Estados", new[] { "id_pais" });
            DropIndex("dbo.Direcciones", new[] { "id_estado" });
            DropIndex("dbo.Pedidos", new[] { "id_asignacion_ruta" });
            DropIndex("dbo.Pedidos", new[] { "id_cliente_entrega" });
            DropIndex("dbo.Pedidos", new[] { "id_dir_entrega" });
            DropIndex("dbo.Pedidos", new[] { "id_dir_recogida" });
            DropIndex("dbo.Pedidos", new[] { "id_cliente" });
            DropIndex("dbo.Clientes", new[] { "id_persona_contacto" });
            DropIndex("dbo.Choferes", new[] { "id_persona" });
            DropIndex("dbo.AsignacionRuta", new[] { "id_transporte" });
            DropIndex("dbo.AsignacionRuta", new[] { "id_chofer" });
            DropIndex("dbo.AsignacionRuta", new[] { "id_ruta" });
            DropTable("dbo.Menu");
            DropTable("dbo.MenuByArea");
            DropTable("dbo.Marcas");
            DropTable("dbo.Modelos");
            DropTable("dbo.TipoTransporte");
            DropTable("dbo.Transportes");
            DropTable("dbo.Rutas");
            DropTable("dbo.Paises");
            DropTable("dbo.Estados");
            DropTable("dbo.Direcciones");
            DropTable("dbo.Pedidos");
            DropTable("dbo.Clientes");
            DropTable("dbo.Personas");
            DropTable("dbo.Choferes");
            DropTable("dbo.AsignacionRuta");
        }
    }
}
