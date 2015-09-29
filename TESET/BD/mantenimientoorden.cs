namespace TESET.BD
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("dbdepmantenimientocc.mantenimientoorden")]
    public partial class mantenimientoorden
    {
        public mantenimientoorden()
        {
            ordertrabajo = new HashSet<ordertrabajo>();
        }

        [Key]
        public int idMantenimiento { get; set; }

        [Required]
        [StringLength(100)]
        public string Tipo { get; set; }

        public virtual ICollection<ordertrabajo> ordertrabajo { get; set; }
    }
}
