using System.Data;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using SqlKata;
using Canducci.SqlKata.Dapper.SqlServer;
using Canducci.SqlKata.Dapper.Extensions.SoftBuilder;

namespace WebAppSqlServer.Controllers
{
    public class CreditsController : Controller
    {
        public IDbConnection Connection { get; }
        public Query Query { get; }

        protected object GetValues(Credit credit)
        {
            return new
            {
                description = credit.Description,
                created = credit.Created
            };
        }

        public CreditsController(IDbConnection connection)
        {
            Connection = connection;
            Query = connection
                .SoftBuild()
                .From("credit");
        }

        public async Task<ActionResult> Index()
        {
            var model = await Query.ListAsync<Credit>();
            return View(model);
        }

        public ActionResult Details(int id)
        {
            var model = Query
                .Where("id", id)
                .FindOne<Credit>();
            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Credit credit)
        {
            try
            {
                credit.Id = Query
                    .AsInsert(GetValues(credit), true)
                    .SaveInsert<int>();

                return RedirectToAction(nameof(Edit), new { id = credit.Id });
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            var model = Query
                .Where("id", id)
                .FindOne<Credit>();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Credit credit)
        {
            try
            {
                Query
                    .AsUpdate(GetValues(credit))
                    .Where("id", credit.Id)
                    .SaveUpdate();

                return RedirectToAction(nameof(Edit), new { id });
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            var model = Query
                .Where("id", id)
                .FindOne<Credit>();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                Query
                    .AsDelete()
                    .Where("id", id)
                    .UniqueResultToInt();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}