using BookStore.Models;
using BookStore.Models.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    public class AutherController : Controller
    {
        private readonly Models.Repositories.IBookStoreRepository<Auther> _autherStoreRepository;

        public AutherController(IBookStoreRepository<Auther> autherStoreRepository)
        {
            _autherStoreRepository = autherStoreRepository;
        }

        // GET: AutherController
        public ActionResult Index()
        {
            var authers = _autherStoreRepository.List();
            return View(authers);
        }

        // GET: AutherController/Details/5
        public ActionResult Details(int id)
        {
            var auther = _autherStoreRepository.Find(id);
            return View(auther);
        }

        // GET: AutherController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AutherController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Auther auther)
        {
            try
            {
                _autherStoreRepository.Add(auther);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AutherController/Edit/5
        public ActionResult Edit(int id)
        {
            var auther = _autherStoreRepository.Find(id);
            return View(auther);
            //return View();
        }

        // POST: AutherController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Auther auther)
        {
            try
            {
                _autherStoreRepository.Update(id, auther);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AutherController/Delete/5
        public ActionResult Delete(int id)
        {
            var auther = _autherStoreRepository.Find(id);
            return View(auther);
        }

        // POST: AutherController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Auther auther)
        {
            try
            {
                _autherStoreRepository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
