using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AspNetUserRolesEntityFramework.Models;
using AspNetUserRolesEntityFramework.Data;
using Microsoft.EntityFrameworkCore;

namespace AspNetUserRolesEntityFramework.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;


        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            

            // return View(await _context.MachineLearningCompaniesFeedback.ToListAsync());

            if (!User.Identity.IsAuthenticated)
            {
                foreach (var post in _context.MachineLearningCompaniesFeedback)
                {
                    post.canIncreaseLike = true;
                    post.canIncreaseDislike = true;
                }
                await _context.SaveChangesAsync();
            }


            var allDiscussions = from result in _context.MachineLearningCompaniesFeedback
                                 orderby result.PostDate descending
                                 select result;

            return View(await allDiscussions.ToListAsync());
        }

        public IActionResult About()
        {
            return View();
        }

        
        public async Task<IActionResult> Privacy()
        {
            if (!User.IsInRole("Manager")) {
                return RedirectToAction("Index");
            }
           
                ViewData["Message"] = "Your application description page.";

            // return View(await _context.MachineLearningCompaniesFeedback.ToListAsync());

            if (!User.Identity.IsAuthenticated)
            {
                foreach (var post in _context.MachineLearningCompaniesFeedback)
                {
                    post.canIncreaseLike = true;
                    post.canIncreaseDislike = true;
                }
                await _context.SaveChangesAsync();
            }


            var allDiscussions = from result in _context.MachineLearningCompaniesFeedback
                                 orderby result.PostDate descending
                                 select result;

            return View(await allDiscussions.ToListAsync());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
