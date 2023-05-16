using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.Extensions.Configuration;
using Wifi.SD.Core.Entities.Movies;

namespace SD.Persistence.Repositories.DBContext
{
    public class MovieDbContext : DbContext
    {
        public MovieDbContext() { }

        public MovieDbContext(DbContextOptions<MovieDbContext> options) : base(options)
        {
            Database.SetCommandTimeout(90);
        }

        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<MediumType> MediumTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>(entity =>
            {
                entity.ToTable(nameof(Movie) + "s");
                //entity.HasKey(e = entity.Id); Schlüssel definieren hier nicht notwendig, da implizit
                entity.Property(p => p.Title).IsRequired().HasMaxLength(128);
                entity.HasIndex(p => p.Title).HasDatabaseName("IX_" + nameof(Movie) + "s_" + nameof(Movie.Title));
                entity.Property(p => p.ReleaseDate).HasColumnType("date");
                entity.Property(p => p.Price).HasPrecision(18, 2).HasDefaultValue(0M); // wichtig für differenzen wie €

            });

            modelBuilder.Entity<MediumType>(entity =>
            {
                entity.ToTable(nameof(MediumType) + "s").HasKey(nameof(MediumType.Code));
            });

            // Foreign Key Constaint 0 : n
            modelBuilder.Entity<Movie>()
                .HasOne(m => m.MediumType)
                .WithMany(m => m.Movies)
                .HasForeignKey(m => m.MediumTypeCode)
                .OnDelete(DeleteBehavior.ClientSetNull) /* Löschweitergabe => Wert in Movie auf NULL setzen */
                .IsRequired(false);

            // Foreign Key Constaint 1 : n
            modelBuilder.Entity<Genre>().ToTable(nameof(Genre) + "s")
                .HasMany(g => g.Movies)
                .WithOne(g => g.Genre)
                .HasForeignKey(g => g.GenreId)
                .OnDelete(DeleteBehavior.Restrict);

            // Seed - Default Daten schreiben

            modelBuilder.Entity<Genre>().HasData(
                new Genre { Id = 1, Name = "Action" },
                new Genre { Id = 2, Name = "Drama" },
                new Genre { Id = 3, Name = "Science Fiction" },
                new Genre { Id = 4, Name = "Comedy" },
                new Genre { Id = 5, Name = "Thriller" }
                );

            modelBuilder.Entity<MediumType>().HasData(
                new MediumType { Code = "VHS", Name = "Videokassette" },
                new MediumType { Code = "DVD", Name = "Digital Versitale Disc" },
                new MediumType { Code = "BR", Name = "Blu-Ray" },
                new MediumType { Code = "BR3D", Name = "3D Blu-Ray" },
                new MediumType { Code = "BRHD", Name = "HD Blu-Ray" },
                new MediumType { Code = "BR4K", Name = "4K Blu-Ray" }
                );

            modelBuilder.Entity<Movie>().HasData(

                new Movie
                {
                    Id = new Guid("19066b7c-431a-40f6-9f47-a463955f337f"),
                    Title = "Rambo",
                    Price = 4.9M,
                    MediumTypeCode = "VHS",
                    ReleaseDate = new DateTime(1985, 4, 13),
                    GenreId = 1
                },

                new Movie
                {
                    Id = new Guid("75b8cac2-19e8-4acb-a406-def948c666f2"),
                    Title = "Star Trek - Beyond",
                    Price = 12.99M,
                    MediumTypeCode = "BR3D",
                    ReleaseDate = new DateTime(2016, 5, 30),
                    GenreId = 3
                },

                new Movie
                {
                    Id = new Guid("6a419262-20c5-4626-927f-d181f8a34f68"),
                    Title = "Star Wars - Episode IV",
                    Price = 99.99M,
                    MediumTypeCode = "DVD",
                    ReleaseDate = new DateTime(1987, 4, 13),
                    GenreId = 3
                },

                new Movie
                {
                    Id = new Guid("83a72fa7-f8f7-406b-b832-a42e695c2ce6"),
                    Title = "Dexter",
                    Price = 8.50M,
                    MediumTypeCode = "BR",
                    ReleaseDate = new DateTime(2005, 10, 12),
                    GenreId = 5
                },

                

new Movie
{
    Id = new Guid("a8f1e4f8-14e1-4f0e-8f3b-4f7b44a8be30"),
    Title = "Interstellar",
    Price = 10.99M,
    MediumTypeCode = "BR",
    ReleaseDate = new DateTime(2014, 11, 7),
    GenreId = 2
},

    new Movie
    {
        Id = new Guid("b23e5e6f-9a67-4a74-8231-2d3b6d384a0a"),
        Title = "The Dark Knight",
        Price = 14.99M,
        MediumTypeCode = "DVD",
        ReleaseDate = new DateTime(2008, 7, 18),
        GenreId = 1
    },

    new Movie
    {
        Id = new Guid("c1c1f6b7-1c29-4c9e-a6f2-0b582b5b8a13"),
        Title = "Inception",
        Price = 9.99M,
        MediumTypeCode = "BR",
        ReleaseDate = new DateTime(2010, 7, 16),
        GenreId = 2
    },

    new Movie
    {
        Id = new Guid("d3f2b0a7-836c-4b44-a1a9-9d2b4b4a3b30"),
        Title = "The Lord of the Rings: The Return of the King",
        Price = 15.99M,
        MediumTypeCode = "BR",
        ReleaseDate = new DateTime(2003, 12, 17),
        GenreId = 4
    },


    new Movie
    {
        Id = new Guid("de2a6f8d-1c4e-4c5e-b9f2-0a2b6b4a5b39"),
        Title = "Inglourious Basterds",
        Price = 7.99M,
        MediumTypeCode = "DVD",
        ReleaseDate = new DateTime(2009, 8, 21),
        GenreId = 1
    },

    new Movie
    {
        Id = new Guid("ef2b7b9a-7d3d-4f4d-8b2b-0b3b6b5b6b4a"),
        Title = "Pulp Fiction",
        Price = 7.99M,
        MediumTypeCode = "DVD",
        ReleaseDate = new DateTime(1994, 10, 14),
        GenreId = 1
    });


        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var currentDirectory = Directory.GetCurrentDirectory();
#if DEBUG
            /* Verzeichnis bis \bin kürzen */
            if (currentDirectory.IndexOf("bin") > -1)
            {
                currentDirectory = currentDirectory.Substring(0, currentDirectory.IndexOf("bin"));
            }

#endif
            var configurationBuilder = new ConfigurationBuilder()
                                         .SetBasePath(currentDirectory)
                                         .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            var configuration = configurationBuilder.Build();
            var connectionString = configuration.GetConnectionString(nameof(MovieDbContext));
            optionsBuilder.UseSqlServer(connectionString, opts => opts.CommandTimeout(60));
        }
    }
}
