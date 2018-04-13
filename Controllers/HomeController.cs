using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using wedding_planner.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace wedding_planner.Controllers
{
    public class HomeController : Controller
    {
        private WeddingContext _context;
        public HomeController(WeddingContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [Route("dashboard")]
        public IActionResult Dashboard()
        {
            int? CurrentId = HttpContext.Session.GetInt32("CurrentId");
            // User ReturnedUser = _context.Users.SingleOrDefault(user => user.UserId == CurrentId);
            // ViewBag.User = ReturnedUser;
            List<Wedding> ReturnedWeddings = _context.Weddings.Include(w => w.Guests).ToList();
            ViewBag.Weddings = ReturnedWeddings;
            ViewBag.CurrentId = CurrentId;

            return View();
        }

        [HttpGet]
        [Route("newWedding")]
        public IActionResult NewWedding()
        {
            return View();
        }
        [HttpPost]
        [Route("newuser")]
        public IActionResult NewUser(RegisterViewModel model)
        {
            if(ModelState.IsValid)
            {
                User addUser = new User 
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Password = model.Password,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                };
                _context.Add(addUser);
                _context.SaveChanges();
                HttpContext.Session.SetInt32("CurrentId", addUser.UserId);
                return RedirectToAction("Dashboard");
            }
            ViewBag.errors = ModelState.Values;
            return View("Index");
        }
        [HttpPost]
        [Route("login")]
        public IActionResult Login(string loginEmail, string loginPass)
        {
            User ReturnedUser = _context.Users.SingleOrDefault(user => user.Email == loginEmail);
            if (ReturnedUser != null){
                if(ReturnedUser.Password == loginPass)
                {
                    Console.WriteLine("RETURNED USER ID " + ReturnedUser.UserId);
                    HttpContext.Session.SetInt32("CurrentId", ReturnedUser.UserId);
                    Console.WriteLine("SESSION ID ENTERED ON LOGIN" + HttpContext.Session.GetInt32("CurrentId"));
                    return RedirectToAction("Dashboard");
                }
                else 
                {
                   ViewBag.error = "This email/password combination is not valid";
                    return View("Login"); 
                }
            }else 
            {
                ViewBag.error = "This email is not in use";
                return View("Login");
            }
        }
        [HttpPost]
        [Route("addWedding")]
        public IActionResult AddWedding(WeddingViewModel model)
        {
            if(ModelState.IsValid)
            {
                Wedding addWedding = new Wedding 
                {
                    Names = model.Name1 + " and " + model.Name2,
                    Date = model.Date,
                    UserId = (int) HttpContext.Session.GetInt32("CurrentId"),
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                };
                _context.Add(addWedding);
                _context.SaveChanges();
                return RedirectToAction("Dashboard");
            }
            ViewBag.errors = ModelState.Values;
            return View("NewWedding");
        }
        [HttpGet]
        [Route("wedding/{WeddingId}")]
        public IActionResult SpecificWedding(int WeddingId)
        {
            // Method body
            List<Wedding> ReturnedWeddings = _context.Weddings.Where(wedding => wedding.WeddingId == WeddingId).Include(w => w.Guests).ToList();
            ViewBag.Wedding = ReturnedWeddings[0];
            List<User> ReturnedUsers = _context.Users.ToList();
            ViewBag.AllUsers = ReturnedUsers;
            ViewBag.CurrentId = HttpContext.Session.GetInt32("CurrentId");
            return View();
        }

        [HttpGet]
        [Route("addguest/{WeddingId}")]
        public IActionResult AddGuest(int WeddingId)
        {
            // Method body
             Guest addGuest = new Guest 
            {
                UserId = (int) HttpContext.Session.GetInt32("CurrentId"),
                WeddingId = WeddingId,
            };
            _context.Add(addGuest);
            _context.SaveChanges();
            return RedirectToAction("Dashboard");
        }

        [HttpGet]
        [Route("removeguest/{WeddingId}")]
        public IActionResult RemoveGuest(int WeddingId)
        {
            Guest RetrievedGuest = _context.Guests.SingleOrDefault(guest => guest.UserId == (int) HttpContext.Session.GetInt32("CurrentId") && guest.WeddingId == WeddingId);
            _context.Guests.Remove(RetrievedGuest);
            _context.SaveChanges();
            return RedirectToAction("Dashboard");
        }
        [HttpGet]
        [Route("removewedding/{WeddingId}")]
        public IActionResult RemoveWedding(int WeddingId)
        {
            Wedding RetrievedWedding = _context.Weddings.SingleOrDefault(wedding =>  wedding.WeddingId == WeddingId);
            _context.Weddings.Remove(RetrievedWedding);
            _context.SaveChanges();
            return RedirectToAction("Dashboard");
        }
        [HttpGet]
        [Route("logout")]
        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Dashboard");
        }
    }
    
}
