using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MusicfyAPI.Data.Entities
{
    public abstract class EntityBase : IEntityBase
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public bool IsDeleted { get; set; }
    }

    public interface IEntityBase
    {        
        bool IsDeleted { get; set; }
    }
}
