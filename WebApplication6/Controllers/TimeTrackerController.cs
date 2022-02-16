using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication6.Areas.Identity.Data;
using WebApplication6.Data;


namespace TimeTracker.Controllers
{
    [Authorize(Roles = "Admin,Users")]
    public class TimeTrackerController : Controller
    {
        private readonly TimeTrackerDbContext _dbContext;
        public TimeTrackerController(TimeTrackerDbContext timeTacker)
        {
            _dbContext = timeTacker;
        }
        public IActionResult Index()
        {
            return View();
        }
        [TempData]
        public string StatusMessage { get; set; }

        public async Task<IActionResult> Timetracker(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var timeFromDbFirst = _dbContext.timeTackers
                .Where(u => u.IdUser == id)
                .OrderByDescending(y => y.Id).Take(10);

            if (timeFromDbFirst == null)
            {
                return NotFound();
            }
            return View(timeFromDbFirst);
        }

        public async Task<IActionResult> TimetrackerMonth(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var timeFromDbFirst = _dbContext.timeTackers
                .Where(u => u.IdUser == id && u.CurrentDate.Month == DateTime.Now.Month)
                .OrderByDescending(y => y.Id).Take(10);

            if (timeFromDbFirst == null)
            {
                return NotFound();
            }
            return View(timeFromDbFirst);
        }



        //Get
        public IActionResult Create()
        {
            return View("Create");
        }


        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TimeTrackers timeTacker)
        {
            if (ModelState.IsValid)
            {
                if (timeTacker.CurrentDate != DateTime.Today)
                {
                    TempData["err"] = "Not Successfully";
                    return NotFound();
                }
                else
                {

                    _dbContext.Add(timeTacker);
                    _dbContext.SaveChanges();
                    StatusMessage = "Your time has been Created";
                    return RedirectToAction("Index");
                }

            }
            return View(timeTacker);
        }


        //GET
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var timeFromDb = _dbContext.timeTackers.Find(id);

            if (timeFromDb == null)
            {
                return NotFound();
            }

            return View(timeFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(TimeTrackers timeTackers)
        {
            if (ModelState.IsValid)
            {

                _dbContext.timeTackers.Update(timeTackers);
                _dbContext.SaveChanges();
                StatusMessage = "Your time has been updated";
                return RedirectToAction("Index");
            }
            return View(timeTackers);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var timeFromDb = _dbContext.timeTackers.Find(id);

            if (timeFromDb == null)
            {
                return NotFound();
            }

            return View(timeFromDb);
        }

        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _dbContext.timeTackers.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            _dbContext.timeTackers.Remove(obj);
            _dbContext.SaveChanges();
            StatusMessage = "Your time has been Deleted";
            return RedirectToAction("Index");

        }
        public IActionResult Port(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var port = _dbContext.timeTackers.Where(u => u.IdUser == id);
            if (port == null)
            {
                return NotFound();
            }

            return View(port);
        }

    }
}
