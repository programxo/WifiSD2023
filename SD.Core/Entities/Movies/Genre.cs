namespace Wifi.SD.Core.Entities.Movies
{
    public class Genre : IEntity // 5.
    {
        public Genre()
        {
            this.Movies = new HashSet<Movie>();
        }

        // The virtual keyword is used to modify a method, property, indexer, or event declaration
        // and allow for it to be overridden in a derived class.

        public virtual int Id { get; set; }

        [MaxLength(256)]
        public virtual string Name { get; set; }

        public virtual ICollection<Movie> Movies { get;}
    }
}
