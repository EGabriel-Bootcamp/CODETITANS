namespace LibraryManagementAPI.Models
{
    public class Publisher
    {
        public int Id { get; set; }
        public string PublisherName { get; set; }
        public string YearPublished { get; set; }


        //Relationship
        public List<Author> Author { get; set; }
    }
}
