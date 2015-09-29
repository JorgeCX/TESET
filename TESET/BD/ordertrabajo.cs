namespace TESET.BD
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("dbdepmantenimientocc.ordertrabajo")]
    public partial class ordertrabajo
    {
        [Key]
        public int idOrder { get; set; }

        public int idMantenimiento { get; set; }

        public int idTipoSer { get; set; }

        [Required]
        [StringLength(300)]
        public string Asignado { get; set; }

        public DateTime FechaRealizacion { get; set; }

        [Required]
        [StringLength(3000)]
        public string TrabajoRealizado { get; set; }

        [Required]
        [StringLength(3000)]
        public string Aprovado { get; set; }

        public int idSolicitud { get; set; }

        [Column(TypeName = "varbinary")]
        public byte[] firma { get; set; }

        public virtual mantenimientoorden mantenimientoorden { get; set; }

        public virtual tiposervicio tiposervicio { get; set; }

        public virtual solicitudmantenimiento solicitudmantenimiento { get; set; }
    }
}
