using System.ComponentModel.DataAnnotations;

namespace WEB_Boletim.Models
{
    public class Curso
    {
        public int ID { get; set; }
        [RegularExpression(@"^[a-zA-ZáàâãéèêíïóôõöúçñÁÀÂÃÉÈÍÏÓÔÕÖÚÇÑ ]+$", ErrorMessage = "Utilize apenas letras.")]
        [Required(ErrorMessage = "Esse campo é obrigatório.")]
        [Display(Name = "Curso")]
        public string CursoFaculdade { get; set; }
    }
}
