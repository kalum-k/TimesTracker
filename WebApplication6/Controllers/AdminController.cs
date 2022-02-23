using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Linq;
using TimeTracker.Areas.Identity.Models;
using WebApplication6.Areas.Identity.Data;
using WebApplication6.Data;

namespace TimeTacker.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly TimeTrackerDbContext _dbContext;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AdminController(TimeTrackerDbContext timeTacker, RoleManager<IdentityRole> roleManager)
        {

            _dbContext = timeTacker;
            _roleManager = roleManager;
        }

        //User time in day
        public IActionResult Index()
        {
            ViewData["countDate"] = _dbContext.timeTackers.Count(u => u.CurrentDate == DateTime.Today).ToString();
            ViewData["countMonth"] = _dbContext.timeTackers.Count(m => m.CurrentDate.Month == DateTime.Now.Month).ToString();
            //find who check time today
            var date = _dbContext.timeTackers
                .Where(d => d.CurrentDate == DateTime.Today)
                .OrderByDescending(a => a.CurrentDate);
            return View(date);
        }

        public IActionResult IndexMonth()
        {
            ViewData["countDate"] = _dbContext.timeTackers.Count(u => u.CurrentDate == DateTime.Today).ToString();
            ViewData["countMonth"] = _dbContext.timeTackers.Count(m => m.CurrentDate.Month == DateTime.Now.Month).ToString();
            var date = _dbContext.timeTackers
                .Where(d => d.CurrentDate.Month == DateTime.Now.Month)
                .OrderByDescending(a => a.Id);
            return View(date);
        }

        //show all user
        public IActionResult GetUser()
        {
            IEnumerable<TimeSystemUser> users = _dbContext.Users.OrderBy(x => x.FirstName).ToList();
            return View(users);
        }
        public IActionResult GetUserFilter2()
        {
            IEnumerable<TimeSystemUser> users = _dbContext.Users.OrderBy(x => x.FirstName).ToList();
            return View(users);
        }

        //Get User Time
        public IActionResult GetUserTime(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var timeFromDbFirst = _dbContext.timeTackers
                .Where(u => u.IdUser == id)
                //.Take(10)
                .OrderByDescending(y => y.Id);
            if (timeFromDbFirst == null)
            {
                return NotFound();
            }
            return View(timeFromDbFirst);
        }

        public IActionResult GetUserTimeMonth(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var timeFromDbFirst = _dbContext.timeTackers
                .Where(u => u.IdUser == id && u.CurrentDate.Month == DateTime.Now.Month)
                //.Take(10)
                .OrderByDescending(y => y.Id);
            if (timeFromDbFirst == null)
            {
                return NotFound();
            }
            return View(timeFromDbFirst);
        }

        //Role
        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(RoleModel role)
        {
            var roleExisist = await _roleManager.RoleExistsAsync(role.RoleName);
            if (!roleExisist)
            {
                var results = await _roleManager.CreateAsync(new IdentityRole(role.RoleName));
            }
            return View();
        }

        //GET
        //find n Edit time of user
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
        public IActionResult Edit(TimeTrackers timeTacker)
        {
            if (ModelState.IsValid)
            {
                _dbContext.timeTackers.Update(timeTacker);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(timeTacker);
        }

        //GET
        //Edit Profile User
        public IActionResult EditProfileUser(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var timeFromDb = _dbContext.user.Find(id);

            if (timeFromDb == null)
            {
                return NotFound();
            }

            return View(timeFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditProfileUser(TimeSystemUser user)
        {
            if (ModelState.IsValid)
            {

                _dbContext.user.Update(user);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }



        //find n delete user
        public IActionResult DeleteUser(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var timeFromDb = _dbContext.user.Find(id);

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
            var obj = _dbContext.user.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            _dbContext.user.Remove(obj);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");

        }


    }
}
