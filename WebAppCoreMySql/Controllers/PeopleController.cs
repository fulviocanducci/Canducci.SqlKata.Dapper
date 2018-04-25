using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Canducci.SqlKata.Dapper.MySql;
using Models;
namespace WebAppCoreMySql.Controllers
{
    public class PeopleController : Controller
    {
        public MySqlConnection Database { get; }

        public PeopleController(MySqlConnection database)
        {            
            Database = database;
        }
        
        public ActionResult Index()
        {
            return View(Database.Query("Peoples").List<People>());
        }
                
        public ActionResult Details(int id)
        {
            return View(Database.Find<People>(id));
        }
                
        public ActionResult Create()
        {
            return View();
        }
                
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(People people)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    people = Database.Insert(people);
                    return RedirectToAction(nameof(Edit), new { people.Id });
                }                
            }
            catch
            {
                return View();
            }
            return View();
        }
                
        public ActionResult Edit(int id)
        {
            var people = Database.Find<People>(id);
            if (people != null)
            {
                return View(people);
            }
            return RedirectToAction(nameof(Index));
        }
                
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, People people)
        {
            try
            {
                if (Database.Update(people))
                {
                    return RedirectToAction(nameof(Edit), new { people.Id });
                }                
            }
            catch
            {
                return View();
            }
            return View();
        }
                
        public ActionResult Delete(int id)
        {
            var people = Database.Find<People>(id);
            if (people != null)
            {
                return View(people);
            }
            return RedirectToAction(nameof(Index));
        }
                
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, People people)
        {
            try
            {
                if (Database.Delete(people))
                {
                    return RedirectToAction(nameof(Index));
                }                
            }
            catch
            {
                return View();
            }
            return NotFound();
        }
    }
}