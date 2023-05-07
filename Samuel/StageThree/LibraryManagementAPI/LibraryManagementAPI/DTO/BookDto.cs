namespace LibraryManagementAPI.DTO
{
    public class BookDto
    {
        public string Title { get; set; }
        public decimal Price { get; set; }



        //Relationship
        public int AuthorId { get; set; }
    }
}
