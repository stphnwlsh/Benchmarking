namespace Benchmarking.Models.Cat;

public class SearchResult
{
    public Breed[] breeds { get; set; }
    public string id { get; set; }
    public string url { get; set; }
    public int width { get; set; }
    public int height { get; set; }
}