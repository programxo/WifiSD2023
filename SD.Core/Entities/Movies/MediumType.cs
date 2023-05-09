using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Wifi.SD.Core.Entities.Movies
{
    public class MediumType : IEntity // 6.
    {
        public MediumType()
        {
            this.Movies = new HashSet<Movie>();
        }

        // The virtual method is declared in a base class and has an implementation, but the child class may override the default implementation.
        [Key]
        [MaxLength(8)]
        public virtual string Code { get; set; }

        [MaxLength(32), MinLength(2)]
        public virtual string Name { get; set; }

        [JsonIgnore]
        public virtual ICollection<Movie> Movies { get; }
    }
}
