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


        public IActionResult Index()
        {
            //find who check time today
            var date = _dbContext.timeTackers
                .Where(d => d.CurrentDate == DateTime.Today)
                .OrderByDescending(a => a.Id);
            return View(date);
        }

        

        public IActionResult IndexMonth()
        {
            var date = _dbContext.timeTackers
                //.Count(u => u.IdUser == null)
                .Where(d => d.CurrentDate.Month == DateTime.Now.Month)
                .OrderByDescending(a => a.Id);
            return View(date);
        }

        //show all user
        public IActionResult GetUser()
        {
            IEnumerable<TimeSystemUser> users = _dbContext.Users;
            return View(users);
        }
        public IActionResult GetUserTime(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var timeFromDbFirst = _dbContext.timeTackers
                .Where(u => u.IdUser == id)
                .Take(10)
                .OrderByDescending(y => y.Id);
            //.Include(y => y.user);
            if (timeFromDbFirst == null)
            {
                return NotFound();
            }
            return View(timeFromDbFirst);
        }

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


    }
}
