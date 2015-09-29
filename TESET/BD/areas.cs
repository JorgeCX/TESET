namespace TESET.BD
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("dbdepmantenimientocc.areas")]
    public partial class areas
    {
        public areas()
        {
            solicitudmantenimiento = new HashSet<solicitudmantenimiento>();
        }

        [Key]
        public int idArea { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        public virtual ICollection<solicitudmantenimiento> solicitudmantenimiento { get; set; }
    }
}
