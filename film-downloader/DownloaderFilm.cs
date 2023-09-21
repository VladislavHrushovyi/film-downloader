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
        Stopwatch watch = new Stopwatch();
        while (true)
        {
            watch.Start();
            var segmentBytes = await GetFilmSegmentBytes(seg);
            watch.Stop();
            if (!segmentBytes.Any())
            {
                break;
            }
            
            var responseWeightMb = (double)segmentBytes.Length / (1024 * 1024);
            weightFilmMb += responseWeightMb;
            var currSpeed = responseWeightMb / watch.Elapsed.TotalSeconds;

            _filmWriter.Write(segmentBytes);
            _filmWriter.Flush();
            seg++;
            watch.Reset();

            Console.Clear();
            Console.WriteLine($"Downloaded: {Math.Round(weightFilmMb, 2)} Mb");
            Console.WriteLine($"Speed: {Math.Round(currSpeed, 2)} Mb/s");
            Console.WriteLine($"Count of segments: {seg}");
        }

        _filmWriter.Close();
    }

    public async Task DownLoadParallel()
    {
        int startSeg = 1;
        int offsetPart = 10;
        var st = new Stopwatch();
        double weightFilmMb = 0;
        while (true)
        {
            st.Start();
            var downloadTasks = Enumerable.Range(0, offsetPart).Select(_ => GetFilmSegmentBytes(startSeg++));
            var resultsFromTasks = await Task.WhenAll(downloadTasks);
            st.Stop();
            var contentsFromTasks = resultsFromTasks.Where(x => x.Any()).SelectMany(x => x).ToArray();
            if (contentsFromTasks.Length == 0)
            {
                break;
            }
            var weightContents = contentsFromTasks.Length / (1024d * 1024d);
            weightFilmMb += weightContents;
            var currSpeed = weightContents / st.Elapsed.TotalSeconds;

            _filmWriter.Write(contentsFromTasks);
            _filmWriter.Flush();
            st.Reset();

            Console.Clear();
            Console.WriteLine($"Downloaded: {Math.Round(weightFilmMb, 2)} Mb");
            Console.WriteLine($"Speed: {Math.Round(currSpeed, 2)} Mb/s");
            Console.WriteLine($"Count of segments: {startSeg - 1}");
        }
    }

    private async Task<byte[]> GetFilmSegmentBytes(int seg)
    {
        var result = await _httpClient.GetAsync(_filmService.BuildPath(seg));
        if (!result.IsSuccessStatusCode)
        {
            return Array.Empty<byte>();
        }
        var response = await result.Content.ReadAsByteArrayAsync();

        return response;
    }
}