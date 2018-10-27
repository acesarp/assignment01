using System.Linq;
using System.Threading.Tasks;
using Assignment01_ASP.Data;
using Assignment01_ASP.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Assignment01_ASP.Controllers {

    public class UserController : Controller {

        private readonly ApplicationDbContext _context;
        public static UserManager<AppUser> userManager;

        public UserController(ApplicationDbContext context) {
            _context = context;
        }

        public IActionResult Index() {
            return View();
        }

        public IActionResult CompoundInterest() {
            InterestCalculator calc = new InterestCalculator();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CompoundInterest(
            [Bind("Principal, Rate, Years, Compound")] InterestCalculator calc) {
            ViewData["result"] = calc.CompoundInterestCalc();
            return View(calc);
        }


        // GET: Users
        public async Task<IActionResult> UserList() {
            return View(await _context.AppUsers.ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(string id) {
            if(id == null) {
                return NotFound();
            }

            var user = await _context.AppUsers
                .FirstOrDefaultAsync(m => m.Id == id);
            if(user == null) {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create() {
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserName, Email, MobileNumber, Password, " +
                                                        "FirstName, LastName, Street, City, Province, " +
                                                          " PostalCode, Country")] AppUser user) {
            if(ModelState.IsValid) {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Create));
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(string id) {
            if(id == null) {
                return NotFound();
            }

            var user = await _context.AppUsers.FindAsync(id);
            if(user == null) {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("UserName, Email, MobileNumber, Password, " +
                                                        "FirstName, LastName, Street, City, Province, " +
                                                          " PostalCode, Country, IsAdmin")] AppUser user) {
            if(id != user.Id) {
                return NotFound();
            }
            string memberStatus;
            string newStatus;

            if(user.IsAdmin) {
                newStatus = "Admin";
                memberStatus = "Member";
            } else {
                newStatus = "Member";
                memberStatus = "Admin";
            }

            if(ModelState.IsValid) {
                try {    
                    _context.Update(user);
                    //var userRoles = _context.AppUsers
                    await userManager.AddToRoleAsync(user, newStatus);
                    await userManager.RemoveFromRoleAsync(user, memberStatus);
                    await _context.SaveChangesAsync();
                }
                catch(DbUpdateConcurrencyException) {
                    if(!UserExists(user.Id)) {
                        return NotFound();
                    } else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(UserList));
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(string id) {
            if(id == null) {
                return NotFound();
            }

            var user = await _context.AppUsers.FirstOrDefaultAsync(m => m.Id == id);
            if(user == null) {
                return NotFound();
            }
            return RedirectToAction(nameof(UserList));
            //return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id) {
            var user = await _context.AppUsers.FindAsync(id);
            _context.AppUsers.Remove(user);
            await _context.SaveChangesAsync();
            await DeleteConfirmed(id);

            return RedirectToAction(nameof(UserList));
        }

        private bool UserExists(string id) {
            return _context.AppUsers.Any(e => e.Id == id);
        }

    }
}