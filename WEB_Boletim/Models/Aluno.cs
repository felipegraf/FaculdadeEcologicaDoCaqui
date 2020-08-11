using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WEB_Boletim.Models
{
    public class Aluno
    {
        public int ID { get; set; }

        [RegularExpression(@"^[a-zA-ZáàâãéèêíïóôõöúçñÁÀÂÃÉÈÍÏÓÔÕÖÚÇÑ ]+$", ErrorMessage = "Utilize apenas letras.")]
        [Required(ErrorMessage = "Esse campo é obrigatório.")]
        [Display(Name = "Nome")]
        public string NomeAluno { get; set; }

        [RegularExpression(@"^[a-zA-ZáàâãéèêíïóôõöúçñÁÀÂÃÉÈÍÏÓÔÕÖÚÇÑ ]+$", ErrorMessage = "Utilize apenas letras.")]
        [Required(ErrorMessage = "Esse campo é obrigatório.")]
        [Display(Name = "Sobrenome")]
        public string SobrenomeAluno { get; set; }

        [Required(ErrorMessage = "Esse campo é obrigatório.")]
        [Display(Name = "Data de Nascimento")]
        [DataType(DataType.Date)]
        public DateTime DataNascimentoAluno { get; set; }

        [Required(ErrorMessage = "Esse campo é obrigatório.")]
        [Display(Name = "CPF")]
        public string CPFAluno { get; set; }

        [Display(Name = "Curso")]
        public int CursoID { get; set; }
        [Display(Name = "Curso")]
        public Curso Cursos { get; set; }

        public virtual IList<Nota> Notas { get; set; }
    }
}
