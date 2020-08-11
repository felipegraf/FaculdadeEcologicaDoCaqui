using System;
using System.ComponentModel.DataAnnotations;

namespace WEB_Boletim.Models
{
    public class Materia
    {
        public int ID { get; set; }
        [RegularExpression(@"^[a-zA-ZáàâãéèêíïóôõöúçñÁÀÂÃÉÈÍÏÓÔÕÖÚÇÑ ]+$", ErrorMessage = "Utilize apenas letras.")]
        [Required(ErrorMessage = "Esse campo é obrigatório.")]
        [Display(Name = "Matéria")]
        public string DescricaoMateria { get; set; }

        [Display(Name = "Data de Cadastro")]
        [DataType(DataType.Date)]
        public DateTime DataDeCadastroMateria { get; set; }
        [Display(Name = "Situação")]
        public int SituacaoID { get; set; }
        [Display(Name = "Situação")]
        public Situacao Situacoes { get; set; }
    }
}
