namespace Project.Event
{
    public class SubtitleDetected
    {
        public string subtitleCategory;
        
        public SubtitleDetected(string _subtitleCategory)
        {
            subtitleCategory = _subtitleCategory;
        }
    }
}