namespace film_downloader.FilmServices;

public interface IFilmService
{
    string BuildPath(int seg);
}