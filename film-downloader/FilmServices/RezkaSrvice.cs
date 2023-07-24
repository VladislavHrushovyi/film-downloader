namespace film_downloader.FilmServices;

public class RezkaSrvice : IFilmService
{
    private readonly string[] _pathInfos;
    public RezkaSrvice(string path)
    {
        this._pathInfos = path.Split("-");
    }
    public string BuildPath(int seg)
    {
        _pathInfos[_pathInfos.Length - 3] = seg.ToString();

        return string.Join("-", _pathInfos);
    }
}