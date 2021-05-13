using ADMPublishers.DataAccess.Repository.IRepository;
using ADMPublishers.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADMPublishers.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AuthorController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public AuthorController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            Author author = new Author();
            if (id == null)
            {
                //this is for create
                return View(author);
            }
            //this is for edit
            author = _unitOfWork.Author.Get(id.GetValueOrDefault());
            if (author == null)
            {
                return NotFound();
            }
            return View(author);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]//Prevent cross site request fogery: form from another site submitting hidden contet to your site pretending to be authenticated, check if cookie value matches
        public IActionResult Upsert(Author author)
        {
            if (ModelState.IsValid)
            {
                if (author.Id == 0)
                {
                    _unitOfWork.Author.Add(author);
                }
                else
                {
                    _unitOfWork.Author.Update(author);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(author);
        }

        //API Calls
        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var allObj = _unitOfWork.Author.GetAll();
            return Json(new { data = allObj });
        }

        [HttpGet]
        public IActionResult GetAllArray()
        {
            var allObj = _unitOfWork.Author.GetAll();
            return Json(allObj);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.Author.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting and Author" });
            }
            _unitOfWork.Author.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Author Deleted Successful" });
        }
        #endregion API CALLS
    }
}
