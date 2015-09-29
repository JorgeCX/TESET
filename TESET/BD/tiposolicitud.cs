namespace TESET.BD
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("dbdepmantenimientocc.tiposolicitud")]
    public partial class tiposolicitud
    {
        public tiposolicitud()
        {
            solicitudmantenimiento = new HashSet<solicitudmantenimiento>();
        }

        [Key]
        public int idTipo { get; set; }

        [Required]
        [StringLength(200)]
        public string Tipo { get; set; }

        public virtual ICollection<solicitudmantenimiento> solicitudmantenimiento { get; set; }
    }
}
