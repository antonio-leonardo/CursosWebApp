using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cursos.Domain.Domain.Models
{
    [Table("Professores")]
    public class Instructor
    {
        [Key]
        public Guid Id { get; set; }

        [Column("NomeCompleto")]
        public string CompleteName { get; set; }

        [Column("DataCriacao")]
        public DateTime CreatedDate { get; set; }
    }
}