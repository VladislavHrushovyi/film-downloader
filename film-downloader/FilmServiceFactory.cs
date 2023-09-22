using film_downloader.FilmServices;

namespace film_downloader;

public class FilmServiceFactory
{
    public IFilmService GetFilmService(FilmServiceTypes type, string path)
    {
        return type switch
        {
            FilmServiceTypes.HdRezka => new RezkaSrvice(path),
            FilmServiceTypes.UaKinoClub => new UaKinoService(path),
            _ => throw new TypeAccessException("Invalid service type")
        };
    }
    
    public IFilmService GetFilmService(int type, string path)
    {
        return type switch
        {
            (int)FilmServiceTypes.HdRezka => new RezkaSrvice(path),
            (int)FilmServiceTypes.UaKinoClub => new UaKinoService(path),
            _ => throw new TypeAccessException("Invalid service type")
        };
    }
}