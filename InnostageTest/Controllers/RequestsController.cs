using System.Linq;
using System.Threading.Tasks;
using InnostageTest.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace InnostageTest.Controllers
{
    [Authorize(Roles = "operator")]
    public class RequestsController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly ApplicationContext _db;

        public RequestsController(UserManager<User> userManager, ApplicationContext db)
        {
            _userManager = userManager;
            _db = db;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View("CreateRequest");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Request request)
        {
            if (!ModelState.IsValid)
            {
                return View("CreateRequest", request);
            }

            try
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                var status = _db.RequestStatuses.FirstOrDefault(s => s.Name == "Зарегистрировано");

                request.Creator = user;
                request.RequestStatus = status;

                _db.Requests.Add(request);
                await _db.SaveChangesAsync();
            }
            catch
            {
                ModelState.AddModelError("", "Возникала ошибка при создании заявки. Проверьте правильность введенных данных.");
                return View("CreateRequest", request);
            }

            return RedirectToAction("ViewRequests");
        }

        [HttpGet]
        [Route("[controller]/View")]
        public async Task<IActionResult> ViewRequests()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            return View(user);
        }

        [HttpGet]
        [Route("[controller]/View/{id}")]
        public IActionResult ViewRequest(int id)
        {
            var request = _db.Requests
                .Include(r => r.RequestStatus)
                .FirstOrDefault(r => r.RequestId == id);

            if(request == null)
            {
                return NotFound();
            }

            return View(request);
        }
    }
}