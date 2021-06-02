using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpticaSanfrancisco.Models
{
    [Table("t_product")]
    public class Productos
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int ID {get; set;}

        [Required(ErrorMessage = "Por favor ingrese nombre de producto")]
        [Display(Name="Nombre Producto")]
        public String Name {get; set;}

        [Required(ErrorMessage = "Porfavor ingrese el precio")]
        public Decimal Price { get; set; }

        [Required(ErrorMessage = "Porfavor ingrese la imagen")]
        public String ImagenName { get; set; }

        [Required(ErrorMessage = "Please enter Status")]
        public String Status { get; set; }

        //public virtual ICollection<Proforma> ProformaItems { get; set; }

    }
}