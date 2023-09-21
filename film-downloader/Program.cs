
using film_downloader;
using film_downloader.FilmServices;

Console.WriteLine("Choose film service:");
Console.WriteLine("1. HdRezka.ag");
Console.WriteLine("2. UaKino.club");

FilmServiceTypes chooseService = (FilmServiceTypes)Int32.Parse(Console.ReadLine() ?? string.Empty);

Console.WriteLine("Enter path to film:");
string? path = Console.ReadLine();

if (path != null)
{
    IFilmService filmService = new FilmServiceFactory().GetFilmService(chooseService, path);

    var downloader = new DownloaderFilm(filmService);
    await downloader.DownLoadParallel();
}
else
{
    Console.WriteLine("ERROR PATH DOES NOT CAN BE EMPTY");
}

Console.WriteLine("Press any button");
Console.ReadKey();