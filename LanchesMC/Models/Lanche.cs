using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LanchesMC.Models
{
    public class Lanche
    {
        public int LancheId { get; set; }
        [StringLength(100)]
        public string Nome { get; set; }
        [StringLength(100)]
        [Display(Name = "Descrição curta")]
        public string DescricaoCurta { get; set; }
        [StringLength(255)]
        [Display(Name = "Descrição detalhada")]
        public string DescricaoDetalhada { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Preço unitário")]
        public decimal Preco { get; set; }
        [StringLength(200)]
        [Display(Name = "URL da imagem")]
        public string ImagemUrl { get; set; }
        [StringLength(200)]
        [Display(Name = "URL da miniatura da imagem")]
        public string ImagemThumbnailUrl { get; set; }
        [Display(Name = "Preferido?")]
        public bool IsLanchePreferido { get; set; }
        [Display(Name = "Em estoque?")]
        public bool EmEstoque { get; set; }
        public int CategoriaId { get; set; }
        public virtual Categoria Categoria { get; set; }
    }
}
