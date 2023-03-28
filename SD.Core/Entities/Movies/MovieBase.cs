namespace Wifi.SD.Core.Entities.Movies
{
    public abstract class MovieBase // 3.
    {
        [Key] /* Nicht zwingend notwendig, da implizit als Schlüssel erkannt */
        public Guid Id { get; set; }

        [Required]
        [MaxLength(128)]
        public string Title { get; set; }

        [Column(TypeName = "date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        //public DateOnly ReleaseDate { get; set; } = > Als Alternative seit .NET 6
        public DateTime ReleaseDate { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        public decimal Price { get; set; }

        public int GenreId { get; set; }

        [MaxLength(8)]
        public string MediumTypeCode { get; set; }
    }
}
