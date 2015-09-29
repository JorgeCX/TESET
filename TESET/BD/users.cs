namespace TESET.BD
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("dbdepmantenimientocc.users")]
    public partial class users
    {
        public users()
        {
            roles = new HashSet<roles>();
        }

        [Key]
        public int idUser { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [Required]
        [StringLength(200)]
        public string APaterno { get; set; }

        [Required]
        [StringLength(200)]
        public string AMaterno { get; set; }

        [Required]
        [StringLength(200)]
        public string userName { get; set; }

        [Required]
        [StringLength(200)]
        public string password { get; set; }

        [Required]
        [StringLength(200)]
        public string Email { get; set; }

        public bool? isEnable { get; set; }

        public int? cookie { get; set; }

        public virtual ICollection<roles> roles { get; set; }
    }
}
