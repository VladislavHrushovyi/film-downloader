using film_downloader.FilmServices;

namespace film_downloader;

public class DownloaderFilm
{
    private readonly IFilmService _filmService;
    private readonly BinaryWriter _filmWriter;
    private readonly HttpClient _httpClient;
    public DownloaderFilm(IFilmService filmService)
    {
        _filmService = filmService;
        _filmWriter = new BinaryWriter(File.OpenWrite($"film-{Guid.NewGuid()}.mp4"));
        _httpClient = new HttpClient();
    }

    public async Task Download()
    {
        int seg = 1;
        while (true)
        {
            var result = await _httpClient.GetAsync(_filmService.BuildPath(seg));
            if (!result.IsSuccessStatusCode)
            {
                break;
            }
            var response = await result.Content.ReadAsByteArrayAsync();
            Console.WriteLine($"SEGMENT {seg} downloaded");
            _filmWriter.Write(response);
            _filmWriter.Flush();
            Console.WriteLine($"SEGMENT {seg} is wrote");
            seg++;
        }
        _filmWriter.Close();
    }
}