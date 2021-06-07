using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpticaSanfrancisco.Models
{
    
      [Table("Validacion")]   
       public class Validacion
    {
         [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int ID {get; set;}
         public int OPCIONES {get; set;}
         public int N_DOCUMENTO {get; set;}

        public String EMAIL {get; set;}

        public String NOMBRE {get; set;}

        public String APELLIDO{get; set;}

        public int TELEFONO { get; set; }

         [Display(Name="Tipo ")]
        [ForeignKey("IDTipo")]
         public string Tipo { get; set; }
         public int IDTipo { get; set; }

         [NotMapped]
         public string Respuesta { get; set; }
        
    }
}