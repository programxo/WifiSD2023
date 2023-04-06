using SD.Persistence.Repositories.Base;
using Wifi.SD.Core.Attributes;
using Wifi.SD.Core.Repositories;

namespace SD.Persistence.Repositories.Movies
{
    [MapServiceDependency(nameof(GenreRepository))]
    public class GenreRepository : BaseRepository, IGenreRepository
    {

    }
}
