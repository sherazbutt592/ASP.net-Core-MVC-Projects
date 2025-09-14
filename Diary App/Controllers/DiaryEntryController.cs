using Diary_App.Data;
using Diary_App.Models;
using Microsoft.AspNetCore.Mvc;

namespace Diary_App.Controllers
{
    public class DiaryEntryController: Controller
    {
        private readonly ApplicationDbContext _db;
        public DiaryEntryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<DiaryEntry> entriesList = _db.DiaryEntries.ToList();
            return View(entriesList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(DiaryEntry obj)
        {

            if (obj != null && obj.Title.Length < 3)
            {
                ModelState.AddModelError("Title", "Title is too short!");
                return View(obj);
            }
            if (ModelState.IsValid)
            {
                _db.DiaryEntries.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index", "DiaryEntry");
            }
            return View(obj);
        }
        [HttpGet]
        public IActionResult Edit(int id) { 
            DiaryEntry diaryEntry = _db.DiaryEntries.Find(id);
            if(id == null || id ==0)
            {
                return NotFound();
            }
            if(diaryEntry== null)
            {
                return NotFound();
            }
            return View(diaryEntry);
        }
        [HttpPost]
        public IActionResult Edit(DiaryEntry obj)
        {

            if (obj != null && obj.Title.Length < 3)
            {
                ModelState.AddModelError("Title", "Title is too short!");
                return View(obj);
            }
            if (ModelState.IsValid)
            {
                _db.DiaryEntries.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index", "DiaryEntry");
            }
            return View(obj);
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
           DiaryEntry obj = _db.DiaryEntries.Find(id);
            if (id == null || id == 0)
            {
                return NotFound();
            }
            if (obj == null)
            {
                return NotFound();
            }
            _db.DiaryEntries.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index", "DiaryEntry");
        }
    }
}
