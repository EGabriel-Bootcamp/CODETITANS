namespace CodeTitansLibrary.Entities
{
    public class Publisher
    {
        public int Id { get; set; }
        public string PublisherName { get; set; }
        public string YearPublished { get; set; } = DateTime.Now.Year.ToString();
    }
}
