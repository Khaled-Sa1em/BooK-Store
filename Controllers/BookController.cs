using BookStore.Models;
using BookStore.Models.Repositories;
using BookStore.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{

    public class BookController : Controller
    {
        private readonly IBookStoreRepository<Book> _bookStoreRepository;
        private readonly IBookStoreRepository<Auther> _autherStoreRepository;
        private readonly IHostingEnvironment _hosting;

        public BookController(IBookStoreRepository<Book> bookStoreRepository, IBookStoreRepository<Auther> autherStoreRepository, IHostingEnvironment hosting)
        {
            _bookStoreRepository = bookStoreRepository;
            _autherStoreRepository = autherStoreRepository;
            _hosting = hosting;
        }
        // GET: BookController
        [HttpGet]
        public ActionResult Index(List<Book> result)
        {
            if (result.Count != 0)
            {
                return View(result);

            }
            var books = _bookStoreRepository.List();
            return View(books);
        }

        // GET: BookController/Details/5
        public ActionResult Details(int id)
        {
            var book = _bookStoreRepository.Find(id);
            return View(book);
        }

        // GET: BookController/Create
        public ActionResult Create()
        {

            var model = new BookAuthorViewModel()
            {
                Authers = _autherStoreRepository.List().ToList()
            };
            return View(model);
        }

        // POST: BookController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookAuthorViewModel bookVM)
        {
            var model = new BookAuthorViewModel()
            {
                Authers = _autherStoreRepository.List().ToList()
            };

            if (ModelState.IsValid)
            {
                try
                {
                    ////check  if user choose auther or not 
                    //if (bookVM.AutherId == 0)
                    //{
                    //    ViewData["Message"] = "-- PLZ choose an auther --";
                    //    model.AutherId = -1;
                    //    return View(model);
                    //}


                    // ! this steps to save an img in an images folder
                    string imgName = string.Empty;
                    if (bookVM.Img != null)
                    {
                        // path from system.IO

                        string imagesFolderPath = Path.Combine(_hosting.WebRootPath, "images");
                        imgName = bookVM.Img.FileName;
                        string imgFullPath = Path.Combine(imagesFolderPath, imgName);
                        bookVM.Img.CopyTo(new FileStream(imgFullPath, FileMode.Create));
                    }

                    _bookStoreRepository.Add(new Book
                    {
                        Title = bookVM.Title,
                        Description = bookVM.Description,
                        ImageUrl = imgName,
                        Auther = _autherStoreRepository.Find(bookVM.AutherId)
                    });
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
            }
            ModelState.AddModelError("", "plz fill all the required data");
            return View(model);
        }

        // GET: BookController/Edit/5
        public ActionResult Edit(int id)
        {
            var book = _bookStoreRepository.Find(id);
            // I could use mapper here to map from book to bookvm 
            //var im =
            var bookVM = new BookAuthorViewModel
            {
                BookId = book.Id,
                Title = book.Title,
                Description = book.Description,
                AutherId = book.Auther != null ? book.Auther.Id : 0,
                ImageUrl = book.ImageUrl,
                Authers = _autherStoreRepository.List().ToList(),
            };
            return View(bookVM);
        }

        // POST: BookController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BookAuthorViewModel bookVM)
        {
            try
            {
                string imgName = string.Empty;
                if (bookVM.Img != null)
                {
                    // path from system.IO
                    string imagesFolderPath = Path.Combine(_hosting.WebRootPath, "images");
                    imgName = bookVM.Img.FileName;
                    string imgFullPath = Path.Combine(imagesFolderPath, imgName);
                    //delete the old image or file 
                    string oldImageName = bookVM.ImageUrl;
                    string fullOldPath = Path.Combine(imagesFolderPath, oldImageName);
                    if (fullOldPath != imgFullPath)
                    {
                        System.IO.File.Delete(fullOldPath);
                        // save the new image or file 
                        bookVM.Img.CopyTo(new FileStream(imgFullPath, FileMode.Create));
                    }
                }

                var book = new Book
                {
                    Id = bookVM.BookId,
                    Title = bookVM.Title,
                    Description = bookVM.Description,
                    Auther = _autherStoreRepository.Find(bookVM.AutherId),
                    ImageUrl = imgName,
                };

                _bookStoreRepository.Update(bookVM.BookId, book);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BookController/Delete/5
        public ActionResult Delete(int id)
        {
            var book = _bookStoreRepository.Find(id);
            return View(book);
        }

        // POST: BookController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                _bookStoreRepository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Search(string searchPhrase)
        {
            if (searchPhrase == null || searchPhrase[0] == ' ')
            {
                return RedirectToAction("Index");
            }

            var result = _bookStoreRepository.SearchResult(searchPhrase);
            return View("Index", result);
        }
    }
}
