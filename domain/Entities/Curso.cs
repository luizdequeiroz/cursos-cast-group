using domain.Attributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace domain.Entities
{
    public class Curso : Entity
    {
        [Required(ErrorMessage = "Assunto obrigatório.")]
        public string Assunto { get; set; }
        [Required(ErrorMessage = "Data de início do curso obrigatória.")]
        public DateTime DataInicio { get; set; }
        [Required(ErrorMessage = "Data de término do curso obrigatória.")]
        public DateTime DataTermino { get; set; }
        public int QuantidadeAlunos { get; set; }

        [ForeignKey("Categoria")]
        [NotEqual(0, ErrorMessage = "É necessário atribuir uma categoria ao curso.")]
        public int CodigoCategoria { get; set; }
        public Categoria Categoria { get; set; }
    }
}
