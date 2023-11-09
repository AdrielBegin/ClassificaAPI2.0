using System.ComponentModel.DataAnnotations;

namespace Classifica3._0.Model
{
    public class RoleModification
    {
        //[Required]
        public string? RoleName { get; set; }
        public string? RoleId { get; set; }
        public string[]? AddIds { get; set; }
        public string[]? DeleteIds { get; set; }
    }
}
