using Inventory.Data;
using Inventory.Models;
using Microsoft.EntityFrameworkCore;
using static System.Reflection.Metadata.BlobBuilder;

namespace Inventory.Repository
{
    public class BookStoreRepository : IBookStoreRepository
    {
        private readonly ApplicationDbContext _context;
        public BookStoreRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<BookStore> GetBooks()
        {
            if (_context.BookStore == null)
            {
                Console.WriteLine("CouldNot Find Books");
            }
            else
                return _context.BookStore.ToList();
            return default;
        }

        public void AddBooks(BookStore store)
        {
            //var books = GetBooks();
            using (var transaction = _context.Database.BeginTransaction())
            {

                _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT BookStore ON");
                _context.BookStore.Add(store);

                _context.SaveChanges();
                _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT BookStore OFF");


                transaction.Commit();
            }



        }
        public void EditBookStore(BookStore store)
        {
            //AddBooks(store);
            _context.BookStore.Update(store);
            _context.SaveChanges();

        }
        public void DeleteBookStore(int id)
        {
            var book = _context.BookStore.FirstOrDefault(x => x.Id == id);
            _context.BookStore.Remove(book);
            _context.SaveChanges();
        }
    }

    public interface IBookStoreRepository
    {
        List<BookStore> GetBooks();
        void AddBooks(BookStore store);
        void EditBookStore(BookStore store);
        void DeleteBookStore(int id);
    }
}
