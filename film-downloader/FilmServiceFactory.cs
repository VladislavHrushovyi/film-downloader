using film_downloader.FilmServices;

namespace film_downloader;

public class FilmServiceFactory
{
    public IFilmService GetFilmService(FilmServiceTypes type, string path)
    {
        switch (type)
        {
            case FilmServiceTypes.HdRezka:
                return new RezkaSrvice(path);
            case FilmServiceTypes.UaKinoClub:
                return new UaKinoService(path);
            default:
                throw new TypeAccessException("Invalid service type");
        }
    }
    
    public IFilmService GetFilmService(int type, string path)
    {
        switch (type)
        {
            case (int)FilmServiceTypes.HdRezka:
                return new RezkaSrvice(path);
            case (int)FilmServiceTypes.UaKinoClub:
                return new UaKinoService(path);
            default:
                throw new TypeAccessException("Invalid service type");
        }
    }
}