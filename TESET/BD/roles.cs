namespace TESET.BD
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("dbdepmantenimientocc.roles")]
    public partial class roles
    {
        public roles()
        {
            users = new HashSet<users>();
        }

        [Key]
        public int idRol { get; set; }

        [Required]
        [StringLength(100)]
        public string NombreRol { get; set; }

        public virtual ICollection<users> users { get; set; }
    }
}
