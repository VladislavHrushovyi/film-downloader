namespace film_downloader.FilmServices;

public class UaKinoService : IFilmService
{
    private readonly string[] _pathInfos;

    public UaKinoService(string path)
    {
        _pathInfos = path.Split("/");
    }
    
    public string BuildPath(int seg)
    {
        _pathInfos[^1] = $"segment{seg}.ts";
        
        return String.Join("/", _pathInfos);
    }
}