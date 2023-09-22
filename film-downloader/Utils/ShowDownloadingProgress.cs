namespace film_downloader.Utils;

public class ShowDownloadingProgress
{
    public static void ShowProgress(double downloadedMb, double currSpeed, int amountSeg)
    {
        Console.Clear();
        Console.WriteLine($"Downloaded: {Math.Round(downloadedMb, 2)} Mb");
        Console.WriteLine($"Speed: {Math.Round(currSpeed, 2)} Mb/s");
        Console.WriteLine($"Count of segments: {amountSeg}");
    }
}