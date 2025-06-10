using System.ComponentModel.DataAnnotations;

namespace FlatFileGenerator.Core.Models
{
    public class Entity<T>
    {
        public T Id { get; set; }

        [Display(Name = "Oprettet")]
        public DateTime CreatedDate { get; set; }
    }
}
