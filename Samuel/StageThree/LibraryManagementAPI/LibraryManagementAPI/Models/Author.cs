namespace LibraryManagementAPI.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }


        //Relationship
        public int PublisherId { get; set; }
        public Publisher Publisher { get; set; }
    }
}
