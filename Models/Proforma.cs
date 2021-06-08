using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpticaSanfrancisco.Models
{
    //CARRITO
    [Table("t_proforma")]
    public class Proforma
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int ID {get; set;}

        public String UserID {get; set;}

        public Productos Producto {get; set;}

        public int Quantity{get; set;}

        public Decimal Price { get; set; }
           public String Codigo {get; set;}
             public String Imagen { get; set; }
    }
}