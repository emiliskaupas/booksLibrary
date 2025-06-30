public class OpenLibraryService
{
    private readonly HttpClient _httpClient;
    private const string BaseUrl = "https://openlibrary.org";

    public OpenLibraryService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<BookSearchResult> SearchBooksAsync(string query, int page = 1, int pageSize = 20)
    {
        var url = $"{BaseUrl}/search.json?q={Uri.EscapeDataString(query)}&page={page}&limit={pageSize}";
        
        var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();
        
        var json = await response.Content.ReadAsStringAsync();
        var searchResponse = JsonSerializer.Deserialize<OpenLibrarySearchResponse>(json);
        
        // Convert to your Book models
        var books = searchResponse.Docs.Select(ConvertToBook).ToList();
        
        return new BookSearchResult
        {
            Books = books,
            TotalResults = searchResponse.NumFound,
            Page = page,
            PageSize = pageSize
        };
    }

    private Book ConvertToBook(OpenLibraryDoc doc)
    {
        return new Book
        {
            Title = doc.Title,
            Author = doc.Author_Name?.FirstOrDefault() ?? "Unknown",
            Year = doc.First_Publish_Year,
            Isbn = doc.Isbn?.FirstOrDefault(),
            PageCount = doc.Number_Of_Pages_Median,
            Publisher = doc.Publisher?.FirstOrDefault(),
            Subjects = doc.Subject,
            CoverUrl = doc.Cover_I > 0 ? $"https://covers.openlibrary.org/b/id/{doc.Cover_I}-M.jpg" : null
        };
    }
}