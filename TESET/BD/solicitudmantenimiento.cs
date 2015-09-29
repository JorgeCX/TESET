namespace TESET.BD
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("dbdepmantenimientocc.solicitudmantenimiento")]
    public partial class solicitudmantenimiento
    {
        public solicitudmantenimiento()
        {
            ordertrabajo = new HashSet<ordertrabajo>();
        }

        [Key]
        public int idSolicitud { get; set; }

        public int idArea { get; set; }

        public int idTipo { get; set; }

        [Required]
        [StringLength(300)]
        public string Nombre { get; set; }

        public DateTime FechaElaboracion { get; set; }

        [Required]
        [StringLength(3000)]
        public string Descripcion { get; set; }

        [Column(TypeName = "varbinary")]
        public byte[] firma { get; set; }

        public int? status { get; set; }

        public virtual areas areas { get; set; }

        public virtual ICollection<ordertrabajo> ordertrabajo { get; set; }

        public virtual tiposolicitud tiposolicitud { get; set; }
    }
}
