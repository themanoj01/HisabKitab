using System.ComponentModel.DataAnnotations;

namespace HisabKitab.Model
{
    public class Tags
    {
        public Guid TagId { get; set; } = Guid.NewGuid();
        public required string TagName { get; set; }
    }
}
