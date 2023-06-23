using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Inventory.Data;
using Inventory.Models;
using Microsoft.AspNetCore.Authorization;
using Inventory.Data.Migrations;
using static System.Reflection.Metadata.BlobBuilder;
using Inventory.Service;

namespace Inventory.Controllers
{
    public class BookStoreController : Controller
    {
        private readonly IBookStoreService _storeService;

        public BookStoreController(IBookStoreService storeService)
        {
            _storeService = storeService;
        }
        public IActionResult Index()
        {
            var books = _storeService.GetBooks();
            return View(books);
        }
        //GET: BookStore
        public ActionResult FilterBooks(string searchBy, string search)
        {
             _storeService.FilterBookStore(searchBy, search);
            
            return View(new List<BookStore>());
        }
        

        public IActionResult Search()
        {
            var books = _storeService.GetBooks();
            return books != null ?
                        View(books.ToList()) :
                        Problem("Entity set 'ApplicationDbContext.BookStore'  is null.");
        }

        [HttpPost]
        public IActionResult Search(string searchterm)
        {
            var books = _storeService.GetBooks();
            return books != null ?
                        View("Index", books.Where(x => x.Title.Contains(searchterm)).ToList()) :
                        Problem("Entity set 'ApplicationDbContext.BookStore'  is null.");

        }


        // GET: BookStore/Details/5
        public async Task<IActionResult> Details(int? id)
        {

            var books = _storeService.GetBooks();
            if (id == null || books == null)
            {
                return NotFound();
            }

            var bookStore = books
                .FirstOrDefault(m => m.Id == id);
            if (bookStore == null)
            {
                return NotFound();
            }

            return View(bookStore);
        }
        [Authorize]
        // GET: BookStore/Create
        public IActionResult Create()
        {
            return View();
        }
        [Authorize]
        // POST: BookStore/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Title,Author,Price,ISBN,QuantityAvailable")] BookStore bookStore)
        {
            if (ModelState.IsValid)
            {
                _storeService.AddBooks(bookStore);
                return RedirectToAction(nameof(Index));
            }
            return View(bookStore);

        }
        [Authorize]
        // GET: BookStore/Edit/5
        public IActionResult Edit(int? id)
        {
            var books = _storeService.GetBooks();
            if (id == null || books == null)
            {
                return NotFound();
            }

            //var bookStore = await _context.BookStore.FindAsync(id);
            var bookStore = books.Where(x => x.Id == id).FirstOrDefault();
            if (bookStore == null)
            {
                return NotFound();
            }
            return View(bookStore);
        }
        [Authorize]
        // POST: BookStore/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(BookStore bookStore)
        {
            _storeService.EditBookStore(bookStore);
            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        // GET: BookStore/Delete/5
        public IActionResult Delete(int? id)
        {
            var books = _storeService.GetBooks();
            if (id == null || books == null)
            {
                return NotFound();
            }

            var bookStore = books.Where
                (m => m.Id == id).FirstOrDefault();
            if (bookStore == null)
            {
                return NotFound();
            }

            return View(bookStore);
        }
        [Authorize]
        // POST: BookStore/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id, BookStore store)
        {

            try
            {
                _storeService.DeleteBookStore(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

            }
            return View();

            //await _context.SaveChangesAsync();

        }
        //[Authorize]
        private bool BookStoreExists(int id)
        {
            var books = _storeService.GetBooks();
            return (books?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }

}
