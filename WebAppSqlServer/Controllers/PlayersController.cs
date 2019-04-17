using System.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Canducci.SqlKata.Dapper.SqlServer;
using Canducci.SqlKata.Dapper.Extensions.SoftBuilder;
using SqlKata;
using Models;
using System.Threading.Tasks;
using System;

namespace WebAppSqlServer.Controllers
{
    public class PlayersController : Controller
    {
        public IDbConnection Connection { get; }
        public Query Query { get; }

        protected object GetValues(Player credit)
        {
            return new
            {
                credit.Description,
                credit.Created
            };
        }

        public PlayersController(IDbConnection connection)
        {
            Connection = connection;
            Query = connection
                .SoftBuild()
                .From("player");
        }

        public async Task<ActionResult> Index()
        {
            var model = await Query.ListAsync<Player>();
            return View(model);
        }

        public ActionResult Details(Guid id)
        {
            var model = Query
                .Where("id", id)
                .FindOne<Player>();
            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Player player)
        {
            try
            {
                player.Id = Query
                    .AsInsert(GetValues(player))
                    .SaveInsert<Guid>();

                return RedirectToAction(nameof(Edit), new { id = player.Id });
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        public ActionResult Edit(Guid id)
        {
            var model = Query
                .Where("Id", id)
                .FindOne<Player>();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, Player player)
        {
            try
            {
                Query
                    .AsUpdate(GetValues(player))
                    .Where("id", player.Id)
                    .SaveUpdate();

                return RedirectToAction(nameof(Edit), new { id });
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(Guid id)
        {
            var model = Query
                .Where("id", id)
                .FindOne<Player>();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, IFormCollection collection)
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