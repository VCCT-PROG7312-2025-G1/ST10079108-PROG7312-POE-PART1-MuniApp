using Microsoft.AspNetCore.Mvc;
using MuniApp.Models;
using System.Collections.Generic;

namespace MuniApp.Controllers
{
    public class UpdateController : Controller
    {
        private static Dictionary<int, Update> updatesDict = new Dictionary<int, Update>
        {
            {1, new Update{ Id = 1,
                    Title = "Community Cleanup Drive",
                    Description = "Join us this Saturday for a cleanup at Riverside Park.",
                    Category = "Environment",
                    Type = "Event",
                    EventDate = DateTime.Now.AddDays(3),
                    DateCreated = DateTime.Now.AddDays(-2)  }
            }, 
            {2, new Update{Id = 2,
                    Title = "New Library Opening",
                    Description = "The new downtown library is now open to the public.",
                    Category = "Education",
                    Type = "Announcement",
                    EventDate = DateTime.Now.AddDays(-1),
                    DateCreated = DateTime.Now.AddDays(-5) }
            }, {3, new Update{Id = 3,
                    Title = "Road Repairs on Main Street",
                    Description = "Expect delays due to road repairs scheduled this week.",
                    Category = "Infrastructure",
                    Type = "Event",
                    EventDate = DateTime.Now.AddDays(2),
                    DateCreated = DateTime.Now.AddDays(-4)}}
            
        };
        private static Stack<Update> recentlyViewed = new();
        private static HashSet<string> categories = new HashSet<string>{"Community","Infrastructure","Health & Safety","Environment","Education"};
        private static int nextId = 4;

        //public IActionResult ListUpdates()
        //{
        //    return View(updatesDict.Values);
        //}

        public IActionResult CreateUpdate()
        {
            ViewBag.Categories = new HashSet<string>
    {
        "Community",
        "Sports",
        "Education",
        "Health",
        "Environment"
    };
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateUpdate(Update update)
        {
            if (ModelState.IsValid)
            {
                update.Id = nextId++;
                updatesDict[update.Id] = update;

                if (!string.IsNullOrWhiteSpace(update.Category))
                    categories.Add(update.Category);

                return RedirectToAction("ListUpdates");
            }

            return View(update);
        }

        public IActionResult ViewUpdate(int id)
        {
            if (updatesDict.TryGetValue(id, out var update))
            {
                recentlyViewed.Push(update);
                return View(update);
            }

            return NotFound();
        }

        public IActionResult ListUpdates(string searchQuery, DateTime? searchDate, string sortBy)
        {
            
            var updates = updatesDict.Values.AsQueryable();



            if (!string.IsNullOrEmpty(searchQuery))
            {
                updates = updates.Where(u =>
                    u.Title.Contains(searchQuery, StringComparison.OrdinalIgnoreCase) ||
                    u.Category.Contains(searchQuery, StringComparison.OrdinalIgnoreCase) ||
                    u.Type.Contains(searchQuery, StringComparison.OrdinalIgnoreCase));
            }

            if (searchDate.HasValue)
            {
                updates = updates.Where(u =>
                    (u.EventDate.HasValue && u.EventDate.Value.Date == searchDate.Value.Date) ||
                    u.DateCreated.Date == searchDate.Value.Date);
            }

            if (sortBy == "EventDate")
            {
                updates = updates.OrderBy(u => u.EventDate);
            }
            else if (sortBy == "CreatedDate")
            {
                updates = updates.OrderByDescending(u => u.DateCreated);
            }
            else if (sortBy == "Category")
            {
                updates = updates.OrderBy(u => u.Category);
            }
            else if (sortBy == "Title")
            {
                updates = updates.OrderBy(u => u.Title);
            }

            
            ViewBag.Categories = categories;

            ViewBag.RecentlyViewed = recentlyViewed.Take(5).ToList();

            return View(updates.ToList());
        }
    }
}
