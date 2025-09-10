using Microsoft.AspNetCore.Mvc;
using MuniApp.Models;
using System.Diagnostics;

namespace MuniApp.Controllers
{
    public class IssuesController : Controller
    {
       
        private static Dictionary<int, Issue> issueDict = new Dictionary<int, Issue>();
        private static int nextId = 1;

        public IActionResult ListIssues()
        {
            return View(issueDict.Values);
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

                return RedirectToAction("ListIssues");
            }

            return View(issue);
        }


    }
}
