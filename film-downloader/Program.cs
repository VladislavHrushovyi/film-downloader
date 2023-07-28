
using film_downloader;
using film_downloader.FilmServices;

Console.WriteLine("Choose film service:");
Console.WriteLine("1. HdRezka.ag");
Console.WriteLine("2. UaKino.club");

int chooseService = Int32.Parse(Console.ReadLine() ?? string.Empty);

Console.WriteLine("Enter path to film:");
string? path = Console.ReadLine();

if (path != null)
{
    IFilmService filmService = chooseService switch
    {
        1 => new RezkaSrvice(path),
        2 => new UaKinoService(path),
        _ => throw new ArgumentException("Invalid choose")
    };

    var downloader = new DownloaderFilm(filmService);
    await downloader.Download();
}
else
{
    Console.WriteLine("ERROR PATH DOES NOT CAN BE EMPTY");
}

Console.WriteLine("Press any button");
Console.ReadKey();