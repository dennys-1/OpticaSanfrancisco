using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using OpticaSanfrancisco.Models;

namespace OpticaSanfrancisco.Models
{
    public class Tipo
    {
        
  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID")]
        public int ID { get; set; }

        [Column("Tipo")]
        public string Nombre { get; set; }

        [Column("Deshabilitado")]
        public bool Deshabilitado { get; set; }

        // [ForeignKey("IdTipo")]
        public ICollection<Validacion> Validaciones { get; set; }

        public Tipo()
        {
            Validaciones = new List<Validacion>();
        }





    }
}