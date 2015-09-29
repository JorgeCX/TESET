namespace TESET.BD
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class dbManteniemnto : DbContext
    {
        public dbManteniemnto()
            : base("name=dbManteniemnto")
        {
        }

        public virtual DbSet<areas> areas { get; set; }
        public virtual DbSet<mantenimientoorden> mantenimientoorden { get; set; }
        public virtual DbSet<ordertrabajo> ordertrabajo { get; set; }
        public virtual DbSet<roles> roles { get; set; }
        public virtual DbSet<solicitudmantenimiento> solicitudmantenimiento { get; set; }
        public virtual DbSet<tiposervicio> tiposervicio { get; set; }
        public virtual DbSet<tiposolicitud> tiposolicitud { get; set; }
        public virtual DbSet<users> users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<areas>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<areas>()
                .HasMany(e => e.solicitudmantenimiento)
                .WithRequired(e => e.areas)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<mantenimientoorden>()
                .Property(e => e.Tipo)
                .IsUnicode(false);

            modelBuilder.Entity<mantenimientoorden>()
                .HasMany(e => e.ordertrabajo)
                .WithRequired(e => e.mantenimientoorden)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ordertrabajo>()
                .Property(e => e.Asignado)
                .IsUnicode(false);

            modelBuilder.Entity<ordertrabajo>()
                .Property(e => e.TrabajoRealizado)
                .IsUnicode(false);

            modelBuilder.Entity<ordertrabajo>()
                .Property(e => e.Aprovado)
                .IsUnicode(false);

            modelBuilder.Entity<roles>()
                .Property(e => e.NombreRol)
                .IsUnicode(false);

            modelBuilder.Entity<solicitudmantenimiento>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<solicitudmantenimiento>()
                .Property(e => e.Descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<solicitudmantenimiento>()
                .HasMany(e => e.ordertrabajo)
                .WithRequired(e => e.solicitudmantenimiento)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tiposervicio>()
                .Property(e => e.Servicio)
                .IsUnicode(false);

            modelBuilder.Entity<tiposervicio>()
                .HasMany(e => e.ordertrabajo)
                .WithRequired(e => e.tiposervicio)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tiposolicitud>()
                .Property(e => e.Tipo)
                .IsUnicode(false);

            modelBuilder.Entity<tiposolicitud>()
                .HasMany(e => e.solicitudmantenimiento)
                .WithRequired(e => e.tiposolicitud)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<users>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<users>()
                .Property(e => e.APaterno)
                .IsUnicode(false);

            modelBuilder.Entity<users>()
                .Property(e => e.AMaterno)
                .IsUnicode(false);

            modelBuilder.Entity<users>()
                .Property(e => e.userName)
                .IsUnicode(false);

            modelBuilder.Entity<users>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<users>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<users>()
                .HasMany(e => e.roles)
                .WithMany(e => e.users)
                .Map(m => m.ToTable("userrol").MapLeftKey("idUser").MapRightKey("idRol"));
        }
    }
}
