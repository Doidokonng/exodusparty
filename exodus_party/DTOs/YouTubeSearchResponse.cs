namespace exodus_party.DTOs
{
    public class YouTubeSearchResponse
    {
        public List<YouTubeItem> Items { get; set; }
    }

    public class YouTubeItem 
    {
        public YouTubeId Id  { get; set; }
        public YouTubeSnippet Snippet { get; set; }
    }

    public class YouTubeId 
    {
        public string VideoId { get; set; }
    }

    public class YouTubeSnippet 
    {
        public string Title { get; set; }
        public string ChannelTitle { get; set; }
    }

}
