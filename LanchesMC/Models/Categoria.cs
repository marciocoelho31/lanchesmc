using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LanchesMC.Models
{
    public class Categoria
    {
        public int CategoriaId { get; set; }
        [StringLength(100)]
        [Display(Name = "Nome")]
        public string CategoriaNome { get; set; }
        [StringLength(200)]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
        public List<Lanche> Lanches { get; set; }
    }
}
