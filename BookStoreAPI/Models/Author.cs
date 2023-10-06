namespace BookStoreAPI.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Book> Books { get; set; }
        public ICollection<AuthorBook> AuthorBooks { get; set; }


    }

    // Junction table
    public class AuthorBook
    {
        public int AuthorId { get; set; }
        public Author Author { get; set; } = null!;
        public int BookId { get; set; }
        public Book Book { get; set; } = null!;
    }
}
