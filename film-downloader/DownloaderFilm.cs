using System.Diagnostics;
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
        double weightFilmMb = 0;
        double currSpeed = 0;
        Stopwatch watch = new Stopwatch();
        while (true)
        {
            watch.Start();
            var result = await _httpClient.GetAsync(_filmService.BuildPath(seg));
            if (!result.IsSuccessStatusCode)
            {
                break;
            }
            var response = await result.Content.ReadAsByteArrayAsync();
            
            watch.Stop();
            var responseWeightMb = (double)response.Length / (1024 * 2);
            weightFilmMb += responseWeightMb;
            currSpeed = responseWeightMb / watch.Elapsed.TotalSeconds;
            
            _filmWriter.Write(response);
            _filmWriter.Flush();
            seg++;
            watch.Reset();
            
            Console.Clear();
            Console.WriteLine($"Download: {Math.Round(weightFilmMb, 2)} Mb");
            Console.WriteLine($"Speed: {Math.Round(currSpeed, 2)} Mb/s");
            Console.WriteLine($"Count of segments: {seg}");
        } 
        _filmWriter.Close();
    }
}