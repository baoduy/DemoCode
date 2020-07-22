using System.ComponentModel.DataAnnotations;
using GenericServices;

namespace DataLayer
{
    public class UserDto : ILinkToEntity<User>
    {
        public int Id { get; set; }

        [StringLength(200)]
        public string Name { get; set; }

        public bool IsDeleted { get; set; }
    }
}
