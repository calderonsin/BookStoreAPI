namespace BookStoreAPI.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
       
        public decimal Price { get; set; }
        public int QuantityAvailable { get; set; }
        public ICollection<Author> Authors { get; set; }
        public ICollection<AuthorBook> AuthorBooks { get; set; }
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
    }
}
