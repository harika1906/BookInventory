using Inventory.Models;
using Inventory.Repository;
using static System.Reflection.Metadata.BlobBuilder;


namespace Inventory.Service
{
    public class BookStoreService : IBookStoreService
    {
        private readonly IBookStoreRepository _storeRepository;
        public BookStoreService(IBookStoreRepository storeRepository)
        {
            this._storeRepository = storeRepository;
        }
        public List<BookStore> GetBooks()
        {
            return _storeRepository.GetBooks();
        }
        public void AddBooks(BookStore store)
        {
            _storeRepository.AddBooks(store);
        }
        public void EditBookStore(BookStore store)
        {

            _storeRepository.EditBookStore(store);

        }

        public void DeleteBookStore(int id)
        {
            _storeRepository.DeleteBookStore(id);
        }

        public List<BookStore> FilterBookStore( string searchBy, string search)
        {
            var books = GetBooks();
            if (searchBy == "Title")
            {
                var book = books.Where(x => x.Title.ToLower() == search.ToLower() || search == null);
                return (book.ToList());


            }
            else if (searchBy == "Price")
            {
                return (books.Where(x => x.Price.Equals(search) || search == null).ToList()); 
                       
            }
            else if (searchBy == "Author")
            {
                return (books.Where(x => x.Author == search || search == null).ToList());

            }
            else if (searchBy == "ISBN")
            {
                return (books.Where(x => x.ISBN.Equals(search) || search == null).ToList()); 
            }
            return default;
        }
    }


    public interface IBookStoreService
    {
        List<BookStore> GetBooks();
        void AddBooks(BookStore store);
        List<BookStore> FilterBookStore(string search, string searchBy);
        void EditBookStore(BookStore store);
        void DeleteBookStore(int id);
    }
}
