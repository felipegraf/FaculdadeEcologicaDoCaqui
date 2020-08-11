using System.ComponentModel.DataAnnotations;

namespace WEB_Boletim.Models
{
    public class Nota
    {
        public int ID { get; set; }
        [Display(Name = "Aluno")]
        public int AlunoID { get; set; }
        [Display(Name = "Aluno")]
        public Aluno Alunos { get; set; }
        [Display(Name = "Matéria")]
        public int MateriaID { get; set; }
        [Display(Name = "Matéria")]
        public Materia Materias { get; set; }

        [Display(Name = "Nota")]
        [Required(ErrorMessage = "Por favor, digite a nota.")]
        public float NotaAluno { get; set; }
    }
}
