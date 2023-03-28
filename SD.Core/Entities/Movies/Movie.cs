namespace Wifi.SD.Core.Entities.Movies
{
    public class Movie : MovieBase, IEntity // 2.
    {
        public Genre Genre { get; set; }

        [ForeignKey(nameof(base.MediumTypeCode))] // 7.
        public MediumType MediumType { get; set; }
    }
}
