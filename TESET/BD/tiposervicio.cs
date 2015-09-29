namespace TESET.BD
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("dbdepmantenimientocc.tiposervicio")]
    public partial class tiposervicio
    {
        public tiposervicio()
        {
            ordertrabajo = new HashSet<ordertrabajo>();
        }

        [Key]
        public int idTipoSer { get; set; }

        [StringLength(200)]
        public string Servicio { get; set; }

        public virtual ICollection<ordertrabajo> ordertrabajo { get; set; }
    }
}
