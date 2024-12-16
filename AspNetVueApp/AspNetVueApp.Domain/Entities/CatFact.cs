using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetVueApp.Domain.Entities
{
    public class CatFact
    {
        [Key] // Clave primaria incremental
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required] // El campo Fact es obligatorio
        public string Fact { get; set; }

        [Required] // La fecha es obligatoria
        public DateTime SearchedOn { get; set; }
    }
}
