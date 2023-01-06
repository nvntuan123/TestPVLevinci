using Microsoft.AspNetCore.Mvc;
using WebAPI_Levinci.Models;
using WebAPI_Levinci.Repository;

namespace WebAPI_Levinci.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly LevinciContext _context;
        private UserRepository _userRepository;

        public UsersController(LevinciContext context)
        {
            _context = context;
            _userRepository = new UserRepository();
        }

        // GET: Users
        [HttpGet(Name = "GetUsers")]
        public IEnumerable<Users> Get()
        {
            //ResponMessages result = new ResponMessages();
            //List<Users> users = _userRepository.GetAll();
            //if (users.Count > 0)
            //{
            //    result.Status = 200;
            //    result.messages = "Get User Info Success!";
            //    result.Data = users;
            //    return new JsonResult(result);
            //}
            //else
            //{
            //    result.Status = 404;
            //    result.messages = "User not found";
            //    return new JsonResult(result);
            //}

            return Enumerable.Range(1, 5).Select(index => new Users
            {
                strID = "1",
                strUserName = "nvntuan",
                strPassword = "123456",
                strName = "Nhat Tuan",
                strRole = "Admin",
                strEmail = "nvntuan123"
            })
            .ToArray();
        }

        //// GET: Users/Details/5
        //[HttpGet(Name = "GetDetails")]
        //public async Task<IActionResult> Details(string id)
        //{
        //    if (id == null || _context.users == null)
        //    {
        //        return NotFound();
        //    }

        //    var users = await _context.users
        //        .FirstOrDefaultAsync(m => m.strID == id);
        //    if (users == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(users);
        //}

        //// GET: Users/Create
        //[HttpGet(Name = "Create")]
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Users/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("strID,strUserName,strPassword,strName,strRole,strEmail")] Users users)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(users);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(users);
        //}

        //// GET: Users/Edit/5
        //[HttpGet(Name = "Update")]
        //public async Task<IActionResult> Edit(string id)
        //{
        //    if (id == null || _context.users == null)
        //    {
        //        return NotFound();
        //    }

        //    var users = await _context.users.FindAsync(id);
        //    if (users == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(users);
        //}

        //// POST: Users/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(string id, [Bind("strID,strUserName,strPassword,strName,strRole,strEmail")] Users users)
        //{
        //    if (id != users.strID)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(users);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!UsersExists(users.strID))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(users);
        //}

        //// GET: Users/Delete/5
        //public async Task<IActionResult> Delete(string id)
        //{
        //    if (id == null || _context.users == null)
        //    {
        //        return NotFound();
        //    }

        //    var users = await _context.users
        //        .FirstOrDefaultAsync(m => m.strID == id);
        //    if (users == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(users);
        //}

        //// POST: Users/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(string id)
        //{
        //    if (_context.users == null)
        //    {
        //        return Problem("Entity set 'LevinciContext.users'  is null.");
        //    }
        //    var users = await _context.users.FindAsync(id);
        //    if (users != null)
        //    {
        //        _context.users.Remove(users);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool UsersExists(string id)
        //{
        //    return (_context.users?.Any(e => e.strID == id)).GetValueOrDefault();
        //}
    }
}
