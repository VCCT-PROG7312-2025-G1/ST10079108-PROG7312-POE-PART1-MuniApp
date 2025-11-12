using Microsoft.AspNetCore.Mvc;
using MuniApp.Models;
using MuniApp.Models.MuniApp.Models;
using System.Diagnostics;

namespace MuniApp.Controllers
{
    public class IssuesController : Controller
    {
       
        private static Dictionary<int, Issue> issueDict = new Dictionary<int, Issue>();
        private static int nextId = 1;

        public IssuesController()
        {
            
            if (!issueDict.Any())
            {
                issueDict[nextId] = new Issue
                {
                    Id = nextId++,
                    Location = "Central Park",
                    Category = "Infrastructure",
                    Description = "Broken street light near the fountain.",
                    Priority = 3, 
                    Status = "Pending",
                    ImagePath = "/images/bgp4.jpg"
                };

                issueDict[nextId] = new Issue
                {
                    Id = nextId++,
                    Location = "Downtown Clinic",
                    Category = "Health",
                    Description = "Medical waste disposal issue in back alley.",
                    Priority = 2,
                    Status = "In Progress",
                    ImagePath = "/images/bgp4.jpg"
                };

                issueDict[nextId] = new Issue
                {
                    Id = nextId++,
                    Location = "Riverside Road",
                    Category = "Sanitation",
                    Description = "Overflowing garbage bin near bus stop.",
                    Priority = 1, 
                    Status = "In Progress",
                    ImagePath = "/images/bgp4.jpg"
                };

                issueDict[nextId] = new Issue
                {
                    Id = nextId++,
                    Location = "Maple Street School",
                    Category = "Education",
                    Description = "Damaged playground equipment needs repair.",
                    Priority = 2,
                    Status = "In Progress",
                    ImagePath = "/images/bgp4.jpg"
                };

                issueDict[nextId] = new Issue
                {
                    Id = nextId++,
                    Location = "City Hospital",
                    Category = "Health",
                    Description = "Shortage of medical supplies in emergency ward.",
                    Priority = 3, 
                    Status = "In Progress",
                    ImagePath = "/images/bgp4.jpg"
                };

                issueDict[nextId] = new Issue
                {
                    Id = nextId++,
                    Location = "Oak Avenue",
                    Category = "Infrastructure",
                    Description = "Potholes causing traffic congestion.",
                    Priority = 3, 
                    Status = "Complete",
                    ImagePath = "/images/bgp4.jpg"
                };

                issueDict[nextId] = new Issue
                {
                    Id = nextId++,
                    Location = "Riverside Park",
                    Category = "Environment",
                    Description = "Illegal dumping of waste near the riverbank.",
                    Priority = 2,
                    Status = "Complete",
                    ImagePath = "/images/bgp4.jpg"
                };

                issueDict[nextId] = new Issue
                {
                    Id = nextId++,
                    Location = "Main Street",
                    Category = "Security Risk",
                    Description = "Broken traffic signal causing accidents at intersection.",
                    Priority = 3, 
                    Status = "In Progress",
                    ImagePath = "/images/bgp4.jpg"
                };
            }
        }
        public IActionResult ListIssues()
        {
            
            var issues = issueDict.Values.ToList();
            return View(issues);

        }

        public IActionResult ReportIssues()
        {
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ReportIssues(Issue issue, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                
                if (file != null && file.Length > 0)
                {
                    var filePath = Path.Combine("wwwroot/images", file.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    issue.ImagePath = "/images/" + file.FileName;
                }

                issue.Id = nextId++;
                issueDict[issue.Id] = issue;
                issue.Status = "Pending";
                issue.Priority = new Random().Next(1, 3); 
                return RedirectToAction("ListIssues");
            }

            return View(issue);
        }

        public IActionResult Dashboard(string searchQuery)
        {
            var issues = issueDict.Values.AsEnumerable();


            
            if (!string.IsNullOrEmpty(searchQuery))
            {
                issues = issues.Where(i =>
                    i.Id.ToString().Contains(searchQuery) ||
                    i.Category.Contains(searchQuery, StringComparison.OrdinalIgnoreCase) ||
                    i.Location.Contains(searchQuery, StringComparison.OrdinalIgnoreCase) ||
                    i.Description.Contains(searchQuery, StringComparison.OrdinalIgnoreCase));
            }

            var categoryCounts = issueDict.Values
                .GroupBy(i => i.Category)
                .ToDictionary(g => g.Key, g => g.Count());

            var maxHeap = new MaxBinHeap();

            foreach (var issue in issueDict.Values)
            {
                var node = new Node(issue);
                maxHeap.Insert(node);
            }

            var topIssues = new List<Issue>();
            for (int i = 0; i < 3 && maxHeap.Count > 0; i++)
            {
                topIssues.Add(maxHeap.ExtractMax());
            }

            ViewBag.CategoryCounts = categoryCounts;
            ViewBag.TopIssues = topIssues;
            ViewBag.SearchQuery = searchQuery;

            return View(issues.ToList());
        }


    }
}
