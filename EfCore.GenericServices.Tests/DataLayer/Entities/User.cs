using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; private set; }

        [MaxLength(200)]
        public string Name { get; set; }

        public bool IsDeleted { get; set; }
    }
}
