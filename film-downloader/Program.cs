
HttpClient client = new HttpClient();
BinaryWriter writer = new BinaryWriter(File.OpenWrite($"film-{Guid.NewGuid()}.mp4"));
int seg = 1;
Console.WriteLine("ENTER PATH TO FILM:");
var path = Console.ReadLine();
var splitPath = path.Split("-");
while (true)
{
    splitPath[splitPath.Length - 3] = seg.ToString();
    var result = await client.GetAsync(string.Join("-", splitPath));
    if (!result.IsSuccessStatusCode)
    {
        break;
    }
    var response = await result.Content.ReadAsByteArrayAsync();
    Console.WriteLine($"SEGMENT {seg} downloaded");
    writer.Write(response);
    writer.Flush();
    Console.WriteLine($"SEGMENT {seg} is wrote");
    seg++;
}
writer.Close();