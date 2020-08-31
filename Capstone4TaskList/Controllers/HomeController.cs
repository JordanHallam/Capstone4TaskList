using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Capstone4TaskList.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Capstone4TaskList.Controllers
{
    public class HomeController : Controller
    {
        private readonly aspnetCapstone4TaskListContext _context;
        public HomeController(aspnetCapstone4TaskListContext context)
        {
            _context = context;
        }
        
       [HttpGet]
       public IActionResult AddThingToDo()
        {
            return View();
        }
        
        
        [HttpPost]
        public IActionResult AddThingToDo(ThingsToDo thing)
        {
            if (ModelState.IsValid)
            {
                string id = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                thing.UserId = id;
                _context.ThingsToDo.Add(thing);
                _context.SaveChanges();

            }

            return RedirectToAction("ViewThingsToDo");
        }

        public IActionResult Index()
        {

            return View();
        }


        [Authorize]
        public IActionResult ViewThingsToDo()
        {
            string id = User.FindFirst(ClaimTypes.NameIdentifier).Value;
           
            var taskList = _context.ThingsToDo.Where(x => x.UserId == id).ToList();
            return View(taskList);
        }

        public IActionResult DeleteThingToDo(int id)
        {
            var deleteThing = _context.ThingsToDo.Find(id);
            if (deleteThing != null)
            {
                
                _context.ThingsToDo.Remove(deleteThing);
                _context.SaveChanges();
            }

            return RedirectToAction("ViewThingsToDo");
        }
        public IActionResult ChangeCompletion(int idIndex)
        {
            var thing = _context.ThingsToDo.Find(idIndex);
            if (thing != null)
            {
                if (thing.Completion == true)
                {
                    thing.Completion = false;
                }
                else
                {
                    thing.Completion = true;
                }

            _context.Entry(thing).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.Update(thing);
            _context.SaveChanges();
            }
           return RedirectToAction("ViewThingsToDo");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
